namespace SharpMeasures.Generators.Tests.Units.Definitions.Biased;

using SharpMeasures.Generators.Tests.Verify;

using VerifyXunit;

using Xunit;

[UsesVerify]
public class PluralParsing
{
    [Fact]
    public void CustomShorthand() => AssertIdentical(CustomShorthandText);

    [Fact]
    public void Regex() => AssertIdentical(RegexText);

    private static GeneratorVerifier AssertIdentical(string source) => GeneratorVerifier.Construct<SharpMeasuresGenerator>(source).AssertIdenticalSources(Identical);

    private static string CustomShorthandText => """
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;

        [SharpMeasuresScalar(typeof(UnitOfTemperature))]
        public partial class TemperatureDifference { }

        [BiasedUnitInstance("Celsius", "[*]", "Kelvin", -273.15)]
        [FixedUnitInstance("Kelvin", "Kelvin")]
        [SharpMeasuresUnit(typeof(TemperatureDifference), BiasTerm = true)]
        public partial class UnitOfTemperature { }
        """;

    private static string RegexText => """
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;

        [SharpMeasuresScalar(typeof(UnitOfTemperature))]
        public partial class TemperatureDifference { }
        
        [BiasedUnitInstance("Celsius", ".$", "Kelvin", -273.15, PluralFormRegexSubstitution = "s")]
        [FixedUnitInstance("Kelvin", "Kelvin")]
        [SharpMeasuresUnit(typeof(TemperatureDifference), BiasTerm = true)]
        public partial class UnitOfTemperature { }
        """;

    private static GeneratorVerifier Identical => GeneratorVerifier.Construct<SharpMeasuresGenerator>(IdenticalText);

    private static string IdenticalText => """
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        
        [SharpMeasuresScalar(typeof(UnitOfTemperature))]
        public partial class TemperatureDifference { }
        
        [BiasedUnitInstance("Celsius", "Celsius", "Kelvin", -273.15)]
        [FixedUnitInstance("Kelvin", "Kelvin")]
        [SharpMeasuresUnit(typeof(TemperatureDifference), BiasTerm = true)]
        public partial class UnitOfTemperature { }
        """;
}
