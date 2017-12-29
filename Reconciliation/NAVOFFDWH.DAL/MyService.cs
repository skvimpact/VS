using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NAVOFFDWH.DAL
{


    public class MyServiceTest
    {        
        public static int getCurrentEndOltpEntryNo(PieceOfWork pieceOfWork)
        {
            int GlobalFirstEntry = 1;
            return pieceOfWork?.EndOltpEntryNo ?? (GlobalFirstEntry - 1);
        }
    } 




    class ControlMetrics
    {
        public int OltpEntryNo { get; set; }
    }
}
