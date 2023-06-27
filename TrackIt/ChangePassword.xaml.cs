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
        }
        private void ConfirmButtonClick(object sender, RoutedEventArgs e)
        {
            string password = new System.Net.NetworkCredential(string.Empty, Properties.Settings.Default.password).Password;
            if (password != NewPassword.Password)
            {
                if (NewPassword.Password == NewPasswordConfirmation.Password & NewPassword.Password.Length > 7)
                {
                    Properties.Settings.Default.password = NewPassword.SecurePassword;
                    Properties.Settings.Default.Save();
                    var newForm = new PasswordSaved();
                    newForm.Show();
                }
                if (NewPassword.Password != NewPasswordConfirmation.Password & NewPassword.Password.Length > 7)
                {
                    Properties.Settings.Default.password = NewPassword.SecurePassword;
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
