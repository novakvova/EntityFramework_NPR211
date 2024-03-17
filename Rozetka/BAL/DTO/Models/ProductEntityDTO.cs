using DAL.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BAL.DTO.Models
{
    public class ProductEntityDTO : BaseEntityDTO<int>
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public int CategoryId { get; set; }
        public virtual CategoryEntityDTO Category { get; set; }
        public virtual ICollection<ProductImageEntityDTO> Images { get; set; }
        public virtual ICollection<BasketEntityDTO> Baskets { get; set; }
        public virtual ICollection<Sales_ProductEntityDTO> Sales_Products { get; set; }
        public virtual ICollection<OrderItemEntityDTO> OrderItems { get; set; }
    }
}
