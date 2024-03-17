using DAL.Data.Entities;
using DAL.Data;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repository
{
    public class ProductRepository : GenericRepository<ProductEntity>,
        IProductRepository
    {
        public ProductRepository(EFAppContext context) : base(context)
        {
        }

        public ICollection<ProductEntity> GetProducts()
        {
            return GetAll()
                        .Include(x => x.Images)
                        .Include(x => x.Category)
                        .Include(x => x.OrderItems)
                            .ThenInclude(x=>x.Order)
                        .Include(x => x.Baskets)
                            .ThenInclude(x => x.User)
                        .Include(x => x.Sales_Products)
                            .ThenInclude(x=>x.Sale)
                        .ToList();
        }

    }
}
