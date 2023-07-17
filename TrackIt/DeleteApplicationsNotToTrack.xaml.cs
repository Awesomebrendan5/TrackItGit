using System;
using System.IO;
using System.Collections.Generic;
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

namespace TrackIt
{
    /// <summary>
    /// Interaction logic for DeleteApplicationsNotToTrack.xaml
    /// </summary>
    public partial class DeleteApplicationsNotToTrack : Window
    {
        public DeleteApplicationsNotToTrack()
        {
            InitializeComponent();
        }
        private void ConfirmButtonClick(object sender, RoutedEventArgs e)
        {
            if (Properties.Settings.Default.Password == Password.Password)
            {
                string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                string directoryPath = System.IO.Path.Combine(documentsPath, "TrackIt");
                string FilePath = System.IO.Path.Combine(directoryPath, "ApplicationsNotToTrack.csv");
                File.Delete(FilePath);
                var newForm = new AppliationsNotToTrackRemoved();
                newForm.Show();
                this.Close();
            }
            if (Properties.Settings.Default.Password != Password.Password)
            {
                var newForm = new WrongPassword2();
                newForm.Show();
            }
        }
        private void BackButtonClick(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.MiniWindowOpened = false;
            this.Close();
        }
    }
}
