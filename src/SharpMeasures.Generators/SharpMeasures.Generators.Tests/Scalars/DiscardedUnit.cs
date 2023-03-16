namespace SharpMeasures.Generators.Tests.Scalars;

using SharpMeasures.Generators.Tests.Verify;

using VerifyXunit;

using Xunit;

[UsesVerify]
public class DiscardedUnit
{
    [Fact]
    public void Verify() => GeneratorVerifier.Construct<SharpMeasuresGenerator>(Text, GeneratorVerifierSettings.NoAssertions);

    private static string Text => """
        using SharpMeasures.Generators;
        
        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Distance { }

        public partial class Length { }
        
        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;
}
