using DAL.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BAL.DTO.Models
{
    public class Sales_ProductEntityDTO
    {
        public int ProductId { get; set; }
        public int SaleId { get; set; }

        public virtual ProductEntityDTO Product { get; set; }
        public virtual SaleEntityDTO Sale { get; set; }
    }
}
