namespace SharpMeasures.Generators.Tests.Units.Definitions.Biased;

using SharpMeasures.Generators.Tests.Verify;

using System.Threading.Tasks;

using VerifyXunit;

using Xunit;

[UsesVerify]
public class VerifyGeneratedCode
{
    [Fact]
    public Task SimpleBiasedUnit()
    {
        var source = """
            using SharpMeasures.Generators.Scalars;
            using SharpMeasures.Generators.Units;
            
            [SharpMeasuresScalar(typeof(UnitOfTemperature))]
            public partial class TemperatureDifference { }
            
            [FixedUnit("Kelvin", "Kelvin")]
            [BiasedUnit("Celsius", "Celsius", "Kelvin", -273.15)]
            [SharpMeasuresUnit(typeof(TemperatureDifference), BiasTerm = true)]
            public partial class UnitOfTemperature { }
            """;

        return GeneratorVerifier.Construct<SharpMeasuresGenerator>(source).VerifyMatchingSourceNames("UnitOfTemperature_Definitions.g.cs");
    }
}
