﻿using System.Windows;
using System.Windows.Controls;

namespace TrackIt
{
    /// <summary>
    /// Interaction logic for PasswordAlreadyCreated.xaml
    /// </summary>
    public partial class PasswordAlreadyCreated : Window
    {
        public PasswordAlreadyCreated()
        {
            InitializeComponent();
            Screenscale();
        }
        void Screenscale()
        {
            if (SystemParameters.PrimaryScreenHeight != 1080 | SystemParameters.PrimaryScreenWidth != 1920) //Check that the screen resolution is different to default.
            {
                MinHeight = SystemParameters.PrimaryScreenHeight * (282.0 / 1080.0); //Set MinHeight property.
                MinWidth = SystemParameters.PrimaryScreenWidth * (461.0 / 1920); //Set MinWidth property.

                ErrorMessage.SetValue(Canvas.TopProperty, 10 * (SystemParameters.PrimaryScreenHeight / 1080));
                ErrorMessage.Height = SystemParameters.PrimaryScreenHeight * 0.0444;
                ErrorMessage.Width = SystemParameters.PrimaryScreenWidth * 0.1339;
                ErrorMessage.SetValue(Canvas.LeftProperty, 102 * (SystemParameters.PrimaryScreenWidth / 1920));
                ErrorMessage.FontSize = (30 * SystemParameters.PrimaryScreenHeight / 1080);

                Information.SetValue(Canvas.TopProperty, 70 * (SystemParameters.PrimaryScreenHeight / 1080));
                Information.Height = SystemParameters.PrimaryScreenHeight * 0.0991 ;
                Information.Width = SystemParameters.PrimaryScreenWidth * 0.188021;
                Information.SetValue(Canvas.LeftProperty, 50 * (SystemParameters.PrimaryScreenWidth / 1920));
                Information.FontSize = (20 * SystemParameters.PrimaryScreenHeight / 1080);

                Exit.SetValue(Canvas.TopProperty, 182 * (SystemParameters.PrimaryScreenHeight / 1080));
                Exit.Height = SystemParameters.PrimaryScreenHeight * 0.05;
                Exit.Width = SystemParameters.PrimaryScreenWidth * 0.0729;
                Exit.SetValue(Canvas.LeftProperty, 160 * (SystemParameters.PrimaryScreenWidth / 1920));
                Exit.FontSize = (25 * SystemParameters.PrimaryScreenHeight / 1080);
            }
        }
        private void ExitButtonClick(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.MiniWindowOpened = false; //Set MiniWindowOpened to false.
            Properties.Settings.Default.Save();
            this.Close(); //Close the window.
        }
        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            Properties.Settings.Default.MiniWindowOpened = false; //Set MiniWindowOpened to false.
            Properties.Settings.Default.Save();
        }
    }
}
