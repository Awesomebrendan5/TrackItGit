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
        }

        void CalendarButtonClick(object sender, RoutedEventArgs e)
        {
            var newForm = new CalendarMenu();
            newForm.Show();
            this.Close();

        }
        void ScreenTimeButtonClick(object sender, RoutedEventArgs e)
        {
            var newForm = new ScreentimeMenu();
            newForm.Show();
            this.Close();
        }
        void PasswordButtonClick(object sender, RoutedEventArgs e)
        {
            var newForm = new PasswordMenu();
            newForm.Show();
            this.Close();
        }
        void HomeButtonClick(object sender, RoutedEventArgs e)
        {

        }
        private void SettingsButtonClick(object sender, RoutedEventArgs e)
        {

        }

        private void DeleteBlacklistButtonClick(object sender, RoutedEventArgs e)
        {
            var newForm = new DeleteBlacklists();
            newForm.Show();
        }
        private void DeleteNotTrackButtonClick(object sender, RoutedEventArgs e)
        {
            var newForm = new DeleteApplicationsNotToTrack();
            newForm.Show();
        }
        private void DeleteScreentimeButtonClick(object sender, RoutedEventArgs e)
        {
            var newForm = new DeleteScreentime();
            newForm.Show();
        }
        private void DeleteLimitsButtonClick(object sender, RoutedEventArgs e)
        {
            var newForm = new DeleteLimits();
            newForm.Show();
        }
    }
}
