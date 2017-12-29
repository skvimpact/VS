using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

using Reconciliation.DAL;
using Logger;

namespace Reconciliation.Controller
{
    public class ReconciliationController : IReconciliationController
    {
        public string PrepareData(int year, int month)
        {
            //Log4Net.Log.Info("Вызов подготовки");
            return NAVOFF.PrepareData(year, month);
        }

        public ReconciliationItem[] GetData(int year, int month)
        {
            return (ReconciliationItem[]) NAVOFF.GetData(year, month).ToArray();
        }

        public ReconciliationItemCaption[] GetDataCaption()
        {
            ReconciliationItemCaption[] cap = { new ReconciliationItemCaption() };
            return (ReconciliationItemCaption[])cap;
        }

        public CashedDataInfo[] GetCashedDataInfo()
        {
            return (CashedDataInfo[])NAVOFF.GetCashedDataInfo().ToArray();
        }
    }
}
