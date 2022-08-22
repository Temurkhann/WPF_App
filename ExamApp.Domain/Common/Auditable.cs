using System;

namespace ExamApp.Domain.Common
{
    public abstract class Auditable
    {
        public long Id { get; set; }
        public DateTime CreateAt { get; set; }
        public DateTime UpdateAt { get; set; }

        public void Create()
            => CreateAt = DateTime.UtcNow;

        public void Update()
            => UpdateAt = DateTime.UtcNow;
    }
}
