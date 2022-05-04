namespace SharpMeasures.Generators.Tests.Units.Definitions.Aliases;

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

[GeneratedScalarQuantity(typeof(UnitOfLength))]
public class Length { }

[FixedUnit(""Metre"", ""Metres"", 1)]
[UnitAlias(""Meter"", ""Meters"")]
[GeneratedUnit(typeof(Length))]
public partial class UnitOfLength { }
";
        GeneratorVerifier.Construct<UnitGenerator>(source).IdenticalOutputTo(CommonResults.Length_OnlyFixedMetre);
    }

    [Fact]
    public void BaseUnitNotFound()
    {
        string source = @"
using SharpMeasures.Generators.Scalars;
using SharpMeasures.Generators.Units;

[GeneratedScalarQuantity(typeof(UnitOfLength))]
public class Length { }

[FixedUnit(""Metre"", ""Metres"", 1)]
[UnitAlias(""Meter"", ""Meters"", ""Invalid"")]
[GeneratedUnit(typeof(Length))]
public partial class UnitOfLength { }
";

        GeneratorVerifier.Construct<UnitGenerator>(source).IdenticalOutputTo(CommonResults.Length_OnlyFixedMetre);
    }

    [Fact]
    public void NameNull()
    {
        string source = @"
using SharpMeasures.Generators.Scalars;
using SharpMeasures.Generators.Units;

[GeneratedScalarQuantity(typeof(UnitOfLength))]
public class Length { }

[FixedUnit(""Metre"", ""Metres"", 1)]
[UnitAlias(null, ""Meters"", ""Metre"")]
[GeneratedUnit(typeof(Length))]
public partial class UnitOfLength { }
";

        GeneratorVerifier.Construct<UnitGenerator>(source).IdenticalOutputTo(CommonResults.Length_OnlyFixedMetre);
    }

    [Fact]
    public void PluralNullAndNotConstant()
    {
        string source = @"
using SharpMeasures.Generators.Scalars;
using SharpMeasures.Generators.Units;

[GeneratedScalarQuantity(typeof(UnitOfLength))]
public class Length { }

[FixedUnit(""Metre"", ""Metres"", 1)]
[UnitAlias(""Meter"", null, ""Metre"")]
[GeneratedUnit(typeof(Length))]
public partial class UnitOfLength { }
";

        GeneratorVerifier.Construct<UnitGenerator>(source).IdenticalOutputTo(CommonResults.Length_OnlyFixedMetre);
    }

    [Fact]
    public void BaseUnitNull()
    {
        string source = @"
using SharpMeasures.Generators.Scalars;
using SharpMeasures.Generators.Units;

[GeneratedScalarQuantity(typeof(UnitOfLength))]
public class Length { }

[FixedUnit(""Metre"", ""Metres"", 1)]
[UnitAlias(""Meter"", ""Meters"", null)]
[GeneratedUnit(typeof(Length))]
public partial class UnitOfLength { }
";

        GeneratorVerifier.Construct<UnitGenerator>(source).IdenticalOutputTo(CommonResults.Length_OnlyFixedMetre);
    }

    [Fact]
    public void DuplicateName()
    {
        string source = @"
using SharpMeasures.Generators.Scalars;
using SharpMeasures.Generators.Units;

[GeneratedScalarQuantity(typeof(UnitOfLength))]
public class Length { }

[FixedUnit(""Metre"", ""Metres"", 1)]
[UnitAlias(""Metre"", ""Metres"", ""Metre"")]
[GeneratedUnit(typeof(Length))]
public partial class UnitOfLength { }
";

        GeneratorVerifier.Construct<UnitGenerator>(source).IdenticalOutputTo(CommonResults.Length_OnlyFixedMetre);
    }
}
