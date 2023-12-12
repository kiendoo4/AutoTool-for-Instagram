using DATH_IT008.UserControl;
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

namespace DATH_IT008.OtherWindows
{
    /// <summary>
    /// Interaction logic for Extension.xaml
    /// </summary>
    public partial class Extension : Window
    {
        public bool a, b;
        public Extension()
        {
            InitializeComponent();
        }
        private void HideClick(object sender, RoutedEventArgs e)
        {
            Window window = Application.Current.MainWindow as Window;
            window.WindowState = WindowState.Minimized;
        }

        private void CloseClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void FinishClick(object sender, RoutedEventArgs e)
        {
            if (First.IsChecked == true)
                a = true;
            else a = false;
            if (Second.IsChecked == true)
                b = true;
            else b = false; 
            this.Close();
        }
    }
}
