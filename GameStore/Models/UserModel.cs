using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GameStore.Models
{
    public class UserModel
    {
        [Key]
        public int UserId { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string Lastname { get; set; }

        [Required]
        public string UserName { get; set; }


        public int GenderId { get; set; }

        [Required]
        public string Email { get; set; }

        public string City { get; set; }

        public string Country { get; set; }

        public int ZipCode { get; set; }

        public int PhoneNumber { get; set; }

        [ForeignKey("RoleModel")]
        public int RoleId { get; set; }
    }
}
