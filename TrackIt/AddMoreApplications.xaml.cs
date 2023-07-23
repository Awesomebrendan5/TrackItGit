using CsvHelper.Configuration;
using CsvHelper;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using static System.Net.Mime.MediaTypeNames;

namespace TrackIt
{
    public class ApplicationsNotToMonitor
    {
        public string Apps { get; set; }
    }
    public partial class AddMoreApplications : Window
    {
        bool Fileexists;
        public AddMoreApplications()
        {
            InitializeComponent();
            Screenscale();
        }

        void Screenscale()
        {
            if (SystemParameters.PrimaryScreenHeight != 1080)
            {
                MinHeight = SystemParameters.PrimaryScreenHeight * (740.0 / 1080.0);
                MinWidth = SystemParameters.PrimaryScreenWidth * (428.0 / 1920);

                AddMore.SetValue(Canvas.TopProperty, 10 * (SystemParameters.PrimaryScreenHeight / 1080));
                AddMore.Height = SystemParameters.PrimaryScreenHeight * 0.0445;
                AddMore.Width = SystemParameters.PrimaryScreenWidth * 0.2063;
                AddMore.SetValue(Canvas.LeftProperty, 16 * (SystemParameters.PrimaryScreenWidth / 1920));
                AddMore.FontSize = (30 * SystemParameters.PrimaryScreenHeight / 1080);

                EnterApplication.SetValue(Canvas.TopProperty, 118 * (SystemParameters.PrimaryScreenHeight / 1080));
                EnterApplication.Height = SystemParameters.PrimaryScreenHeight * 0.0445;
                EnterApplication.Width = SystemParameters.PrimaryScreenWidth * 0.1208;
                EnterApplication.SetValue(Canvas.LeftProperty, 98 * (SystemParameters.PrimaryScreenWidth / 1920));
                EnterApplication.FontSize = (20 * SystemParameters.PrimaryScreenHeight / 1080);

                InputApplication.SetValue(Canvas.TopProperty, 171 * (SystemParameters.PrimaryScreenHeight / 1080));
                InputApplication.Height = SystemParameters.PrimaryScreenHeight * 0.0426;
                InputApplication.Width = SystemParameters.PrimaryScreenWidth * 0.0875;
                InputApplication.SetValue(Canvas.LeftProperty, 125 * (SystemParameters.PrimaryScreenWidth / 1920));

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
            }
        }
        private void ConfirmButtonClick(object sender, RoutedEventArgs e)
        {
            if (InputApplication.Text != null)
            {
                string ApplicationNamed = InputApplication.Text;
                string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                string directoryPath = System.IO.Path.Combine(documentsPath, "TrackIt");
                string FilePath = System.IO.Path.Combine(directoryPath, "ApplicationsNotToTrack.csv");
                if (File.Exists(FilePath))
                {
                    Fileexists = true;
                }
                else
                {
                    Fileexists = false;
                }
                var records = new List<ApplicationsNotToMonitor>()
                {

                new ApplicationsNotToMonitor { Apps = ApplicationNamed}
                };
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
                        csv.WriteRecords(records);
                    }
                    var newForm = new ApplicationAdded();
                    newForm.Show();
                    this.Close();
                }
                if (Fileexists == false)
                {
                    Directory.CreateDirectory(directoryPath);
                    using (var writer = new StreamWriter(FilePath))
                    using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
                    {
                        csv.WriteRecords(records);
                    }
                    Fileexists = true;
                    var newForm = new ApplicationAdded();
                    newForm.Show();
                    this.Close();
                }
            }
        }
        private void BackButtonClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            Properties.Settings.Default.MiniWindowOpened1 = false;
            Properties.Settings.Default.Save();
        }
    }
}
