
using System.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Script_Executor;
using Bulk_Inserter;
using System.Collections;


namespace NAVOFFDWH_DAL
{

    public class OltpTableAttribute : TableAttribute
    {
        public OltpTableAttribute(string name) : base(name) { }
    }

    public class DwhTableAttribute : TableAttribute
    {
        public DwhTableAttribute(string name) : base(name) { }
    }

    public abstract class DBObject
    {
        private IDictionary<string, string> _bulkMapper;
        protected string _name;
        protected string _company;        
        protected static ScriptExecutor _oltpExecutor;
        protected static ScriptExecutor _dwhExecutor;
        protected static BulkInserter   _bulkInserter;
        private string _insertStatement;

        public void DBInsert(DBObjectItem item)
        {
            _dwhExecutor.Run(String.Format(InsertStatement, item.GetValues()));
        }

        public string TableCreator
        {
            get
            {
                ColumnAttribute columnAttribute;
                StringBuilder s = new StringBuilder();
                List<string> keys = new List<string>();
                bool thisIsKey;
                s.AppendFormat($"CREATE TABLE [dbo].[{_name}]");
                s.Append("(");
                foreach (var property in Type.GetType(DBObjectItemType).GetProperties())
                {
                    if ((columnAttribute = (ColumnAttribute)property.GetCustomAttributes(false).Where(x => x is ColumnAttribute).FirstOrDefault()) != null)
                    {
                        if (thisIsKey = (KeyAttribute)property.GetCustomAttributes(false).Where(x => x is KeyAttribute).FirstOrDefault() != null)
                        {
                            keys.Add(String.Format("[{0}] ASC", columnAttribute.Name));
                        }                            
                        s.AppendFormat("   [{0}] {1} {2},",
                            columnAttribute.Name,
                            columnAttribute.TypeName,
                            (RequiredAttribute)property.GetCustomAttributes(false).Where(x => x is RequiredAttribute).FirstOrDefault() != null ? "NOT NULL" : thisIsKey ? "NOT NULL" : "NULL"
                            );
                    }
                }
                s.AppendFormat($"  CONSTRAINT[PK_{_name}] PRIMARY KEY CLUSTERED");
                s.Append("  (");
                s.Append(String.Join(",", keys));
                s.Append("  )");
                s.Append(")");
                return (s.ToString());
            }
        }

        protected string DBObjectItemType { get; set; }

        public string InsertStatement
        {
            get
            {
                if (_insertStatement == null)
                {
                    ColumnAttribute columnAttribute;
                    List<string> fields = new List<string>();
                    List<string> values = new List<string>();
                    int index = 0;
                    foreach (var property in Type.GetType(DBObjectItemType).GetProperties())
                    {
                        if ((columnAttribute = ((ColumnAttribute)property.GetCustomAttributes(false).Where(x => x is ColumnAttribute).FirstOrDefault())) != null)
                        {
                            values.Add((columnAttribute.TypeName ?? "varchar").ToLower().Contains("varchar") ? " '{" + index + "}'" : " {" + index + "}"); index++;
                            fields.Add(String.Format(" [{0}]", columnAttribute.Name));
                        }
                    }
                    _insertStatement = String.Format("insert into {0} ({1}) values({2})", _name,
                                                                                          String.Join(",", fields),
                                                                                          String.Join(",", values));
                }
                return _insertStatement;
            }
        }


        // public abstract string GeItemType();

        public Func<string[], string> OltpSelector;
      //  public Func<string[], string> DwhInserter;
      //  private Func<string[], string> _tableCreator;

        public IDictionary<string, string> BulkMapper
        {
            get
            {
                if (_bulkMapper == null)
                {
                    ColumnAttribute columnAttribute;
                    _bulkMapper = new Dictionary<string, string>();
                    foreach (var property in Type.GetType(DBObjectItemType).GetProperties())
                    {
                        if ((columnAttribute = ((ColumnAttribute)property.GetCustomAttributes(false).Where(x => x is ColumnAttribute).FirstOrDefault())) != null)
                        {
                            _bulkMapper[property.Name] = columnAttribute.Name;
                        }
                    }
                }
                return _bulkMapper;
            }
        }

        protected DBObject(string name, string company)
        {
            _name = name;
            _company = company;
        }

        static DBObject()
        {
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
            SqlConnectionStringBuilder dbuilder = new SqlConnectionStringBuilder();

            builder.DataSource = "nav-sql-bck";
            builder.InitialCatalog = "NAV-Offshores-UAT";
            builder.UserID = "ETL";
            builder.Password = "Pas_Nav0210!";
            builder.IntegratedSecurity = false;
            
            dbuilder["Data Source"] = "nav-ssis-dev";
            dbuilder["integrated Security"] = true;
            dbuilder["Initial Catalog"] = "Monitor";
            

            _oltpExecutor = new ScriptExecutor(builder.ConnectionString, "System.Data.SqlClient");
            _dwhExecutor = new ScriptExecutor(dbuilder.ConnectionString, "System.Data.SqlClient");
            _bulkInserter = new BulkInserter(dbuilder.ConnectionString);
        }

        public void CreateDBObject()
        {
            _dwhExecutor.Run(TableCreator);
        }

        public void InsertItem(IDBInsertable item)
        {
            _dwhExecutor.Run(String.Format("insert into [dbo].[{0}] ({1}) values({2})", _name, item.Fields, item.Values));
        }
    }
}
