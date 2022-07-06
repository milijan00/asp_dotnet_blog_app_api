using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlogApp.Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlogApp.DataAccess.Configuration
{
    public class RolesConfiguration : EntityConfiguration<Role>
    {
        protected override void ConfigureChildRules(EntityTypeBuilder<Role> builder)
        {
            builder.Property(x => x.Name).HasMaxLength(30).IsRequired();
            builder.HasIndex(x => x.Name).IsUnique();
            // role - user
            builder.HasMany(r => r.Users)
                .WithOne(u => u.Role)
                .HasForeignKey(u => u.RoleId)
                .OnDelete(Microsoft.EntityFrameworkCore.DeleteBehavior.Cascade);

            
        }
    }
}
