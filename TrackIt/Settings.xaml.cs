using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace TrackIt
{
    /// <summary>
    /// Interaction logic for Settings.xaml
    /// </summary>
    public partial class Settings : Window
    {
        public Settings()
        {
            InitializeComponent();
            ScreenScale();
        }
        void ScreenScale()
        {
            if (SystemParameters.PrimaryScreenHeight != 1080)
            {
                Line.Height = SystemParameters.PrimaryScreenHeight * 1;
                Line.Width = SystemParameters.PrimaryScreenWidth * 0.0031;
                Line.SetValue(Canvas.LeftProperty, 324.0 * (SystemParameters.PrimaryScreenWidth / 1920));

                CalendarIcon.SetValue(Canvas.TopProperty, 182 * (SystemParameters.PrimaryScreenHeight / 1080));
                CalendarIcon.Height = SystemParameters.PrimaryScreenHeight * (CalendarIcon.Height / 1080);
                CalendarIcon.Width = SystemParameters.PrimaryScreenWidth * (CalendarIcon.Width / 1920);
                CalendarIcon.SetValue(Canvas.LeftProperty, 5.0 * (SystemParameters.PrimaryScreenWidth / 1920));

                CalendarButton.SetValue(Canvas.TopProperty, 207.0 * (SystemParameters.PrimaryScreenHeight / 1080));
                CalendarButton.Height = SystemParameters.PrimaryScreenHeight * 0.05;
                CalendarButton.Width = SystemParameters.PrimaryScreenWidth * 0.0885;
                CalendarButton.FontSize = (40 * SystemParameters.PrimaryScreenHeight / 1080);
                CalendarButton.SetValue(Canvas.LeftProperty, 95.0 * (SystemParameters.PrimaryScreenWidth / 1920));

                SccreenTimeIcon.SetValue(Canvas.TopProperty, 404.0 * (SystemParameters.PrimaryScreenHeight / 1080));
                SccreenTimeIcon.Height = SystemParameters.PrimaryScreenHeight * 0.074;
                SccreenTimeIcon.Width = SystemParameters.PrimaryScreenWidth * 0.0417;
                SccreenTimeIcon.SetValue(Canvas.LeftProperty, 10.0 * (SystemParameters.PrimaryScreenWidth / 1920));

                ScreenTime.SetValue(Canvas.TopProperty, 404.0 * (SystemParameters.PrimaryScreenHeight / 1080));
                ScreenTime.Height = SystemParameters.PrimaryScreenHeight * 0.05;
                ScreenTime.Width = SystemParameters.PrimaryScreenWidth * 0.117;
                ScreenTime.FontSize = (40 * SystemParameters.PrimaryScreenHeight / 1080);
                ScreenTime.SetValue(Canvas.LeftProperty, 93.0 * (SystemParameters.PrimaryScreenWidth / 1920));

                PasswordIcon.SetValue(Canvas.TopProperty, 621 * (SystemParameters.PrimaryScreenHeight / 1080));
                PasswordIcon.Height = SystemParameters.PrimaryScreenHeight * 0.0815;
                PasswordIcon.Width = SystemParameters.PrimaryScreenWidth * 0.039;
                PasswordIcon.SetValue(Canvas.LeftProperty, 12 * (SystemParameters.PrimaryScreenWidth / 1920));

                Password.SetValue(Canvas.TopProperty, 646 * (SystemParameters.PrimaryScreenHeight / 1080));
                Password.Height = SystemParameters.PrimaryScreenHeight * 0.05;
                Password.Width = SystemParameters.PrimaryScreenWidth * 0.1;
                Password.FontSize = (40 * SystemParameters.PrimaryScreenHeight / 1080);
                Password.SetValue(Canvas.LeftProperty, 93.0 * (SystemParameters.PrimaryScreenWidth / 1920));

                HomeIcon.SetValue(Canvas.TopProperty, 848 * (SystemParameters.PrimaryScreenHeight / 1080));
                HomeIcon.Height = SystemParameters.PrimaryScreenHeight * 0.0704;
                HomeIcon.Width = SystemParameters.PrimaryScreenWidth * 0.0396;
                HomeIcon.SetValue(Canvas.LeftProperty, 12 * (SystemParameters.PrimaryScreenWidth / 1920));

                Home.SetValue(Canvas.TopProperty, 864 * (SystemParameters.PrimaryScreenHeight / 1080));
                Home.Height = SystemParameters.PrimaryScreenHeight * 0.05;
                Home.Width = SystemParameters.PrimaryScreenWidth * 0.0625;
                Home.FontSize = (40 * SystemParameters.PrimaryScreenHeight / 1080);
                Home.SetValue(Canvas.LeftProperty, 93.0 * (SystemParameters.PrimaryScreenWidth / 1920));

                TrackIt.SetValue(Canvas.TopProperty, 0 * (SystemParameters.PrimaryScreenHeight / 1080));
                TrackIt.Height = SystemParameters.PrimaryScreenHeight * 0.0444;
                TrackIt.Width = SystemParameters.PrimaryScreenWidth * 0.0802;
                TrackIt.FontSize = (40 * SystemParameters.PrimaryScreenHeight / 1080);
                TrackIt.SetValue(Canvas.LeftProperty, 95.0 * (SystemParameters.PrimaryScreenWidth / 1920));

                SettingsButton.SetValue(Canvas.TopProperty, 995 * (SystemParameters.PrimaryScreenHeight / 1080));
                SettingsButton.Height = SystemParameters.PrimaryScreenHeight * 0.05;
                SettingsButton.Width = SystemParameters.PrimaryScreenWidth * 0.0875;
                SettingsButton.FontSize = (40 * SystemParameters.PrimaryScreenHeight / 1080);
                SettingsButton.SetValue(Canvas.LeftProperty, 97 * (SystemParameters.PrimaryScreenWidth / 1920));

                SettingsIcon.SetValue(Canvas.TopProperty, 981 * (SystemParameters.PrimaryScreenHeight / 1080));
                SettingsIcon.Height = SystemParameters.PrimaryScreenHeight * 0.0713;
                SettingsIcon.Width = SystemParameters.PrimaryScreenWidth * 0.04167;
                SettingsIcon.SetValue(Canvas.LeftProperty, 12 * (SystemParameters.PrimaryScreenWidth / 1920));

                DeleteBlacklist.SetValue(Canvas.TopProperty, 24 * (SystemParameters.PrimaryScreenHeight / 1080));
                DeleteBlacklist.Height = SystemParameters.PrimaryScreenHeight * 0.0556;
                DeleteBlacklist.Width = SystemParameters.PrimaryScreenWidth * 0.1563;
                DeleteBlacklist.FontSize = (40 * SystemParameters.PrimaryScreenHeight / 1080);
                DeleteBlacklist.SetValue(Canvas.LeftProperty, 480 * (SystemParameters.PrimaryScreenWidth / 1920));

                DeleteNotTrack.SetValue(Canvas.TopProperty, 24 * (SystemParameters.PrimaryScreenHeight / 1080));
                DeleteNotTrack.Height = SystemParameters.PrimaryScreenHeight * 0.0556;
                DeleteNotTrack.Width = SystemParameters.PrimaryScreenWidth * 0.3083;
                DeleteNotTrack.FontSize = (40 * SystemParameters.PrimaryScreenHeight / 1080);
                DeleteNotTrack.SetValue(Canvas.LeftProperty, 1257 * (SystemParameters.PrimaryScreenWidth / 1920));

                DeleteScreentime.SetValue(Canvas.TopProperty, 424 * (SystemParameters.PrimaryScreenHeight / 1080));
                DeleteScreentime.Height = SystemParameters.PrimaryScreenHeight * 0.0556;
                DeleteScreentime.Width = SystemParameters.PrimaryScreenWidth * 0.2230;
                DeleteScreentime.FontSize = (40 * SystemParameters.PrimaryScreenHeight / 1080);
                DeleteScreentime.SetValue(Canvas.LeftProperty, 416 * (SystemParameters.PrimaryScreenWidth / 1920));

                DeleteLimits.SetValue(Canvas.TopProperty, 424 * (SystemParameters.PrimaryScreenHeight / 1080));
                DeleteLimits.Height = SystemParameters.PrimaryScreenHeight * 0.0556;
                DeleteLimits.Width = SystemParameters.PrimaryScreenWidth * 0.2438;
                DeleteLimits.FontSize = (40 * SystemParameters.PrimaryScreenHeight / 1080);
                DeleteLimits.SetValue(Canvas.LeftProperty, 1319 * (SystemParameters.PrimaryScreenWidth / 1920));

                DeleteBlacklistButton.SetValue(Canvas.TopProperty, 125 * (SystemParameters.PrimaryScreenHeight / 1080));
                DeleteBlacklistButton.Height = SystemParameters.PrimaryScreenHeight * 0.0759;
                DeleteBlacklistButton.Width = SystemParameters.PrimaryScreenWidth * 0.1120;
                DeleteBlacklistButton.FontSize = (40 * SystemParameters.PrimaryScreenHeight / 1080);
                DeleteBlacklistButton.SetValue(Canvas.LeftProperty, 518 * (SystemParameters.PrimaryScreenWidth / 1920));

                DeleteNotTrackButton.SetValue(Canvas.TopProperty, 125 * (SystemParameters.PrimaryScreenHeight / 1080));
                DeleteNotTrackButton.Height = SystemParameters.PrimaryScreenHeight * 0.0759;
                DeleteNotTrackButton.Width = SystemParameters.PrimaryScreenWidth * 0.1115;
                DeleteNotTrackButton.FontSize = (40 * SystemParameters.PrimaryScreenHeight / 1080);
                DeleteNotTrackButton.SetValue(Canvas.LeftProperty, 1446 * (SystemParameters.PrimaryScreenWidth / 1920));

                DeleteScreentimeButton.SetValue(Canvas.TopProperty, 646 * (SystemParameters.PrimaryScreenHeight / 1080));
                DeleteScreentimeButton.Height = SystemParameters.PrimaryScreenHeight * 0.0759;
                DeleteScreentimeButton.Width = SystemParameters.PrimaryScreenWidth * 0.1120;
                DeleteScreentimeButton.FontSize = (40 * SystemParameters.PrimaryScreenHeight / 1080);
                DeleteScreentimeButton.SetValue(Canvas.LeftProperty, 518 * (SystemParameters.PrimaryScreenWidth / 1920));

                DeleteLimitsButton.SetValue(Canvas.TopProperty, 652 * (SystemParameters.PrimaryScreenHeight / 1080));
                DeleteLimitsButton.Height = SystemParameters.PrimaryScreenHeight * 0.0759;
                DeleteLimitsButton.Width = SystemParameters.PrimaryScreenWidth * 0.1120;
                DeleteLimitsButton.FontSize = (40 * SystemParameters.PrimaryScreenHeight / 1080);
                DeleteLimitsButton.SetValue(Canvas.LeftProperty, 1445 * (SystemParameters.PrimaryScreenWidth / 1920));

                Help.SetValue(Canvas.TopProperty, 757 * (SystemParameters.PrimaryScreenHeight / 1080));
                Help.Height = SystemParameters.PrimaryScreenHeight * 0.0556;
                Help.Width = SystemParameters.PrimaryScreenWidth * 0.1281;
                Help.FontSize = (40 * SystemParameters.PrimaryScreenHeight / 1080);
                Help.SetValue(Canvas.LeftProperty, 960 * (SystemParameters.PrimaryScreenWidth / 1920));
            }
        }

        void CalendarButtonClick(object sender, RoutedEventArgs e)
        {
            if (Properties.Settings.Default.MiniWindowOpened == false & Properties.Settings.Default.MiniWindowOpened1 == false)
            {
                var newForm = new CalendarMenu();
                newForm.Show();
                this.Close();
            }

        }
        void ScreenTimeButtonClick(object sender, RoutedEventArgs e)
        {
            if (Properties.Settings.Default.MiniWindowOpened == false & Properties.Settings.Default.MiniWindowOpened1 == false)
            {
                var newForm = new ScreentimeMenu();
                newForm.Show();
                this.Close();
            }
        }
        void PasswordButtonClick(object sender, RoutedEventArgs e)
        {
            if (Properties.Settings.Default.MiniWindowOpened == false & Properties.Settings.Default.MiniWindowOpened1 == false)
            {
                var newForm = new PasswordMenu();
                newForm.Show();
                this.Close();
            }
        }
        void HomeButtonClick(object sender, RoutedEventArgs e)
        {
            if (Properties.Settings.Default.MiniWindowOpened == false & Properties.Settings.Default.MiniWindowOpened1 == false)
            {
                var newForm = new MainWindow();
                newForm.Show();
                this.Close();
            }
        }
        private void SettingsButtonClick(object sender, RoutedEventArgs e)
        {

        }

        private void DeleteBlacklistButtonClick(object sender, RoutedEventArgs e)
        {
            if (Properties.Settings.Default.MiniWindowOpened == false & Properties.Settings.Default.MiniWindowOpened1 == false)
            {
                Properties.Settings.Default.MiniWindowOpened = true;
                var newForm = new DeleteBlacklists();
                newForm.Show();
            }
        }
        private void DeleteNotTrackButtonClick(object sender, RoutedEventArgs e)
        {
            if (Properties.Settings.Default.MiniWindowOpened == false & Properties.Settings.Default.MiniWindowOpened1 == false)
            {
                Properties.Settings.Default.MiniWindowOpened = true;
                var newForm = new DeleteApplicationsNotToTrack();
                newForm.Show();
            }
        }
        private void DeleteScreentimeButtonClick(object sender, RoutedEventArgs e)
        {
            if (Properties.Settings.Default.MiniWindowOpened == false & Properties.Settings.Default.MiniWindowOpened1 == false)
            {
                Properties.Settings.Default.MiniWindowOpened = true;
                var newForm = new DeleteScreentime();
                newForm.Show();
            }
        }
        private void DeleteLimitsButtonClick(object sender, RoutedEventArgs e)
        {
            if (Properties.Settings.Default.MiniWindowOpened == false & Properties.Settings.Default.MiniWindowOpened1 == false)
            {
                Properties.Settings.Default.MiniWindowOpened = true;
                var newForm = new DeleteLimits();
                newForm.Show();
            }
        }
    }
}
