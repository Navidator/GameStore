using GameStore.Enums;

namespace GameStore.Models
{
    public class UserModel
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string Lastname { get; set; }

        public string UserName { get; set; }

        public Gender Gender { get; set; }

        public string Email { get; set; }

        public string City { get; set; }

        public string Country { get; set; }

        public int ZipCode { get; set; }

        public int PhoneNumber { get; set; }
    }
}
