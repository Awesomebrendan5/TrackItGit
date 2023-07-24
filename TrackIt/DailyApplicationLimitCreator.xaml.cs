using CsvHelper.Configuration;
using CsvHelper;
using Microsoft.Win32;
using System.Globalization;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Collections.Generic;
using System;
using System.Windows.Input;
using System.Text.RegularExpressions;

namespace TrackIt
{
    /// <summary>
    /// Interaction logic for DailyApplicationLimitCreator.xaml
    /// </summary>
    public partial class DailyApplicationLimitCreator : Window
    {
        public bool Fileexists;
        public DailyApplicationLimitCreator()
        {
            InitializeComponent();
            ListofApplications();
            Screenscale();
        }
        public class ScreentimeLimits
        {
            public string ApplicationName { get; set; } //Define ApplicationName string.
            public long ScreenTimeLimit { get; set; } //Define ScreenTimeLimit Long.
        }

        void Screenscale()
        {
            if (SystemParameters.PrimaryScreenHeight != 1080) //Check that the screen resolution is different to default.
            {
                MinHeight = SystemParameters.PrimaryScreenHeight * (740.0 / 1080.0); //Set MinHeight property.
                MinWidth = SystemParameters.PrimaryScreenWidth * (428.0 / 1920); //Set MinWidth property.

                UseLimit.SetValue(Canvas.TopProperty, 10 * (SystemParameters.PrimaryScreenHeight / 1080));
                UseLimit.Height = SystemParameters.PrimaryScreenHeight * 0.0445;
                UseLimit.Width = SystemParameters.PrimaryScreenWidth * 0.2063;
                UseLimit.SetValue(Canvas.LeftProperty, 16 * (SystemParameters.PrimaryScreenWidth / 1920));
                UseLimit.FontSize = (30 * SystemParameters.PrimaryScreenHeight / 1080);

                SelectApplications.SetValue(Canvas.TopProperty, 70 * (SystemParameters.PrimaryScreenHeight / 1080));
                SelectApplications.Height = SystemParameters.PrimaryScreenHeight * 0.0306;
                SelectApplications.Width = SystemParameters.PrimaryScreenWidth * 0.0854;
                SelectApplications.SetValue(Canvas.LeftProperty, 132 * (SystemParameters.PrimaryScreenWidth / 1920));
                SelectApplications.FontSize = (20 * SystemParameters.PrimaryScreenHeight / 1080);

                Applications.SetValue(Canvas.TopProperty, 108 * (SystemParameters.PrimaryScreenHeight / 1080));
                Applications.Height = SystemParameters.PrimaryScreenHeight * 0.3;
                Applications.Width = SystemParameters.PrimaryScreenWidth * 0.1052;
                Applications.SetValue(Canvas.LeftProperty, 113 * (SystemParameters.PrimaryScreenWidth / 1920));
                Applications.FontSize = (20 * SystemParameters.PrimaryScreenHeight / 1080);

                SelectEventTime.SetValue(Canvas.TopProperty, 442 * (SystemParameters.PrimaryScreenHeight / 1080));
                SelectEventTime.Height = SystemParameters.PrimaryScreenHeight * 0.0445;
                SelectEventTime.Width = SystemParameters.PrimaryScreenWidth * 0.1339;
                SelectEventTime.SetValue(Canvas.LeftProperty, 78 * (SystemParameters.PrimaryScreenWidth / 1920));
                SelectEventTime.FontSize = (20 * SystemParameters.PrimaryScreenHeight / 1080);

                Hour.SetValue(Canvas.TopProperty, 493 * (SystemParameters.PrimaryScreenHeight / 1080));
                Hour.Height = SystemParameters.PrimaryScreenHeight * 0.0204;
                Hour.Width = SystemParameters.PrimaryScreenWidth * 0.0260;
                Hour.SetValue(Canvas.LeftProperty, 95 * (SystemParameters.PrimaryScreenWidth / 1920));
                Hour.FontSize = (20 * SystemParameters.PrimaryScreenHeight / 1080);

                Minute.SetValue(Canvas.TopProperty, 493 * (SystemParameters.PrimaryScreenHeight / 1080));
                Minute.Height = SystemParameters.PrimaryScreenHeight * 0.0204;
                Minute.Width = SystemParameters.PrimaryScreenWidth * 0.0359;
                Minute.SetValue(Canvas.LeftProperty, 171 * (SystemParameters.PrimaryScreenWidth / 1920));
                Minute.FontSize = (20 * SystemParameters.PrimaryScreenHeight / 1080);

                Second.SetValue(Canvas.TopProperty, 493 * (SystemParameters.PrimaryScreenHeight / 1080));
                Second.Height = SystemParameters.PrimaryScreenHeight * 0.0204;
                Second.Width = SystemParameters.PrimaryScreenWidth * 0.0359;
                Second.SetValue(Canvas.LeftProperty, 254 * (SystemParameters.PrimaryScreenWidth / 1920));
                Second.FontSize = (20 * SystemParameters.PrimaryScreenHeight / 1080);

                HourBox.SetValue(Canvas.TopProperty, 520 * (SystemParameters.PrimaryScreenHeight / 1080));
                HourBox.Height = SystemParameters.PrimaryScreenHeight * 0.0296;
                HourBox.Width = SystemParameters.PrimaryScreenWidth * 0.0260;
                HourBox.SetValue(Canvas.LeftProperty, 95 * (SystemParameters.PrimaryScreenWidth / 1920));

                MinuteBox.SetValue(Canvas.TopProperty, 520 * (SystemParameters.PrimaryScreenHeight / 1080));
                MinuteBox.Height = SystemParameters.PrimaryScreenHeight * 0.0296;
                MinuteBox.Width = SystemParameters.PrimaryScreenWidth * 0.0260;
                MinuteBox.SetValue(Canvas.LeftProperty, 180 * (SystemParameters.PrimaryScreenWidth / 1920));

                SecondBox.SetValue(Canvas.TopProperty, 520 * (SystemParameters.PrimaryScreenHeight / 1080));
                SecondBox.Height = SystemParameters.PrimaryScreenHeight * 0.0296;
                SecondBox.Width = SystemParameters.PrimaryScreenWidth * 0.0260;
                SecondBox.SetValue(Canvas.LeftProperty, 265 * (SystemParameters.PrimaryScreenWidth / 1920));

                Confirm.SetValue(Canvas.TopProperty, 578 * (SystemParameters.PrimaryScreenHeight / 1080));
                Confirm.Height = SystemParameters.PrimaryScreenHeight * 0.0398;
                Confirm.Width = SystemParameters.PrimaryScreenWidth * 0.0646;
                Confirm.SetValue(Canvas.LeftProperty, 144 * (SystemParameters.PrimaryScreenWidth / 1920));
                Confirm.FontSize = (20 * SystemParameters.PrimaryScreenHeight / 1080);

                Back.SetValue(Canvas.TopProperty, 665 * (SystemParameters.PrimaryScreenHeight / 1080));
                Back.Height = SystemParameters.PrimaryScreenHeight * (43.0 / 1080.0);
                Back.Width = SystemParameters.PrimaryScreenWidth * (124.0 / 1920.0);
                Back.SetValue(Canvas.LeftProperty, 0 * (SystemParameters.PrimaryScreenWidth / 1920));
                Back.FontSize = (20 * SystemParameters.PrimaryScreenHeight / 1080);

                Add_Another.SetValue(Canvas.TopProperty, 665 * (SystemParameters.PrimaryScreenHeight / 1080));
                Add_Another.Height = SystemParameters.PrimaryScreenHeight * (43.0 / 1080.0);
                Add_Another.Width = SystemParameters.PrimaryScreenWidth * (124.0 / 1920.0);
                Add_Another.SetValue(Canvas.LeftProperty, 294 * (SystemParameters.PrimaryScreenWidth / 1920));
                Add_Another.FontSize = (20 * SystemParameters.PrimaryScreenHeight / 1080);
            }
        }
        private void ConfirmButtonClick(object sender, RoutedEventArgs e)
        {
            int hour, minute, second; //Define the hour, minute and second integers.
            if (Applications.SelectedItem != null) //Check that the user has selected applications from the list.
                {
                if (int.TryParse(HourBox.Text, out hour) && int.TryParse(MinuteBox.Text, out minute) && int.TryParse(SecondBox.Text, out second)) //Check that the input text is an integer.
                {
                    if ((hour * 3600000 + minute * 60000 + second * 1000) <= 86400000) //Check that the screntime limit entered by the user is not greater than 1 day.
                    {
                        string ApplicationNamed = Applications.SelectedItem.ToString(); //Save the user input application name.
                        long SetLength = Convert.ToInt32(HourBox.Text) * 3600000 + Convert.ToInt32(MinuteBox.Text) * 60000 + Convert.ToInt32(SecondBox.Text) * 1000; //Set the length of the screentime limit.
                        string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments); //Save the documents path.
                        string directoryPath = Path.Combine(documentsPath, "TrackIt"); //Save TrackIt's directory path.
                        string FilePath = Path.Combine(directoryPath, "Limits.csv"); //Save the file path.
                        if (File.Exists(FilePath)) //Check if the file path exists.
                        {
                            Fileexists = true;
                        }
                        else
                        {
                            Fileexists = false;
                        }
                        var records = new List<ScreentimeLimits>()
                        {

                        new ScreentimeLimits { ApplicationName = ApplicationNamed, ScreenTimeLimit = SetLength}
                        };
                        if (Fileexists == false)
                        {
                            Directory.CreateDirectory(directoryPath); //Create the directory path.
                            using (var writer = new StreamWriter(FilePath))
                            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
                            {
                                csv.WriteRecords(records); //Write the record into Limit.csv.
                            }
                            Fileexists = true;
                        }
                        if (Fileexists == true)
                        {
                            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
                            {
                                HasHeaderRecord = false,
                            };
                            using (var stream = File.Open(FilePath, FileMode.Append))
                            using (var writer = new StreamWriter(stream))
                            using (var csv = new CsvWriter(writer, config))
                            {
                                csv.WriteRecords(records); //Write the record into Limit.csv.
                            }
                        }
                        Properties.Settings.Default.MiniWindowOpened1 = true; //Set MiniWindowOpened1 to true.
                        var newForm = new LimitSaved(); //Open the LimitSaved window.
                        newForm.Show();
                        this.Close(); //Close the window.
                    }
                    else
                    {
                        if (Properties.Settings.Default.MiniWindowOpened1 == false)
                        {
                            Properties.Settings.Default.MiniWindowOpened1 = true; //Set MiniWindowOpened1 to true.
                            var newForm = new LimitOver24Hours(); //Open LimitOver24Hours
                            newForm.Show();
                        }
                    }
                }
            }
        }
        private void BackButtonClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void Add_AnotherClick(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.MiniWindowOpened1 = true;
            var newForm = new AddAnotherLimit();
            newForm.Show();
            this.Close();
        }
        void ListofApplications()
        {
            string registry_key = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall";
            using Microsoft.Win32.RegistryKey key = Registry.LocalMachine.OpenSubKey(registry_key);
            foreach (string subkey_name in key.GetSubKeyNames())
            {
                using RegistryKey subkey = key.OpenSubKey(subkey_name);
                Applications.Items.Add(subkey.GetValue("DisplayName"));
            }
        }
        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            Properties.Settings.Default.MiniWindowOpened = false;
            Properties.Settings.Default.MiniWindowOpened1 = false;
            Properties.Settings.Default.Save();
        }

        void HourBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regexchecker = new Regex("[^0-9]+"); //Check that the text the user is inputting is a number.
            e.Handled = regexchecker.IsMatch(e.Text);
        }
        private void HourBox_Pasting(object sender, DataObjectPastingEventArgs e)
        {
            if (e.DataObject.GetDataPresent(typeof(string)))
            {
                string pastedText = (string)e.DataObject.GetData(typeof(string));
                if (!IsValidInteger(pastedText))
                {
                    e.CancelCommand(); //Stop the user from pasting.
                }
                bool IsValidInteger(string input)
                {
                    int number;
                    return int.TryParse(input, out number); //Allow the user to paste.
                }
            }

            else
            {
                e.CancelCommand(); //Stop the user from pasting.
            }
        }

        private void MinuteBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regexchecker = new Regex("[^0-9]+"); //Check that the text the user is inputting is a number.
            e.Handled = regexchecker.IsMatch(e.Text);
        }

        private void MinuteBox_Pasting(object sender, DataObjectPastingEventArgs e)
        {
            if (e.DataObject.GetDataPresent(typeof(string)))
            {
                string pastedText = (string)e.DataObject.GetData(typeof(string));
                if (!IsValidInteger(pastedText))
                {
                    e.CancelCommand(); //Stop the user from pasting.
                }
                bool IsValidInteger(string input)
                {
                    int number;
                    return int.TryParse(input, out number); //Allow the user to paste.
                }
            }

            else
            {
                e.CancelCommand(); //Stop the user from pasting.
            }
        }

        private void SecondBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regexchecker = new Regex("[^0-9]+"); //Check that the text the user is inputting is a number.
            e.Handled = regexchecker.IsMatch(e.Text); 
        }
        private void SecondBox_Pasting(object sender, DataObjectPastingEventArgs e)
        {
            if (e.DataObject.GetDataPresent(typeof(string)))
            {
                string pastedText = (string)e.DataObject.GetData(typeof(string));
                if (!IsValidInteger(pastedText))
                {
                    e.CancelCommand(); //Stop the user from pasting.
                }
                bool IsValidInteger(string input)
                {
                    int number;
                    return int.TryParse(input, out number); //Allow the user to paste.
                }
            }

            else
            {
                e.CancelCommand(); //Stop the user from pasting.
            }
        }
    }
}
