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
using System.Threading;
using System.Windows.Threading;
using System.Diagnostics;

namespace CashCat
{

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private FileSystemOperation fileOperations;
        private static readonly HttpClient client = new HttpClient();
        public FileInfo[] filesToEncrypt;
        public List<FileInfo> filesToDecrypt;
        public string currentPath;

        private bool encryptionInProgress = false;
        private string inProgressMessage = "Your important files are being encrypted!" + System.Environment.NewLine + System.Environment.NewLine + "Your files are being locked using a unique public RSA-4096 generated for this computer. Once the encryption operations are completed you will be unable to access your files without obtaining a special key from our internet dark web cyber server." + System.Environment.NewLine + System.Environment.NewLine + "Be prepared to pay the Ransom!";
        private string completedMessage = "Your important files are now encrypted!" + System.Environment.NewLine + System.Environment.NewLine + "To decrypt files you need to obtain the private key. The Single copy of the private key which allow you to decrypt the files is on a secret server on the internet dark web. The server will destroy the key after a time specified in this window." + System.Environment.NewLine + System.Environment.NewLine + "To obtain the private key for this computer, you need dot pay 300 USD / 300 EUR similar amount in other currency.";
        private string inProgressDecryptingMessage = "Your important files are being decrypted!" + System.Environment.NewLine + System.Environment.NewLine + "Congratulations! Your files are being unlocked using the unique public RSA-4096 generated for this computer.";
        private string decryptedMessage = "Your important files are now decrypted!" + System.Environment.NewLine + System.Environment.NewLine + "Thank You for being a great customer.";
        private string decryptedHeaderText = "Decrypted... have a nice day!";

        private ImageSource LockIconSource = new BitmapImage(new Uri(@"font-awesome_4-7-0_lock_100_0_ffffff_none.png", UriKind.Relative));
        private ImageSource UnLockIconSource = new BitmapImage(new Uri(@"font-awesome_4-7-0_unlock_100_0_ffffff_none.png", UriKind.Relative));


        public DateTime lockTime = DateTime.Now.AddDays(5);
        public DateTime priceTime = DateTime.Now.AddDays(1);

        private RansomFiles ransomwareDefinition = new RansomFiles();



        public MainWindow()
        {


            InitializeComponent();


            ShowHideCountDowns(false);


            //Time SEtup
            txtblk_fileLostTime.Text = lockTime.ToString();
            txtblk_paymentRaiseTime.Text = priceTime.ToString();

            System.Windows.Threading.DispatcherTimer CountDownTimer = new System.Windows.Threading.DispatcherTimer();
            CountDownTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            CountDownTimer.Interval = new TimeSpan(0, 0, 1);
            CountDownTimer.Start();


            fileOperations = new FileSystemOperation();

            fileOperations.WriteLog("CashCat Started!");
            fileOperations.WriteLog("CashCat Searching for Config!");

            //Load Config JSON
            currentPath = AppDomain.CurrentDomain.BaseDirectory;
            ConfigurationFile CurrentConfig = new ConfigurationFile();
            //CurrentConfig = CurrentConfig.ConfigurationFileSetup(currentPath);

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

                        //  var client = new RestClient(webHookUri);

                        // var request = new RestRequest();

                        // execute the request
                        // IRestResponse response = client.Execute(request);
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


        }

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {


            Dispatcher.Invoke(new Action(() =>
            {
                TimeSpan timeLeft = (lockTime - DateTime.Now);
                string timeLeftDisplay = String.Format("{0}h:{1:D2}m:{2:D2}s", (int)timeLeft.TotalHours, timeLeft.Minutes, timeLeft.Seconds);
                txtblk_fileLostCountDown.Text = timeLeftDisplay;



                timeLeft = (priceTime - DateTime.Now);
                timeLeftDisplay = String.Format("{0}h:{1:D2}m:{2:D2}s", (int)timeLeft.TotalHours, timeLeft.Minutes, timeLeft.Seconds);
                txtblk_paymentRaiseCountDown.Text = timeLeftDisplay;


            }), DispatcherPriority.Normal);

            //throw new NotImplementedException();
        }

