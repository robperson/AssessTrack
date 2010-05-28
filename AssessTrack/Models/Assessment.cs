using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Data.Linq;
using System.Collections.Generic;
using System.Web.Mvc;
using AssessTrack.Helpers;
using System.Xml.Xsl;
using System.Xml;
using System.IO;

namespace AssessTrack.Models
{
    [Bind(Include="AllowMultipleSubmissions,Name,DueDate,IsExtraCredit,AssessmentTypeID,Data,CreatedDate,IsVisible,IsOpen,IsGradable")]
    public partial class Assessment
    {
        public bool IsValid
        {
            get { return (GetRuleViolations().Count() == 0); }
        }

        public IEnumerable<RuleViolation> GetRuleViolations()
        {
            AssessTrackDataRepository dataRepository = new AssessTrackDataRepository();
            if (String.IsNullOrEmpty(Name))
                yield return new RuleViolation("Name is required", "Name");

            if (string.IsNullOrEmpty(Data))
                yield return new RuleViolation("Data cannot be empty", "Data");

            if (CourseTerm != null)
            {
                int nameCount = CourseTerm.Assessments.Count(a => a.Name == Name);
                if (nameCount > 1)
                {
                    yield return new RuleViolation(@"An Assessment named """ + Name + "\" already exists for this course/term", "Name");
                }
            }

            //Ensure that we aren't deleting an answer and leaving dangling responses
            if (AssessmentID != null && AssessmentID != Guid.Empty)
            {
                XmlDocument data = new XmlDocument();
                data.LoadXml(Data);
                foreach (Answer answer in Answers)
                {
                    if (answer.Responses.Count() > 0)
                    {
                        //make sure this answer is present in the markup if it has responses
                        XmlNode answerNode = data.SelectSingleNode(string.Format("//answer[@id='{0}']", answer.AnswerID));
                        if (answerNode == null)
                        {
                            yield return new RuleViolation("Answers cannot be removed from an assessment without removing their corresponding responses first.", "Data");
                            break;
                        }
                    }
                }
            }

            yield break;
        }

        partial void OnValidate(ChangeAction action)
        {
            if (!IsValid)
                throw new RuleViolationException("Rule violations prevent saving");
        }

        public double Weight
        {
            get { return !IsExtraCredit ? Questions.Sum(q => q.Weight) : 0; }
            
        }
    }
}
