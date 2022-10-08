namespace SharpMeasures.Generators.Tests.Scalars;

using SharpMeasures.Generators.Tests.Verify;

using VerifyXunit;

using Xunit;

[UsesVerify]
public class DiscardedVector
{
    [Fact]
    public void Verify() => GeneratorVerifier.Construct<SharpMeasuresGenerator>(Text, GeneratorVerifierSettings.NoAssertions);

    private static string Text => """
        using SharpMeasures.Generators;
        
        [ScalarQuantity(typeof(UnitOfLength), Vector = typeof(Displacement3))]
        public partial class Length { }

        public partial class Displacement3 { }
        
        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;
}
