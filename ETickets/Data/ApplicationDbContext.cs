using ETickets.Models;
using Microsoft.EntityFrameworkCore;
using ETickets.ViewModel;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace ETickets.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Actor> Actors { get; set; }
        public DbSet<ActorMovie> ActorMovies { get; set; }
        
        public DbSet<Cinema> Cinemas { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
      
        public DbSet<User> Users { get; set; }
        public ApplicationDbContext()
        {
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", true, true)
                .Build()
                .GetConnectionString("DefaultConnection");

            optionsBuilder.UseSqlServer(builder);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ActorMovie>().HasKey(am => new { am.ActorId, am.MovieId });

            modelBuilder.Entity<ActorMovie>().HasOne(am => am.Movie).WithMany(m => m.ActorMovies).HasForeignKey(am => am.MovieId);

            modelBuilder.Entity<ActorMovie>().HasOne(am => am.Actor).WithMany(a => a.ActorMovies).HasForeignKey(am => am.ActorId);
            
        }
        public DbSet<ETickets.ViewModel.MovieViewModel> MovieViewModel { get; set; } = default!;
        public DbSet<ETickets.ViewModel.ApplicationUserVM> ApplicationUserVM { get; set; } = default!;
        public DbSet<ETickets.ViewModel.UserLoginVM> UserLoginVM { get; set; } = default!;
        public DbSet<ETickets.ViewModel.UserRoleVM> UserRoleVM { get; set; } = default!;



    }
    
}
