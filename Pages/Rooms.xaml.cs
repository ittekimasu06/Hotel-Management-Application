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
            bool hasError = false;

            if (string.IsNullOrWhiteSpace(txtNumberPeople.Text))
            {
                txtNumberPeopleError.Visibility = Visibility.Visible;
                hasError = true;
            }
            else
            {
                txtNumberPeopleError.Visibility = Visibility.Collapsed;
            }

            if (string.IsNullOrWhiteSpace(txtNumberBed.Text))
            {
                txtNumberBedError.Visibility = Visibility.Visible;
                hasError = true;
            }
            else
            {
                txtNumberBedError.Visibility = Visibility.Collapsed;
            }

            if (comboboxQuality.SelectedIndex == -1)
            {
                txtQualityError.Visibility = Visibility.Visible;
                hasError = true;
            }
            else
            {
                txtQualityError.Visibility = Visibility.Collapsed;
            }

            if (comboboxBedType.SelectedIndex == -1)
            {
                txtBedTypeError.Visibility = Visibility.Visible;
                hasError = true;
            }
            else
            {
                txtBedTypeError.Visibility = Visibility.Collapsed;
            }

            if (string.IsNullOrWhiteSpace(txtPrice.Text))
            {
                txtPriceError.Visibility = Visibility.Visible;
                hasError = true;
            }
            else
            {
                txtPriceError.Visibility = Visibility.Collapsed;
            }

            if (comboboxRoomType.SelectedIndex == -1)
            {
                txtRoomTypeError.Visibility = Visibility.Visible;
                hasError = true;
            }
            else
            {
                txtRoomTypeError.Visibility = Visibility.Collapsed;
            }

            if (string.IsNullOrWhiteSpace(txtRoomArea.Text))
            {
                txtRoomAreaError.Visibility = Visibility.Visible;
                hasError = true;
            }
            else
            {
                txtRoomAreaError.Visibility = Visibility.Collapsed;
            }

            if (hasError)
            {
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
                txtNumberPeopleError.Visibility = Visibility.Collapsed;
                txtNumberBedError.Visibility = Visibility.Collapsed;
                txtQualityError.Visibility = Visibility.Collapsed;
                txtBedTypeError.Visibility = Visibility.Collapsed;
                txtPriceError.Visibility = Visibility.Collapsed;
                txtRoomTypeError.Visibility = Visibility.Collapsed;
                txtRoomAreaError.Visibility = Visibility.Collapsed;

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
            txtNumberPeopleError.Visibility = Visibility.Collapsed;
            txtNumberBedError.Visibility = Visibility.Collapsed;
            txtQualityError.Visibility = Visibility.Collapsed;
            txtBedTypeError.Visibility = Visibility.Collapsed;
            txtPriceError.Visibility = Visibility.Collapsed;
            txtRoomTypeError.Visibility = Visibility.Collapsed;
            txtRoomAreaError.Visibility = Visibility.Collapsed;
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
