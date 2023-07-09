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

            t = new System.Timers.Timer(10000) { AutoReset = true };
            t.Elapsed += OnEventExecution;
            Console.ReadLine();
            Startup();
            void Startup()
            {
                String path = AppDomain.CurrentDomain.BaseDirectory;
                RegistryKey key = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
                key.SetValue("Trackit", path + "ConsoleApp12.exe");
            }
            void OnEventExecution(object sender, System.Timers.ElapsedEventArgs e)
            {
                IntPtr NewOpenWindow = GetForegroundWindow(); //Defines NewOpenWindow as the window now open.
                if (OldOpenWindow == NewOpenWindow) //Checks if the OldOpenWindow is the same as the NewOpenWindow.
                {
                }
                if (OldOpenWindow != NewOpenWindow) //Checks if the OldOpenWindow is different to the NewOpenWindow. 
                {
                    int textLength = GetWindowTextLength(CurrentWindow);
                    StringBuilder ApplicationName = new StringBuilder(textLength + 1);
                    GetWindowText(CurrentWindow, ApplicationName, ApplicationName.Capacity);
                    DateTime DateCollected = DateTime.Now;
                    long TimerDuration = ScreenTimer.ElapsedMilliseconds;
                    String ApplicationNamed = ApplicationName.ToString();
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


