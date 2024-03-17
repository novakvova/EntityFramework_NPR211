using DAL.Data.Entities;
using DAL.Data;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace DAL.Repository
{
    public class SaleRepository : GenericRepository<SaleEntity>
    {
        public SaleRepository(EFAppContext context) : base(context) { }

        public IEnumerable<SaleEntity> GetAllSales()
        {
            return GetAll().Include(x => x.Sales_Products)
                               .ThenInclude(x => x.Product)
                                   .ThenInclude(x => x.Images)
                           .Include(x => x.Sales_Products)
                               .ThenInclude(x => x.Product)
                                   .ThenInclude(x => x.OrderItems)
                           .Include(x => x.Sales_Products)
                               .ThenInclude(x => x.Product)
                                    .ThenInclude(x => x.Baskets)
                           .Include(x => x.Sales_Products)
                               .ThenInclude(x => x.Product)
                                    .ThenInclude(x => x.Category)
                           .ToList();
        }

        public async Task AddSalesProduct(Sales_ProductEntity entity)
        {
            await _dbContext.Set<Sales_ProductEntity>().AddAsync(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteSalesProduct(Sales_ProductEntity entity)
        {
            _dbContext.Set<Sales_ProductEntity>().Remove(entity);
            await _dbContext.SaveChangesAsync();
        }
    }
}
