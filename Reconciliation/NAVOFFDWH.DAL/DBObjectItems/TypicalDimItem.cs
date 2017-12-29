using System.ComponentModel.DataAnnotations.Schema;

namespace NAVOFFDWH_DAL
{
    public class TypicalDimItem : DimItem
    {
        [Column("Name", TypeName = "varchar(50)")]
        public string Name { get; set; }
    }
}
