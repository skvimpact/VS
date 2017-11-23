using System;
using System.Runtime.Serialization;
namespace Reconciliation.DAL
{
    [DataContract(Namespace = "")]
    public class Account : IComparable<Account>
    {
        [DataMember]
        public string name { get; set; }

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

    }
}
