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
using static System.Net.Mime.MediaTypeNames;

namespace TrackIt
{
    /// <summary>
    /// Interaction logic for EventCreator.xaml
    /// </summary>
    public partial class Scheduler : Window
    {
        public Scheduler()
        {
            InitializeComponent();
            DateTitle();
        }
        void DateTitle()
        {
            DateToday.Text = Properties.Settings.Default.DatePicked.ToShortDateString();
        }
        private void NewEventClick(object sender, RoutedEventArgs e)
        {
            var newForm = new DateCreator();
            newForm.Show();
            this.Close();
        }
        private void BackButtonClick(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.MiniWindowOpened = false;
            this.Close();
        }
        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            Properties.Settings.Default.MiniWindowOpened = false;
            Properties.Settings.Default.Save();
        }
    }
}
