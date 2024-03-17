using DAL.Data;
using DAL.Data.Entities;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository
{
    public class OrderRepository : GenericRepository<OrderEntity>,
        IOrderRepository
    {
        public OrderRepository(EFAppContext context) : base(context)
        {
        }

        public IQueryable<OrderEntity> Orders => GetAll()
                                                    .Include(x=>x.OrderStatus)
                                                    .Include(x=>x.User)
                                                    .Include(x=>x.OrderItems)
                                                        .ThenInclude(x=>x.Product)
                                                            .ThenInclude(x=>x.Category)
                                                    .Include(x => x.OrderItems)
                                                        .ThenInclude(x => x.Product)
                                                            .ThenInclude(x => x.Images)
                                                    .Include(x => x.OrderItems)
                                                        .ThenInclude(x => x.Product)
                                                            .ThenInclude(x => x.Sales_Products)
                                                                .ThenInclude(x => x.Sale);

        public IQueryable<OrderStatusEntity> OrderStatuses => _dbContext.OrderStatuses.AsNoTracking();

        public async Task CreateOrderItem(OrderItemEntity entity)
        {
            await _dbContext.Set<OrderItemEntity>().AddAsync(entity);
            await _dbContext.SaveChangesAsync();
        }
    }
}
