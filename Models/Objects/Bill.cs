using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuanLyKhachSan.Models.Objects;

namespace QuanLyKhachSan.Models.Objects
{
    
    public class Bill 
    {
        [Key]
        public int billId { get; set; }

        [Required]
        public int roomId { get; set; }

        [ForeignKey("roomId")]
        public Room Room { get; set; }

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

        //total = price * (end - start)
        [Required]
        public float total { get; set; }

        //status: 0 - Unpaid, 1 - Paid, 2 - Cancel, 3 - Checkin, 4 - Checkout
        [Required]
        public int status { get; set; }


        private Room GetRoomById(int roomId)
        {
            using (var context = new DatabaseContext())
            {
                return context.Rooms.SingleOrDefault(r => r.roomId == roomId);
            }
        }

        public float Total
        {
            get
            {
                Room room = GetRoomById(roomId);
                return (endDate - startDate).Days * room.price;
            }
        }

        public string StatusName
        {
            get
            {
                return status switch
                {
                    0 => "Chưa thanh toán",
                    1 => "Đã thanh toán",
                    2 => "Hủy",
                    3 => "Checkin",
                    4 => "Checkout",
                    _ => "Không xác định"
                };
            }
        }

    }
}
