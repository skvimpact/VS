using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NAVOFFDWH_DAL
{
    public enum Base
    {
        OLTP,
        DWH
    }

    public enum Table
    {
        BOOK,
        CURRENCY,
        COMPANY,
        COUNTERPARTY,
        ACCOUNT,
        ACCRUAL_TYPE,
        FUND_STATUS,
        INC_TAX
    }

    public enum QueryType
    {
        SELECT,
        INSERT
    }

    public class Router
    {
        public  Base        Base { get; set; }
        public  Table       Table { get; set; }
        public  QueryType   QueryType { get; set; }
        public Router(Base baseType, Table tableName, QueryType queryType)
        {
            this.Base       = baseType;
            this.Table      = tableName;
            this.QueryType  = queryType;
        }
        public override bool Equals(Object obj) => obj?.ToString() == ToString();

        public override string ToString()
        {
            return String.Format("{0}_{1}_{2}", Base, Table, QueryType);
        }

        public override int GetHashCode()
        {
            return 100 * (int)Base + 10 * (int)Table + 1 * (int)QueryType;
        }
    }

    class ScriptService
    {
        private static Func<DbDataReader, CompanyDimItem> CompanyCreator = dr => new CompanyDimItem { BKey = (string)dr[0], LegalEntityId = (string)dr[1], LegalEntityType = (string)dr[2] };

        private static IDictionary<Table, string> _oltpDimensionCode = new Dictionary<Table, string>();
        public static IDictionary<Table, string> _dwhTables = new Dictionary<Table, string>();

        public static Func<DbDataReader, string>        BKeyCreator = dr => { return (string)dr[0]; };
        public static Func<DbDataReader, int>           SKeyCreator = dr => { return (int)dr[1]; };

        public static Func<string, string> TypicalDimTableCreator = (string name) =>
        {
            StringBuilder s = new StringBuilder();
            s.AppendFormat("CREATE TABLE[dbo].[{0}]", name);
            s.Append("(");
            s.Append("  [SKey][int] NOT NULL,");
            s.Append("  [BKey] [varchar] (20) NOT NULL,");
            s.Append("  [Name] [varchar] (50) NULL,");
            s.AppendFormat("  CONSTRAINT[PK_{0}] PRIMARY KEY CLUSTERED", name);
            s.Append("  (");
            s.Append("     [SKey] ASC");
            s.Append("  )");
            s.Append(")");
            return (s.ToString());
        };
        public static Func<string, string> CompanyTableCreator = (string name) =>
        {
            StringBuilder s = new StringBuilder();
            s.AppendFormat("CREATE TABLE[dbo].[{0}]", name);
            s.Append("(");
            s.Append("  [SKey][int] NOT NULL,");
            s.Append("  [BKey] [varchar] (30) NOT NULL,");
            s.Append("  [Legal Entity Id] [varchar] (15) NULL,");
            s.Append("  [Legal Entity Type] [varchar] (10) NULL,");
            s.AppendFormat("  CONSTRAINT[PK_{0}] PRIMARY KEY CLUSTERED", name);
            s.Append("  (");
            s.Append("     [SKey] ASC");
            s.Append("  )");
            s.Append(")");
            return (s.ToString());
        };

        public static Func<string, string> DimDwhSelector = (string name) =>
        {
            StringBuilder s = new StringBuilder();
            s.Append("select");
            s.Append("	[BKey],");
            s.Append("	[SKey]");
            s.Append("from");
            s.AppendFormat("	[dbo].[{0}]", name);
            return (s.ToString());
        };

        public static Func<string[], string> TypicalDimTableOltpSeletor = (string[] args) =>
        {
            StringBuilder s = new StringBuilder();
            s.Append("select");
            s.Append("  [Code],");
            s.Append("  [Name]");
            s.Append("from");
            s.Append("  dbo.[Dimension Value]");
            s.Append("where");
            s.AppendFormat("  [Dimension Code] = '{0}'", _oltpDimensionCode);
            s.Append((args?.Length ?? 0) > 0 ? String.Format("  and Code = '{0}'", args[0]) : "");
            return (s.ToString());
        };

        public static Func<string[], string> CompanyTableOltpSeletor = (string[] args) =>
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




    /*
    static ScriptService()
    {
        _oltpDimensionCode[Table.ACCRUAL_TYPE]  = "ACCRUAL.TYPE";
        _oltpDimensionCode[Table.BOOK]          = "BOOK";
        _oltpDimensionCode[Table.COUNTERPARTY]  = "COUNTERPARTY";
        _oltpDimensionCode[Table.FUND_STATUS]   = "FUND.STATUS";
        _oltpDimensionCode[Table.INC_TAX]       = "INC.TAX";            

        _dwhTables[Table.ACCRUAL_TYPE]  = "[dbo].[Accrual Type]";
        _dwhTables[Table.BOOK]          = "[dbo].[Book]";
        _dwhTables[Table.COUNTERPARTY]  = "[dbo].[Counterparty]";
        _dwhTables[Table.FUND_STATUS]   = "[dbo].[Fund Status]";
        _dwhTables[Table.INC_TAX]       = "[dbo].[Inc Tax]";
        _dwhTables[Table.COMPANY]       = "[dbo].[Company]";

    }

    public static string Script(Router rooter, string[] args)
    {            
        StringBuilder s = new StringBuilder();

        switch (rooter.Base)
        {
            case Base.OLTP:
                switch (rooter.Table)
                {
                    case Table.ACCRUAL_TYPE:
                    case Table.BOOK:
                    case Table.COUNTERPARTY:
                    case Table.FUND_STATUS:
                    case Table.INC_TAX:
                        s.Append("select");
                        s.Append("  [Code],");
                        s.Append("  [Name]");
                        s.Append("from");
                        s.Append("  dbo.[Dimension Value]");
                        s.Append("where");
                        s.AppendFormat("  [Dimension Code] = '{0}'", _oltpDimensionCode[rooter.Table]);
                        break;
                    case Table.COMPANY:
                        s.Append("select");
                        s.Append("  [Name],");
                        s.Append("	[Legal Entity Id],");
                        s.Append("	[Legal Entity Type]");
                        s.Append("from");
                        s.Append("	[dbo].[Company]");
                        s.Append((args?.Length ?? 0) > 0 ? "where" : "");
                        s.Append((args?.Length ?? 0) > 0 ? String.Format("    Name = '{0}'", args[0]) : "");
                        break;
                }
                break;
            case Base.DWH:
                switch (rooter.Table)
                {
                    case Table.ACCRUAL_TYPE:
                    case Table.BOOK:
                    case Table.COUNTERPARTY:
                    case Table.FUND_STATUS:
                    case Table.INC_TAX:
                    case Table.COMPANY:
                        s.Append("select");
                        s.Append("	[BKey],");
                        s.Append("	[SKey]");
                        s.Append("from");
                        s.AppendFormat("	{0}", _dwhTables[rooter.Table]);
                        break;
                }
                break;
        }
       // Console.WriteLine(s.ToString());   
        return s.ToString();
    }*/
}
}
