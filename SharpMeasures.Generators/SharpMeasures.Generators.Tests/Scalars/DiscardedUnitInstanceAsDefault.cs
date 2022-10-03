namespace SharpMeasures.Generators.Tests.Scalars;

using SharpMeasures.Generators.Tests.Verify;

using VerifyXunit;

using Xunit;

[UsesVerify]
public class DiscardedUnitInstanceAsDefault
{
    [Fact]
    public void Unbiased() => Assert(UnbiasedText);

    private static GeneratorVerifier Assert(string source) => GeneratorVerifier.Construct<SharpMeasuresGenerator>(source, GeneratorVerifierSettings.NoAssertions);

    private static string UnbiasedText => """
        using SharpMeasures.Generators;
        
        [ScalarQuantity(typeof(UnitOfA))]
        public partial class A { }
        
        [ScalarQuantity(typeof(UnitOfB), DefaultUnitInstanceName = "d", DefaultUnitInstanceSymbol = "e")]
        public partial class B { }
        
        [FixedUnitInstance("f", "[*]")]
        [Unit(typeof(A))]
        public partial class UnitOfA { }
        
        [DerivedUnitInstance("d", "[*]", new[] { "c" })]
        [DerivableUnit("1 / {0}", typeof(UnitOfA))]
        [Unit(typeof(B))]
        public partial class UnitOfB { }
        """;
}
