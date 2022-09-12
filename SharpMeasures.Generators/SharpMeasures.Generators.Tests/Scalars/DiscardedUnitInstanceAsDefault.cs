namespace SharpMeasures.Generators.Tests.Scalars;

using SharpMeasures.Generators.Tests.Verify;

using VerifyXunit;

using Xunit;

[UsesVerify]
public class DiscardedUnitInstanceAsDefault
{
    [Fact]
    public void Unbiased() => Assert(UnbiasedText);

    private static GeneratorVerifier Assert(string source) => GeneratorVerifier.Construct<SharpMeasuresGenerator>(source, new GeneratorVerifierSettings(false, false));

    private static string UnbiasedText => """
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Scalars;
        
        [SharpMeasuresScalar(typeof(UnitOfA))]
        public partial class A { }
        
        [SharpMeasuresScalar(typeof(UnitOfB), DefaultUnitInstanceName = "d", DefaultUnitInstanceSymbol = "e")]
        public partial class B { }
        
        [FixedUnitInstance("f", "[*]")]
        [SharpMeasuresUnit(typeof(A))]
        public partial class UnitOfA { }
        
        [DerivedUnitInstance("d", "[*]", new[] { "c" })]
        [DerivableUnit("1 / {0}", typeof(UnitOfA))]
        [SharpMeasuresUnit(typeof(B))]
        public partial class UnitOfB { }
        """;
}
