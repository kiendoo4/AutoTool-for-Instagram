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

namespace Instagram_tool
{
    /// <summary>
    /// Interaction logic for FastPost.xaml
    /// </summary>
    public partial class FastPost : Window
    {
        public FastPost()
        {
            InitializeComponent();
        }
        private void rad_invidual_Checked(object sender, RoutedEventArgs e)
        {
            frame_FastPost.Navigate(new FastPost_invidual(tbx_path.Text));
        }

        private void rad_multiple_Checked(object sender, RoutedEventArgs e)
        {
            frame_FastPost.Navigate(new FastPost_multiple(tbx_path.Text));
        }

        private void frame_FastPost_Loaded(object sender, RoutedEventArgs e)
        {
            rad_invidual.IsChecked = true;
        }
    }
}
