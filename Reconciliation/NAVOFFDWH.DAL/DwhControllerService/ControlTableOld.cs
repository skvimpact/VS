using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Threading.Tasks;

namespace NAVOFFDWH_DAL
{
    [Table("", Schema = "dbo")]
    [Obsolete]
    public class ControlTableOld : DBObject
    {
        private byte _dBObjectID { get; set; }     
        private byte _companySKey { get; set; }
        
        public ControlTableOld(byte dBObjectID, byte companySKey) : base("Control Table", "")
        {
            _dBObjectID = dBObjectID;
            _companySKey = companySKey;
        }

        public void Insert(int start, int end)
        {            
            InsertItem(new ControlTableItemOld { DBObjectID = _dBObjectID, CompanySKey = _companySKey, StartOltpEntryNo = start, EndOltpEntryNo = end });
        }
    }
}
