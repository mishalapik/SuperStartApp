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
    /// Interaction logic for NewPreset.xaml
    /// </summary>
    public partial class NewPreset : Window
    {
        public NewPreset()
        {
            InitializeComponent();
        }
        private void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                TextBox textBox = sender as TextBox;
                if (textBox != null )
                {
                    string text = textBox.Text;
                    Edit_Preset newPreset = new Edit_Preset(text);
                    newPreset.Show();
                    this.Close();
                }
                

            }
        }
    }
}
