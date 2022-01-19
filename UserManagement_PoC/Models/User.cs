namespace UserManagement_PoC.Models
{
    public class User : Entity
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
    }
}
