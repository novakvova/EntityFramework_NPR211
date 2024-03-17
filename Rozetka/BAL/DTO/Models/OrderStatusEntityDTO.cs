using System;
using System.Collections.Generic;
using System.Text;

namespace BAL.DTO.Models
{
    public class OrderStatusEntityDTO : BaseEntityDTO<int>
    {
        public string Name { get; set; }
        public virtual ICollection<OrderEntityDTO> Orders { get; set; }
    }
}
