using System;
using System.Data.Common;
using Script_Executor;

namespace Reconciliation.DAL
{

    public class Account : IComparable<Account>, IMappable<Account>
    {
        public string name { get; set; }

        public Account()
        {
            this.name = "DUMMY";
        }
        public Account(string name)
        {
            this.name = name;
        }

        public int CompareTo(Account other)
        {
            if (other == null) return 1;
            return name.CompareTo(other.name);
        }

        public override bool Equals(Object obj)
        {
            if (obj is Account && obj != null)
            {
                Account temp = (Account)obj;
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

        public Account GetFromDbDataReader(DbDataReader dr, String[] consts)
        {
            return new Account((string)dr[0]);
        }

    }
}
