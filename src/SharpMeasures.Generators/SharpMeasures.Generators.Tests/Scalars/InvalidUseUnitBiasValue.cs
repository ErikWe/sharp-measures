namespace SharpMeasures.Generators.Tests.Scalars;

using SharpMeasures.Generators.Tests.Verify;

using VerifyXunit;

using Xunit;

[UsesVerify]
public class InvalidUseUnitBiasValue
{
    [Fact]
    public void Falsee() => AssertIdentical(FalseeText);

    private static GeneratorVerifier AssertIdentical(string source) => GeneratorVerifier.Construct<SharpMeasuresGenerator>(source, GeneratorVerifierSettings.GeneratedCodeAssertions).AssertIdenticalSources(Identical);

    private static string FalseeText => """
        using SharpMeasures.Generators;
        
        [ScalarQuantity(typeof(UnitOfLength), UseUnitBias = falsee, DefaultUnit = "Metre", DefaultSymbol = "m")]
        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }
        
        [FixedUnitInstance("Metre", "Metres")]
        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier Identical { get; } = GeneratorVerifier.Construct<SharpMeasuresGenerator>(IdenticalText);

    private static string IdenticalText => """
        using SharpMeasures.Generators;
        
        [ScalarQuantity(typeof(UnitOfLength), DefaultUnit = "Metre", DefaultSymbol = "m")]
        public partial class Length { }
        
        [FixedUnitInstance("Metre", "Metres")]
        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;
}
