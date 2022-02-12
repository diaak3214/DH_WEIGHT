using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.IO;
using System.Reflection;
using System.Net;
using System.Diagnostics;

namespace OctoUpdate
{
    public partial class FrmLiveUpdate : Form
    {
        private readonly string UPDATE_LIST_FILE_NAME = "UpdateFiles.xml";

        public FrmLiveUpdate()
        {
            InitializeComponent();
        }

        private void FrmLiveUpdate_Load(object sender, EventArgs e)
        {
            try
            {
                // 화면을 초기화한다
                InitControls();

                // 업데이트 파일을 검사한다
                this.timer1.Interval = 8000;
                this.timer1.Enabled = true;
            }
            catch
            {
                MessageBox.Show("화면 초기화 오류", this.Text);
            }

        }
        private void InitControls()
        {
            this.ProgressBar.Maximum = 100;
            this.ProgressBar.Minimum = 0;
            this.ProgressBar.Value = 0;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.timer1.Enabled = false;

            string clientFolder = Application.StartupPath + @"\";

            try
            {
                // 서버 및 클라이언트의 업데이트 폴더를 구한다
                if (ProgressBar.Value + 2 <= 100)
                    ProgressBar.Value += 2;

                // 클라이언트에서 업데이트 파일의 목록을 정의한 XML 파일을 로드한다
                XmlDocument clientDoc = new XmlDocument();
                clientDoc.Load(clientFolder + UPDATE_LIST_FILE_NAME);

                // 서버의 업데이트 경로를 구한다
                string updateUrl = GetUpdateUrl(clientDoc);
                // 서버에서 업데이트 파일의 목록을 정의한 XML 파일을 로드한다
                XmlDocument serverDoc = new XmlDocument();
                serverDoc.Load(updateUrl + UPDATE_LIST_FILE_NAME);

                if (ProgressBar.Value + 2 <= 100)
                    ProgressBar.Value += 2;

                // 파일버전 목록을 만든다
                FileVersionInfo currentFileVerList = GetFileVerList(clientDoc);
                FileVersionInfo serverFileVerList = GetFileVerList(serverDoc);

                // 로컬의 파일버전 목록과 서버의 파일버전 목록을 비교하여
                // 업데이트 할 목록을 생성한다
                FileVersionInfo toUpdateFileVerList = MakeFileVerListToUpdate(clientFolder, currentFileVerList, serverFileVerList);

                //  업데이트할 파일이 존재하면 다운로드 받는다
                if (toUpdateFileVerList.Count > 0)
                {
                    int Incvalue = (ProgressBar.Maximum - ProgressBar.Value) / toUpdateFileVerList.Count;
                    lblStatus.Text = "최신 버전을 다운로드 합니다...";
                    lblStatus.Update();
                    int count = 0;
                    int totalCount = toUpdateFileVerList.Keys.Count;
                    string shortFileName = string.Empty;
                    foreach (string fileName in toUpdateFileVerList.Keys)
                    {
                        count++;
                        shortFileName = GetFileNameOnly(fileName);
                        lblStatus.Text = string.Format("{0}/{1} {2} 파일을 다운로드 합니다.",
                            count.ToString(), totalCount.ToString(), shortFileName);
                        lblStatus.Update();

                        DownloadFile(updateUrl + fileName, clientFolder + shortFileName);
                        if (ProgressBar.Value + Incvalue <= 100)
                            ProgressBar.Value += Incvalue;
                    }
                    serverDoc.Save(clientFolder + UPDATE_LIST_FILE_NAME);
                    lblStatus.Text = "업데이트가 완료되었습니다...";
                }
                else
                {
                    lblStatus.Text = "업데이트할 파일이 없습니다...";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("업데이트 중 오류가 발생했습니다." + "\r\n" + ex.ToString(), this.Text);
            }
            finally
            {
                lblStatus.Update();
                ProgressBar.Value =100;

                lblStatus.Text = "프로그램을 실행합니다...";
                lblStatus.Update();

                // Application을 실행하고, LiveUpdate를 종료한다
                //2019-12-03 한민호
                Process.Start(clientFolder + "OCT_WEIGHT.Manager.exe", string.Empty);
                //Process.Start(clientFolder + "OCTO.INSPECT.MAIN.exe", string.Empty);
                Close();
            }

        }

        private void FrmLiveUpdate_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
        private string GetFileNameOnly(string fileName)
        {
            string[] names = fileName.Split('/');
            return names[names.Length - 1];
        }

        /// <summary>
        /// Makes the file ver list to update.
        /// </summary>
        /// <param name="currentFileVerList">The current file ver list.</param>
        /// <param name="serverFileVerList">The server file ver list.</param>
        /// <returns></returns>
        private FileVersionInfo MakeFileVerListToUpdate(string clientFolder, FileVersionInfo currentFileVerList, FileVersionInfo serverFileVerList)
        {
            FileVersionInfo result = new FileVersionInfo();

            foreach (string fileName in serverFileVerList.Keys)
            {
                // File Version이 다르거나 PDA 로컬에 파일이 존재하지 않으면
                // Update 목록에 추가
                if (serverFileVerList[fileName] != currentFileVerList[fileName]
                    || !File.Exists(clientFolder + GetFileNameOnly(fileName)))
                {
                    result.Add(fileName, serverFileVerList[fileName]);
                    if (ProgressBar.Value + 1 <= 100)
                        ProgressBar.Value += 1;
                }
            }

            return result;
        }

        /// <summary>
        /// Gets the file ver list.
        /// </summary>
        /// <param name="updateFileDoc">The update file doc.</param>
        /// <returns></returns>
        private FileVersionInfo GetFileVerList(XmlDocument updateFileDoc)
        {
            FileVersionInfo result = new FileVersionInfo();

            XmlNode updateFilesNode = updateFileDoc.DocumentElement;
            for (int i = 2; i <= updateFilesNode.ChildNodes.Count - 1; i++)	// UpdateUrl, ServiceURL Node는 제외하고 다음 Node부터 시작
            {
                XmlNode filesNode = updateFilesNode.ChildNodes[i];
                if (filesNode.Attributes["Name"] == null) continue;
                if (filesNode.Attributes["Name"].Value == "ApplicationFiles")
                {
                    for (int j = 0; j <= filesNode.ChildNodes.Count - 1; j++)
                    {
                        XmlNode fileNode = filesNode.ChildNodes[j];
                        result.Add(fileNode.Attributes["Name"].InnerText, fileNode.InnerText);

                        if (ProgressBar.Value + 2 <= 100)
                            ProgressBar.Value += 2;
                    }
                }
            }

            return result;
        }


        /// <summary>
        /// Gets the update url.
        /// </summary>
        /// <param name="clientDoc">The client doc.</param>
        /// <returns></returns>
        private string GetUpdateUrl(XmlDocument clientDoc)
        {
            XmlElement clientDocRoot = clientDoc.DocumentElement;
            XmlNode nodeUpdateUrl = null;
            nodeUpdateUrl = clientDocRoot.SelectSingleNode("descendant::UpdateUrl");
            return nodeUpdateUrl.Attributes["Url"].InnerText;

            //XmlElement clientDocRoot = clientDoc.DocumentElement;
            //XmlNode nodeUpdateUrl
            //    = clientDocRoot.SelectSingleNode("descendant::UpdateUrl");
            //return nodeUpdateUrl.Attributes["Url"].InnerText;
        }

        /// <summary>
        /// 파일을 다운로드한다
        /// </summary>
        /// <param name="serverFileName">Name of the server file.</param>
        /// <param name="clientFileName">Name of the client file.</param>
        private void DownloadFile(string serverFileName, string clientFileName)
        {
            // 디버깅시 주석 해제하여 실행
            /*string fileInfo = "Server File : '" + serverFileName + "'\r\n";
            fileInfo += "Client File : '" + clientFileName + "'";

            MessageBox.Show(fileInfo, "Download...");*/

            HttpWebResponse wrep = null;
            FileStream fs = null;

            try
            {
                HttpWebRequest wreq = (HttpWebRequest)HttpWebRequest.Create(serverFileName);

                wrep = (HttpWebResponse)wreq.GetResponse();

                Stream s = wrep.GetResponseStream();
                s.Flush();

                int BuffSize = 4096;
                byte[] buff = new byte[BuffSize];

                fs = new FileStream(clientFileName, FileMode.Create, FileAccess.Write);
                while ((BuffSize = s.Read(buff, 0, 4096)) >= 1)
                {
                    fs.Flush();
                    fs.Write(buff, 0, BuffSize);
                }
            }
            catch (Exception ex)
            {
                if (fs != null) fs.Close();
                if (wrep != null) wrep.Close();
            }
            finally
            {
                if (fs != null) fs.Close();
                if (wrep != null) wrep.Close();
            }
        }

    }
}