using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using QuanLyKhachSan.Models;
using QuanLyKhachSan.Models.Objects;

namespace QuanLyKhachSan.Pages
{
    /// <summary>
    /// Interaction logic for Register.xaml
    /// </summary>
    public partial class Register : Window
    {
        private readonly DatabaseContext _context = new DatabaseContext();
        public Register()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnRestore_Click(object sender, RoutedEventArgs e)
        {
            if (WindowState == WindowState.Normal)
                WindowState = WindowState.Maximized;
            else
                WindowState = WindowState.Normal;
        }

        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            string fullname = txtFullName.Text;
            string username = txtUsername.Text;
            string email = txtEmail.Text;
            string password = txtPassword.Password;

            if (string.IsNullOrWhiteSpace(fullname) || (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password)))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!");
                return;
            }

            if (_context.Users.Any(u => u.Username == username))
            {
                MessageBox.Show("Tài khoản đã tồn tại!");
                return;
            }

            string passwordHash = BCrypt.Net.BCrypt.HashPassword(password);
            User newUser = new User { FullName = fullname, Username = username, Email = email, PasswordHash = passwordHash , Phone = string.Empty, Role = 0 };

            _context.Users.Add(newUser);
            _context.SaveChanges();

            MessageBox.Show("Đăng ký thành công!");

            Login loginPage = new Login();
            loginPage.Show();
            this.Close();
        }
    }
}
