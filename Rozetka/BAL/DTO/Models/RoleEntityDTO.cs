using DAL.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BAL.DTO.Models
{
    public class RoleEntityDTO : BaseEntityDTO<int>
    {
        public string Name { get; set; }
        public virtual ICollection<UserRoleEntityDTO> UserRoles { get; set; }
    }
}
