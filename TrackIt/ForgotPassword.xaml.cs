using System.Windows;
using System.Windows.Controls;
using System.IO;
using System;

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
            if (SystemParameters.PrimaryScreenHeight != 1080) //Check that the screen resolution is different to default.
            {
                MinHeight = SystemParameters.PrimaryScreenHeight * (740.0 / 1080.0); //Set MinHeight property.
                MinWidth = SystemParameters.PrimaryScreenWidth * (428.0 / 1920); //Set MinWidth property.

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

                Back.SetValue(Canvas.TopProperty, 665 * (SystemParameters.PrimaryScreenHeight / 1080));
                Back.Height = SystemParameters.PrimaryScreenHeight * (43.0 / 1080.0);
                Back.Width = SystemParameters.PrimaryScreenWidth * (124.0 / 1920.0);
                Back.SetValue(Canvas.LeftProperty, 0 * (SystemParameters.PrimaryScreenWidth / 1920));
                Back.FontSize = (20 * SystemParameters.PrimaryScreenHeight / 1080);
            }
        }
        private void ConfirmButtonClick(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.Password = null; //Set the password to null.
            Properties.Settings.Default.Save();
            var newForm = new PasswordReset(); //Open the PasswordReset window.
            string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments); //Save the documents path.
            string directoryPath = System.IO.Path.Combine(documentsPath, "TrackIt"); //Save TrackIt's directory path.
            string FilePath = System.IO.Path.Combine(directoryPath, "Blacklists.csv"); //Save the file path.
            string FilePaths = System.IO.Path.Combine(directoryPath, "BlacklistsCombined.csv"); //Save the file path.
            string directoriesPath = System.IO.Path.Combine(documentsPath, "TrackIt"); //Save TrackIt's directory path.
            string FilesPath = System.IO.Path.Combine(directoriesPath, "ApplicationsNotToTrack.csv"); //Save the file path.
            string ApplicationdirectoryPath = System.IO.Path.Combine(documentsPath, "TrackIt"); //Save TrackIt's directory path.
            string ApplicationFilePath = System.IO.Path.Combine(ApplicationdirectoryPath, "Limits.csv"); //Save the file path.
            string directoryPaths = System.IO.Path.Combine(documentsPath, "TrackIt"); //Save TrackIt's directory path.
            string FilesPaths = System.IO.Path.Combine(directoryPaths, "ScreentimeData.csv"); //Save the file path.
            File.Delete(FilePath); //Delete the file path.
            File.Delete(FilesPaths); //Delete the file path.
            File.Delete(ApplicationFilePath); //Delete the file path.
            File.Delete(FilesPath);  //Delete the file path.
            File.Delete(FilePaths); //Delete the file path.
            newForm.Show();
            this.Close(); //Close the window.
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
