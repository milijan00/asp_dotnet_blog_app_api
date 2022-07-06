using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlogApp.Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace BlogApp.DataAccess.Configuration
{
    public  class CategoriesConfiguration : EntityConfiguration<Category>
    {
        protected override void ConfigureChildRules(EntityTypeBuilder<Category> builder)
        {
            builder.Property(x => x.Name).HasMaxLength(30).IsRequired();

            // categories - post
            builder.HasMany(c  => c.Posts) 
                .WithOne(p => p.Category)
                .HasForeignKey(p => p.CategoryId)
                .OnDelete(Microsoft.EntityFrameworkCore.DeleteBehavior.Cascade);

            builder.HasIndex(x => x.Name).IsUnique();
        }
    }
}
