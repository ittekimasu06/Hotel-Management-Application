using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using QuanLyKhachSan.Pages;

namespace QuanLyKhachSan
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        public MainWindow(string username, string email)
        {
            InitializeComponent();
            SetUsername(username);
            SetEmail(email);
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

        private void rdHome_Click(object sender, RoutedEventArgs e)
        {
            placeHolder.Visibility = Visibility.Collapsed;
            PagesNavigation.Navigate(new System.Uri("Pages/Home.xaml", UriKind.RelativeOrAbsolute));
        }

        private void rdRooms_Click(object sender, RoutedEventArgs e)
        {
            placeHolder.Visibility = Visibility.Collapsed;
            PagesNavigation.Navigate(new System.Uri("Pages/Rooms.xaml", UriKind.RelativeOrAbsolute));
        }

        private void rdBills_Click(object sender, RoutedEventArgs e)
        {
            placeHolder.Visibility = Visibility.Collapsed;
            PagesNavigation.Navigate(new System.Uri("Pages/Bills.xaml", UriKind.RelativeOrAbsolute));
        }

        private void rdUsers_Click(object sender, RoutedEventArgs e)
        {
            placeHolder.Visibility = Visibility.Collapsed;
            PagesNavigation.Navigate(new System.Uri("Pages/Users.xaml", UriKind.RelativeOrAbsolute));
        }

        private void rdStats_Click(object sender, RoutedEventArgs e)
        {
            placeHolder.Visibility = Visibility.Collapsed;
            PagesNavigation.Navigate(new System.Uri("Pages/Stats.xaml", UriKind.RelativeOrAbsolute));
        }

        private void rdBooking_Click(object sender, RoutedEventArgs e)
        {
            placeHolder.Visibility = Visibility.Collapsed;
            PagesNavigation.Navigate(new System.Uri("Pages/Booking.xaml", UriKind.RelativeOrAbsolute));
        }

        //set txtUsername.Text = currently logged in user
        public void SetUsername(string username)
        {
            txtUsername.Text = username;
        }
        //set txtEmail.Text = currently logged in user
        public void SetEmail(string email)
        {
            txtEmail.Text = email;
        }

        private void rdLogout_Click(object sender, RoutedEventArgs e)
        {
            var login = new Login();
            login.Show();
            Close();
        }
    }
}