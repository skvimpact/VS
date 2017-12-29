using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NAVOFFDWH_DAL
{
    public interface IDBInsertable
    {
        string Fields { get; }
        string Values { get; }
    }
    public interface IControllable
    {
        string EntityName { get; }
        string Company { get; }
        int LastOltpEntryNo { get; }
    }

    public interface IController
    {
        string EntityName { get; }
        string Company { get; }
        int LastOltpEntryNo { get; }
    }
    public enum Status
    {
        PLANNED,
        IN_WORK,
        DONE
    }

    public class FillInfo
    {
        public int StartOltpEntryNo { get; set; }
        public int EndOltpEntryNo { get; set; }
    }

    [Obsolete]
    public class ControlTableItemOld : FillInfo, IDBInsertable
    {
        //public string EntityName { get; set; }
        //public string Company { get; set; }
        public byte DBObjectID { get; set; }
        public byte CompanySKey { get; set; }

        public string Fields => String.Format("[{0}],[{1}],[{2}],[{3}]", "DBObject ID", "Company SKey", "Start Oltp Entry No", "End Oltp Entry No");
        public string Values => String.Format("{0},{1},{2},{3}", DBObjectID, CompanySKey, StartOltpEntryNo, EndOltpEntryNo);
    }

    public class DwhControllerService
    {
        public ControlledObject[] controlledObject;

        public DwhControllerService()
        {
            controlledObject = new ControlledObject[]
            {
                new ControlledObject(99, 101)
            };
        }

        public PieceOfWork GetPieceOfWork(int i) => controlledObject[i].GetPieceOfWork();
        public void SetDone(int i, PieceOfWork pieceOfWork) => controlledObject[i].SetDone(pieceOfWork);
        public PieceOfWork[] Done(int i) => controlledObject[i].Done;
        public PieceOfWork[] InWork(int i) => controlledObject[i].InWork;
        public PieceOfWork[] Planned(int i) => controlledObject[i].Planned;
    }
}
