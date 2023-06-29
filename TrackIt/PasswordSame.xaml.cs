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
    /// Interaction logic for PasswordSame.xaml
    /// </summary>
    public partial class PasswordSame : Window
    {
        public PasswordSame()
        {
            InitializeComponent();
            Screenscale();
        }
        void Screenscale()
        {
            if (SystemParameters.PrimaryScreenHeight != 1080)
            {
                Height = SystemParameters.PrimaryScreenHeight * 0.2611;
                Width = SystemParameters.PrimaryScreenWidth * 0.2401;

                ErrorMessage.SetValue(Canvas.TopProperty, 10 * (SystemParameters.PrimaryScreenHeight / 1080));
                ErrorMessage.Height = SystemParameters.PrimaryScreenHeight * 0.0444;
                ErrorMessage.Width = SystemParameters.PrimaryScreenWidth * 0.1339;
                ErrorMessage.SetValue(Canvas.LeftProperty, 102 * (SystemParameters.PrimaryScreenWidth / 1920));
                ErrorMessage.FontSize = (30 * SystemParameters.PrimaryScreenHeight / 1080);

                Information.SetValue(Canvas.TopProperty, 95 * (SystemParameters.PrimaryScreenHeight / 1080));
                Information.Height = SystemParameters.PrimaryScreenHeight * 0.0491;
                Information.Width = SystemParameters.PrimaryScreenWidth * 0.188021;
                Information.SetValue(Canvas.LeftProperty, 50 * (SystemParameters.PrimaryScreenWidth / 1920));
                Information.FontSize = (20 * SystemParameters.PrimaryScreenHeight / 1080);

                Exit.SetValue(Canvas.TopProperty, 182 * (SystemParameters.PrimaryScreenHeight / 1080));
                Exit.Height = SystemParameters.PrimaryScreenHeight * 0.05;
                Exit.Width = SystemParameters.PrimaryScreenWidth * 0.0729;
                Exit.SetValue(Canvas.LeftProperty, 160 * (SystemParameters.PrimaryScreenWidth / 1920));
                Exit.FontSize = (25 * SystemParameters.PrimaryScreenHeight / 1080);
            }
        }
        private void ExitButtonClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
