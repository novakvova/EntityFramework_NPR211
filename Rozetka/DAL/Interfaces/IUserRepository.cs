using DAL.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IUserRepository : IGenericRepository<UserEntity, int>
    {
        Task<UserEntity> FindByEmailOrPhone(string findBy);
        IEnumerable<RoleEntity> GetAllRoles();
        IEnumerable<UserEntity> GetAllUsers();
        Task AddUserRole(UserRoleEntity roleEntity);
        Task EditUserRole(UserRoleEntity roleEntity, UserRoleEntity entity);
        Task AddProductToBasket(BasketEntity entity);
        Task EditProductBasket(BasketEntity entity);
        Task DeleteProductBasket(BasketEntity entity);
    }
}
