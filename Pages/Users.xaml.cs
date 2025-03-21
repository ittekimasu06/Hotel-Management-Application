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
            if(txtUsername == null || txtFullName == null || txtEmail == null || txtPhone == null || txtPassword == null || comboboxRole.SelectedItem == null)
            {
                MessageBox.Show("Hãy điền đầy đủ thông tin.");
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
                _context.SaveChanges();
                UsersDataGrid.Items.Refresh();
                ClearForm();
            }
        }

        private void BtnDelUser_Click(object sender, RoutedEventArgs e)
        {
            if (UsersDataGrid.SelectedItem is User selectedUser)
            {
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
        }

    }
}
