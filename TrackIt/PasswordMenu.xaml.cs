using System.Windows;
using System.Windows.Controls;

namespace TrackIt
{
    /// <summary>
    /// Interaction logic for PasswordMenu.xaml
    /// </summary>
    public partial class PasswordMenu : Window
    {
        public PasswordMenu()
        {
            InitializeComponent();
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            WindowState = WindowState.Maximized;
            ScreenScale();
        }
        void ScreenScale()
        {
            if (SystemParameters.PrimaryScreenHeight != 1080 | SystemParameters.PrimaryScreenWidth != 1920)
            {
                Line.Height = SystemParameters.PrimaryScreenHeight * 1;
                Line.Width = SystemParameters.PrimaryScreenWidth * 0.0031;
                Line.SetValue(Canvas.LeftProperty, 324.0 * (SystemParameters.PrimaryScreenWidth / 1920));

                CalendarIcon.SetValue(Canvas.TopProperty, 182.0 * (SystemParameters.PrimaryScreenHeight / 1080));
                CalendarIcon.Height = SystemParameters.PrimaryScreenHeight * 0.0963;
                CalendarIcon.Width = SystemParameters.PrimaryScreenWidth * 0.0443;
                CalendarIcon.SetValue(Canvas.LeftProperty, 5.0 * (SystemParameters.PrimaryScreenWidth / 1920));

                Calendar.SetValue(Canvas.TopProperty, 207.0 * (SystemParameters.PrimaryScreenHeight / 1080));
                Calendar.Height = SystemParameters.PrimaryScreenHeight * 0.05;
                Calendar.Width = SystemParameters.PrimaryScreenWidth * 0.0885;
                Calendar.FontSize = (40 * SystemParameters.PrimaryScreenHeight / 1080);
                Calendar.SetValue(Canvas.LeftProperty, 95.0 * (SystemParameters.PrimaryScreenWidth / 1920));

                SccreenTimeIcon.SetValue(Canvas.TopProperty, 404.0 * (SystemParameters.PrimaryScreenHeight / 1080));
                SccreenTimeIcon.Height = SystemParameters.PrimaryScreenHeight * 0.074;
                SccreenTimeIcon.Width = SystemParameters.PrimaryScreenWidth * 0.0417;
                SccreenTimeIcon.SetValue(Canvas.LeftProperty, 10.0 * (SystemParameters.PrimaryScreenWidth / 1920));

                ScreenTime.SetValue(Canvas.TopProperty, 404.0 * (SystemParameters.PrimaryScreenHeight / 1080));
                ScreenTime.Height = SystemParameters.PrimaryScreenHeight * 0.05;
                ScreenTime.Width = SystemParameters.PrimaryScreenWidth * 0.117;
                ScreenTime.FontSize = (40 * SystemParameters.PrimaryScreenHeight / 1080);
                ScreenTime.SetValue(Canvas.LeftProperty, 93.0 * (SystemParameters.PrimaryScreenWidth / 1920));

                PasswordIcon.SetValue(Canvas.TopProperty, 621 * (SystemParameters.PrimaryScreenHeight / 1080));
                PasswordIcon.Height = SystemParameters.PrimaryScreenHeight * 0.0815;
                PasswordIcon.Width = SystemParameters.PrimaryScreenWidth * 0.039;
                PasswordIcon.SetValue(Canvas.LeftProperty, 12 * (SystemParameters.PrimaryScreenWidth / 1920));

                Password.SetValue(Canvas.TopProperty, 646 * (SystemParameters.PrimaryScreenHeight / 1080));
                Password.Height = SystemParameters.PrimaryScreenHeight * 0.05;
                Password.Width = SystemParameters.PrimaryScreenWidth * 0.1;
                Password.FontSize = (40 * SystemParameters.PrimaryScreenHeight / 1080);
                Password.SetValue(Canvas.LeftProperty, 93.0 * (SystemParameters.PrimaryScreenWidth / 1920));

                HomeIcon.SetValue(Canvas.TopProperty, 848 * (SystemParameters.PrimaryScreenHeight / 1080));
                HomeIcon.Height = SystemParameters.PrimaryScreenHeight * 0.0704;
                HomeIcon.Width = SystemParameters.PrimaryScreenWidth * 0.0396;
                HomeIcon.SetValue(Canvas.LeftProperty, 12 * (SystemParameters.PrimaryScreenWidth / 1920));

                Home.SetValue(Canvas.TopProperty, 864 * (SystemParameters.PrimaryScreenHeight / 1080));
                Home.Height = SystemParameters.PrimaryScreenHeight * 0.05;
                Home.Width = SystemParameters.PrimaryScreenWidth * 0.0625;
                Home.FontSize = (40 * SystemParameters.PrimaryScreenHeight / 1080);
                Home.SetValue(Canvas.LeftProperty, 93.0 * (SystemParameters.PrimaryScreenWidth / 1920));

                TrackIt.SetValue(Canvas.TopProperty, 0 * (SystemParameters.PrimaryScreenHeight / 1080));
                TrackIt.Height = SystemParameters.PrimaryScreenHeight * 0.0444;
                Home.Width = SystemParameters.PrimaryScreenWidth * 0.0802;
                TrackIt.FontSize = (40 * SystemParameters.PrimaryScreenHeight / 1080);
                TrackIt.SetValue(Canvas.LeftProperty, 95.0 * (SystemParameters.PrimaryScreenWidth / 1920));

                Box.SetValue(Canvas.TopProperty, 272 * (SystemParameters.PrimaryScreenHeight / 1080));
                Box.Height = SystemParameters.PrimaryScreenHeight * 0.4824;
                Box.Width = SystemParameters.PrimaryScreenWidth * 0.1781;
                Box.SetValue(Canvas.LeftProperty, 960 * (SystemParameters.PrimaryScreenWidth / 1920));

                PasswordLabel.SetValue(Canvas.TopProperty, 24 * (SystemParameters.PrimaryScreenHeight / 1080));
                PasswordLabel.Height = SystemParameters.PrimaryScreenHeight * 0.0565;
                PasswordLabel.Width = SystemParameters.PrimaryScreenWidth * 0.2104;
                PasswordLabel.FontSize = (50 * SystemParameters.PrimaryScreenHeight / 1080);
                PasswordLabel.SetValue(Canvas.LeftProperty, 929 * (SystemParameters.PrimaryScreenWidth / 1920));

                PasswordCreationButton.SetValue(Canvas.TopProperty, 342 * (SystemParameters.PrimaryScreenHeight / 1080));
                PasswordCreationButton.Height = SystemParameters.PrimaryScreenHeight * 0.0481;
                PasswordCreationButton.Width = SystemParameters.PrimaryScreenWidth * 0.1365;
                PasswordCreationButton.SetValue(Canvas.LeftProperty, 1000 * (SystemParameters.PrimaryScreenWidth / 1920));

                ChangePasswordButton.SetValue(Canvas.TopProperty, 480 * (SystemParameters.PrimaryScreenHeight / 1080));
                ChangePasswordButton.Height = SystemParameters.PrimaryScreenHeight * 0.0481;
                ChangePasswordButton.Width = SystemParameters.PrimaryScreenWidth * 0.1365;
                ChangePasswordButton.SetValue(Canvas.LeftProperty, 1000 * (SystemParameters.PrimaryScreenWidth / 1920));

                ForgotPasswordButton.SetValue(Canvas.TopProperty, 618 * (SystemParameters.PrimaryScreenHeight / 1080));
                ForgotPasswordButton.Height = SystemParameters.PrimaryScreenHeight * 0.0481;
                ForgotPasswordButton.Width = SystemParameters.PrimaryScreenWidth * 0.1365;
                ForgotPasswordButton.SetValue(Canvas.LeftProperty, 1000 * (SystemParameters.PrimaryScreenWidth / 1920));

                SettingsButton.SetValue(Canvas.TopProperty, 995 * (SystemParameters.PrimaryScreenHeight / 1080));
                SettingsButton.Height = SystemParameters.PrimaryScreenHeight * 0.05;
                SettingsButton.Width = SystemParameters.PrimaryScreenWidth * 0.0875;
                SettingsButton.FontSize = (40 * SystemParameters.PrimaryScreenHeight / 1080);
                SettingsButton.SetValue(Canvas.LeftProperty, 97 * (SystemParameters.PrimaryScreenWidth / 1920));

                SettingsIcon.SetValue(Canvas.TopProperty, 981 * (SystemParameters.PrimaryScreenHeight / 1080));
                SettingsIcon.Height = SystemParameters.PrimaryScreenHeight * 0.0713;
                SettingsIcon.Width = SystemParameters.PrimaryScreenWidth * 0.04167;
                SettingsIcon.SetValue(Canvas.LeftProperty, 12 * (SystemParameters.PrimaryScreenWidth / 1920));

                RedCrossButton.SetValue(Canvas.TopProperty, 0 * (SystemParameters.PrimaryScreenHeight / 1080));
                RedCrossButton.Height = SystemParameters.PrimaryScreenHeight * 0.0463;
                RedCrossButton.Width = SystemParameters.PrimaryScreenWidth * 0.0260;
                RedCrossButton.SetValue(Canvas.LeftProperty, 1870 * (SystemParameters.PrimaryScreenWidth / 1920));

                RedCross.SetValue(Canvas.TopProperty, 0 * (SystemParameters.PrimaryScreenHeight / 1080));
                RedCross.Height = SystemParameters.PrimaryScreenHeight * 0.0463;
                RedCross.Width = SystemParameters.PrimaryScreenWidth * 0.0260;
                RedCross.SetValue(Canvas.LeftProperty, 1870 * (SystemParameters.PrimaryScreenWidth / 1920));
            }
        }
        void CalendarButtonClick(object sender, RoutedEventArgs e)
        {
            if (Properties.Settings.Default.MiniWindowOpened == false)
            {
                var newForm = new CalendarMenu();
                newForm.Show();
                this.Close();
            }

        }
        void ScreenTimeButtonClick(object sender, RoutedEventArgs e)
        {
            if (Properties.Settings.Default.MiniWindowOpened == false)
            {
                var newForm = new ScreentimeMenu();
                newForm.Show();
                this.Close();
            }
        }
        void PasswordButtonClick(object sender, RoutedEventArgs e)
        {
        }
        void HomeButtonClick(object sender, RoutedEventArgs e)
        {
            if (Properties.Settings.Default.MiniWindowOpened == false)
            {
                var newForm = new MainWindow();
                newForm.Show();
                this.Close();
            }
        }
        void RedCrossButtonClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        } 
        void PasswordCreationClick(object sender, RoutedEventArgs e)
        {
            string password = new System.Net.NetworkCredential(string.Empty, Properties.Settings.Default.Password).Password;
            if (Properties.Settings.Default.MiniWindowOpened == false & string.IsNullOrEmpty(password))
            {
                Properties.Settings.Default.MiniWindowOpened = true;
                var newForm = new PasswordCreation();
                newForm.Show();
            }
            if (Properties.Settings.Default.MiniWindowOpened == false & !string.IsNullOrEmpty(password))
            {
                Properties.Settings.Default.MiniWindowOpened = true;
                var newForm = new PasswordAlreadyCreated();
                newForm.Show();
            }
        }
        void ChangePasswordClick(object sender, RoutedEventArgs e)
        {
            string password = new System.Net.NetworkCredential(string.Empty, Properties.Settings.Default.Password).Password;
            if (Properties.Settings.Default.MiniWindowOpened == false & !string.IsNullOrEmpty(password))
            {
                Properties.Settings.Default.MiniWindowOpened = true;
                var newForm = new ChangePassword();
                newForm.Show();
            }
            if (Properties.Settings.Default.MiniWindowOpened == false & string.IsNullOrEmpty(password))
            {
                Properties.Settings.Default.MiniWindowOpened = true;
                var newForm = new NoPasswordCreated();
                newForm.Show();
            }
        }
        void ForgotPasswordClick(object sender, RoutedEventArgs e)
        {
            string password = new System.Net.NetworkCredential(string.Empty, Properties.Settings.Default.Password).Password;
            if (Properties.Settings.Default.MiniWindowOpened == false & !string.IsNullOrEmpty(password))
            {
                Properties.Settings.Default.MiniWindowOpened = true;
                var newForm = new ForgotPassword();
                newForm.Show();
            }
            if (Properties.Settings.Default.MiniWindowOpened == false & string.IsNullOrEmpty(password))
            {
                Properties.Settings.Default.MiniWindowOpened = true;
                var newForm = new NoPasswordCreated();
                newForm.Show();
            }
        }
        private void SettingsButtonClick(object sender, RoutedEventArgs e)
        {
            var newForm = new Settings();
            newForm.Show();
            this.Close();
        }
    }
}
