using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.DynamicData;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GameAward.Models
{

    [Table("GamingRecord")]
    public class GamingRecord
    {
        [Key]
        [DatabaseGenerated(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [StringLength(50)]
        public string UserId { get; set; }
        [StringLength(50)]
        public string UserName { get; set; }
        [StringLength(50)]
        public string UserSpace { get; set; }
        public DateTime CreateTime { get; set; }
        public bool Struts { get; set; }
        [StringLength(50)]
        public string ItemId { get; set; }
        [StringLength(50)]
        public string ItemName { get; set; }
    }
}