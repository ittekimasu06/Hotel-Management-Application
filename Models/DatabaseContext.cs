using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace QuanLyKhachSan.Models
{
    class DatabaseContext : DbContext
    {
        public DatabaseContext() : base("QuanLyKhachSan") { }

        public DbSet<Objects.User> Users { get; set; }
        public DbSet<Objects.Room> Rooms { get; set; }
        public DbSet<Objects.Bill> Bills { get; set; }
    }
}
