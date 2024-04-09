namespace Task.DTO
{
    public class City
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public ICollection<User>? Users { get; set; }
        public DateTime CreateDate { get; set; }
        public bool IsDeleted { get; set; }
    }
}
