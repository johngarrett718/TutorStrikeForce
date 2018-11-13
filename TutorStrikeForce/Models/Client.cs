namespace TutorStrikeForce.Models
{
    public class Client
    {
        public int ClientId { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string City { get; set; }
        public decimal Hours { get; set; }
        public bool IsDeleted { get; set; }
    }
}
