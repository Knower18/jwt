using System.ComponentModel.DataAnnotations;
using System.Runtime.ExceptionServices;

namespace jwt.serveces
{
    public class Register
    {
        [Required,StringLength(100)]
        public string fistname { get; set; }
        [Required, StringLength(100)]
        public string lastname { get; set; }
        [Required, StringLength(100)]
        public string email { get; set; }
        [Required, StringLength(100)]
        public string password { get; set; }
        [Required, StringLength(100)]
        public string usernmae { get; set; }
    }
}
