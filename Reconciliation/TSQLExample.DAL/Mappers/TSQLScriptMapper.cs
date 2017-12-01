using System;
using System.Data.SqlClient;
using System.Data.Common;
namespace TSQLExample.DAL
{
    public class TSQLScriptMapper
    {
        static public Company getCompanyByDbDataReader(DbDataReader dr)
        {
            return (new Company(name: (string)dr[0]));
        }

        static public TD_CRM_Accrual_Storno_LED getLEDByDbDataReader(DbDataReader dr)
        {
            return (new TD_CRM_Accrual_Storno_LED
                        {
                            IdLegalEntity       = (string)dr[0],
                            TableID             = (int)dr[1],
                            EntryNo             = (int)dr[2],
                            DimensionCode       = (string)dr[3],
                            DimensionValueCode  = (string)dr[4]
                        }
                );


        }

    }
}
