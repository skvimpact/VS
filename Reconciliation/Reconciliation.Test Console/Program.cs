using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Reconciliation.DAL;

namespace Reconciliation.Test_Console
{
    class Program
    {
        static void Main(string[] args)
        {
//            Console.WriteLine(MDXScriptBuilder.getNavRusAccountList());
            ICollection<RecItem> records = NAVOFF.Calc(2017, 6);
            ICollection<RecItem> records2 = NAVOFF.Calc(2017, 6);
            Console.WriteLine("Создано {0} записей", records.Count);
            Console.WriteLine(Environment.NewLine + "Press any key to continue.");
            Console.ReadKey();
        }
    }
}
