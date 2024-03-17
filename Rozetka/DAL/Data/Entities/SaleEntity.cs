using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DAL.Data.Entities
{
    [Table("tblSales")]

    public class SaleEntity : BaseEntity<int>
    {
        [Required, StringLength(255)]
        public string SaleName { get; set; }//Назва акції
        [Required, StringLength(255)]
        public string ImagePath { get; set; }//Шлях до зображення банера для акції 
        [StringLength(4000)]
        public string SaleDescription { get; set; }//Опис знижки
        public int DecreasePercent { get; set; }//Процент пониження ціни товару під час акції
        public DateTime ExpireTime { get; set; }//Час коли акція закінчиться
        public virtual ICollection<Sales_ProductEntity> Sales_Products { get; set; }
    }
}
