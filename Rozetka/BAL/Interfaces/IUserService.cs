using BAL.DTO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Interfaces
{
    public interface IUserService
    {
        IEnumerable<UserEntityDTO> GetAllUsers();
        Task EditUserRole(UserRoleEntityDTO old, string entityDTO);
        Task Registrate(UserEntityDTO entity);
        Task AddProductToBasket(BasketEntityDTO entity);
        Task EditProductBasket(BasketEntityDTO entity);
        Task EditUserInformation(UserEntityDTO entity);
        Task DeleteProductBasket(BasketEntityDTO entity);
        Task<UserEntityDTO> Login(UserEntityDTO entity);
        Task<UserEntityDTO> FindUserByEmailOrPhone(string findBy);
    }
}
