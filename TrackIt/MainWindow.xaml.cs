﻿using System;
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
using Windows.ApplicationModel.VoiceCommands;
using System.Windows.Documents;
using IWshRuntimeLibrary;

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
            Properties.Settings.Default.MiniWindowOpened = false;
            Properties.Settings.Default.MiniWindowOpened1 = false;
            ScreenScale();
            StoreDayRecords();
            StoreWeekRecords();
            StoreMonthRecords();
            StoreYearRecords();
            Startup();
            ListofEvents();
            String path = AppDomain.CurrentDomain.BaseDirectory;
            string wpfAppExePath = path + "TrackIt.exe"; 
            string shortcutName = "TrackIt";

            CreateShortcutOnDesktop(wpfAppExePath, shortcutName);
        }

        void Startup()
        {
            String path = AppDomain.CurrentDomain.BaseDirectory;
            RegistryKey key = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true); //Checks if TrackItMonitor is set to launch on startup.
            if (key.GetValue("Trackit") == null)
            {
                Process.Start("TrackItMonitor.exe"); //Starts TrackItMonitor .
            }

        }
        public static void CreateShortcutOnDesktop(string targetExePath, string shortcutName)
        {
            string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string shortcutPath = Path.Combine(desktopPath, $"{shortcutName}.lnk");

            dynamic shell = Activator.CreateInstance(Type.GetTypeFromProgID("WScript.Shell"));
            dynamic shortcut = shell.CreateShortcut(shortcutPath);

            shortcut.TargetPath = targetExePath;
            shortcut.Save();
        }
        void ScreenScale()
        {
            if (SystemParameters.PrimaryScreenHeight != 1080 | SystemParameters.PrimaryScreenWidth!= 1920) //Checks that the screen resolution is different to default.
            {
                Line.Height = SystemParameters.PrimaryScreenHeight * 1;
                Line.Width = SystemParameters.PrimaryScreenWidth * 0.0031;
                Line.SetValue(Canvas.LeftProperty, 324.0 * (SystemParameters.PrimaryScreenWidth / 1920));

                CalendarIcon.SetValue(Canvas.TopProperty, 182 * (SystemParameters.PrimaryScreenHeight / 1080));
                CalendarIcon.Height = SystemParameters.PrimaryScreenHeight * (CalendarIcon.Height / 1080);
                CalendarIcon.Width = SystemParameters.PrimaryScreenWidth * (CalendarIcon.Width / 1920);
                CalendarIcon.SetValue(Canvas.LeftProperty, 5.0 * (SystemParameters.PrimaryScreenWidth / 1920));

                CalendarButton.SetValue(Canvas.TopProperty, 207.0 * (SystemParameters.PrimaryScreenHeight / 1080));
                CalendarButton.Height = SystemParameters.PrimaryScreenHeight * 0.05;
                CalendarButton.Width = SystemParameters.PrimaryScreenWidth * 0.0885;
                CalendarButton.FontSize = (40 * SystemParameters.PrimaryScreenHeight / 1080);
                CalendarButton.SetValue(Canvas.LeftProperty, 95.0 * (SystemParameters.PrimaryScreenWidth / 1920));

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
                TrackIt.Width = SystemParameters.PrimaryScreenWidth * 0.0802;
                TrackIt.FontSize = (40 * SystemParameters.PrimaryScreenHeight / 1080);
                TrackIt.SetValue(Canvas.LeftProperty, 95.0 * (SystemParameters.PrimaryScreenWidth / 1920));

                UpcomingEvents.SetValue(Canvas.TopProperty, 182 * (SystemParameters.PrimaryScreenHeight / 1080));
                UpcomingEvents.Height = SystemParameters.PrimaryScreenHeight * 0.0713;
                UpcomingEvents.Width = SystemParameters.PrimaryScreenWidth * 0.1672;
                UpcomingEvents.FontSize = (40 * SystemParameters.PrimaryScreenHeight / 1080);
                UpcomingEvents.SetValue(Canvas.LeftProperty, 1525 * (SystemParameters.PrimaryScreenWidth / 1920));

                EventList.SetValue(Canvas.TopProperty, 268 * (SystemParameters.PrimaryScreenHeight / 1080));
                EventList.Height = SystemParameters.PrimaryScreenHeight * 0.6593;
                EventList.Width = SystemParameters.PrimaryScreenWidth * 0.2089;
                EventList.FontSize = (20 * SystemParameters.PrimaryScreenHeight / 1080);
                EventList.SetValue(Canvas.LeftProperty, 1485 * (SystemParameters.PrimaryScreenWidth / 1920));

                SettingsButton.SetValue(Canvas.TopProperty, 995 * (SystemParameters.PrimaryScreenHeight / 1080));
                SettingsButton.Height = SystemParameters.PrimaryScreenHeight * 0.05;
                SettingsButton.Width = SystemParameters.PrimaryScreenWidth * 0.0875;
                SettingsButton.FontSize = (40 * SystemParameters.PrimaryScreenHeight / 1080);
                SettingsButton.SetValue(Canvas.LeftProperty, 97 * (SystemParameters.PrimaryScreenWidth / 1920));

                SettingsIcon.SetValue(Canvas.TopProperty, 981 * (SystemParameters.PrimaryScreenHeight / 1080));
                SettingsIcon.Height = SystemParameters.PrimaryScreenHeight * 0.0713;
                SettingsIcon.Width = SystemParameters.PrimaryScreenWidth * 0.04167;
                SettingsIcon.SetValue(Canvas.LeftProperty, 12 * (SystemParameters.PrimaryScreenWidth / 1920));

                RedCrossButton.SetValue(Canvas.TopProperty, 0 * (SystemParameters.PrimaryScreenHeight / 1080));
                RedCrossButton.Height = SystemParameters.PrimaryScreenHeight * 0.0463;
                RedCrossButton.Width = SystemParameters.PrimaryScreenWidth * 0.0260;
                RedCrossButton.SetValue(Canvas.LeftProperty, 1870 * (SystemParameters.PrimaryScreenWidth / 1920));

                RedCross.SetValue(Canvas.TopProperty, 0 * (SystemParameters.PrimaryScreenHeight / 1080));
                RedCross.Height = SystemParameters.PrimaryScreenHeight * 0.0463;
                RedCross.Width = SystemParameters.PrimaryScreenWidth * 0.0260;
                RedCross.SetValue(Canvas.LeftProperty, 1870 * (SystemParameters.PrimaryScreenWidth / 1920));

            }
        }
        public void StoreDayRecords()
        {
            string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            string FilePath = Path.Combine(documentsPath, "TrackIt", "ScreentimeData.csv");
            Dictionary<string, ScreentimeStats> mergedRecords = new Dictionary<string, ScreentimeStats>();
            if (System.IO.File.Exists(FilePath))
            {
                using (var reader = new StreamReader(FilePath))
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
                using (var reader = new StreamReader(FilePath))
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    var records = csv.GetRecords<ScreentimeStats>().ToList();
                    var today = DateTime.Today;
                    records = records.Where(r => r.DateCollected.Date == today).ToList();
                    List<ScreentimeStats> alist = records.Where(r => r.ApplicationName.Length <= 17).ToList();
                    alist.Sort((b, a) => a.ScreenTimeCollect.CompareTo(b.ScreenTimeCollect));
                    try
                    {
                            Properties.Settings.Default.a = alist[0].ApplicationName;
                            Properties.Settings.Default.avalue = (alist[0].ScreenTimeCollect / 60000);
                    }
                    catch
                    {

                        Properties.Settings.Default.a = null;
                        Properties.Settings.Default.avalue = 0;
                    }
                    try
                    {
                            Properties.Settings.Default.b = alist[1].ApplicationName;
                            Properties.Settings.Default.bvalue = (alist[1].ScreenTimeCollect / 60000);
                    }
                    catch
                    {
                        Properties.Settings.Default.b = null;
                        Properties.Settings.Default.bvalue = 0;
                    }
                    try
                    {
                            Properties.Settings.Default.c = alist[2].ApplicationName;
                            Properties.Settings.Default.cvalue = (alist[2].ScreenTimeCollect / 60000);
                    }
                    catch
                    {
                        Properties.Settings.Default.c = null;
                        Properties.Settings.Default.cvalue = 0;
                    }
                    try
                    {
                            Properties.Settings.Default.d = alist[3].ApplicationName;
                            Properties.Settings.Default.dvalue = (alist[3].ScreenTimeCollect / 60000);
                    }
                    catch
                    {
                        Properties.Settings.Default.d = null;
                        Properties.Settings.Default.dvalue = 0;
                    }
                    try
                    {
                            Properties.Settings.Default.e = alist[4].ApplicationName;
                            Properties.Settings.Default.evalue = (alist[4].ScreenTimeCollect / 60000);
                    }
                    catch
                    {
                        Properties.Settings.Default.e = null;
                        Properties.Settings.Default.evalue = 0;
                    }
                    Properties.Settings.Default.Save();
                }
                using (var writer = new StreamWriter(FilePath))
                using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
                {
                    csv.WriteRecords(mergedRecords.Values);
                }
            }
        }
        public void StoreWeekRecords()
        {
            string documentPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            string directoryPath = Path.Combine(documentPath, "TrackIt");
            string FilePath = Path.Combine(directoryPath, "ScreentimeData.csv");
            Dictionary<string, ScreentimeStats> mergedRecords = new Dictionary<string, ScreentimeStats>();
            if (System.IO.File.Exists(FilePath))
            {
                using (var reader = new StreamReader(FilePath))
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    var records = csv.GetRecords<ScreentimeStats>().ToList(); ;
                    List<ScreentimeStats> alist = records.ToList();
                    foreach (ScreentimeStats record in records)
                    {
                        int weekNumber = CultureInfo.CurrentCulture.Calendar.GetWeekOfYear(record.DateCollected, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
                        int year = record.DateCollected.Year;
                        string key = $"{record.ApplicationName}_{weekNumber}_{year}";
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
                string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                string directoriesPath = Path.Combine(documentPath, "TrackIt");
                string FilesPath = Path.Combine(directoryPath, "ScreentimeWeekData.csv");
                if (System.IO.File.Exists(FilesPath))
                {
                    using (var writer = new StreamWriter(FilesPath))
                    using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
                    {
                        csv.WriteRecords(mergedRecords.Values);
                    }
                    using (var reader = new StreamReader(FilesPath))
                    using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                    {
                        var records = csv.GetRecords<ScreentimeStats>().ToList();
                        var today = DateTime.Today;
                        var pastWeekRecords = records.Where(r => r.DateCollected.Date >= today.AddDays(-7)).ToList();
                        var pastWeekRecordsSmall = pastWeekRecords.Where(r => r.ApplicationName.Length <= 17).ToList();
                        pastWeekRecordsSmall.Sort((b, a) => a.ScreenTimeCollect.CompareTo(b.ScreenTimeCollect));
                        records = records.Where(r => r.DateCollected.Date == today).ToList();
                        try
                        {
                                Properties.Settings.Default.f = pastWeekRecordsSmall[0].ApplicationName;
                                Properties.Settings.Default.fvalue = (pastWeekRecordsSmall[0].ScreenTimeCollect / 60000);
                        }
                        catch
                        {
                            Properties.Settings.Default.f = null;
                            Properties.Settings.Default.fvalue = 0;
                        }
                        try
                        {
                                Properties.Settings.Default.g = pastWeekRecordsSmall[1].ApplicationName;
                                Properties.Settings.Default.gvalue = (pastWeekRecordsSmall[1].ScreenTimeCollect / 60000);
                        }
                        catch
                        {
                            Properties.Settings.Default.g = null;
                            Properties.Settings.Default.gvalue = 0;
                        }
                        try
                        {
                                Properties.Settings.Default.h = pastWeekRecordsSmall[2].ApplicationName;
                                Properties.Settings.Default.hvalue = (pastWeekRecordsSmall[2].ScreenTimeCollect / 60000);
                        }
                        catch
                        {
                            Properties.Settings.Default.h = null;
                            Properties.Settings.Default.hvalue = 0;
                        }
                        try
                        {
                                Properties.Settings.Default.i = pastWeekRecordsSmall[3].ApplicationName;
                                Properties.Settings.Default.ivalue = (pastWeekRecordsSmall[3].ScreenTimeCollect / 60000);
                        }
                        catch
                        {
                            Properties.Settings.Default.i = null;
                            Properties.Settings.Default.ivalue = 0;
                        }
                        try
                        {
                                Properties.Settings.Default.j = pastWeekRecordsSmall[4].ApplicationName;
                                Properties.Settings.Default.jvalue = (pastWeekRecordsSmall[4].ScreenTimeCollect / 60000);
                        }
                        catch
                        {
                            Properties.Settings.Default.j = null;
                            Properties.Settings.Default.jvalue = 0;
                        }
                        Properties.Settings.Default.Save();
                    }
                }
                if (!System.IO.File.Exists(FilesPath))
                {
                    Directory.CreateDirectory(directoryPath);
                    using (var writer = new StreamWriter(FilesPath))
                    using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
                    {
                        csv.WriteRecords(mergedRecords.Values);
                    }
                    using (var reader = new StreamReader(FilesPath))
                    using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                    {
                        var records = csv.GetRecords<ScreentimeStats>().ToList();
                        var today = DateTime.Today;
                        var pastWeekRecords = records.Where(r => r.DateCollected.Date >= today.AddDays(-7)).ToList();
                        var pastWeekRecordsSmall = pastWeekRecords.Where(r => r.ApplicationName.Length <= 17).ToList();
                        pastWeekRecordsSmall.Sort((b, a) => a.ScreenTimeCollect.CompareTo(b.ScreenTimeCollect));
                        records = records.Where(r => r.DateCollected.Date == today).ToList();
                        try
                        {
                                Properties.Settings.Default.f = pastWeekRecordsSmall[0].ApplicationName;
                                Properties.Settings.Default.fvalue = (pastWeekRecordsSmall[0].ScreenTimeCollect / 60000);
                        }
                        catch
                        {
                            Properties.Settings.Default.f = null;
                            Properties.Settings.Default.fvalue = 0;
                        }
                        try
                        {
                                Properties.Settings.Default.g = pastWeekRecordsSmall[1].ApplicationName;
                                Properties.Settings.Default.gvalue = (pastWeekRecordsSmall[1].ScreenTimeCollect / 60000);
                        }
                        catch
                        {
                            Properties.Settings.Default.g = null;
                            Properties.Settings.Default.gvalue = 0;
                        }
                        try
                        {
                                Properties.Settings.Default.h = pastWeekRecordsSmall[2].ApplicationName;
                                Properties.Settings.Default.hvalue = (pastWeekRecordsSmall[2].ScreenTimeCollect / 60000);
                        }
                        catch
                        {
                            Properties.Settings.Default.h = null;
                            Properties.Settings.Default.hvalue = 0;
                        }
                        try
                        {
                                Properties.Settings.Default.i = pastWeekRecordsSmall[3].ApplicationName;
                                Properties.Settings.Default.ivalue = (pastWeekRecordsSmall[3].ScreenTimeCollect / 60000);
                        }
                        catch
                        {
                            Properties.Settings.Default.i = null;
                            Properties.Settings.Default.ivalue = 0;
                        }
                        try
                        {
                                Properties.Settings.Default.j = pastWeekRecordsSmall[4].ApplicationName;
                                Properties.Settings.Default.jvalue = (pastWeekRecordsSmall[4].ScreenTimeCollect / 60000);
                        }
                        catch
                        {
                            Properties.Settings.Default.j = null;
                            Properties.Settings.Default.jvalue = 0;
                        }
                        Properties.Settings.Default.Save();
                    }
                }
            }
        }
        void StoreMonthRecords()
        {
            string documentPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            string FilePath = Path.Combine(documentPath, "TrackIt", "ScreentimeData.csv");
            Dictionary<string, ScreentimeStats> mergedRecords = new Dictionary<string, ScreentimeStats>();
            if (System.IO.File.Exists(FilePath))
            {
                var today = DateTime.Today;
                var firstDayOfMonth = new DateTime(today.Year, today.Month, 1);
                using (var reader = new StreamReader(FilePath))
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    var records = csv.GetRecords<ScreentimeStats>().ToList(); ;
                    records = records.Where(r => r.DateCollected >= firstDayOfMonth && r.DateCollected <= today.AddDays(1)).ToList();
                    List<ScreentimeStats> alist = records.ToList();
                    foreach (ScreentimeStats record in records)
                    {
                        int weekNumber = CultureInfo.CurrentCulture.Calendar.GetWeekOfYear(record.DateCollected, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
                        int year = record.DateCollected.Year;
                        string key = $"{record.ApplicationName}_{record.DateCollected.Month}_{record.DateCollected.Year}";
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
                string documentsPaths = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                string directoriesPath = Path.Combine(documentPath, "TrackIt");
                string FilesPaths = Path.Combine(directoriesPath, "ScreentimeMonthData.csv");
                if (System.IO.File.Exists(FilesPaths))
                {
                    using (var writer = new StreamWriter(FilesPaths))
                    using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
                    {
                        csv.WriteRecords(mergedRecords.Values);
                    }
                    using (var reader = new StreamReader(FilesPaths))
                    using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                    {
                        var records = csv.GetRecords<ScreentimeStats>().ToList();
                        var today1 = DateTime.Today;
                        var pastWeekRecords = records.Where(r => r.DateCollected.Date >= today1.AddDays(-31)).ToList();
                        var pastWeekRecordsSmall = pastWeekRecords.Where(r => r.ApplicationName.Length <= 17).ToList();
                        pastWeekRecordsSmall.Sort((b, a) => a.ScreenTimeCollect.CompareTo(b.ScreenTimeCollect));
                        records = records.Where(r => r.DateCollected.Date == today1).ToList();
                        try
                        {
                                Properties.Settings.Default.k = pastWeekRecordsSmall[0].ApplicationName;
                                Properties.Settings.Default.kvalue = (pastWeekRecordsSmall[0].ScreenTimeCollect / 60000);
                        }
                        catch
                        {
                            Properties.Settings.Default.k = null;
                            Properties.Settings.Default.kvalue = 0;
                        }
                        try
                        {
                                Properties.Settings.Default.l = pastWeekRecordsSmall[1].ApplicationName;
                                Properties.Settings.Default.lvalue = (pastWeekRecordsSmall[1].ScreenTimeCollect / 60000);
                        }
                        catch
                        {
                            Properties.Settings.Default.l = null;
                            Properties.Settings.Default.lvalue = 0;
                        }
                        try
                        {
                                Properties.Settings.Default.m = pastWeekRecordsSmall[2].ApplicationName;
                                Properties.Settings.Default.mvalue = (pastWeekRecordsSmall[2].ScreenTimeCollect / 60000);
                        }
                        catch
                        {
                            Properties.Settings.Default.m = null;
                            Properties.Settings.Default.mvalue = 0;
                        }
                        try
                        {
                                Properties.Settings.Default.n = pastWeekRecordsSmall[3].ApplicationName;
                                Properties.Settings.Default.nvalue = (pastWeekRecordsSmall[3].ScreenTimeCollect / 60000);
                        }
                        catch
                        {
                            Properties.Settings.Default.n = null;
                            Properties.Settings.Default.nvalue = 0;
                        }
                        try
                        {
                                Properties.Settings.Default.o = pastWeekRecordsSmall[4].ApplicationName;
                                Properties.Settings.Default.ovalue = (pastWeekRecordsSmall[4].ScreenTimeCollect / 60000);
                        }
                        catch
                        {
                            Properties.Settings.Default.o = null;
                            Properties.Settings.Default.ovalue = 0;
                        }
                        Properties.Settings.Default.Save();
                    }
                }
                if (!System.IO.File.Exists(FilesPaths))
                {
                    Directory.CreateDirectory(directoriesPath);
                    using (var writer = new StreamWriter(FilesPaths))
                    using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
                    {
                        csv.WriteRecords(mergedRecords.Values);
                    }
                    using (var reader = new StreamReader(FilesPaths))
                    using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                    {
                        var records = csv.GetRecords<ScreentimeStats>().ToList();
                        var today1 = DateTime.Today;
                        var pastWeekRecords = records.Where(r => r.DateCollected.Date >= today1.AddDays(-31)).ToList();
                        var pastWeekRecordsSmall = pastWeekRecords.Where(r => r.ApplicationName.Length <= 17).ToList();
                        pastWeekRecordsSmall.Sort((b, a) => a.ScreenTimeCollect.CompareTo(b.ScreenTimeCollect));
                        records = records.Where(r => r.DateCollected.Date == today1).ToList();
                        try
                        {
                                Properties.Settings.Default.k = pastWeekRecordsSmall[0].ApplicationName;
                                Properties.Settings.Default.kvalue = (pastWeekRecordsSmall[0].ScreenTimeCollect / 60000);
                        }
                        catch
                        {
                            Properties.Settings.Default.k = null;
                            Properties.Settings.Default.kvalue = 0;
                        }
                        try
                        {
                                Properties.Settings.Default.l = pastWeekRecordsSmall[1].ApplicationName;
                                Properties.Settings.Default.lvalue = (pastWeekRecordsSmall[1].ScreenTimeCollect / 60000);
                        }
                        catch
                        {
                            Properties.Settings.Default.l = null;
                            Properties.Settings.Default.lvalue = 0;
                        }
                        try
                        {
                                Properties.Settings.Default.m = pastWeekRecordsSmall[2].ApplicationName;
                                Properties.Settings.Default.mvalue = (pastWeekRecordsSmall[2].ScreenTimeCollect / 60000);
                        }
                        catch
                        {
                            Properties.Settings.Default.m = null;
                            Properties.Settings.Default.mvalue = 0;
                        }
                        try
                        {
                                Properties.Settings.Default.n = pastWeekRecordsSmall[3].ApplicationName;
                                Properties.Settings.Default.nvalue = (pastWeekRecordsSmall[3].ScreenTimeCollect / 60000);
                        }
                        catch
                        {
                            Properties.Settings.Default.n = null;
                            Properties.Settings.Default.nvalue = 0;
                        }
                        try
                        {
                                Properties.Settings.Default.o = pastWeekRecordsSmall[4].ApplicationName;
                                Properties.Settings.Default.ovalue = (pastWeekRecordsSmall[4].ScreenTimeCollect / 60000);
                        }
                        catch
                        {
                            Properties.Settings.Default.o = null;
                            Properties.Settings.Default.ovalue = 0;
                        }
                        Properties.Settings.Default.Save();
                    }
                }
            }
        }
        void StoreYearRecords()
        {
            string documentPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            string FilePath = Path.Combine(documentPath, "TrackIt", "ScreentimeData.csv");
            Dictionary<string, ScreentimeStats> mergedRecords = new Dictionary<string, ScreentimeStats>();
            if (System.IO.File.Exists(FilePath))
            {
            using (var reader = new StreamReader(FilePath))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                var today = DateTime.Today;
                var oneYearAgo = today.AddYears(-1);
                var records = csv.GetRecords<ScreentimeStats>().ToList();
                    records = records.Where(r => r.DateCollected >= oneYearAgo && r.DateCollected <= today.AddDays(1)).ToList();
                    List<ScreentimeStats> alist = records.ToList();
                foreach (ScreentimeStats record in records)
                {
                    int weekNumber = CultureInfo.CurrentCulture.Calendar.GetWeekOfYear(record.DateCollected, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
                    int year = record.DateCollected.Year;
                        string key = $"{record.ApplicationName}_{record.DateCollected.Month}_{record.DateCollected.Year}";
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
            string documentsPaths = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            string directoriesPath = Path.Combine(documentPath, "TrackIt");
            string FilePaths = Path.Combine(directoriesPath, "ScreentimeYearData.csv");
            if (System.IO.File.Exists(FilePaths))
            {
                using (var writer = new StreamWriter(FilePaths))
                using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
                {
                    csv.WriteRecords(mergedRecords.Values);
                }
                using (var reader = new StreamReader(FilePaths))
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    var records = csv.GetRecords<ScreentimeStats>().ToList();
                    var today = DateTime.Today;
                    var pastWeekRecords = records.Where(r => r.DateCollected.Date >= today.AddDays(-365)).ToList();
                    var pastWeekRecordsSmall = pastWeekRecords.Where(r => r.ApplicationName.Length <= 17).ToList();
                    pastWeekRecordsSmall.Sort((b, a) => a.ScreenTimeCollect.CompareTo(b.ScreenTimeCollect));
                    records = records.Where(r => r.DateCollected.Date == today).ToList();
                    try
                    {
                                Properties.Settings.Default.p = pastWeekRecordsSmall[0].ApplicationName;
                                Properties.Settings.Default.pvalue = (pastWeekRecordsSmall[0].ScreenTimeCollect / 60000);
                    }
                    catch
                    {
                            Properties.Settings.Default.p = null;
                            Properties.Settings.Default.pvalue = 0;
                    }
                    try
                    {
                                Properties.Settings.Default.q = pastWeekRecordsSmall[1].ApplicationName;
                                Properties.Settings.Default.qvalue = (pastWeekRecordsSmall[1].ScreenTimeCollect / 60000);
                    }
                    catch
                    {
                            Properties.Settings.Default.q = null;
                            Properties.Settings.Default.qvalue = 0;
                    }
                    try
                    {
                                Properties.Settings.Default.r = pastWeekRecordsSmall[2].ApplicationName;
                                Properties.Settings.Default.rvalue = (pastWeekRecordsSmall[2].ScreenTimeCollect / 60000);
                    }
                    catch
                    {
                            Properties.Settings.Default.r = null;
                            Properties.Settings.Default.rvalue = 0;
                    }
                    try
                    {
                                Properties.Settings.Default.s = pastWeekRecordsSmall[3].ApplicationName;
                                Properties.Settings.Default.svalue = (pastWeekRecordsSmall[3].ScreenTimeCollect / 60000);
                    }
                    catch
                    {
                            Properties.Settings.Default.s = null;
                            Properties.Settings.Default.svalue = 0;
                    }
                    try
                    {
                                Properties.Settings.Default.t = pastWeekRecordsSmall[4].ApplicationName;
                                Properties.Settings.Default.tvalue = (pastWeekRecordsSmall[4].ScreenTimeCollect / 60000);
                    }
                    catch
                    {
                            Properties.Settings.Default.t = null;
                            Properties.Settings.Default.tvalue = 0;
                    }
                    Properties.Settings.Default.Save();
                }
            }
                if (!System.IO.File.Exists(FilePaths))
                {
                    Directory.CreateDirectory(directoriesPath);
                    using (var writer = new StreamWriter(FilePaths))
                    using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
                    {
                        csv.WriteRecords(mergedRecords.Values);
                    }
                    using (var reader = new StreamReader(FilePaths))
                    using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                    {
                        var records = csv.GetRecords<ScreentimeStats>().ToList();
                        var today = DateTime.Today;
                        var pastWeekRecords = records.Where(r => r.DateCollected.Date >= today.AddDays(-365)).ToList();
                        var pastWeekRecordsSmall = pastWeekRecords.Where(r => r.ApplicationName.Length <= 17).ToList();
                        pastWeekRecordsSmall.Sort((b, a) => a.ScreenTimeCollect.CompareTo(b.ScreenTimeCollect));
                        records = records.Where(r => r.DateCollected.Date == today).ToList();
                        try
                        {
                                Properties.Settings.Default.p = pastWeekRecordsSmall[0].ApplicationName;
                                Properties.Settings.Default.pvalue = (pastWeekRecordsSmall[0].ScreenTimeCollect / 60000);
                        }
                        catch
                        {
                            Properties.Settings.Default.p = null;
                            Properties.Settings.Default.pvalue = 0;
                        }
                        try
                        {
                                Properties.Settings.Default.q = pastWeekRecordsSmall[1].ApplicationName;
                                Properties.Settings.Default.qvalue = (pastWeekRecordsSmall[1].ScreenTimeCollect / 60000);
                        }
                        catch
                        {
                            Properties.Settings.Default.q = null;
                            Properties.Settings.Default.qvalue = 0;
                        }
                        try
                        {
                                Properties.Settings.Default.r = pastWeekRecordsSmall[2].ApplicationName;
                                Properties.Settings.Default.rvalue = (pastWeekRecordsSmall[2].ScreenTimeCollect / 60000);
                        }
                        catch
                        {
                            Properties.Settings.Default.r = null;
                            Properties.Settings.Default.rvalue = 0;
                        }
                        try
                        {
                                Properties.Settings.Default.s = pastWeekRecordsSmall[3].ApplicationName;
                                Properties.Settings.Default.svalue = (pastWeekRecordsSmall[3].ScreenTimeCollect / 60000);
                        }
                        catch
                        {
                            Properties.Settings.Default.s = null;
                            Properties.Settings.Default.svalue = 0;
                        }
                        try
                        {
                                Properties.Settings.Default.t = pastWeekRecordsSmall[4].ApplicationName;
                                Properties.Settings.Default.tvalue = (pastWeekRecordsSmall[4].ScreenTimeCollect / 60000);
                        }
                        catch
                        {
                            Properties.Settings.Default.t = null;
                            Properties.Settings.Default.tvalue = 0;
                        }
                        Properties.Settings.Default.Save();
                    }
                }
            }
        }
        void CalendarButtonClick(object sender, RoutedEventArgs e)
        {
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
        void RedCrossButtonClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void SettingsButtonClick(object sender, RoutedEventArgs e)
        {
            var newForm = new Settings();
            newForm.Show();
            this.Close();
        }
        void ListofEvents()
        {
            string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            string directoryPath = Path.Combine(documentsPath, "TrackIt");
            string FilePath = Path.Combine(directoryPath, "BlacklistsCombined.csv");
            if (System.IO.File.Exists(FilePath))
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
                var addedEvents = new HashSet<string>();
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

                        if (startTime.Date == DateTime.Now.Date)
                        {
                            var name = blacklist.EventName;
                            var eventString = name + " " + startTime.TimeOfDay + " - " + endTime.TimeOfDay;
                            if (!addedEvents.Contains(eventString))
                            {
                                EventList.Items.Add(name + " " + startTime.TimeOfDay + " - " + endTime.TimeOfDay);
                                addedEvents.Add(eventString);
                            }
                        }
                    }
                }
            }
        }
    }
}
