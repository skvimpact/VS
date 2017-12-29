using System;
using System.Collections.Generic;
using System.Linq;

using Script_Executor;
using Logger;

using Reconciliation.DAL.RF_Resources;

namespace Reconciliation.DAL
{
    static public class NAVOFF
    {
        static private IDictionary<ParameterSet, ICollection<ReconciliationItem>> _data;
        static private ICollection<CashedDataInfo> _cashedDataInfo;


        static private string PrepareData(ParameterSet key)
        {
            string result;
            if (_data == null)
            {
                _data = new Dictionary<ParameterSet, ICollection<ReconciliationItem>>();
                _cashedDataInfo = new SortedSet<CashedDataInfo>();
            }

            if (!_data.ContainsKey(key))
            {
                DateTime start; DateTime end;
                start = DateTime.Now;
                _data.Add(key, Calc(key.year, key.month));
                end = DateTime.Now;
                _cashedDataInfo.Add(new CashedDataInfo(key, _data[key].Count, start, end));
                result = key.ToString() + String.Format(" has been added in {0}.", end - start);
            }
            else
                result = key.ToString() + " already exists.";
            Log4Net.Info(result);
            return result;
        }

        static public string PrepareData(int year, int month)
        {
            ParameterSet key = new ParameterSet(year, month);
            
            return PrepareData(key);
        }

        static public ICollection<ReconciliationItem> GetData(int year, int month)
        {
            ParameterSet key = new ParameterSet(year, month);
            PrepareData(new ParameterSet(year, month));
            return _data[key];
        }

        static public ICollection<CashedDataInfo> GetCashedDataInfo()
        {
            ICollection<CashedDataInfo> result = new List<CashedDataInfo>();
            if (_cashedDataInfo != null)
            {
                result = _cashedDataInfo;
            }
            Log4Net.Info("Получение информации о кэше");
            return result;
        }

