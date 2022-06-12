﻿namespace SharpMeasures.Generators.Tests.Units.Definitions.Offset;

using SharpMeasures.Generators.Tests.Verify;
using SharpMeasures.Generators.Units;

using VerifyXunit;

using Xunit;

[UsesVerify]
public class AbortedCases
{
    [Fact]
    public void NoMatchingConstructor()
    {
        string source = @"
using SharpMeasures.Generators.Scalars;
using SharpMeasures.Generators.Units;

[GeneratedScalarQuantity(typeof(UnitOfTemperature))]
public class TemperatureDifference { }

[FixedUnit(""Kelvin"", ""Kelvin"", 1, Bias = 0)]
[OffsetUnit(""Celsius"", ""Celsius"", ""Kelvin"")]
[GeneratedUnit(typeof(TemperatureDifference), Biased = true)]
public partial class UnitOfTemperature { }
";

        GeneratorVerifier.Construct<UnitGenerator>(source).IdenticalOutputTo(CommonResults.Temperature_OnlyFixedKelvin);
    }

    [Fact]
    public void BaseUnitNotFound()
    {
        string source = @"
using SharpMeasures.Generators.Scalars;
using SharpMeasures.Generators.Units;

[GeneratedScalarQuantity(typeof(UnitOfTemperature))]
public class TemperatureDifference { }

[FixedUnit(""Kelvin"", ""Kelvin"", 1, Bias = 0)]
[OffsetUnit(""Celsius"", ""Celsius"", ""Invalid"", -273.15)]
[GeneratedUnit(typeof(TemperatureDifference), Biased = true)]
public partial class UnitOfTemperature { }
";

        GeneratorVerifier.Construct<UnitGenerator>(source).IdenticalOutputTo(CommonResults.Temperature_OnlyFixedKelvin);
    }

    [Fact]
    public void NameNull()
    {
        string source = @"
using SharpMeasures.Generators.Scalars;
using SharpMeasures.Generators.Units;

[GeneratedScalarQuantity(typeof(UnitOfTemperature))]
public class TemperatureDifference { }

[FixedUnit(""Kelvin"", ""Kelvin"", 1, Bias = 0)]
[OffsetUnit(null, ""Celsius"", ""Kelvin"", -273.15)]
[GeneratedUnit(typeof(TemperatureDifference), Biased = true)]
public partial class UnitOfTemperature { }
";

        GeneratorVerifier.Construct<UnitGenerator>(source).IdenticalOutputTo(CommonResults.Temperature_OnlyFixedKelvin);
    }

    [Fact]
    public void PluralNullAndNotConstant()
    {
        string source = @"
using SharpMeasures.Generators.Scalars;
using SharpMeasures.Generators.Units;

[GeneratedScalarQuantity(typeof(UnitOfTemperature))]
public class TemperatureDifference { }

[FixedUnit(""Kelvin"", ""Kelvin"", 1, Bias = 0)]
[OffsetUnit(""Celcius"", null, ""Kelvin"", -273.15)]
[GeneratedUnit(typeof(TemperatureDifference), Biased = true)]
public partial class UnitOfTemperature { }
";

        GeneratorVerifier.Construct<UnitGenerator>(source).IdenticalOutputTo(CommonResults.Temperature_OnlyFixedKelvin);
    }

    [Fact]
    public void BaseUnitNull()
    {
        string source = @"
using SharpMeasures.Generators.Scalars;
using SharpMeasures.Generators.Units;

[GeneratedScalarQuantity(typeof(UnitOfTemperature))]
public class TemperatureDifference { }

[FixedUnit(""Kelvin"", ""Kelvin"", 1, Bias = 0)]
[OffsetUnit(""Celcius"", ""Celcius"", null, -273.15)]
[GeneratedUnit(typeof(TemperatureDifference), Biased = true)]
public partial class UnitOfTemperature { }
";

        GeneratorVerifier.Construct<UnitGenerator>(source).IdenticalOutputTo(CommonResults.Temperature_OnlyFixedKelvin);
    }

    [Fact]
    public void DuplicateName()
    {
        string source = @"
using SharpMeasures.Generators.Scalars;
using SharpMeasures.Generators.Units;

[GeneratedScalarQuantity(typeof(UnitOfTemperature))]
public class TemperatureDifference { }

[FixedUnit(""Kelvin"", ""Kelvin"", 1, Bias = 0)]
[OffsetUnit(""Kelvin"", ""Kelvin"", ""Kelvin"", -273.15)]
[GeneratedUnit(typeof(TemperatureDifference), Biased = true)]
public partial class UnitOfTemperature { }
";

        GeneratorVerifier.Construct<UnitGenerator>(source).IdenticalOutputTo(CommonResults.Temperature_OnlyFixedKelvin);
    }

    [Fact]
    public void OffsetNull()
    {
        string source = @"
using SharpMeasures.Generators.Scalars;
using SharpMeasures.Generators.Units;

[GeneratedScalarQuantity(typeof(UnitOfTemperature))]
public class TemperatureDifference { }

[FixedUnit(""Kelvin"", ""Kelvin"", 1, Bias = 0)]
[OffsetUnit(""Celsius"", ""Celsius"", ""Kelvin"", null)]
[GeneratedUnit(typeof(TemperatureDifference), Biased = true)]
public partial class UnitOfTemperature { }
";

        GeneratorVerifier.Construct<UnitGenerator>(source).IdenticalOutputTo(CommonResults.Temperature_OnlyFixedKelvin);
    }
}