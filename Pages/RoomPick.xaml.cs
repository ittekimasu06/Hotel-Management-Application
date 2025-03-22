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
using System.Windows.Shapes;
using QuanLyKhachSan.Models.Objects;
using QuanLyKhachSan.Models;

namespace QuanLyKhachSan.Pages
{
    /// <summary>
    /// Interaction logic for RoomPick.xaml
    /// </summary>
    public partial class RoomPick : Window
    {
        private ObservableCollection<Room> rooms;
        private readonly DatabaseContext _context = new DatabaseContext();
        public int SelectedRoomId { get; private set; }
        public RoomPick()
        {
            InitializeComponent();
            LoadRooms();
        }

        private void LoadRooms()
        {
            rooms = new ObservableCollection<Room>(_context.Rooms.ToList());
            RoomsDataGrid.ItemsSource = rooms;
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

        private void btnPickRoom_Click(object sender, RoutedEventArgs e)
        {
            if (RoomsDataGrid.SelectedItem is Room selectedRoom)
            {
                SelectedRoomId = selectedRoom.roomId;
                DialogResult = true;
                Close();
            }
            else
            {
                MessageBox.Show("Hãy chọn một phòng.");
            }
        }

        private void btnUndo_Click(object sender, RoutedEventArgs e)
        {
            RoomsDataGrid.SelectedItem = null;
        }

        private void btnCloseWindow_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }

    }
}
