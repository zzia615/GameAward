using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace GameAward.Models
{
    [Table("TBL_XWWL_Char")]
    public class TBL_XWWL_Char
    {
        [Key]
        [StringLength(50)]
        public string FLD_NAME { get; set; }

        public int FLD_ZS { get; set; }

        public byte[] FLD_ITEM { get; set; }

        [StringLength(50)]
        public string FLD_ID { get; set; }
        public int FLD_JOB { get; set; }
        public int FLD_ZX { get; set; }
        [NotMapped]
        public List<ItemClass> ItemsClass { get; set; }
    }
    public class ItemClass
    {
        public bool Checked { get; set; }
        public bool FLD_Bind { get; set; }
        public int FLD_CjFh { get; set; }
        public string FLD_GF { get; set; }
        public long FLD_ID { get; set; }
        public int FLD_ItmeID{ get; set; }
        public int FLD_JingHua { get; set; }
        public int FLD_MAGIC0 { get; set; }
        public int FLD_MAGIC1 { get; set; }
        public int FLD_MAGIC2 { get; set; }
        public int FLD_MAGIC3 { get; set; }
        public int FLD_MAGIC4 { get; set; }
        public string FLD_NAME { get; set; }
        public int FLD_Suliang { get; set; }
        public int FLD_Type { get; set; }
        public int FLD_ZjFh { get; set; }
        public int ID { get; set; }
    }

}