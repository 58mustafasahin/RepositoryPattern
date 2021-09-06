namespace AppCore.Entities
{
    public class BaseEntity : Audit, ISoftDeleted
    {
        public BaseEntity()
        {
            IsDeleted = false;
        }
        public bool IsDeleted { get; set; }
    }
}
