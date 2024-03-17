using BAL.DTO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Interfaces
{
    public interface IOrderService
    {
        Task CreateOrder(OrderEntityDTO entity);
        Task EditOrder(OrderEntityDTO entity);
        Task<ICollection<OrderEntityDTO>> GetOrdersBy(Func<OrderEntityDTO, bool> predicate, int skip, int take);
        Task<ICollection<OrderStatusEntityDTO>> GetOrderStatuses();
        Task CreateOrderItemRange(ICollection<OrderItemEntityDTO> entitys);
    }
}
