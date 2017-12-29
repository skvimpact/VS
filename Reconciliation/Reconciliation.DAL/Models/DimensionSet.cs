using System;
using System.Data.Common;
using Script_Executor;

namespace Reconciliation.DAL
{

    public class DimensionSet : IComparable<DimensionSet>, IMappable<DimensionSet>
    {
        public string D1 { get; set; }
        public string D2 { get; set; }
        public string D3 { get; set; }
        public string D4 { get; set; }
        public string D5 { get; set; }
        public string D6 { get; set; }
        public string D7 { get; set; }
        public string D8 { get; set; }
        public string D9 { get; set; }
        public string D10 { get; set; }
        public string D11 { get; set; }
        public string D12 { get; set; }
        public string D13 { get; set; }
        public string D14 { get; set; }
        public string D15 { get; set; }
        public string D16 { get; set; }
        public string D17 { get; set; }
        public string D18 { get; set; }
        public string D19 { get; set; }
        public string D20 { get; set; }
        public string D21 { get; set; }
        public string D22 { get; set; }
        public string D23 { get; set; }
        public string D24 { get; set; }
        public string D25 { get; set; }
        public string D26 { get; set; }
        public string D27 { get; set; }
        public string D28 { get; set; }
        public string D29 { get; set; }
        public string D30 { get; set; }

        public int CompareTo(DimensionSet other)
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

        public DimensionSet GetFromDbDataReader(DbDataReader dr, String[] consts)
        {
            int _off = -1;
            return new DimensionSet
            {
                D1 = consts[0],
                D30 = consts[1],
                D2  = (string)(dr[1 + _off] ?? ""),
                D3  = (string)(dr[2 + _off] ?? ""),
                D4  = (string)(dr[3 + _off] ?? ""),
                D5  = (string)(dr[4 + _off] ?? ""),
                D6  = (string)(dr[5 + _off] ?? ""),
                D7  = (string)(dr[6 + _off] ?? ""),
                D8  = (string)(dr[7 + _off] ?? ""),
                D9  = (string)(dr[8 + _off] ?? ""),
                D10 = (string)(dr[9 + _off] ?? ""),
                D11 = (string)(dr[10 + _off] ?? ""),
                D12 = (string)(dr[11 + _off] ?? ""),
                D13 = (string)(dr[12 + _off] ?? ""),
                D14 = (string)(dr[13 + _off] ?? ""),
                D15 = (string)(dr[14 + _off] ?? ""),
                D16 = (string)(dr[15 + _off] ?? ""),
                D17 = (string)(dr[16 + _off] ?? ""),
                D18 = (string)(dr[17 + _off] ?? ""),
                D19 = (string)(dr[18 + _off] ?? ""),
                D20 = (string)(dr[19 + _off] ?? ""),
                D21 = (string)(dr[20 + _off] ?? ""),
                D22 = (string)(dr[21 + _off] ?? ""),
                D23 = (string)(dr[22 + _off] ?? ""),
                D24 = (string)(dr[23 + _off] ?? ""),
                D25 = (string)(dr[24 + _off] ?? ""),
                D26 = (string)(dr[25 + _off] ?? ""),
                D27 = (string)(dr[26 + _off] ?? ""),
                D28 = (string)(dr[27 + _off] ?? ""),
                D29 = (string)(dr[28 + _off] ?? "")
            };
        }
    }
}
