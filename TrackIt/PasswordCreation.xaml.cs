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
using TrackIt.Properties;


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
            if (Password.Password == PasswordConfirmation.Password & Password.Password.Length > 7)
            {
                Properties.Settings.Default.password = PasswordConfirmation.SecurePassword;
                Properties.Settings.Default.Save();
                var newForm = new PasswordSaved();
                newForm.Show();
            }
            if (Password.Password != PasswordConfirmation.Password & Password.Password.Length > 7)
            {
                var newForm = new PasswordMismatch();
                newForm.Show();
            }
            if (Password.Password == PasswordConfirmation.Password & Password.Password.Length < 7)
            {
                var newForm = new PasswordTooShort();
                newForm.Show();
            }
            if (Password.Password != PasswordConfirmation.Password & Password.Password.Length < 7)
            {
                var newForm = new PasswordDoubleMismatch();
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
