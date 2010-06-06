using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AssessTrack.Models;
using System.Data.Linq;
using System.Xml.Linq;

namespace AssessTrack.Backup
{
    public class SiteBackup
    {
        private List<IBackupItem> items = new List<IBackupItem>();

        public void AddItem(IBackupItem item)
        {
            //TODO - throw exception if duplicate objectID detected
            items.Add(item);
        }

        public IBackupItem GetItem(Guid objectID)
        {
            var item = from i in items
                       where i.objectID == objectID
                       select i;
            return item.SingleOrDefault();

        }

        public void SaveBackup(string filename)
        {
            XElement root = new XElement("sitebackup");
            foreach (IBackupItem item in items)
            {
                root.Add(item.Serialize());
            }
            root.Save(filename);
        }

        public void LoadBackup(AssessTrackModelClassesDataContext dataContext, string filename)
        {
            XElement root = XElement.Load(filename);
            //Deserialize and insert the backup items
            foreach (XElement item in root.Elements())
            {
                IBackupItem importedItem = BackupItemFactory.CreateBackupItem(item.Name.ToString());
                importedItem.Deserialize(item);
                importedItem.Insert(dataContext);
                items.Add(importedItem);
            }

            dataContext.SubmitChanges();
        }
    }
}
