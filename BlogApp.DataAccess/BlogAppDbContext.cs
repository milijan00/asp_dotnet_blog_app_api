using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using BlogApp.Domain;
namespace BlogApp.DataAccess
{
    public class BlogAppDbContext :  DbContext
    {
        public DbSet<Category> Categories{ get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Post> Posts{ get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<LikedPosts> LikedPosts { get; set; }
        public DbSet<LikedComment> LikedComments { get; set; }
        public DbSet<NavigationLink> NavigationLinks { get; set; }
        public DbSet<PostsImage> PostsImages { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserUseCase> UsersUseCases { get; set; }
        public IActionUser User { get; }
        public BlogAppDbContext(IActionUser user  = null)
        {
            this.User = user; 
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);
            modelBuilder.Entity<LikedComment>().HasKey(x => new { x.CommentId, x.UserId });
            modelBuilder.Entity<LikedPosts>().HasKey(x => new { x.PostId, x.UserId });
            modelBuilder.Entity<UserUseCase>().HasKey(x => new { x.UserId, x.UseCaseId });
            base.OnModelCreating(modelBuilder);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=DESKTOP-39RJT5L\SQLEXPRESS;Initial Catalog=BlogApp;Integrated Security=True");
        }
        public override int SaveChanges()
        {
            foreach(var entry in this.ChangeTracker.Entries())
            {
                if(entry.Entity is Entity e)
                {
                    switch(entry.State)
                    {
                        case EntityState.Added:
                            e.IsActive = true;
                            e.CreatedAt = DateTime.UtcNow;
                            break;
                        case EntityState.Modified:
                            e.UpdatedAt = DateTime.UtcNow;
                            e.UpdatedBy = User.Identity;
                            break;
                    }
                }
            }
            return base.SaveChanges();
        }
    }
}
