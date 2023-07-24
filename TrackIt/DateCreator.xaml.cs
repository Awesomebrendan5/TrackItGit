using System.Windows;
using System.Windows.Controls;
using System;
using CsvHelper;
using LiveChartsCore;
using static TrackIt.BlacklistCreation;
using System.Globalization;
using System.IO;
using CsvHelper.Configuration;
using CsvHelper;
using System.Formats.Asn1;

namespace TrackIt
{
    /// <summary>
    /// Interaction logic for DateCreator.xaml
    /// </summary>
    public partial class DateCreator : Window
    {
        public bool Fileexists;
        public DateCreator()
        {
            InitializeComponent();
            DateTitle();
            Screenscale();
        }
        public class Blacklists
        {
            public string BlacklistName { get; set; }
            public string Applications { get; set; }
        }
        public class NewBlacklists
        {
            public string EventName { get; set; }
            public string BlacklistName { get; set; }
            public string Applications { get; set; }

            public string DateRange { get; set; }
        }
        void Screenscale()
        {
            if (SystemParameters.PrimaryScreenHeight != 1080)
            {
                MinHeight = SystemParameters.PrimaryScreenHeight * (740.0 / 1080.0);
                MinWidth = SystemParameters.PrimaryScreenWidth * (428.0 / 1920);

                EventLabel.SetValue(Canvas.TopProperty, 10 * (SystemParameters.PrimaryScreenHeight / 1080));
                EventLabel.Height = SystemParameters.PrimaryScreenHeight * 0.0445;
                EventLabel.Width = SystemParameters.PrimaryScreenWidth * 0.1339;
                EventLabel.SetValue(Canvas.LeftProperty, 69 * (SystemParameters.PrimaryScreenWidth / 1920));
                EventLabel.FontSize = (30 * SystemParameters.PrimaryScreenHeight / 1080);

                EnterEvent.SetValue(Canvas.TopProperty, 86 * (SystemParameters.PrimaryScreenHeight / 1080));
                EnterEvent.Height = SystemParameters.PrimaryScreenHeight * 0.0445;
                EnterEvent.Width = SystemParameters.PrimaryScreenWidth * 0.1339;
                EnterEvent.SetValue(Canvas.LeftProperty, 69 * (SystemParameters.PrimaryScreenWidth / 1920));
                EnterEvent.FontSize = (20 * SystemParameters.PrimaryScreenHeight / 1080);

                EventInput.SetValue(Canvas.TopProperty, 120 * (SystemParameters.PrimaryScreenHeight / 1080));
                EventInput.Height = SystemParameters.PrimaryScreenHeight * 0.0445;
                EventInput.Width = SystemParameters.PrimaryScreenWidth * 0.1052;
                EventInput.SetValue(Canvas.LeftProperty, 96 * (SystemParameters.PrimaryScreenWidth / 1920));

                SelectEventTime.SetValue(Canvas.TopProperty, 246 * (SystemParameters.PrimaryScreenHeight / 1080));
                SelectEventTime.Height = SystemParameters.PrimaryScreenHeight * 0.0445;
                SelectEventTime.Width = SystemParameters.PrimaryScreenWidth * 0.1339;
                SelectEventTime.SetValue(Canvas.LeftProperty, 69 * (SystemParameters.PrimaryScreenWidth / 1920));
                SelectEventTime.FontSize = (20 * SystemParameters.PrimaryScreenHeight / 1080);

                Hour.SetValue(Canvas.TopProperty, 332 * (SystemParameters.PrimaryScreenHeight / 1080));
                Hour.Height = SystemParameters.PrimaryScreenHeight * 0.0204;
                Hour.Width = SystemParameters.PrimaryScreenWidth * 0.0260;
                Hour.SetValue(Canvas.LeftProperty, 87 * (SystemParameters.PrimaryScreenWidth / 1920));
                Hour.FontSize = (20 * SystemParameters.PrimaryScreenHeight / 1080);

                Minute.SetValue(Canvas.TopProperty, 332 * (SystemParameters.PrimaryScreenHeight / 1080));
                Minute.Height = SystemParameters.PrimaryScreenHeight * 0.0204;
                Minute.Width = SystemParameters.PrimaryScreenWidth * 0.0359;
                Minute.SetValue(Canvas.LeftProperty, 163 * (SystemParameters.PrimaryScreenWidth / 1920));
                Minute.FontSize = (20 * SystemParameters.PrimaryScreenHeight / 1080);

                Second.SetValue(Canvas.TopProperty, 332 * (SystemParameters.PrimaryScreenHeight / 1080));
                Second.Height = SystemParameters.PrimaryScreenHeight * 0.0204;
                Second.Width = SystemParameters.PrimaryScreenWidth * 0.0359;
                Second.SetValue(Canvas.LeftProperty, 246 * (SystemParameters.PrimaryScreenWidth / 1920));
                Second.FontSize = (20 * SystemParameters.PrimaryScreenHeight / 1080);

                HourBox.SetValue(Canvas.TopProperty, 359 * (SystemParameters.PrimaryScreenHeight / 1080));
                HourBox.Height = SystemParameters.PrimaryScreenHeight * 0.0296;
                HourBox.Width = SystemParameters.PrimaryScreenWidth * 0.0260;
                HourBox.SetValue(Canvas.LeftProperty, 87 * (SystemParameters.PrimaryScreenWidth / 1920));

                MinuteBox.SetValue(Canvas.TopProperty, 359 * (SystemParameters.PrimaryScreenHeight / 1080));
                MinuteBox.Height = SystemParameters.PrimaryScreenHeight * 0.0296;
                MinuteBox.Width = SystemParameters.PrimaryScreenWidth * 0.0260;
                MinuteBox.SetValue(Canvas.LeftProperty, 172 * (SystemParameters.PrimaryScreenWidth / 1920));

                SecondBox.SetValue(Canvas.TopProperty, 359 * (SystemParameters.PrimaryScreenHeight / 1080));
                SecondBox.Height = SystemParameters.PrimaryScreenHeight * 0.0296;
                SecondBox.Width = SystemParameters.PrimaryScreenWidth * 0.0260;
                SecondBox.SetValue(Canvas.LeftProperty, 257 * (SystemParameters.PrimaryScreenWidth / 1920));

                ToLabel.SetValue(Canvas.TopProperty, 414 * (SystemParameters.PrimaryScreenHeight / 1080));
                ToLabel.Height = SystemParameters.PrimaryScreenHeight * 0.025;
                ToLabel.Width = SystemParameters.PrimaryScreenWidth * 0.0116;
                ToLabel.SetValue(Canvas.LeftProperty, 186 * (SystemParameters.PrimaryScreenWidth / 1920));
                ToLabel.FontSize = (20 * SystemParameters.PrimaryScreenHeight / 1080);

                Hour_Copy.SetValue(Canvas.TopProperty, 463 * (SystemParameters.PrimaryScreenHeight / 1080));
                Hour_Copy.Height = SystemParameters.PrimaryScreenHeight * 0.0204;
                Hour_Copy.Width = SystemParameters.PrimaryScreenWidth * 0.0260;
                Hour_Copy.SetValue(Canvas.LeftProperty, 87 * (SystemParameters.PrimaryScreenWidth / 1920));
                Hour_Copy.FontSize = (20 * SystemParameters.PrimaryScreenHeight / 1080);

                Minute_Copy.SetValue(Canvas.TopProperty, 463 * (SystemParameters.PrimaryScreenHeight / 1080));
                Minute_Copy.Height = SystemParameters.PrimaryScreenHeight * 0.0204;
                Minute_Copy.Width = SystemParameters.PrimaryScreenWidth * 0.0359;
                Minute_Copy.SetValue(Canvas.LeftProperty, 163 * (SystemParameters.PrimaryScreenWidth / 1920));
                Minute_Copy.FontSize = (20 * SystemParameters.PrimaryScreenHeight / 1080);

                Second_Copy.SetValue(Canvas.TopProperty, 463 * (SystemParameters.PrimaryScreenHeight / 1080));
                Second_Copy.Height = SystemParameters.PrimaryScreenHeight * 0.0204;
                Second_Copy.Width = SystemParameters.PrimaryScreenWidth * 0.0359;
                Second_Copy.SetValue(Canvas.LeftProperty, 246 * (SystemParameters.PrimaryScreenWidth / 1920));
                Second_Copy.FontSize = (20 * SystemParameters.PrimaryScreenHeight / 1080);

                HourBox_Copy.SetValue(Canvas.TopProperty, 490 * (SystemParameters.PrimaryScreenHeight / 1080));
                HourBox_Copy.Height = SystemParameters.PrimaryScreenHeight * 0.0296;
                HourBox_Copy.Width = SystemParameters.PrimaryScreenWidth * 0.0260;
                HourBox_Copy.SetValue(Canvas.LeftProperty, 87 * (SystemParameters.PrimaryScreenWidth / 1920));

                MinuteBox_Copy.SetValue(Canvas.TopProperty, 490 * (SystemParameters.PrimaryScreenHeight / 1080));
                MinuteBox_Copy.Height = SystemParameters.PrimaryScreenHeight * 0.0296;
                MinuteBox_Copy.Width = SystemParameters.PrimaryScreenWidth * 0.0260;
                MinuteBox_Copy.SetValue(Canvas.LeftProperty, 172 * (SystemParameters.PrimaryScreenWidth / 1920));

                SecondBox_Copy.SetValue(Canvas.TopProperty, 490 * (SystemParameters.PrimaryScreenHeight / 1080));
                SecondBox_Copy.Height = SystemParameters.PrimaryScreenHeight * 0.0296;
                SecondBox_Copy.Width = SystemParameters.PrimaryScreenWidth * 0.0260;
                SecondBox_Copy.SetValue(Canvas.LeftProperty, 257 * (SystemParameters.PrimaryScreenWidth / 1920));

                Confirm.SetValue(Canvas.TopProperty, 581 * (SystemParameters.PrimaryScreenHeight / 1080));
                Confirm.Height = SystemParameters.PrimaryScreenHeight * 0.0398;
                Confirm.Width = SystemParameters.PrimaryScreenWidth * 0.0646;
                Confirm.SetValue(Canvas.LeftProperty, 135 * (SystemParameters.PrimaryScreenWidth / 1920));
                Confirm.FontSize = (20 * SystemParameters.PrimaryScreenHeight / 1080);

                Back.SetValue(Canvas.TopProperty, 665 * (SystemParameters.PrimaryScreenHeight / 1080));
                Back.Height = SystemParameters.PrimaryScreenHeight * (43.0 / 1080.0);
                Back.Width = SystemParameters.PrimaryScreenWidth * (124.0 / 1920.0);
                Back.SetValue(Canvas.LeftProperty, 0 * (SystemParameters.PrimaryScreenWidth / 1920));
                Back.FontSize = (20 * SystemParameters.PrimaryScreenHeight / 1080);
            }
        }
        void DateTitle()
        {
            EventLabel.Text = Properties.Settings.Default.DatePicked.ToShortDateString();
        }
        private void ConfirmButtonClick(object sender, RoutedEventArgs e)
        {
            int hour, minute, second, hour1, minute1, second1;
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
            if (Fileexists == false)
            {
                Directory.CreateDirectory(directoryPath);
            }
            if (int.TryParse(HourBox.Text, out hour) && int.TryParse(MinuteBox.Text, out minute) && int.TryParse(SecondBox.Text, out second) & int.TryParse(HourBox_Copy.Text, out hour1) && int.TryParse(MinuteBox_Copy.Text, out minute1) && int.TryParse(SecondBox_Copy.Text, out second1) & !String.IsNullOrEmpty(EventInput.Text))
            {
                if (EventInput.Text.Length < 8)
                {
                    if (hour * 60000 + minute * 3600 + second * 60 <= hour1 * 60000 + minute * 3600 + second * 60)
                    {
                        if (hour * 3600000 + minute * 60000 + second * 1000 <= 86400000 && hour1 * 3600000 + minute * 60000 + second * 1000 <= 86400000)
                        {
                            DateTime currentDate = Properties.Settings.Default.DatePicked.Date;
                            DateTime FirstDateTime = currentDate.AddHours(hour).AddMinutes(minute).AddSeconds(second);
                            DateTime SecondDateTime = currentDate.AddHours(hour1).AddMinutes(minute1).AddSeconds(second1);
                            string dateRange = FirstDateTime.ToString() + " - " + SecondDateTime.ToString();
                            string NameofEvent = EventInput.Text;
                            string documented = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                            string directoriesPath = Path.Combine(documentsPath, "TrackIt");
                            string BlacklistsPath = Path.Combine(directoryPath, "Blacklists.csv");
                            if (File.Exists(BlacklistsPath))
                            {


                                using (var reader = new StreamReader(BlacklistsPath))
                                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                                {
                                    if (File.Exists(FilePath))
                                    {
                                        Fileexists = true;
                                    }
                                    else
                                    {
                                        Fileexists = false;
                                    }
                                    var records = csv.GetRecords<Blacklists>();
                                    if (Fileexists == true)
                                    {
                                        var config = new CsvConfiguration(CultureInfo.InvariantCulture)
                                        {
                                            HasHeaderRecord = false,
                                        };
                                        using (var stream = File.Open(FilePath, FileMode.Append))
                                        using (var writer = new StreamWriter(stream))
                                        using (var csvWriter = new CsvWriter(writer, config))
                                        {
                                            foreach (var record in records)
                                            {
                                                var newRecord = new NewBlacklists
                                                {
                                                    BlacklistName = record.BlacklistName,
                                                    EventName = NameofEvent,
                                                    Applications = record.Applications,
                                                    DateRange = dateRange
                                                };

                                                csvWriter.WriteRecord(newRecord);
                                                csvWriter.NextRecord();
                                            }
                                        }
                                    }
                                    if (Fileexists == false)
                                    {
                                        using (var writer = new StreamWriter(FilePath))
                                        using (var csvWriter = new CsvWriter(writer, CultureInfo.InvariantCulture))
                                        {
                                            csvWriter.WriteHeader<NewBlacklists>();
                                            csvWriter.NextRecord();
                                            foreach (var record in records)
                                            {
                                                var newRecord = new NewBlacklists
                                                {
                                                    BlacklistName = record.BlacklistName,
                                                    EventName = NameofEvent,
                                                    Applications = record.Applications,
                                                    DateRange = dateRange
                                                };

                                                csvWriter.WriteRecord(newRecord);
                                                csvWriter.NextRecord();
                                            }
                                        }
                                        Fileexists = true;
                                    }
                                    var newForm = new EventCreated();
                                    newForm.Show();
                                    this.Close();
                                }
                            }
                            if (!File.Exists(BlacklistsPath))
                            {

                                var newForm = new BlacklistDoesNotExist();
                                newForm.Show();
                                this.Close();
                            }
                        }
                    }
                    if (hour * 3600000 + minute * 60000 + second * 1000 !<= 86400000 | hour1 * 3600000 + minute * 60000 + second * 1000 !<= 86400000)
                    {

                    } 
                        
                }
                if (hour * 60000 + minute * 3600 + second * 60 !<= hour1 * 60000 + minute * 3600 + second * 60)
                {

                }
            }
            if (EventInput.Text.Length <= 8)
            {

            }
        }


        private void BackButtonClick(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.MiniWindowOpened1 = false;
            Properties.Settings.Default.MiniWindowOpened = false;
            this.Close();
        }
        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            Properties.Settings.Default.MiniWindowOpened1 = false;
            Properties.Settings.Default.MiniWindowOpened = false;
            Properties.Settings.Default.Save();
        }
    }
}

