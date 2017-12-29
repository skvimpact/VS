using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NAVOFFDWH_DAL
{
    public class ObjectRegister
    {
        static private IDictionary<string, int> _objects;
        static ObjectRegister()
        {
            _objects = new Dictionary<string, int>();
            _objects["GLEntry"] = 0;
            _objects["Company"] = 1;
            //_objects[""] = 0;
            //_objects[""] = 0;
            //_objects[""] = 0;
            //_objects[""] = 0;
            _objects["D0"] = 30;
            _objects["D1"] = 31;
            _objects["D2"] = 32;
            _objects["D3"] = 33;
            _objects["D4"] = 34;
            _objects["D5"] = 35;
        }

        public int? this[string BKey]
        {
            get
            {
                if (BKey == null) return null;
                if (!_objects.ContainsKey(BKey)) return null;
                return _objects[BKey];
            }
            set => _objects[BKey] = value ?? 0;
        }

    }
}
