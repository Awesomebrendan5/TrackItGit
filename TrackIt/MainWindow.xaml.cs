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
using Windows.ApplicationModel.VoiceCommands;
using System.Windows.Documents;

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
            StoreDayRecords();
            StoreWeekRecords();
            StoreMonthRecords();
            StoreYearRecords();
            Startup();
            ListofEvents();
        }

        void Startup()
        {
            String path = AppDomain.CurrentDomain.BaseDirectory;
            RegistryKey key = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
            if (key.GetValue("Trackit") == null)
            {
                Process.Start("TrackItMonitor.exe");
            }

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
            }
        }
        public void StoreDayRecords()
        {
            string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            string FilePath = Path.Combine(documentsPath, "TrackIt", "ScreentimeData.csv");
            Dictionary<string, ScreentimeStats> mergedRecords = new Dictionary<string, ScreentimeStats>();
            if (File.Exists(FilePath))
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
                    List<ScreentimeStats> alist = records.ToList();
                    alist.Sort((b, a) => a.ScreenTimeCollect.CompareTo(b.ScreenTimeCollect));
                    try
                    {
                        Properties.Settings.Default.a = alist[0].ApplicationName;
                        Properties.Settings.Default.avalue = (alist[0].ScreenTimeCollect / 60000);
                        Properties.Settings.Default.Save();
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
            if (File.Exists(FilePath))
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
                if (File.Exists(FilesPath))
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
                        pastWeekRecords.Sort((b, a) => a.ScreenTimeCollect.CompareTo(b.ScreenTimeCollect));
                        records = records.Where(r => r.DateCollected.Date == today).ToList();
                        try
                        {
                            Properties.Settings.Default.f = pastWeekRecords[0].ApplicationName;
                            Properties.Settings.Default.fvalue = (pastWeekRecords[0].ScreenTimeCollect / 60000);
                        }
                        catch
                        {

                        }
                        try
                        {
                            Properties.Settings.Default.g = pastWeekRecords[1].ApplicationName;
                            Properties.Settings.Default.gvalue = (pastWeekRecords[1].ScreenTimeCollect / 60000);
                        }
                        catch
                        {

                        }
                        try
                        {
                            Properties.Settings.Default.h = pastWeekRecords[2].ApplicationName;
                            Properties.Settings.Default.hvalue = (pastWeekRecords[2].ScreenTimeCollect / 60000);
                        }
                        catch
                        {

                        }
                        try
                        {
                            Properties.Settings.Default.i = pastWeekRecords[3].ApplicationName;
                            Properties.Settings.Default.ivalue = (pastWeekRecords[3].ScreenTimeCollect / 60000);
                        }
                        catch
                        {
                        }
                        try
                        {
                            Properties.Settings.Default.j = pastWeekRecords[4].ApplicationName;
                            Properties.Settings.Default.jvalue = (pastWeekRecords[4].ScreenTimeCollect / 60000);
                        }
                        catch
                        {

                        }
                        Properties.Settings.Default.Save();
                    }
                }
                if (!File.Exists(FilesPath))
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
                        pastWeekRecords.Sort((b, a) => a.ScreenTimeCollect.CompareTo(b.ScreenTimeCollect));
                        records = records.Where(r => r.DateCollected.Date == today).ToList();
                        try
                        {
                            Properties.Settings.Default.f = pastWeekRecords[0].ApplicationName;
                            Properties.Settings.Default.fvalue = (pastWeekRecords[0].ScreenTimeCollect / 60000);
                        }
                        catch
                        {

                        }
                        try
                        {
                            Properties.Settings.Default.g = pastWeekRecords[1].ApplicationName;
                            Properties.Settings.Default.gvalue = (pastWeekRecords[1].ScreenTimeCollect / 60000);
                        }
                        catch
                        {

                        }
                        try
                        {
                            Properties.Settings.Default.h = pastWeekRecords[2].ApplicationName;
                            Properties.Settings.Default.hvalue = (pastWeekRecords[2].ScreenTimeCollect / 60000);
                        }
                        catch
                        {

                        }
                        try
                        {
                            Properties.Settings.Default.i = pastWeekRecords[3].ApplicationName;
                            Properties.Settings.Default.ivalue = (pastWeekRecords[3].ScreenTimeCollect / 60000);
                        }
                        catch
                        {
                        }
                        try
                        {
                            Properties.Settings.Default.j = pastWeekRecords[4].ApplicationName;
                            Properties.Settings.Default.jvalue = (pastWeekRecords[4].ScreenTimeCollect / 60000);
                        }
                        catch
                        {

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
            if (File.Exists(FilePath))
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
                string documentsPaths = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                string directoriesPath = Path.Combine(documentPath, "TrackIt");
                string FilesPaths = Path.Combine(directoriesPath, "ScreentimeMonthData.csv");
                if (File.Exists(FilesPaths))
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
                        var today = DateTime.Today;
                        var pastWeekRecords = records.Where(r => r.DateCollected.Date >= today.AddDays(-31)).ToList();
                        pastWeekRecords.Sort((b, a) => a.ScreenTimeCollect.CompareTo(b.ScreenTimeCollect));
                        records = records.Where(r => r.DateCollected.Date == today).ToList();
                        try
                        {
                            Properties.Settings.Default.k = pastWeekRecords[0].ApplicationName;
                            Properties.Settings.Default.kvalue = (pastWeekRecords[0].ScreenTimeCollect / 60000);
                        }
                        catch
                        {

                        }
                        try
                        {
                            Properties.Settings.Default.l = pastWeekRecords[1].ApplicationName;
                            Properties.Settings.Default.lvalue = (pastWeekRecords[1].ScreenTimeCollect / 60000);
                        }
                        catch
                        {

                        }
                        try
                        {
                            Properties.Settings.Default.m = pastWeekRecords[2].ApplicationName;
                            Properties.Settings.Default.mvalue = (pastWeekRecords[2].ScreenTimeCollect / 60000);
                        }
                        catch
                        {

                        }
                        try
                        {
                            Properties.Settings.Default.n = pastWeekRecords[3].ApplicationName;
                            Properties.Settings.Default.nvalue = (pastWeekRecords[3].ScreenTimeCollect / 60000);
                        }
                        catch
                        {

                        }
                        try
                        {
                            Properties.Settings.Default.o = pastWeekRecords[4].ApplicationName;
                            Properties.Settings.Default.ovalue = (pastWeekRecords[4].ScreenTimeCollect / 60000);
                        }
                        catch
                        {

                        }
                        Properties.Settings.Default.Save();
                    }
                }
                if (!File.Exists(FilesPaths))
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
                        var today = DateTime.Today;
                        var pastWeekRecords = records.Where(r => r.DateCollected.Date >= today.AddDays(-31)).ToList();
                        pastWeekRecords.Sort((b, a) => a.ScreenTimeCollect.CompareTo(b.ScreenTimeCollect));
                        records = records.Where(r => r.DateCollected.Date == today).ToList();
                        try
                        {
                            Properties.Settings.Default.k = pastWeekRecords[0].ApplicationName;
                            Properties.Settings.Default.kvalue = (pastWeekRecords[0].ScreenTimeCollect / 60000);
                        }
                        catch
                        {

                        }
                        try
                        {
                            Properties.Settings.Default.l = pastWeekRecords[1].ApplicationName;
                            Properties.Settings.Default.lvalue = (pastWeekRecords[1].ScreenTimeCollect / 60000);
                        }
                        catch
                        {

                        }
                        try
                        {
                            Properties.Settings.Default.m = pastWeekRecords[2].ApplicationName;
                            Properties.Settings.Default.mvalue = (pastWeekRecords[2].ScreenTimeCollect / 60000);
                        }
                        catch
                        {

                        }
                        try
                        {
                            Properties.Settings.Default.n = pastWeekRecords[3].ApplicationName;
                            Properties.Settings.Default.nvalue = (pastWeekRecords[3].ScreenTimeCollect / 60000);
                        }
                        catch
                        {

                        }
                        try
                        {
                            Properties.Settings.Default.o = pastWeekRecords[4].ApplicationName;
                            Properties.Settings.Default.ovalue = (pastWeekRecords[4].ScreenTimeCollect / 60000);
                        }
                        catch
                        {

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
            if (File.Exists(FilePath))
            {
            using (var reader = new StreamReader(FilePath))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                var records = csv.GetRecords<ScreentimeStats>().ToList();
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
            string documentsPaths = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            string directoriesPath = Path.Combine(documentPath, "TrackIt");
            string FilePaths = Path.Combine(directoriesPath, "ScreentimeYearData.csv");
            if (File.Exists(FilePaths))
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
                    pastWeekRecords.Sort((b, a) => a.ScreenTimeCollect.CompareTo(b.ScreenTimeCollect));
                    records = records.Where(r => r.DateCollected.Date == today).ToList();
                    try
                    {
                        Properties.Settings.Default.p = pastWeekRecords[0].ApplicationName;
                        Properties.Settings.Default.pvalue = (pastWeekRecords[0].ScreenTimeCollect / 60000);
                    }
                    catch
                    {

                    }
                    try
                    {
                        Properties.Settings.Default.q = pastWeekRecords[1].ApplicationName;
                        Properties.Settings.Default.qvalue = (pastWeekRecords[1].ScreenTimeCollect / 60000);
                    }
                    catch
                    {

                    }
                    try
                    {
                        Properties.Settings.Default.r = pastWeekRecords[2].ApplicationName;
                        Properties.Settings.Default.rvalue = (pastWeekRecords[2].ScreenTimeCollect / 60000);
                    }
                    catch
                    {
                        Properties.Settings.Default.s = pastWeekRecords[3].ApplicationName;
                        Properties.Settings.Default.svalue = (pastWeekRecords[3].ScreenTimeCollect / 60000);
                    }
                    try
                    {
                        Properties.Settings.Default.t = pastWeekRecords[4].ApplicationName;
                        Properties.Settings.Default.tvalue = (pastWeekRecords[4].ScreenTimeCollect / 60000);
                    }
                    catch
                    {

                    }
                    Properties.Settings.Default.Save();
                }
            }
                if (!File.Exists(FilePaths))
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
                        pastWeekRecords.Sort((b, a) => a.ScreenTimeCollect.CompareTo(b.ScreenTimeCollect));
                        records = records.Where(r => r.DateCollected.Date == today).ToList();
                        try
                        {
                            Properties.Settings.Default.p = pastWeekRecords[0].ApplicationName;
                            Properties.Settings.Default.pvalue = (pastWeekRecords[0].ScreenTimeCollect / 60000);
                        }
                        catch
                        {

                        }
                        try
                        {
                            Properties.Settings.Default.q = pastWeekRecords[1].ApplicationName;
                            Properties.Settings.Default.qvalue = (pastWeekRecords[1].ScreenTimeCollect / 60000);
                        }
                        catch
                        {

                        }
                        try
                        {
                            Properties.Settings.Default.r = pastWeekRecords[2].ApplicationName;
                            Properties.Settings.Default.rvalue = (pastWeekRecords[2].ScreenTimeCollect / 60000);
                        }
                        catch
                        {
                            Properties.Settings.Default.s = pastWeekRecords[3].ApplicationName;
                            Properties.Settings.Default.svalue = (pastWeekRecords[3].ScreenTimeCollect / 60000);
                        }
                        try
                        {
                            Properties.Settings.Default.t = pastWeekRecords[4].ApplicationName;
                            Properties.Settings.Default.tvalue = (pastWeekRecords[4].ScreenTimeCollect / 60000);
                        }
                        catch
                        {

                        }
                        Properties.Settings.Default.Save();
                    }
                }
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
        private void SettingsButtonClick(object sender, RoutedEventArgs e)
        {
            var newForm = new Settings();
            newForm.Show();
            this.Close();
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
