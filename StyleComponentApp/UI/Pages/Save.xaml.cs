using ExamApp.Service.DTOs;
using ExamApp.Service.Interfaces;
using ExamApp.Service.Services;
using Microsoft.Win32;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace ExamApp.UI.Pages
{
    /// <summary>
    /// Interaction logic for IntroductionPage.xaml
    /// </summary>
    public partial class Save : Page
    {
        private string passportPath;
        private string imagePath;

        public long Id { get; set; }

        public Save()
        {
            InitializeComponent();
        }

        private void ImgUploadbtn_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files(*.PNG,*.JPG)|*.JPG;*.PNG";

            if (openFileDialog.ShowDialog() == true)
            {
                imagePath = openFileDialog.FileName;
                
                var button = (Button)sender;
                button.Content = "Uploaded...";
            }
        }

        private void PsprtUploadBtn_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new ();
            openFileDialog.Filter = "Image Files(*.PNG,*.JPG)|*.JPG;*.PNG";

            if (openFileDialog.ShowDialog() == true)
            {
                passportPath = openFileDialog.FileName;

                var button = (Button)sender;
                button.Content = "Uploaded...";
            }
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {

            using IStudentService studentService = new StudentService();

            var oldStudent = await studentService.GetAsync(Id);

            if (oldStudent is null)
            {
                MessageBox.Show("Student not fount");

                return;
            }

            var updateStudentInfo = new StudentForCreationDTO();

            if (!string.IsNullOrEmpty(Firstname.Text))
                updateStudentInfo.FirstName = Firstname.Text;
            else
                updateStudentInfo.FirstName = oldStudent.FirstName;

            if (!string.IsNullOrEmpty(Lastname.Text))
                updateStudentInfo.LastName = Lastname.Text;
            else
                updateStudentInfo.LastName = oldStudent.LastName;

            if (!string.IsNullOrEmpty(Faculty.Text))
                updateStudentInfo.Faculty = Faculty.Text;
            else
                updateStudentInfo.Faculty = oldStudent.Faculty;

            if (imagePath != null && passportPath != null)
                await studentService.UploadPicturesAsync(oldStudent.Id, imagePath, passportPath);

            if (imagePath != null && passportPath == null ||
                imagePath == null && passportPath != null)
            {
                MessageBox.Show("Please upload both images");
                
                return;
            }

            var response = await studentService.UpdateAsync(Id, updateStudentInfo);


            if (response is not null)
                MessageBox.Show("Student is updated successfully");
        }

        private async void DeleteBtn_Click(object sender, RoutedEventArgs e)
        {
            using (IStudentService studentService = new StudentService())
            {
                if (!await studentService.DeleteAsync(Id))
                    MessageBox.Show("No user with this id found");
                else
                {
                    await studentService.DeleteAsync(Id);

                    MessageBox.Show("Student deleted successfully!");
                }

            }

        }
    }
}

