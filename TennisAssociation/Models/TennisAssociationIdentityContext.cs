using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace TennisAssociation.Models
{
    /// <summary>
    /// Simple class for connecting Entity and Identity frameworks.
    /// </summary>
    /// <param name="options"></param>
    public class TennisAssociationIdentityContext : IdentityDbContext<MyUser>
    {
        public TennisAssociationIdentityContext(DbContextOptions<TennisAssociationIdentityContext> options)
            : base(options)
        {}
        
    }
}