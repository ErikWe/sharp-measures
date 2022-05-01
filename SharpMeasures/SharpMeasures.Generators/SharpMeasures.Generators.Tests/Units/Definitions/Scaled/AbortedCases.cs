namespace SharpMeasures.Generators.Tests.Units.Definitions.Scaled;

using SharpMeasures.Generators.Units;
using SharpMeasures.Generators.Tests.Verify;

using System.Threading.Tasks;

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
[ScaledUnit(""Foot"", ""Feet"", ""Metre"")]
[GeneratedUnit(typeof(Length))]
public partial class UnitOfLength { }
";

        VerifyGenerator.VerifyIdentical<UnitGenerator>(CommonResults.Length_OnlyFixedMetre, source);
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
[ScaledUnit(""Foot"", ""Feet"", ""Invalid"", 0.3048)]
[GeneratedUnit(typeof(Length))]
public partial class UnitOfLength { }
";

        VerifyGenerator.VerifyIdentical<UnitGenerator>(CommonResults.Length_OnlyFixedMetre, source);
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
[ScaledUnit(null, ""Feet"", ""Metre"", 0.3048)]
[GeneratedUnit(typeof(Length))]
public partial class UnitOfLength { }
";

        VerifyGenerator.VerifyIdentical<UnitGenerator>(CommonResults.Length_OnlyFixedMetre, source);
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
[ScaledUnit(""Kilometre"", null, ""Metre"", 0.3048)]
[GeneratedUnit(typeof(Length))]
public partial class UnitOfLength { }
";

        VerifyGenerator.VerifyIdentical<UnitGenerator>(CommonResults.Length_OnlyFixedMetre, source);
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
[ScaledUnit(""Kilometre"", ""Kilometres"", null, 0.3048)]
[GeneratedUnit(typeof(Length))]
public partial class UnitOfLength { }
";

        VerifyGenerator.VerifyIdentical<UnitGenerator>(CommonResults.Length_OnlyFixedMetre, source);
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
[ScaledUnit(""Metre"", ""Metres"", ""Metre"", 1)]
[GeneratedUnit(typeof(Length))]
public partial class UnitOfLength { }
";

        VerifyGenerator.VerifyIdentical<UnitGenerator>(CommonResults.Length_OnlyFixedMetre, source);
    }

    [Fact]
    public void ScaleNull()
    {
        string source = @"
using SharpMeasures.Generators.Scalars;
using SharpMeasures.Generators.Units;

[GeneratedScalarQuantity(typeof(UnitOfLength))]
public class Length { }

[FixedUnit(""Metre"", ""Metres"", 1)]
[ScaledUnit(""Foot"", ""Feet"", ""Metre"", null)]
[GeneratedUnit(typeof(Length))]
public partial class UnitOfLength { }
";

        VerifyGenerator.VerifyIdentical<UnitGenerator>(CommonResults.Length_OnlyFixedMetre, source);
    }
}
