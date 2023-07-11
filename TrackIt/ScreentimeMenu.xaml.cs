using System;
using System.Text;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Runtime.InteropServices;
using CommunityToolkit.Mvvm.ComponentModel;
using LiveChartsCore;
using LiveChartsCore.Defaults;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using SkiaSharp;
using static TrackIt.ScreentimeMenu;
using CsvHelper.Configuration;
using CsvHelper;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Timers;
using System.Collections;
using static System.Net.Mime.MediaTypeNames;
using System.Linq;
using LiveChartsCore.SkiaSharpView.Painting.Effects;
using System.Collections.ObjectModel;
using LiveChartsCore.Kernel.Sketches;
using System.Xml;

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
        }

        public class ScreentimeStats
        {
            public string ApplicationName { get; set; }
            public long ScreenTimeCollect { get; set; }
            public DateTime DateCollected { get; set; }
        }
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
                Chart.SetValue(Canvas.LeftProperty, 350 * (SystemParameters.PrimaryScreenWidth / 1920));
                Chart.FontSize = (30 * SystemParameters.PrimaryScreenHeight / 1080);

                Chart1.SetValue(Canvas.TopProperty, 97 * (SystemParameters.PrimaryScreenHeight / 1080));
                Chart1.Height = SystemParameters.PrimaryScreenHeight * 0.3583;
                Chart1.Width = SystemParameters.PrimaryScreenWidth * 0.3042;
                Chart1.SetValue(Canvas.LeftProperty, 1000 * (SystemParameters.PrimaryScreenWidth / 1920));
                Chart1.FontSize = (30 * SystemParameters.PrimaryScreenHeight / 1080);

                Chart2.SetValue(Canvas.TopProperty, 607 * (SystemParameters.PrimaryScreenHeight / 1080));
                Chart2.Height = SystemParameters.PrimaryScreenHeight * 0.3583;
                Chart2.Width = SystemParameters.PrimaryScreenWidth * 0.3042;
                Chart2.SetValue(Canvas.LeftProperty, 350 * (SystemParameters.PrimaryScreenWidth / 1920));
                Chart2.FontSize = (30 * SystemParameters.PrimaryScreenHeight / 1080);

                Chart3.SetValue(Canvas.TopProperty, 607 * (SystemParameters.PrimaryScreenHeight / 1080));
                Chart3.Height = SystemParameters.PrimaryScreenHeight * 0.3583;
                Chart3.Width = SystemParameters.PrimaryScreenWidth * 0.3042;
                Chart3.SetValue(Canvas.LeftProperty, 1000 * (SystemParameters.PrimaryScreenWidth / 1920));
                Chart3.FontSize = (30 * SystemParameters.PrimaryScreenHeight / 1080);
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
        public class ViewModel
        {
            static string message1 = Properties.Settings.Default.a;
            static string message2 = Properties.Settings.Default.b;
            static string message3 = Properties.Settings.Default.c;
            static string message4 = Properties.Settings.Default.d;
            static string message5 = Properties.Settings.Default.e;

            public ISeries[] Series { get; set; }
                = new ISeries[]
                {
                new ColumnSeries<int>
                {
                    Values = new[]
                    {
                        Convert.ToInt32(Properties.Settings.Default.avalue),
                        Convert.ToInt32(Properties.Settings.Default.bvalue),
                        Convert.ToInt32(Properties.Settings.Default.cvalue),
                        Convert.ToInt32(Properties.Settings.Default.dvalue),
                        Convert.ToInt32(Properties.Settings.Default.evalue)
                    },
                    MaxBarWidth = double.MaxValue
                }
                };

            public Axis[] XAxes { get; set; }
                = new Axis[]
                {
                new Axis
                {
                    Labels = new string[] { message1, message2, message3, message4, message5 }
                }
                };

            public Axis[] YAxes { get; set; }
                = new Axis[]
                {
                new Axis
                {
                }
                };
        }
    }
}

