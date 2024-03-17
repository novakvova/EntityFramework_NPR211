using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    [Table("tblCategories")]
    public class CategoryEntity
    {
        [Key]
        public int Id { get; set; }
        [Required, StringLength(200)]
        public string Name { get; set; }
        [StringLength(200)]
        public string Image { get; set; }
        [StringLength(4000)]
        public string Description { get; set; }

        //Номер в послідовності
        public short Priority { get; set; }

        [ForeignKey("Parent")]
        public int ? ParentId { get; set; }
        public virtual CategoryEntity Parent { get; set; }
        public virtual ICollection<CategoryEntity> Children { get; set; }
    }
}
