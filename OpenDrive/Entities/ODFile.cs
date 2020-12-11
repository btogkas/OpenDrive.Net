using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenDrive.Download;
using OpenDrive.File;
using OpenDrive.Folders;
using OpenDrive.Upload;

namespace OpenDrive.Entities
{
    public class ODFile:FileResponse
    {

        public string RemotePath { get; set; }
        private void GetFileInfo(string sessionId)
        {
            if (String.IsNullOrEmpty(FileId))
            {
                if (string.IsNullOrEmpty(RemotePath))
                {
                    throw new ArgumentException("RemotePath or FileId should be provided in order to delete folder");
                }

                GetFileIdRequest req = new GetFileIdRequest()
                {
                    Path = RemotePath,
                    SessionId = sessionId
                };
                var resp = req.Call();
                FileId = resp.FileId;
            }
        }

        

        /// <summary>
        /// Deletes the specified session identifier.
        /// </summary>
        /// <param name="sessionId">The session identifier.</param>
        public void Delete(string sessionId)
        {
            GetFileInfo(sessionId);
            new TrashFileRequest() { SessionId = sessionId, FileId = FileId }.Call();
        }

        /// <summary>
        /// Moves the specified session identifier.
        /// </summary>
        /// <param name="sessionId">The session identifier.</param>
        /// <param name="destinationFolderId">The destination folder identifier.</param>
        /// <param name="overwrite">if set to <c>true</c> [overwrite].</param>
        public void Move(string sessionId, string destinationFolderId,bool overwrite=true)
        {
            GetFileInfo(sessionId);
            MoveCopyFileRequest req = new MoveCopyFileRequest()
            {
                SessionId = sessionId,
                SourceFileId = FileId,
                DestinationFolderId = destinationFolderId,
                Move = true.ToString(),
                Overwrite = overwrite.ToString()
            };
            req.Call();

            //Get the folder path

        }

        /// <summary>
        /// Renames the specified session identifier.
        /// </summary>
        /// <param name="sessionId">The session identifier.</param>
        /// <param name="newFileName">New name of the file.</param>
        public void Rename(string sessionId, string newFileName)
        {
            GetFileInfo(sessionId);
            RenameFileRequest req = new RenameFileRequest
            {
                SessionId = sessionId,
                FileId = FileId,
                NewFileName = newFileName
            };
            req.Call();


        }

        /// <summary>
        /// Downloads the file.
        /// </summary>
        /// <param name="sessionId">The session identifier.</param>
        /// <param name="targetFilePath">The target file path.</param>
        public void DownloadFile(string sessionId, string targetFilePath)
        {
            GetFileInfo(sessionId);
            new DownloadFileRequest()
            {
                SessionId = sessionId,
                FileId = FileId,
                TargetFile = targetFilePath
            }.Call();
        }

        /// <summary>
        /// Downloads as zip.
        /// </summary>
        /// <param name="sessionId">The session identifier.</param>
        /// <param name="targetFilePath">The target file path.</param>
        public void DownloadAsZip(string sessionId, string targetFilePath)
        {
            new DownloadZippedRequest()
            {
                SessionId = sessionId,
                Files = FileId,
                TargetFile = targetFilePath
            }.Call();
        }
    }
}
