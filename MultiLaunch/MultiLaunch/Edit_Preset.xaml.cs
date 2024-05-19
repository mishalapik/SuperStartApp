using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using static MultiLaunch.MainWindow;

namespace MultiLaunch
{
    public struct PresetApp
    {
        public string IconPath { get; set; }
        public string Path { get; set; }
        public string Name { get; set; }
        public int IsEdited { get; set; }
    };
    public partial class Edit_Preset : Window
    {
        public Edit_Preset()
        {
            InitializeComponent();

            var AppList = new List<PresetApp>
            {
                new PresetApp(){ Name="VS 2022", IconPath="P1_Assets/VS.png", IsEdited = 0},
                new PresetApp(){ Name="Google Chrome", IconPath="P1_Assets/GC.png", IsEdited = 60},
                new PresetApp(){ Name="Figma", IconPath="P1_Assets/F.png"}
            };

            AppMenu.ItemsSource = AppList;
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
