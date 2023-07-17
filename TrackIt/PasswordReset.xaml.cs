using System.Windows;
using System.Windows.Controls;

namespace TrackIt
{
    /// <summary>
    /// Interaction logic for PasswordReset.xaml
    /// </summary>
    public partial class PasswordReset : Window
    {
        public PasswordReset()
        {
            InitializeComponent();
            Screenscale();
        }
        void Screenscale()
        {
            if (SystemParameters.PrimaryScreenHeight != 1080)
            {
                MinHeight = SystemParameters.PrimaryScreenHeight * (282.0 / 1080.0);
                MinWidth = SystemParameters.PrimaryScreenWidth * (461.0 / 1920);

                SuccessMessage.SetValue(Canvas.TopProperty, 10 * (SystemParameters.PrimaryScreenHeight / 1080));
                SuccessMessage.Height = SystemParameters.PrimaryScreenHeight * 0.0444;
                SuccessMessage.Width = SystemParameters.PrimaryScreenWidth * 0.1339;
                SuccessMessage.SetValue(Canvas.LeftProperty, 102 * (SystemParameters.PrimaryScreenWidth / 1920));
                SuccessMessage.FontSize = (30 * SystemParameters.PrimaryScreenHeight / 1080);

                Information.SetValue(Canvas.TopProperty, 95 * (SystemParameters.PrimaryScreenHeight / 1080));
                Information.Height = SystemParameters.PrimaryScreenHeight * 0.0491;
                Information.Width = SystemParameters.PrimaryScreenWidth * 0.188021;
                Information.SetValue(Canvas.LeftProperty, 50 * (SystemParameters.PrimaryScreenWidth / 1920));
                Information.FontSize = (20 * SystemParameters.PrimaryScreenHeight / 1080);

                Exit.SetValue(Canvas.TopProperty, 182 * (SystemParameters.PrimaryScreenHeight / 1080));
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
