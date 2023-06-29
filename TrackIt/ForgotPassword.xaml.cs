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
    /// Interaction logic for ForgotPassword.xaml
    /// </summary>
    public partial class ForgotPassword : Window
    {
        public ForgotPassword()
        {
            InitializeComponent();
            Screenscale();
        }
        void Screenscale()
        {
            if (SystemParameters.PrimaryScreenHeight != 1080)
            {
                Height = SystemParameters.PrimaryScreenHeight * 0.6667;
                Width = SystemParameters.PrimaryScreenWidth * 0.2229;

                ForgotPasswordText.SetValue(Canvas.TopProperty, 10 * (SystemParameters.PrimaryScreenHeight / 1080));
                ForgotPasswordText.Height = SystemParameters.PrimaryScreenHeight * 0.0444;
                ForgotPasswordText.Width = SystemParameters.PrimaryScreenWidth * 0.1339;
                ForgotPasswordText.SetValue(Canvas.LeftProperty, 69 * (SystemParameters.PrimaryScreenWidth / 1920));
                ForgotPasswordText.FontSize = (30 * SystemParameters.PrimaryScreenHeight / 1080);

                Information.SetValue(Canvas.TopProperty, 142 * (SystemParameters.PrimaryScreenHeight / 1080));
                Information.Height = SystemParameters.PrimaryScreenHeight * 0.125;
                Information.Width = SystemParameters.PrimaryScreenWidth * 0.1880;
                Information.SetValue(Canvas.LeftProperty, 39 * (SystemParameters.PrimaryScreenWidth / 1920));
                Information.FontSize = (20 * SystemParameters.PrimaryScreenHeight / 1080);

                Confirm.SetValue(Canvas.TopProperty, 277 * (SystemParameters.PrimaryScreenHeight / 1080));
                Confirm.Height = SystemParameters.PrimaryScreenHeight * 0.0398;
                Confirm.Width = SystemParameters.PrimaryScreenWidth * 0.0646;
                Confirm.SetValue(Canvas.LeftProperty, 152 * (SystemParameters.PrimaryScreenWidth / 1920));
                Confirm.FontSize = (20 * SystemParameters.PrimaryScreenHeight / 1080);

                Back.SetValue(Canvas.TopProperty, 651 * (SystemParameters.PrimaryScreenHeight / 1080));
                Back.Height = SystemParameters.PrimaryScreenHeight * 0.0398;
                Back.Width = SystemParameters.PrimaryScreenWidth * 0.0646;
                Back.SetValue(Canvas.LeftProperty, -4 * (SystemParameters.PrimaryScreenWidth / 1920));
                Back.FontSize = (20 * SystemParameters.PrimaryScreenHeight / 1080);
            }
        }
        private void ConfirmButtonClick(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.Password = null;
            Properties.Settings.Default.Save();
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
