using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace HelpLife.DataBase
{
    public class LifeContext : IdentityDbContext<IdentityUser>
    {
    }
}
