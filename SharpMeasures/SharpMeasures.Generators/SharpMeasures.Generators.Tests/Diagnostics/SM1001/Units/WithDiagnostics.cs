namespace SharpMeasures.Generators.Tests.Diagnostics.SM1001.Units;

using SharpMeasures.Generators.Tests.Utility;
using SharpMeasures.Generators.Units;

using System.Threading.Tasks;

using VerifyXunit;

using Xunit;

[UsesVerify]
public class WithDiagnostics
{
    [Fact]
    public Task GeneratedUnitQuantity()
    {
        string source = @"
using SharpMeasures.Generators;

public partial class Length { }

[GeneratedUnit(typeof(Length))]
public partial class UnitOfLength { }
";

        return VerifyGeneratorDiagnostics.VerifyMatch<UnitGenerator>(source);
    }
}
