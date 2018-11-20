using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Net.Http;
using RestSharp;

namespace CashCat
{

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private FileSystemOperation fileOperations;
        private static readonly HttpClient client = new HttpClient();

        public MainWindow()
        {
            InitializeComponent();

            string currentPath = AppDomain.CurrentDomain.BaseDirectory;


            try
            {
                //Load Config JSON
                ConfigurationFile CurrentConfig = new ConfigurationFile();
                CurrentConfig = CurrentConfig.ConfigurationFileSetup(currentPath);


                // Do Config Things....

                //If Webhookenabled is true..
                if (CurrentConfig.webHookEnabled)
                {
                    try
                    {
                        //Trigger Webhook
                        string webHookUri = CurrentConfig.webHookURI;

                        var client = new RestClient(webHookUri);

                        var request = new RestRequest();

                        // execute the request
                        IRestResponse response = client.Execute(request);
                    }
                    catch
                    {
                        // Something went wrong with the webhook
                    }



                }

                if (CurrentConfig.catMode)
                {
                    // Enable Cat Mode
                    lblMainLabel.Content = "CashCat has encrypted your files!";
                    LockerIcon.Visibility = Visibility.Collapsed;
                    maingrid.Background = new SolidColorBrush(Colors.Transparent);
                    CashCatBackground.Visibility = Visibility.Visible;

                }
            }
            catch
            {
                //Something went wrong loading / processing the config file.
            }
           
           

            //lock it Up!
            fileOperations = new FileSystemOperation();
            fileOperations.RenameTXTFiles(currentPath);


            
        }

        
        private void Button_Click(object sender, RoutedEventArgs e)
        {

            if (txtbox_Bitcoingaddess.Text == "123456789")
            {

                fileOperations.UnlockLockyFiles(AppDomain.CurrentDomain.BaseDirectory);
                MessageBox.Show("Unlocked! Thanks!", "You did it correct", MessageBoxButton.OK);

            }
            else
            {
                MessageBox.Show("Denied!", "You did it wrong", MessageBoxButton.OK);
            }

        }

        private void txtbox_Bitcoingaddess_GotFocus(object sender, RoutedEventArgs e)
        {
            //Clear Text On Click
            if (txtbox_Bitcoingaddess.Text == "Unlock Code Here")
            {
                txtbox_Bitcoingaddess.Text = "";
            }
        }

    
    }
}
