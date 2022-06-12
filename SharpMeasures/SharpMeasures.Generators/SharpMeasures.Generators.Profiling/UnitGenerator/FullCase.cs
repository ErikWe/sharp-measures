namespace SharpMeasures.Generators.Profiling.UnitGenerator;

using Microsoft.CodeAnalysis;

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

[GeneratedScalar(typeof(UnitOfLength))]
public partial class Length { }

[GeneratedUnit(typeof(Length))]
public partial class UnitOfLength { }";

        GeneratorDriver driver = DriverConstruction.Construct<SharpMeasuresGenerator>(ProjectPath.Path);

        Stopwatch stopwatch = Stopwatch.StartNew();

        driver = DriverConstruction.Run(source, driver);

        stopwatch.Stop();
        Console.WriteLine(stopwatch.ElapsedMilliseconds);

        var runResult = driver.GetRunResult();

        foreach (GeneratorRunResult result in runResult.Results)
        {
            foreach (GeneratedSourceResult output in result.GeneratedSources)
            {
                Console.WriteLine($"{output.HintName}: {output.SourceText.Length}");
            }
        }
    }
}
