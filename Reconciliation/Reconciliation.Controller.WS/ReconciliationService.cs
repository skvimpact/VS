using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;

namespace Reconciliation.Controller.WS
{
    public partial class ReconciliationService : ServiceBase
    {
        private ServiceHost myHost;

        public ReconciliationService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            if (myHost != null)
            {
                myHost.Close();
                myHost = null;
            }
            myHost = new ServiceHost(typeof(ReconciliationController));
            myHost.Open();
        }

        protected override void OnStop()
        {
            if (myHost != null)
                myHost.Close();
        }
    }
}
