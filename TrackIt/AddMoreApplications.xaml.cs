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
        }
        private void ConfirmButtonClick(object sender, RoutedEventArgs e)
        {
            if (InputApplication.Text != null)
            {
                string ApplicationNamed = InputApplication.Text;
                String path = "C:\\Users\\brend\\source\\repos\\TrackIt\\TrackIt\\ApplicationsNotToTrack.csv";
                if (File.Exists(path))
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
                if (Fileexists == false)
                {
                    using (var writer = new StreamWriter("C:\\Users\\brend\\source\\repos\\TrackIt\\TrackIt\\ApplicationsNotToTrack.csv"))
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
                    using (var stream = File.Open("C:\\Users\\brend\\source\\repos\\TrackIt\\TrackIt\\ApplicationsNotToTrack.csv", FileMode.Append))
                    using (var writer = new StreamWriter(stream))
                    using (var csv = new CsvWriter(writer, config))
                    {
                        csv.WriteRecords(records);
                    }
                }

            }
        }
        private void BackButtonClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
