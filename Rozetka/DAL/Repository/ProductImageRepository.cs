using DAL.Data.Entities;
using DAL.Data;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Repository
{
    public class ProductImageRepository : GenericRepository<ProductImageEntity>,
        IProductImageRepository
    {
        public ProductImageRepository(EFAppContext context) : base(context)
        {
        }
    }
}
