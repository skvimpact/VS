using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TSQLExample.DAL
{
    public class Company : IComparable<Company>
    {
        public string name { get; set; }

        public Company(string name)
        {
            this.name = name;
        }

        public int CompareTo(Company other)
        {
            if (other == null) return 1;
            return name.CompareTo(other.name);
        }

        public override bool Equals(Object obj)
        {
            if (obj is Company && obj != null)
            {
                Company temp = (Company)obj;
                if (temp.name.Equals(this.name))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return false;
        }

        public override int GetHashCode()
        {
            return this.name.GetHashCode();
        }

    }
}
