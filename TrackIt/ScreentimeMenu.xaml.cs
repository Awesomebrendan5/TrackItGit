using System;
using System.Text;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Runtime.InteropServices;
using CommunityToolkit.Mvvm.ComponentModel;
using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using SkiaSharp;
using static TrackIt.ScreentimeMenu;

namespace TrackIt
{

    /// <summary>
    /// Interaction logic for ScreentimeMenu.xaml
    /// </summary>
    public partial class ScreentimeMenu : Window
    {
        [DllImport("user32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        static extern IntPtr GetForegroundWindow();
        [DllImport("user32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        static extern int GetWindowText(IntPtr hWnd, StringBuilder lpString, int nMaxCount);
        [DllImport("user32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        static extern int GetWindowTextLength(IntPtr hWnd);


        public ScreentimeMenu()
        {
            InitializeComponent();
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            WindowState = WindowState.Maximized;
            ScreenScale();
            ScreenTimeStat();
        }

        public record ScreentimeStats(String ApplicationName, long ScreenTimeCollect, DateTime DateCollected);
        void ScreenScale()
        {
            if (SystemParameters.PrimaryScreenHeight != 1080)
            {
                Line.Height = SystemParameters.PrimaryScreenHeight * 1;
                Line.Width = SystemParameters.PrimaryScreenWidth * 0.0031;
                Line.SetValue(Canvas.LeftProperty, 324.0 * (SystemParameters.PrimaryScreenWidth / 1920));

                CalendarIcon.SetValue(Canvas.TopProperty, 182.0 * (SystemParameters.PrimaryScreenHeight / 1080));
                CalendarIcon.Height = SystemParameters.PrimaryScreenHeight * 0.0963;
                CalendarIcon.Width = SystemParameters.PrimaryScreenWidth * 0.0443;
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

                ScreenTimeLabel.SetValue(Canvas.TopProperty, 24 * (SystemParameters.PrimaryScreenHeight / 1080));
                ScreenTimeLabel.Height = SystemParameters.PrimaryScreenHeight * 0.0565;
                ScreenTimeLabel.Width = SystemParameters.PrimaryScreenWidth * 0.1417;
                ScreenTimeLabel.SetValue(Canvas.LeftProperty, 947 * (SystemParameters.PrimaryScreenWidth / 1920));
                ScreenTimeLabel.FontSize = (50 * SystemParameters.PrimaryScreenHeight / 1080);

                AddApplicationLabel.SetValue(Canvas.TopProperty, 62 * (SystemParameters.PrimaryScreenHeight / 1080));
                AddApplicationLabel.Height = SystemParameters.PrimaryScreenHeight * 0.0639;
                AddApplicationLabel.Width = SystemParameters.PrimaryScreenWidth * 0.0823;
                AddApplicationLabel.SetValue(Canvas.LeftProperty, 1662 * (SystemParameters.PrimaryScreenWidth / 1920));
                AddApplicationLabel.FontSize = (27 * SystemParameters.PrimaryScreenHeight / 1080);

                BlacklistBox.SetValue(Canvas.TopProperty, 136 * (SystemParameters.PrimaryScreenHeight / 1080));
                BlacklistBox.Height = SystemParameters.PrimaryScreenHeight * 0.6991;
                BlacklistBox.Width = SystemParameters.PrimaryScreenWidth * 0.1417;
                BlacklistBox.SetValue(Canvas.LeftProperty, 1608 * (SystemParameters.PrimaryScreenWidth / 1920));

                AddApplicationButton.SetValue(Canvas.TopProperty, 812 * (SystemParameters.PrimaryScreenHeight / 1080));
                AddApplicationButton.Height = SystemParameters.PrimaryScreenHeight * 0.0481;
                AddApplicationButton.Width = SystemParameters.PrimaryScreenWidth * 0.1182;
                AddApplicationButton.SetValue(Canvas.LeftProperty, 1628 * (SystemParameters.PrimaryScreenWidth / 1920));
                AddApplicationButton.FontSize = (30 * SystemParameters.PrimaryScreenHeight / 1080);

                CreateDailyUseLimit.SetValue(Canvas.TopProperty, 964 * (SystemParameters.PrimaryScreenHeight / 1080));
                CreateDailyUseLimit.Height = SystemParameters.PrimaryScreenHeight * 0.0556;
                CreateDailyUseLimit.Width = SystemParameters.PrimaryScreenWidth * 0.1552;
                CreateDailyUseLimit.SetValue(Canvas.LeftProperty, 1592 * (SystemParameters.PrimaryScreenWidth / 1920));
                CreateDailyUseLimit.FontSize = (30 * SystemParameters.PrimaryScreenHeight / 1080);

                Chart.SetValue(Canvas.TopProperty, 97 * (SystemParameters.PrimaryScreenHeight / 1080));
                Chart.Height = SystemParameters.PrimaryScreenHeight * 0.3583;
                Chart.Width = SystemParameters.PrimaryScreenWidth * 0.3042;
                Chart.SetValue(Canvas.LeftProperty, 416 * (SystemParameters.PrimaryScreenWidth / 1920));
                Chart.FontSize = (30 * SystemParameters.PrimaryScreenHeight / 1080);
            }

        }
        void ScreenTimeStat()
        {
            IntPtr CurrentWindow = GetForegroundWindow(); //Saves the currently open Window.
            Int32 OpenApplication = CurrentWindow.ToInt32(); //Saves that window into an array.
            System.Timers.Timer t = new System.Timers.Timer(TimeSpan.FromSeconds(10).TotalSeconds); //Sets a system timer for 10 seconds.
            IntPtr OldOpenWindow = CurrentWindow; //Saves the currently open window as OldOpenWindow.
            Stopwatch ScreenTimer = new Stopwatch();
            ScreenTimer.Start();
            t.AutoReset = true; //Sets the timer to Autoreset.
            t.Elapsed += new System.Timers.ElapsedEventHandler(timer_Elapsed); //Starts a method if timer elapses.
            t.Start(); //Starts timer.


            void timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
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
                    ScreentimeStats i = new(ApplicationName: ApplicationNamed, ScreenTimeCollect: TimerDuration, DateCollected: DateCollected);
                    Properties.Settings.Default.ListofRecords.Add(i);
                    ScreenTimer.Restart();
                    CurrentWindow = NewOpenWindow;
                    OldOpenWindow = NewOpenWindow;
                }
            }
        }
        void CalendarButtonClick(object sender, RoutedEventArgs e)
        {
            if (Properties.Settings.Default.MiniWindowOpened == false)
            {
                var newForm = new CalendarMenu();
                newForm.Show();
                this.Close();
            }

        }
        void ScreenTimeButtonClick(object sender, RoutedEventArgs e)
        {
        }
        void PasswordButtonClick(object sender, RoutedEventArgs e)
        {
            if (Properties.Settings.Default.MiniWindowOpened == false)
            {
                var newForm = new PasswordMenu();
                newForm.Show();
                this.Close();
            }
        }
        void HomeButtonClick(object sender, RoutedEventArgs e)
        {
            if (Properties.Settings.Default.MiniWindowOpened == false)
            {
                var newForm = new MainWindow();
                newForm.Show();
                this.Close();
            }
        }
        private void AddApplicationClick(object sender, RoutedEventArgs e)
        {
            if (Properties.Settings.Default.MiniWindowOpened == false)
            {
                Properties.Settings.Default.MiniWindowOpened = true;
                var newForm = new ApplicationsNotToTrack();
                newForm.Show();
            }
        }
        private void CreateDailyUseClick(object sender, RoutedEventArgs e)
        {
            if (Properties.Settings.Default.MiniWindowOpened == false)
            {
                Properties.Settings.Default.MiniWindowOpened = true;
                var newForm = new DailyApplicationLimitCreator();
                newForm.Show();
            }
        }
    }


    namespace ViewModelsSamples.Bars.Basic
    {
        public partial class Test
        { 
            public Test()
            {
                Bob();
                void Bob()
                {
                    Properties.Settings.Default.ListofRecords.Sort((a, b) => a.ScreenTimeCollect.CompareTo(b.ScreenTimeCollect));
                    Properties.Settings.Default.Save();
                }
            }
        }
        public partial class ViewModel : ObservableObject
        {

            public ISeries[] Series { get; set; } =
            {

        new ColumnSeries<double>
        {
            //Name = (Properties.Settings.Default.ListofRecords[0].ApplicationName),
            Values = new double[] { 2, 5, 4 }
        },
        new ColumnSeries<double>
        {
            Name = "Ana",
            Values = new double[] { 3, 1, 6 }
        }
    };

            public Axis[] XAxes { get; set; } =
            {
        new Axis
        {
            Labels = new string[] { "Category 1", "Category 2", "Category 3" },
            LabelsRotation = 0,
            SeparatorsPaint = new SolidColorPaint(new SKColor(200, 200, 200)),
            SeparatorsAtCenter = false,
            TicksPaint = new SolidColorPaint(new SKColor(35, 35, 35)),
            TicksAtCenter = true
        }
    };
        }
    }
}
