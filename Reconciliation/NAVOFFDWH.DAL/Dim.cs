using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Script_Executor;
using Bulk_Inserter;
using System.Data.Common;
//using NAVOFFDWH.DAL;

namespace NAVOFFDWH_DAL
{
    public interface IDim
    {
        int? SKey(string BKey);
        //int? SKeyd[string BKey];
    }

    public interface IDimN
    {
        int? SKey(string BKey);
    }

    public abstract class Dim : DBObject
    {
        private StringIntMap _items;
        public Func<string> DwhSelector;
        public Func<DbDataReader, DimItem> OltpObjectCreator;
        public Func<DbDataReader, DimItem> DwhObjectCreator;

        public string DwhTableName { get { return String.Format("[dbo].[{0}]", _name); } }

        public Dim(string name) : base(name, "")
        {
            DwhSelector = () =>
            {
                StringBuilder s = new StringBuilder();
                s.Append("select");
                s.Append("	[BKey],");
                s.Append("	[SKey]");
                s.Append("from");
                s.AppendFormat("    [dbo].[{0}]", _name);
                return (s.ToString());
            };

        }

        public void Init()
        {
            _items = new StringIntMap(_dwhExecutor.GetDictionary<string, int>(
                                                                                DwhSelector(),
                                                                                dr => { return (string)dr[0]; },
                                                                                dr => { return (int)dr[1]; }
                                                                             ));
            Console.WriteLine("{0}. Init вставил {1} записей в кэш", _name, _items.Count);
        }

        public int? this[string BKey]
        {
            get
            {
                if (BKey == null) return null;
                if (!_items.ContainsKey(BKey))
                {
                    var newItems = _oltpExecutor.GetList<DimItem>(
                                                            OltpSelector(new[] { BKey }),
                                                            OltpObjectCreator);
                    _processNewItem(newItems);
                }
                return _items[BKey];
            }
            set => _items[BKey] = value ?? 0;
        }

        public void FullUpdate()
        {
            var newItems = _oltpExecutor.GetList<DimItem>(
                                                       OltpSelector(null),
                                                       OltpObjectCreator
                                                    ).Except<DimItem>(
                                                                   _dwhExecutor.GetList<DimItem>(
                                                                                              DwhSelector(),
                                                                                              DwhObjectCreator
                                                                                          )
                                                               ).ToList<DimItem>();
            Console.WriteLine("{0}. FullUpdate обнаружил {1} новых записей", _name, newItems.Count);
            _processNewItem(newItems);
        }

        private void _processNewItem(IList<DimItem> newItems)
        {
            int i = _items.MaxInt + 1;
            foreach (var newItem in newItems)
            {
                newItem.SKey = i++;
                _items[newItem.BKey] = newItem.SKey;
                // Console.WriteLine("{0}. В кэш вставлен BKey {1}", _name, newItem.BKey);
            }
            BulkInsert(newItems);
        }
        public virtual void BulkInsert(IEnumerable<DimItem> newItems){}
    }    
}
