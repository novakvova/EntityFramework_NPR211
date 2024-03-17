using DAL.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IOrderRepository : IGenericRepository<OrderEntity, int>
    {
        IQueryable<OrderEntity> Orders { get; }
        IQueryable<OrderStatusEntity> OrderStatuses { get; }
        Task CreateOrderItem(OrderItemEntity entity);
    }
}
