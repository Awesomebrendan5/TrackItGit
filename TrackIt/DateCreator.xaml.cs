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
            public string BlacklistName { get; set; } //Define the BlacklistName string.
            public string Applications { get; set; } //Define the Applications string.
        }
        public class NewBlacklists
        {
            public string EventName { get; set; } //Define the EventName string.
            public string BlacklistName { get; set; } //Define the BlacklistName string.
            public string Applications { get; set; } //Define the Applications string.

            public string DateRange { get; set; } //Define the DateRange String.
        }
        void Screenscale()
        {
            if (SystemParameters.PrimaryScreenHeight != 1080 | SystemParameters.PrimaryScreenWidth != 1920) //Check that the screen resolution is different to default.
            {
                MinHeight = SystemParameters.PrimaryScreenHeight * (740.0 / 1080.0); //Set MinHeight property.
                MinWidth = SystemParameters.PrimaryScreenWidth * (428.0 / 1920); //Set MinWidth property.

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
            EventLabel.Text = Properties.Settings.Default.DatePicked.ToShortDateString(); //Set the EventLabel to the DatePicked
        }
        private void ConfirmButtonClick(object sender, RoutedEventArgs e)
        {
            int hour, minute, second, hour1, minute1, second1; //Define the hour, hour1, minute, minute1, second and second1 integers.
            string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments); //Save the documents path.
            string directoryPath = Path.Combine(documentsPath, "TrackIt"); //Save TrackIt's directory path.
            string FilePath = Path.Combine(directoryPath, "BlacklistsCombined.csv"); //Save the file path.
            if (File.Exists(FilePath)) //Check if the file path exists.
            {
                Fileexists = true;
            }
            else
            {
                Fileexists = false;
            }
            if (Fileexists == false)
            {
                Directory.CreateDirectory(directoryPath); //Create the directory path.
            }
            if (int.TryParse(HourBox.Text, out hour) && int.TryParse(MinuteBox.Text, out minute) && int.TryParse(SecondBox.Text, out second) & int.TryParse(HourBox_Copy.Text, out hour1) && int.TryParse(MinuteBox_Copy.Text, out minute1) && int.TryParse(SecondBox_Copy.Text, out second1) & !String.IsNullOrEmpty(EventInput.Text)) //Check that the hours, minutes and seconds input by the users are integers and that the user has input an event name.
            {
                if (EventInput.Text.Length < 8) //Check that the event name is under 8 chacracters.
                {
                    if (hour * 60000 + minute * 3600 + second * 60 <= hour1 * 60000 + minute * 3600 + second * 60) //Check that the first timeset is earlier than the second timeset.
                    {
                        if (hour * 3600000 + minute * 60000 + second * 1000 <= 86400000 && hour1 * 3600000 + minute * 60000 + second * 1000 <= 86400000) //Check that neither timeset are greater than a day in length.
                        {
                            DateTime currentDate = Properties.Settings.Default.DatePicked.Date; //Set the currentDate to the DatePicked.
                            DateTime FirstDateTime = currentDate.AddHours(hour).AddMinutes(minute).AddSeconds(second); //Set the FirstDateTime to the CurrentDate added with the hours, minutes and seconds.
                            DateTime SecondDateTime = currentDate.AddHours(hour1).AddMinutes(minute1).AddSeconds(second1); //Set the SecondDateTime to the CurrentDate added with the hours, minutes and seconds.
                            string dateRange = FirstDateTime.ToString() + " - " + SecondDateTime.ToString(); //Creates a dateRange that is the first date separated from the second date. 
                            string NameofEvent = EventInput.Text; //Set NameofEvent to the user input event name.
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

                                                csvWriter.WriteRecord(newRecord); //Write the record into BlacklistsCombined.csv.
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

                                                csvWriter.WriteRecord(newRecord); //Write the record into BlacklistsCombined.csv.
                                                csvWriter.NextRecord();
                                            }
                                        }
                                        Fileexists = true;
                                    }
                                    var newForm = new EventCreated(); //Open the EventCreated window.
                                    newForm.Show();
                                    this.Close(); //Close the window.
                                }
                            }
                            if (!File.Exists(BlacklistsPath))
                            {
                                if (Properties.Settings.Default.MiniWindowOpened1 == false)
                                {
                                    Properties.Settings.Default.MiniWindowOpened1 = true; //Set MiniWindowOpened1 to true.
                                    var newForm = new BlacklistDoesNotExist(); //Open the BlacklistDoesNotExist window.
                                    newForm.Show();
                                    this.Close(); //Close the window.
                                }
                            }
                        }
                        else
                        {
                            if (Properties.Settings.Default.MiniWindowOpened1 == false)
                            {
                                Properties.Settings.Default.MiniWindowOpened1 = true; //Set MiniWindowOpened1 to true.
                                var newForm = new NextDay(); //Open the NextDay window.
                                newForm.Show();
                            }
                        }
                    }
                    if (hour * 60000 + minute * 3600 + second * 60! >= hour1 * 60000 + minute * 3600 + second * 60)
                    {
                        if (Properties.Settings.Default.MiniWindowOpened1 == false)
                        {
                            Properties.Settings.Default.MiniWindowOpened1 = true; //Set MiniWindowOpened1 to true.
                            var newForm = new FirstTimeLess(); //Open the FirstTimeLess window.
                            newForm.Show();
                        }
                    }
                }
            }
            if (EventInput.Text.Length >= 8)
            {
                if (Properties.Settings.Default.MiniWindowOpened1 == false)
                {
                    Properties.Settings.Default.MiniWindowOpened1 = true; //Set MiniWindowOpened1 to true.
                    var newForm = new EventTooLong(); //Open the EventTooLong window.
                    newForm.Show();
                }
            }
        }


        private void BackButtonClick(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.MiniWindowOpened1 = false; //Set MiniWindowOpened1 to true.
            Properties.Settings.Default.MiniWindowOpened = false; //Set MiniWindowOpened to true.
            this.Close(); //Close the window.
        }
        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            Properties.Settings.Default.MiniWindowOpened1 = false; //Set MiniWindowOpened1 to true.
            Properties.Settings.Default.MiniWindowOpened = false; //Set MiniWindowOpened to true.
            Properties.Settings.Default.Save();
        }
    }
}

