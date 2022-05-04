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
using Uh = SharpMeasures.Generators.Units.GeneratedUnitAttribute;

[GeneratedScalarQuantity(typeof(UnitOfLength))]
public class Length { }

[GeneratedScalarQuantity(typeof(UnitOfTime))]
public class Time { }

[GeneratedUnit(typeof(Length))]
public partial class UnitOfLength { }

[Uh(typeof(Time))]
public partial class UnitOfTime { }";

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
