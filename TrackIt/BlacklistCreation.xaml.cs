using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
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
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class BlacklistCreation : Window
    {
        public BlacklistCreation()
        {
            InitializeComponent();
            ListofApplications();
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
