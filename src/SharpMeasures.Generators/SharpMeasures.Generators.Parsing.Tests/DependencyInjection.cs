namespace SharpMeasures.Generators.Parsing.Tests;

using Microsoft.Extensions.DependencyInjection;

using SharpMeasures.Generators.DriverUtility.Extensions;
using SharpMeasures.Generators.Parsing.Extensions;

using System;

internal static class DependencyInjection
{
    private static IServiceProvider? Provider { get; set; }

    private static IServiceProvider GetProvider()
    {
        if (Provider is not null)
        {
            return Provider;
        }

        ServiceCollection services = new();

        services.AddSharpMeasuresAttributeParsing();
        services.AddGeneratorDriverUtility();

        Provider = services.BuildServiceProvider();

        return Provider;
    }

    public static T GetRequiredService<T>() where T : notnull => GetProvider().GetRequiredService<T>();
}
