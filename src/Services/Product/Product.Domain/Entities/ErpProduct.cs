using Product.Domain.Common;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Product.Domain.Entities
{
    [Table(name: "erpproducts", Schema = "public")]
    public class ErpProduct:IEntityBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RecId { get; set; }
        public string ProductName { get; set; }
        public string Barcode { get; set; }
        public decimal? Price { get; set; }

        public string Description { get; set; }

        
    }
}
