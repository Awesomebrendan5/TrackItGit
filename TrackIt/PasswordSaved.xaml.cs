using System.Windows;
using System.Windows.Controls;

namespace TrackIt
{
    /// <summary>
    /// Interaction logic for PasswordSaved.xaml
    /// </summary>
    public partial class PasswordSaved : Window
    {
        public PasswordSaved()
        {
            InitializeComponent();
            Screenscale();
        }
        void Screenscale()
        {
            if (SystemParameters.PrimaryScreenHeight != 1080 | SystemParameters.PrimaryScreenWidth != 1920)
            {
                MinHeight = SystemParameters.PrimaryScreenHeight * (282.0 / 1080.0);
                MinWidth = SystemParameters.PrimaryScreenWidth * (461.0 / 1920);

                SaveMessage.SetValue(Canvas.TopProperty, 10 * (SystemParameters.PrimaryScreenHeight / 1080));
                SaveMessage.Height = SystemParameters.PrimaryScreenHeight * 0.0444;
                SaveMessage.Width = SystemParameters.PrimaryScreenWidth * 0.1339;
                SaveMessage.SetValue(Canvas.LeftProperty, 102 * (SystemParameters.PrimaryScreenWidth / 1920));
                SaveMessage.FontSize = (30 * SystemParameters.PrimaryScreenHeight / 1080);

                Information.SetValue(Canvas.TopProperty, 85 * (SystemParameters.PrimaryScreenHeight / 1080));
                Information.Height = SystemParameters.PrimaryScreenHeight * 0.035185;
                Information.Width = SystemParameters.PrimaryScreenWidth * 0.143229;
                Information.SetValue(Canvas.LeftProperty, 93 * (SystemParameters.PrimaryScreenWidth / 1920));
                Information.FontSize = (20 * SystemParameters.PrimaryScreenHeight / 1080);

                Exit.SetValue(Canvas.TopProperty, 191 * (SystemParameters.PrimaryScreenHeight / 1080));
                Exit.Height = SystemParameters.PrimaryScreenHeight * 0.05;
                Exit.Width = SystemParameters.PrimaryScreenWidth * 0.0729;
                Exit.SetValue(Canvas.LeftProperty, 160 * (SystemParameters.PrimaryScreenWidth / 1920));
                Exit.FontSize = (25 * SystemParameters.PrimaryScreenHeight / 1080);
            }
        }
        private void ExitButtonClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
