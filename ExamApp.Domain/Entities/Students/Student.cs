using ExamApp.Domain.Common;
using ExamApp.Domain.Entities.Attachments;

namespace ExamApp.Domain.Entities.Students
{
    public class Student : Auditable
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Faculty { get; set; }
        public long? PassportId { get; set; }
        public long? ImageId { get; set; }
        public Attachment Passport { get; set; }
        public Attachment Image { get; set; }
    }
}
