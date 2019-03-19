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
            fileOperations = new FileSystemOperation();

            fileOperations.WriteLog("CashCat Started!");


            fileOperations.WriteLog("CashCat Searching for Config!");

            //Load Config JSON
            string currentPath = AppDomain.CurrentDomain.BaseDirectory;
            ConfigurationFile CurrentConfig = new ConfigurationFile();
            CurrentConfig = CurrentConfig.ConfigurationFileSetup(currentPath);

            if (CurrentConfig != null)
            {
                fileOperations.WriteLog("CashCat found a Config!");

                //If Webhookenabled is true..
                if (CurrentConfig.webHookEnabled)
                {
                    fileOperations.WriteLog("Executing Launch WebHook!");
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
                    fileOperations.WriteLog("ENABLING CAT MODE!");
                    lblMainLabel.Content = "CashCat has encrypted your files!";
                    LockerIcon.Visibility = Visibility.Collapsed;
                    maingrid.Background = new SolidColorBrush(Colors.Transparent);
                    CashCatBackground.Visibility = Visibility.Visible;

                }

            }
            else
            {
                fileOperations.WriteLog("CashCat DID NOT FIND a Config - using default settings!");
            }



            fileOperations.WriteLog("Starting File Rename Operations!");
            //lock it Up!
            
            fileOperations.RenameTXTFiles(currentPath);

            fileOperations.WriteLog("File Rename Operations COMPLETED!");

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            if (txtbox_Bitcoingaddess.Text == "123456789")
            {
                fileOperations.WriteLog("Starting Unlock Rename Operations!");
                fileOperations.UnlockLockyFiles(AppDomain.CurrentDomain.BaseDirectory);
                fileOperations.WriteLog("Unlock Rename Operations Completed!");
                MessageBox.Show("Unlocked! Thanks!", "You did it correct", MessageBoxButton.OK);
                

            }
            else
            {
                fileOperations.WriteLog("Failed Password Entry!");
                MessageBox.Show("Denied!", "You did it wrong", MessageBoxButton.OK);
            }

        }

        private void txtbox_Bitcoingaddess_GotFocus(object sender, RoutedEventArgs e)
        {
            fileOperations.WriteLog("User Focused on BitcoinAddressField!");
            //Clear Text On Click
            if (txtbox_Bitcoingaddess.Text == "Unlock Code Here")
            {
                txtbox_Bitcoingaddess.Text = "";
            }
        }

    
    }
}
