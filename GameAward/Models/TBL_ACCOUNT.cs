using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.DynamicData;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GameAward.Models
{
    [Table("TBL_ACCOUNT")]
    public class TBL_ACCOUNT
    {
        [Key]
        [StringLength(50)]
        public string FLD_ID { get; set; }

        [StringLength(50)]
        public string FLD_NAME { get; set; }

        [StringLength(50)]
        public string FLD_PASSWORD { get; set; }

        public int FLD_ONLINE { get; set; }
        public long FLD_RXPIONT { get; set; }
        public long FLD_RXPIONTX { get; set; }
    }
}