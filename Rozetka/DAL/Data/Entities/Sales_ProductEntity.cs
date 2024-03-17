using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DAL.Data.Entities
{
    [Table("tblSales_ProductEnity")]
    public class Sales_ProductEntity
    {
        [ForeignKey("Product")]
        public int ProductId { get; set; }
        [ForeignKey("Sale")]
        public int SaleId { get; set; }

        public virtual ProductEntity Product { get; set; }
        public virtual SaleEntity Sale { get; set; }
    }
}
