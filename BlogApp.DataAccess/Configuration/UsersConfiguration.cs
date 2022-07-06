using BlogApp.Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.DataAccess.Configuration
{
    public class UsersConfiguration : EntityConfiguration<User>
    {
        protected override void ConfigureChildRules(EntityTypeBuilder<User> builder)
        {
            builder.Property(x => x.Username).HasMaxLength(20).IsRequired();
            builder.Property(x => x.Firstname).HasMaxLength(30).IsRequired();
            builder.Property(x => x.Lastname).HasMaxLength(30).IsRequired();
            builder.Property(x => x.Email).HasMaxLength(50).IsRequired();
            builder.Property(x => x.Password).HasMaxLength(100).IsRequired();
            builder.Property(x => x.RoleId).IsRequired();

            builder.HasIndex(x => x.Username).IsUnique();
            builder.HasIndex(x => x.Firstname);
            builder.HasIndex(x => x.Lastname);
            builder.HasIndex(x => x.Email).IsUnique();


            #region Relationships
            builder.HasMany(x => x.CreatedPosts)
                    .WithOne(x => x.Author)
                    .HasForeignKey(x => x.UserId)
                    .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(u => u.CreatedComments)
                .WithOne(c => c.CommentedBy)
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.Restrict); 

            builder.HasMany(u => u.LikedComments)
                .WithOne(x => x.User)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(u => u.LikedPosts)
                .WithOne(x => x.User)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(u => u.UseCases)
                .WithOne(uc => uc.User)
                .HasForeignKey(uc => uc.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            #endregion

             
        }
    }
}
