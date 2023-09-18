using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Win32;
using UserControl = System.Windows.Controls.UserControl;

namespace Workflower.App.Wpf.Controls
{
    /// <summary>
    /// Interaction logic for Menu.xaml
    /// </summary>
    public partial class Menu : UserControl
    {
        public Menu()
        {
            InitializeComponent();
        }

        private void Refresh_Click(object sender, EventArgs e)
        {
            MainWindow.Reload();
        }

        private void ChangeProject_Click(object sender, EventArgs e)
        {
            var openFileDialog = new FolderBrowserDialog();
          
            var result = openFileDialog.ShowDialog();

            if (result == DialogResult.OK)
            {
                MainWindow.Reload(openFileDialog.SelectedPath);
            }

        }
    }
}
