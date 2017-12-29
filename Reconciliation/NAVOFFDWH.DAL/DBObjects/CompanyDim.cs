using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
namespace NAVOFFDWH_DAL
{
    public class CompanyDim : Dim
    {
        public CompanyDim(string name) : base(name)
        {
            _name = name;
            DBObjectItemType = typeof(CompanyDimItem).FullName;
            OltpSelector = (args) =>
            {
                StringBuilder s = new StringBuilder();
                s.Append("select");
                s.Append("  [Name],");
                s.Append("	[Legal Entity Id],");
                s.Append("	[Legal Entity Type]");
                s.Append("from");
                s.Append("	[dbo].[Company]");
                s.Append((args?.Length ?? 0) > 0 ? String.Format("  where Name = '{0}'", args[0]) : "");
                return (s.ToString());
            };
            OltpObjectCreator = (dr) => new CompanyDimItem
            {
                BKey = (string)dr[0],
                LegalEntityId = (string)dr[1],
                LegalEntityType = (string)dr[2]
            };
            DwhObjectCreator = (dr) => new CompanyDimItem
            {
                BKey = (string)dr[0]
            };
        }
        public override void BulkInsert(IEnumerable<DimItem> newItems)
        {
            if (newItems.Count() != 0)
            {
                List<CompanyDimItem> newCompanies = newItems.Select(x => (CompanyDimItem)x).ToList<CompanyDimItem>();
                _bulkInserter.Run<CompanyDimItem>(newCompanies, BulkMapper, DwhTableName);
                Console.WriteLine("{0}. В базу вставили {1} записей", _name, newCompanies.Count());
            }
        }
    }

}
