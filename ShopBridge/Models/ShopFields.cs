using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ShopBridge.Models
{
    public class ShopFields
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProductId { get; set; }
        [Required]
        public string ProductName { get; set; }
       
        public double ProductPrice { get; set; }
        [MinLength(3)]
        public string ProductDesp { get; set; }

    }
}