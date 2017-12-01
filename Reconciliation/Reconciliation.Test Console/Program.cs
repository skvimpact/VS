using System;
using System.Collections.Generic;
using System.Text;

using Reconciliation.DAL;
using TSQLExample.DAL;

namespace Reconciliation.Test_Console
{
    class Program
    {
        static void Main(string[] args)
        {

//            Console.WriteLine(MDXScriptBuilder.getNavRusAccountList());
            
            //ICollection<ReconciliationItem> records = NAVOFF.Calc(2016, 6);
            //ICollection<ReconciliationItem> records2 = NAVOFF.Calc(2017, 6);
            //Console.WriteLine("Создано {0} записей", records.Count);
            
            

            
            ICollection<ParameterSet> parameters = new List<ParameterSet>();

            parameters.Add(new ParameterSet(2017, 6));
            //parameters.Add(new ParameterSet(2016, 6));
            //parameters.Add(new ParameterSet(2016, 6));

            
            DateTime start;  DateTime end;
            foreach (var item in parameters)
            {
                start = DateTime.Now;
                ICollection<ReconciliationItem> records = NAVOFF.GetData(item.year, item.month);
                end = DateTime.Now;
                Console.WriteLine("Получено {0} записей за {1}", records.Count, end - start);
            }
            
            ICollection<Company> records2 = OFF.getCompanies();
            foreach (var item in records2)
            {
                Console.WriteLine(item.name);
            }
             
            Console.WriteLine("------------------------------------------------");
            ICollection<TD_CRM_Accrual_Storno_LED> documents = OFF.getDocuments();
            foreach (var item in documents)
            {
                Console.WriteLine(item.ToString());
            }
            
            Console.WriteLine(Environment.NewLine + "Press any key to continue.");
            Console.ReadKey();
        }


        static void sss()
        {
            /*parameters.Add(new ParameterSet(2015, 3));
            parameters.Add(new ParameterSet(2015, 6));
            parameters.Add(new ParameterSet(2015, 9));
            parameters.Add(new ParameterSet(2015, 12));

            parameters.Add(new ParameterSet(2016, 3));
            parameters.Add(new ParameterSet(2016, 6));
            parameters.Add(new ParameterSet(2016, 9));
            parameters.Add(new ParameterSet(2016, 12));

            parameters.Add(new ParameterSet(2017, 3));
            parameters.Add(new ParameterSet(2017, 6));
            parameters.Add(new ParameterSet(2017, 9));*/

        }
    }
}
