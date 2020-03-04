using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
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

namespace Lektion08_GradedExercise
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>

    public partial class MainWindow : Window
    {
        private ObservableCollection<User> data;

        public MainWindow()
        {
            InitializeComponent();

            data = new ObservableCollection<User>();

            outerGrid.DataContext = data;
        }

        public class User : INotifyPropertyChanged
        {
            private int _id;
            private string _name;
            private int _age;
            private int _score;

            public User(int id, string name, int age, int score)
            {
                _id = id;
                _name = name;
                _age = age;
                _score = score;
            }

            public int Id
            {
                get { return _id; }
                set
                {
                    _id = value;
                    NotifyPropertyChanged(nameof(Id));
                }
            }
            public string Name
            {
                get { return _name; }
                set
                {
                    _name = value;
                    NotifyPropertyChanged(nameof(Name));
                }
            }
            public int Age
            {
                get { return _age; }
                set
                {
                    _age = value;
                    NotifyPropertyChanged(nameof(Age));
                }
            }
            public int Score
            {
                get { return _score; }
                set
                {
                    _score = value;
                    NotifyPropertyChanged(nameof(Score));
                }
            }

            public override string ToString()
            {
                return Id + ", " + Name + ", " + Age + ", " + Score;
            }

            public String ListBoxToString
            {
                get
                {
                    return Id + " : " + Name;
                }
            }

            public event PropertyChangedEventHandler PropertyChanged;

            private void NotifyPropertyChanged(string propertyName)
            {
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private void OpenFile_Click(object sender, RoutedEventArgs e)
        {

            OpenFileDialog openFileDialog = new OpenFileDialog();

            // Time when file is loaded
            DateTime timeLoaded = DateTime.Now;

            // Counts the lines in the listbox
            int lineCounter = 0;

            if (openFileDialog.ShowDialog() == true)
            {
                using (StreamReader streamReader = new StreamReader(openFileDialog.OpenFile()))
                {
                    openFileDialog.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";

                    string[] users = File.ReadAllLines(openFileDialog.FileName);

                    // Iterate through all elements in the file
                    foreach (var user in users)
                    {
                        // Split users on ';'
                        string[] entries = user.Split(';');

                        // Get properties of the user
                        int id = int.Parse(entries[0]);
                        string name = entries[1];
                        int age = int.Parse(entries[2]);
                        int score = int.Parse(entries[3]);

                        // Add the user to the list
                        data.Add(new User(id, name, age, score));
                        lineCounter++;
                    }

                    // Writes to statusbar
                    StatusBarContent.Content = $"File loaded at: {timeLoaded} - Lines counted: {lineCounter}";
                }
            }
        }
    }
}