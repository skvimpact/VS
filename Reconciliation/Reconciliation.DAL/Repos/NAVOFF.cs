using System;
using System.Collections.Generic;
using System.Linq;

using MDX_Script_Executor;

namespace Reconciliation.DAL
{
    public class NAVOFF
    {
        static public ICollection<RecItem> buffer;
        static public ICollection<RecItem> Calc(int year, int month)
        {
            if (buffer == null)
            {
                buffer = CalcInner(year, month);
            }
            return buffer;
        }
        static public ICollection<RecItem> CalcInner(int year, int month)
        {
            
            //Console.WriteLine("Calc is starting at {0}", DateTime.Now.ToString("HH:mm:ss tt"));
            MDXScriptExecutor rus = new MDXScriptExecutor("Data Source=nav-sql-bck;Initial Catalog=CONS_RUS_UAT");
            MDXScriptExecutor off = new MDXScriptExecutor("Data Source=nav-sql-bck;Initial Catalog=Offshores_UAT");

            //            ISet<Account> NavRusAccounts = new HashSet<Account>();
            //            ISet<Account> NavOffshAccounts = new HashSet<Account>();
            ISet<Account> NavRusAccounts = new SortedSet<Account>();
            ISet<Account> NavOffshAccounts = new SortedSet<Account>();


            rus.Run<Account>(
                                MDXScriptBuilder.getNavRusAccountList(),
                                NavRusAccounts,
                                MDXScriptMapper.getAccountByAdomdDataReader);
            off.Run<Account>(
                                MDXScriptBuilder.getNavOffshOriginalAccountList(),
                                NavOffshAccounts,
                                MDXScriptMapper.getAccountByAdomdDataReader);

            // Test >>>>>>>>
            
            ISet<Account> TestAccounts = new SortedSet<Account>();
            TestAccounts.Add(new Account("01-0000100"));
            TestAccounts.Add(new Account("01-0000200"));
            TestAccounts.Add(new Account("01-0000300"));
            TestAccounts.Add(new Account("01-0000400"));

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

            TestAccounts.Add(new Account("08-0000000"));
            TestAccounts.Add(new Account("08-0000100"));
            TestAccounts.Add(new Account("08-0000101"));
            TestAccounts.Add(new Account("08-0000102"));
            TestAccounts.Add(new Account("08-0000200"));
            TestAccounts.Add(new Account("08-0000201"));
            TestAccounts.Add(new Account("08-0000202"));


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



            NavRusAccounts.IntersectWith(TestAccounts);
            
            // Test <<<<<<<<

            NavOffshAccounts.IntersectWith(NavRusAccounts);

            IDictionary<DimensionSet, MeasureSet> NavRusGLInfo = new SortedList<DimensionSet, MeasureSet>();
            foreach (var account in NavRusAccounts)
            {
                String[] args = { account.name, DateTime.Now.ToString("HH:mm:ss tt") };
                Console.WriteLine(args[0]);
                rus.Run<DimensionSet, MeasureSet>(
                                                    MDXScriptBuilder.getNavRusGLInfo(year, month, account.name),
                                                    NavRusGLInfo,
                                                    MDXScriptMapper.getDimensionSetByAdomdDataReader, MDXScriptMapper.getMeasureSetByAdomdDataReader,
                                                    args);
            }
            //Console.WriteLine("Rus is ready at {0}. {1} records", DateTime.Now.ToString("HH:mm:ss tt"), NavRusGLInfo.Count);

            IDictionary<DimensionSet, MeasureSet> NavOffshGLInfo = new SortedList<DimensionSet, MeasureSet>();
            foreach (var account in NavOffshAccounts)
            {
                String[] args = { account.name, DateTime.Now.ToString("HH:mm:ss tt") };
                off.Run<DimensionSet, MeasureSet>(
                                                    MDXScriptBuilder.getNavOffshGLInfo(year, month, account.name),
                                                    NavOffshGLInfo,
                                                    MDXScriptMapper.getDimensionSetByAdomdDataReader, MDXScriptMapper.getMeasureSetByAdomdDataReader,
                                                    args);
                // Test !!!
                //                rus.Run<DimensionSet, MeasureSet>(
                //                                                    MDXScriptBuilder.getNavRusGLInfo(year, month, account.name),
                //                                                    NavOffshGLInfo,
                //                                                    MDXScriptMapper.getDimensionSetByAdomdDataReader, MDXScriptMapper.getMeasureSetByAdomdDataReader,
                //                                                    args);


            }
            //Console.WriteLine("Off is ready at {0}. {1} records", DateTime.Now.ToString("HH:mm:ss tt"), NavOffshGLInfo.Count);

            ICollection<DimensionSet> allKeys = NavRusGLInfo.Keys;
            allKeys.Union(NavOffshGLInfo.Keys);
            ICollection<RecItem> reconciliation = new List<RecItem>();
            int i = 0;
            foreach (var key in allKeys)
            {
                
                MeasureSet leftMS = NavRusGLInfo.ContainsKey(key) ? NavRusGLInfo[key] : null;
                MeasureSet rightMS = NavOffshGLInfo.ContainsKey(key) ? NavOffshGLInfo[key] : null;
                if ((leftMS != null) && (rightMS != null))
                {
                    if (leftMS.Equals(rightMS))
                    {
                        continue;
                    }
                }
                if (i < 1000000)
                    reconciliation.Add(new RecItem(key, leftMS, rightMS));
                i += 1;
            }


            for (int j = 0; j < -1000; j++)
                reconciliation.Add(
                    new RecItem(new DimensionSet(DateTime.Now.ToString("HH:mm:ss tt"), "B", "C", "D", "E", "F", "G", "B", "C", "D", "E", "F", "G", "B", "C", "D", "E", "F", "G", "B", "C", "D", "E", "F", "G"),
                    new MeasureSet(100.0, 100.0, 100.0, 100.0),
                    new MeasureSet(200.0, 200.0, 200.0, 200.0)));            



            //Console.WriteLine("{0} {1} {2} {3}", NavRusGLInfo.Keys.Count, NavOffshGLInfo.Keys.Count, allKeys.Count, reconciliation.Count);          
            return reconciliation;
        }


        static public ICollection<RecItem> CalcTest(int year, int month)
        {
            ICollection<RecItem> reconciliation = new List<RecItem>();
            for( int j =0; j < 10; j ++)
                reconciliation.Add(
                    new RecItem(new DimensionSet(DateTime.Now.ToString("HH:mm:ss tt"), "B", "C", "D", "E", "F", "G", "B", "C", "D", "E", "F", "G", "B", "C", "D", "E", "F", "G", "B", "C", "D", "E", "F", "G"),
                    new MeasureSet(100.0, 100.0, 100.0, 100.0),
                    new MeasureSet(200.0, 200.0, 200.0, 200.0)));            

            return reconciliation;
        }
    }
}
