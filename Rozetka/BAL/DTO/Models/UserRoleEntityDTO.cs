using DAL.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BAL.DTO.Models
{
    public class UserRoleEntityDTO
    {
        public int UserId { get; set; }
        public int RoleId { get; set; }

        public virtual UserEntityDTO User { get; set; }
        public virtual RoleEntityDTO Role { get; set; }
    }
}
