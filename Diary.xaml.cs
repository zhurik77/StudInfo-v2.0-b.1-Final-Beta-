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

namespace StudInfo_Final
{
    /// <summary>
    /// Логика взаимодействия для Diary.xaml
    /// </summary>
    public partial class Diary : Window
    {
        private string FIO_Student, StudentGroup,Theme_Zone;
        private int StudentID, Ocenka_Zone;
        private int updatingStudentID = 0;
        private void Load_StudentDB()
        {
            StudInfoDBEntities db = new StudInfoDBEntities();
            this.StudentDG.ItemsSource = db.Students.ToList();
            
            StudInfoDBEntities dc = new StudInfoDBEntities();
            this.ThemeDG.ItemsSource = db.Dnevniks.ToList();
        }
        private void Add_T()
        {

            StudInfoDBEntities dc = new StudInfoDBEntities();
            Dnevnik student = new Dnevnik()
            {
                Theme = Theme_Zone,
                Ocenka = Ocenka_Zone,
                Id_Student = StudentID
            };
            dc.Dnevniks.Add(student);
            dc.SaveChanges();
        }
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Add_Ocenka_Theme_Click(object sender, RoutedEventArgs e)
        {
            Theme_Zone = Theme_TextBox.Text.ToString();
            if(Ocenka_TextBox.Text =="")
            {
                Ocenka_Zone = 0;
            }
            else if(Convert.ToInt32(Ocenka_TextBox.Text)>0 && Convert.ToInt32(Ocenka_TextBox.Text)<6)
            {
                Ocenka_Zone = Convert.ToInt32(Ocenka_TextBox.Text);
            }
            else
            {
                MessageBox.Show("Введена некорректная оценка\n Повторите ввод!", "StudInfo v2.0 b.3", MessageBoxButton.OK, MessageBoxImage.Warning);

            }
            if (Theme_Zone != null)
            {
                Add_T();
            }
            else
            {
                MessageBox.Show("Введена некорректная информация, или введено пустое поле \n Повторите ввод!", "StudInfo v2.0 b.3", MessageBoxButton.OK, MessageBoxImage.Warning);

            }
            Load_StudentDB();
        }

        private void Save_Changes_Click(object sender, RoutedEventArgs e)
        {
            StudInfoDBEntities db = new StudInfoDBEntities();
            var r = from d in db.Dnevniks
                    where d.Id == updatingStudentID
                    select d;
            Dnevnik obj = r.SingleOrDefault();
            if (obj != null)
            {
                obj.Theme = Theme_TextBox.Text.ToString();
                obj.Id_Student = updatingStudentID;
                obj.Ocenka = Convert.ToInt32(Ocenka_TextBox.Text);
            }
            db.SaveChanges();
            Load_StudentDB();
        }

        private void Delete_Theme_Click(object sender, RoutedEventArgs e)
        {
            StudInfoDBEntities db = new StudInfoDBEntities();
            var r = from d in db.Dnevniks
                    where d.Id == updatingStudentID
                    select d;
            Dnevnik obj = r.SingleOrDefault();
            if (obj != null)
            {
                MessageBoxResult result = MessageBox.Show("Удалить студента?", "StudInfo v2.0 b.3", MessageBoxButton.YesNo);
                switch (result)
                {
                    case MessageBoxResult.Yes:
                        db.Dnevniks.Remove(obj);
                        db.SaveChanges();
                        MessageBox.Show("Студент успешно удален", "StudInfo v2.0 b.3");
                        break;
                }
            }
            db.SaveChanges();

            Load_StudentDB();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        public Diary()
        {
            InitializeComponent();
            StudInfoDBEntities db = new StudInfoDBEntities();
            this.StudentDG.ItemsSource = db.Students.ToList();
            StudInfoDBEntities dc = new StudInfoDBEntities();
            this.ThemeDG.ItemsSource = db.Dnevniks.ToList();
        }

        private void StudentDG_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.StudentDG.SelectedIndex >= 0)
            {
                if (this.StudentDG.SelectedItems.Count >= 0)
                {
                    if (this.StudentDG.SelectedItems[0].GetType() == typeof(Student))
                    {
                        Student Edit_S = (Student)this.StudentDG.SelectedItems[0];
                        FIO_Student = Edit_S.Name;
                        textBoxStudent_FIO.Content = Edit_S.Name;
                        // Group_Student.Text = Edit_S.S_Group;
                        updatingStudentID = Edit_S.Id;
                        StudentID = Edit_S.Id;
                        textBox_GroupStudent.Content = Edit_S.Group;
                        ID_Label.Content = Edit_S.Id;
                    }
                }
            }
        }
    

        private void ThemeDG_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.ThemeDG.SelectedIndex >= 0)
            {
                if (this.ThemeDG.SelectedItems.Count >= 0)
                {
                    if (this.ThemeDG.SelectedItems[0].GetType() == typeof(Dnevnik))
                    {
                        Dnevnik Edit_S = (Dnevnik)this.ThemeDG.SelectedItems[0];
                        Theme_TextBox.Text = Edit_S.Theme;
                        Ocenka_TextBox.Text = Edit_S.Ocenka.ToString();
                        Theme_Enter_Zone.Text = Edit_S.Theme;
                        Ocenka_Enter_Zone.Content = Edit_S.Ocenka;
                        StudentID = Edit_S.Id;
                    }
                }
            }
        }
    }
}
