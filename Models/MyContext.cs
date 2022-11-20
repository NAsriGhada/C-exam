#pragma warning disable CS8618

using Microsoft.EntityFrameworkCore;
namespace csharpexam.Models;
public class MyContext : DbContext 
{ 
    public MyContext(DbContextOptions options) : base(options) { }

    public DbSet<User> Users { get; set; } 

    public DbSet<Participation> Participations { get; set; }

    public DbSet<MeetUp> MeetUps { get; set; }
}