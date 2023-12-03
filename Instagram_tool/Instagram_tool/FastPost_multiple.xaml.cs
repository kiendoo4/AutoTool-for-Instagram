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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Instagram_tool
{
    /// <summary>
    /// Interaction logic for FastPost_multiple.xaml
    /// </summary>
    public partial class FastPost_multiple : Page
    {
        // init components
        private string path;
        private Abilities ab = new Abilities();

        public FastPost_multiple(string path)
        {
            InitializeComponent();
            this.path = path;
        }
    }
}
