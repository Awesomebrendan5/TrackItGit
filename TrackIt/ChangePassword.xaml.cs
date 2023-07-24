using System.Windows;
using System.Windows.Controls;

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
            if (SystemParameters.PrimaryScreenHeight != 1080) //Check that the screen resolution is different to default.
            {
                MinHeight = SystemParameters.PrimaryScreenHeight * (740.0 / 1080.0); //Set MinHeight property.
                MinWidth = SystemParameters.PrimaryScreenWidth * (428.0 / 1920); //Set MinWidth property.

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

                Back.SetValue(Canvas.TopProperty, 665 * (SystemParameters.PrimaryScreenHeight / 1080));
                Back.Height = SystemParameters.PrimaryScreenHeight * (43.0 / 1080.0);
                Back.Width = SystemParameters.PrimaryScreenWidth * (124.0 / 1920.0);
                Back.SetValue(Canvas.LeftProperty, 0 * (SystemParameters.PrimaryScreenWidth / 1920));
                Back.FontSize = (20 * SystemParameters.PrimaryScreenHeight / 1080);
            }
        }
        private void ConfirmButtonClick(object sender, RoutedEventArgs e)
        {
            if (Properties.Settings.Default.Password == OldPassword.Password) //Check if the user input the correct password.
            {
                if (Properties.Settings.Default.Password == NewPassword.Password) //Check if the user entered their old password.
                {
                    var newForm = new PasswordSame(); //Open the PasswordSame window.
                    newForm.Show();
                }
                if (NewPassword.Password != Properties.Settings.Default.Password) //Check if the user did not enter their old pasword.
                {
                    if (NewPassword.Password == NewPasswordConfirmation.Password & NewPassword.Password.Length > 7) //Check that the new password is equal to the confirmed password and it is greater than 7 characters.
                    {
                        Properties.Settings.Default.Password = NewPassword.Password; //Store the NewPassword.
                        Properties.Settings.Default.Save();
                        var newForm = new PasswordSaved(); //Open the PasswordSaved window.
                        newForm.Show();
                        this.Close(); //Close the window.
                    }
                    if (NewPassword.Password != NewPasswordConfirmation.Password & NewPassword.Password.Length > 7) //Check that the new password is not equal to the confirmed password and it is greater than 7 characters.
                    {
                        if (Properties.Settings.Default.MiniWindowOpened1 == false) //Check that MiniWindowOpened1 is false.
                        {
                            Properties.Settings.Default.MiniWindowOpened1 = true; //Set MiniWindowOpened1 to true.
                            var newForm = new PasswordMismatch(); //Open the PasswordMismatch window.
                            newForm.Show();
                        }
                    }
                    if (NewPassword.Password == NewPasswordConfirmation.Password & NewPassword.Password.Length <= 7) //Check that the new password is equal to the confirmed password and it is less than or equal to 7 characters.
                    {
                        if (Properties.Settings.Default.MiniWindowOpened1 == false) //Check that MiniWindowOpened1 is false.
                        {
                            Properties.Settings.Default.MiniWindowOpened1 = true; //Set MiniWindowOpened1 to true.
                            var newForm = new PasswordTooShort(); //Open the PasswordTooShort window.
                            newForm.Show();
                        }
                    }
                    if (NewPassword.Password != NewPasswordConfirmation.Password & NewPassword.Password.Length <= 7) //Check that the new password is not equal to the confirmed password and it is less than or equal to 7 characters.
                    {
                        if (Properties.Settings.Default.MiniWindowOpened1 == false) //Check that MiniWindowOpened1 is false.
                        {
                            Properties.Settings.Default.MiniWindowOpened1 = true; //Set MiniWindowOpened1 to true.
                            var newForm = new PasswordDoubleMismatch(); //Open the PasswordDoubleMismatch window.
                            newForm.Show();
                        }
                    }
                }
            }
            else
            {
                if (Properties.Settings.Default.MiniWindowOpened1 == false) //Check that MiniWindowOpened1 is false.
                {
                    Properties.Settings.Default.MiniWindowOpened1 = true; //Set MiniWindowOpened1 to true.
                    var newForm = new WrongPassword(); //Open the WrongPassword window.
                    newForm.Show();
                }
            }
        }

        private void BackButtonClick(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.MiniWindowOpened = false; //Set MiniWindowOpened to false.
            this.Close(); //Close the window.
        }
        protected override void OnClosing(System.ComponentModel.CancelEventArgs e) 
        {
            Properties.Settings.Default.MiniWindowOpened = false; //Set MiniWindowOpened to false.
            Properties.Settings.Default.Save();
        }
    }
}
