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
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        
        [SpecializedSharpMeasuresScalar(typeof(Length))]
        public partial class Distance { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }

        public class UnitOfLength { }
        """;

    private static string IdenticalText => string.Empty;
}
