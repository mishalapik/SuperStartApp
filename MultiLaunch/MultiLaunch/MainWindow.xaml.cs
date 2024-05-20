using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Diagnostics;
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
        public class Preset
        {
            public string Name { get; set; }
        };

        string currentPreset;
        public MainWindow()
        {
            var PresetList = new List<Preset>
            {
                
            };
            InitializeComponent();
            string connectionString = "Data Source=SuperStartApp.db;Version=3;";
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                string distinctSelectionPresets = "SELECT DISTINCT preset_name FROM programs;";
                using (SQLiteCommand command = new SQLiteCommand(distinctSelectionPresets, connection))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            PresetList.Add(new Preset() { Name = reader.GetString(0) });
                        }
                    }
                }

            }
            
            

            PresetMenu.ItemsSource = PresetList;
        }
        private void OpenProgramsByPreset(string presetName)
        {
            string connectionString = "Data Source=SuperStartApp.db;Version=3;";
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                string distinctSelectionPresets = $"SELECT program_path FROM programs WHERE preset_name = '{presetName}';";
                using (SQLiteCommand command = new SQLiteCommand(distinctSelectionPresets, connection))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string path = reader.GetString(0);
                            Process.Start(new ProcessStartInfo(path) { UseShellExecute = true });
                        }
                    }
                }

            }
        }
        private void MItem_Click(object sender, RoutedEventArgs e)
        {

            Button button = sender as Button;
            StackPanel stackPanel = button.Content as StackPanel;
            foreach (var child  in stackPanel.Children)
            {
                if (child is TextBlock textBlock)
                {
                    currentPreset= textBlock.Text;

                    OpenProgramsByPreset(currentPreset);
                    break;
                }
            }
        }
        private void EditPreset_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            string text = button.Tag.ToString();
            Edit_Preset editPresetWindow = new Edit_Preset(text);
            editPresetWindow.Show();
            this.Close();
            currentPreset = text;
            
        }
        private void RemovePreset_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            string text = button.Tag.ToString();
            MessageBox.Show($"Удаление пресета {text}");
        }
        private void DeleteAllPresets_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Удаляем все пресеты");
        }
        private void CreateNewPreset_Click(object sender, RoutedEventArgs e)
        {
            NewPreset preset = new NewPreset();
            preset.Show();
            //MessageBox.Show("Создаем новый пресет");
        }


    }
}
