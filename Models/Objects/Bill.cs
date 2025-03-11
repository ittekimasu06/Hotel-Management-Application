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

        //start date

        //end date

        //number adult

        //number children

        //number room

        //total 

    }
}
