using System;
using System.Data.Common;

namespace Reconciliation.DAL
{

    public class MDXScriptMapper
    {
       static public DimensionSet getDimensionSetByDbDataReader(DbDataReader dr, String[] args)
        {
            int _off = -1;
            return (new DimensionSet
                        {                                       
                            D1  = args[0],
                            D30 = args[1],
                            D2  = (string)(dr[1  + _off] ?? ""),
                            D3  = (string)(dr[2  + _off] ?? ""),
                            D4  = (string)(dr[3  + _off] ?? ""),
                            D5  = (string)(dr[4  + _off] ?? ""),
                            D6  = (string)(dr[5  + _off] ?? ""),
                            D7  = (string)(dr[6  + _off] ?? ""),
                            D8  = (string)(dr[7  + _off] ?? ""),
                            D9  = (string)(dr[8  + _off] ?? ""),
                            D10 = (string)(dr[9  + _off] ?? ""),
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
                        }
                    );
        }

        static public MeasureSet getMeasureSetByDbDataReader(DbDataReader dr)
        {
            int _off = 27;
            return (new MeasureSet
                        {
                            M1 = (double)(dr[1 + _off] ?? 0.0),
                            M2 = (double)(dr[1 + _off] ?? 0.0),
                            M3 = (double)(dr[1 + _off] ?? 0.0),
                            M4 = (double)(dr[1 + _off] ?? 0.0)
                        }
                    );
        }
    }
}
