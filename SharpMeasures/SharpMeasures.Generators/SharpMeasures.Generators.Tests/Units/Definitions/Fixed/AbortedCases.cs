namespace SharpMeasures.Generators.Tests.Units.Definitions.Fixed;

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
using SharpMeasures.Generators.Scalars;
using SharpMeasures.Generators.Units;

[GeneratedScalarQuantity(typeof(UnitOfLength))]
public class Length { }

[FixedUnit(""Metre"", 1)]
[GeneratedUnit(typeof(Length))]
public partial class UnitOfLength { }
";

        VerifyGenerator.VerifyIdentical<UnitGenerator>(CommonResults.Length_NoDefinitions, source);
    }

    [Fact]
    public void NameNull()
    {
        string source = @"
using SharpMeasures.Generators.Scalars;
using SharpMeasures.Generators.Units;

[GeneratedScalarQuantity(typeof(UnitOfLength))]
public class Length { }

[FixedUnit(null, ""Metres"", 1)]
[GeneratedUnit(typeof(Length))]
public partial class UnitOfLength { }
";

        VerifyGenerator.VerifyIdentical<UnitGenerator>(CommonResults.Length_NoDefinitions, source);
    }

    [Fact]
    public void PluralNullAndNotConstant()
    {
        string source = @"
using SharpMeasures.Generators.Scalars;
using SharpMeasures.Generators.Units;

[GeneratedScalarQuantity(typeof(UnitOfLength))]
public class Length { }

[FixedUnit(""Metre"", null, 1)]
[GeneratedUnit(typeof(Length))]
public partial class UnitOfLength { }
";

        VerifyGenerator.VerifyIdentical<UnitGenerator>(CommonResults.Length_NoDefinitions, source);
    }

    [Fact]
    public void ValueNull()
    {
        string source = @"
using SharpMeasures.Generators.Scalars;
using SharpMeasures.Generators.Units;

[GeneratedScalarQuantity(typeof(UnitOfLength))]
public class Length { }

[FixedUnit(""Metre"", ""Metres"", null)]
[GeneratedUnit(typeof(Length))]
public partial class UnitOfLength { }
";

        VerifyGenerator.VerifyIdentical<UnitGenerator>(CommonResults.Length_NoDefinitions, source);
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
[FixedUnit(""Metre"", ""Metres"", 1)]
[GeneratedUnit(typeof(Length))]
public partial class UnitOfLength { }
";

        VerifyGenerator.VerifyIdentical<UnitGenerator>(CommonResults.Length_OnlyFixedMetre, source);
    }
}
