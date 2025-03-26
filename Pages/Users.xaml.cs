using System;
using System.Collections.Generic;
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
using System.Collections.ObjectModel;
using QuanLyKhachSan.Models.Objects;
using QuanLyKhachSan.Models;
using System.Text.RegularExpressions;

namespace QuanLyKhachSan.Pages
{
    /// <summary>
    /// Interaction logic for Users.xaml
    /// </summary>
    public partial class Users : Page
    {
        private ObservableCollection<User> users;
        private readonly DatabaseContext _context = new DatabaseContext();

        public Users()
        {
            InitializeComponent();
            LoadUsers();
        }

        private void LoadUsers()
        {
            users = new ObservableCollection<User>(_context.Users.ToList());
            UsersDataGrid.ItemsSource = users;
        }

        private void BtnAddUser_Click(object sender, RoutedEventArgs e)
        {
            bool hasError = false;

            if (string.IsNullOrWhiteSpace(txtUsername.Text))
            {
                txtUsernameError.Visibility = Visibility.Visible;
                hasError = true;
            }
            else if (txtUsername.Text.Length >= 50)
            {
                txtUsernameError.Visibility = Visibility.Visible;
                txtUsernameError.Text = "Tên đăng nhập quá 50 ký tự";
                hasError = true;
            }
            else
            {
                txtUsernameError.Visibility = Visibility.Collapsed;
            }

            if (string.IsNullOrWhiteSpace(txtFullName.Text))
            {
                txtFullNameError.Visibility = Visibility.Visible;
                hasError = true;
            }
            else if (txtFullName.Text.Length >= 50)
            {
                txtFullNameError.Visibility = Visibility.Visible;
                txtFullNameError.Text = "Họ tên quá 50 ký tự";
                hasError = true;
            }
            else
            {
                txtFullNameError.Visibility = Visibility.Collapsed;
            }

            if (string.IsNullOrWhiteSpace(txtEmail.Text))
            {
                txtEmailError.Visibility = Visibility.Visible;
                hasError = true;
            }
            else if (txtEmail.Text.Length >= 100)
            {
                txtEmailError.Visibility = Visibility.Visible;
                txtEmailError.Text = "Email quá 100 ký tự!";
                hasError = true;
            }
            else if (!Regex.IsMatch(txtEmail.Text, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                txtEmailError.Visibility = Visibility.Visible;
                txtEmailError.Text = "Email không hợp lệ!";
                hasError = true;
            }
            else
            {
                txtEmailError.Visibility = Visibility.Collapsed;
            }

            if (string.IsNullOrWhiteSpace(txtPhone.Text))
            {
                txtPhoneError.Visibility = Visibility.Visible;
                hasError = true;
            }
            else if (txtPhone.Text.Length >= 12)
            {
                txtPhoneError.Visibility = Visibility.Visible;
                txtPhoneError.Text = "Số điện thoại quá 12 ký tự!";
                hasError = true;
            }
            else if (!Regex.IsMatch(txtPhone.Text, @"^0\d{9,10}$"))
            {
                txtPhoneError.Visibility = Visibility.Visible;
                txtPhoneError.Text = "Số điện thoại không hợp lệ!";
                hasError = true;
            }
            else
            {
                txtPhoneError.Visibility = Visibility.Collapsed;
            }

            if (string.IsNullOrWhiteSpace(txtPassword.Password))
            {
                txtPasswordError.Visibility = Visibility.Visible;
                hasError = true;
            }
            else if (txtPassword.Password.Length < 6)
            {
                txtPasswordError.Visibility = Visibility.Visible;
                txtPasswordError.Text = "Mật khẩu phải có ít nhất 6 ký tự!";
                hasError = true;
            }
            else
            {
                txtPasswordError.Visibility = Visibility.Collapsed;
            }

            if (comboboxRole.SelectedItem == null)
            {
                txtRoleError.Visibility = Visibility.Visible;
                hasError = true;
            }
            else
            {
                txtRoleError.Visibility = Visibility.Collapsed;
            }

            if (hasError)
            {
                return;
            }

            var user = new User
            {
                Username = txtUsername.Text,
                FullName = txtFullName.Text,
                Email = txtEmail.Text,
                Phone = txtPhone.Text,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(txtPassword.Password),
                Role = int.Parse(((ComboBoxItem)comboboxRole.SelectedItem).Tag.ToString())
            };
            Notification notification = new Notification("Thành công", "Tạo tài khoản thành công");
            notification.Owner = Window.GetWindow(this);
            notification.Show();
            _context.Users.Add(user);
            _context.SaveChanges();
            users.Add(user);
            ClearForm();
        }

        private void BtnUpdateUser_Click(object sender, RoutedEventArgs e)
        {
            if (UsersDataGrid.SelectedItem is User selectedUser)
            {
                selectedUser.Username = txtUsername.Text;
                selectedUser.FullName = txtFullName.Text;
                selectedUser.Email = txtEmail.Text;
                selectedUser.Phone = txtPhone.Text;
                if (txtPassword.Password != "current password")
                {
                    selectedUser.PasswordHash = BCrypt.Net.BCrypt.HashPassword(txtPassword.Password);
                }
                selectedUser.Role = int.Parse(((ComboBoxItem)comboboxRole.SelectedItem).Tag.ToString());
                Notification notification = new Notification("Thành công", "Cập nhật tài khoản thành công");
                notification.Owner = Window.GetWindow(this);
                notification.Show();
                _context.SaveChanges();
                UsersDataGrid.Items.Refresh();
                ClearForm();
            }
        }

        private void BtnDelUser_Click(object sender, RoutedEventArgs e)
        {
            if (UsersDataGrid.SelectedItem is User selectedUser)
            {
                Notification notification = new Notification("Thành công", "Xóa tài khoản thành công");
                notification.Owner = Window.GetWindow(this);
                notification.Show();
                _context.Users.Remove(selectedUser);
                _context.SaveChanges();
                users.Remove(selectedUser);
                ClearForm();
            }
        }

        private void UsersDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (UsersDataGrid.SelectedItem is User selectedUser)
            {
                txtUsernameError.Visibility = Visibility.Collapsed;
                txtFullNameError.Visibility = Visibility.Collapsed;
                txtEmailError.Visibility = Visibility.Collapsed;
                txtPhoneError.Visibility = Visibility.Collapsed;
                txtPasswordError.Visibility = Visibility.Collapsed;
                txtRoleError.Visibility = Visibility.Collapsed;

                txtUsername.Text = selectedUser.Username;
                txtFullName.Text = selectedUser.FullName;
                txtEmail.Text = selectedUser.Email;
                txtPassword.Password = "current password";
                txtPhone.Text = selectedUser.Phone;
                comboboxRole.SelectedIndex = selectedUser.Role;
            }
        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            LoadUsers();
            ClearForm();
        }
     

        private void ClearForm()
        {
            txtUsername.Clear();
            txtFullName.Clear();
            txtEmail.Clear();
            txtPhone.Clear();
            txtPassword.Clear();
            comboboxRole.SelectedIndex = -1;
            txtUsernameError.Visibility = Visibility.Collapsed;
            txtFullNameError.Visibility = Visibility.Collapsed;
            txtEmailError.Visibility = Visibility.Collapsed;
            txtPhoneError.Visibility = Visibility.Collapsed;
            txtPasswordError.Visibility = Visibility.Collapsed;
            txtRoleError.Visibility = Visibility.Collapsed;
            UsersDataGrid.SelectedItem = null;
        }   
    }
}
