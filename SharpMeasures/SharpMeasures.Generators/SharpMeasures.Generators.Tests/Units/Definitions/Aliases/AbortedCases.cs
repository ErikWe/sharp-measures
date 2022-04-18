namespace SharpMeasures.Generators.Tests.Units.Definitions.Aliases;

using SharpMeasures.Generators.Tests.Utility;
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
using SharpMeasures.Generators;
using SharpMeasures.Generators.Units;

[GeneratedScalarQuantity(typeof(UnitOfLength))]
public class Length { }

[FixedUnit(""Metre"", ""Metres"", 1)]
[UnitAlias(""Meter"", ""Meters"")]
[GeneratedUnit(typeof(Length))]
public partial class UnitOfLength { }
";
        VerifyGenerator.VerifyIdentical<UnitGenerator>(CommonResults.Length_OnlyFixedMetre, source);
    }

    [Fact]
    public void BaseUnitNotFound()
    {
        string source = @"
using SharpMeasures.Generators;
using SharpMeasures.Generators.Units;

[GeneratedScalarQuantity(typeof(UnitOfLength))]
public class Length { }

[FixedUnit(""Metre"", ""Metres"", 1)]
[UnitAlias(""Meter"", ""Meters"", ""Invalid"")]
[GeneratedUnit(typeof(Length))]
public partial class UnitOfLength { }
";

        VerifyGenerator.VerifyIdentical<UnitGenerator>(CommonResults.Length_OnlyFixedMetre, source);
    }

    [Fact]
    public void NameNull()
    {
        string source = @"
using SharpMeasures.Generators;
using SharpMeasures.Generators.Units;

[GeneratedScalarQuantity(typeof(UnitOfLength))]
public class Length { }

[FixedUnit(""Metre"", ""Metres"", 1)]
[UnitAlias(null, ""Meters"", ""Metre"")]
[GeneratedUnit(typeof(Length))]
public partial class UnitOfLength { }
";

        VerifyGenerator.VerifyIdentical<UnitGenerator>(CommonResults.Length_OnlyFixedMetre, source);
    }

    [Fact]
    public void PluralNullAndNotConstant()
    {
        string source = @"
using SharpMeasures.Generators;
using SharpMeasures.Generators.Units;

[GeneratedScalarQuantity(typeof(UnitOfLength))]
public class Length { }

[FixedUnit(""Metre"", ""Metres"", 1)]
[UnitAlias(""Meter"", null, ""Metre"")]
[GeneratedUnit(typeof(Length))]
public partial class UnitOfLength { }
";

        VerifyGenerator.VerifyIdentical<UnitGenerator>(CommonResults.Length_OnlyFixedMetre, source);
    }

    [Fact]
    public void BaseUnitNull()
    {
        string source = @"
using SharpMeasures.Generators;
using SharpMeasures.Generators.Units;

[GeneratedScalarQuantity(typeof(UnitOfLength))]
public class Length { }

[FixedUnit(""Metre"", ""Metres"", 1)]
[UnitAlias(""Meter"", ""Meters"", null)]
[GeneratedUnit(typeof(Length))]
public partial class UnitOfLength { }
";

        VerifyGenerator.VerifyIdentical<UnitGenerator>(CommonResults.Length_OnlyFixedMetre, source);
    }

    [Fact]
    public void DuplicateName()
    {
        string source = @"
using SharpMeasures.Generators;
using SharpMeasures.Generators.Units;

[GeneratedScalarQuantity(typeof(UnitOfLength))]
public class Length { }

[FixedUnit(""Metre"", ""Metres"", 1)]
[UnitAlias(""Metre"", ""Metres"", ""Metre"")]
[GeneratedUnit(typeof(Length))]
public partial class UnitOfLength { }
";

        VerifyGenerator.VerifyIdentical<UnitGenerator>(CommonResults.Length_OnlyFixedMetre, source);
    }
}
