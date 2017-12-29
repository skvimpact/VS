using System;
using System.Data.Common;
using Script_Executor;

namespace TSQLExample.DAL
{
    public class TD_CRM_Accrual_Storno_LED : IMappable<TD_CRM_Accrual_Storno_LED>
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

        public TD_CRM_Accrual_Storno_LED GetFromDbDataReader(DbDataReader dr, String[] consts)
        {
            return new TD_CRM_Accrual_Storno_LED
            {
                IdLegalEntity = (string)dr[0],
                TableID = (int)dr[1],
                EntryNo = (int)dr[2],
                DimensionCode = (string)dr[3],
                DimensionValueCode = (string)dr[4]
            };               
        }
    }
}
