using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    [Table("tblApartmentImages")]
    public class ApartmentImageEntity
    {
        [Key]
        public int Id { get; set; }

        [Required, StringLength(200)]
        public string Name { get; set; }

        //номер в послідовності
        public short Priority { get; set; }
        
        [ForeignKey("Apartment")]
        public int ApartmentId { get; set; }
        public virtual ApartmentEntity Apartment { get; set; }
    }
}
