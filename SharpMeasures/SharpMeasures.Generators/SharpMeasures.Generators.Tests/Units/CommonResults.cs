namespace SharpMeasures.Generators.Tests.Units;

using SharpMeasures.Generators.Tests.Verify;

internal static class CommonResults
{
    public static GeneratorVerifier Length_OnlyFixedMetre { get; }
        = GeneratorVerifier.Construct<SharpMeasuresGenerator>(Length_OnlyFixedMetre_Source);

    public static GeneratorVerifier Length_NoDefinitions { get; }
        = GeneratorVerifier.Construct<SharpMeasuresGenerator>(Length_NoDefinitions_Source);

    public static GeneratorVerifier Temperature_OnlyFixedKelvin { get; }
        = GeneratorVerifier.Construct<SharpMeasuresGenerator>(Temperature_OnlyFixedKelvin_Source);

    public static GeneratorVerifier Temperature_NoDefinitions { get; }
        = GeneratorVerifier.Construct<SharpMeasuresGenerator>(Temperature_NoDefinitions_Source);

    public static GeneratorVerifier LengthTimeSpeed_NoDerivable { get; }
        = GeneratorVerifier.Construct<SharpMeasuresGenerator>(LengthTimeSpeed_NoDerivable_Source);

    private const string Length_OnlyFixedMetre_Source = @"
using SharpMeasures.Generators.Scalars;
using SharpMeasures.Generators.Units;

[SharpMeasuresScalar(typeof(UnitOfLength))]
public partial class Length { }

[FixedUnit(""Metre"", ""Metres"", 1)]
[SharpMeasuresUnit(typeof(Length))]
public partial class UnitOfLength { }
";

    private const string Length_NoDefinitions_Source = @"
using SharpMeasures.Generators.Scalars;
using SharpMeasures.Generators.Units;

[SharpMeasuresScalar(typeof(UnitOfLength))]
public partial class Length { }

[SharpMeasuresUnit(typeof(Length))]
public partial class UnitOfLength { }
";

    private const string Temperature_OnlyFixedKelvin_Source = @"
using SharpMeasures.Generators.Scalars;
using SharpMeasures.Generators.Units;

[SharpMeasuresScalar(typeof(UnitOfTemperature))]
public partial class TemperatureDifference { }

[FixedUnit(""Kelvin"", ""Kelvin"", 1, Bias = 0)]
[SharpMeasuresUnit(typeof(TemperatureDifference), Biased = true)]
public partial class UnitOfTemperature { }
";

    private const string Temperature_NoDefinitions_Source = @"
using SharpMeasures.Generators.Scalars;
using SharpMeasures.Generators.Units;

[SharpMeasuresScalar(typeof(UnitOfTemperature))]
public partial class TemperatureDifference { }

[FixedUnit(""Kelvin"", ""Kelvin"", 1, Bias = 0)]
[SharpMeasuresUnit(typeof(TemperatureDifference), Biased = true)]
public partial class UnitOfTemperature { }
";

    private const string LengthTimeSpeed_NoDerivable_Source = @"
using SharpMeasures.Generators.Scalars;
using SharpMeasures.Generators.Units;

[SharpMeasuresScalar(typeof(UnitOfLength))]
public partial class Length { }

[SharpMeasuresScalar(typeof(UnitOfTime))]
public partial class Time { }

[SharpMeasuresScalar(typeof(UnitOfSpeed))]
public partial class Speed { }

[SharpMeasuresUnit(typeof(Length))]
public partial class UnitOfLength { }

[SharpMeasuresUnit(typeof(Time))]
public partial class UnitOfTime { }

[SharpMeasuresUnit(typeof(Speed))]
public partial class UnitOfSpeed { }
";
}
