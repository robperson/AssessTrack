using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Linq;
using System.Diagnostics;
using AssessTrack.Helpers;

namespace AssessTrack.Models
{
    public partial class AssessTrackDataRepository
    {
        public void AddItem<T>(T item) where T : class
        {
            dc.GetTable<T>().InsertOnSubmit(item);
        }

        public T Single<T>(Func<T,bool> exp) where T : class
        {
            return dc.GetTable<T>().SingleOrDefault(exp);
        }

        public void Remove<T>(T entity) where T : class
        {
            dc.GetTable<T>().DeleteOnSubmit(entity);
        }

        public ChangeSet GetChangeSet()
        {
            return dc.GetChangeSet();
        }

        public void EnableDebugLogging()
        {
            dc.Log = new DebugTextWriter();
        }
    }
}
