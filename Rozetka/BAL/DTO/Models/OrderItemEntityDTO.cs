using System;
using System.Collections.Generic;
using System.Text;

namespace BAL.DTO.Models
{
    public class OrderItemEntityDTO : BaseEntityDTO<int>
    {
        public decimal PriceBuy { get; set; }
        public short Count { get; set; }
        public int OrderId { get; set; }
        public virtual OrderEntityDTO Order { get; set; }
        public int ProductId { get; set; }
        public virtual ProductEntityDTO Product { get; set; }
    }
}
