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
    /// Interaction logic for Bills.xaml
    /// </summary>
    public partial class Bills : Page
    {
        private readonly DatabaseContext _context = new DatabaseContext();
        private ObservableCollection<Bill> bills;

        public Bills()
        {
            InitializeComponent();
            LoadBills();
        }

        private void LoadBills()
        {
            bills = new ObservableCollection<Bill>(_context.Bills.ToList());
            BillsDataGrid.ItemsSource = bills;
        }

        private void BillsDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (BillsDataGrid.SelectedItem is Bill selectedBill)
            {
                txtEmail.Text = selectedBill.email;
                txtPhone.Text = selectedBill.phone;
                txtNgayNhan.Text = selectedBill.startDate.ToString("dd/MM/yyyy");
                txtNgayTra.Text = selectedBill.endDate.ToString("dd/MM/yyyy");
                txtSoNguoiLon.Text = selectedBill.numberAdult.ToString();
                txtSoTreEm.Text = selectedBill.numberChildren.ToString();
                txtTongTien.Text = selectedBill.total.ToString() + " VND";
                comboboxTrangThai.SelectedIndex = selectedBill.status;

            }
        }

        private void UpdateStatusButton_Click(object sender, RoutedEventArgs e)
        {
            if (BillsDataGrid.SelectedItem is Bill selectedBill)
            {
                selectedBill.status = int.Parse(((ComboBoxItem)comboboxTrangThai.SelectedItem).Tag.ToString());
                _context.SaveChanges();
                LoadBills();
                Notification notification = new Notification("Thành công", "Cập nhật hóa đơn thành công");
                notification.Owner = Window.GetWindow(this);
                notification.Show();
            }
        }

        private void BtnDelBill_Click(object sender, RoutedEventArgs e)
        {
            if (BillsDataGrid.SelectedItem is Bill selectedBill)
            {
                _context.Bills.Remove(selectedBill);
                _context.SaveChanges();
                LoadBills();
                Notification notification = new Notification("Thành công", "Xóa hóa đơn thành công");
                notification.Owner = Window.GetWindow(this);
                notification.Show();
            }
        }

        private void BtnRefresh_Click(object sender, RoutedEventArgs e)
        {
            comboboxTrangThai.SelectedIndex = -1;
            txtEmail.Text = "";
            txtPhone.Text = "";
            txtNgayNhan.Text = "";
            txtNgayTra.Text = "";
            txtSoNguoiLon.Text = "";
            txtSoTreEm.Text = "";
            txtTongTien.Text = "";
        }
    }
}
