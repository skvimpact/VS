using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NAVOFFDWH_DAL
{
    public abstract class DimItem : DBObjectItem, IComparable<DimItem>//, IBulkInsertable<DimItem>
    {
        [Key]
        [Column("SKey", TypeName = "int")]
        public int SKey { get; set; }
        [Required]
        [Column("BKey", TypeName = "varchar(20)")]
        public string BKey { get; set; }

        public int CompareTo(DimItem other) => BKey?.CompareTo(other?.BKey) ?? -1;
        public override bool Equals(Object obj) => obj?.ToString() == ToString();
        public override string ToString() => this.BKey;
        public override int GetHashCode() => this.BKey.GetHashCode();

        public IDictionary<string, string> BulkMapper()
        {
            throw new NotImplementedException();
        }
    }
}
