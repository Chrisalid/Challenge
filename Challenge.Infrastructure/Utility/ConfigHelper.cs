using System;
using Microsoft.Extensions.Configuration;

namespace Challenge.Infrastructure.Utility;

public class ConfigHelper
{
    private static IConfiguration configuration;

    public static void Initialize(IConfiguration config)
    {
        configuration = config;
    }

    private static string GetConfiguration(string config)
    {
        if (configuration.GetConnectionString(config) != null)
            return configuration.GetConnectionString(config);

        return configuration.GetSection("AppSettings").GetSection(config).Value;
    }

    public static long DefaultUserId
    {
        get
        {
            try
            {
                _ = long.TryParse(GetConfiguration("DefaultUserId"), out var defaultUserId);
                return defaultUserId;
            }
            catch
            {
                return 1;
            }
        }
    }

    public static string JwtKey
    {
        get
        {
            try
            {
                return GetConfiguration("JwtKey");
            }
            catch
            {
                return string.Empty;
            }
        }
    }
}
