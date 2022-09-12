namespace SharpMeasures.Generators.Tests.Units.Definitions.Derived;

using SharpMeasures.Generators.Tests.Verify;

using VerifyXunit;

using Xunit;

[UsesVerify]
public class DiscardedDerivation
{
    [Fact]
    public void RunTest() => Assert(Text);

    private static GeneratorVerifier Assert(string source) => GeneratorVerifier.Construct<SharpMeasuresGenerator>(source, new GeneratorVerifierSettings(false, false));

    private static string Text => """
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Scalars;
        
        public partial class A { }
        
        [SharpMeasuresScalar(typeof(UnitOfB))]
        public partial class B { }
        
        [FixedUnitInstance("Uh", "Uhs")]
        [SharpMeasuresUnit(typeof(A))]
        public partial class UnitOfA { }
        
        [DerivedUnitInstance("Ah", "Ahs", new[] { "Uh" })]
        [DerivableUnit("1 / {0}", typeof(UnitOfA))]
        [SharpMeasuresUnit(typeof(B))]
        public partial class UnitOfB { }
        """;
}
