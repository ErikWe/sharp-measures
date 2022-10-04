namespace SharpMeasures.Generators.Tests.SpecializedScalars;

using SharpMeasures.Generators.Tests.Verify;

using VerifyXunit;

using Xunit;

[UsesVerify]
public class DiscardedOriginalScalars
{
    [Fact]
    public void Assert() => GeneratorVerifier.Construct<SharpMeasuresGenerator>(Text).AssertIdenticalSources<SharpMeasuresGenerator>(IdenticalText);

    private static string Text => """
        using SharpMeasures.Generators;
        
        [SpecializedScalarQuantity(typeof(Length))]
        public partial class Distance { }

        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }

        public class UnitOfLength { }
        """;

    private static string IdenticalText => string.Empty;
}
