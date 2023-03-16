namespace SharpMeasures.Generators.Tests.Units.Definitions.Derived;

using SharpMeasures.Generators.Tests.Verify;

using VerifyXunit;

using Xunit;

[UsesVerify]
public class DiscardedDerivation
{
    [Fact]
    public void RunTest() => Assert(Text);

    private static GeneratorVerifier Assert(string source) => GeneratorVerifier.Construct<SharpMeasuresGenerator>(source, GeneratorVerifierSettings.NoAssertions);

    private static string Text => """
        using SharpMeasures.Generators;
        
        public partial class A { }
        
        [ScalarQuantity(typeof(UnitOfB))]
        public partial class B { }
        
        [FixedUnitInstance("Uh", "Uhs")]
        [Unit(typeof(A))]
        public partial class UnitOfA { }
        
        [DerivedUnitInstance("Ah", "Ahs", new[] { "Uh" })]
        [DerivableUnit("1 / {0}", typeof(UnitOfA))]
        [Unit(typeof(B))]
        public partial class UnitOfB { }
        """;
}
