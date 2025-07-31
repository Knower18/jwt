using Microsoft.AspNetCore.Identity;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Identity;
using System.ComponentModel.DataAnnotations;

namespace jwt.model
{
    public class user:IdentityUser
    {
        [Required,MaxLength(50)]
        public string Name { get; set; }
        [Required,MaxLength(50)]
        public string lastname {  get; set; }
    }
}
