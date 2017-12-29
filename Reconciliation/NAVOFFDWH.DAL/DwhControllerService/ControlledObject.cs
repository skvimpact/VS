using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NAVOFFDWH_DAL
{
    public class ControlledObject
    {
        private byte _dBObjectID { get; set; }
        private byte _companySKey { get; set; }

        private int _globalFirstEntry;
        private int _globalLastEntry;
        private int _bufferSize;
        private IList<FillInfo> _controlTable;
        private IList<FillInfo> _gapTable;
        private IList<PieceOfWork> _toDoTable;

        private ControlTableOld controlTable;



        public PieceOfWork[] Planned { get { return _toDoTable.Where(x => x.status == Status.PLANNED).Select(x => x).ToArray(); } }
        public PieceOfWork[] InWork { get { return _toDoTable.Where(x => x.status == Status.IN_WORK).Select(x => x).ToArray(); } }
        public PieceOfWork[] Done { get { return _toDoTable.Where(x => x.status == Status.DONE).Select(x => x).ToArray(); } }

        public ControlledObject(byte dBObjectID, byte companySKey , int first = 1, int bufferSize = 10)
        {
            _dBObjectID = dBObjectID;
            _companySKey = companySKey;

            controlTable = new ControlTableOld(_dBObjectID, _companySKey);

            _globalFirstEntry = first;
            _bufferSize = bufferSize;
            _globalLastEntry = 100; //!!!!!!!!!!!!!
            _controlTable = new List<FillInfo>();
            _gapTable = new List<FillInfo>();
            _toDoTable = new List<PieceOfWork>();
            UpdateInfo();
        }

        public void UpdateInfo()
        {
            fillControlTable();
            fillGapTable();            
            fillToDoTable();
        }

        private void fillControlTable()
        {
            _controlTable?.Clear();
            _controlTable = new List<FillInfo>()
            {
                new FillInfo { StartOltpEntryNo = 11, EndOltpEntryNo = 20 }
                //,new PieceOfWork { StartOltpEntryNo = 99, EndOltpEntryNo = 2000 }
                //,new PieceOfWork { StartOltpEntryNo = 10, EndOltpEntryNo = 20 }
            };
        }

        private void fillGapTable()
        {
            _gapTable?.Clear();
            FillInfo previous = null,
                     current = null;

            foreach (var item in _controlTable.OrderBy(i => i.StartOltpEntryNo))
            {
                current = item;
                int previousEndOltpEntryNo = getCurrentEndOltpEntryNo(previous),
                    gap = current.StartOltpEntryNo - previousEndOltpEntryNo;

                if (gap > 1) _gapTable.Add(new FillInfo { StartOltpEntryNo = previousEndOltpEntryNo + 1, EndOltpEntryNo = current.StartOltpEntryNo - 1 });
                previous = current;
            }

            if ((current?.EndOltpEntryNo ?? 0) < _globalLastEntry)
                _gapTable.Add(new FillInfo { StartOltpEntryNo = getCurrentEndOltpEntryNo(current) + 1, EndOltpEntryNo = _globalLastEntry });
        }

        private void fillToDoTable()
        {
            _toDoTable?.Clear();
            foreach (var gap in _gapTable)
            {
                int Start = gap.StartOltpEntryNo;
                int End = gap.EndOltpEntryNo;

                while (Start <= End)
                {
                    PieceOfWork newPieceOfWork = new PieceOfWork { StartOltpEntryNo = Start, EndOltpEntryNo = ((End - Start) >= _bufferSize) ? Start + _bufferSize - 1 : End, status = Status.PLANNED };
                    Start += newPieceOfWork.Size;
                    _toDoTable.Add(newPieceOfWork);
                }
            }
        }

        private int getCurrentEndOltpEntryNo(FillInfo pieceOfWork)
        {
            return pieceOfWork?.EndOltpEntryNo ?? (_globalFirstEntry - 1);
        }

        public PieceOfWork GetPieceOfWork()
        {
            PieceOfWork result = default(PieceOfWork);
            result = _toDoTable.Where(x => x.status == Status.PLANNED).FirstOrDefault();
            if (result != null)
                result.status = Status.IN_WORK;
            return result;
        }

        public void SetDone(PieceOfWork pieceOfWork)
        {
            PieceOfWork result = _toDoTable.Where(x => x.StartOltpEntryNo == pieceOfWork.StartOltpEntryNo).First();
            result.status = Status.DONE;
             controlTable.Insert(result.StartOltpEntryNo, result.EndOltpEntryNo);
        }

    }
}
