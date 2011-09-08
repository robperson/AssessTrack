using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AssessTrack.Models
{
    public partial class AssessTrackDataRepository
    {
        public void SaveFile(File file)
        {
            dc.Files.InsertOnSubmit(file);
            //dc.SubmitChanges();
        }

        public File GetFileByID(Guid id)
        {
            File file = dc.Files.SingleOrDefault(f => f.FileID == id);
            return file;
        }

        public void DeleteFile(File file)
        {
            foreach (var courseTerm in file.CourseTerms)
            {
                courseTerm.Syllabus = null;
            }

            dc.CourseTermFiles.DeleteAllOnSubmit(file.CourseTermFiles);

            dc.Files.DeleteOnSubmit(file);

            dc.SubmitChanges();
        }
    }
}
