namespace SharpMeasures.Generators.Tests.Units.Definitions.Fixed;

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

[FixedUnit(""Metre"", 1)]
[GeneratedUnit(typeof(Length))]
public partial class UnitOfLength { }
";

        GeneratorVerifier.Construct<SharpMeasuresGenerator>(source).AssertIdenticalSources(CommonResults.Length_NoDefinitions);
    }

    [Fact]
    public void NameNull_NoAdditionalSource()
    {
        string source = @"
using SharpMeasures.Generators.Scalars;
using SharpMeasures.Generators.Units;

[GeneratedScalar(typeof(UnitOfLength))]
public partial class Length { }

[FixedUnit(null, ""Metres"", 1)]
[GeneratedUnit(typeof(Length))]
public partial class UnitOfLength { }
";

        GeneratorVerifier.Construct<SharpMeasuresGenerator>(source).AssertIdenticalSources(CommonResults.Length_NoDefinitions);
    }

    [Fact]
    public void PluralNull_NoAdditionalSource()
    {
        string source = @"
using SharpMeasures.Generators.Scalars;
using SharpMeasures.Generators.Units;

[GeneratedScalar(typeof(UnitOfLength))]
public partial class Length { }

[FixedUnit(""Metre"", null, 1)]
[GeneratedUnit(typeof(Length))]
public partial class UnitOfLength { }
";

        GeneratorVerifier.Construct<SharpMeasuresGenerator>(source).AssertIdenticalSources(CommonResults.Length_NoDefinitions);
    }

    [Fact]
    public void ValueNull_NoAdditionalSource()
    {
        string source = @"
using SharpMeasures.Generators.Scalars;
using SharpMeasures.Generators.Units;

[GeneratedScalar(typeof(UnitOfLength))]
public partial class Length { }

[FixedUnit(""Metre"", ""Metres"", null)]
[GeneratedUnit(typeof(Length))]
public partial class UnitOfLength { }
";

        GeneratorVerifier.Construct<SharpMeasuresGenerator>(source).AssertIdenticalSources(CommonResults.Length_NoDefinitions);
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
[FixedUnit(""Metre"", ""Meters"", 1)]
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
[FixedUnit(""Meter"", ""Metres"", 1)]
[GeneratedUnit(typeof(Length))]
public partial class UnitOfLength { }
";

        GeneratorVerifier.Construct<SharpMeasuresGenerator>(source).AssertIdenticalSources(CommonResults.Length_OnlyFixedMetre);
    }
}
