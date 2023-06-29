using Microsoft.Win32;
using System.Windows;
using System.Windows.Controls;

namespace TrackIt
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class BlacklistCreation : Window
    {
        public BlacklistCreation()
        {
            InitializeComponent();
            ListofApplications();
            Screenscale();
        }

        void Screenscale()
        {
            if (SystemParameters.PrimaryScreenHeight != 1080)
            {
                Height = SystemParameters.PrimaryScreenHeight * 0.6667;
                Width = SystemParameters.PrimaryScreenWidth * 0.2229;

                BlacklistBox.SetValue(Canvas.TopProperty, 10 * (SystemParameters.PrimaryScreenHeight / 1080));
                BlacklistBox.Height = SystemParameters.PrimaryScreenHeight * 0.0445;
                BlacklistBox.Width = SystemParameters.PrimaryScreenWidth * 0.1688;
                BlacklistBox.SetValue(Canvas.LeftProperty, 52 * (SystemParameters.PrimaryScreenWidth / 1920));
                BlacklistBox.FontSize = (30 * SystemParameters.PrimaryScreenHeight / 1080);

                EnterBlacklist.SetValue(Canvas.TopProperty, 101 * (SystemParameters.PrimaryScreenHeight / 1080));
                EnterBlacklist.Height = SystemParameters.PrimaryScreenHeight * 0.0287;
                EnterBlacklist.Width = SystemParameters.PrimaryScreenWidth * 0.1354;
                EnterBlacklist.SetValue(Canvas.LeftProperty, 84 * (SystemParameters.PrimaryScreenWidth / 1920));
                EnterBlacklist.FontSize = (20 * SystemParameters.PrimaryScreenHeight / 1080);

                EnterBlacklistName.SetValue(Canvas.TopProperty, 137 * (SystemParameters.PrimaryScreenHeight / 1080));
                EnterBlacklistName.Height = SystemParameters.PrimaryScreenHeight * 0.0445;
                EnterBlacklistName.Width = SystemParameters.PrimaryScreenWidth * 0.1052;
                EnterBlacklistName.SetValue(Canvas.LeftProperty, 113 * (SystemParameters.PrimaryScreenWidth / 1920));

                SelectApplications.SetValue(Canvas.TopProperty, 216 * (SystemParameters.PrimaryScreenHeight / 1080));
                SelectApplications.Height = SystemParameters.PrimaryScreenHeight * 0.0519;
                SelectApplications.Width = SystemParameters.PrimaryScreenWidth * 0.1667;
                SelectApplications.SetValue(Canvas.LeftProperty, 54 * (SystemParameters.PrimaryScreenWidth / 1920));
                SelectApplications.FontSize = (20 * SystemParameters.PrimaryScreenHeight / 1080);

                Applications.SetValue(Canvas.TopProperty, 272 * (SystemParameters.PrimaryScreenHeight / 1080));
                Applications.Height = SystemParameters.PrimaryScreenHeight * 0.3;
                Applications.Width = SystemParameters.PrimaryScreenWidth * 0.1052;
                Applications.SetValue(Canvas.LeftProperty, 113 * (SystemParameters.PrimaryScreenWidth / 1920));
                Applications.FontSize = (20 * SystemParameters.PrimaryScreenHeight / 1080);

                Confirm.SetValue(Canvas.TopProperty, 611 * (SystemParameters.PrimaryScreenHeight / 1080));
                Confirm.Height = SystemParameters.PrimaryScreenHeight * 0.0398;
                Confirm.Width = SystemParameters.PrimaryScreenWidth * 0.0646;
                Confirm.SetValue(Canvas.LeftProperty, 152 * (SystemParameters.PrimaryScreenWidth / 1920));
                Confirm.FontSize = (20 * SystemParameters.PrimaryScreenHeight / 1080);

                Back.SetValue(Canvas.TopProperty, 651 * (SystemParameters.PrimaryScreenHeight / 1080));
                Back.Height = SystemParameters.PrimaryScreenHeight * 0.0398;
                Back.Width = SystemParameters.PrimaryScreenWidth * 0.0646;
                Back.SetValue(Canvas.LeftProperty, -4 * (SystemParameters.PrimaryScreenWidth / 1920));
                Back.FontSize = (20 * SystemParameters.PrimaryScreenHeight / 1080);
            }
        }

        private void EnterBlacklistName_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void ConfirmButtonClick(object sender, RoutedEventArgs e)
        {
            void SelectedItems(object sender, RoutedEventArgs e, string[] selectedText)
            {
                if (Applications.SelectedItem != null && EnterBlacklistName.SelectedText != null)
                {
                    int i = 0;
                    while (i < 0)
                    {
                        selectedText[i] = EnterBlacklistName.SelectedText;
                        Properties.Settings.Default.AppBlacklist[i] = selectedText;
                        int j = 0;
                        while (j < 0)
                        {
                            Properties.Settings.Default.AppBlacklist[i][j] = (string)Applications.SelectedItems[j];
                            j = j + 1;
                        }
                        i = i + 1;
                    }
                }
            }
        }
        private void BackButtonClick(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.MiniWindowOpened = false;
            this.Close();
        }
        void ListofApplications()
        {
            string registry_key = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall";
            using Microsoft.Win32.RegistryKey key = Registry.LocalMachine.OpenSubKey(registry_key);
            foreach (string subkey_name in key.GetSubKeyNames())
            {
                using RegistryKey subkey = key.OpenSubKey(subkey_name);
                Applications.Items.Add(subkey.GetValue("DisplayName"));
            }
        }
        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            Properties.Settings.Default.MiniWindowOpened = false;
            Properties.Settings.Default.Save();
        }

    }
}
