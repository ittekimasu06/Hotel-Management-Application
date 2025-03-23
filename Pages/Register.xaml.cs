using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

            bool hasError = false;

            if (string.IsNullOrWhiteSpace(fullname))
            {
                txtFullNameError.Visibility = Visibility.Visible;
                hasError = true;
            }
            else if (fullname.Length >= 50)
            {
                txtFullNameError.Visibility = Visibility.Visible;
                txtFullNameError.Text = "Họ tên quá 50 ký tự!";
                hasError = true;
            }
            else
            {
                txtFullNameError.Visibility = Visibility.Collapsed;
            }

            if (string.IsNullOrWhiteSpace(username))
            {
                txtUsernameError.Visibility = Visibility.Visible;
                hasError = true;
            }
            else if (username.Length >= 50)
            {
                txtUsernameError.Visibility = Visibility.Visible;
                txtUsernameError.Text = "Tên đăng nhập quá 50 ký tự!";
                hasError = true;
            }
            else
            {
                txtUsernameError.Visibility = Visibility.Collapsed;
            }

            if (string.IsNullOrWhiteSpace(email))
            {
                txtEmailError.Visibility = Visibility.Visible;
                hasError = true;
            }
            else if(email.Length >= 100)
            {
                txtEmailError.Visibility = Visibility.Visible;
                txtEmailError.Text = "Email quá 100 ký tự!";
                hasError = true;
            }
            else if (!Regex.IsMatch(txtEmail.Text, @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$"))
            {
                txtEmailError.Visibility = Visibility.Visible;
                txtEmailError.Text = "Email không hợp lệ!";
                hasError = true;
            }
            else
            {
                txtEmailError.Visibility = Visibility.Collapsed;
            }

            if (string.IsNullOrWhiteSpace(password))
            {
                txtPasswordError.Visibility = Visibility.Visible;
                hasError = true;
            }
            else if(password.Length < 6)
            {
                txtPasswordError.Visibility = Visibility.Visible;
                txtPasswordError.Text = "Mật khẩu phải có ít nhất 6 ký tự!";
                hasError = true;
            }
            else
            {
                txtPasswordError.Visibility = Visibility.Collapsed;
            }

            if (hasError)
            {
                return;
            }

            if (_context.Users.Any(u => u.Username == username))
            {
                NotificationError notification = new NotificationError("Thất bại", "Tài khoản đã tồn tại");
                notification.Owner = this;
                notification.Show();
                return;
            }

            string passwordHash = BCrypt.Net.BCrypt.HashPassword(password);
            User newUser = new User { FullName = fullname, Username = username, Email = email, PasswordHash = passwordHash , Phone = string.Empty, Role = 0 };

            _context.Users.Add(newUser);
            _context.SaveChanges();

            Notification notification1 = new Notification("Thành công", "Đăng ký thành công");
            notification1.Owner = this;
            notification1.Show();

            Login loginPage = new Login();
            loginPage.Show();
            this.Close();
        }

        private void btnBackToLogin_Click(object sender, RoutedEventArgs e)
        {
            Login loginPage = new Login();
            loginPage.Show();
            this.Close();
        }
    }
}
