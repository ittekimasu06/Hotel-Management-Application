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
            if (!DateTime.TryParse(dateNgayNhan.Text, out DateTime startDate) ||
                !DateTime.TryParse(dateNgayTra.Text, out DateTime endDate) || endDate < startDate)
            {
                MessageBox.Show("Ngày trả phải sau ngày nhận.");
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
        }
    }
}
