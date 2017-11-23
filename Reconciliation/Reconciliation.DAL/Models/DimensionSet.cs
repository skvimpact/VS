using System;
using System.Runtime.Serialization;
namespace Reconciliation.DAL
{
    [DataContract(Namespace = "")]
    public class DimensionSet : IComparable<DimensionSet>
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

        public DimensionSet(string D1 = "",  string D2 = "",  string D3 = "",  string D4 = "",  string D5 = "",
                            string D6 = "",  string D7 = "",  string D8 = "",  string D9 = "",  string D10 = "",
                            string D11 = "", string D12 = "", string D13 = "", string D14 = "", string D15 = "",
                            string D16 = "", string D17 = "", string D18 = "", string D19 = "", string D20 = "",
                            string D21 = "", string D22 = "", string D23 = "", string D24 = "", string D25 = "",
                            string D26 = "", string D27 = "", string D28 = "", string D29 = "", string D30 = "")
        {
            this.D1 = D1;
            this.D2 = D2;
            this.D3 = D3;
            this.D4 = D4;
            this.D5 = D5;
            this.D6 = D6;
            this.D7 = D7;
            this.D8 = D8;
            this.D9 = D9;
            this.D10 = D10;
            this.D11 = D11;
            this.D12 = D12;
            this.D13 = D13;
            this.D14 = D14;
            this.D15 = D15;
            this.D16 = D16;
            this.D17 = D17;
            this.D18 = D18;
            this.D19 = D19;
            this.D20 = D20;
            this.D21 = D21;
            this.D22 = D22;
            this.D23 = D23;
            this.D24 = D24;
            this.D25 = D25;
            this.D26 = D26;
            this.D27 = D27;
            this.D28 = D28;
            this.D29 = D29;
            this.D30 = D30;
        }

        public int CompareTo(DimensionSet other)
        {
            //int result2 = D1.CompareTo(null);

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
        /*
        public override int GetHashCode()
        {
            int first = 1;// "A".GetHashCode();
            int second = 1;// "A".GetHashCode();
            long third = (long)second << 32;
            long mmm = 9000;

            long myLong = ((long)second << 32) + first;

            int h = myLong.GetHashCode();

            int hh2 = getHash(1, 1);


            int hh3 = 4294967296.GetHashCode();
            int hh4 = mmm.GetHashCode();



            return 0;
            //return base.GetHashCode();
        }
        private int getHash(int a, int b)
        {
            return (((long)a << 32) + b).GetHashCode();
        }
        */

        public override bool Equals(Object obj)
        {
            if (obj is DimensionSet && obj != null)
            {
                DimensionSet temp = (DimensionSet)obj;
                if (
                    temp.D1.Equals(this.D1)
                 && temp.D2.Equals(this.D2)
                 && temp.D3.Equals(this.D3)
                 && temp.D4.Equals(this.D4)
                 && temp.D5.Equals(this.D5)
                 && temp.D6.Equals(this.D6)
                 && temp.D7.Equals(this.D7)
                 && temp.D8.Equals(this.D8)
                 && temp.D9.Equals(this.D9)
                 && temp.D10.Equals(this.D10)
                 && temp.D11.Equals(this.D11)
                 && temp.D12.Equals(this.D12)
                 && temp.D13.Equals(this.D13)
                 && temp.D14.Equals(this.D14)
                 && temp.D15.Equals(this.D15)
                 && temp.D16.Equals(this.D16)
                 && temp.D17.Equals(this.D17)
                 && temp.D18.Equals(this.D18)
                 && temp.D19.Equals(this.D19)
                 && temp.D20.Equals(this.D20)
                 && temp.D21.Equals(this.D21)
                 && temp.D22.Equals(this.D22)
                 && temp.D23.Equals(this.D23)
                 && temp.D24.Equals(this.D24)
                 && temp.D25.Equals(this.D25)
                 && temp.D26.Equals(this.D26)
                 && temp.D27.Equals(this.D27)
                 && temp.D28.Equals(this.D28)
                 && temp.D29.Equals(this.D29)
                    //  && (temp.D30).Equals(this.D30)
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
