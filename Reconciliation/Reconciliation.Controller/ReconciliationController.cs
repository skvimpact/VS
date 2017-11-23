using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

using Reconciliation.DAL;

namespace Reconciliation.Controller
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.
    public class ReconciliationController : IReconciliationController
    {
        public ICollection<RecItem> records = null;
/*        public Account[] GetData()
        {
            ICollection<Account> records = new List<Account>();
            records.Add(new Account("Zoya"));
            records.Add(new Account("Marat"));

            return (Account[])records.ToArray();
            //return 96;
        }*/

        public RecItem[] GetData(int year, int month)
        {
            records = NAVOFF.Calc(year, month);

            //ICollection<RecItem> records = new List<RecItem>();
            //records.Add(new RecItem(new DimensionSet("A", "B", "C"), new MeasureSet(100.0), new MeasureSet(200.0)));

            return (RecItem[])records.ToArray();
        }


    }
}
