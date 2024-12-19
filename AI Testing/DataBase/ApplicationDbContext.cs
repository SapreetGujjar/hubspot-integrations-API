using AI_Testing.Models;
using Microsoft.EntityFrameworkCore;

namespace AI_Testing.DataBase
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<AI_Agent> Agents { get; set; }
        public DbSet<HubSpotLeads> HubSpotLeads { get; set; }
    }
}
