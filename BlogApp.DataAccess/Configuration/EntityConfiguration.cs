using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlogApp.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlogApp.DataAccess.Configuration
{
    public abstract class EntityConfiguration<T> : IEntityTypeConfiguration<T> where T : Entity
    {
        public void Configure(EntityTypeBuilder<T> builder)
        {
            // base configuration 
            builder.Property(x => x.IsActive).HasDefaultValue(true);
            builder.Property(x => x.Id).IsRequired();
            builder.Property(x => x.UpdatedAt).IsRequired(false);
            builder.Property(x => x.UpdatedBy).IsRequired(false);
            // inherited configuration
            this.ConfigureChildRules(builder);
        }
        protected abstract void ConfigureChildRules(EntityTypeBuilder<T> builder);
        
    }
}
