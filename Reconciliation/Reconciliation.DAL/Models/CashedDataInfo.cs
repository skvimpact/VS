using System;
using System.Runtime.Serialization;

namespace Reconciliation.DAL
{
    [DataContract(Name = "Cashed Data", Namespace = "")]
    public class CashedDataInfo : IComparable<CashedDataInfo>
    {
        [DataMember]
        public int Year { get; set; }
        [DataMember]
        public int Month { get; set; }
        [DataMember]
        public int Count { get; set; }
        [DataMember]
        public DateTime Start { get; set; }
        [DataMember]
        public DateTime End { get; set; }

        public CashedDataInfo(ParameterSet parameterSet, int count,DateTime start, DateTime end)
        {
            this.Year   = parameterSet.year;
            this.Month  = parameterSet.month;
            this.Count  = count;
            this.Start  = start;
            this.End    = end;
        }

        public int CompareTo(CashedDataInfo other)
        {
            int result = 1;
            if (other != null)
            {
                result = Year.CompareTo(other.Year);
                if (result == 0)
                {
                    return Month.CompareTo(other.Month);
                }
            }
            return result;
        }

    }
}
