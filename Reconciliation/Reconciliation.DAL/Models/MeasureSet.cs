using System;
using System.Data.Common;
using Script_Executor;
namespace Reconciliation.DAL
{
    public class MeasureSet : IMappable<MeasureSet>
    {
        static double delta = 0.009f;

        public double M1 { get; set; }
        public double M2 { get; set; }
        public double M3 { get; set; }
        public double M4 { get; set; }

        public bool IsNull()
        {

            if (
                 Math.Abs(M1) < delta
              && Math.Abs(M2) < delta
              && Math.Abs(M3) < delta
              && Math.Abs(M4) < delta
            )
            {
                return true;
            }
            return false;
        }

        public override bool Equals(Object obj)
        {
            if (obj is MeasureSet && obj != null)
            {
                MeasureSet temp = (MeasureSet)obj;
                if (
                    Math.Abs(temp.M1 - this.M1) < delta
                && Math.Abs(temp.M2 - this.M2) < delta
                && Math.Abs(temp.M3 - this.M3) < delta
                && Math.Abs(temp.M4 - this.M4) < delta
                )
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

        public MeasureSet GetFromDbDataReader(DbDataReader dr, String[] consts)
        {
            int _off = 27;
            return new MeasureSet
            {
                M1 = (double)(dr[1 + _off] ?? 0.0),
                M2 = (double)(dr[1 + _off] ?? 0.0),
                M3 = (double)(dr[1 + _off] ?? 0.0),
                M4 = (double)(dr[1 + _off] ?? 0.0)
            };
        }
    }
}
