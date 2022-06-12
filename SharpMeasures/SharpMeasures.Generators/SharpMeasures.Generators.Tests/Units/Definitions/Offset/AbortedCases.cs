namespace SharpMeasures.Generators.Tests.Units.Definitions.Offset;

using SharpMeasures.Generators.Tests.Verify;

using VerifyXunit;

using Xunit;

[UsesVerify]
public class AbortedCases
{
    [Fact]
    public void NoMatchingConstructor_NoAdditionalSource()
    {
        string source = @"
using SharpMeasures.Generators.Scalars;
using SharpMeasures.Generators.Units;

[GeneratedScalar(typeof(UnitOfTemperature))]
public partial class TemperatureDifference { }

[FixedUnit(""Kelvin"", ""Kelvin"", 1, Bias = 0)]
[OffsetUnit(""Celsius"", ""Celsius"", ""Kelvin"")]
[GeneratedUnit(typeof(TemperatureDifference), Biased = true)]
public partial class UnitOfTemperature { }
";

        GeneratorVerifier.Construct<SharpMeasuresGenerator>(source).AssertIdenticalSources(CommonResults.Temperature_OnlyFixedKelvin);
    }

    [Fact]
    public void BaseUnitNotFound_NoAdditionalSource()
    {
        string source = @"
using SharpMeasures.Generators.Scalars;
using SharpMeasures.Generators.Units;

[GeneratedScalar(typeof(UnitOfTemperature))]
public partial class TemperatureDifference { }

[FixedUnit(""Kelvin"", ""Kelvin"", 1, Bias = 0)]
[OffsetUnit(""Celsius"", ""Celsius"", ""Invalid"", -273.15)]
[GeneratedUnit(typeof(TemperatureDifference), Biased = true)]
public partial class UnitOfTemperature { }
";

        GeneratorVerifier.Construct<SharpMeasuresGenerator>(source).AssertIdenticalSources(CommonResults.Temperature_OnlyFixedKelvin);
    }

    [Fact]
    public void NameNull_NoAdditionalSource()
    {
        string source = @"
using SharpMeasures.Generators.Scalars;
using SharpMeasures.Generators.Units;

[GeneratedScalar(typeof(UnitOfTemperature))]
public partial class TemperatureDifference { }

[FixedUnit(""Kelvin"", ""Kelvin"", 1, Bias = 0)]
[OffsetUnit(null, ""Celsius"", ""Kelvin"", -273.15)]
[GeneratedUnit(typeof(TemperatureDifference), Biased = true)]
public partial class UnitOfTemperature { }
";

        GeneratorVerifier.Construct<SharpMeasuresGenerator>(source).AssertIdenticalSources(CommonResults.Temperature_OnlyFixedKelvin);
    }

    [Fact]
    public void PluralNull_NoAdditionalSource()
    {
        string source = @"
using SharpMeasures.Generators.Scalars;
using SharpMeasures.Generators.Units;

[GeneratedScalar(typeof(UnitOfTemperature))]
public partial class TemperatureDifference { }

[FixedUnit(""Kelvin"", ""Kelvin"", 1, Bias = 0)]
[OffsetUnit(""Celcius"", null, ""Kelvin"", -273.15)]
[GeneratedUnit(typeof(TemperatureDifference), Biased = true)]
public partial class UnitOfTemperature { }
";

        GeneratorVerifier.Construct<SharpMeasuresGenerator>(source).AssertIdenticalSources(CommonResults.Temperature_OnlyFixedKelvin);
    }

    [Fact]
    public void BaseUnitNull_NoAdditionalSource()
    {
        string source = @"
using SharpMeasures.Generators.Scalars;
using SharpMeasures.Generators.Units;

[GeneratedScalar(typeof(UnitOfTemperature))]
public partial class TemperatureDifference { }

[FixedUnit(""Kelvin"", ""Kelvin"", 1, Bias = 0)]
[OffsetUnit(""Celcius"", ""Celcius"", null, -273.15)]
[GeneratedUnit(typeof(TemperatureDifference), Biased = true)]
public partial class UnitOfTemperature { }
";

        GeneratorVerifier.Construct<SharpMeasuresGenerator>(source).AssertIdenticalSources(CommonResults.Temperature_OnlyFixedKelvin);
    }

    [Fact]
    public void NameDuplicate_NoAdditionalSource()
    {
        string source = @"
using SharpMeasures.Generators.Scalars;
using SharpMeasures.Generators.Units;

[GeneratedScalar(typeof(UnitOfTemperature))]
public partial class TemperatureDifference { }

[FixedUnit(""Kelvin"", ""Kelvin"", 1, Bias = 0)]
[OffsetUnit(""Kelvin"", ""Kelvins"", ""Kelvin"", -273.15)]
[GeneratedUnit(typeof(TemperatureDifference), Biased = true)]
public partial class UnitOfTemperature { }
";

        GeneratorVerifier.Construct<SharpMeasuresGenerator>(source).AssertIdenticalSources(CommonResults.Temperature_OnlyFixedKelvin);
    }

    [Fact]
    public void PluralDuplicate_NoAdditionalSource()
    {
        string source = @"
using SharpMeasures.Generators.Scalars;
using SharpMeasures.Generators.Units;

[GeneratedScalar(typeof(UnitOfTemperature))]
public partial class TemperatureDifference { }

[FixedUnit(""Kelvin"", ""Kelvin"", 1, Bias = 0)]
[OffsetUnit(""OneKelvin"", ""Kelvin"", ""Kelvin"", -273.15)]
[GeneratedUnit(typeof(TemperatureDifference), Biased = true)]
public partial class UnitOfTemperature { }
";

        GeneratorVerifier.Construct<SharpMeasuresGenerator>(source).AssertIdenticalSources(CommonResults.Temperature_OnlyFixedKelvin);
    }

    [Fact]
    public void OffsetNull_NoAdditionalSource()
    {
        string source = @"
using SharpMeasures.Generators.Scalars;
using SharpMeasures.Generators.Units;

[GeneratedScalar(typeof(UnitOfTemperature))]
public partial class TemperatureDifference { }

[FixedUnit(""Kelvin"", ""Kelvin"", 1, Bias = 0)]
[OffsetUnit(""Celsius"", ""Celsius"", ""Kelvin"", null)]
[GeneratedUnit(typeof(TemperatureDifference), Biased = true)]
public partial class UnitOfTemperature { }
";

        GeneratorVerifier.Construct<SharpMeasuresGenerator>(source).AssertIdenticalSources(CommonResults.Temperature_OnlyFixedKelvin);
    }
}
