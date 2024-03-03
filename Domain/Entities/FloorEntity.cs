using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    [Table("tblFloors")]
    public class FloorEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [ForeignKey("Hotel")]
        public int HotelId { get; set; }
        public virtual HotelEntity Hotel { get; set; }

        public virtual ICollection<ApartmentEntity> Apartments { get; set; }
    }
}
