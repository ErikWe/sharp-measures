namespace SharpMeasures.Generators.Tests.Units;

using SharpMeasures.Generators.Tests.Verify;

using System.Text.RegularExpressions;
using System.Threading.Tasks;

using VerifyXunit;

using Xunit;

[UsesVerify]
public class NormalCases
{
    [Fact]
    public Task UnbiasedUnit() => GeneratorVerifier.Construct<SharpMeasuresGenerator>(UnbiasedUnitText).AssertNoDiagnosticsReported().VerifyMatchingSourceNames(new Regex(@"UnitOfLength.\S+\.g\.cs"));

    [Fact]
    public Task BiasedUnit() => GeneratorVerifier.Construct<SharpMeasuresGenerator>(BiasedUnitText).AssertNoDiagnosticsReported().VerifyMatchingSourceNames(new Regex(@"UnitOfTemperature.\S+\.g\.cs"));

    private static string UnbiasedUnitText => $$"""
        using SharpMeasures.Generators;
        
        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }
        
        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static string BiasedUnitText => """
        using SharpMeasures.Generators;
        
        [ScalarQuantity(typeof(UnitOfTemperature))]
        public partial class TemperatureDifference { }
        
        [Unit(typeof(TemperatureDifference), BiasTerm = true)]
        public partial class UnitOfTemperature { }
        """;
}
