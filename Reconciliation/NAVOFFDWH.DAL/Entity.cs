using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NAVOFFDWH.DAL
{
    public abstract class Entity //: IControllable
    {
        protected string _name;
        protected string _company;

        public Func<string[], string> OltpSelector;
        public Func<string> TableCreator;

        protected Entity(string name, string company)
        {
            _name = name;
            _company = company;
        }        
    }

}
