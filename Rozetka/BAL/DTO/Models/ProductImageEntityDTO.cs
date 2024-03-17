using System;
using System.Collections.Generic;
using System.Text;

namespace BAL.DTO.Models
{
    public class ProductImageEntityDTO : BaseEntityDTO<int>
    {
        public string Name { get; set; }
        public short Priority { get; set; }
        public int ProductId { get; set; }
        public virtual ProductEntityDTO Product { get; set; }
    }
}
