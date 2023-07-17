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
using Microsoft.Toolkit.Uwp.Notifications;
using System.Windows;

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
        bool applicationExistsInList;

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
        public class BlacklistsRecords
        {
            public string BlacklistName { get; set; }
            public string Applications { get; set; }
            
            public string DateRange { get; set; }
        }
        public class ApplicationsNotToMonitor
        {
            public string Apps { get; set; }
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
            string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            string FilePath = Path.Combine(documentsPath, "TrackIt", "ScreentimeData.csv");
            if (File.Exists(FilePath))
            {
                Fileexists = true;
            }
            else
            {
                Fileexists = false;
            }
            t = new System.Timers.Timer(10000) { AutoReset = true };
            Start();
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
                        if (ApplicationNamed.Contains("Google Chrome"))
                        {
                            ApplicationNamed = "Google Chrome";
                            Console.WriteLine("works");
                        }
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
                        Dictionary<string, ApplicationsNotToMonitor> DoNotMonitor = new Dictionary<string, ApplicationsNotToMonitor>();
                            string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                            string FilePath = Path.Combine(documentsPath, "TrackIt", "ApplicationsNotToTrack.csv");
                            using (var reader = new StreamReader(FilePath))
                            using (var csv1 = new CsvReader(reader, CultureInfo.InvariantCulture))
                            {
                            var records = csv1.GetRecords<ApplicationsNotToMonitor>().ToList();
                            List<ApplicationsNotToMonitor> alist = records.ToList();
                            applicationExistsInList = alist.Any(a => a.Apps == ApplicationNamed);
                            }
                        if (applicationExistsInList == false)
                        {
                            if (!ApplicationNamed.Contains("Lock Screen"))
                            {
                                if (textLength > 0)
                                {
                                    NonNullName = ApplicationNamed;

                                    if (!String.IsNullOrEmpty(ApplicationNamed))
                                    {
                                        var records = new List<ScreentimeStats>
                                    {
                                    new ScreentimeStats { ApplicationName = ApplicationNamed, ScreenTimeCollect = TimerDuration, DateCollected = DateCollected}
                                    };
                                        if (Fileexists == false)
                                        {
                                            string documentPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                                            string directoryPath = Path.Combine(documentsPath, "TrackIt");
                                            string ScreentimePath = Path.Combine(directoryPath, "ScreentimeData.csv");
                                            Directory.CreateDirectory(directoryPath);
                                            using (var writer = new StreamWriter(ScreentimePath))
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
                                            string documentPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                                            string directoryPath = Path.Combine(documentsPath, "TrackIt");
                                            string ScreentimePath = Path.Combine(directoryPath, "ScreentimeData.csv");
                                            using (var stream = File.Open(ScreentimePath, FileMode.Append))
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
                                    var records = new List<ScreentimeStats>
                                    {
                                    new ScreentimeStats { ApplicationName = NonNullName, ScreenTimeCollect = TimerDuration, DateCollected = DateCollected}
                                    };
                                    if (Fileexists == false)
                                    {
                                        string documentPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                                        string directoryPath = Path.Combine(documentsPath, "TrackIt");
                                        string ScreentimePath = Path.Combine(directoryPath, "ScreentimeData.csv");
                                        Directory.CreateDirectory(directoryPath);
                                        using (var writer = new StreamWriter(ScreentimePath))
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
                                        string documentPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                                        string directoryPath = Path.Combine(documentsPath, "TrackIt");
                                        string ScreentimePath = Path.Combine(directoryPath, "ScreentimeData.csv");
                                        using (var stream = File.Open(ScreentimePath, FileMode.Append))
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
                    }
                    string DocumentedPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                    string ScreentimesPath = Path.Combine(documentsPath, "TrackIt", "ScreentimeData.csv");
                    Dictionary<string, ScreentimeStats> mergedRecords = new Dictionary<string, ScreentimeStats>();
                    using (var reader = new StreamReader(ScreentimesPath))
                    using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                    {
                        var records = csv.GetRecords<ScreentimeStats>().ToList();
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
                    using (var writer = new StreamWriter(ScreentimesPath))
                    using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
                    {
                        csv.WriteRecords(mergedRecords.Values);
                    }
                    try
                    {
                        var limitRecords = new Dictionary<string, long>(); // Dictionary to store the limits
                        string UserDocumentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                        string LimitsPath = Path.Combine(documentsPath, "TrackIt", "Limits.csv");
                        using (var reader = new StreamReader(LimitsPath))
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
                        using (var reader = new StreamReader(ScreentimesPath))
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
                                    const int WM_CLOSE = 0x10;
                                    SendMessage(CurrentWindow1, WM_CLOSE, 0, 0);
                                    new ToastContentBuilder()
                                    .AddArgument("action", "viewConversation")
                                    .AddArgument("conversationId", 9813)
                                    .AddText($"Screen time limit exceeded for {applicationName}.")
                                    .AddText("Wait until tomorrow or turn off screentime limits.")
                                    .Show();
                                }
                                if (screenTimeLimit - screenTime < 600000 & screenTimeLimit - screenTime > 590001 & ApplicationNamed == applicationName)
                                {
                                    new ToastContentBuilder()
                                    .AddArgument("action", "viewConversation")
                                    .AddArgument("conversationId", 9813)
                                    .AddText($"Within 10 minutes of limit for {applicationName}.")
                                    .AddText("Wrap up what you are doing.")
                                    .Show();
                                }
                            }
                        }
                    }
                    catch
                    {

                    }
                    try
                    {
                        var currentDate1 = DateTime.Now;
                        var BlacklistRecords = new Dictionary<string, long>();
                        string BlacklistsDocumentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                        string BlacklistsPath = Path.Combine(documentsPath, "TrackIt", "BlacklistsCombined.csv");
                        using (var reader = new StreamReader(BlacklistsPath))
                        using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                        {
                            var Blacklists = csv.GetRecords<BlacklistsRecords>();
                            foreach (var blacklist in Blacklists)
                            {
                                var dateRange = blacklist.DateRange.Split('-');
                                if (dateRange.Length != 2)
                                {
                                    continue;
                                }

                                var startTime = DateTime.Parse(dateRange[0].Trim());
                                var endTime = DateTime.Parse(dateRange[1].Trim());

                                if (currentDate1 >= startTime && currentDate1 <= endTime)
                                {
                                    var lockedApplications = blacklist.Applications.Split(',');
                                    IntPtr CurrentWindow2 = GetForegroundWindow();
                                    int textLength = GetWindowTextLength(CurrentWindow2);
                                    StringBuilder ApplicationNames = new StringBuilder(textLength + 1);
                                    GetWindowText(CurrentWindow2, ApplicationNames, ApplicationNames.Capacity);
                                    String ApplicationNamed = ApplicationNames.ToString();
                                    foreach (var application in lockedApplications)
                                    {
                                        if (Convert.ToString(application) == ApplicationNamed)
                                        {
                                            const int WM_CLOSE = 0x10;
                                            SendMessage(CurrentWindow2, WM_CLOSE, 0, 0);
                                            new ToastContentBuilder()
                                            .AddArgument("action", "viewConversation")
                                            .AddArgument("conversationId", 9813)
                                            .AddText($"{application} is on the " + blacklist.BlacklistName + " blacklist")
                                            .AddText("please wait until " + endTime.TimeOfDay + ".")
                                            .Show();
                                        }
                                    }
                                }
                            }
                        }
                    }
                    catch
                    {

                    }
                }
                catch(Exception ex)
                {
                    Console.WriteLine("An exception occurred" + ex.Message);
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


