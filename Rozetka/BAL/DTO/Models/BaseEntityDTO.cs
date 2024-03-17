using System;
using System.Collections.Generic;
using System.Text;

namespace BAL.DTO.Models
{
    public interface IEntity<T>
    {
        T Id { get; set; }
        bool IsDelete { get; set; }
        DateTime DateCreated { get; set; }
    }
    public abstract class BaseEntityDTO<T> : IEntity<T>
    {
        public T Id { get; set; }
        public bool IsDelete { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
