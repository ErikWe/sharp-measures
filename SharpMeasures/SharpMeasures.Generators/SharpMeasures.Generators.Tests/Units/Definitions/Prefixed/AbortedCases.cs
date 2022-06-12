namespace SharpMeasures.Generators.Tests.Units.Definitions.Prefixed;

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

[GeneratedScalar(typeof(UnitOfLength))]
public partial class Length { }

[FixedUnit(""Metre"", ""Metres"", 1)]
[PrefixedUnit(""Kilometre"", ""Kilometres"", ""Metre"")]
[GeneratedUnit(typeof(Length))]
public partial class UnitOfLength { }
";

        GeneratorVerifier.Construct<SharpMeasuresGenerator>(source).AssertIdenticalSources(CommonResults.Length_OnlyFixedMetre);
    }

    [Fact]
    public void BaseUnitNotFound_NoAdditionalSource()
    {
        string source = @"
using SharpMeasures.Generators.Scalars;
using SharpMeasures.Generators.Units;

[GeneratedScalar(typeof(UnitOfLength))]
public partial class Length { }

[FixedUnit(""Metre"", ""Metres"", 1)]
[PrefixedUnit(""Kilometre"", ""Kilometres"", ""Invalid"", MetricPrefixName.Kilo)]
[GeneratedUnit(typeof(Length))]
public partial class UnitOfLength { }
";

        GeneratorVerifier.Construct<SharpMeasuresGenerator>(source).AssertIdenticalSources(CommonResults.Length_OnlyFixedMetre);
    }

    [Fact]
    public void NameNull_NoAdditionalSource()
    {
        string source = @"
using SharpMeasures.Generators.Scalars;
using SharpMeasures.Generators.Units;

[GeneratedScalar(typeof(UnitOfLength))]
public partial class Length { }

[FixedUnit(""Metre"", ""Metres"", 1)]
[PrefixedUnit(null, ""Kilometres"", ""Metre"", MetricPrefixNames.Kilo)]
[GeneratedUnit(typeof(Length))]
public partial class UnitOfLength { }
";

        GeneratorVerifier.Construct<SharpMeasuresGenerator>(source).AssertIdenticalSources(CommonResults.Length_OnlyFixedMetre);
    }

    [Fact]
    public void PluralNull_NoAdditionalSource()
    {
        string source = @"
using SharpMeasures.Generators.Scalars;
using SharpMeasures.Generators.Units;

[GeneratedScalar(typeof(UnitOfLength))]
public partialclass Length { }

[FixedUnit(""Metre"", ""Metres"", 1)]
[PrefixedUnit(""Kilometre"", null, ""Metre"", MetricPrefixNames.Kilo)]
[GeneratedUnit(typeof(Length))]
public partial class UnitOfLength { }
";

        GeneratorVerifier.Construct<SharpMeasuresGenerator>(source).AssertIdenticalSources(CommonResults.Length_OnlyFixedMetre);
    }

    [Fact]
    public void BaseUnitNull_NoAdditionalSource()
    {
        string source = @"
using SharpMeasures.Generators.Scalars;
using SharpMeasures.Generators.Units;

[GeneratedScalar(typeof(UnitOfLength))]
public partial class Length { }

[FixedUnit(""Metre"", ""Metres"", 1)]
[PrefixedUnit(""Kilometre"", ""Kilometres"", null, MetricPrefixNames.Kilo)]
[GeneratedUnit(typeof(Length))]
public partial class UnitOfLength { }
";

        GeneratorVerifier.Construct<SharpMeasuresGenerator>(source).AssertIdenticalSources(CommonResults.Length_OnlyFixedMetre);
    }

    [Fact]
    public void NameDuplicate_NoAdditionalSource()
    {
        string source = @"
using SharpMeasures.Generators.Scalars;
using SharpMeasures.Generators.Units;

[GeneratedScalar(typeof(UnitOfLength))]
public partial class Length { }

[FixedUnit(""Metre"", ""Metres"", 1)]
[PrefixedUnit(""Metre"", ""Meters"", ""Metre"", MetricPrefixNames.Identity)]
[GeneratedUnit(typeof(Length))]
public partial class UnitOfLength { }
";

        GeneratorVerifier.Construct<SharpMeasuresGenerator>(source).AssertIdenticalSources(CommonResults.Length_OnlyFixedMetre);
    }

    [Fact]
    public void PluralDuplicate_NoAdditionalSource()
    {
        string source = @"
using SharpMeasures.Generators.Scalars;
using SharpMeasures.Generators.Units;

[GeneratedScalar(typeof(UnitOfLength))]
public partial class Length { }

[FixedUnit(""Metre"", ""Metres"", 1)]
[PrefixedUnit(""Meter"", ""Metres"", ""Metre"", MetricPrefixNames.Identity)]
[GeneratedUnit(typeof(Length))]
public partial class UnitOfLength { }
";

        GeneratorVerifier.Construct<SharpMeasuresGenerator>(source).AssertIdenticalSources(CommonResults.Length_OnlyFixedMetre);
    }

    [Fact]
    public void PrefixNull_NoAdditionalSource()
    {
        string source = @"
using SharpMeasures.Generators.Scalars;
using SharpMeasures.Generators.Units;

[GeneratedScalar(typeof(UnitOfLength))]
public partial class Length { }

[FixedUnit(""Metre"", ""Metres"", 1)]
[PrefixedUnit(""Kilometre"", ""Kilometres"", ""Metre"", null)]
[GeneratedUnit(typeof(Length))]
public partial class UnitOfLength { }
";

        GeneratorVerifier.Construct<SharpMeasuresGenerator>(source).AssertIdenticalSources(CommonResults.Length_OnlyFixedMetre);
    }

    [Fact]
    public void PrefixNotDefined_NoAdditionalSource()
    {
        string source = @"
using SharpMeasures.Generators.Scalars;
using SharpMeasures.Generators.Units;

[GeneratedScalar(typeof(UnitOfLength))]
public partial class Length { }

[FixedUnit(""Metre"", ""Metres"", 1)]
[PrefixedUnit(""Foometre"", ""Foometres"", ""Metre"", (MetricPrefixNames)-1)]
[GeneratedUnit(typeof(Length))]
public partial class UnitOfLength { }
";

        GeneratorVerifier.Construct<SharpMeasuresGenerator>(source).AssertIdenticalSources(CommonResults.Length_OnlyFixedMetre);
    }
}
