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

    [Table("GamingGoods")]
    public class GamingGoods
    {
        [Key]
        [DatabaseGenerated(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [StringLength(50)]
        public string GoodId { get; set; }
        [StringLength(255)]
        public string Image { get; set; }
        [StringLength(50)]
        public string Name { get; set; }
        public int LuckNum { get; set; }
    }
}