using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AssessTrack.Models;

namespace AssessTrack.Helpers
{
    public static class AssessmentHelpers
    {
        public static bool StudentHasExtension(Assessment assessment, Guid StudentID)
        {
            SubmissionException exc = (from se in assessment.SubmissionExceptions
                                       where se.StudentID == StudentID
                                       select se).SingleOrDefault();
            if (exc != null && exc.DueDate.CompareTo(DateTime.Now) > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
