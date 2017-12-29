using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NAVOFFDWH_DAL
{
    public class ControlTable : DBObject
    {
        public ControlTable(string name, string company) : base(name, company)
        {
            _name = name;
            _company = company;
             DBObjectItemType = typeof(ControlTableItem).FullName;
        }        
    }
}
