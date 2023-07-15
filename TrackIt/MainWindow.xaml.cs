using System;
using System.Threading;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using System.Text;
using System.Diagnostics;
using System.Runtime.InteropServices;
using CommunityToolkit.Mvvm.ComponentModel;
using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using SkiaSharp;
using static TrackIt.ScreentimeMenu;
using CsvHelper;
using CsvHelper.Configuration;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using Microsoft.Win32;
using System.Reflection;
using System.Drawing.Drawing2D;
using System.Windows.Media.Media3D;

namespace TrackIt
{
    
    // <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        bool Fileexists;
        public class NewBlacklists
        {
            public string EventName { get; set; }
            public string BlacklistName { get; set; }
            public string Applications { get; set; }

            public string DateRange { get; set; }
        }
        public MainWindow()
        {
            InitializeComponent();
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            WindowState = WindowState.Maximized;
            ScreenScale();
            StoreRecords();
            Startup();
            ListofEvents();
        }

        void Startup()
        {
            /*if (Properties.Settings.Default.StartupSet == false)
            {
                Process.Start("TrackItMonitor.exe");
                Properties.Settings.Default.StartupSet = true;
                Properties.Settings.Default.Save();
            }*/

        }

        void ScreenScale()
        {
            if (SystemParameters.PrimaryScreenHeight != 1080)
            {
                Line.Height = SystemParameters.PrimaryScreenHeight * 1;
                Line.Width = SystemParameters.PrimaryScreenWidth * 0.0031;
                Line.SetValue(Canvas.LeftProperty, 324.0 * (SystemParameters.PrimaryScreenWidth / 1920));

                CalendarIcon.SetValue(Canvas.TopProperty, 182 * (SystemParameters.PrimaryScreenHeight / 1080));
                CalendarIcon.Height = SystemParameters.PrimaryScreenHeight * (CalendarIcon.Height / 1080);
                CalendarIcon.Width = SystemParameters.PrimaryScreenWidth * (CalendarIcon.Width / 1920);
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
                Home.Width = SystemParameters.PrimaryScreenWidth * 0.0802;
                TrackIt.FontSize = (40 * SystemParameters.PrimaryScreenHeight / 1080);
                TrackIt.SetValue(Canvas.LeftProperty, 95.0 * (SystemParameters.PrimaryScreenWidth / 1920));
            }
        }
        public void StoreRecords()
        {
            Dictionary<string, ScreentimeStats> mergedRecords = new Dictionary<string, ScreentimeStats>();
            using (var reader = new StreamReader("C:\\Users\\brend\\source\\repos\\TrackIt\\TrackIt\\Storage7.csv"))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                var records = csv.GetRecords<ScreentimeStats>().ToList(); ;
                List<ScreentimeStats> alist = records.ToList();
                foreach (ScreentimeStats record in records)
                {
                    // Check if the ApplicationName and DateCollected already exist
                    string key = record.ApplicationName + record.DateCollected.Date.ToString();
                    if (mergedRecords.ContainsKey(key))
                    {
                        // If the key already exists, add the ScreenTimeCollect value to the existing record
                        mergedRecords[key].ScreenTimeCollect += record.ScreenTimeCollect;
                    }
                    else
                    {
                        // If the key does not exist, add the record to the merged records
                        mergedRecords.Add(key, record);
                    }
                }
            }
            using (var writer = new StreamWriter("C:\\Users\\brend\\source\\repos\\TrackIt\\TrackIt\\Storage7.csv"))
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                csv.WriteRecords(mergedRecords.Values);
            }
            using (var reader = new StreamReader("C:\\Users\\brend\\source\\repos\\TrackIt\\TrackIt\\Storage7.csv"))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                var records = csv.GetRecords<ScreentimeStats>().ToList();
                var today = DateTime.Today;
                records = records.Where(r => r.DateCollected.Date == today).ToList();
                List<ScreentimeStats> alist = records.ToList();
                alist.Sort((b, a) => a.ScreenTimeCollect.CompareTo(b.ScreenTimeCollect));
                Properties.Settings.Default.a = alist[0].ApplicationName;
                Properties.Settings.Default.avalue = (alist[0].ScreenTimeCollect / 60000);
                Properties.Settings.Default.b = alist[1].ApplicationName;
                Properties.Settings.Default.bvalue = (alist[1].ScreenTimeCollect / 60000);
                Properties.Settings.Default.c = alist[2].ApplicationName;
                Properties.Settings.Default.cvalue = (alist[2].ScreenTimeCollect / 60000);
                Properties.Settings.Default.d = alist[3].ApplicationName;
                Properties.Settings.Default.dvalue = (alist[3].ScreenTimeCollect / 60000);
                Properties.Settings.Default.e = alist[4].ApplicationName;
                Properties.Settings.Default.evalue = (alist[4].ScreenTimeCollect / 60000);
                Properties.Settings.Default.Save();
            }
        }
        void CalendarButtonClick(object sender, RoutedEventArgs e)
        {
            string messageBoxText = Properties.Settings.Default.e;
            string caption = "Word Processor";
            MessageBoxButton button = MessageBoxButton.YesNoCancel;
            MessageBoxImage icon = MessageBoxImage.Warning;
            MessageBoxResult result;

            result = MessageBox.Show(messageBoxText, caption, button, icon, MessageBoxResult.Yes);
            var newForm = new CalendarMenu();
            newForm.Show();
            this.Close();

        }
        void ScreenTimeButtonClick(object sender, RoutedEventArgs e)
        {
            var newForm = new ScreentimeMenu();
            newForm.Show();
            this.Close();
        }
        void PasswordButtonClick(object sender, RoutedEventArgs e)
        {
            var newForm = new PasswordMenu();
            newForm.Show();
            this.Close();
        }
        void HomeButtonClick(object sender, RoutedEventArgs e)
        {

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
