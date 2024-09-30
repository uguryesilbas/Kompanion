using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Kompanion.Domain.Abstracts;

namespace Kompanion.Infrastructure.Extensions;

public static class ModelBuilderExtensions
{
    public static void ApplyAllConfigurations(this ModelBuilder modelBuilder)
    {
        IEnumerable<Assembly> configurationAssemblies = AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(SelectBaseEntityTypeConfiguration)
            .Distinct();

        foreach (var assembly in configurationAssemblies)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(assembly);
        }
    }

    private static IEnumerable<Assembly> SelectBaseEntityTypeConfiguration(Assembly assembly)
    {
        return assembly.GetTypes()
            .Where(type => type.IsSubclassOf(typeof(BaseEntityTypeConfiguration<>)) && !type.IsAbstract)
            .Select(type => type.Assembly);
    }

}