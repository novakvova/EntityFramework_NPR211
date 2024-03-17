using System;
using System.Collections.Generic;
using System.Text;

namespace BAL.DTO.Models
{
    public class OrderEntityDTO : BaseEntityDTO<int>
    {
        public int OrderStatusId { get; set; }
        public virtual OrderStatusEntityDTO OrderStatus { get; set; }
        public int UserId { get; set; }
        public virtual UserEntityDTO User { get; set; }
        public virtual ICollection<OrderItemEntityDTO> OrderItems { get; set; }
    }
}
