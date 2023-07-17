using CsvHelper;
using CsvHelper.Configuration;
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
                MinHeight = SystemParameters.PrimaryScreenHeight * (740.0 / 1080.0);
                MinWidth = SystemParameters.PrimaryScreenWidth * (428.0 / 1920);

                Box.SetValue(Canvas.TopProperty, 106 * (SystemParameters.PrimaryScreenHeight / 1080));
                Box.Height = SystemParameters.PrimaryScreenHeight * 0.3407;
                Box.Width = SystemParameters.PrimaryScreenWidth * 0.1229; 
                Box.SetValue(Canvas.LeftProperty, 96 * (SystemParameters.PrimaryScreenWidth / 1920));

                DateToday.SetValue(Canvas.TopProperty, 63 * (SystemParameters.PrimaryScreenHeight / 1080));
                DateToday.Height = SystemParameters.PrimaryScreenHeight * 0.0352;
                DateToday.Width = SystemParameters.PrimaryScreenWidth * 0.1344;
                DateToday.SetValue(Canvas.LeftProperty, 106 * (SystemParameters.PrimaryScreenWidth / 1920));
                DateToday.FontSize = (25 * SystemParameters.PrimaryScreenHeight / 1080);

                EventList.SetValue(Canvas.TopProperty, 122 * (SystemParameters.PrimaryScreenHeight / 1080));
                EventList.Height = SystemParameters.PrimaryScreenHeight * 0.2435;
                EventList.Width = SystemParameters.PrimaryScreenWidth * 0.1083;
                EventList.SetValue(Canvas.LeftProperty, 110 * (SystemParameters.PrimaryScreenWidth / 1920));
                EventList.FontSize = (20 * SystemParameters.PrimaryScreenHeight / 1080);

                CreateNewEvent.SetValue(Canvas.TopProperty, 418 * (SystemParameters.PrimaryScreenHeight / 1080));
                CreateNewEvent.Height = SystemParameters.PrimaryScreenHeight * 0.0398;
                CreateNewEvent.Width = SystemParameters.PrimaryScreenWidth * 0.0646;
                CreateNewEvent.SetValue(Canvas.LeftProperty, 155 * (SystemParameters.PrimaryScreenWidth / 1920));
                CreateNewEvent.FontSize = (20 * SystemParameters.PrimaryScreenHeight / 1080);

                Back.SetValue(Canvas.TopProperty, 665 * (SystemParameters.PrimaryScreenHeight / 1080));
                Back.Height = SystemParameters.PrimaryScreenHeight * (43.0 / 1080.0);
                Back.Width = SystemParameters.PrimaryScreenWidth * (124.0 / 1920.0);
                Back.SetValue(Canvas.LeftProperty, 0 * (SystemParameters.PrimaryScreenWidth / 1920));
                Back.FontSize = (20 * SystemParameters.PrimaryScreenHeight / 1080);
            }
        }
        void DateTitle()
        {
            DateToday.Text = Properties.Settings.Default.DatePicked.ToShortDateString() + "- Schedule";
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
            string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            string directoryPath = Path.Combine(documentsPath, "TrackIt");
            string FilePath = Path.Combine(directoryPath, "BlacklistsCombined.csv");
            if (File.Exists(FilePath))
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
                var config = new CsvConfiguration(CultureInfo.InvariantCulture)
                {
                    // Don't write the header again.
                    HasHeaderRecord = false,
                };
                using (var reader = new StreamReader(FilePath))
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

                        if (startTime.Date == Properties.Settings.Default.DatePicked)
                        {
                            var name = blacklist.EventName;
                            EventList.Items.Add(name + " " + startTime.TimeOfDay + " - " + endTime.TimeOfDay);
                        }
                    }
                }
            }
            if (!Fileexists)
            {
            }
        }
    }
}
