using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DAL.Data.Entities
{
    [Table("tblRoles")]
    public class RoleEntity : BaseEntity<int>
    {
        [Required, StringLength(255)]
        public string Name { get; set; }
        public virtual ICollection<UserRoleEntity> UserRoles { get; set; }
    }
}
