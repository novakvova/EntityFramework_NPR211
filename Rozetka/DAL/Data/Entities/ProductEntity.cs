using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DAL.Data.Entities
{
    [Table("tblProducts")]
    public class ProductEntity : BaseEntity<int>
    {
        [Required, StringLength(255)]
        public string Name { get; set; }
        public decimal Price { get; set; }
        [StringLength(4000)]
        public string Description { get; set; }
        [ForeignKey("Category")]
        public int CategoryId { get; set; }
        public virtual CategoryEntity Category { get; set; }
        public virtual ICollection<ProductImageEntity> Images { get; set; }
        public virtual ICollection<BasketEntity> Baskets { get; set; }
        public virtual ICollection<Sales_ProductEntity> Sales_Products { get; set; }
        public virtual ICollection<OrderItemEntity> OrderItems { get; set; }
    }
}
