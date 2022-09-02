namespace SharpMeasures.Generators.Tests.Units.Definitions.Biased;

using SharpMeasures.Generators.Tests.Verify;

using System.Threading.Tasks;

using VerifyXunit;

using Xunit;

[UsesVerify]
public class NormalCases
{
    [Fact]
    public Task SimpleBiasedUnit()
    {
        var source = """
            using SharpMeasures.Generators.Scalars;
            using SharpMeasures.Generators.Units;
            
            [SharpMeasuresScalar(typeof(UnitOfTemperature))]
            public partial class TemperatureDifference { }
            
            [FixedUnitInstance("Kelvin", "Kelvin")]
            [BiasedUnitInstance("Celsius", "Celsius", "Kelvin", -273.15)]
            [SharpMeasuresUnit(typeof(TemperatureDifference), BiasTerm = true)]
            public partial class UnitOfTemperature { }
            """;

        return GeneratorVerifier.Construct<SharpMeasuresGenerator>(source).VerifyMatchingSourceNames("UnitOfTemperature_Instances.g.cs");
    }
}
