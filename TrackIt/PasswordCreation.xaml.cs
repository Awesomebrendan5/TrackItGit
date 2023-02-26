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
    /// Interaction logic for PasswordCreation.xaml
    /// </summary>
    public partial class PasswordCreation : Window
    {
        public PasswordCreation()
        {
            InitializeComponent();
        }
        private void ConfirmButtonClick(object sender, RoutedEventArgs e)
        {

        }
        private void BackButtonClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
