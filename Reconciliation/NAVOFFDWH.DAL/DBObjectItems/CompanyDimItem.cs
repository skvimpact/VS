using System.ComponentModel.DataAnnotations.Schema;

namespace NAVOFFDWH_DAL
{
    public class CompanyDimItem : DimItem
    {
        [Column("Legal Entity Id", TypeName = "varchar(10)")]
        public string LegalEntityId { get; set; }
        [Column("Legal Entity Type", TypeName = "varchar(20)")]
        public string LegalEntityType { get; set; }
    }
}
