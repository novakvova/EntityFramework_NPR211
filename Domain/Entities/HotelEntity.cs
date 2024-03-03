using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    [Table("tblHotel")]
    public class HotelEntity
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(250)]
        public string Name { get; set; }
        [StringLength(4000)]
        public string Description { get; set; }
        [Required, StringLength(500)]
        public string Address { get; set; }

        public virtual ICollection<FloorEntity> Floors { get; set; }
    }
}
