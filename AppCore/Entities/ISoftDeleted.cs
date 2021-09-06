namespace AppCore.Entities
{
    public interface ISoftDeleted
    {
        public bool IsDeleted { get; set; }
    }
}
