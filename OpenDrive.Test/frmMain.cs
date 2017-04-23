using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OpenDrive.AccountUsers;
using OpenDrive.Download;
using OpenDrive.Entities;
using OpenDrive.File;
using OpenDrive.Folders;
using OpenDrive.Session;
using OpenDrive.Upload;

namespace OpenDrive.Test
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        /// <summary>
        /// First Login in order to get the session Id
        /// </summary>
        /// <returns></returns>
        private LoginResponse LoginCommand()
        {
            //TODO: Change login credentials here.
            LoginRequest request = new LoginRequest
            {
                UserName = "XXX",
                Password = "XXX",
                Version = "10"
            };
            LoginResponse response = request.Call();
            return response;
        }

        private string GetSessionId()
        {
            string applicationPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            string settingFilePath = Path.Combine(applicationPath, "settings.ini");
            FileInfo info = new FileInfo(settingFilePath);
            string sessionId = "";
            bool needLogin = true;
            if (info.Exists)
            {
                sessionId = System.IO.File.ReadAllText(settingFilePath);
                SessionExistsRequest ser = new SessionExistsRequest { SessionId = sessionId };
                var serR = ser.Call();
                if (serR)
                {
                    needLogin = false;
                }

            }

            if (needLogin)
            {
                LoginResponse response = LoginCommand();
                sessionId = response.SessionID;
            }

            System.IO.File.WriteAllText(settingFilePath, sessionId);
            return sessionId;
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            FullUploadDeleteCycle();
        }

        private void FullUploadDeleteCycle()
        {
            string sessionId = GetSessionId();
            ODFolder folder = new ODFolder {RemotePath = @"Uploads\TestFolder\NestedFolder"};
            //string testFilePath = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), @"Test\TestOneChunk.txt");
            //ODFile file =  folder.UploadFile(sessionId, testFilePath);

            string testFilePath2 = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), @"Test\MultipleUploadChunks.txt");
            ODFile file2 = folder.UploadFile(sessionId, testFilePath2);
            file2.Delete(sessionId);
            //file.DownloadFile(sessionId, "c:\\S\\Target.txt");
            //file.DownloadAsZip(sessionId, "c:\\S\\Target.zip");



        }
        
    }
}
