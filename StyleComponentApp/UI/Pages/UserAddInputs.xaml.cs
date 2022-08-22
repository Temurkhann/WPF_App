using ExamApp.Service.DTOs;
using ExamApp.Service.Interfaces;
using ExamApp.Service.Services;
using System.Windows;
using System.Windows.Controls;

namespace ExamApp.UI.Pages
{
    /// <summary>
    /// Interaction logic for UserAddInputs.xaml
    /// </summary>
    public partial class UserAddInputs : Page
    {
        public UserAddInputs()
        {
            InitializeComponent();
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            using (IStudentService studentService = new StudentService())
            {

                if (!string.IsNullOrEmpty(Firstname.Text) &&
                   !string.IsNullOrEmpty(Lastname.Text) &&
                   !string.IsNullOrEmpty(Faculty.Text))
                {
                    var newUser = new StudentForCreationDTO()
                    {
                        FirstName = Firstname.Text,
                        LastName = Lastname.Text,
                        Faculty = Faculty.Text,
                    };

                    await studentService.CreateAsync(newUser);

                    MessageBox.Show("Student created successfully");
                }

                else
                    MessageBox.Show("All inputs must be filled!!!");


            }

        }
    }
}