        private void Window_ContentRendered(object sender, EventArgs e)
        {

            ShowHideCountDowns(false);
            InitialLoading();

        }


        //TODO - Spin on Timer Tick.
        private void RotateIcon(Image image, int angle)
        {
            var transform = image.RenderTransform as RotateTransform;
            transform.Angle += angle;
        }

        private void SetIconRotation(Image image, int angle)
        {
            var transform = image.RenderTransform as RotateTransform;
            transform.Angle = angle;
        }


        private void InitialLoading()
        {
            fileOperations.WriteLog("Starting File Rename Operations!");

            Execute_EncryptionOperations();

            ShowHideCountDowns(true);

            //Restore Full Size
            Dispatcher.Invoke(new Action(() =>
            {
                    SetIconRotation(LockerIcon, 0);
                    txtBoxLockingFile.Text = "";
                    txtbox_Instructions.Text = completedMessage;
                    btn_send.Visibility = Visibility.Visible;
                    txtbox_Bitcoingaddess.Visibility = Visibility.Visible;
                    rectInstructionsBackground.Height = 445;
                    Height = 575;

            }), DispatcherPriority.ContextIdle);
            
        }

        private void Execute_EncryptionOperations()
        {
            Dispatcher.Invoke(new Action(() =>
            {
                SetIconRotation(LockerIcon, 0);
                txtBoxLockingFile.Text = "Scanning for Files";
                txtbox_Instructions.Text = inProgressMessage;
                rectInstructionsBackground.Height = 335;
                Height = 455;
            }), DispatcherPriority.ContextIdle);

            filesToEncrypt = fileOperations.GetTXTFileCount(currentPath);
            fileOperations.WriteLog("Found: " + filesToEncrypt.Count() + " to Encrypt!");

            if (filesToEncrypt.Count() != 0)
            {

                //Get a new extension for this run
                string newExtension = ransomwareDefinition.GetRandomFileExtension();

                btn_send.Visibility = Visibility.Hidden;
                txtbox_Bitcoingaddess.Visibility = Visibility.Hidden;
                txtbox_Instructions.Height = 280;

                encryptionInProgress = true;

                foreach (FileInfo file in filesToEncrypt)
                {
                    FileInfo fileToEncrypt = file;
                    fileOperations.WriteLog("Encrypting: " + file.Name.ToString() + "!");

                    Dispatcher.Invoke(new Action(() =>
                    {
                        RotateIcon(LockerIcon, 5);
                        txtBoxLockingFile.Text = ("Encrypting: " + System.Environment.NewLine + file.Name);

                    }), DispatcherPriority.ContextIdle);


                    fileOperations.LockTXTFile(fileToEncrypt, newExtension);
                    Thread.Sleep(50);

                }
            }

            encryptionInProgress = false;

            fileOperations.WriteLog("File Rename Operations COMPLETED!");
        }

