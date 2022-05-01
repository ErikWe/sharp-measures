namespace SharpMeasures.Generators.Tests.Diagnostics.SM1002;

using SharpMeasures.Generators.Tests.Utility;
using SharpMeasures.Generators.Units;

using System.Threading.Tasks;

using VerifyXunit;

using Xunit;

[UsesVerify]
public class UnitsWithDiagnostics
{
    [Fact]
    public Task BiasedQuantity()
    {
        string source = @"
using SharpMeasures.Generators.Scalars;
using SharpMeasures.Generators.Units;

[GeneratedScalarQuantity(typeof(UnitOfTemperature), Biased = true)]
public partial class Temperature { }

[GeneratedUnit(typeof(Temperature))]
public partial class UnitOfTemperature { }
";

        return VerifyGeneratorDiagnostics.VerifyMatch<UnitGenerator>(source);
    }
}
