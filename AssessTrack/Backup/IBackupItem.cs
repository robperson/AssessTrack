using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using AssessTrack.Models;

namespace AssessTrack.Backup
{
    interface IBackupItem
    {
        public XElement Serialize();
        public void Deserialize(XElement source);
        public void LinkRelationships(SiteBackup sitebackup);

        //The items ID as found in the backup file.
        public Guid objectID;
        public void Insert(AssessTrackModelClassesDataContext dc);
        public void OnPostInsert();
    }
}
