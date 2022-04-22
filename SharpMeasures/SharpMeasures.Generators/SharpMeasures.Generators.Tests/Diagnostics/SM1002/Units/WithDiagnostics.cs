namespace SharpMeasures.Generators.Tests.Diagnostics.SM1002.Units;

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

[GeneratedScalarQuantity(typeof(UnitOfTemperature), Biased = true)]
public partial class Temperature { }

[GeneratedUnit(typeof(Temperature))]
public partial class UnitOfTemperature { }
";

        return VerifyGeneratorDiagnostics.VerifyMatch<UnitGenerator>(source);
    }
}
