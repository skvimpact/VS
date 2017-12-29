using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;
using log4net.Config;
using System.IO;
namespace Logger
{
    public static class Log4Net
    {
        static private ILog log;// = LogManager.GetLogger("LOGGER");


        /*public static ILog Log
        {
            get { return log; }
        }*/

        static public void Info(string message)
        {
            return;
            if (log == null)
            {
               var fileInfo = new FileInfo("c:\\Data\\VS\\Reconciliation\\Reconciliation.Controller.WS\\log4net.config");

                XmlConfigurator.Configure(fileInfo);
                log = LogManager.GetLogger("LOGGER");
                //log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
            }

            log.Info(message);
        }


        //public static void InitLogger()
        // {
        //    XmlConfigurator.Configure();
        //}
    }
}
