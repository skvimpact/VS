using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NAVOFFDWH_DAL
{
    public class TypicalDim : Dim
    {
        public TypicalDim(string name, string oltpDimensionCode) : base(name)
        {
            _name = name;
            DBObjectItemType = typeof(TypicalDimItem).FullName;
            OltpSelector = (args) =>
            {
                StringBuilder s = new StringBuilder();
                s.Append("select");
                s.Append("  [Code],");
                s.Append("  [Name]");
                s.Append("from");
                s.Append("  dbo.[Dimension Value]");
                s.Append("where");
                s.AppendFormat("  [Dimension Code] = '{0}'", oltpDimensionCode);
                s.Append((args?.Length ?? 0) > 0 ? String.Format("  and Code = '{0}'", args[0]) : "");
                return (s.ToString());
            };
            OltpObjectCreator = (dr) => new TypicalDimItem
            {
                BKey = (string)dr[0],
                Name = (string)dr[1]
            };
            DwhObjectCreator = (dr) => new TypicalDimItem
            {
                BKey = (string)dr[0]
            };
            //BulkMapper = new Dictionary<string, string>
            //{
            //    { "SKey",           "SKey"},
            //    { "BKey",           "BKey"},
            //    { "Name",           "Name"}
            //};
        }

        public override void BulkInsert(IEnumerable<DimItem> newItems)
        {
            if (newItems.Count() != 0)
            {
                List<TypicalDimItem> newTypicalDims = newItems.Select(x => (TypicalDimItem)x).ToList<TypicalDimItem>();
                _bulkInserter.Run<TypicalDimItem>(newTypicalDims, BulkMapper, DwhTableName);
                Console.WriteLine("{0}. В базу вставили {1} записей", _name, newTypicalDims.Count());
            }
        }
    }
}
