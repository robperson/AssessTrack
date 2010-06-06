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
using System.Xml.XPath;
using AssessTrack.Backup;

namespace AssessTrack.Models
{
    public partial class Answer: IBackupItem
    {
        public int Number
        {
            get
            {
                if (Assessment != null)
                {
                    XElement markup = XElement.Parse(Assessment.Data);
                    return Convert.ToInt32(markup.XPathEvaluate(string.Format("count(//answer[@id='{0}']/preceding-sibling::answer) + 1", AnswerID)));
                }
                throw new Exception("Answer cannot have a number until it is assigned to an Assessment.");
            }
        }

        #region IBackupItem Members

        public XElement Serialize()
        {
            XElement answer = 
                new XElement("answer",
                    new XElement("answerid", AnswerID.ToString()),
                    new XElement("weight", Weight),
                    new XElement("questionid", QuestionID.ToString()),
                    new XElement("assessmentid", AssessmentID.ToString()),
                    new XElement("answerkeytext", AnswerKeyText),
                    new XElement("type", this.Type));
            return answer;
        }

        public void Deserialize(XElement source)
        {
            try
            {
                AnswerID = new Guid(source.Element("answerid").Value);
                Weight = Convert.ToDouble(source.Element("weight").Value);
                QuestionID = new Guid(source.Element("questionid").Value);
                AssessmentID = new Guid(source.Element("assessmentid").Value);
                AnswerKeyText = source.Element("answerkeytext").Value;
                this.Type = source.Element("type").Value;
            }
            catch
            {
                throw new Exception("Failed to deserialize Answer entity.");
            }
        }
        
        private Guid _objectID;
        public Guid objectID
        {
            get
            {
                return _objectID;
            }
            set
            {
                _objectID = value;
            }
        }

        public void Insert(AssessTrackModelClassesDataContext dc)
        {
            dc.Answers.InsertOnSubmit(this);
        }

        #endregion
    }
}
