using System.ComponentModel.DataAnnotations;

namespace GameStore.Dtos
{
    public class EditUserDto
    {
        //public string UserId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        public string UserName { get; set; }

        public string AvatarUrl { get; set; }

        //public int GenderId { get; set; }

        public string City { get; set; }

        public string Country { get; set; }

        public int ZipCode { get; set; }
    }
}
