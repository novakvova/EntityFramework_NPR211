using DAL.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BAL.DTO.Models
{
    public class UserEntityDTO : BaseEntityDTO<int>
    {
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public virtual ICollection<BasketEntityDTO> Baskets { get; set; }
        public virtual ICollection<OrderEntityDTO> Orders { get; set; }
        public virtual ICollection<UserRoleEntityDTO> UserRoles { get; set; }
    }
}
