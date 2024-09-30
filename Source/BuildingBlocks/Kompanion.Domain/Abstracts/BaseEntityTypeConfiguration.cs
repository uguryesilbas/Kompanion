using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kompanion.Domain.Abstracts;

public abstract class BaseEntityTypeConfiguration<TEntity>:IEntityTypeConfiguration<TEntity> where TEntity : BaseEntity
{
    public abstract void Configure(EntityTypeBuilder<TEntity> builder);
}