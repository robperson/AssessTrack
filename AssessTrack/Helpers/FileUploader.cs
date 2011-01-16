using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using AssessTrack.Models;

namespace AssessTrack.Helpers
{
    public static class FileUploader
    {
        public static AssessTrack.Models.File GetFile(string filename, HttpRequestBase Request)
        {
            if (Request.Files[filename] == null || Request.Files[filename].ContentLength == 0)
            {
                return null;
            }
            string mimeType = Request.Files[filename].ContentType;
            Stream fileStream = Request.Files[filename].InputStream;
            string fileName = Path.GetFileName(Request.Files[filename].FileName);
            int fileLength = Request.Files[filename].ContentLength;
            byte[] fileData = new byte[fileLength];
            fileStream.Read(fileData, 0, fileLength);

            AssessTrack.Models.File file = new AssessTrack.Models.File();

            file.Data = new System.Data.Linq.Binary(fileData);
            file.Mimetype = mimeType;
            file.OwnerID = UserHelpers.GetCurrentUserID();
            file.Name = fileName;

            return file;
        }

        public static void UpdateFile(string filename, HttpRequestBase Request, AssessTrack.Models.File file)
        {
            if (file == null)
            {
                throw new ArgumentNullException("file");
            }
            if (Request.Files[filename] == null || Request.Files[filename].ContentLength == 0)
            {
                return;
            }
            string mimeType = Request.Files[filename].ContentType;
            Stream fileStream = Request.Files[filename].InputStream;
            string fileName = Path.GetFileName(Request.Files[filename].FileName);
            int fileLength = Request.Files[filename].ContentLength;
            byte[] fileData = new byte[fileLength];
            fileStream.Read(fileData, 0, fileLength);


            file.Data = new System.Data.Linq.Binary(fileData);
            file.Mimetype = mimeType;
            file.OwnerID = UserHelpers.GetCurrentUserID();
            file.Name = fileName;
        }

        public static void SaveFile(AssessTrackDataRepository repo, AssessTrack.Models.File file)
        {
            repo.SaveFile(file);
        }
    }
}
