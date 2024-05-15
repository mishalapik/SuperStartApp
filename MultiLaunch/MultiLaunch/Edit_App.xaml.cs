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

namespace MultiLaunch
{
    /// <summary>
    /// Логика взаимодействия для Edit_App.xaml
    /// </summary>
    public partial class Edit_App : Window
    {
        public Edit_App()
        {
            InitializeComponent();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Edit_App taskWindow = new Edit_App();
            taskWindow.Show();

            this.Close();
        }
    }
}
