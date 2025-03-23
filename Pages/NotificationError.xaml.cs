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
using System.Windows.Shapes;
using System.Windows.Media.Animation;

namespace QuanLyKhachSan.Pages
{
    /// <summary>
    /// Interaction logic for NotificationError.xaml
    /// </summary>
    public partial class NotificationError : Window
    {
        public NotificationError(string title, string content)
        {
            InitializeComponent();
            txtNotificationError.Text = title;
            txtContentError.Text = content;
            this.Loaded += NotificationWindow_Loaded;
        }

        private void NotificationWindow_Loaded(object sender, RoutedEventArgs e)
        {
            // Lấy cửa sổ cha (Owner) làm mốc
            var owner = this.Owner;

            if (owner != null)
            {

                var ownerPosition = owner.PointToScreen(new Point(0, 0));
                double ownerLeft = ownerPosition.X;
                double ownerTop = ownerPosition.Y;
                double ownerWidth = owner.ActualWidth;
                double ownerHeight = owner.ActualHeight;

                double startTop = ownerTop + ownerHeight;
                double endTop = ownerTop + ownerHeight - this.Height - 20;
                double leftPosition = ownerLeft + ownerWidth - this.Width - 20;

                this.Left = leftPosition;
                this.Top = startTop;

                var storyboard = new Storyboard();

                var slideIn = new DoubleAnimation
                {
                    From = startTop,
                    To = endTop,
                    Duration = TimeSpan.FromSeconds(0.5),
                    EasingFunction = new QuadraticEase()
                };
                Storyboard.SetTarget(slideIn, this);
                Storyboard.SetTargetProperty(slideIn, new PropertyPath(TopProperty));

                var slideOut = new DoubleAnimation
                {
                    From = endTop,
                    To = startTop,
                    Duration = TimeSpan.FromSeconds(0.5),
                    BeginTime = TimeSpan.FromSeconds(5),
                    EasingFunction = new QuadraticEase()
                };
                Storyboard.SetTarget(slideOut, this);
                Storyboard.SetTargetProperty(slideOut, new PropertyPath(TopProperty));

                storyboard.Children.Add(slideIn);
                storyboard.Children.Add(slideOut);

                storyboard.Completed += (s, a) => this.Close();

                storyboard.Begin();
            }
            else
            {
                var desktopWorkingArea = System.Windows.SystemParameters.WorkArea;
                this.Left = desktopWorkingArea.Right - this.Width - 20;
                this.Top = desktopWorkingArea.Bottom - this.Height - 20;
            }
        }


        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            var desktopWorkingArea = System.Windows.SystemParameters.WorkArea;

            // Animation trượt xuống khi bấm đóng
            var slideOut = new DoubleAnimation
            {
                From = this.Top,
                To = desktopWorkingArea.Bottom - 200,
                Duration = TimeSpan.FromSeconds(0.5),
                EasingFunction = new QuadraticEase()
            };

            // Đóng cửa sổ sau khi hoàn tất
            slideOut.Completed += (s, a) => this.Close();

            // Bắt đầu trượt xuống
            this.BeginAnimation(TopProperty, slideOut);
        }
    }
}
