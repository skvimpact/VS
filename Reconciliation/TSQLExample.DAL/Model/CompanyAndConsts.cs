using System;
using System.Data.Common;
using Script_Executor;

namespace TSQLExample.DAL
{
    public class CompanyAndConsts : IComparable<CompanyAndConsts>, IMappable<CompanyAndConsts>
    {

        public string name1 { get; set; } = "?";
        public string name2 { get; set; } = "?";
        public string name3 { get; set; } = "Grand Zero";
        public string name4 { get; set; } = "?";

        public int CompareTo(CompanyAndConsts other)
        {
            if (other == null) return 1;
            return name1.CompareTo(other.name1);
        }

        public override bool Equals(Object obj)
        {
            if (obj is CompanyAndConsts && obj != null)
            {
                CompanyAndConsts temp = (CompanyAndConsts)obj;
                if (temp.name1.Equals(this.name1))
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
            return this.name1.GetHashCode();
        }

        public override string ToString()
        {
            return String.Format("{0};{1};{2};{3}", name1, name2, name3, name4);
        }

        public CompanyAndConsts GetFromDbDataReader(DbDataReader dr, String[] consts)
        {
            return new CompanyAndConsts
            {                
                name1 = (string)dr[0],
                name2 = (consts?.Length ?? 0) > 0 ? consts[0] : "Dummy",
                name3 = (consts?.Length ?? 0) > 1 ? consts[1] : name3,
                name4 = (consts?.Length ?? 0) > 2 ? consts[2] : "Dummy"
            };
            
        }
    }
}
