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
    /// Логика взаимодействия для Edit_Student_Window.xaml
    /// </summary>
    public partial class Edit_Student_Window : Window
    {

        string S_Name, S_Group, S_Date;

        public Edit_Student_Window()
        {
            InitializeComponent();
            StudInfoDBEntities db = new StudInfoDBEntities();
            this.gridStudents.ItemsSource = db.Students.ToList();
            
        }
        private void Load_StudentDB()
        {
            StudInfoDBEntities db = new StudInfoDBEntities();
            this.gridStudents.ItemsSource = db.Students.ToList();
        }
        private void Add_S()
        {

            StudInfoDBEntities db = new StudInfoDBEntities();
            Student student = new Student()
            {
                Name = S_Name,
                Group = S_Group,
                Date = S_Date
            };
            db.Students.Add(student);
            db.SaveChanges();
        }
        private void Delete_Student_Click(object sender, RoutedEventArgs e)
        {
            StudInfoDBEntities db = new StudInfoDBEntities();
            var r = from d in db.Students
                    where d.Id == updatingStudentID
                    select d;
            Student obj = r.SingleOrDefault();
            if (obj != null)
            {
                MessageBoxResult result = MessageBox.Show("Удалить студента?", "StudInfo v2.0 b.3", MessageBoxButton.YesNo);
                switch (result)
                {
                    case MessageBoxResult.Yes:
                        db.Students.Remove(obj);
                        db.SaveChanges();
                        MessageBox.Show("Студент успешно удален", "StudInfo v2.0 b.3");
                        break;
                }
            }
            db.SaveChanges();

            Load_StudentDB();

        }
        private int updatingStudentID = 0;
        private void gridStudents_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.gridStudents.SelectedIndex >= 0)
            {
                if (this.gridStudents.SelectedItems.Count >= 0)
                {
                    if (this.gridStudents.SelectedItems[0].GetType() == typeof(Student))
                    {
                        Student Edit_S = (Student)this.gridStudents.SelectedItems[0];
                        FIO_Student.Text = Edit_S.Name;
                        Date_Student.Text = Edit_S.Date;
                        Group_Student.Text = Edit_S.Group;
                        updatingStudentID = Edit_S.Id;
                    }
                }
            }
        }

        private void Add_Student_Click(object sender, RoutedEventArgs e)
        {
            S_Name = FIO_Student.Text.ToString();
            S_Group = Group_Student.Text.ToString();
            DateTime? DT_Date = Date_Student.SelectedDate;
            if (DT_Date == null || Name == null || S_Group == null)
            {
                MessageBox.Show("Введена некорректная информация, или введено пустое поле \n Повторите ввод!", "StudInfo v2.0 b.3", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                S_Date = DT_Date.Value.ToShortDateString();
                Add_S();


            }
            Load_StudentDB();
        }

        private void Save_Student_Click(object sender, RoutedEventArgs e)
        {
            StudInfoDBEntities db = new StudInfoDBEntities();
            var r = from d in db.Students
                    where d.Id == updatingStudentID
                    select d;
            Student obj = r.SingleOrDefault();
            if (obj != null)
            {
                obj.Name = FIO_Student.Text.ToString();
                DateTime? DT_Date = Date_Student.SelectedDate;
                S_Date = DT_Date.Value.ToShortDateString();
                obj.Date = S_Date;
                obj.Group = Group_Student.Text.ToString();
            }
            db.SaveChanges();
            Load_StudentDB();
        }

        private void Cancel_Adding(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
