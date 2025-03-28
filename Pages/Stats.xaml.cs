using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Emit;
using System.Runtime.Serialization;
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
using LiveCharts;
using LiveCharts.Wpf;
using QuanLyKhachSan.Models;

namespace QuanLyKhachSan.Pages
{
    public partial class Stats : Page
    {
        private readonly DatabaseContext _context = new DatabaseContext();

        public Stats()
        {
            InitializeComponent();
            dateThoiGian.SelectedDateChanged += DateThoiGian_SelectedDateChanged;
        }

        private void DateThoiGian_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dateThoiGian.SelectedDate.HasValue)
            {
                DateTime selectedDate = dateThoiGian.SelectedDate.Value;
                LoadStats(selectedDate);
                LoadChart();
            }
        }

        private void LoadStats(DateTime date)
        {
            var billsInMonth = _context.Bills
                .Where(b => b.createdAt.Month == date.Month && b.createdAt.Year == date.Year && (b.status == 1 || b.status == 3 || b.status == 4)) // 1 = da thanh toan, 3 = da nhan phong, 4 = da tra phong
                .ToList();

            ClearStats();
            if (billsInMonth.Any())
            {
                // Phòng đặt nhiều nhất
                var mostBookedRoom = billsInMonth
                    .GroupBy(b => b.roomId)
                    .OrderByDescending(g => g.Count())
                    .FirstOrDefault();

                if (mostBookedRoom != null)
                {
                    txtPhongNhieu.Text = $"Phòng số {mostBookedRoom.Key}";
                    txtSoLan.Text = $"{mostBookedRoom.Count()} lần";
                }
                // Lợi nhuận
                float totalRevenue = billsInMonth.Sum(b => b.total ?? 0);
                txtLoiNhuan.Text = $"{totalRevenue} VND";
                txtThang.Text = $"Tháng {date.Month}";
            }
        }

        private void LoadChart()
        {
            SeriesCollection = new SeriesCollection
            {
                new LineSeries
                {
                    Title = "Năm nay",
                    Values = new ChartValues<float>(GetMonthlyRevenue(DateTime.Now.Year)),
                    Stroke = System.Windows.Media.Brushes.Blue,
                    Fill = System.Windows.Media.Brushes.Transparent,
                    PointGeometrySize = 10
                },
                new LineSeries
                {
                    Title = "Năm ngoái",
                    Values = new ChartValues<float>(GetMonthlyRevenue(DateTime.Now.Year - 1)),
                    Stroke = System.Windows.Media.Brushes.Yellow,
                    Fill = System.Windows.Media.Brushes.Transparent,
                    PointGeometrySize = 10
                }
            };

            Labels = new[] { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12" };
            Formatter = value => $"{Math.Round(value, 2)}M";

            DataContext = this;
        }


        private List<float> GetMonthlyRevenue(int year)
        {
            var monthlyRevenue = new List<float>(new float[12]);

            var bills = _context.Bills
                .Where(b => b.createdAt.Year == year && (b.status == 1 || b.status == 3 || b.status == 4)) // 1 = da thanh toan, 3 = da nhan phong, 4 = da tra phong
                .GroupBy(b => b.createdAt.Month)
                .Select(g => new { Month = g.Key, Total = g.Sum(b => b.total ?? 0) })
                .ToList();

            foreach (var bill in bills)
            {
                monthlyRevenue[bill.Month - 1] = bill.Total / 1_000_000f; 
            }

            return monthlyRevenue;
        }

        public SeriesCollection SeriesCollection { get; set; }
        public string[] Labels { get; set; }
        public Func<double, string> Formatter { get; set; }

        private void ClearStats()
        {
            txtPhongNhieu.Text = "";
            txtSoLan.Text = "";
            txtLoiNhuan.Text = "";
            txtThang.Text = "";
        }
    }
}

