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

namespace QuanLyKhachSan.Pages
{
    /// <summary>
    /// Interaction logic for Booking.xaml
    /// </summary>
    public partial class Booking : Page
    {
        private List<BookingInfo> bookings;

        public Booking()
        {
            InitializeComponent();
            LoadBookings();
        }

        private void LoadBookings()
        {
            // Load existing bookings from a data source (e.g., database)
            // For demonstration, we'll use a sample list
            bookings = new List<BookingInfo>
            {
                new BookingInfo { BookingId = "B001", CheckInDate = DateTime.Now, CheckOutDate = DateTime.Now.AddDays(1), RoomType = "Phòng đơn", GuestCount = 1, SpecialRequest = "None", RoomStatus = "Đã đặt" },
                new BookingInfo { BookingId = "B002", CheckInDate = DateTime.Now, CheckOutDate = DateTime.Now.AddDays(2), RoomType = "Phòng đôi", GuestCount = 2, SpecialRequest = "None", RoomStatus = "Trống" }
            };

            BookingDataGrid.ItemsSource = bookings;
        }

        private void AddBooking_Click(object sender, RoutedEventArgs e)
        {
            var newBooking = new BookingInfo
            {
                BookingId = BookingIdTextBox.Text,
                CheckInDate = CheckInDatePicker.SelectedDate ?? DateTime.Now,
                CheckOutDate = CheckOutDatePicker.SelectedDate ?? DateTime.Now.AddDays(1),
                RoomType = (RoomTypeComboBox.SelectedItem as ComboBoxItem)?.Content.ToString(),
                GuestCount = int.Parse(GuestCountTextBox.Text),
                SpecialRequest = SpecialRequestTextBox.Text,
                RoomStatus = "Đã đặt"
            };

            bookings.Add(newBooking);
            BookingDataGrid.ItemsSource = null;
            BookingDataGrid.ItemsSource = bookings;
        }

        private void UpdateBooking_Click(object sender, RoutedEventArgs e)
        {
            if (BookingDataGrid.SelectedItem is BookingInfo selectedBooking)
            {
                selectedBooking.BookingId = BookingIdTextBox.Text;
                selectedBooking.CheckInDate = CheckInDatePicker.SelectedDate ?? DateTime.Now;
                selectedBooking.CheckOutDate = CheckOutDatePicker.SelectedDate ?? DateTime.Now.AddDays(1);
                selectedBooking.RoomType = (RoomTypeComboBox.SelectedItem as ComboBoxItem)?.Content.ToString();
                selectedBooking.GuestCount = int.Parse(GuestCountTextBox.Text);
                selectedBooking.SpecialRequest = SpecialRequestTextBox.Text;

                BookingDataGrid.ItemsSource = null;
                BookingDataGrid.ItemsSource = bookings;
            }
        }

        private void DeleteBooking_Click(object sender, RoutedEventArgs e)
        {
            if (BookingDataGrid.SelectedItem is BookingInfo selectedBooking)
            {
                bookings.Remove(selectedBooking);
                BookingDataGrid.ItemsSource = null;
                BookingDataGrid.ItemsSource = bookings;
            }
        }

        private void BookingDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (BookingDataGrid.SelectedItem is BookingInfo selectedBooking)
            {
                BookingIdTextBox.Text = selectedBooking.BookingId;
                CheckInDatePicker.SelectedDate = selectedBooking.CheckInDate;
                CheckOutDatePicker.SelectedDate = selectedBooking.CheckOutDate;
                RoomTypeComboBox.SelectedItem = RoomTypeComboBox.Items.Cast<ComboBoxItem>().FirstOrDefault(item => item.Content.ToString() == selectedBooking.RoomType);
                GuestCountTextBox.Text = selectedBooking.GuestCount.ToString();
                SpecialRequestTextBox.Text = selectedBooking.SpecialRequest;
            }
        }
    }

    public class BookingInfo
    {
        public string BookingId { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public string RoomType { get; set; }
        public int GuestCount { get; set; }
        public string SpecialRequest { get; set; }
        public string RoomStatus { get; set; }
    }
}
