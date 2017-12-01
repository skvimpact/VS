using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TSQLExample.DAL
{
    public class TD_CRM_Accrual_Storno_LED
    {
        public string IdLegalEntity { get; set; }
        public int TableID { get; set; }
        public int EntryNo { get; set; }
        public string DimensionCode { get; set; }
        public string DimensionValueCode { get; set; }

        public override string ToString()
        {
            return String.Format("{0}\t{1}\t{2}\t{3}\t\t{4}", IdLegalEntity, TableID, EntryNo, DimensionCode, DimensionValueCode);
        }
    }
}
