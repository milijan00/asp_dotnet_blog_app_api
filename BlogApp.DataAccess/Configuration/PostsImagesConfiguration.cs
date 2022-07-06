using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlogApp.Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlogApp.DataAccess.Configuration
{
    public class PostsImagesConfiguration : EntityConfiguration<PostsImage>
    {
        protected override void ConfigureChildRules(EntityTypeBuilder<PostsImage> builder)
        {
            builder.Property(x => x.Source).HasMaxLength(255).IsRequired();
            builder.Property(x => x.PostId).IsRequired();
        }
    }
}
