using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GameStore.Models
{
    public class UserModel : IdentityUser<int>
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        public string AvatarUrl { get; set; }

        public int GenderId { get; set; }

        public string City { get; set; }

        public string Country { get; set; }

        public int ZipCode { get; set; }

        public IList<CommentModel> Comments { get; set; } = new List<CommentModel>();
    }
}
