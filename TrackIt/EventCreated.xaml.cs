using System;
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
    /// Interaction logic for EventCreated.xaml
    /// </summary>
    public partial class EventCreated : Window
    {
        public EventCreated()
        {
            InitializeComponent();
            Properties.Settings.Default.MiniWindowOpened1 = true; //Set MiniWindowOpened1 to true.
            Properties.Settings.Default.MiniWindowOpened = true; //Set MiniWindowOpened to true.
            Screenscale();
        }
        void Screenscale()
        {
            if (SystemParameters.PrimaryScreenHeight != 1080) //Check that the screen resolution is different to default.
            {
                {
                    MinHeight = SystemParameters.PrimaryScreenHeight * (282.0 / 1080.0); //Set Minheight property.
                    MinWidth = SystemParameters.PrimaryScreenWidth * (461.0 / 1920); //Set MinWidth property.

                    SavedMessage.SetValue(Canvas.TopProperty, 10 * (SystemParameters.PrimaryScreenHeight / 1080));
                    SavedMessage.Height = SystemParameters.PrimaryScreenHeight * 0.0444;
                    SavedMessage.Width = SystemParameters.PrimaryScreenWidth * 0.1339;
                    SavedMessage.SetValue(Canvas.LeftProperty, 102 * (SystemParameters.PrimaryScreenWidth / 1920));
                    SavedMessage.FontSize = (30 * SystemParameters.PrimaryScreenHeight / 1080);

                    Information.SetValue(Canvas.TopProperty, 95 * (SystemParameters.PrimaryScreenHeight / 1080));
                    Information.Height = SystemParameters.PrimaryScreenHeight * 0.0491;
                    Information.Width = SystemParameters.PrimaryScreenWidth * (361.0 / 1920.0);
                    Information.SetValue(Canvas.LeftProperty, 50 * (SystemParameters.PrimaryScreenWidth / 1920));
                    Information.FontSize = (20 * SystemParameters.PrimaryScreenHeight / 1080);

                    Exit.SetValue(Canvas.TopProperty, 182 * (SystemParameters.PrimaryScreenHeight / 1080));
                    Exit.Height = SystemParameters.PrimaryScreenHeight * 0.05;
                    Exit.Width = SystemParameters.PrimaryScreenWidth * 0.0729;
                    Exit.SetValue(Canvas.LeftProperty, 160 * (SystemParameters.PrimaryScreenWidth / 1920));
                    Exit.FontSize = (25 * SystemParameters.PrimaryScreenHeight / 1080);
                }
            }
        }
        private void ExitButtonClick(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.MiniWindowOpened1 = false; //Set MiniWindowOpened1 to false.
            Properties.Settings.Default.MiniWindowOpened = false; //Set MiniWindowOpened to false.
            this.Close(); //Close the window.
        }
        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        { 
            Properties.Settings.Default.MiniWindowOpened1 = false; //Set MiniWindowOpened to false.
            Properties.Settings.Default.MiniWindowOpened = false; //Set MiniWindowOpened to false.
            Properties.Settings.Default.Save();
        }
    }
}
