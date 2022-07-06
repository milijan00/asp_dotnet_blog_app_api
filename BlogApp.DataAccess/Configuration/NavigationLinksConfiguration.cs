using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlogApp.Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlogApp.DataAccess.Configuration
{
    public class NavigationLinksConfiguration : EntityConfiguration<NavigationLink>
    {
        protected override void ConfigureChildRules(EntityTypeBuilder<NavigationLink> builder)
        {
            builder.Property(x => x.Name).HasMaxLength(30).IsRequired();
            builder.Property(x => x.Route).HasMaxLength(30).IsRequired();
        }
    }
}
