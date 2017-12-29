using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NAVOFFDWH_DAL
{
    public class PieceOfWork : FillInfo
    {
        public int Size { get { return EndOltpEntryNo - StartOltpEntryNo + 1; } }
        public Status status;
        public bool NothingToDo { get { return (StartOltpEntryNo == 0) && (EndOltpEntryNo == 0); } }
        public override string ToString() => String.Format("{0}_{1}", StartOltpEntryNo, EndOltpEntryNo);
    }
}
