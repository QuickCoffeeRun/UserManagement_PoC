namespace UserManagement_PoC.DTOs
{
    public class CreateUserDto
    {
        public CreateUserDto(string name, string lastName, string email, string phone)
        {
            Name = name;
            LastName = lastName;
            Email = email;
            Phone = phone;
        }

        public string Name { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
    }
}
