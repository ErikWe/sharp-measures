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
    public Task IncorrectArguments()
    {
        string source = @"
using SharpMeasures.Generators;
using SharpMeasures.Generators.Units;

[GeneratedScalarQuantity(typeof(UnitOfLength))]
public partial class Length { }

[FixedUnit(""Metre"", 3)]
[GeneratedUnit(typeof(Length))]
public partial class UnitOfLength { }
";

        return VerifyGenerator.FromRawText<UnitGenerator>(source);
    }

    [Fact]
    public Task NullName()
    {
        string source = @"
using SharpMeasures.Generators;
using SharpMeasures.Generators.Units;

[GeneratedScalarQuantity(typeof(UnitOfLength))]
public partial class Length { }

[FixedUnit(null, ""Test"", 3)]
[GeneratedUnit(typeof(Length))]
public partial class UnitOfLength { }
";

        return VerifyGenerator.FromRawText<UnitGenerator>(source);
    }
}
