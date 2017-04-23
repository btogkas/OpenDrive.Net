using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using OpenDrive.File;
using OpenDrive.Folders;
using OpenDrive.Upload;

namespace OpenDrive.Entities
{
    public class ODFolder
    {

        #region Properties

        [JsonIgnore]
        public string FolderName => RemotePath.Substring(RemotePath.LastIndexOf(@"\", StringComparison.Ordinal) + 1);

        public string RemotePath { get; set; }
        public string FolderId { get; set; }
        private readonly List<ODFile> _files = new List<ODFile>();
        public List<ODFile> Files
        {
            get { return _files; }
        }
        private readonly List<ODFolder> _folders = new List<ODFolder>();
        public List<ODFolder> Folders
        {
            get { return _folders; }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ODFolder"/> class.
        /// </summary>
        public ODFolder()
        {
            RemotePath = "Uploads";
        }

        #endregion

        #region Methods

        /// <summary>
        /// Creates the folder.
        /// </summary>
        /// <param name="sessionId">The session identifier.</param>
        /// <param name="parent">The parent.</param>
        /// <returns></returns>
        public ODFolder CreateFolder(string sessionId, string parent)
        {
            CreateFolderRequest request = new CreateFolderRequest
            {
                SessionId = sessionId,
                FolderSubParent = parent,
                FolderName = FolderName
            };

            FolderResponse response = request.Call();
            ODFolder folder = new ODFolder
            {
                FolderId = response.FolderID,
                RemotePath = FolderName
            };
            return folder;
        }

        /// <summary>
        /// Creates the or get folder.
        /// </summary>
        /// <param name="sessionId">The session identifier.</param>
        /// <returns></returns>
        public string CreateOrGetFolder(string sessionId)
        {
            if (string.IsNullOrEmpty(FolderId))
            {

                FolderResponse response = GetFolder(sessionId);
                if (response == null)
                {
                    ////Get the folder subparent
                    string subParentId = "";

                    List<string> dirs = RemotePath.Split(new string[] { @"\" }, StringSplitOptions.None).ToList();
                    if (dirs.Count > 1)
                    {
                        string path = RemotePath;
                        int missingChildDirsCount = dirs.Count - 1;
                        while (missingChildDirsCount != 0 && string.IsNullOrEmpty(subParentId))
                        {
                            path = path.Substring(0, path.LastIndexOf(@"\", StringComparison.Ordinal));
                            ODFolder folder = new ODFolder
                            {
                                RemotePath = path
                            };
                            FolderResponse resp = folder.GetFolder(sessionId);
                            if (resp != null)
                            {
                                subParentId = resp.FolderID;
                                break;
                            }
                            missingChildDirsCount--;
                        }

                        while (missingChildDirsCount < dirs.Count)
                        {
                            if (missingChildDirsCount == 0)
                            {
                                subParentId = "0";
                            }

                            string existingFolderPath = "";
                            for (int j = 0; j < missingChildDirsCount; j++)
                            {
                                existingFolderPath += dirs[j] + @"\";
                            }


                            ODFolder parentFolder = new ODFolder
                            {
                                RemotePath = existingFolderPath + dirs[missingChildDirsCount]
                            };
                            ODFolder childFolder = parentFolder.CreateFolder(sessionId, subParentId);
                            subParentId = childFolder.FolderId;

                            parentFolder.Folders.Add(childFolder);

                            missingChildDirsCount++;
                        }
                        FolderId = subParentId;



                    }
                    else
                    {
                        FolderId = "0";
                    }


                }
                else
                {
                    FolderId = response.FolderID;
                }

            }
            return FolderId;
        }

        /// <summary>
        /// Uploads the file.
        /// </summary>
        /// <param name="sessionId">The session identifier.</param>
        /// <param name="fileName">Name of the file.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentException">Cannot Upload file without having the remote path.</exception>
        public ODFile UploadFile(string sessionId, string fileName)
        {
            if (String.IsNullOrEmpty(FolderId) && String.IsNullOrEmpty(RemotePath))
            {
                throw new ArgumentException("Cannot Upload file without having the remote path.");
            }


            FileInfo info = new FileInfo(fileName);
            //Get Root Folder
            string folderId = CreateOrGetFolder(sessionId);

            //Upload non existing file on Folder
            Upload.CreateFileRequest req = new Upload.CreateFileRequest
            {
                SessionId = sessionId,
                FolderId = folderId,
                FileName = Path.GetFileName(fileName),
                OpenIfExists = 1,
                FileSize = info.Length,
                FileHash = fileName.CalculateFileMd5()
            };
            var resp = req.Call();

            if (resp.RequireHashOnly != 1)
            {

                OpenFileForUploadRequest offur = new OpenFileForUploadRequest()
                {
                    SessionId = sessionId,
                    FileId = resp.FileId,
                    FileSize = info.Length
                };
                FileResponse offurResp = offur.Call();

                UploadFileChunkRequest upd = new UploadFileChunkRequest
                {
                    SessionId = sessionId,
                    FullFilePath = fileName,
                    FileId = resp.FileId,
                    TempLocation = resp.TempLocation

                };
                UploadFileChunkResponse r2 = upd.Call()[0];
            }

            var close = new CloseUploadRequest
            {
                SessionId = sessionId,
                FileId = resp.FileId,
                FileSize = info.Length,
                TempLocation = resp.TempLocation,
                FileTime = resp.UpdateTime
            };
            var response = close.Call();

            ODFile file = new ODFile
            {
                RemotePath = Path.Combine(RemotePath, fileName),
                FileId = response.FileId
            };

            return file;
        }

        /// <summary>
        /// Gets a Folder Id from the specific Path.
        /// </summary>
        /// <param name="sessionId">The session identifier.</param>
        /// <param name="path">The path.</param>
        /// <returns></returns>
        private FolderResponse GetFolder(string sessionId)
        {
            if (String.IsNullOrEmpty(RemotePath))
            {
                throw new ArgumentException("Cannot get folder without having the remote path.");
            }

            GetFolderIdByPathRequest id = new GetFolderIdByPathRequest
            {
                SessionId = sessionId,
                Path = RemotePath.Replace(@"\","/")
            };
            FolderResponse resp = id.Call();
            if (resp != null)
            {
                FolderId = resp.FolderID;
            }
            return resp;

        }

        /// <summary>
        /// Gets the content.
        /// </summary>
        /// <param name="sessionId">The session identifier.</param>
        /// <param name="searchQuery">The search query.</param>
        /// <returns></returns>
        public ODFolder GetContent(string sessionId, string searchQuery = "")
        {
            GetFolder(sessionId);
            FolderResponse response = new ListFolderContentRequest
            {
                SessionId = sessionId,
                FolderId = FolderId,
                SearchQuery = searchQuery
            }.Call();

            foreach (FileResponse file in response.Files)
            {
                if (Files.All(x => x.FileId != file.FileId))
                {
                    Files.Add(JsonConvert.DeserializeObject<ODFile>(JsonConvert.SerializeObject(file)));
                }
            }

            foreach (FolderResponse folder in response.Folders)
            {
                if (Folders.All(x => x.FolderId != folder.FolderID))
                {
                    Folders.Add(JsonConvert.DeserializeObject<ODFolder>(JsonConvert.SerializeObject(folder)));
                }
            }
            return this;
        }

        /// <summary>
        /// Trashes the specified session identifier.
        /// </summary>
        /// <param name="sessionId">The session identifier.</param>
        public void Delete(string sessionId)
        {
            FolderResponse resp = GetFolder(sessionId);
            new TrashFolderRequest() { SessionId = sessionId, FolderId = resp.FolderID}.Call();
        }

        #endregion

    }
}
