namespace SharpMeasures.Generators.Tests.Units.Definitions.Biased;

using SharpMeasures.Generators.Tests.Verify;

using System.Threading.Tasks;

using VerifyXunit;

using Xunit;

[UsesVerify]
public class NormalCases
{
    [Fact]
    public Task Value() => Verify(ValueText);

    [Fact]
    public Task Expression() => Verify(ExpressionText);

    private static Task Verify(string source) => GeneratorVerifier.Construct<SharpMeasuresGenerator>(source).AssertNoDiagnosticsReported().VerifyMatchingSourceNames("UnitOfTemperature.Instances.g.cs");

    private static string ValueText => """
        using SharpMeasures.Generators;
        
        [ScalarQuantity(typeof(UnitOfTemperature))]
        public partial class TemperatureDifference { }
        
        [FixedUnitInstance("Kelvin", "Kelvin")]
        [BiasedUnitInstance("Celsius", "Celsius", "Kelvin", -273.15)]
        [Unit(typeof(TemperatureDifference), BiasTerm = true)]
        public partial class UnitOfTemperature { }
        """;

    private static string ExpressionText => """
        using SharpMeasures.Generators;
            
        [ScalarQuantity(typeof(UnitOfTemperature))]
        public partial class TemperatureDifference { }
            
        [FixedUnitInstance("Kelvin", "Kelvin")]
        [BiasedUnitInstance("Celsius", "Celsius", "Kelvin", "-273.15")]
        [Unit(typeof(TemperatureDifference), BiasTerm = true)]
        public partial class UnitOfTemperature { }
        """;
}
