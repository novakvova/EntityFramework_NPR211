using DAL.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BAL.DTO.Models
{
    public class SaleEntityDTO : BaseEntityDTO<int>
    {
        public string SaleName { get; set; }//Назва акції
        public string ImagePath { get; set; }//Шлях до зображення банера для акції 
        public string SaleDescription { get; set; }//Опис знижки
        public int DecreasePercent { get; set; }//Процент пониження ціни товару під час акції
        public DateTime ExpireTime { get; set; }//Час коли акція закінчиться
        public virtual ICollection<Sales_ProductEntityDTO> Sales_Products { get; set; }
    }
}
