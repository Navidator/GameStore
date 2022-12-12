using System.ComponentModel.DataAnnotations;

namespace GameStore.Dtos
{
    public class RefreshTokenDto
    {
        [Required]
        public string Token { get; set; }
        [Required]
        public string RefreshToken { get; set; }
    }
}
