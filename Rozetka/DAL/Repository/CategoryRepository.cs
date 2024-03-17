using DAL.Data.Entities;
using DAL.Data;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace DAL.Repository
{
    public class CategoryRepository : GenericRepository<CategoryEntity>,
        ICategoryRepository
    {
        public CategoryRepository(EFAppContext context) : base(context)
        {

        }

        public IEnumerable<CategoryEntity> GetAllCategories()
        {
            return GetAll().Include(x => x.Products)
                               .ThenInclude(x => x.Images)
                           .Include(x => x.Products)
                               .ThenInclude(x => x.Baskets)
                                   .ThenInclude(x => x.User)
                                       .ThenInclude(x => x.UserRoles)
                                           .ThenInclude(x => x.Role)
                           .Include(x => x.Products)
                               .ThenInclude(x => x.Sales_Products)
                                   .ThenInclude(x => x.Sale)
                           .Include(x => x.Products)
                               .ThenInclude(x => x.OrderItems)
                                    .ThenInclude(x => x.Order)
                                        .ThenInclude(x => x.OrderStatus);
        }
    }
}
