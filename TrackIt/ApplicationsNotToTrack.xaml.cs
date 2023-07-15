using CsvHelper.Configuration;
using CsvHelper;
using Microsoft.Win32;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System;
using System.Windows;
using System.Windows.Controls;

namespace TrackIt
{
    /// <summary>
    /// Interaction logic for ApplicationsNotToTrack.xaml
    /// </summary>
    public partial class ApplicationsNotToTrack : Window
    {
        public bool Fileexists;
        private List<string> ListofApps;
        public class ApplicationsNotToMonitor
        {
            public string Apps { get; set; }
        }
        public ApplicationsNotToTrack()
        {
            InitializeComponent();
            ListofApplications();
            ListofApps = new List<string>();
            Screenscale();
        }
        void Screenscale()
        {
            if (SystemParameters.PrimaryScreenHeight != 1080)
            {
                Height = SystemParameters.PrimaryScreenHeight * 0.6667;
                Width = SystemParameters.PrimaryScreenWidth * 0.2229;

                DoNotTrack.SetValue(Canvas.TopProperty, 10 * (SystemParameters.PrimaryScreenHeight / 1080));
                DoNotTrack.Height = SystemParameters.PrimaryScreenHeight * 0.0444;
                DoNotTrack.Width = SystemParameters.PrimaryScreenWidth * 0.2063;
                DoNotTrack.SetValue(Canvas.LeftProperty, 16 * (SystemParameters.PrimaryScreenWidth / 1920));
                DoNotTrack.FontSize = (30 * SystemParameters.PrimaryScreenHeight / 1080);

                SelectApplications.SetValue(Canvas.TopProperty, 97 * (SystemParameters.PrimaryScreenHeight / 1080));
                SelectApplications.Height = SystemParameters.PrimaryScreenHeight * 0.0444;
                SelectApplications.Width = SystemParameters.PrimaryScreenWidth * 0.2063;
                SelectApplications.SetValue(Canvas.LeftProperty, 22 * (SystemParameters.PrimaryScreenWidth / 1920));
                SelectApplications.FontSize = (20 * SystemParameters.PrimaryScreenHeight / 1080);

                Applications.SetValue(Canvas.TopProperty, 174 * (SystemParameters.PrimaryScreenHeight / 1080));
                Applications.Height = SystemParameters.PrimaryScreenHeight * 0.3;
                Applications.Width = SystemParameters.PrimaryScreenWidth * 0.1052;
                Applications.SetValue(Canvas.LeftProperty, 113 * (SystemParameters.PrimaryScreenWidth / 1920));
                Applications.FontSize = (20 * SystemParameters.PrimaryScreenHeight / 1080);

                Confirm.SetValue(Canvas.TopProperty, 522 * (SystemParameters.PrimaryScreenHeight / 1080));
                Confirm.Height = SystemParameters.PrimaryScreenHeight * 0.0398;
                Confirm.Width = SystemParameters.PrimaryScreenWidth * 0.0646;
                Confirm.SetValue(Canvas.LeftProperty, 152 * (SystemParameters.PrimaryScreenWidth / 1920));
                Confirm.FontSize = (20 * SystemParameters.PrimaryScreenHeight / 1080);

                Back.SetValue(Canvas.TopProperty, 651 * (SystemParameters.PrimaryScreenHeight / 1080));
                Back.Height = SystemParameters.PrimaryScreenHeight * 0.0398;
                Back.Width = SystemParameters.PrimaryScreenWidth * 0.0646;
                Back.SetValue(Canvas.LeftProperty, -4 * (SystemParameters.PrimaryScreenWidth / 1920));
                Back.FontSize = (20 * SystemParameters.PrimaryScreenHeight / 1080);
            }
        }

        private void ConfirmButtonClick(object sender, RoutedEventArgs e)
        {
            SelectedItems();
            void SelectedItems()
            {
                if (Applications.SelectedItem != null)
                {
                    ListofApps.Clear();
                    string filePath = "C:\\Users\\brend\\source\\repos\\TrackIt\\TrackIt\\ApplicationsNotToTrack.csv";
                    if (File.Exists(filePath))
                    {
                        Fileexists = true;
                    }
                    else
                    {
                        Fileexists = false;
                    }
                    foreach (var item in Applications.SelectedItems)
                    {
                        ListofApps.Add(item.ToString());
                    }
                    var records = new List<ApplicationsNotToMonitor>();
                    records.Add(new ApplicationsNotToMonitor {Apps = string.Join(",", ListofApps) });
                    if (Fileexists == false)
                    {
                        using (var writer = new StreamWriter("C:\\Users\\brend\\source\\repos\\TrackIt\\TrackIt\\ApplicationsNotToTrack.csv"))
                        using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
                        {
                            csv.WriteRecords(records);
                        }
                        Fileexists = true;
                    }
                    if (Fileexists == true)
                    {
                        var config = new CsvConfiguration(CultureInfo.InvariantCulture)
                        {
                            HasHeaderRecord = false,
                        };
                        using (var stream = File.Open("C:\\Users\\brend\\source\\repos\\TrackIt\\TrackIt\\ApplicationsNotToTrack.csv", FileMode.Append))
                        using (var writer = new StreamWriter(stream))
                        using (var csv = new CsvWriter(writer, config))
                        {
                            csv.WriteRecords(records);
                        }
                    }
                }
            }
        }
        private void BackButtonClick(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.MiniWindowOpened = false;
            this.Close();
        }
        void Add_AnotherClick(object sender, RoutedEventArgs e)
        {
            var newForm = new AddMoreApplications();
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
            Properties.Settings.Default.Save();
        }
    }
}
