﻿using System;
using System.IO;
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
    /// Interaction logic for DeleteBlacklists.xaml
    /// </summary>
    public partial class DeleteBlacklists : Window
    {
        public DeleteBlacklists()
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
                PasswordLabel.Height = SystemParameters.PrimaryScreenHeight * 0.0444;
                PasswordLabel.Width = SystemParameters.PrimaryScreenWidth * 0.1688;
                PasswordLabel.SetValue(Canvas.LeftProperty, 52 * (SystemParameters.PrimaryScreenWidth / 1920));
                PasswordLabel.FontSize = (30 * SystemParameters.PrimaryScreenHeight / 1080);

                EnterPasswordInfo.SetValue(Canvas.TopProperty, 101 * (SystemParameters.PrimaryScreenHeight / 1080));
                EnterPasswordInfo.Height = SystemParameters.PrimaryScreenHeight * 0.0287;
                EnterPasswordInfo.Width = SystemParameters.PrimaryScreenWidth * 0.0917;
                EnterPasswordInfo.SetValue(Canvas.LeftProperty, 126 * (SystemParameters.PrimaryScreenWidth / 1920));
                EnterPasswordInfo.FontSize = (20 * SystemParameters.PrimaryScreenHeight / 1080);

                Password.SetValue(Canvas.TopProperty, 137 * (SystemParameters.PrimaryScreenHeight / 1080));
                Password.Height = SystemParameters.PrimaryScreenHeight * 0.0417;
                Password.Width = SystemParameters.PrimaryScreenWidth * 0.1052;
                Password.SetValue(Canvas.LeftProperty, 113 * (SystemParameters.PrimaryScreenWidth / 1920));

                Warning.SetValue(Canvas.TopProperty, 362 * (SystemParameters.PrimaryScreenHeight / 1080));
                Warning.Height = SystemParameters.PrimaryScreenHeight * 0.0370;
                Warning.Width = SystemParameters.PrimaryScreenWidth * 0.0917;
                Warning.SetValue(Canvas.LeftProperty, 126 * (SystemParameters.PrimaryScreenWidth / 1920));
                Warning.FontSize = (30 * SystemParameters.PrimaryScreenHeight / 1080);

                Disclaimer.SetValue(Canvas.TopProperty, 417 * (SystemParameters.PrimaryScreenHeight / 1080));
                Disclaimer.Height = SystemParameters.PrimaryScreenHeight * 0.0593;
                Disclaimer.Width = SystemParameters.PrimaryScreenWidth * 0.1771;
                Disclaimer.SetValue(Canvas.LeftProperty, 44 * (SystemParameters.PrimaryScreenWidth / 1920));
                Disclaimer.FontSize = (20 * SystemParameters.PrimaryScreenHeight / 1080);

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
            if (Properties.Settings.Default.Password == Password.Password) //Check that the input password is correct.
            {
                string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments); //Save the documents path.
                string directoryPath = System.IO.Path.Combine(documentsPath, "TrackIt"); //Save TrackIt's directory path.
                string FilePath = System.IO.Path.Combine(directoryPath, "Blacklists.csv"); //Save the file path.
                string FilePaths = System.IO.Path.Combine(directoryPath, "BlacklistsCombined.csv"); //Save the file path.
                File.Delete(FilePath); //Delete the file path.
                File.Delete(FilePaths); //Delete the file path.
                var newForm = new BlacklistsRemoved(); //Open the BlacklistsRemoved window.
                newForm.Show();
                this.Close(); //Close the window.
            }
            if (Properties.Settings.Default.Password != Password.Password) //Check that the input password is incorrect.
            {
                if (Properties.Settings.Default.MiniWindowOpened1 == false)
                {
                    Properties.Settings.Default.MiniWindowOpened1 = true; //Set MiniWindowOpened1 to true.
                    var newForm = new WrongPassword2(); //Open the WrongPassword2 window.
                    newForm.Show();
                }
            }
        }
        private void BackButtonClick(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.MiniWindowOpened = false; //Set MiniWindowOpened to false.
            this.Close(); //Close the window.
        }
    }
}
