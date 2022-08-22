namespace ExamApp.Service.DTOs
{
    public class StudentForCreationDTO
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Faculty { get; set; }
        public AttachmentViewModelDTO Image { get; set; }
        public AttachmentViewModelDTO Passport { get; set; }
    }
}
