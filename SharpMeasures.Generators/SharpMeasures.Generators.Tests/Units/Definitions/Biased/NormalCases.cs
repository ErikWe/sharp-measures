namespace SharpMeasures.Generators.Tests.Units.Definitions.Biased;

using SharpMeasures.Generators.Tests.Verify;

using System.Threading.Tasks;

using VerifyXunit;

using Xunit;

[UsesVerify]
public class NormalCases
{
    [Fact]
    public Task Value() => GeneratorVerifier.Construct<SharpMeasuresGenerator>(ValueText).VerifyMatchingSourceNames("UnitOfTemperature.Instances.g.cs");

    [Fact]
    public Task Expression() => GeneratorVerifier.Construct<SharpMeasuresGenerator>(ExpressionText).VerifyMatchingSourceNames("UnitOfTemperature.Instances.g.cs");

    private static string ValueText => """
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        
        [SharpMeasuresScalar(typeof(UnitOfTemperature))]
        public partial class TemperatureDifference { }
        
        [FixedUnitInstance("Kelvin", "Kelvin")]
        [BiasedUnitInstance("Celsius", "Celsius", "Kelvin", -273.15)]
        [SharpMeasuresUnit(typeof(TemperatureDifference), BiasTerm = true)]
        public partial class UnitOfTemperature { }
        """;

    private static string ExpressionText => """
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
            
        [SharpMeasuresScalar(typeof(UnitOfTemperature))]
        public partial class TemperatureDifference { }
            
        [FixedUnitInstance("Kelvin", "Kelvin")]
        [BiasedUnitInstance("Celsius", "Celsius", "Kelvin", "-273.15")]
        [SharpMeasuresUnit(typeof(TemperatureDifference), BiasTerm = true)]
        public partial class UnitOfTemperature { }
        """;
}
