using System;
using System.Runtime.Serialization;

namespace Reconciliation.DAL
{
    [DataContract(Namespace = "")]
    public class RecItem
    {
        [DataMember]
        public string D1 { get; set; }
        [DataMember]
        public string D2 { get; set; }
        [DataMember]
        public string D3 { get; set; }
        [DataMember]
        public string D4 { get; set; }
        [DataMember]
        public string D5 { get; set; }
        [DataMember]
        public string D6 { get; set; }
        [DataMember]
        public string D7 { get; set; }
        [DataMember]
        public string D8 { get; set; }
        [DataMember]
        public string D9 { get; set; }
        [DataMember]
        public string D10 { get; set; }
        [DataMember]
        public string D11 { get; set; }
        [DataMember]
        public string D12 { get; set; }
        [DataMember]
        public string D13 { get; set; }
        [DataMember]
        public string D14 { get; set; }
        [DataMember]
        public string D15 { get; set; }
        [DataMember]
        public string D16 { get; set; }
        [DataMember]
        public string D17 { get; set; }
        [DataMember]
        public string D18 { get; set; }
        [DataMember]
        public string D19 { get; set; }
        [DataMember]
        public string D20 { get; set; }
        [DataMember]
        public string D21 { get; set; }
        [DataMember]
        public string D22 { get; set; }
        [DataMember]
        public string D23 { get; set; }
        [DataMember]
        public string D24 { get; set; }
        [DataMember]
        public string D25 { get; set; }
        [DataMember]
        public string D26 { get; set; }
        [DataMember]
        public string D27 { get; set; }
        [DataMember]
        public string D28 { get; set; }
        [DataMember]
        public string D29 { get; set; }
        [DataMember]
        public string D30 { get; set; }
        [DataMember]
        public double L1 { get; set; }
        [DataMember]
        public double L2 { get; set; }
        [DataMember]
        public double L3 { get; set; }
        [DataMember]
        public double L4 { get; set; }
        [DataMember]
        public double R1 { get; set; }
        [DataMember]
        public double R2 { get; set; }
        [DataMember]
        public double R3 { get; set; }
        [DataMember]
        public double R4 { get; set; }

        public RecItem(DimensionSet dimensionSet, MeasureSet leftMeasureSet, MeasureSet rightMeasureSet)
        {
            this.D1  = dimensionSet.D1;
            this.D2  = dimensionSet.D2;
            this.D3  = dimensionSet.D3;
            this.D4  = dimensionSet.D4;
            this.D5  = dimensionSet.D5;
            this.D6  = dimensionSet.D6;
            this.D7  = dimensionSet.D7;
            this.D8  = dimensionSet.D8;
            this.D9  = dimensionSet.D9;
            this.D10 = dimensionSet.D10;
            this.D11 = dimensionSet.D11;
            this.D12 = dimensionSet.D12;
            this.D13 = dimensionSet.D13;
            this.D14 = dimensionSet.D14;
            this.D15 = dimensionSet.D15;
            this.D16 = dimensionSet.D16;
            this.D17 = dimensionSet.D17;
            this.D18 = dimensionSet.D18;
            this.D19 = dimensionSet.D19;
            this.D20 = dimensionSet.D20;
            this.D21 = dimensionSet.D21;
            this.D22 = dimensionSet.D22;
            this.D23 = dimensionSet.D23;
            this.D24 = dimensionSet.D24;
            this.D25 = dimensionSet.D25;
            this.D26 = dimensionSet.D26;
            this.D27 = dimensionSet.D27;
            this.D28 = dimensionSet.D28;
            this.D29 = dimensionSet.D29;
            this.D30 = dimensionSet.D30;

            if (leftMeasureSet != null)
            {
                this.L1 = leftMeasureSet.M1;
                this.L2 = leftMeasureSet.M2;
                this.L3 = leftMeasureSet.M3;
                this.L4 = leftMeasureSet.M4;
            }

            if (rightMeasureSet != null)
            {
                this.R1 = rightMeasureSet.M1;
                this.R2 = rightMeasureSet.M2;
                this.R3 = rightMeasureSet.M3;
                this.R4 = rightMeasureSet.M4;
            }
        }
    }

}
