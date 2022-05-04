namespace SharpMeasures.Generators.Tests.Units;

using SharpMeasures.Generators.Tests.Verify;
using SharpMeasures.Generators.Units;

internal static class CommonResults
{
    public static GeneratorVerifier Length_OnlyFixedMetre { get; }
        = GeneratorVerifier.Construct<UnitGenerator>(Length_OnlyFixedMetre_Source);

    public static GeneratorVerifier Length_NoDefinitions { get; }
        = GeneratorVerifier.Construct<UnitGenerator>(Length_NoDefinitions_Source);

    public static GeneratorVerifier Temperature_OnlyFixedKelvin { get; }
        = GeneratorVerifier.Construct<UnitGenerator>(Temperature_OnlyFixedKelvin_Source);

    public static GeneratorVerifier Temperature_NoDefinitions { get; }
        = GeneratorVerifier.Construct<UnitGenerator>(Temperature_NoDefinitions_Source);

    public static GeneratorVerifier LengthTimeSpeed_NoDerivable { get; }
        = GeneratorVerifier.Construct<UnitGenerator>(LengthTimeSpeed_NoDerivable_Source);

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
public partial class UnitOfLength { }

[GeneratedUnit(typeof(Time))]
public partial class UnitOfTime { }

[GeneratedUnit(typeof(Speed))]
public partial class UnitOfSpeed { }
";
}
