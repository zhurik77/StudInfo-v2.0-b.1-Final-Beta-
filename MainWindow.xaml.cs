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

namespace StudInfo_Final
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Open_Student_Window(object sender, RoutedEventArgs e)
        {
            Edit_Student_Window edit_Student_Window = new Edit_Student_Window();
            edit_Student_Window.Show();
        }

        private void Open_Diary_Click(object sender, RoutedEventArgs e)
        {
            Diary diary = new Diary();
            diary.Show();
        }

        private void Add_Theme_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