        static private ICollection<ReconciliationItem> Calc(int year, int month)
        {
            
            //Console.WriteLine("Calc is starting at {0}", DateTime.Now.ToString("HH:mm:ss tt"));
            
            /*
            ScriptExecutor rus;
            ScriptExecutor off;
            ConnectionStringSettings conn;
            using (RFResourceControllerClient client = new RFResourceControllerClient())
            {
                conn = client.GetConnectionStringSettings("OLAP_NAVRUS");
                rus = new ScriptExecutor(conn.ConnectionString, conn.ProviderName);
                conn = client.GetConnectionStringSettings("OLAP_NAVOFFSH");
                off = new ScriptExecutor(conn.ConnectionString, conn.ProviderName);
            }
            */
            ScriptExecutor rus = new ScriptExecutor("Provider=MSOLAP.6;Data Source=nav-sql-bck;Initial Catalog=CONS_RUS_UAT", "System.Data.OleDb");
            ScriptExecutor off = new ScriptExecutor("Provider=MSOLAP.6;Data Source=nav-sql-bck;Initial Catalog=Offshores_UAT", "System.Data.OleDb");

            //            ISet<Account> NavRusAccounts = new HashSet<Account>();
            //            ISet<Account> NavOffshAccounts = new HashSet<Account>();
            ISet<Account> NavRusAccounts = new SortedSet<Account>();
            ISet<Account> NavOffshAccounts = new SortedSet<Account>();

            
            rus.Run<Account>(
                                MDXScriptBuilder.getNavRusAccountList(),
                                NavRusAccounts,
                                null);

            off.Run<Account>(
                                MDXScriptBuilder.getNavOffshOriginalAccountList(),
                                NavOffshAccounts,
                                null);
            

            // Test >>>>>>>>
            
            ISet<Account> TestAccounts = new SortedSet<Account>();
            TestAccounts.Add(new Account("01-0000100"));
           // TestAccounts.Add(new Account("01-0000200"));
           // TestAccounts.Add(new Account("01-0000300"));
           // TestAccounts.Add(new Account("01-0000400"));
            /*
            TestAccounts.Add(new Account("02-0000100"));
            TestAccounts.Add(new Account("02-0000200"));
            TestAccounts.Add(new Account("02-0000300"));
            TestAccounts.Add(new Account("02-0000400"));

            TestAccounts.Add(new Account("04-0000100"));
            TestAccounts.Add(new Account("04-0000200"));
            TestAccounts.Add(new Account("04-0000800"));
            TestAccounts.Add(new Account("04-0000900"));

            TestAccounts.Add(new Account("05-0000000"));
            TestAccounts.Add(new Account("05-0000100"));
            TestAccounts.Add(new Account("05-0000200"));
            TestAccounts.Add(new Account("05-0000800"));
            */
           // TestAccounts.Add(new Account("08-0000000"));
           // TestAccounts.Add(new Account("08-0000100"));
          //  TestAccounts.Add(new Account("08-0000101"));
          //  TestAccounts.Add(new Account("08-0000102"));
          //  TestAccounts.Add(new Account("08-0000200"));
          //  TestAccounts.Add(new Account("08-0000201"));
          //  TestAccounts.Add(new Account("08-0000202"));

            /*
            TestAccounts.Add(new Account("50-0000000"));
            TestAccounts.Add(new Account("50-0100000"));
            TestAccounts.Add(new Account("50-0200000"));

            TestAccounts.Add(new Account("51-0000000"));
            TestAccounts.Add(new Account("51-0100000"));
            TestAccounts.Add(new Account("51-0101000"));
            TestAccounts.Add(new Account("51-0102000"));


            TestAccounts.Add(new Account("57-0101000"));
            TestAccounts.Add(new Account("57-0102000"));
            TestAccounts.Add(new Account("57-0103000"));
            TestAccounts.Add(new Account("57-0104000"));
            TestAccounts.Add(new Account("57-0200000"));
            TestAccounts.Add(new Account("57-0201000"));
            TestAccounts.Add(new Account("57-0202000"));
            TestAccounts.Add(new Account("57-0203000"));
            TestAccounts.Add(new Account("58-0000000"));
            TestAccounts.Add(new Account("58-0100000"));
            TestAccounts.Add(new Account("58-0101000"));
            TestAccounts.Add(new Account("58-0101100"));
            TestAccounts.Add(new Account("58-0101200"));
            TestAccounts.Add(new Account("58-0101300"));
            TestAccounts.Add(new Account("58-0102000"));
            TestAccounts.Add(new Account("58-0102100"));
            TestAccounts.Add(new Account("58-0102200"));
            TestAccounts.Add(new Account("58-0103000"));
            TestAccounts.Add(new Account("58-0105000"));
            TestAccounts.Add(new Account("58-0105100"));
            TestAccounts.Add(new Account("58-0105110"));
            TestAccounts.Add(new Account("58-0105120"));
            TestAccounts.Add(new Account("58-0200000"));
            TestAccounts.Add(new Account("58-0201000"));
            TestAccounts.Add(new Account("58-0201100"));
            TestAccounts.Add(new Account("58-0201200"));
            TestAccounts.Add(new Account("58-0201300"));
            TestAccounts.Add(new Account("58-0202000"));
            TestAccounts.Add(new Account("58-0202100"));
            TestAccounts.Add(new Account("58-0202200"));
            TestAccounts.Add(new Account("58-0203000"));
            TestAccounts.Add(new Account("58-0204000"));
            TestAccounts.Add(new Account("58-0204100"));
            TestAccounts.Add(new Account("58-0204200"));
            TestAccounts.Add(new Account("58-0205000"));
            */
            
            
            NavRusAccounts.IntersectWith(TestAccounts);

            NavOffshAccounts.IntersectWith(TestAccounts);
            
            // Test <<<<<<<<

            NavOffshAccounts.IntersectWith(NavRusAccounts);

            IDictionary<DimensionSet, MeasureSet> NavRusGLInfo = new SortedList<DimensionSet, MeasureSet>();
            foreach (var account in NavRusAccounts)
            {
                String[] consts = { account.name, DateTime.Now.ToString("HH:mm:ss tt") };
                Console.WriteLine(consts[0]);
                rus.Run<DimensionSet, MeasureSet>(
                                                    MDXScriptBuilder.getNavRusGLInfo(year, month, account.name),
                                                    NavRusGLInfo,                                                   
                                                    consts);
            }
            //Console.WriteLine("Rus is ready at {0}. {1} records", DateTime.Now.ToString("HH:mm:ss tt"), NavRusGLInfo.Count);

            IDictionary<DimensionSet, MeasureSet> NavOffshGLInfo = new SortedList<DimensionSet, MeasureSet>();

            foreach (var account in NavOffshAccounts)
            //foreach (var account in NavRusAccounts)
            {
                String[] consts = { account.name, DateTime.Now.ToString("HH:mm:ss tt") };
                Console.WriteLine(consts[0]);
                off.Run<DimensionSet, MeasureSet>(
                                                    MDXScriptBuilder.getNavOffshGLInfo(year, month, account.name),
                                                    NavOffshGLInfo,
                                                    consts);
                // Test !!!
               //                 rus.Run<DimensionSet, MeasureSet>(
               //                                                     MDXScriptBuilder.getNavRusGLInfo(year, month, account.name),
               //                                                     NavOffshGLInfo,
               //                                                     MDXScriptMapper.getDimensionSetByAdomdDataReader, MDXScriptMapper.getMeasureSetByAdomdDataReader,
               //                                                     args);


            }
            //Console.WriteLine("Off is ready at {0}. {1} records", DateTime.Now.ToString("HH:mm:ss tt"), NavOffshGLInfo.Count);

            Console.WriteLine("Merger's been started");

            ICollection<DimensionSet> allKeys = NavRusGLInfo.Keys;
            allKeys.Union(NavOffshGLInfo.Keys);
            ISet<ReconciliationItem> reconciliation = new SortedSet<ReconciliationItem>();
            int i = 0;
            foreach (var key in allKeys)
            {

                MeasureSet leftMS = NavRusGLInfo.ContainsKey(key) ? (NavRusGLInfo[key].IsNull() ? null : NavRusGLInfo[key]) : null;
                MeasureSet rightMS = NavOffshGLInfo.ContainsKey(key) ? (NavOffshGLInfo[key].IsNull() ? null  : NavOffshGLInfo[key]) : null;

                if ((leftMS == null) && (rightMS == null))
                {
                    continue;
                }


                if ((leftMS != null) && (rightMS != null))
                {
                    if (leftMS.Equals(rightMS))
                    {
                        continue;
                    }
                }

                if (i < 1000000)
                    reconciliation.Add(new ReconciliationItem(key, leftMS, rightMS));
                i += 1;
            }
            Console.WriteLine("Merger's finished");

            return reconciliation;
        }

    }
}
