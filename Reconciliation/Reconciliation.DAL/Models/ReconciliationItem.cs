using System;
using System.Runtime.Serialization;

namespace Reconciliation.DAL
{
    [DataContract(Name = "RI", Namespace = "")]
    public class ReconciliationItem : IComparable<ReconciliationItem>
    {
        [DataMember (Name="A")]
        public string D1 { get; set; }
        [DataMember (Name = "B")]
        public string D2 { get; set; }
        [DataMember (Name = "C")]
        public string D3 { get; set; }
        [DataMember (Name = "D")]
        public string D4 { get; set; }
        [DataMember (Name = "E")]
        public string D5 { get; set; }
        [DataMember (Name = "F")]
        public string D6 { get; set; }
        [DataMember (Name = "G")]
        public string D7 { get; set; }
        [DataMember (Name = "H")]
        public string D8 { get; set; }
        [DataMember(Name = "I")]
        public string D9 { get; set; }
        [DataMember(Name = "J")]
        public string D10 { get; set; }
        [DataMember(Name = "K")]
        public string D11 { get; set; }
        [DataMember(Name = "L")]
        public string D12 { get; set; }
        [DataMember(Name = "M")]
        public string D13 { get; set; }
        [DataMember(Name = "N")]
        public string D14 { get; set; }
        [DataMember(Name = "O")]
        public string D15 { get; set; }
        [DataMember(Name = "P")]
        public string D16 { get; set; }
        [DataMember(Name = "Q")]
        public string D17 { get; set; }
        [DataMember(Name = "R")]
        public string D18 { get; set; }
        [DataMember(Name = "S")]
        public string D19 { get; set; }
        [DataMember(Name = "T")]
        public string D20 { get; set; }
        [DataMember(Name = "U")]
        public string D21 { get; set; }
        [DataMember(Name = "V")]
        public string D22 { get; set; }
        [DataMember(Name = "W")]
        public string D23 { get; set; }
        [DataMember(Name = "X")]
        public string D24 { get; set; }
        [DataMember(Name = "Y")]
        public string D25 { get; set; }
        [DataMember(Name = "Z")]
        public string D26 { get; set; }
        [DataMember(Name = "a")]
        public string D27 { get; set; }
        [DataMember(Name = "b")]
        public string D28 { get; set; }
        [DataMember(Name = "c")]
        public string D29 { get; set; }
        [DataMember(Name = "d")]
        public string D30 { get; set; }
        [DataMember(Name = "y1")]
        public double L1 { get; set; }
        [DataMember(Name = "y2")]
        public double L2 { get; set; }
        [DataMember(Name = "y3")]
        public double L3 { get; set; }
        [DataMember(Name = "y4")]
        public double L4 { get; set; }
        [DataMember(Name = "z1")]
        public double R1 { get; set; }
        [DataMember(Name = "z2")]
        public double R2 { get; set; }
        [DataMember(Name = "z3")]
        public double R3 { get; set; }
        [DataMember(Name = "z4")]
        public double R4 { get; set; }

        public ReconciliationItem(DimensionSet dimensionSet, MeasureSet leftMeasureSet, MeasureSet rightMeasureSet)
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

        public int CompareTo(ReconciliationItem other)
        {
            int result = 1;
            if (other != null)
            {
                result = D1.CompareTo(other.D1);
                if (result == 0)
                {
                    result = D2.CompareTo(other.D2);
                    if (result == 0)
                    {
                        result = D3.CompareTo(other.D3);
                        if (result == 0)
                        {
                            result = D4.CompareTo(other.D4);
                            if (result == 0)
                            {
                                result = D5.CompareTo(other.D5);
                                if (result == 0)
                                {
                                    result = D6.CompareTo(other.D6);
                                    if (result == 0)
                                    {
                                        result = D7.CompareTo(other.D7);
                                        if (result == 0)
                                        {
                                            result = D8.CompareTo(other.D8);
                                            if (result == 0)
                                            {
                                                result = D9.CompareTo(other.D9);
                                                if (result == 0)
                                                {
                                                    result = D10.CompareTo(other.D10);
                                                    if (result == 0)
                                                    {
                                                        result = D11.CompareTo(other.D11);
                                                        if (result == 0)
                                                        {
                                                            result = D12.CompareTo(other.D12);
                                                            if (result == 0)
                                                            {
                                                                result = D13.CompareTo(other.D13);
                                                                if (result == 0)
                                                                {
                                                                    result = D14.CompareTo(other.D14);
                                                                    if (result == 0)
                                                                    {
                                                                        result = D15.CompareTo(other.D15);
                                                                        if (result == 0)
                                                                        {
                                                                            result = D16.CompareTo(other.D16);
                                                                            if (result == 0)
                                                                            {
                                                                                result = D17.CompareTo(other.D17);
                                                                                if (result == 0)
                                                                                {
                                                                                    result = D18.CompareTo(other.D18);
                                                                                    if (result == 0)
                                                                                    {
                                                                                        result = D19.CompareTo(other.D19);
                                                                                        if (result == 0)
                                                                                        {
                                                                                            result = D20.CompareTo(other.D20);
                                                                                            if (result == 0)
                                                                                            {
                                                                                                result = D21.CompareTo(other.D21);
                                                                                                if (result == 0)
                                                                                                {
                                                                                                    result = D22.CompareTo(other.D22);
                                                                                                    if (result == 0)
                                                                                                    {
                                                                                                        result = D23.CompareTo(other.D23);
                                                                                                        if (result == 0)
                                                                                                        {
                                                                                                            result = D24.CompareTo(other.D24);
                                                                                                            if (result == 0)
                                                                                                            {
                                                                                                                result = D25.CompareTo(other.D25);
                                                                                                                if (result == 0)
                                                                                                                {
                                                                                                                    result = D26.CompareTo(other.D26);
                                                                                                                    if (result == 0)
                                                                                                                    {
                                                                                                                        result = D27.CompareTo(other.D27);
                                                                                                                        if (result == 0)
                                                                                                                        {
                                                                                                                            result = D28.CompareTo(other.D28);
                                                                                                                            if (result == 0)
                                                                                                                            {
                                                                                                                                return D29.CompareTo(other.D29);
                                                                                                                            }
                                                                                                                        }
                                                                                                                    }
                                                                                                                }
                                                                                                            }
                                                                                                        }
                                                                                                    }
                                                                                                }
                                                                                            }
                                                                                        }
                                                                                    }
                                                                                }
                                                                            }
                                                                        }
                                                                    }
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return result;
        }
    }

}
