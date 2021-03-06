﻿using System;
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
using AssessTrack.Backup;

namespace AssessTrack.Models
{
    [Bind(Include="AllowMultipleSubmissions,Name,DueDate,IsExtraCredit,AssessmentTypeID,Data,CreatedDate,IsVisible,IsOpen,IsGradable")]
    public partial class Assessment: IBackupItem, ITaggable
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

            

            yield break;
        }

        partial void OnValidate(ChangeAction action)
        {
            if (!IsValid)
            {
                RuleViolation first = GetRuleViolations().First();
                throw new RuleViolationException(first.ErrorMessage);
            }
        }

        public double Weight
        {
            get { return !IsExtraCredit ? Answers.Sum(a => a.Weight) : 0; }
            
        }

        #region IBackupItem Members

        public XElement Serialize()
        {
            XElement assessment =
                new XElement("assessment",
                    new XElement("assessmentid", AssessmentID.ToString()),
                    new XElement("name", Name),
                    new XElement("duedate", DueDate.ToString()),
                    new XElement("isextracredit", IsExtraCredit.ToString()),
                    new XElement("assessmenttypeid", AssessmentTypeID.ToString()),
                    new XElement("data", HttpContext.Current.Server.HtmlEncode(Data)),
                    new XElement("createddate", CreatedDate.ToString()),
                    new XElement("isvisible", IsVisible.ToString()),
                    new XElement("isopen", IsOpen.ToString()),
                    new XElement("isgradable", IsGradable.ToString()),
                    new XElement("allowmultiplesubmissions", AllowMultipleSubmissions.ToString()),
                    new XElement("coursetermid", CourseTermID.ToString()));
            return assessment;
        }

        public void Deserialize(XElement source)
        {
            try
            {
                AssessmentID = new Guid(source.Element("assessmentid").Value);
                Name = source.Element("name").Value;
                DueDate = DateTime.Parse(source.Element("duedate").Value);
                IsExtraCredit = bool.Parse(source.Element("isextracredit").Value);
                AssessmentTypeID = new Guid(source.Element("assessmenttypeid").Value);
                Data = HttpContext.Current.Server.HtmlDecode(source.Element("data").Value);
                CreatedDate = DateTime.Parse(source.Element("createddate").Value);
                IsVisible = bool.Parse(source.Element("isvisible").Value);
                IsOpen = bool.Parse(source.Element("isopen").Value);
                IsGradable = bool.Parse(source.Element("isgradable").Value);
                AllowMultipleSubmissions = bool.Parse(source.Element("allowmultiplesubmissions").Value);
                CourseTermID = new Guid(source.Element("coursetermid").Value);
            }
            catch
            {
                throw new Exception("Failed to deserialize Assessment entity.");
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
            dc.Assessments.InsertOnSubmit(this);
        }

        #endregion

        

        #region ITaggable Members

        double ITaggable.Weight
        {
            get { return this.Weight; }
        }

        string ITaggable.Name
        {
            get { return this.Name; }
        }

        double ITaggable.Score(Profile profile)
        {
            Grade grade = new Grade(this, profile);
            return grade.Points;
        }

        double ITaggable.Score()
        {
            AssessTrackDataRepository repo = new AssessTrackDataRepository();
            return (this as ITaggable).Score(repo.GetLoggedInProfile());
        }

        #endregion
    }
}
