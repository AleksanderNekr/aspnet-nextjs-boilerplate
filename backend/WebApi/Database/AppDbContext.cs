using Microsoft.EntityFrameworkCore;
using WebApi.Domain;

namespace WebApi.Database;

public sealed class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
	public DbSet<User> Users { get; init; }
}