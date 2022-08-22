using ExamApp.Data.IRepositories;
using ExamApp.Data.Repositories;
using ExamApp.Domain.Entities.Students;
using ExamApp.Service.Constans;
using ExamApp.Service.DTOs;
using ExamApp.Service.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq.Expressions;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ExamApp.Service.Services
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository studentRepository;

        private readonly HttpClient httpClient;

        private readonly AttachmentRepository attachmentRepository;

        private readonly string url = Constant.BASE_URL + "Students/";

        public StudentService()
        {
            attachmentRepository = new AttachmentRepository();
            studentRepository = new StudentRepository();

            httpClient = new HttpClient();
        }

        public async Task<Student> CreateAsync(StudentForCreationDTO User)
        {
            var newStudent = JsonConvert.SerializeObject(User);

            var requestContent = new StringContent
                (newStudent, Encoding.UTF8, "application/json");

            var response = await httpClient.PostAsync
                (url, requestContent);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();

                var createdStudent = JsonConvert.DeserializeObject<Student>(content);

                await studentRepository.AddAsync(createdStudent);

                await studentRepository.SaveAsync();

                return createdStudent;
            }

            return null;
        }

        public async Task<bool> DeleteAsync(long id)
        {
            var response = await httpClient.DeleteAsync(url + id);

            if (response.IsSuccessStatusCode)
            {
                await studentRepository.DeleteAsync(p => p.Id == id);

                await studentRepository.SaveAsync();

                return true;

            }
            return false;

            
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public async Task<IEnumerable<Student>> GetAllAsync(Expression<Func<Student, bool>> expression = null, Tuple<int, int> pagination = null)
        {
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue
                ("Basic", Convert.ToBase64String(Encoding.ASCII.GetBytes("admin:12345")));

            var response = await httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var resultContent = await response.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<IEnumerable<Student>>(resultContent);
            }

            return null;

        }

        public async Task<Student> GetAsync(long id)
        {
            var response = await httpClient.GetAsync(url + id);

            if (response.IsSuccessStatusCode)
            {
                var resultContent = await response.Content.ReadAsStringAsync();

                resultContent = resultContent.Replace('\\', '/');

                return JsonConvert.DeserializeObject<Student>(resultContent);
            }

            return null;
        }

        public async Task<Student> UpdateAsync(long id, StudentForCreationDTO User)
        {
            var newUserAsJson = JsonConvert.SerializeObject(User);

            StringContent responseContent = new(newUserAsJson,
                Encoding.UTF8, "application/json");

            var response = await httpClient.PutAsync
                            (url + id, responseContent);


            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();

                content = content.Replace('\\', '/');

                var updatedUser = JsonConvert.DeserializeObject<Student>(content);

                if ( !await studentRepository.DeleteAsync(st => st.Id == updatedUser.Id) )
                {
                     var student = await studentRepository.AddAsync( new Student()
                     {
                         FirstName = updatedUser.FirstName,
                         LastName = updatedUser.LastName,
                         Faculty = updatedUser.Faculty,
                         Id = updatedUser.Id,
                         ImageId = updatedUser.ImageId,
                         PassportId = updatedUser.PassportId,
                         UpdateAt = updatedUser.UpdateAt,
                         CreateAt = updatedUser.CreateAt
                     });

                    await studentRepository.SaveAsync();

                    return student;
                }


                studentRepository.Update(updatedUser);

                await studentRepository.SaveAsync();

                return updatedUser;
            }

            return null;
        }

        public async Task UploadPicturesAsync(long id, string imagePath, string passportPath)
        {
            using HttpClient client = new();

            MultipartFormDataContent formData = new();

            if (imagePath is not null)
                formData.Add(new StreamContent(File.OpenRead(imagePath)), "image", "image.png");
            
            if (passportPath is not null)
                formData.Add(new StreamContent(File.OpenRead(passportPath)), "passport", "passport.png");

            var isUploadedSucceccfully = await client.PostAsync(url + "attachments/" + id, formData);

            if (isUploadedSucceccfully.IsSuccessStatusCode)
            {
                var response = await GetAsync(id);

                await attachmentRepository.AddAsync(response.Image);

                await attachmentRepository.AddAsync(response.Passport);

                await attachmentRepository.SaveAsync();

                return;
            }

        }
    }


}
