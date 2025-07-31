using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace jwt.model
{
    public class appcontext:IdentityDbContext<user>
    {
        public appcontext(DbContextOptions<appcontext> options):base(options)
        {
            
        }

    }
}
