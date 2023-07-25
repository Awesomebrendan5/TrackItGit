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
        private List<string> ListofApps; //Define ListofApps as a private string list.
        public class ApplicationsNotToMonitor
        {
            public string Apps { get; set; } //Define the Apps string.
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
            if (SystemParameters.PrimaryScreenHeight != 1080 | SystemParameters.PrimaryScreenWidth != 1920) //Check that the screen resolution is different to default.
            {
                MinHeight = SystemParameters.PrimaryScreenHeight * (740.0 / 1080.0); //Set MinHeight property.
                MinWidth = SystemParameters.PrimaryScreenWidth * (428.0 / 1920); //Set MinWidth property.

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
            SelectedItems();
            void SelectedItems()
            {
                if (Applications.SelectedItem != null) //Check that the user has a selected an item in the list.
                {
                    string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments); //Save the documents path.
                    string directoryPath = Path.Combine(documentsPath, "TrackIt"); //Save TrackIt's directory path.
                    string FilePath = Path.Combine(directoryPath, "ApplicationsNotToTrack.csv"); //Save the file path.
                    if (File.Exists(FilePath))
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
                    records.Add(new ApplicationsNotToMonitor {Apps = string.Join(",", ListofApps) }); //Create a record with the apps variable set to all selected applications in the ListofApps.
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
                            csv.WriteRecords(records); //Write the record into ApplicationsNotToTrack.csv.
                        }
                        var newForm = new ApplicationAdded();
                        newForm.Show();
                        this.Close();
                    }
                    if (Fileexists == false)
                    {
                        Directory.CreateDirectory(directoryPath); //Create the directory if missing.
                        using (var writer = new StreamWriter(FilePath))
                        using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
                        {
                            csv.WriteRecords(records); //Write the record into ApplicationsNotToTrack.csv.
                        }
                        Fileexists = true;
                        var newForm = new ApplicationAdded(); //Open the ApplicationAdded window.
                        newForm.Show();
                        this.Close(); //Close the window.
                    }
                }
            }
        }
        private void BackButtonClick(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.MiniWindowOpened = false; //Set MiniWindowOpened to false.
            this.Close(); //Close the window.
        }
        void Add_AnotherClick(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.MiniWindowOpened1 = true; //Set MiniWindowOpened1 to true.
            var newForm = new AddMoreApplications(); //Open the AddMoreApplications window.
            newForm.Show();
            this.Close(); //Close the window.
        }
        void ListofApplications()
        {
            string registry_key = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall"; //Access the specified registry key.
            using Microsoft.Win32.RegistryKey key = Registry.LocalMachine.OpenSubKey(registry_key);
            foreach (string subkey_name in key.GetSubKeyNames()) //Iterate through the subkey_name of all appliations in the key.
            {
                using RegistryKey subkey = key.OpenSubKey(subkey_name);
                Applications.Items.Add(subkey.GetValue("DisplayName")); //Add the subkey DisplayName property to the Applications list.
            }
        }
        protected override void OnClosing(System.ComponentModel.CancelEventArgs e) 
        {
            Properties.Settings.Default.MiniWindowOpened = false; //Set MiniWindowOpened to false.
            Properties.Settings.Default.Save();
        }
    }
}
