using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyKhachSan.Models.Objects
{
    public class RoomType
    {
        [Key]
        public int roomTypeId { get; set; }

        [Required]
        [MaxLength(50)]
        public string roomTypeName { get; set; }
    }
}
