using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NAVOFFDWH_DAL
{
    public abstract class FactN : DBObject, IControllable
    {
        //public IList<GLEntryOltp> _glEntryOltpBuffer;
        //public IList<GLEntryDwh> _glEntryDwhBuffer;
        //public string GetName { get { return _name; } }

        private class Int { public int Value { get; set; } }

        public int LastOltpEntryNo { get {
                Int last = _oltpExecutor.GetObject<Int>(_lastOltpEntrySelector(null),
                                                        (dr) => new Int { Value = dr.IsDBNull(0) ? 0 : (int)dr[0] });
                return last.Value; }}

        protected Func<string[], string> _lastOltpEntrySelector;

        protected string _oltpTableName { get; set; }

        protected FactN(string name, string company, string oltpTableName) : base(name, company)
        {
            _oltpTableName = oltpTableName;
        }

        public string EntityName => _name;

        public string Company => _company;

    }

    public abstract class FactBundle
    {
        public FactN[] companies;
        public FactBundle(FactN[] companies)
        {            
            this.companies = companies;
        }
    }

    public class GLEntryBundle : FactBundle
    {    
        public GLEntryBundle(GLEntryFact[] companies) : base(companies.Select(x => (FactN)x).ToArray()){}
    }

    public class GLEntryFact : FactN
    {

        public GLEntryFact(string company) : base("GL Entry", company, "G_L Entry")
        {
            OltpSelector = (args) =>
            {
                StringBuilder s = new StringBuilder();
                s.Append("select");
                s.Append("  Top 10");
                s.Append("  [Entry No_]");
                s.Append("from");
                s.AppendFormat("  dbo.[{0}${1}]", company, _oltpTableName);
                return (s.ToString());
            };

            _lastOltpEntrySelector = (args) =>
            {
                StringBuilder s = new StringBuilder();
                s.Append("select");
                s.Append("  MAX([Entry No_])");                
                s.Append("from");
                s.AppendFormat("  dbo.[{0}${1}]", company, _oltpTableName);
                return (s.ToString());
            };


            /*TableCreator = () =>
            {
                StringBuilder s = new StringBuilder();
                s.AppendFormat("CREATE TABLE[dbo].[{0}]", _name);
                s.Append("(");
                s.Append("  [Entry No][int] NOT NULL,");
                s.Append("  [LE GLE] [int] NOT NULL");
                s.AppendFormat("  CONSTRAINT[PK_{0}] PRIMARY KEY CLUSTERED", _name);
                s.Append("  (");
                s.Append("     [SKey] ASC");
                s.Append("  )");
                s.Append(")");
                return (s.ToString());
            };*/
           // _lastOltpEntryNo = () => 6560000;
        }
    }
}