        private void Execute_DecryptionOperations()
        {
            ShowHideCountDowns(false);

            filesToDecrypt = fileOperations.GetRansomedFileCount(currentPath);   

            if (filesToDecrypt.Count() != 0)
            {

                Dispatcher.Invoke(new Action(() =>
                {
                    SetIconRotation(LockerIcon, 0);
                    rectInstructionsBackground.Height = 335;
                    Height = 455;
                    btn_send.Visibility = Visibility.Hidden;
                    txtbox_Bitcoingaddess.Visibility = Visibility.Hidden;
                    txtbox_Instructions.Text = inProgressDecryptingMessage;
                }), DispatcherPriority.ContextIdle);

                encryptionInProgress = true;

                foreach (FileInfo file in filesToDecrypt)
                {
                    FileInfo fileToDEcrypt = file;
                    fileOperations.WriteLog("Decrypting: " + file.Name.ToString() + "!");

                    Dispatcher.Invoke(new Action(() =>
                    {
                        LockerIcon.Source = UnLockIconSource;
                        RotateIcon(LockerIcon, -5);
                        txtBoxLockingFile.Text = ("Decrypting: " + System.Environment.NewLine + file.Name);

                    }), DispatcherPriority.ContextIdle);

                    fileOperations.UnlockRansomwareFile(fileToDEcrypt);

                }
            }

            Dispatcher.Invoke(new Action(() =>
            {
                SetIconRotation(LockerIcon, 0);
                txtBoxLockingFile.Text = "";
                txtbox_Instructions.Text = decryptedMessage;
                lblMainLabel.Content = decryptedHeaderText;
                btn_send.Visibility = Visibility.Hidden;
                txtbox_Bitcoingaddess.Visibility = Visibility.Hidden;
            }), DispatcherPriority.ContextIdle);

            fileOperations.WriteLog("Unlock Rename Operations Completed!");
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {

            string unlockCode = txtbox_Bitcoingaddess.Text;
            if (txtbox_Bitcoingaddess.Text == "123456789" || txtbox_Bitcoingaddess.Text == "987654321" || txtbox_Bitcoingaddess.Text == "12345" || txtbox_Bitcoingaddess.Text == "69420" || txtbox_Bitcoingaddess.Text == "42069" || txtbox_Bitcoingaddess.Text == "69" || txtbox_Bitcoingaddess.Text == "420")
            {
                fileOperations.WriteLog("Starting Unlock Rename Operations!");
                MessageBox.Show("Unlocked! Thanks! Your files will now be decrypted", "You did it correct", MessageBoxButton.OK);

                Execute_DecryptionOperations();

            }
            else
            {
                fileOperations.WriteLog("Failed Password Entry!");
                MessageBox.Show("Denied, incorrect passkey! (HINT: try: 123456789)", "You did it wrong", MessageBoxButton.OK);
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

        private void RichTextBox_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Process.Start("https://github.com/leeberg/CashCatRansomwareSimulator");
        }

        private void Run_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Process.Start("https://github.com/leeberg/CashCatRansomwareSimulator");
        }


        private void ShowHideCountDowns(bool state)
        {
            if(state == true)
            {
                rect_losttime.Visibility = Visibility.Visible;
                rect_pricetime.Visibility = Visibility.Visible;

                txtblk_paymentRaiseCountDown.Visibility = Visibility.Visible;
                txtblk_paymentRaiseHeader.Visibility = Visibility.Visible;
                txtblk_paymentRaiseTime.Visibility = Visibility.Visible;
                txtblk_paymentRaiseLeftLabel.Visibility = Visibility.Visible;

                txtblk_fileLostCountDown.Visibility = Visibility.Visible;
                txtblk_fileLostHeader.Visibility = Visibility.Visible;
                txtblk_fileLostLeftLabel.Visibility = Visibility.Visible;
                txtblk_fileLostTime.Visibility = Visibility.Visible;
            }
            else
            {
                rect_losttime.Visibility = Visibility.Hidden;
                rect_pricetime.Visibility = Visibility.Hidden;

                txtblk_paymentRaiseCountDown.Visibility = Visibility.Hidden;
                txtblk_paymentRaiseHeader.Visibility = Visibility.Hidden;
                txtblk_paymentRaiseTime.Visibility = Visibility.Hidden;
                txtblk_paymentRaiseLeftLabel.Visibility = Visibility.Hidden;

                txtblk_fileLostCountDown.Visibility = Visibility.Hidden;
                txtblk_fileLostHeader.Visibility = Visibility.Hidden;
                txtblk_fileLostLeftLabel.Visibility = Visibility.Hidden;
                txtblk_fileLostTime.Visibility = Visibility.Hidden;
            }
        }
    }
}
