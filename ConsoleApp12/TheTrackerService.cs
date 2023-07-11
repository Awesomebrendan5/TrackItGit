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


        public TheTrackerService()
        {
            bool Fileexists = false;
            IntPtr CurrentWindow = GetForegroundWindow(); //Saves the currently open Window.
            Int32 OpenApplication = CurrentWindow.ToInt32(); //Saves that window as an integer.
            IntPtr OldOpenWindow = CurrentWindow; //Saves the currently open window as OldOpenWindow.
            Stopwatch ScreenTimer = new Stopwatch();
            Startup();
            ScreenTimer.Start();
            String path = "C:\\Users\\brend\\source\\repos\\TrackIt\\TrackIt\\Storage6.csv";
            if (File.Exists(path))
            {
                Fileexists = true;
            }
            else
            {
                Fileexists = false;
            }

            System.Timers.Timer t = new System.Timers.Timer(10000) { AutoReset = true };
            t.Start();
            t.Elapsed += OnEventExecution;
            Console.ReadLine();
            void Startup()
            {
                String path = AppDomain.CurrentDomain.BaseDirectory;
                RegistryKey key = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
                key.SetValue("Trackit", path + "ConsoleApp12.exe");
            }
            void OnEventExecution(object sender, System.Timers.ElapsedEventArgs e)
            {
                Console.WriteLine("Ran" + DateTime.Now);
                try
                {

                    {
                        IntPtr NewOpenWindow = GetForegroundWindow(); //Defines NewOpenWindow as the window now open.
                        if (OldOpenWindow == NewOpenWindow) //Checks if the OldOpenWindow is the same as the NewOpenWindow.
                        {
                            int textLength = GetWindowTextLength(CurrentWindow);
                            StringBuilder ApplicationName = new StringBuilder(textLength + 1);
                            GetWindowText(CurrentWindow, ApplicationName, ApplicationName.Capacity);
                            String ApplicationNamed = ApplicationName.ToString();
                            NonNullName = ApplicationNamed;
                        }
                        if (OldOpenWindow != NewOpenWindow) //Checks if the OldOpenWindow is different to the NewOpenWindow. 
                        {
                            int textLength = GetWindowTextLength(CurrentWindow);
                            StringBuilder ApplicationName = new StringBuilder(textLength + 1);
                            GetWindowText(CurrentWindow, ApplicationName, ApplicationName.Capacity);
                            DateTime DateCollected = DateTime.Now;
                            long TimerDuration = ScreenTimer.ElapsedMilliseconds;
                            String ApplicationNamed = ApplicationName.ToString();
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
                                        using (var writer = new StreamWriter("C:\\Users\\brend\\source\\repos\\TrackIt\\TrackIt\\Storage6.csv"))
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
                                        using (var stream = File.Open("C:\\Users\\brend\\source\\repos\\TrackIt\\TrackIt\\Storage6.csv", FileMode.Append))
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
                            if (textLength <= 0)
                            {
                                var records = new List<ScreentimeStats>
                                    {
                                    new ScreentimeStats { ApplicationName = NonNullName, ScreenTimeCollect = TimerDuration, DateCollected = DateCollected}
                                    };
                                if (Fileexists == false)
                                {
                                    using (var writer = new StreamWriter("C:\\Users\\brend\\source\\repos\\TrackIt\\TrackIt\\Storage6.csv"))
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
                                    using (var stream = File.Open("C:\\Users\\brend\\source\\repos\\TrackIt\\TrackIt\\Storage6.csv", FileMode.Append))
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


