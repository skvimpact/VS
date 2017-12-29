using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Data.Common;
using System.Data.SqlClient;
using System.Data;
using Script_Executor;
using Bulk_Inserter;
namespace NAVOFFDWH_DAL
{
    public class Fact<T>
    {
        //int 
        public Fact()
        {

        }
    }


    public class GLEntry
    {
        private static ScriptExecutor _oltpExecutor;
        private static ScriptExecutor _dwhExecutor;
        private static BulkInserter _bulkInserter;
        private string _le;
        private IDim[] _dimensions;
        public int StartOlapEntryNo { get; set; }
        public int EndOlapEntryNo { get; set; }
        public int BufferSize { get; set; }       = 1000;
        public int LastOlapEntryNo { get; set; }  = 1000;
        public GLEntry(ScriptExecutor oltpExecutor,
                       ScriptExecutor dwhExecutor,
                       BulkInserter bulkInserter,
                       string le,
                       IDim[] dimensions)
        {
            _oltpExecutor = oltpExecutor;
            _dwhExecutor = dwhExecutor;
            _bulkInserter = bulkInserter;
            _le = le;
            _dimensions = dimensions;
            _glEntryDwhBuffer = new List<GLEntryDwh>();
        }        

        public IList<GLEntryOltp> _glEntryOltpBuffer;
        public IList<GLEntryDwh> _glEntryDwhBuffer;

        public void FullUpdate()
        {
            StartOlapEntryNo = 1;
            EndOlapEntryNo = 0;
            while (EndOlapEntryNo < LastOlapEntryNo)
            {
                EndOlapEntryNo = StartOlapEntryNo + BufferSize - 1;


                // Do
                DateTime start; DateTime end;
                start = DateTime.Now;
                Console.WriteLine("Старт чтения {0}", DateTime.Now);
                FillGLEntryOltpBuffer();
                Console.WriteLine("Старт записи {0}", DateTime.Now);
                SaveGLEntryDwhBuffer();
                end = DateTime.Now;
                Console.WriteLine("Записали {0} записей за {1}. Дошли до {2}", _glEntryDwhBuffer.Count, end - start, EndOlapEntryNo);
                
                
                // Do

                StartOlapEntryNo += BufferSize;
            }
        }

        public void FillGLEntryOltpBuffer()
        {
            _glEntryOltpBuffer?.Clear();
            _glEntryDwhBuffer.Clear();
            List<DbParameter> parameters = new List<DbParameter>
            {
                new SqlParameter
                {
                    ParameterName = "LE",
                    SqlDbType = SqlDbType.VarChar,
                    Size = 30,
                    Value = _le,
                    Direction = ParameterDirection.Input
                },
                new SqlParameter
                {
                    ParameterName = "EntryS",
                    SqlDbType = SqlDbType.Int,
                    Value = StartOlapEntryNo,
                    Direction = ParameterDirection.Input
                },
                new SqlParameter
                {
                    ParameterName = "EntryE",
                    SqlDbType = SqlDbType.Int,
                    Value = EndOlapEntryNo,
                    Direction = ParameterDirection.Input
                }
            };
            _glEntryOltpBuffer = _oltpExecutor.GetList<GLEntryOltp>("[dbo].[TD_DWH]",
                                                                    parameters,
                                                                    (dr) => new GLEntryOltp
                                                                    {
                                                                        EntryNo = (int)dr[0],
                                                                        LocalAccount = dr.IsDBNull(1) ? null : (string)dr[1],                                                                        
                                                                        Account = dr.IsDBNull(2) ? null : (string)dr[2],
                                                                        Amount = dr.GetDecimal(3),
                                                                        D1 = dr.IsDBNull(4) ? null : (string)dr[4],
                                                                        D2 = dr.IsDBNull(5) ? null : (string)dr[5],
                                                                        D3 = dr.IsDBNull(6) ? null : (string)dr[6],
                                                                        D4 = dr.IsDBNull(7) ? null : (string)dr[7]
                                                                    });
            foreach(var item in _glEntryOltpBuffer)
            {
                _glEntryDwhBuffer.Add(new GLEntryDwh {
                    LocalAccount = null,
                    Account = null,
                    Amount = item.Amount,
                    D1 = _dimensions[0].SKey(item.D1),
                    D2 = _dimensions[1].SKey(item.D2),
                    D3 = _dimensions[2].SKey(item.D3)
                });
                ;
            }
        }

        public void SaveGLEntryDwhBuffer()
        {

            _bulkInserter.Run<GLEntryDwh>(_glEntryDwhBuffer,
                                          new Dictionary<string, string>
                                          {
                                              { "LocalAccount", "Local Account"},
                                              { "Account",      "Account"},
                                              { "Amount",       "Amount"},
                                              { "D1",           "D1"},
                                              { "D2",           "D2"},
                                              { "D3",           "D3"},
                                              { "D4",           "D4"},
                                              { "D5",           "D5"}
                                          },
                                          "GLEntry");                
            foreach (var item in _glEntryDwhBuffer)
            {
               // Console.WriteLine(item.ToString());
            }
        }
    }


    public class GLEntryOltp
    {
        public int EntryNo { get; set; }
        public string LocalAccount { get; set; }
        public string Account { get; set; }
        public decimal Amount { get; set; }
        public string D1 { get; set; }
        public string D2 { get; set; }
        public string D3 { get; set; }
        public string D4 { get; set; }
        public string D5 { get; set; }

        public override string ToString() => String.Format("{0}\t{1}\t{2}\t{3}\t{4}\t{5}\t{6}\t{7}", EntryNo, LocalAccount, Account, Amount, D1, D2, D3, D4, D5);
    }

    public class GLEntryDwh
    {
        public int EntryNo { get; set; }
        public int? LocalAccount { get; set; }
        public int? Account { get; set; }
        public decimal Amount { get; set; }
        public int? D1 { get; set; }
        public int? D2 { get; set; }
        public int? D3 { get; set; }
        public int? D4 { get; set; }
        public int? D5 { get; set; }

        public override string ToString() => String.Format("{0}\t{1}\t{2}\t{3}\t{4}\t{5}\t{6}\t{7}", EntryNo, LocalAccount, Account, Amount, D1, D2, D3, D4, D5);
    }



    public class IntIntMap : IEnumerable
    {
        private IDictionary<int, int> _dictionary;

        public IntIntMap(IDictionary<int, int> dictionary)
        {
            _dictionary = dictionary;
        }

        public bool ContainsKey(int entryNo) => _dictionary.ContainsKey(entryNo);

        public int MaxInt => _dictionary.Count == 0 ? 0 : _dictionary.Values.Max();

        public int this[int entryNo]
        {
            get => _dictionary[entryNo];
            set => _dictionary[entryNo] = value;
        }

        public void ClearMap() => _dictionary.Clear();

        public int Count => _dictionary.Count;

        IEnumerator IEnumerable.GetEnumerator() => _dictionary.GetEnumerator();
    }

}
