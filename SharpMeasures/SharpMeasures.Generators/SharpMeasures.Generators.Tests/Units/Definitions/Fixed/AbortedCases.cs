namespace SharpMeasures.Generators.Tests.Units.Definitions.Fixed;

using SharpMeasures.Generators.Units;
using SharpMeasures.Generators.Tests.Utility;

using System.Threading.Tasks;

using VerifyXunit;

using Xunit;

[UsesVerify]
public class AbortedCases
{
    [Fact]
    public Task NoMatchingConstructor()
    {
        string source = @"
using SharpMeasures.Generators;
using SharpMeasures.Generators.Units;

[GeneratedScalarQuantity(typeof(UnitOfLength))]
public partial class Length { }

[FixedUnit(""Metre"", 1)]
[GeneratedUnit(typeof(Length))]
public partial class UnitOfLength { }
";

        return VerifyGenerator.VerifyMatch<UnitGenerator>(source);
    }

    [Fact]
    public Task NameNull()
    {
        string source = @"
using SharpMeasures.Generators;
using SharpMeasures.Generators.Units;

[GeneratedScalarQuantity(typeof(UnitOfLength))]
public partial class Length { }

[FixedUnit(null, ""Metres"", 1)]
[GeneratedUnit(typeof(Length))]
public partial class UnitOfLength { }
";

        return VerifyGenerator.VerifyMatch<UnitGenerator>(source);
    }

    [Fact]
    public Task ValueNull()
    {
        string source = @"
using SharpMeasures.Generators;
using SharpMeasures.Generators.Units;

[GeneratedScalarQuantity(typeof(UnitOfLength))]
public partial class Length { }

[FixedUnit(""Metre"", ""Metres"", null)]
[GeneratedUnit(typeof(Length))]
public partial class UnitOfLength { }
";

        return VerifyGenerator.VerifyMatch<UnitGenerator>(source);
    }

    [Fact]
    public Task DuplicateName()
    {
        string source = @"
using SharpMeasures.Generators;
using SharpMeasures.Generators.Units;

[GeneratedScalarQuantity(typeof(UnitOfLength))]
public partial class Length { }

[FixedUnit(""Metre"", ""Metres"", 1)]
[FixedUnit(""Metre"", ""Metres"", 1)]
[GeneratedUnit(typeof(Length))]
public partial class UnitOfLength { }
";

        return VerifyGenerator.VerifyMatch<UnitGenerator>(source);
    }
}
