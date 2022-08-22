using ExamApp.Domain.Common;

namespace ExamApp.Domain.Entities.Attachments
{
    public class Attachment : Auditable
    {
        public string Name { get; set; }
        public string Path { get; set; }
    }
}
