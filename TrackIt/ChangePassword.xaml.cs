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
    /// Interaction logic for ChangePassword.xaml
    /// </summary>
    public partial class ChangePassword : Window
    {
        public ChangePassword()
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

                PasswordLabel.SetValue(Canvas.TopProperty, 10 * (SystemParameters.PrimaryScreenHeight / 1080));
                PasswordLabel.Height = SystemParameters.PrimaryScreenHeight * 0.0445;
                PasswordLabel.Width = SystemParameters.PrimaryScreenWidth * 0.1688;
                PasswordLabel.SetValue(Canvas.LeftProperty, 52 * (SystemParameters.PrimaryScreenWidth / 1920));
                PasswordLabel.FontSize = (30 * SystemParameters.PrimaryScreenHeight / 1080);

                EnterPasswordInfo.SetValue(Canvas.TopProperty, 101 * (SystemParameters.PrimaryScreenHeight / 1080));
                EnterPasswordInfo.Height = SystemParameters.PrimaryScreenHeight * 0.0287;
                EnterPasswordInfo.Width = SystemParameters.PrimaryScreenWidth * 0.0917;
                EnterPasswordInfo.SetValue(Canvas.LeftProperty, 126 * (SystemParameters.PrimaryScreenWidth / 1920));
                EnterPasswordInfo.FontSize = (20 * SystemParameters.PrimaryScreenHeight / 1080);

                OldPassword.SetValue(Canvas.TopProperty, 137 * (SystemParameters.PrimaryScreenHeight / 1080));
                OldPassword.Height = SystemParameters.PrimaryScreenHeight * 0.0417;
                OldPassword.Width = SystemParameters.PrimaryScreenWidth * 0.1052;
                OldPassword.SetValue(Canvas.LeftProperty, 113 * (SystemParameters.PrimaryScreenWidth / 1920));

                EnterPassword.SetValue(Canvas.TopProperty, 223 * (SystemParameters.PrimaryScreenHeight / 1080));
                EnterPassword.Height = SystemParameters.PrimaryScreenHeight * 0.025;
                EnterPassword.Width = SystemParameters.PrimaryScreenWidth * 0.1052;
                EnterPassword.SetValue(Canvas.LeftProperty, 113 * (SystemParameters.PrimaryScreenWidth / 1920));
                EnterPassword.FontSize = (20 * SystemParameters.PrimaryScreenHeight / 1080);

                NewPassword.SetValue(Canvas.TopProperty, 255 * (SystemParameters.PrimaryScreenHeight / 1080));
                NewPassword.Height = SystemParameters.PrimaryScreenHeight * 0.0417;
                NewPassword.Width = SystemParameters.PrimaryScreenWidth * 0.1052;
                NewPassword.SetValue(Canvas.LeftProperty, 113 * (SystemParameters.PrimaryScreenWidth / 1920));

                ConfirmPassword.SetValue(Canvas.TopProperty, 341 * (SystemParameters.PrimaryScreenHeight / 1080));
                ConfirmPassword.Height = SystemParameters.PrimaryScreenHeight * 0.025;
                ConfirmPassword.Width = SystemParameters.PrimaryScreenWidth * 0.1052;
                ConfirmPassword.SetValue(Canvas.LeftProperty, 113 * (SystemParameters.PrimaryScreenWidth / 1920));
                ConfirmPassword.FontSize = (20 * SystemParameters.PrimaryScreenHeight / 1080);

                NewPasswordConfirmation.SetValue(Canvas.TopProperty, 373 * (SystemParameters.PrimaryScreenHeight / 1080));
                NewPasswordConfirmation.Height = SystemParameters.PrimaryScreenHeight * 0.0417;
                NewPasswordConfirmation.Width = SystemParameters.PrimaryScreenWidth * 0.1052;
                NewPasswordConfirmation.SetValue(Canvas.LeftProperty, 113 * (SystemParameters.PrimaryScreenWidth / 1920));

                Confirm.SetValue(Canvas.TopProperty, 482 * (SystemParameters.PrimaryScreenHeight / 1080));
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
            if (Properties.Settings.Default.Password != NewPassword.Password)
            {
                if (NewPassword.Password == NewPasswordConfirmation.Password & NewPassword.Password.Length > 7)
                {
                    Properties.Settings.Default.Password = NewPassword.Password;
                    Properties.Settings.Default.Save();
                    var newForm = new PasswordSaved();
                    newForm.Show();
                }
                if (NewPassword.Password != NewPasswordConfirmation.Password & NewPassword.Password.Length > 7)
                {
                    Properties.Settings.Default.Password = NewPassword.Password;
                    var newForm = new PasswordMismatch();
                    newForm.Show();
                }
                if (NewPassword.Password == NewPasswordConfirmation.Password & NewPassword.Password.Length < 7)
                {
                    var newForm = new PasswordTooShort();
                    newForm.Show();
                }
                if (NewPassword.Password != NewPasswordConfirmation.Password & NewPassword.Password.Length < 7)
                {
                    var newForm = new PasswordDoubleMismatch();
                    newForm.Show();
                }
            }
            else
            {
                var newForm = new PasswordSame();
                newForm.Show();
            }
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
