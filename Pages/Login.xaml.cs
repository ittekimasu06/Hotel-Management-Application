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
using System.Windows.Navigation;
using System.Windows.Shapes;
using QuanLyKhachSan.Models;



namespace QuanLyKhachSan.Pages
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {

        private readonly DatabaseContext _context = new DatabaseContext();
        public Login()
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

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            string username = txtUsername.Text;
            string password = txtPassword.Password;
            bool hasError = false;

            var user = _context.Users.FirstOrDefault(u => u.Username == username);
            if (user != null && BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
            {
                var mainWindow = new MainWindow(user.Username, user.Email);
                mainWindow.Show();

                // Tạo Notification và gán Owner là mainWindow
                Notification notification = new Notification("Thành công", "Đăng nhập thành công");
                notification.Owner = mainWindow;
                notification.Show();

                this.Close();
            }
            else
            {
               if(string.IsNullOrWhiteSpace(username))
                {
                    txtUsernameError.Visibility = Visibility.Visible;
                    hasError = true;
                }
                else
                {
                    txtUsernameError.Visibility = Visibility.Collapsed;
                }
                if (string.IsNullOrWhiteSpace(password))
                {
                    txtPasswordError.Visibility = Visibility.Visible;
                    hasError = true;
                }
                else
                {
                    txtPasswordError.Visibility = Visibility.Collapsed;
                }
                if (!hasError)
                {
                    txtUsernameError.Text = "Tài khoản hoặc mật khẩu không đúng!";
                    txtPasswordError.Text = "Tài khoản hoặc mật khẩu không đúng!";
                    txtUsernameError.Visibility = Visibility.Visible;
                    txtPasswordError.Visibility = Visibility.Visible;
                    hasError = true;
                }
                if (hasError)
                {
                    return;
                }

            }
        }

        private void Hyperlink_RequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            if (e.Uri.OriginalString == "Register")
            {
                Register registerPage = new Register();
                registerPage.Show();
                this.Close();
            }
            e.Handled = true;
        }
    }
}
