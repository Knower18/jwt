using System.ComponentModel.DataAnnotations;

namespace jwt.serveces
{
    public class Tokenrquenst
    {
        [Required]
        public string mail { get; set; }
        [Required]
        public string password { get; set; }
    }
}
