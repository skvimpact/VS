using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using System.Threading.Tasks;
//using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

using Script_Executor;

namespace TSQLExample.DAL
{
    public class OFF
    {
        static private ScriptExecutor _off;
        static private ScriptExecutor off()
        {
            if (_off == null)
            {
                SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();

                builder.DataSource = "nav-sql-bck";
                builder.InitialCatalog = "NAV-Offshores-UAT";
                builder.UserID = "ETL";
                builder.Password = "Pas_Nav0210!";
                builder.IntegratedSecurity = false;
                _off = new ScriptExecutor(builder.ConnectionString, "System.Data.SqlClient");
            }
            return _off;
        }

        static public ICollection<Company> getCompanies()
        {
            ISet<Company> companies = new SortedSet<Company>();
            off().Run<Company>(
                                TSQLScriptBuilder.getCompanyList(),
                                companies,
                                TSQLScriptMapper.getCompanyByDbDataReader);
            return companies;
        }


        static public ICollection<TD_CRM_Accrual_Storno_LED> getDocuments()
        {
            ICollection<TD_CRM_Accrual_Storno_LED> documents = new List<TD_CRM_Accrual_Storno_LED>();

            List<DbParameter> parameters = new List<DbParameter>();
            parameters.Add(new SqlParameter
            {
                ParameterName = "@ExternalSystemID",
                SqlDbType = SqlDbType.VarChar,
                Size = 2,
                Value = "SI",
                Direction = ParameterDirection.Input
            });
            parameters.Add(new SqlParameter
            {
                ParameterName = "IdLegalEntity",
                SqlDbType = SqlDbType.VarChar,
                Size = 15,
                Value = "TDUSA",
                Direction = ParameterDirection.Input
            });

            off().Run<TD_CRM_Accrual_Storno_LED>(
                                "[dbo].[TD_CRM_Accrual_Storno_LED]",
                                parameters,
                                documents,
                                TSQLScriptMapper.getLEDByDbDataReader);
            return documents;
        }
    }
}
