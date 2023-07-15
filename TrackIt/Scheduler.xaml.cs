﻿using CsvHelper;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace TrackIt
{
    /// <summary>
    /// Interaction logic for EventCreator.xaml
    /// </summary>
    public partial class Scheduler : Window
    {
        bool Fileexists;
        public class NewBlacklists
        {
            public string EventName { get; set; }
            public string BlacklistName { get; set; }
            public string Applications { get; set; }

            public string DateRange { get; set; }
        }
        public Scheduler()
        {
            InitializeComponent();
            DateTitle();
            Screenscale();
            ListofEvents();
        }
        void Screenscale()
        {
            if (SystemParameters.PrimaryScreenHeight != 1080)
            {
                Height = SystemParameters.PrimaryScreenHeight * 0.6667;
                Width = SystemParameters.PrimaryScreenWidth * 0.2229;

                Box.SetValue(Canvas.TopProperty, 106 * (SystemParameters.PrimaryScreenHeight / 1080));
                Box.Height = SystemParameters.PrimaryScreenHeight * 0.3407;
                Box.Width = SystemParameters.PrimaryScreenWidth * 0.1229; 
                Box.SetValue(Canvas.LeftProperty, 96 * (SystemParameters.PrimaryScreenWidth / 1920));

                DateToday.SetValue(Canvas.TopProperty, 10 * (SystemParameters.PrimaryScreenHeight / 1080));
                DateToday.Height = SystemParameters.PrimaryScreenHeight * 0.0445;
                DateToday.Width = SystemParameters.PrimaryScreenWidth * 0.0828;
                DateToday.SetValue(Canvas.LeftProperty, 80 * (SystemParameters.PrimaryScreenWidth / 1920));
                DateToday.FontSize = (30 * SystemParameters.PrimaryScreenHeight / 1080);

                Schedule.SetValue(Canvas.TopProperty, 10 * (SystemParameters.PrimaryScreenHeight / 1080));
                Schedule.Height = SystemParameters.PrimaryScreenHeight * 0.0445;
                Schedule.Width = SystemParameters.PrimaryScreenWidth * 0.0677;
                Schedule.SetValue(Canvas.LeftProperty, 214 * (SystemParameters.PrimaryScreenWidth / 1920));
                Schedule.FontSize = (30 * SystemParameters.PrimaryScreenHeight / 1080);

                CreateNewEvent.SetValue(Canvas.TopProperty, 418 * (SystemParameters.PrimaryScreenHeight / 1080));
                CreateNewEvent.Height = SystemParameters.PrimaryScreenHeight * 0.0398;
                CreateNewEvent.Width = SystemParameters.PrimaryScreenWidth * 0.0646;
                CreateNewEvent.SetValue(Canvas.LeftProperty, 155 * (SystemParameters.PrimaryScreenWidth / 1920));
                CreateNewEvent.FontSize = (20 * SystemParameters.PrimaryScreenHeight / 1080);

                Back.SetValue(Canvas.TopProperty, 651 * (SystemParameters.PrimaryScreenHeight / 1080));
                Back.Height = SystemParameters.PrimaryScreenHeight * 0.0398;
                Back.Width = SystemParameters.PrimaryScreenWidth * 0.0646;
                Back.SetValue(Canvas.LeftProperty, -4 * (SystemParameters.PrimaryScreenWidth / 1920));
                Back.FontSize = (20 * SystemParameters.PrimaryScreenHeight / 1080);
            }
        }
        void DateTitle()
        {
            DateToday.Text = Properties.Settings.Default.DatePicked.ToShortDateString();
        }
        private void NewEventClick(object sender, RoutedEventArgs e)
        {
            var newForm = new DateCreator();
            newForm.Show();
            this.Close();
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
        void ListofEvents()
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
                    var Blacklists = csv.GetRecords<NewBlacklists>();
                    foreach (var blacklist in Blacklists)
                    {
                        var dateRange = blacklist.DateRange.Split('-');
                        if (dateRange.Length != 2)
                        {
                            continue;
                        }

                        var startTime = DateTime.Parse(dateRange[0].Trim());
                        var endTime = DateTime.Parse(dateRange[1].Trim());
                        foreach (var events in Blacklists)
                        {
                            if (startTime.Date == Properties.Settings.Default.DatePicked)
                            {
                                var name = blacklist.EventName;
                                EventList.Items.Add(name + " " + startTime.TimeOfDay + " - " + endTime.TimeOfDay);
                            }
                        }
                    }
                }
            }
        }
    }
}
