using DAL.Data.Entities;
using DAL.Data;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Linq;

namespace DAL.Repository
{
    public class UserRepository : GenericRepository<UserEntity>,
        IUserRepository
    {
        public UserRepository(EFAppContext context) : base(context)
        {
        }

        public async Task AddProductToBasket(BasketEntity entity)
        {
            await _dbContext.Set<BasketEntity>().AddAsync(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task AddUserRole(UserRoleEntity roleEntity)
        {
            await _dbContext.Set<UserRoleEntity>().AddAsync(roleEntity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteProductBasket(BasketEntity entity)
        {
            _dbContext.Set<BasketEntity>().Remove(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task EditUserRole(UserRoleEntity roleEntity, UserRoleEntity entity)
        {
            var local = _dbContext.Set<UserRoleEntity>()
                                 .Local
                                 .FirstOrDefault(entry => entry.RoleId.Equals(roleEntity.RoleId));

            if (local != null)
            {
                _dbContext.Entry(local).State = EntityState.Detached;
            }

            _dbContext.Set<UserRoleEntity>().Remove(roleEntity);
            await _dbContext.Set<UserRoleEntity>().AddAsync(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task EditProductBasket(BasketEntity entity)
        {
            _dbContext.Set<BasketEntity>().Update(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<UserEntity> FindByEmailOrPhone(string findBy)
        {
            var user = await _dbContext.Set<UserEntity>()
                                .AsNoTracking()
                                .Include(x => x.Baskets)
                                    .ThenInclude(x => x.Product)
                                        .ThenInclude(x => x.Category)
                                .Include(x => x.Baskets)
                                    .ThenInclude(x => x.Product)
                                        .ThenInclude(x => x.Sales_Products)
                                            .ThenInclude(x => x.Sale)
                                .Include(x => x.Baskets)
                                    .ThenInclude(x => x.Product)
                                        .ThenInclude(x => x.Images)
                                .Include(x => x.UserRoles)
                                    .ThenInclude(x => x.Role)
                                .Include(x => x.Orders)
                                    .ThenInclude(x => x.OrderStatus)
                                .Include(x => x.Orders)
                                    .ThenInclude(x => x.OrderItems)
                                        .ThenInclude(x=>x.Product)
                                            .ThenInclude(x=>x.Images)
                                .FirstOrDefaultAsync(e => e.Email.ToLower() == findBy.ToLower() || e.Phone == findBy);
            return user;
        }

        public IEnumerable<UserEntity> GetAllUsers()
        {
            return GetAll().Include(x => x.Baskets)
                                .ThenInclude(x => x.Product)
                                    .ThenInclude(x => x.Category)
                           .Include(x => x.Baskets)
                                .ThenInclude(x => x.Product)
                                    .ThenInclude(x => x.Images)
                           .Include(x => x.UserRoles)
                                .ThenInclude(x => x.Role)
                           .Include(x => x.Orders)
                                .ThenInclude(x => x.OrderStatus)
                           .Include(x => x.Orders)
                                .ThenInclude(x => x.OrderItems)
                                    .ThenInclude(x => x.Product)
                                        .ThenInclude(x => x.Images);
        }

        public IEnumerable<RoleEntity> GetAllRoles()
        {
            return _dbContext.Set<RoleEntity>()
                             .AsNoTracking();
        }
    }
}
