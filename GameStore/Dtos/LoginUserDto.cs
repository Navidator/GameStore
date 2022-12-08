using System.ComponentModel.DataAnnotations;

namespace GameStore.Dtos
{
    public class LoginUserDto
    {
        [Required, DataType(DataType.EmailAddress)]
        public string email { get; set; }

        [Required, DataType(DataType.Password)]
        public string password { get; set; }
    }
}
