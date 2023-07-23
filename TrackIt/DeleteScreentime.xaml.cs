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
    /// Interaction logic for DeleteScreentime.xaml
    /// </summary>
    public partial class DeleteScreentime : Window
    {
        public DeleteScreentime()
        {
            InitializeComponent();
            Screenscale();
        }
        void Screenscale()
        {
            if (SystemParameters.PrimaryScreenHeight != 1080)
            {
                MinHeight = SystemParameters.PrimaryScreenHeight * (740.0 / 1080.0);
                MinWidth = SystemParameters.PrimaryScreenWidth * (428.0 / 1920);

                PasswordLabel.SetValue(Canvas.TopProperty, 10 * (SystemParameters.PrimaryScreenHeight / 1080));
                PasswordLabel.Height = SystemParameters.PrimaryScreenHeight * 0.0444;
                PasswordLabel.Width = SystemParameters.PrimaryScreenWidth * 0.16875;
                PasswordLabel.SetValue(Canvas.LeftProperty, 52 * (SystemParameters.PrimaryScreenWidth / 1920));
                PasswordLabel.FontSize = (25 * SystemParameters.PrimaryScreenHeight / 1080);

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

                Disclaimer.SetValue(Canvas.TopProperty, 422 * (SystemParameters.PrimaryScreenHeight / 1080));
                Disclaimer.Height = SystemParameters.PrimaryScreenHeight * 0.0546;
                Disclaimer.Width = SystemParameters.PrimaryScreenWidth * 0.1771;
                Disclaimer.SetValue(Canvas.LeftProperty, 44 * (SystemParameters.PrimaryScreenWidth / 1920));
                Disclaimer.FontSize = (20 * SystemParameters.PrimaryScreenHeight / 1080);

                Confirm.SetValue(Canvas.TopProperty, 514 * (SystemParameters.PrimaryScreenHeight / 1080));
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
            if (Properties.Settings.Default.Password == Password.Password)
            {
                string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                string directoryPath = System.IO.Path.Combine(documentsPath, "TrackIt");
                string FilePath = System.IO.Path.Combine(directoryPath, "ScreentimeData.csv");
                File.Delete(FilePath);
                var newForm = new ScreentimeDataRemoved();
                newForm.Show();
                this.Close();
            }
            if (Properties.Settings.Default.Password != Password.Password)
            {
                var newForm = new WrongPassword2();
                newForm.Show();
            }
        }
        private void BackButtonClick(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.MiniWindowOpened = false;
            this.Close();
        }
    }
}
