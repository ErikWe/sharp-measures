namespace SharpMeasures.Generators.Profiling.UnitGenerator;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Units;
using SharpMeasures.Generators.DriverUtility;

using System;
using System.Diagnostics;

internal static class FullCase
{
    public static void Run()
    {
        string source = @"
using SharpMeasures.Generators.Scalars;
using SharpMeasures.Generators.Units;

[GeneratedScalarQuantity(typeof(UnitOfLength))]
public class Length { }

[GeneratedUnit(typeof(Length))]
public readonly partial record struct UnitOfLength { }";

        GeneratorDriver driver = DriverConstruction.Construct<UnitGenerator>(ProjectPath.Path);

        Stopwatch stopwatch = Stopwatch.StartNew();

        driver = DriverConstruction.Run(source, driver);

        stopwatch.Stop();
        Console.WriteLine(stopwatch.ElapsedMilliseconds);

        foreach (GeneratorRunResult result in driver.GetRunResult().Results)
        {
            foreach (GeneratedSourceResult output in result.GeneratedSources)
            {
                Console.WriteLine($"{output.HintName}: {output.SourceText.Length}");
            }
        }
    }
}
