using System.ComponentModel.Composition;
using System.Windows;
using RearrangeItemsInListboxViaDragAndDrop.Applications.Views;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Windows.Input;

namespace RearrangeItemsInListboxViaDragAndDrop.Presentation.Views
{
    [Export(typeof(IShellView))]
    public partial class ShellWindow : Window, IShellView
    {
        ObservableCollection<Student> StudentList = new ObservableCollection<Student>();

        public ShellWindow()
        {
            InitializeComponent();

            StudentList.Add(new Student("1", 22));
            StudentList.Add(new Student("2", 18));
            StudentList.Add(new Student("3", 29));
            StudentList.Add(new Student("4", 9));
            StudentList.Add(new Student("5", 29));
            StudentList.Add(new Student("6", 9));
            listbox1.DisplayMemberPath = "Name";
            listbox1.ItemsSource = StudentList;

            Style itemContainerStyle = new Style(typeof(ListBoxItem));
            itemContainerStyle.Setters.Add(new Setter(ListBoxItem.AllowDropProperty, true));
            itemContainerStyle.Setters.Add(new EventSetter(ListBoxItem.PreviewMouseLeftButtonDownEvent, new MouseButtonEventHandler(s_PreviewMouseLeftButtonDown)));
            itemContainerStyle.Setters.Add(new EventSetter(ListBoxItem.DropEvent, new DragEventHandler(listbox1_Drop)));
            listbox1.ItemContainerStyle = itemContainerStyle;
        }

        void s_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

            if (sender is ListBoxItem)
            {
                ListBoxItem draggedItem = sender as ListBoxItem;
                DragDrop.DoDragDrop(draggedItem, draggedItem.DataContext, DragDropEffects.Move);
                draggedItem.IsSelected = true;
            }
        }

        void listbox1_Drop(object sender, DragEventArgs e)
        {
            Student droppedData = e.Data.GetData(typeof(Student)) as Student;
            Student target = ((ListBoxItem)(sender)).DataContext as Student;

            int removedIdx = listbox1.Items.IndexOf(droppedData);
            int targetIdx = listbox1.Items.IndexOf(target);

            if (removedIdx < targetIdx)
            {
                StudentList.Insert(targetIdx + 1, droppedData);
                StudentList.RemoveAt(removedIdx);
            }
            else
            {
                int remIdx = removedIdx + 1;
                if (StudentList.Count + 1 > remIdx)
                {
                    StudentList.Insert(targetIdx, droppedData);
                    StudentList.RemoveAt(remIdx);
                }
            }
        }


        public class Student
        {
            public int Age { set; get; }
            public string Name { set; get; }

            public Student(string name, int age)
            {
                this.Name = name;
                this.Age = age;
            }
        }
    }
}
