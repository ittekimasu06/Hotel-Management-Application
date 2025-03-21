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
using QuanLyKhachSan.Models.Objects;
using QuanLyKhachSan.Models;

namespace QuanLyKhachSan.Pages
{
    /// <summary>
    /// Interaction logic for Rooms.xaml
    /// </summary>
    public partial class Rooms : Page
    {
        private ObservableCollection<Room> rooms;
        private readonly DatabaseContext _context = new DatabaseContext();
        public Rooms()
        {
            InitializeComponent();
            LoadRooms();
        }

        private void LoadRooms()
        {
            rooms = new ObservableCollection<Room>(_context.Rooms.ToList());
            RoomsDataGrid.ItemsSource = rooms;
        }

        private void BtnAddRoom_Click(object sender, RoutedEventArgs e)
        {
            if (txtNumberPeople == null || txtNumberBed == null || comboboxQuality.SelectedItem == null ||
                comboboxBedType == null || txtPrice == null || comboboxRoomType.SelectedItem == null || txtRoomArea == null)
            {
                MessageBox.Show("Hãy điền đầy đủ thông tin.");
                return;
            }

            var room = new Room
            {
                numberPeople = int.Parse(txtNumberPeople.Text),
                numberBed = int.Parse(txtNumberBed.Text),
                quality = int.Parse(((ComboBoxItem)comboboxQuality.SelectedItem).Tag.ToString()),
                bedType = ((ComboBoxItem)comboboxBedType.SelectedItem).Tag.ToString(),
                price = float.Parse(txtPrice.Text),
                roomType = ((ComboBoxItem)comboboxRoomType.SelectedItem).Tag.ToString(),
                roomArea = float.Parse(txtRoomArea.Text)
            };
            _context.Rooms.Add(room);
            _context.SaveChanges();
            rooms.Add(room);
            ClearForm();
        }


        private void BtnUpdateRoom_Click(object sender, RoutedEventArgs e)
        {
            if (RoomsDataGrid.SelectedItem is Room selectedRoom)
            {
                selectedRoom.numberPeople = int.Parse(txtNumberPeople.Text);
                selectedRoom.numberBed = int.Parse(txtNumberBed.Text);
                selectedRoom.quality = int.Parse(((ComboBoxItem)comboboxQuality.SelectedItem).Tag.ToString());
                selectedRoom.bedType = ((ComboBoxItem)comboboxBedType.SelectedItem).Tag.ToString();
                selectedRoom.price = float.Parse(txtPrice.Text);
                selectedRoom.roomType = ((ComboBoxItem)comboboxRoomType.SelectedItem).Tag.ToString();
                selectedRoom.roomArea = float.Parse(txtRoomArea.Text);
                _context.SaveChanges();
                LoadRooms();
                ClearForm();
            }
        }

        private void BtnDelRoom_Click(object sender, RoutedEventArgs e)
        {
            if (RoomsDataGrid.SelectedItem is Room selectedRoom)
            {
                _context.Rooms.Remove(selectedRoom);
                _context.SaveChanges();
                rooms.Remove(selectedRoom);
                ClearForm();
            }
        }

        private void RoomsDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (RoomsDataGrid.SelectedItem is Room selectedRoom)
            {
                txtNumberPeople.Text = selectedRoom.numberPeople.ToString();
                txtNumberBed.Text = selectedRoom.numberBed.ToString();
                comboboxQuality.SelectedIndex = selectedRoom.quality - 3; // Adjust index based on your ComboBox items
                comboboxBedType.SelectedItem = comboboxBedType.Items.Cast<ComboBoxItem>()
                    .FirstOrDefault(item => item.Tag.ToString() == selectedRoom.bedType);
                txtPrice.Text = selectedRoom.price.ToString();
                comboboxRoomType.SelectedItem = comboboxRoomType.Items.Cast<ComboBoxItem>()
                    .FirstOrDefault(item => item.Tag.ToString() == selectedRoom.roomType);
                txtRoomArea.Text = selectedRoom.roomArea.ToString();
            }
        }


        private void BtnRefresh_Click(object sender, RoutedEventArgs e)
        {
            LoadRooms();
            ClearForm();
        }

        private void ClearForm()
        {
            txtNumberPeople.Clear();
            txtNumberBed.Clear();
            comboboxQuality.SelectedIndex = -1;
            comboboxBedType.SelectedIndex = -1;
            txtPrice.Clear();
            comboboxRoomType.SelectedIndex = -1;
            txtRoomArea.Clear();
        }
    }
}
