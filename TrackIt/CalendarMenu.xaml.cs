﻿using CsvHelper;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using static System.Net.Mime.MediaTypeNames;
using static TheTracker.TheTrackerService;

namespace TrackIt
{
    /// <summary>
    /// Interaction logic for Calendar.xaml
    /// </summary>
    public partial class CalendarMenu : Window
    {
        bool Fileexists;
        public class BlacklistsRecords
        {
            public string EventName { get; set; }
            public string BlacklistName { get; set; }
            public string Applications { get; set; }

            public string DateRange { get; set; }
        }
        public CalendarMenu()
        {
            InitializeComponent();
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            WindowState = WindowState.Maximized;
            ListofBlacklists();
            /*if (SystemParameters.PrimaryScreenHeight != 1080)
            {
                /*Line.Height = SystemParameters.PrimaryScreenHeight * 1;
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
                TrackIt.Width = SystemParameters.PrimaryScreenWidth * 0.0802;
                TrackIt.FontSize = (40 * SystemParameters.PrimaryScreenHeight / 1080);
                TrackIt.SetValue(Canvas.LeftProperty, 95.0 * (SystemParameters.PrimaryScreenWidth / 1920));

                CalendarContainer.SetValue(Canvas.TopProperty, 145 * (SystemParameters.PrimaryScreenHeight / 1080));
                CalendarContainer.Height = SystemParameters.PrimaryScreenHeight * 0.8287;
                CalendarContainer.Width = SystemParameters.PrimaryScreenWidth * 0.5943;
                CalendarContainer.SetValue(Canvas.LeftProperty, 396 * (SystemParameters.PrimaryScreenWidth / 1920));
                CalendarContainer.StretchDirection = StretchDirection.Both;
                CalendarContainer.Stretch = Stretch.Fill;
                CalendarContainer.MaxWidth = 1000 * (SystemParameters.PrimaryScreenWidth / 1920);
                CalendarContainer.MaxHeight = 1000 * (SystemParameters.PrimaryScreenHeight / 1080);

                CalendarLabel.SetValue(Canvas.TopProperty, 75 * (SystemParameters.PrimaryScreenHeight / 1080));
                CalendarLabel.Height = SystemParameters.PrimaryScreenHeight * 0.0565;
                CalendarLabel.Width = SystemParameters.PrimaryScreenWidth * 0.1063;
                CalendarLabel.FontSize = (50 * SystemParameters.PrimaryScreenHeight / 1080);
                CalendarLabel.SetValue(Canvas.LeftProperty, 771 * (SystemParameters.PrimaryScreenWidth / 1920));

                BlacklistBox.SetValue(Canvas.TopProperty, 235 * (SystemParameters.PrimaryScreenHeight / 1080));
                BlacklistBox.Height = SystemParameters.PrimaryScreenHeight * 0.6991;
                BlacklistBox.Width = SystemParameters.PrimaryScreenWidth * 0.1417;
                BlacklistBox.SetValue(Canvas.LeftProperty, 1603 * (SystemParameters.PrimaryScreenWidth / 1920));

                CreateBlacklist.SetValue(Canvas.TopProperty, 891 * (SystemParameters.PrimaryScreenHeight / 1080));
                CreateBlacklist.Height = SystemParameters.PrimaryScreenHeight * 0.0481;
                CreateBlacklist.Width = SystemParameters.PrimaryScreenWidth * 0.099;
                CreateBlacklist.SetValue(Canvas.LeftProperty, 1644 * (SystemParameters.PrimaryScreenWidth / 1920));

                BlacklistLabel.SetValue(Canvas.TopProperty, 157 * (SystemParameters.PrimaryScreenHeight / 1080));
                BlacklistLabel.Height = SystemParameters.PrimaryScreenHeight * 0.0565;
                BlacklistLabel.Width = SystemParameters.PrimaryScreenWidth * 0.1063;
                BlacklistLabel.FontSize = (50 * SystemParameters.PrimaryScreenHeight / 1080);
                BlacklistLabel.SetValue(Canvas.LeftProperty, 1636 * (SystemParameters.PrimaryScreenWidth / 1920));

            }*/
        }
        void CalendarButtonClick(object sender, RoutedEventArgs e)
        {
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
            if (Properties.Settings.Default.MiniWindowOpened == false)
            {
                var newForm = new PasswordMenu();
                newForm.Show();
                this.Close();
            }
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
        void CreateBlacklistClick(object sender, RoutedEventArgs e)
        {
            if (Properties.Settings.Default.MiniWindowOpened == false)
            {
             var newForm = new BlacklistCreation();
             Properties.Settings.Default.MiniWindowOpened = true;
             newForm.Show();
            }
        }

            void Calendar_SelectedDatesChanged(object sender,SelectionChangedEventArgs e)
                {
                    var calendar = sender as System.Windows.Controls.Calendar;
                    Properties.Settings.Default.DatePicked = calendar.SelectedDate.Value;
                    
                    if (calendar.SelectedDate.HasValue)
                    {
                        DateTime dt = calendar.SelectedDate.Value;
                        Properties.Settings.Default.DatePicked = dt;
                        if (Properties.Settings.Default.MiniWindowOpened == false)
                        {
                            Properties.Settings.Default.MiniWindowOpened = true;
                            var newForm = new Scheduler();
                            newForm.Show();
                        }
                    }
                 }
        void ListofBlacklists()
        {
            string filePath = "C:\\Users\\brend\\source\\repos\\TrackIt\\TrackIt\\BlacklistsCombined.csv";
            if (File.Exists(filePath))
            {
                Fileexists = true;
            }
            else
            {
                Fileexists = false;
            }
            if (Fileexists == true)
            {
                var BlacklistRecords = new Dictionary<string, long>();
                using (var reader = new StreamReader("C:\\Users\\brend\\source\\repos\\TrackIt\\TrackIt\\BlacklistsCombined.csv"))
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    var Blacklists = csv.GetRecords<BlacklistsRecords>();
                    foreach (var blacklist in Blacklists)
                    {
                        var name = blacklist.BlacklistName;
                        BlacklistList.Items.Add(name);
                    }
                }
            }
        }
            
    }
}

