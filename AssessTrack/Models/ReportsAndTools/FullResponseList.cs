using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Linq;

namespace AssessTrack.Models.ReportsAndTools
{
    public class FullResponseList
    {
        public List<Response> GetFullResponseList(Guid? AnswerID)
        {
            if (AnswerID == null)
                throw new ArgumentNullException("AnswerID");
            AssessTrackModelClassesDataContext dc = new AssessTrackModelClassesDataContext();
            DataLoadOptions options = new DataLoadOptions();
            options.LoadWith<Answer>(answer => answer.Responses);
            options.LoadWith<Answer>(answer => answer.Question);
            options.LoadWith<Response>(response => response.SubmissionRecord);
            options.LoadWith<SubmissionRecord>(submission => submission.Profile);
            options.LoadWith<SubmissionRecord>(submission => submission.Assessment);

            dc.LoadOptions = options;

            Answer _answer = (from ans in dc.Answers
                             where ans.AnswerID == AnswerID.Value
                             select ans).First();

            return _answer.Responses.ToList();

        }
    }
}
