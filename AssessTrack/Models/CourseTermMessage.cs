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
using System.Web.Mvc;
using System.Collections.Generic;
using System.Data.Linq;
using AssessTrack.Helpers;
using AssessTrack.Backup;

namespace AssessTrack.Models
{
    [Bind(Include="Subject,Body")]
    public partial class CourseTermMessage: IBackupItem
    {
        public bool IsValid
        {
            get { return (GetRuleViolations().Count() == 0); }
        }

        public IEnumerable<RuleViolation> GetRuleViolations()
        {
            AssessTrackDataRepository dataRepository = new AssessTrackDataRepository();

            if (String.IsNullOrEmpty(Subject))
                yield return new RuleViolation("Subject is required", "Subject");
            if (String.IsNullOrEmpty(Body))
                yield return new RuleViolation("Body is required", "Body");

            yield break;
        }

        partial void OnValidate(ChangeAction action)
        {
            if (!IsValid)
                throw new RuleViolationException("Rule violations prevent saving");
        }

        #region IBackupItem Members

        public XElement Serialize()
        {
            throw new NotImplementedException();
        }

        public void Deserialize(XElement source)
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }

        #endregion
    }
}
