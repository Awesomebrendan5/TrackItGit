using Microsoft.Win32;
using System.Windows;
using System.Windows.Controls;
using CsvHelper.Configuration;
using CsvHelper;
using System.Runtime.InteropServices;
using System.Text;
using System;
using System.Collections.Generic;
using System.IO;
using System.Drawing.Drawing2D;
using System.Windows.Shapes;
using System.Globalization;
using static TrackIt.BlacklistCreation;

namespace TrackIt
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class BlacklistCreation : Window
    {
        public bool Fileexists;
        private List<string> ListofApps; //Define ListofApps as a private string list.
        [DllImport("user32.dll", CharSet = CharSet.Unicode, SetLastError = true)] //Import user32.dll.
        static extern IntPtr GetForegroundWindow(); //Import the GetForegroundWindow functionality.
        [DllImport("user32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        static extern int GetWindowText(IntPtr hWnd, StringBuilder lpString, int nMaxCount); //Import the GetWindowText functionality.
        [DllImport("user32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        static extern int GetWindowTextLength(IntPtr hWnd); //Import the GetWindowTextLength functionality.
        public BlacklistCreation()
        {
            InitializeComponent();
            ListofApps = new List<string>();
            ListofApplications();
            Screenscale();
        }
        public class Blacklists
        {
            public string BlacklistName { get; set; } //Define the BlacklistName string.
            public string Applications { get; set; } //Define the Applications string.
        }

        void Screenscale()
        {
            if (SystemParameters.PrimaryScreenHeight != 1080) //Check that the screen resolution is different to default.
            {
                MinHeight = SystemParameters.PrimaryScreenHeight * (740.0 / 1080.0); //Set MinHeight property.
                MinWidth = SystemParameters.PrimaryScreenWidth * 0.2229; //Set MinWidth property.

                BlacklistBox.SetValue(Canvas.TopProperty, 10 * (SystemParameters.PrimaryScreenHeight / 1080));
                BlacklistBox.Height = SystemParameters.PrimaryScreenHeight * 0.0445;
                BlacklistBox.Width = SystemParameters.PrimaryScreenWidth * 0.1688;
                BlacklistBox.SetValue(Canvas.LeftProperty, 52 * (SystemParameters.PrimaryScreenWidth / 1920));
                BlacklistBox.FontSize = (30 * SystemParameters.PrimaryScreenHeight / 1080);

                EnterBlacklist.SetValue(Canvas.TopProperty, 72 * (SystemParameters.PrimaryScreenHeight / 1080));
                EnterBlacklist.Height = SystemParameters.PrimaryScreenHeight * 0.0287;
                EnterBlacklist.Width = SystemParameters.PrimaryScreenWidth * 0.1354;
                EnterBlacklist.SetValue(Canvas.LeftProperty, 84 * (SystemParameters.PrimaryScreenWidth / 1920));
                EnterBlacklist.FontSize = (20 * SystemParameters.PrimaryScreenHeight / 1080);

                Information.SetValue(Canvas.TopProperty, 108 * (SystemParameters.PrimaryScreenHeight / 1080));
                Information.Height = SystemParameters.PrimaryScreenHeight * 0.0287;
                Information.Width = SystemParameters.PrimaryScreenWidth * 0.1177;
                Information.SetValue(Canvas.LeftProperty, 101 * (SystemParameters.PrimaryScreenWidth / 1920));
                Information.FontSize = (15 * SystemParameters.PrimaryScreenHeight / 1080);

                EnterBlacklistName.SetValue(Canvas.TopProperty, 137 * (SystemParameters.PrimaryScreenHeight / 1080));
                EnterBlacklistName.Height = SystemParameters.PrimaryScreenHeight * 0.0445;
                EnterBlacklistName.Width = SystemParameters.PrimaryScreenWidth * 0.1052;
                EnterBlacklistName.SetValue(Canvas.LeftProperty, 113 * (SystemParameters.PrimaryScreenWidth / 1920));

                SelectApplications.SetValue(Canvas.TopProperty, 216 * (SystemParameters.PrimaryScreenHeight / 1080));
                SelectApplications.Height = SystemParameters.PrimaryScreenHeight * 0.0519;
                SelectApplications.Width = SystemParameters.PrimaryScreenWidth * 0.1667;
                SelectApplications.SetValue(Canvas.LeftProperty, 54 * (SystemParameters.PrimaryScreenWidth / 1920));
                SelectApplications.FontSize = (20 * SystemParameters.PrimaryScreenHeight / 1080);

                Applications.SetValue(Canvas.TopProperty, 272 * (SystemParameters.PrimaryScreenHeight / 1080));
                Applications.Height = SystemParameters.PrimaryScreenHeight * 0.3;
                Applications.Width = SystemParameters.PrimaryScreenWidth * 0.1052;
                Applications.SetValue(Canvas.LeftProperty, 113 * (SystemParameters.PrimaryScreenWidth / 1920));
                Applications.FontSize = (20 * SystemParameters.PrimaryScreenHeight / 1080);

                Confirm.SetValue(Canvas.TopProperty, 611 * (SystemParameters.PrimaryScreenHeight / 1080));
                Confirm.Height = SystemParameters.PrimaryScreenHeight * 0.0398;
                Confirm.Width = SystemParameters.PrimaryScreenWidth * 0.0646;
                Confirm.SetValue(Canvas.LeftProperty, 152 * (SystemParameters.PrimaryScreenWidth / 1920));
                Confirm.FontSize = (20 * SystemParameters.PrimaryScreenHeight / 1080);

                Back.SetValue(Canvas.TopProperty, 665 * (SystemParameters.PrimaryScreenHeight / 1080));
                Back.Height = SystemParameters.PrimaryScreenHeight * (43.0 / 1080.0);
                Back.Width = SystemParameters.PrimaryScreenWidth * (124.0 / 1920.0);
                Back.SetValue(Canvas.LeftProperty, 0 * (SystemParameters.PrimaryScreenWidth / 1920));
                Back.FontSize = (20 * SystemParameters.PrimaryScreenHeight / 1080);

            }
        }

        private void ConfirmButtonClick(object sender, RoutedEventArgs e)
        {
            SelectedItems();
            void SelectedItems()
            {
                if (Applications.SelectedItem != null && !String.IsNullOrEmpty(EnterBlacklistName.Text)) //Check that the user has selected applications and input a blacklist name.
                {
                    if (EnterBlacklistName.Text.Length < 12) //Check that the blacklist name is under 12 characters.
                    {
                        string BlacklistName = EnterBlacklistName.Text; //Set the BlacklistName variable to the user input blacklist name.
                        ListofApps.Clear();
                        string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments); //Save the documents path.
                        string directoryPath = System.IO.Path.Combine(documentsPath, "TrackIt"); //Save TrackIt's directory path.
                        string FilePath = System.IO.Path.Combine(directoryPath, "Blacklists.csv"); //Save the file path.
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
                            ListofApps.Add(item.ToString()); //Add every item that the user has seected into a list of strings.
                        }
                        var records = new List<Blacklists>();
                        records.Add(new Blacklists { BlacklistName = BlacklistName, Applications = string.Join(",", ListofApps) }); //Create a record with the BlacklistName property being the user defined blacklist name and the Applications property being a comma separated list of their selected applications.
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
                                csv.WriteRecords(records); //Write the record into Blacklists.csv.
                            }
                        }
                        if (Fileexists == false)
                        {
                            Directory.CreateDirectory(directoryPath);
                            using (var writer = new StreamWriter(FilePath))
                            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
                            {
                                csv.WriteRecords(records); //Write the record into Blacklists.csv.
                            }
                            Fileexists = true;
                        }
                        Properties.Settings.Default.MiniWindowOpened1 = true; //Set MiniWindowOpened1 to true.
                        var newForm = new BlacklistSaved(); //Open the BlacklistSaved window.
                        newForm.Show();
                        this.Close(); //Close the window.
                    }
                    if (EnterBlacklistName.Text.Length >= 12) //Check that the blacklist name is greater than or equal to 12 characters.
                    {
                        if (Properties.Settings.Default.MiniWindowOpened1 == false) //Check that MiniWindowOpened1 is false.
                        {
                            Properties.Settings.Default.MiniWindowOpened1 = true; //Set MiniWindowOpened1 to true.
                            var newForm = new BlacklistOver12();
                            newForm.Show();
                        }
                    }
                }
            }
        }
        private void BackButtonClick(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.MiniWindowOpened = false;
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
