namespace SharpMeasures.Generators.Tests.Units.Definitions.Prefixed;

using SharpMeasures.Generators.Units;
using SharpMeasures.Generators.Tests.Utility;

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
[PrefixedUnit(""Kilometre"", ""Kilometres"", ""Metre"")]
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
[PrefixedUnit(""Kilometre"", ""Kilometres"", ""Invalid"", ""Kilo"")]
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
[PrefixedUnit(null, ""Kilometres"", ""Metre"", ""Kilo"")]
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
[PrefixedUnit(""Kilometre"", null, ""Metre"", ""Kilo"")]
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
[PrefixedUnit(""Kilometre"", ""Kilometres"", null, ""Kilo"")]
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
[PrefixedUnit(""Metre"", ""Metres"", ""Metre"", ""Identity"")]
[GeneratedUnit(typeof(Length))]
public partial class UnitOfLength { }
";

        VerifyGenerator.VerifyIdentical<UnitGenerator>(CommonResults.Length_OnlyFixedMetre, source);
    }

    [Fact]
    public void PrefixNull()
    {
        string source = @"
using SharpMeasures.Generators;
using SharpMeasures.Generators.Units;

[GeneratedScalarQuantity(typeof(UnitOfLength))]
public class Length { }

[FixedUnit(""Metre"", ""Metres"", 1)]
[PrefixedUnit(""Kilometre"", ""Kilometres"", ""Metre"", null)]
[GeneratedUnit(typeof(Length))]
public partial class UnitOfLength { }
";

        VerifyGenerator.VerifyIdentical<UnitGenerator>(CommonResults.Length_OnlyFixedMetre, source);
    }

    [Fact]
    public void PrefixMissing()
    {
        string source = @"
using SharpMeasures.Generators;
using SharpMeasures.Generators.Units;

[GeneratedScalarQuantity(typeof(UnitOfLength))]
public class Length { }

[FixedUnit(""Metre"", ""Metres"", 1)]
[PrefixedUnit(""Foometre"", ""Foometres"", ""Metre"", ""Foo"")]
[GeneratedUnit(typeof(Length))]
public partial class UnitOfLength { }
";

        VerifyGenerator.VerifyIdentical<UnitGenerator>(CommonResults.Length_OnlyFixedMetre, source);
    }
}
