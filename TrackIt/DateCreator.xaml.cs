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
    /// Interaction logic for DateCreator.xaml
    /// </summary>
    public partial class DateCreator : Window
    {
        public DateCreator()
        {
            InitializeComponent();
            DateTitle();
        }
        void DateTitle()
        {
            TrackIt.Text = Properties.Settings.Default.DatePicked.ToShortDateString();
        }
        private void ConfirmButtonClick(object sender, RoutedEventArgs e)
        {

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
