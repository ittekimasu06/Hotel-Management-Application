using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyKhachSan.Models.Objects
{
    public class Room
    {
        //room id
        [Key]
        public int roomId { get; set; }


        //number people
        [Required]
        public int numberPeople { get; set; }

        //number bed
        [Required]
        public int numberBed { get; set; }

        //quality
        [Required]
        public int quality { get; set; }

        //bed type
        [Required]
        public string bedType { get; set; }

        //price
        [Required]
        public float price { get; set; }

        //room type
        [Required]
        public int roomTypeId { get; set; }

        //room area
        [Required]
        public float roomArea { get; set; }

    }
}
