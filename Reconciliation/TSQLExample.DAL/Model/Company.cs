using System;
using System.Data.Common;
using Script_Executor;

namespace TSQLExample.DAL
{
    public class Company : IComparable<Company>, IMappable<Company>
    {
        public string name { get; set; }

        public Company()
        {
            this.name = "DUMMY";
        }

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

        public Company GetFromDbDataReader(DbDataReader dr, String[] consts)
        {
            return new Company((string)dr[0]);
        }
    }
}
