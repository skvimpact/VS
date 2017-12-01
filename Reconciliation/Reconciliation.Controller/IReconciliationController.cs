using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

using Reconciliation.DAL;

namespace Reconciliation.Controller
{

    [ServiceContract]
    public interface IReconciliationController
    {
        [OperationContract]
        [WebGet]
        string PrepareData(int year, int month);

        [OperationContract]
        [WebGet]
        ReconciliationItem[] GetData(int year, int month);
        
        [OperationContract]
        [WebGet]
        ReconciliationItemCaption[] GetDataCaption();

        [OperationContract]
        [WebGet]
        CashedDataInfo[] GetCashedDataInfo();
    }
}
