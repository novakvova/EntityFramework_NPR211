using System;
using System.Collections.Generic;
using System.Text;

namespace BAL.DTO.Models
{
    public class CategoryEntityDTO: BaseEntityDTO<int>
    {
        public string Name { get; set; }
        public string Image { get; set; }
        public virtual ICollection<ProductEntityDTO> Products { get; set; }
        public override string ToString() => Name;
    }
}
