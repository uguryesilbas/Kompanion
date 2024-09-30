using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Kompanion.Application.Extensions;

public static class ConfigurationExtensions
{
    public static IServiceCollection AddOptions<TOption>(this IServiceCollection services, string settingsSectionName, bool validateDataAnnotations) where TOption : class
    {
        using ServiceProvider serviceProvider = services.BuildServiceProvider();

        IConfiguration configuration = serviceProvider.GetRequiredService<IConfiguration>();

        IConfigurationSection configurationSection = configuration.GetSection(settingsSectionName);

        OptionsBuilder<TOption> optionsBuilder = services.AddOptions<TOption>().Bind(configurationSection);

        if (validateDataAnnotations)
        {
            optionsBuilder
                .ValidateDataAnnotations()
                .ValidateOnStart();
        }

        return services;
    }

    public static IServiceCollection AddOptions<TOption, TOptionValidation>(this IServiceCollection services, string settingsSectionName) where TOption : class where TOptionValidation : class, IValidateOptions<TOption>
    {
        using ServiceProvider serviceProvider = services.BuildServiceProvider();

        IConfiguration configuration = serviceProvider.GetRequiredService<IConfiguration>();

        IConfigurationSection configurationSection = configuration.GetSection(settingsSectionName);

        services.Configure<TOption>(configurationSection.GetSection(settingsSectionName));

        return services.AddSingleton<IValidateOptions<TOption>, TOptionValidation>();
    }

    public static TOption GetOptions<TOption>(this IServiceProvider serviceProvider, string settingsSectionName) where TOption : class
    {
        return serviceProvider.GetRequiredService<IConfiguration>().GetSection(settingsSectionName).Get<TOption>();
    }

    public static TOption GetOptions<TOption>(this IServiceCollection services, string settingsSectionName) where TOption : class
    {
        using ServiceProvider serviceProvider = services.BuildServiceProvider();

        return serviceProvider.GetOptions<TOption>(settingsSectionName);
    }
}