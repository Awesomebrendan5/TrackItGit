using CsvHelper.Configuration;
using CsvHelper;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using static System.Net.Mime.MediaTypeNames;

namespace TrackIt
{
    /// <summary>
    /// Interaction logic for AddAnotherLimit.xaml
    /// </summary>
    public partial class AddAnotherLimit : Window
    {
        public bool Fileexists;
        public AddAnotherLimit()
        {
            InitializeComponent();
            Screenscale();
        }
        public class ScreentimeLimits
        {
            public string ApplicationName { get; set; }
            public long ScreenTimeLimit { get; set; }
        }
        void Screenscale()
        {
            /*if (SystemParameters.PrimaryScreenHeight != 1080)
            {
                Height = SystemParameters.PrimaryScreenHeight * 0.6667;
                Width = SystemParameters.PrimaryScreenWidth * 0.2229;

                UseLimit.SetValue(Canvas.TopProperty, 10 * (SystemParameters.PrimaryScreenHeight / 1080));
                UseLimit.Height = SystemParameters.PrimaryScreenHeight * 0.0445;
                UseLimit.Width = SystemParameters.PrimaryScreenWidth * 0.2063;
                UseLimit.SetValue(Canvas.LeftProperty, 16 * (SystemParameters.PrimaryScreenWidth / 1920));
                UseLimit.FontSize = (30 * SystemParameters.PrimaryScreenHeight / 1080);

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

                Back.SetValue(Canvas.TopProperty, 651 * (SystemParameters.PrimaryScreenHeight / 1080));
                Back.Height = SystemParameters.PrimaryScreenHeight * 0.0398;
                Back.Width = SystemParameters.PrimaryScreenWidth * 0.0646;
                Back.SetValue(Canvas.LeftProperty, -4 * (SystemParameters.PrimaryScreenWidth / 1920));
                Back.FontSize = (20 * SystemParameters.PrimaryScreenHeight / 1080);
            }*/
        }
        private void ConfirmButtonClick(object sender, RoutedEventArgs e)
        {
            if (InputApplication.Text != null)
            {
                string ApplicationNamed = InputApplication.Text;
                long SetLength = Convert.ToInt32(HourBox.Text) * 3600000 + Convert.ToInt32(MinuteBox.Text) * 60000 + Convert.ToInt32(SecondBox.Text) * 1000;
                String path = "C:\\Users\\brend\\source\\repos\\TrackIt\\TrackIt\\Limits.csv";
                if (File.Exists(path))
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
                    using (var writer = new StreamWriter("C:\\Users\\brend\\source\\repos\\TrackIt\\TrackIt\\Limits.csv"))
                    using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
                    {
                        csv.WriteRecords(records);
                    }
                    Fileexists = true;
                }
                if (Fileexists == true)
                {
                    // Append to the file.
                    var config = new CsvConfiguration(CultureInfo.InvariantCulture)
                    {
                        // Don't write the header again.
                        HasHeaderRecord = false,
                    };
                    using (var stream = File.Open("C:\\Users\\brend\\source\\repos\\TrackIt\\TrackIt\\Limits.csv", FileMode.Append))
                    using (var writer = new StreamWriter(stream))
                    using (var csv = new CsvWriter(writer, config))
                    {
                        csv.WriteRecords(records);
                    }
                }
                var newForm = new LimitSaved();
                newForm.Show();
                this.Close();

            }
        }
        private void BackButtonClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        void HourBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regexchecker = new Regex("[^0-9]+"); 
            e.Handled = regexchecker.IsMatch(e.Text);
        }
        private void HourBox_Pasting(object sender, DataObjectPastingEventArgs e)
        {
            if (e.DataObject.GetDataPresent(typeof(string)))
            {
                string pastedText = (string)e.DataObject.GetData(typeof(string));
                if (!IsValidInteger(pastedText))
                {
                    e.CancelCommand();
                }
                bool IsValidInteger(string input)
                {
                    int number;
                    return int.TryParse(input, out number);
                }
            }

            else
            {
                e.CancelCommand();
            }
        }

        private void MinuteBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regexchecker = new Regex("[^0-9]+"); 
            e.Handled = regexchecker.IsMatch(e.Text);
        }

        private void MinuteBox_Pasting(object sender, DataObjectPastingEventArgs e)
        {
            if (e.DataObject.GetDataPresent(typeof(string)))
            {
                string pastedText = (string)e.DataObject.GetData(typeof(string));
                if (!IsValidInteger(pastedText))
                {
                    e.CancelCommand();
                }
                bool IsValidInteger(string input)
                {
                    int number;
                    return int.TryParse(input, out number);
                }
            }

            else
            {
                e.CancelCommand();
            }
        }

        private void SecondBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regexchecker = new Regex("[^0-9]+"); 
            e.Handled = regexchecker.IsMatch(e.Text);
        }
        private void SecondBox_Pasting(object sender, DataObjectPastingEventArgs e)
        {
            if (e.DataObject.GetDataPresent(typeof(string)))
            {
                string pastedText = (string)e.DataObject.GetData(typeof(string));
                if (!IsValidInteger(pastedText))
                {
                    e.CancelCommand();
                }
                bool IsValidInteger(string input)
                {
                    int number;
                    return int.TryParse(input, out number);
                }
            }

            else
            {
                e.CancelCommand();
            }
        }
    }
}
