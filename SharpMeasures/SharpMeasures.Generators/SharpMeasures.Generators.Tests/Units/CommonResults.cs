namespace SharpMeasures.Generators.Tests.Units;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Tests.Utility;
using SharpMeasures.Generators.Units;

internal static class CommonResults
{
    public static GeneratorDriverRunResult Length_OnlyFixedMetre { get; }
        = DriverUtility.ConstructAndRunDriver<UnitGenerator>(Length_OnlyFixedMetre_Source).GetRunResult();

    public static GeneratorDriverRunResult Length_NoDefinitions { get; }
        = DriverUtility.ConstructAndRunDriver<UnitGenerator>(Length_NoDefinitions_Source).GetRunResult();

    public static GeneratorDriverRunResult Temperature_OnlyFixedKelvin { get; }
        = DriverUtility.ConstructAndRunDriver<UnitGenerator>(Temperature_OnlyFixedKelvin_Source).GetRunResult();

    public static GeneratorDriverRunResult Temperature_NoDefinitions { get; }
        = DriverUtility.ConstructAndRunDriver<UnitGenerator>(Temperature_NoDefinitions_Source).GetRunResult();

    public static GeneratorDriverRunResult LengthTimeSpeed_NoDerivable { get; }
        = DriverUtility.ConstructAndRunDriver<UnitGenerator>(LengthTimeSpeed_NoDerivable_Source).GetRunResult();

    private const string Length_OnlyFixedMetre_Source = @"
using SharpMeasures.Generators.Scalars;
using SharpMeasures.Generators.Units;

[GeneratedScalarQuantity(typeof(UnitOfLength))]
public class Length { }

[FixedUnit(""Metre"", ""Metres"", 1)]
[GeneratedUnit(typeof(Length))]
public partial class UnitOfLength { }
";

    private const string Length_NoDefinitions_Source = @"
using SharpMeasures.Generators.Scalars;
using SharpMeasures.Generators.Units;

[GeneratedScalarQuantity(typeof(UnitOfLength))]
public class Length { }

[GeneratedUnit(typeof(Length))]
public partial class UnitOfLength { }
";

    private const string Temperature_OnlyFixedKelvin_Source = @"
using SharpMeasures.Generators.Scalars;
using SharpMeasures.Generators.Units;

[GeneratedScalarQuantity(typeof(UnitOfTemperature))]
public class TemperatureDifference { }

[FixedUnit(""Kelvin"", ""Kelvin"", 1, Bias = 0)]
[GeneratedUnit(typeof(TemperatureDifference), Biased = true)]
public partial class UnitOfTemperature { }
";

    private const string Temperature_NoDefinitions_Source = @"
using SharpMeasures.Generators.Scalars;
using SharpMeasures.Generators.Units;

[GeneratedScalarQuantity(typeof(UnitOfTemperature))]
public class TemperatureDifference { }

[FixedUnit(""Kelvin"", ""Kelvin"", 1, Bias = 0)]
[GeneratedUnit(typeof(TemperatureDifference), Biased = true)]
public partial class UnitOfTemperature { }
";

    private const string LengthTimeSpeed_NoDerivable_Source = @"
using SharpMeasures.Generators.Scalars;
using SharpMeasures.Generators.Units;

[GeneratedScalarQuantity(typeof(UnitOfLength))]
public class Length { }

[GeneratedScalarQuantity(typeof(UnitOfTime))]
public class Time { }

[GeneratedScalarQuantity(typeof(UnitOfSpeed))]
public class Speed { }

[GeneratedUnit(typeof(Length))]
public class UnitOfLength { }

[GeneratedUnit(typeof(Time))]
public class UnitOfTime { }

[GeneratedUnit(typeof(Speed))]
public partial class UnitOfSpeed { }
";
}
