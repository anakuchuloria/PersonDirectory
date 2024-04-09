namespace Task.DTO
{
    public class PhoneNumber
    {
        public int Id { get; set; }
        public string Number { get; set; } = null!;
        public User User { get; set; } = null!;
        public PhoneNumberType PhoneNumberType { get; set; }
        public DateTime CreateDate { get; set; }
        public bool IsDeleted { get; set; }
    }
    public enum PhoneNumberType : byte
    {
        PersonalPhoneNumber = 0,
        WorkPhoneNumber = 1,
        HomePhoneNumber = 2,
    }
}