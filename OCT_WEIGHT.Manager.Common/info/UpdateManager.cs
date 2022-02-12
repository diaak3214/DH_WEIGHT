using System.Xml;
using System.IO;
using System.Net;
using System.Windows.Forms;
//업데이트 추가(2019-12-03 한민호)
using System;
using System.Diagnostics;

namespace OCT_WEIGHT.Manager.Common
{
    public class UpdateManager
    {
        public static string UPDATE_LIST_FILE_NAME = "UpdateFiles.xml";

        //업데이트 추가(2019-12-03 한민호)
        public static bool closeActionFromLiveUpdate = false;

        public static void Update(XmlDocument clientDoc)
        {
            string clientFolder = Application.StartupPath + @"\";

            try
            {
                // 서버의 업데이트 경로를 구한다
                string updateUrl = GetUpdateUrl(clientDoc);
                // 서버에서 업데이트 파일의 목록을 정의한 XML 파일을 로드한다
                XmlDocument serverDoc = new XmlDocument();
                serverDoc.Load(updateUrl + UPDATE_LIST_FILE_NAME);

                // 파일버전 목록을 만든다
                FileVersionInfo currentFileVerList = GetFileVerList(clientDoc);
                FileVersionInfo serverFileVerList = GetFileVerList(serverDoc);

                // 로컬의 파일버전 목록과 서버의 파일버전 목록을 비교하여
                // 업데이트 할 목록을 생성한다
                FileVersionInfo toUpdateFileVerList = MakeFileVerListToUpdate(clientFolder, currentFileVerList, serverFileVerList);

                //  업데이트할 목록이 존재하면 다운로드 받는다
                if (toUpdateFileVerList.Count > 0)
                {
                    if (MessageBox.Show("최신버전이 존재합니다." + "\r\n" + "업데이트하시겠습니까?", "Live UPdate", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Cancel)
                        return;
                    Process.Start(clientFolder + "OctoUpdateV2.exe", string.Empty);
                    closeActionFromLiveUpdate = true;
                    Application.Exit();
                }
            }

                    //foreach (string fileName in toUpdateFileVerList.Keys)
            //{
            //    DownloadFile(updateUrl + fileName, clientFolder + GetFileNameOnly(fileName));
            //}

                    //// 새로운 UpdateFiles.xml을 로컬에 저장한다
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private static FileVersionInfo GetFileVerList(XmlDocument updateFileDoc)
        {
            FileVersionInfo result = new FileVersionInfo();

            XmlNode updateFilesNode = updateFileDoc.DocumentElement;
            for (int i = 0; i <= updateFilesNode.ChildNodes.Count - 1; i++)	// UpdateUrl Node는 제외하고 다음 Node부터 시작
            {
                XmlNode filesNode = updateFilesNode.ChildNodes[i];
                if (filesNode.Attributes["Name"] == null) continue;
                if (filesNode.Attributes["Name"].Value == "ApplicationFiles")
                {
                    for (int j = 0; j <= filesNode.ChildNodes.Count - 1; j++)
                    {
                        XmlNode fileNode = filesNode.ChildNodes[j];
                        result.Add(fileNode.Attributes["Name"].InnerText, fileNode.InnerText);
                    }
                }
            }

            return result;
        }

        private static FileVersionInfo MakeFileVerListToUpdate(string clientFolder, FileVersionInfo currentFileVerList, FileVersionInfo serverFileVerList)
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
                }
            }

            return result;
        }

        /// <summary>
        /// Gets the update url.
        /// </summary>
        /// <param name="clientDoc">The client doc.</param>
        /// <returns></returns>

        public static string GetUpdateUrl(XmlDocument clientDoc)
        {
            XmlElement clientDocRoot = clientDoc.DocumentElement;
            XmlNode nodeUpdateUrl = null;
            nodeUpdateUrl = clientDocRoot.SelectSingleNode("descendant::DeployServerUrl");
            return nodeUpdateUrl.Attributes["Url"].InnerText;

        }
        public static string GetServiceUrl(XmlDocument clientDoc)
        {
            XmlElement clientDocRoot = clientDoc.DocumentElement;
            XmlNode nodeUpdateUrl = null;
            nodeUpdateUrl = clientDocRoot.SelectSingleNode("descendant::ServiceUrl");
            return nodeUpdateUrl.Attributes["Url"].InnerText;
        }
       
        private static string GetFileNameOnly(string fileName)
        {
            string[] names = fileName.Split('/');
            return names[names.Length - 1];
        }

        /// <summary>
        /// 파일을 다운로드한다
        /// </summary>
        /// <param name="serverFileName">Name of the server file.</param>
        /// <param name="clientFileName">Name of the client file.</param>
        private static void DownloadFile(string serverFileName, string clientFileName)
        {
            // 디버깅시 주석 해제하여 실행
            string fileInfo = "Server File : '" + serverFileName + "'\r\n";
            fileInfo += "Client File : '" + clientFileName + "'";
            MessageBox.Show(fileInfo, "Download...");

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
            finally
            {
                if (fs != null) fs.Close();
                if (wrep != null) wrep.Close();
            }
        }
    }
}
