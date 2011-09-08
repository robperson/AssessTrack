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
using System.Collections.Generic;
using System.Data.Linq;
using System.Text.RegularExpressions;
using System.Web.Mvc;
using AssessTrack.Helpers;
using AssessTrack.Backup;

namespace AssessTrack.Models
{
    [Bind(Include="Title,Description,ShortName,Password")]
    public partial class Site : IBackupItem
    {
        public bool IsValid
        {
            get { return (GetRuleViolations().Count() == 0); }
        }

        public IEnumerable<RuleViolation> GetRuleViolations()
        {
            AssessTrackDataRepository dataRepository = new AssessTrackDataRepository();

            if (String.IsNullOrEmpty(Title))
                yield return new RuleViolation("Title is required", "Title");
            if (String.IsNullOrEmpty(ShortName))
                yield return new RuleViolation("Short Name is required", "ShortName");

            if (!Regex.IsMatch(ShortName, @"\A[a-zA-Z0-9_-]+\Z"))
                yield return new RuleViolation("Short Name can only contain letters, numbers, underscores (_) and dashes (-)", "ShortName");
            
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

        #region IBackupItem Members

        public Guid objectID
        {
            get;
            set;
        }

        public XElement Serialize()
        {
            XElement site =
                new XElement("site",
                    new XElement("siteid", SiteID.ToString()),
                    new XElement("title", Title),
                    new XElement("shortname", ShortName),
                    new XElement("description", Description));
            return site;
        }

        public void Deserialize(XElement source)
        {
            try
            {
                SiteID = new Guid(source.Element("siteid").Value);
                Title = source.Element("title").Value;
                ShortName = source.Element("shortname").Value;
                Description = source.Element("description").Value;
            }
            catch (Exception)
            {
                throw new Exception("Failed to deserialize Site entity.");
            }
        }

        public void Insert(AssessTrackModelClassesDataContext dc)
        {
            dc.Sites.InsertOnSubmit(this);
        }

        public List<SiteMember> GetMembers(int minLevel, int maxLevel)
        {
            return (from ctm in SiteMembers
                    where ctm.AccessLevel >= minLevel
                        && ctm.AccessLevel <= maxLevel
                    orderby ctm.Profile.LastName ascending
                    select ctm).ToList();
        }

        public List<Profile> GetMemberProfiles(int minLevel, int maxLevel)
        {
            return (from ctm in SiteMembers
                    where ctm.AccessLevel >= minLevel
                        && ctm.AccessLevel <= maxLevel
                    orderby ctm.Profile.LastName ascending
                    select ctm.Profile).ToList();
        }

        #endregion
    }
}
