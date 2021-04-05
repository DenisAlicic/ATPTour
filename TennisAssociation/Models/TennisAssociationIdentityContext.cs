using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace TennisAssociation.Models
{
    public class TennisAssociationIdentityContext : IdentityDbContext<MyUser>
    {
        public TennisAssociationIdentityContext(DbContextOptions<TennisAssociationIdentityContext> options)
            : base(options)
        {}
        
    }
}