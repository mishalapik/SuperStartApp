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

namespace MultiLaunch
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public struct Preset
        {
            public string Name { get; set; }
        };
        public MainWindow()
        {
            InitializeComponent();

            var PresetList = new List<Preset>
            {
                new Preset(){ Name="Workau tut ochen jestko"},
                new Preset(){ Name="Pork"},
                new Preset(){ Name="Work"},
                new Preset(){ Name="Pork"},
                new Preset(){ Name="Work"},
                new Preset(){ Name="Pork"},
                new Preset(){ Name="Work"},
                new Preset(){ Name="Pork"},
                new Preset(){ Name="Work"},
                new Preset(){ Name="Pork"},
                new Preset(){ Name="Work"}
            };

            PresetMenu.ItemsSource = PresetList;
        }
    }
}
