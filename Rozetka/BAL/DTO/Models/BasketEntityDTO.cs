using System;
using System.Collections.Generic;
using System.Text;

namespace BAL.DTO.Models
{
    public class BasketEntityDTO
    {
        public short Count { get; set; }
        public int ProductId { get; set; }
        public int UserId { get; set; }

        public virtual ProductEntityDTO Product { get; set; }
        public virtual UserEntityDTO User { get; set; }
    }
}
