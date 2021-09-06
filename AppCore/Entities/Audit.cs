using System;

namespace AppCore.Entities
{
    public abstract class Audit
    {
        public int CreateUserId { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public int? ModifiedUserId { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
