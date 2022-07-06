using BlogApp.Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.DataAccess.Configuration
{
    public class PostsConfiguration : EntityConfiguration<Post>
    {
        protected override void ConfigureChildRules(EntityTypeBuilder<Post> builder)
        {
            builder.Property(x => x.Title).HasMaxLength(20).IsRequired();
            builder.Property(x => x.Content).IsRequired();
            builder.Property(x => x.CategoryId).IsRequired();
            builder.Property(x => x.UserId).IsRequired();

            builder.HasIndex(x => x.Title);
            #region Relationships
            // post- comment komentari koji pripadaju objavi
            builder.HasMany(p => p.Comments)
                .WithOne(c => c.Post)
                .HasForeignKey(c => c.PostId)
                .OnDelete(Microsoft.EntityFrameworkCore.DeleteBehavior.Cascade);

            // post - postsImage
            builder.HasMany(p => p.PostsImages)
                .WithOne(pi => pi.Post)
                .HasForeignKey(pi => pi.PostId)
                .OnDelete(Microsoft.EntityFrameworkCore.DeleteBehavior.Cascade);

            #endregion

            // veza sa vezivnom tabelom lajkovanih objava
            builder.HasMany(p => p.UsersWhoLiked)
               .WithOne(x => x.Post)
               .HasForeignKey(x => x.PostId)
               .OnDelete(Microsoft.EntityFrameworkCore.DeleteBehavior.Cascade);
        }
    }
}
