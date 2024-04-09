namespace Task.DTO
{
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public Gender Gender { get; set; }
        public int IdNumber { get; set; }
        public DateTime BirthDate { get; set; }
        public string Photo { get; set; }
        public City City { get; set; } = null!;
        public ICollection<PhoneNumber> PhoneNumbers { get; set; } = null!;
        public ICollection<Relation>? RelationsTo { get; set; }
        public ICollection<Relation>? RelationsFrom { get; set; }
        public DateTime CreateDate { get; set; }
        public bool IsDeleted { get; set; }
    }
    public enum Gender : byte
    {
        Male = 0,
        Female = 1
    }
}