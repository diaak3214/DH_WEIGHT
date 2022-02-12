using System;
using System.Xml;
using System.IO;
using System.Reflection;
using System.Net;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Diagnostics;

namespace MobisPDA.Common
{
    public class UpdateManager
    {
        public static string UPDATE_LIST_FILE_NAME = "UpdateFiles.xml";
        public static frmLogin _FrmLogin_Manage = null;        

        /// <summary>
        /// Makes the file ver list to update.
        /// </summary>
        /// <param name="currentFileVerList">The current file ver list.</param>
        /// <param name="serverFileVerList">The server file ver list.</param>
        /// <returns></returns>
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
        /// Gets the file ver list.
        /// </summary>
        /// <param name="updateFileDoc">The update file doc.</param>
        /// <returns></returns>
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

        /// <summary>
        /// Gets the update url.
        /// </summary>
        /// <param name="clientDoc">The client doc.</param>
        /// <returns></returns>

        private static string GetUpdateUrl(XmlDocument clientDoc)
        {
            XmlElement clientDocRoot = clientDoc.DocumentElement;
            XmlNode nodeUpdateUrl = null;
            nodeUpdateUrl = clientDocRoot.SelectSingleNode("descendant::UpdateUrl");
            return nodeUpdateUrl.Attributes["Url"].InnerText;

        }
        public static string GetServiceUrl(XmlDocument clientDoc)
        {
            XmlElement clientDocRoot = clientDoc.DocumentElement;
            XmlNode nodeUpdateUrl = null;
            nodeUpdateUrl = clientDocRoot.SelectSingleNode("descendant::ServiceUrl");
            return nodeUpdateUrl.Attributes["Url"].InnerText;
        }

        public static string GetSVRIP(XmlDocument clientDoc)
        {
            XmlElement clientDocRoot = clientDoc.DocumentElement;
            XmlNode nodeUpdateUrl = null;
            nodeUpdateUrl = clientDocRoot.SelectSingleNode("descendant::ServerIP");
            return nodeUpdateUrl.Attributes["Value"].InnerText;
        }

        public static string GetPort(XmlDocument clientDoc)
        {
            XmlElement clientDocRoot = clientDoc.DocumentElement;
            XmlNode nodeUpdateUrl = null;
            nodeUpdateUrl = clientDocRoot.SelectSingleNode("descendant::ServerPort");
            return nodeUpdateUrl.Attributes["Value"].InnerText;
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
