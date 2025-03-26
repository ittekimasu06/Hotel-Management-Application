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
            if (!ValidateUserForm())
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

            _context.Users.Add(user);
            _context.SaveChanges();
            users.Add(user);
            Notification notification = new Notification("Thành công", "Tạo tài khoản thành công");
            notification.Owner = Window.GetWindow(this);
            notification.Show();
            ClearForm();
        }

        private void BtnUpdateUser_Click(object sender, RoutedEventArgs e)
        {
            if (UsersDataGrid.SelectedItem is User selectedUser)
            {
                if (!ValidateUserForm())
                {
                    return;
                }

                selectedUser.Username = txtUsername.Text;
                selectedUser.FullName = txtFullName.Text;
                selectedUser.Email = txtEmail.Text;
                selectedUser.Phone = txtPhone.Text;

                // Cập nhật mật khẩu nếu thay đổi
                if (txtPassword.Password != "current password")
                {
                    selectedUser.PasswordHash = BCrypt.Net.BCrypt.HashPassword(txtPassword.Password);
                }

                selectedUser.Role = int.Parse(((ComboBoxItem)comboboxRole.SelectedItem).Tag.ToString());
                _context.SaveChanges();
                Notification notification = new Notification("Thành công", "Cập nhật tài khoản thành công");
                notification.Owner = Window.GetWindow(this);
                notification.Show();
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

        private bool ValidateUserForm()
        {
            bool hasError = false;

            // Username
            if (string.IsNullOrWhiteSpace(txtUsername.Text))
            {
                txtUsernameError.Text = "Vui lòng nhập tên đăng nhập";
                txtUsernameError.Visibility = Visibility.Visible;
                hasError = true;
            }
            else if (txtUsername.Text.Length >= 50)
            {
                txtUsernameError.Text = "Tên đăng nhập quá 50 ký tự";
                txtUsernameError.Visibility = Visibility.Visible;
                hasError = true;
            }
            else
            {
                txtUsernameError.Visibility = Visibility.Collapsed;
            }

            // Full Name
            if (string.IsNullOrWhiteSpace(txtFullName.Text))
            {
                txtFullNameError.Text = "Vui lòng nhập họ tên";
                txtFullNameError.Visibility = Visibility.Visible;
                hasError = true;
            }
            else if (txtFullName.Text.Length >= 50)
            {
                txtFullNameError.Text = "Họ tên quá 50 ký tự";
                txtFullNameError.Visibility = Visibility.Visible;
                hasError = true;
            }
            else
            {
                txtFullNameError.Visibility = Visibility.Collapsed;
            }

            // Email
            if (string.IsNullOrWhiteSpace(txtEmail.Text))
            {
                txtEmailError.Text = "Vui lòng nhập email";
                txtEmailError.Visibility = Visibility.Visible;
                hasError = true;
            }
            else if (txtEmail.Text.Length >= 100)
            {
                txtEmailError.Text = "Email quá 100 ký tự!";
                txtEmailError.Visibility = Visibility.Visible;
                hasError = true;
            }
            else if (!Regex.IsMatch(txtEmail.Text, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                txtEmailError.Text = "Email không hợp lệ!";
                txtEmailError.Visibility = Visibility.Visible;
                hasError = true;
            }
            else
            {
                txtEmailError.Visibility = Visibility.Collapsed;
            }

            // Phone
            if (string.IsNullOrWhiteSpace(txtPhone.Text))
            {
                txtPhoneError.Text = "Vui lòng nhập số điện thoại";
                txtPhoneError.Visibility = Visibility.Visible;
                hasError = true;
            }
            else if (txtPhone.Text.Length >= 12)
            {
                txtPhoneError.Text = "Số điện thoại quá 12 ký tự!";
                txtPhoneError.Visibility = Visibility.Visible;
                hasError = true;
            }
            else if (!Regex.IsMatch(txtPhone.Text, @"^0\d{9,10}$"))
            {
                txtPhoneError.Text = "Số điện thoại không hợp lệ!";
                txtPhoneError.Visibility = Visibility.Visible;
                hasError = true;
            }
            else
            {
                txtPhoneError.Visibility = Visibility.Collapsed;
            }

            // Password (Chỉ kiểm tra trong BtnAddUser hoặc khi có thay đổi)
            if (string.IsNullOrWhiteSpace(txtPassword.Password))
            {
                txtPasswordError.Text = "Vui lòng nhập mật khẩu";
                txtPasswordError.Visibility = Visibility.Visible;
                hasError = true;
            }
            else if (txtPassword.Password.Length < 6)
            {
                txtPasswordError.Text = "Mật khẩu phải có ít nhất 6 ký tự!";
                txtPasswordError.Visibility = Visibility.Visible;
                hasError = true;
            }
            else
            {
                txtPasswordError.Visibility = Visibility.Collapsed;
            }

            // Role
            if (comboboxRole.SelectedItem == null)
            {
                txtRoleError.Text = "Vui lòng chọn vai trò";
                txtRoleError.Visibility = Visibility.Visible;
                hasError = true;
            }
            else
            {
                txtRoleError.Visibility = Visibility.Collapsed;
            }

            return !hasError;
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
