using Microsoft.EntityFrameworkCore;
using GymMembershipManagement.Models;

namespace GymMembershipManagement.Data;
public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Member> Members => Set<Member>();
}
