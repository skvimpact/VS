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
        RecItem[] GetData(int year, int month);
    }
}
