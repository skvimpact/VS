using System;

namespace Reconciliation.DAL
{
    public class ParameterSet : IComparable<ParameterSet>
    {
        public int year { get; set; }
        public int month { get; set; }

        public ParameterSet(int year, int month)
        {
            this.year = year;
            this.month = month;
        }

        public int CompareTo(ParameterSet other)
        {
            int result = 1;
            if (other != null)
            {
                result = year.CompareTo(other.year);
                if (result == 0)
                {
                    return month.CompareTo(other.month);
                }
            }
            return result;
        }

        public override bool Equals(Object obj)
        {
            if (obj is ParameterSet && obj != null)
            {
                ParameterSet temp = (ParameterSet)obj;
                if (temp.year.Equals(this.year) && temp.month.Equals(this.month))
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
            return year * 100 + month;
        }

        public override string ToString()
        {
            return String.Format("year = '{0}'. month = '{1}'", year, month);
        }

    }
}
