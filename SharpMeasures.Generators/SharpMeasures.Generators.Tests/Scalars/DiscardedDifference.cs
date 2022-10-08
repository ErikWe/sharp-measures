namespace SharpMeasures.Generators.Tests.Scalars;

using SharpMeasures.Generators.Tests.Verify;

using VerifyXunit;

using Xunit;

[UsesVerify]
public class DiscardedDifference
{
    [Fact]
    public void Verify() => GeneratorVerifier.Construct<SharpMeasuresGenerator>(Text, GeneratorVerifierSettings.NoAssertions);

    private static string Text => """
        using SharpMeasures.Generators;
        
        [ScalarQuantity(typeof(UnitOfLength), Difference = typeof(Distance))]
        public partial class Length { }

        [ScalarQuantity(typeof(UnitOfDistance))]
        public partial class Distance { }

        public partial class UnitOfDistance { }
        
        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;
}
