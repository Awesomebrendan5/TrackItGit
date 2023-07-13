using CsvHelper.Configuration;
using CsvHelper;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Formats.Asn1;
using Microsoft.Win32;

namespace TheTracker
{
    public class TheTrackerService
    {
        [DllImport("user32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        static extern IntPtr GetForegroundWindow();
        [DllImport("user32.dll")]
        static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImport("user32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        static extern int GetWindowText(IntPtr hWnd, StringBuilder lpString, int nMaxCount);
        [DllImport("user32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        static extern int GetWindowTextLength(IntPtr hWnd);
        private readonly System.Timers.Timer t;
        public string NonNullName;

        public class ScreentimeStats
        {
            public string ApplicationName { get; set; }
            public long ScreenTimeCollect { get; set; }
            public DateTime DateCollected { get; set; }
        }
        public class ScreentimeLimits
        {
            public string ApplicationName { get; set; }
            public long ScreenTimeLimit { get; set; }
        }


        public TheTrackerService()
        {
            bool Fileexists = false;
            IntPtr CurrentWindow = GetForegroundWindow(); //Saves the currently open Window.
            Int32 OpenApplication = CurrentWindow.ToInt32(); //Saves that window as an integer.
            IntPtr OldOpenWindow = CurrentWindow; //Saves the currently open window as OldOpenWindow.
            Stopwatch ScreenTimer = new Stopwatch();
            Startup();
            ScreenTimer.Start();
            String path = "C:\\Users\\brend\\source\\repos\\TrackIt\\TrackIt\\Storage7.csv";
            if (File.Exists(path))
            {
                Fileexists = true;
            }
            else
            {
                Fileexists = false;
            }
            t = new System.Timers.Timer(10000) { AutoReset = true };
            t.Elapsed += OnEventExecution;
            Console.ReadLine();
            void Startup()
            {
                String path = AppDomain.CurrentDomain.BaseDirectory;
                RegistryKey key = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
                key.SetValue("Trackit", path + "TrackItMonitor.exe");
            }
            void OnEventExecution(object sender, System.Timers.ElapsedEventArgs e)
            {
                Console.WriteLine("Ran" + DateTime.Now);
                try
                {
                    {
                        IntPtr NewOpenWindow = GetForegroundWindow(); //Defines NewOpenWindow as the window now open.
                            int textLength = GetWindowTextLength(CurrentWindow);
                            StringBuilder ApplicationName = new StringBuilder(textLength + 1);
                            GetWindowText(CurrentWindow, ApplicationName, ApplicationName.Capacity);
                            DateTime DateCollected = DateTime.Now;
                            long TimerDuration = ScreenTimer.ElapsedMilliseconds;
                            String ApplicationNamed = ApplicationName.ToString();
                            if (!ApplicationNamed.Contains("Lock Screen"))
                            {
                                if (textLength > 0)
                                {
                                    if (ApplicationNamed.Contains("Google Chrome"))
                                    {
                                        ApplicationNamed = "Google Chrome";
                                        Console.WriteLine("works");
                                    }
                                    NonNullName = ApplicationNamed;
                                    if (ApplicationNamed.Contains("Notepad++"))
                                    {
                                        ApplicationNamed = "Notepad++";
                                        Console.WriteLine("works");
                                    }
                                    if (ApplicationNamed.Contains("TrackIt"))
                                    {
                                        ApplicationNamed = "TrackIt";
                                        Console.WriteLine("works");
                                    }
                                    if (ApplicationNamed.Contains("Edge"))
                                    {
                                        ApplicationNamed = "Microsoft Edge";
                                        Console.WriteLine("works");
                                    }
                                    if (ApplicationNamed.Contains("Firefox"))
                                    {
                                        ApplicationNamed = "Firefox";
                                        Console.WriteLine("works");
                                    }
                                    if (ApplicationNamed.Contains("Discord"))
                                    {
                                        ApplicationNamed = "Discord";
                                        Console.WriteLine("works");
                                    }
                                    if (ApplicationNamed.Contains("Opera"))
                                    {
                                        ApplicationNamed = "Opera";
                                        Console.WriteLine("works");
                                    }
                                    if (ApplicationNamed.Contains("OneNote"))
                                    {
                                        ApplicationNamed = "OneNote";
                                        Console.WriteLine("works");
                                    }
                                    if (ApplicationNamed.Contains("Word"))
                                    {
                                        ApplicationNamed = "Microsoft Word";
                                        Console.WriteLine("works");
                                    }
                                    if (ApplicationNamed.Contains("PowerPoint"))
                                    {
                                        ApplicationNamed = "Powerpoint";
                                        Console.WriteLine("works");
                                    }
                                    if (ApplicationNamed.Contains("Excel"))
                                    {
                                        ApplicationNamed = "Excel";
                                        Console.WriteLine("works");
                                    }
                                    if (ApplicationNamed.Contains("Access"))
                                    {
                                        ApplicationNamed = "Access";
                                        Console.WriteLine("works");
                                    }
                                    if (ApplicationNamed.Contains("Outlook"))
                                    {
                                        ApplicationNamed = "Outlook";
                                        Console.WriteLine("works");
                                    }
                                    if (ApplicationNamed.Contains("Adobe XD"))
                                    {
                                        ApplicationNamed = "Adobe XD";
                                        Console.WriteLine("works");
                                    }
                                    if (ApplicationNamed.Contains("Adobe Acrobat"))
                                    {
                                        ApplicationNamed = "Adobe Acrobat";
                                        Console.WriteLine("works");
                                    }
                                    if (ApplicationNamed.Contains("Microsoft Teams"))
                                    {
                                        ApplicationNamed = "Microsoft Teams";
                                        Console.WriteLine("works");
                                    }
                                    if (!String.IsNullOrEmpty(ApplicationNamed))
                                    {
                                        var records = new List<ScreentimeStats>
                                    {
                                    new ScreentimeStats { ApplicationName = ApplicationNamed, ScreenTimeCollect = TimerDuration, DateCollected = DateCollected}
                                    };
                                        if (Fileexists == false)
                                        {
                                            using (var writer = new StreamWriter("C:\\Users\\brend\\source\\repos\\TrackIt\\TrackIt\\Storage7.csv"))
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
                                            using (var stream = File.Open("C:\\Users\\brend\\source\\repos\\TrackIt\\TrackIt\\Storage7.csv", FileMode.Append))
                                            using (var writer = new StreamWriter(stream))
                                            using (var csv = new CsvWriter(writer, config))
                                            {
                                                csv.WriteRecords(records);
                                            }
                                        }
                                        ScreenTimer.Restart();
                                        CurrentWindow = NewOpenWindow;
                                        OldOpenWindow = NewOpenWindow;
                                    }
                                }
                                if (textLength <= 0 & !String.IsNullOrEmpty(NonNullName))
                                {
                                    if (NonNullName.Contains("Google Chrome"))
                                    {
                                        NonNullName = "Google Chrome";
                                        Console.WriteLine("works");
                                    }
                                    if (NonNullName.Contains("Notepad++"))
                                    {
                                        NonNullName = "Notepad++";
                                        Console.WriteLine("works");
                                    }
                                    if (NonNullName.Contains("TrackIt"))
                                    {
                                        NonNullName = "TrackIt";
                                        Console.WriteLine("works");
                                    }
                                    if (NonNullName.Contains("Edge"))
                                    {
                                        NonNullName = "Microsoft Edge";
                                        Console.WriteLine("works");
                                    }
                                    if (NonNullName.Contains("Firefox"))
                                    {
                                        NonNullName = "Firefox";
                                        Console.WriteLine("works");
                                    }
                                    if (NonNullName.Contains("Discord"))
                                    {
                                        NonNullName = "Discord";
                                        Console.WriteLine("works");
                                    }
                                    if (NonNullName.Contains("Opera"))
                                    {
                                        NonNullName = "Opera";
                                        Console.WriteLine("works");
                                    }
                                    if (NonNullName.Contains("OneNote"))
                                    {
                                        NonNullName = "OneNote";
                                        Console.WriteLine("works");
                                    }
                                    if (NonNullName.Contains("Word"))
                                    {
                                        NonNullName = "Microsoft Word";
                                        Console.WriteLine("works");
                                    }
                                    if (NonNullName.Contains("PowerPoint"))
                                    {
                                        NonNullName = "Powerpoint";
                                        Console.WriteLine("works");
                                    }
                                    if (NonNullName.Contains("Excel"))
                                    {
                                        NonNullName = "Excel";
                                        Console.WriteLine("works");
                                    }
                                    if (NonNullName.Contains("Access"))
                                    {
                                        NonNullName = "Access";
                                        Console.WriteLine("works");
                                    }
                                    if (NonNullName.Contains("Outlook"))
                                    {
                                        NonNullName = "Outlook";
                                        Console.WriteLine("works");
                                    }
                                    if (NonNullName.Contains("Adobe XD"))
                                    {
                                        NonNullName = "Adobe XD";
                                        Console.WriteLine("works");
                                    }
                                    if (NonNullName.Contains("Adobe Acrobat"))
                                    {
                                        NonNullName = "Adobe Acrobat";
                                        Console.WriteLine("works");
                                    }
                                    if (NonNullName.Contains("Microsoft Teams"))
                                    {
                                        NonNullName = "Microsoft Teams";
                                        Console.WriteLine("works");
                                    }
                                    var records = new List<ScreentimeStats>
                                    {
                                    new ScreentimeStats { ApplicationName = NonNullName, ScreenTimeCollect = TimerDuration, DateCollected = DateCollected}
                                    };
                                    if (Fileexists == false)
                                    {
                                        using (var writer = new StreamWriter("C:\\Users\\brend\\source\\repos\\TrackIt\\TrackIt\\Storage7.csv"))
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
                                        using (var stream = File.Open("C:\\Users\\brend\\source\\repos\\TrackIt\\TrackIt\\Storage7.csv", FileMode.Append))
                                        using (var writer = new StreamWriter(stream))
                                        using (var csv = new CsvWriter(writer, config))
                                        {
                                            csv.WriteRecords(records);
                                        }
                                    }
                                    ScreenTimer.Restart();
                                    CurrentWindow = NewOpenWindow;
                                    OldOpenWindow = NewOpenWindow;
                                }
                            }                     
                    }
                    Dictionary<string, ScreentimeStats> mergedRecords = new Dictionary<string, ScreentimeStats>();
                    using (var reader = new StreamReader("C:\\Users\\brend\\source\\repos\\TrackIt\\TrackIt\\Storage7.csv"))
                    using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                    {
                        var records = csv.GetRecords<ScreentimeStats>().ToList(); ;
                        List<ScreentimeStats> alist = records.ToList();
                        foreach (ScreentimeStats record in records)
                        {
                            string key = record.ApplicationName + record.DateCollected.Date.ToString();
                            if (mergedRecords.ContainsKey(key))
                            {
                                mergedRecords[key].ScreenTimeCollect += record.ScreenTimeCollect;
                            }
                            else
                            {
                                mergedRecords.Add(key, record);
                            }
                        }
                    }
                    using (var writer = new StreamWriter("C:\\Users\\brend\\source\\repos\\TrackIt\\TrackIt\\Storage7.csv"))
                    using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
                    {
                        csv.WriteRecords(mergedRecords.Values);
                    }
                    var limitRecords = new Dictionary<string, long>(); // Dictionary to store the limits
                    using (var reader = new StreamReader("C:\\Users\\brend\\source\\repos\\TrackIt\\TrackIt\\Limits.csv"))
                    using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                    {
                        var limits = csv.GetRecords<ScreentimeLimits>();
                        foreach (var limit in limits)
                        {
                            limitRecords[limit.ApplicationName] = limit.ScreenTimeLimit;
                        }
                    }

                    var currentDate = DateTime.Today;
                    var usageRecords = new Dictionary<string, long>();
                    using (var reader = new StreamReader("C:\\Users\\brend\\source\\repos\\TrackIt\\TrackIt\\Storage7.csv"))
                    using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                    {
                        var usages = csv.GetRecords<ScreentimeStats>()
                                         .Where(record => record.DateCollected.Date == currentDate);
                        foreach (var usage in usages)
                        {
                            if (usageRecords.ContainsKey(usage.ApplicationName))
                            {
                                usageRecords[usage.ApplicationName] += usage.ScreenTimeCollect;
                            }
                            else
                            {
                                usageRecords[usage.ApplicationName] = usage.ScreenTimeCollect;
                            }
                        }
                    }

                    foreach (var usageRecord in usageRecords)
                    {
                        string applicationName = usageRecord.Key;
                        long screenTime = usageRecord.Value;

                        if (limitRecords.ContainsKey(applicationName))
                        {
                            IntPtr CurrentWindow1 = GetForegroundWindow();
                            long screenTimeLimit = limitRecords[applicationName];
                            int textLength = GetWindowTextLength(CurrentWindow1);
                            StringBuilder ApplicationNames = new StringBuilder(textLength + 1);
                            GetWindowText(CurrentWindow1, ApplicationNames, ApplicationNames.Capacity);
                            String ApplicationNamed = ApplicationNames.ToString();
                            if (screenTime > screenTimeLimit & ApplicationNamed == applicationName)
                            {
                                Console.WriteLine($"Screen time limit exceeded for {applicationName}.");
                                const int WM_CLOSE = 0x10;
                                SendMessage(CurrentWindow1, WM_CLOSE, 0, 0);
                                Process.Start("TimeLimitExceeded.exe");
                            }
                        }
                    }
                }
                catch
                {
                    Console.WriteLine("An exception occurred");
                }
            }
        }
        public void Start()
        {
            t.Start();
        }

        public void stop()
        {
            t.Stop();
        }
    }
}


