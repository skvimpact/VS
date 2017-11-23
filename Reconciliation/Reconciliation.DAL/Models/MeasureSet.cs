using System;
using System.Runtime.Serialization;

namespace Reconciliation.DAL
{
    [DataContract(Namespace = "")]
    public class MeasureSet
    {
        [DataMember]
        public double M1 { get; set; }
        [DataMember]
        public double M2 { get; set; }
        [DataMember]
        public double M3 { get; set; }
        [DataMember]
        public double M4 { get; set; }

        public MeasureSet(double M1 = 0, double M2 = 0, double M3 = 0, double M4 = 0)
        {
            this.M1 = M1;
            this.M2 = M2;
            this.M3 = M3;
            this.M4 = M4;
        }

        public override bool Equals(Object obj)
        {
            double delta = 0.009f;
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
    }
}
