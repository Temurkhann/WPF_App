using ExamApp.Service.DTOs;
using ExamApp.Service.Interfaces;
using ExamApp.Service.Services;
using System.Windows;
using System.Windows.Controls;

namespace ExamApp.UI.Pages
{
    /// <summary>
    /// Interaction logic for UserDeleteInputs.xaml
    /// </summary>
    public partial class UserDeleteInputs : Page
    {
        public UserDeleteInputs()
        {
            InitializeComponent();
        }

        public async void dltBtn_Click(object sender, RoutedEventArgs e)
        {

            using (IStudentService studentService = new StudentService())
            {
                if(long.TryParse(dltId.Text, out long Id))
                {
                    if(!await studentService.DeleteAsync(Id))
                        MessageBox.Show("No user with this id found");
                    else
                    {
                        await studentService.DeleteAsync(Id);

                        MessageBox.Show("Student deleted successfully!");
                    }
                }

                else
                   MessageBox.Show("You must enter only number!");

            }

        }

    }
}
