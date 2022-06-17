﻿namespace SharpMeasures.Generators.Tests.Units.Definitions.Aliases;

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

[SharpMeasuresScalar(typeof(UnitOfLength))]
public partial class Length { }

[FixedUnit(""Metre"", ""Metres"", 1)]
[UnitAlias(""Meter"", ""Meters"")]
[SharpMeasuresUnit(typeof(Length))]
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

[SharpMeasuresScalar(typeof(UnitOfLength))]
public partial class Length { }

[FixedUnit(""Metre"", ""Metres"", 1)]
[UnitAlias(""Meter"", ""Meters"", ""Invalid"")]
[SharpMeasuresUnit(typeof(Length))]
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

[SharpMeasuresScalar(typeof(UnitOfLength))]
public partial class Length { }

[FixedUnit(""Metre"", ""Metres"", 1)]
[UnitAlias(null, ""Meters"", ""Metre"")]
[SharpMeasuresUnit(typeof(Length))]
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

[SharpMeasuresScalar(typeof(UnitOfLength))]
public partial class Length { }

[FixedUnit(""Metre"", ""Metres"", 1)]
[UnitAlias(""Meter"", null, ""Metre"")]
[SharpMeasuresUnit(typeof(Length))]
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

[SharpMeasuresScalar(typeof(UnitOfLength))]
public partial class Length { }

[FixedUnit(""Metre"", ""Metres"", 1)]
[UnitAlias(""Meter"", ""Meters"", null)]
[SharpMeasuresUnit(typeof(Length))]
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

[SharpMeasuresScalar(typeof(UnitOfLength))]
public class Length { }

[FixedUnit(""Metre"", ""Metres"", 1)]
[UnitAlias(""Metre"", ""Meters"", ""Metre"")]
[SharpMeasuresUnit(typeof(Length))]
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

[SharpMeasuresScalar(typeof(UnitOfLength))]
public class Length { }

[FixedUnit(""Metre"", ""Metres"", 1)]
[UnitAlias(""Meter"", ""Metres"", ""Metre"")]
[SharpMeasuresUnit(typeof(Length))]
public partial class UnitOfLength { }
";

        GeneratorVerifier.Construct<SharpMeasuresGenerator>(source).AssertIdenticalSources(CommonResults.Length_OnlyFixedMetre);
    }
}
