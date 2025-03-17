using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyKhachSan.Models.Objects
{
    public class Bill
    {
        [Key]
        public int billId { get; set; }

        [Required]
        public int roomId { get; set; }

        [Required]
        public int userId { get; set; }

        [Required]
        public DateTime createdAt { get; set; }

        [Required]
        [MaxLength(50)]
        public string fullName { get; set; }

        [Required]
        [MaxLength(100)]
        public string email { get; set; }

        //phone
        [Required]
        [MaxLength(12)]
        public string phone { get; set; }

        //start date
        [Required]
        public DateTime startDate { get; set; }

        //end date
        [Required]
        public DateTime endDate { get; set; }

        //number adult
        [Required]
        public int numberAdult { get; set; }

        //number children
        [Required]
        public int numberChildren { get; set; }

        //total 
        [Required]
        public float total { get; set; }

    }
}
