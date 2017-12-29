using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;

using Script_Executor;
using Bulk_Inserter;
using System.Reflection;
using System.Text;
using System.Data;

namespace NAVOFFDWH_DAL
{
    public interface IDimensionOLTP<out T>
    {
        string BKey { get; set; }
    }

    public class Dwh
    {
        //private static ScriptExecutor       _oltpExecutor;
        //private static ScriptExecutor       _dwhExecutor;
        //private static BulkInserter         _bulkInserter;

        public Dim[] dimensions;
        public Dim Book;
        public Dim CostCenter;
        public Dim Counterparty;
        public Dim Currency;
        public Dim Company;
        public Dwh()
        {
            //SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
            //SqlConnectionStringBuilder dbuilder = new SqlConnectionStringBuilder();

            //builder.DataSource = "nav-sql-bck";
            //builder.InitialCatalog = "NAV-Offshores-UAT";
            //builder.UserID = "ETL";
            //builder.Password = "Pas_Nav0210!";
            //builder.IntegratedSecurity = false;
            
            //dbuilder["Data Source"] = "nav-ssis-dev";
            //dbuilder["integrated Security"] = true;
            //dbuilder["Initial Catalog"] = "Monitor";
            

            //_oltpExecutor = new ScriptExecutor(builder.ConnectionString, "System.Data.SqlClient");
            //_dwhExecutor = new ScriptExecutor(dbuilder.ConnectionString, "System.Data.SqlClient");
            //_bulkInserter = new BulkInserter(dbuilder.ConnectionString);

            Book = new TypicalDim("Book", "BOOK");
            CostCenter = new TypicalDim("Cost Center", "COSTCENTER");
            Counterparty = new TypicalDim("Counterparty", "COUNTERPARTY");
            Currency = new TypicalDim("Currency", "CURRENCY");
                //new TypicalDimension(/*_oltpExecutor,
                                     //_dwhExecutor,
                                      //_bulkInserter,*/ "Deal", "DEAL"),
            Company = new CompanyDim("Company");
            dimensions = new Dim[] {Book, CostCenter, Counterparty, Currency, Company};
            foreach (var item in dimensions)
            {
                item.Init();
                item.FullUpdate();
            }



        }

        public void Test()
        {
            Console.WriteLine("Извлечение {0} с результатом {1}", "Sberbank Switzerland AG", Company["Sberbank Switzerland AG"]);
            Console.WriteLine("Извлечение {0} с результатом {1}", "Polenica", Company["Polenica"]);
            Console.WriteLine("Извлечение {0} с результатом {1}", "Sberbank Finance Limited", Company["Sberbank Finance Limited"]);
            Console.WriteLine("Извлечение {0} с результатом {1}", "TD Investments", Company["TD Investments"]);
            Console.WriteLine("Извлечение {0} с результатом {1}", "Sberbank Switzerland AG", Company["Sberbank Switzerland AG"]);
            Console.WriteLine("Извлечение {0} с результатом {1}", "# B1_BRIC", Book["#B1_ARB"]);
            Console.WriteLine("Извлечение {0} с результатом {1}", "# B1_BRIC", Book["#B1_BBB"]);
            Console.WriteLine("Извлечение {0} с результатом {1}", "# B1_BRIC", Book["#B1_BRIC"]);



            //GLEntryFact[] gles = new GLEntryFact[] { new GLEntryFact("Sberbank Switzerland AG"), new GLEntryFact("TD Investments") };
            //GLEntryBundle d = new GLEntryBundle(gles);
            
            //foreach(var item in d.companies)
            //{
            //    string g = item.OltpSelector(null);
            //    g += "";


            //    //Console.WriteLine(item.OltpSelector(null));
            //    //Console.WriteLine(item.TableCreator());

            //}

            //cs.GetPieceOfWork(gles[0]);
            //Console.WriteLine("{0}_{1}", cs.GetPieceOfWork(gles[0]).StartOltpEntryNo, cs.GetPieceOfWork(gles[0]).EndOltpEntryNo);
        }


            //e.FillGLEntryOltpBuffer();
            //e.FillGLEntryOltpBuffer();



//            Company c = new Company();
//            Book b = new Book();
//            Counterparty co = new Counterparty();
//            Currency cu = new Currency();
            ///_dwhExecutor.Run(cu.DhwInserter);
            // Console.WriteLine("Извлечение {0} с результатом {1}", "Intragroup", _companies["Intragroup"]);
            //Console.WriteLine("Извлечение {0} с результатом {1}", "Sberbank Switzerland AG", _companies["Sberbank Switzerland AG"]);
            //Console.WriteLine("Извлечение {0} с результатом {1}", "Polenica", _companies["Polenica"]);
            //Console.WriteLine("Извлечение {0} с результатом {1}", "Sberbank Finance Limited", _companies["Sberbank Finance Limited"]);
            //Console.WriteLine("Извлечение {0} с результатом {1}", "TD Investments", _companies["TD Investments"]);

            //Console.WriteLine("Извлечение {0} с результатом {1}", "# B1_BRIC", _books["#B1_ARB"]);
            //Console.WriteLine("Извлечение {0} с результатом {1}", "# B1_BRIC", _books["#B1_BBB"]);
            //Console.WriteLine("Извлечение {0} с результатом {1}", "# B1_BRIC", _books["#B1_BRIC"]);


            //Console.WriteLine("Старт полного кэширования...");
            //_companies.FullUpdate();
            //_books.FullUpdate();
            //_counterparties.FullUpdate();
            //_currencies.FullUpdate();               
    }   
}
