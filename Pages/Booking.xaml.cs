using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using QuanLyKhachSan.Models;
using QuanLyKhachSan.Models.Objects;
using System.Text.RegularExpressions;

namespace QuanLyKhachSan.Pages
{
    /// <summary>
    /// Interaction logic for Booking.xaml
    /// </summary>
    public partial class Booking : Page
    {
        private readonly DatabaseContext _context = new DatabaseContext();
        private int selectedRoomId;

        public Booking()
        {
            InitializeComponent();
        }

        private void BtnChonPhong_Click(object sender, RoutedEventArgs e)
        {
            RoomPick roomPick = new RoomPick();
            if (roomPick.ShowDialog() == true)
            {
                selectedRoomId = roomPick.SelectedRoomId;
                txtPhong.Text = "Phòng số " + selectedRoomId;
            }
        }

        private void BtnDatPhong_Click(object sender, RoutedEventArgs e)
        {
            bool hasError = false;
            if (string.IsNullOrWhiteSpace(txtHoVaTen.Text))
            {
                txtHoVaTenError.Visibility = Visibility.Visible;
                hasError = true;
            }
            else if (txtHoVaTen.Text.Length >= 50)
            {
                txtHoVaTenError.Visibility = Visibility.Visible;
                txtHoVaTenError.Text = "Họ và tên quá 50 ký tự";
                hasError = true;
            }
            else
            {
                txtHoVaTenError.Visibility = Visibility.Collapsed;
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
            else if (!Regex.IsMatch(txtEmail.Text, @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$"))
            {
                txtEmailError.Visibility = Visibility.Visible;
                txtEmailError.Text = "Email không hợp lệ";
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

            if(txtPhong.Text == "Chưa chọn phòng")
            {
                txtPhongError.Visibility = Visibility.Visible;
                hasError = true;
            }
            else
            {
                txtPhongError.Visibility = Visibility.Collapsed;
            }

            if (string.IsNullOrWhiteSpace(txtSoNguoiLon.Text))
            {
                txtSoNguoiLonError.Visibility = Visibility.Visible;
                hasError = true;
            }
            else if (!int.TryParse(txtSoNguoiLon.Text, out int numberAdult) || numberAdult < 1)
            {
                txtSoNguoiLonError.Visibility = Visibility.Visible;
                txtSoNguoiLonError.Text = "Số người lớn phải là số nguyên dương";
                hasError = true;
            }
            else
            {
                txtSoNguoiLonError.Visibility = Visibility.Collapsed;
            }

            if (string.IsNullOrWhiteSpace(txtSoTreEm.Text))
            {
                txtSoTreEmError.Visibility = Visibility.Visible;
                hasError = true;
            }
            else if (!int.TryParse(txtSoTreEm.Text, out int numberChildren) || numberChildren < 0)
            {
                txtSoTreEmError.Visibility = Visibility.Visible;
                txtSoTreEmError.Text = "Số trẻ em phải là số nguyên dương";
                hasError = true;
            }
            else
            {
                txtSoTreEmError.Visibility = Visibility.Collapsed;
            }

            if (dateNgayNhan.SelectedDate == null)
            {
                dateNgayNhanError.Visibility = Visibility.Visible;
                hasError = true;
            }
            else
            {
                dateNgayNhanError.Visibility = Visibility.Collapsed;
            }

            if (dateNgayTra.SelectedDate == null)
            {
                dateNgayTraError.Visibility = Visibility.Visible;
                hasError = true;
            }
            else
            {
                dateNgayTraError.Visibility = Visibility.Collapsed;
            }

            if (hasError)
            {
                return;
            }



            if (!DateTime.TryParse(dateNgayNhan.Text, out DateTime startDate) ||
                !DateTime.TryParse(dateNgayTra.Text, out DateTime endDate) || endDate < startDate)
            {
                NotificationError notification = new NotificationError("Thất bại", "Ngày nhận phải trước ngày trả");
                notification.Owner = Window.GetWindow(this);
                notification.Show();
                return;
            }
            else if(startDate < DateTime.Now)
            {
                NotificationError notification1 = new NotificationError("Thất bại", "Ngày nhận không được trước hôm nay");
                notification1.Owner = Window.GetWindow(this);
                notification1.Show();
                return;
            }

            else if (_context.Bills.Any(bill => bill.roomId == selectedRoomId && startDate < bill.endDate && endDate > bill.startDate))
            {
                NotificationError notification3 = new NotificationError("Thất bại", "Phòng đã được đặt trong thời gian này");
                notification3.Owner = Window.GetWindow(this);
                notification3.Show();
                return;
            }

            else if (_context.Rooms.Find(selectedRoomId).numberPeople < int.Parse(txtSoNguoiLon.Text) + int.Parse(txtSoTreEm.Text))
            {
                NotificationError notification4 = new NotificationError("Thất bại", "Số người quá số lượng phòng cho phép");
                notification4.Owner = Window.GetWindow(this);
                notification4.Show();
                return;
            }

            var bill = new Bill
                {
                    roomId = selectedRoomId,
                    createdAt = DateTime.Now,
                    fullName = txtHoVaTen.Text,
                    email = txtEmail.Text,
                    phone = txtPhone.Text,
                    startDate = startDate,
                    endDate = endDate,
                    numberAdult = int.Parse(txtSoNguoiLon.Text),
                    numberChildren = int.Parse(txtSoTreEm.Text),
                    status = 0
                };
            bill.total = bill.Total;

            _context.Bills.Add(bill);
            _context.SaveChanges();
            Notification notification2 = new Notification("Thành công", "Đặt phòng thành công");
            notification2.Owner = Window.GetWindow(this);
            notification2.Show();
        }

        private void BtnRefresh_Click(object sender, RoutedEventArgs e)
        {
            txtHoVaTen.Clear();
            txtEmail.Clear();
            txtPhone.Clear();
            txtSoNguoiLon.Clear();
            txtSoTreEm.Clear();
            dateNgayNhan.SelectedDate = null;
            dateNgayTra.SelectedDate = null;
            txtPhong.Text = "Chưa chọn phòng";

        }
    }
}
