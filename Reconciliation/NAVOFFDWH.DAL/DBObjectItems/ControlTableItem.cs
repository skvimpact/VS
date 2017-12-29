using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NAVOFFDWH_DAL
{
    public class ControlTableItem : DBObjectItem
    {
        [Column("Start Oltp Entry No", TypeName = "int")]
        public int StartOltpEntryNo { get; set; }
        [Column("End Oltp Entry No", TypeName = "int")]
        public int EndOltpEntryNo { get; set; }
        [Key]
        [Column("DBObject ID", TypeName = "tinyint")]
        public byte DBObjectID { get; set; }
        [Key]
        [Column("Company SKey", TypeName = "tinyint")]
        public byte CompanySKey { get; set; }
    }
}
