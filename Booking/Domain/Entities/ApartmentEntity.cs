using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    [Table("tblApartments")]
    public class ApartmentEntity
    {
        [Key]
        public int Id { get; set; }

        //Номер у готелі кімнати
        [Required, StringLength(20)]
        public string Number { get; set; }

        //Кількість кімнат в номері
        public int NumberOfRooms { get; set; }

        //Кількість ліжок
        public int NumberOfBeds { get; set; }

        //Ціна за добу
        public decimal PricePerNight { get; set; }

        [ForeignKey("Floor")]
        public int FloorId { get; set; }

        public virtual FloorEntity Floor { get; set; }

        public ICollection<ApartmentImageEntity> ApartmentImages { get; set; }
    }
}
