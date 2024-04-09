namespace Task.DTO
{
    public class Relation
    {
        public int Id { get; set; }
        public int FromId { get; set; }
        public int ToId { get; set; }
        public User FromUser { get; set; } = null!;
        public User ToUser { get; set; } = null!;
        public RelationType RelationType { get; set; }
        public DateTime CreateDate { get; set; }
        public bool IsDeleted { get; set; }
    }
    public enum RelationType : byte
    {
        Colleague = 0,
        Acquaintance = 1,
        Relative = 2,
        Other = 3,
    }
}