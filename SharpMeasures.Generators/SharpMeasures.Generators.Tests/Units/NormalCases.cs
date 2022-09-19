namespace SharpMeasures.Generators.Tests.Units;

using SharpMeasures.Generators.Tests.Verify;

using System.Threading.Tasks;

using VerifyXunit;

using Xunit;

[UsesVerify]
public class NormalCases
{
    [Fact]
    public Task UnbiasedUnit() => GeneratorVerifier.Construct<SharpMeasuresGenerator>(UnbiasedUnitText).AssertNoDiagnosticsReported().VerifyMatchingSourceNames(@"UnitOfLength.\S+\.g\.cs");

    [Fact]
    public Task BiasedUnit() => GeneratorVerifier.Construct<SharpMeasuresGenerator>(BiasedUnitText).AssertNoDiagnosticsReported().VerifyMatchingSourceNames(@"UnitOfTemperature.\S+\.g\.cs");

    private static string UnbiasedUnitText => $$"""
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        
        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }
        
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static string BiasedUnitText => """
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        
        [SharpMeasuresScalar(typeof(UnitOfTemperature))]
        public partial class TemperatureDifference { }
        
        [SharpMeasuresUnit(typeof(TemperatureDifference), BiasTerm = true)]
        public partial class UnitOfTemperature { }
        """;
}
