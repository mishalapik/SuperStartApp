using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Messaging;
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
        }
        
        string currentPreset = "None";
        public ObservableCollection<PresetApp> AppList { get; set; }
        public Edit_Preset(string presetNameFromMain)
        {
            
            InitializeComponent();
            currentPreset = presetNameFromMain;
            TopTextBlock.Text = currentPreset;
            AppList = new ObservableCollection<PresetApp>
            {
                //new PresetApp() { Name = "VS 2022", IconPath = "P1_Assets/VS.png", IsEdited = 0 },
                //new PresetApp() { Name = "Google Chrome", IconPath = "P1_Assets/GC.png", IsEdited = 60 },
                //new PresetApp() { Name = "Figma", IconPath = "P1_Assets/F.png" }
                //new PresetApp() { Name = "Figma", IconPath = "P1_Assets/F.png" }
            };

            string connectionString = "Data Source=SuperStartApp.db;Version=3;";
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                string distinctSelectionPresets = $"SELECT program_path FROM programs WHERE preset_name = '{currentPreset}';";
                using (SQLiteCommand command = new SQLiteCommand(distinctSelectionPresets, connection))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var text = reader.GetString(0);
                            AppList.Add(new PresetApp() { Name = System.IO.Path.GetFileNameWithoutExtension(reader.GetString(0)) ,IconPath="None"});
                        }
                    }
                }

            }

            
            AppMenu.ItemsSource = AppList;
        }

        
        private void BackToMainWindow_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }



        private void UpProgram_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            var programName = button.Tag.ToString();
            MessageBox.Show($"Эта функция будет добавлена позже.");
        }
        private void EditProgram_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            var prevProgramName = button.Tag.ToString();

            OpenFileDialog openFileDialog = new OpenFileDialog();

            // Устанавливаем свойства диалогового окна (необязательно)
            openFileDialog.Title = "Выберите файл"; // Заголовок окна
            // openFileDialog.Filter = "Текстовые файлы (*.txt)|*.txt|Все файлы (*.*)|*.*"; // Фильтр расширений файлов
            openFileDialog.RestoreDirectory = true;//InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments); // Начальная директория

            // Отображаем окно выбора файла и получаем результат
            bool? result = openFileDialog.ShowDialog();

            // Проверяем результат диалога
            if (result == true)
            {
                // Получаем путь выбранного файла
                string selectedFilePath = openFileDialog.FileName;
                string connectionString = "Data Source=SuperStartApp.db;Version=3;";
                using (SQLiteConnection connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();
                    string updateQuery = $"UPDATE programs SET program_path = '{selectedFilePath}' WHERE preset_name = '{currentPreset}' AND program_path LIKE '%{prevProgramName}%';";
                    using (SQLiteCommand command = new SQLiteCommand(updateQuery, connection))
                    {
                        int rowsAffected = command.ExecuteNonQuery();


                    }

                }
                for (int i = 0; i < AppList.Count; i++)
                {
                    if (AppList[i].Name == prevProgramName)
                    {
                        AppList[i] = new PresetApp() { Name = System.IO.Path.GetFileNameWithoutExtension(selectedFilePath), IconPath = "None" };
                    }
                }
                //UPDATE programs SET program_path = 'Newpath' WHERE preset_name = 'name' AND program_path = 'path';
            }
        }
        private void RemoveProgram_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            var programName = button.Tag.ToString();
            for (int i = 0; i < AppList.Count; i++)
            {
                if (AppList[i].Name.ToString() == programName)
                {
                    AppList.Remove(AppList[i]);
                    AppMenu.ItemsSource = AppList;
                    string connectionString = "Data Source=SuperStartApp.db;Version=3;";
                    using (SQLiteConnection connection = new SQLiteConnection(connectionString))
                    {
                        connection.Open();
                        string deleteQuery = $"DELETE FROM programs WHERE preset_name = '{currentPreset}' AND program_path LIKE '%{programName}%';";
                        using (SQLiteCommand command = new SQLiteCommand(deleteQuery, connection))
                        {
                            int rowsAffected = command.ExecuteNonQuery();


                        }

                    }
                    //DELETE FROM programs WHERE program_path = 'path';
                }
            }
            
        }
        private void AddProgram_Click( object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            string selectedFilePath;
            string programName;
            // Устанавливаем свойства диалогового окна (необязательно)
            openFileDialog.Title = "Выберите файл"; // Заголовок окна
            // openFileDialog.Filter = "Текстовые файлы (*.txt)|*.txt|Все файлы (*.*)|*.*"; // Фильтр расширений файлов
            openFileDialog.RestoreDirectory = true;//InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments); // Начальная директория

            // Отображаем окно выбора файла и получаем результат
            bool? result = openFileDialog.ShowDialog();

            // Проверяем результат диалога
            if (result == true)
            {
                // Получаем путь выбранного файла
                selectedFilePath = openFileDialog.FileName;
                programName = System.IO.Path.GetFileNameWithoutExtension(selectedFilePath);
                AppList.Add(new PresetApp() { Name = programName, IconPath = "None" });
                string connectionString = "Data Source=SuperStartApp.db;Version=3;";
                using (SQLiteConnection connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();
                    string insertQuery = $"INSERT INTO programs(preset_name,program_path) VALUES ('{currentPreset}','{selectedFilePath}');";
                    using (SQLiteCommand command = new SQLiteCommand(insertQuery, connection))
                    {
                        int rowsAffected = command.ExecuteNonQuery();


                            //while (reader.Read())
                            //{
                            //    var text = reader.GetString(0);
                            //    AppList.Add(new PresetApp() { Name = reader.GetString(0), IconPath = "None" });
                            //}
                        
                    }

                }
                //INSERT INTO programs(preset_name,program_path) VALUES ('name','path');
            }


        }
        private void RemoveAllProgramms_Click(object sender, RoutedEventArgs e)
        {
            AppList.Clear();
            string connectionString = "Data Source=SuperStartApp.db;Version=3;";
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                string deleteAllQuery = $"DELETE FROM programs WHERE preset_name = '{currentPreset}';";
                using (SQLiteCommand command = new SQLiteCommand(deleteAllQuery, connection))
                {
                    int rowsAffected = command.ExecuteNonQuery();
                }

            }
            //удалить из базы данных все тоже надо
        }
    }
}
