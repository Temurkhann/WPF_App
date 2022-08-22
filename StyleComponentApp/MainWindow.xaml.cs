using ExamApp.Domain.Entities.Students;
using ExamApp.Service.Services;
using ExamApp.UI.Controls;
using ExamApp.UI.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace ExamApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly StudentService studentService;
        private Thread thread;
        private readonly Save savePage;
        private readonly UserAddInputs addPage;
        private readonly UserDeleteInputs deletePage;

        private IEnumerable<Student> allStudents;
        
        public MainWindow()
        {
            savePage = new Save();
            deletePage = new UserDeleteInputs();
            addPage = new UserAddInputs();
            studentService = new StudentService();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            thread = new Thread(async () =>
            {
                this.Dispatcher.Invoke(() => Chat.Items.Clear());

                allStudents = await studentService.GetAllAsync();
                await LoadUsers(allStudents);
            });
            thread.Start();
        }
        
        private async Task LoadUsers(IEnumerable<Student> users)
        {

            foreach (var user in users)
            {
                await this.Dispatcher.InvokeAsync((() =>
                {
                    Button button = new Button();
                    button.Background = new SolidColorBrush(Color.FromRgb(44,46,77));
                    button.Padding = new Thickness(0);
                    button.Height = 60;
                    button.Click += UsersInfoButtonClick; 
                    PrivateChat userList = new PrivateChat();
                    
                    userList.NameTxt.Text = user.FirstName + " " + user.LastName;
                    userList.UserId.Text = user.Id.ToString();
                    userList.UserImg.ImageSource = user.Image is not null
                        ? new BitmapImage(new Uri("https://talabamiz.uz/" + user.Image.Path))
                        : new BitmapImage(new Uri("https://talabamiz.uz/Images//99daf8ac38de4433aa36a61baf4c9c4d.png"));

                    button.Content = userList;

                    this.Chat.Items.Add(button);
                }));
            }
        }

        private async void UsersInfoButtonClick(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;

            var information = (PrivateChat)button.Content;

            var student = await studentService.GetAsync
                                (long.Parse(information.UserId.Text));

            var updatePage = new Save();

            updatePage.Firstname.Text = student.FirstName;
            updatePage.Lastname.Text = student.LastName;
            updatePage.Faculty.Text = student.Faculty;
            updatePage.PassportImage.ImageSource = student.Passport is not null
                        ? new BitmapImage(new Uri("https://talabamiz.uz/" + student.Passport.Path))
                        : new BitmapImage(new Uri("https://talabamiz.uz/Images//99daf8ac38de4433aa36a61baf4c9c4d.png"));

            updatePage.PersonalImage.ImageSource = student.Image is not null
                        ? new BitmapImage(new Uri("https://talabamiz.uz/" + student.Image.Path))
                        : new BitmapImage(new Uri("https://talabamiz.uz/Images//99daf8ac38de4433aa36a61baf4c9c4d.png"));
            updatePage.Id = student.Id;

            MainFrame.Content = updatePage;

        }

        private void SearchTxt_TextChanged(object sender, TextChangedEventArgs e)
        {
            Chat.Items.Clear();

            string text = SearchTxt.Text.ToLower();

            thread = new Thread(async () =>
            {
                allStudents = await studentService.GetAllAsync();

                var users = allStudents.Where(p => p.FirstName.ToLower().Contains(text)
                || p.LastName.ToLower().Contains(text));

                await LoadUsers(users);
            });
            thread.Start();
        }

        private void DelBtn_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = deletePage;
        }

        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = addPage;
        }
    }
}
