using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyKhachSan.Models.Objects
{
    public class User
    {
        [Key]
        public int ID { get; set; }

        [Required]
        [MaxLength(50)]
        public string Username { get; set; }

        [Required]
        public string PasswordHash { get; set; }

        [Required]
        [MaxLength(50)]
        public string FullName { get; set; }

        [Required]
        [MaxLength(100)]
        public string Email { get; set; }

        [MaxLength(12)]
        public string Phone { get; set; }

        //Role: 0 - Admin,  1 - Staff, 2 - Customer
        public int Role { get; set; }

        public string RoleName
        {
            get
            {
                return Role switch
                {
                    0 => "Quản lý",
                    1 => "Nhân viên",
                    _ => "Không xác định"
                };
            }
        }
    }
}
