using System.Windows;
using System.Windows.Controls;


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
            Screenscale();
            Resizer();
        }

        void Resizer()
        {

        }
        void Screenscale()
        {
            if (SystemParameters.PrimaryScreenHeight == 1080)
            {
                MinHeight = SystemParameters.PrimaryScreenHeight * (740.0 / 1080.0);
                MinWidth = SystemParameters.PrimaryScreenWidth * (428.0 / 1920);

                PasswordLabel.SetValue(Canvas.TopProperty, 10 * (SystemParameters.PrimaryScreenHeight / 1080));
                PasswordLabel.Height = SystemParameters.PrimaryScreenHeight * 0.0444;
                PasswordLabel.Width = SystemParameters.PrimaryScreenWidth * 0.1781;
                PasswordLabel.SetValue(Canvas.LeftProperty, 52 * (SystemParameters.PrimaryScreenWidth / 1920));
                PasswordLabel.FontSize = (30 * SystemParameters.PrimaryScreenHeight / 1080);

                EnterPassword.SetValue(Canvas.TopProperty, 156 * (SystemParameters.PrimaryScreenHeight / 1080));
                EnterPassword.Height = SystemParameters.PrimaryScreenHeight * 0.0250;
                EnterPassword.Width = SystemParameters.PrimaryScreenWidth * 0.1052;
                EnterPassword.SetValue(Canvas.LeftProperty, 113 * (SystemParameters.PrimaryScreenWidth / 1920));
                EnterPassword.FontSize = (20 * SystemParameters.PrimaryScreenHeight / 1080);

                Password.SetValue(Canvas.TopProperty, 188 * (SystemParameters.PrimaryScreenHeight / 1080));
                Password.Height = SystemParameters.PrimaryScreenHeight * 0.0417;
                Password.Width = SystemParameters.PrimaryScreenWidth * 0.1052;
                Password.SetValue(Canvas.LeftProperty, 113 * (SystemParameters.PrimaryScreenWidth / 1920));

                ConfirmPassword.SetValue(Canvas.TopProperty, 320 * (SystemParameters.PrimaryScreenHeight / 1080));
                ConfirmPassword.Height = SystemParameters.PrimaryScreenHeight * 0.0250;
                ConfirmPassword.Width = SystemParameters.PrimaryScreenWidth * 0.1052;
                ConfirmPassword.SetValue(Canvas.LeftProperty, 113 * (SystemParameters.PrimaryScreenWidth / 1920));
                ConfirmPassword.FontSize = (20 * SystemParameters.PrimaryScreenHeight / 1080);

                PasswordConfirmation.SetValue(Canvas.TopProperty, 352 * (SystemParameters.PrimaryScreenHeight / 1080));
                PasswordConfirmation.Height = SystemParameters.PrimaryScreenHeight * 0.0417;
                PasswordConfirmation.Width = SystemParameters.PrimaryScreenWidth * 0.1052;
                PasswordConfirmation.SetValue(Canvas.LeftProperty, 113 * (SystemParameters.PrimaryScreenWidth / 1920));

                Confirm.SetValue(Canvas.TopProperty, 489 * (SystemParameters.PrimaryScreenHeight / 1080));
                Confirm.Height = SystemParameters.PrimaryScreenHeight * 0.0398;
                Confirm.Width = SystemParameters.PrimaryScreenWidth * 0.0646;
                Confirm.SetValue(Canvas.LeftProperty, 152 * (SystemParameters.PrimaryScreenWidth / 1920));
                Confirm.FontSize = (20 * SystemParameters.PrimaryScreenHeight / 1080);

                Back.SetValue(Canvas.TopProperty, 665 * (SystemParameters.PrimaryScreenHeight / 1080));
                Back.Height = SystemParameters.PrimaryScreenHeight * (43.0 / 1080.0);
                Back.Width = SystemParameters.PrimaryScreenWidth * (124.0 / 1920.0);
                Back.SetValue(Canvas.LeftProperty, 0 * (SystemParameters.PrimaryScreenWidth / 1920));
                Back.FontSize = (20 * SystemParameters.PrimaryScreenHeight / 1080);

                Info.SetValue(Canvas.TopProperty, 78 * (SystemParameters.PrimaryScreenHeight / 1080));
                Info.Height = SystemParameters.PrimaryScreenHeight * 0.0287;
                Info.Width = SystemParameters.PrimaryScreenWidth * 0.1958;
                Info.SetValue(Canvas.LeftProperty, 26 * (SystemParameters.PrimaryScreenWidth / 1920));
                Info.FontSize = (20 * SystemParameters.PrimaryScreenHeight / 1080);
            }
        }
        private void ConfirmButtonClick(object sender, RoutedEventArgs e)
        {
            if (Password.Password == PasswordConfirmation.Password & Password.Password.Length > 7)
            {
                Properties.Settings.Default.Password = PasswordConfirmation.Password;
                Properties.Settings.Default.Save();
                var newForm = new PasswordSaved();
                newForm.Show();
            }
            if (Password.Password != PasswordConfirmation.Password & Password.Password.Length > 7)
            {
                var newForm = new PasswordMismatch();
                newForm.Show();
            }
            if (Password.Password == PasswordConfirmation.Password & Password.Password.Length <= 7)
            {
                var newForm = new PasswordTooShort();
                newForm.Show();
            }
            if (Password.Password != PasswordConfirmation.Password & Password.Password.Length <= 7)
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
