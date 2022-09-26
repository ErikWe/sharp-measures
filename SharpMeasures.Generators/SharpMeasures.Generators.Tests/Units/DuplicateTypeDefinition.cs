namespace SharpMeasures.Generators.Tests.Units;

using SharpMeasures.Generators.Tests.Verify;

using VerifyXunit;

using Xunit;

[UsesVerify]
public class DuplicateTypeDefinition
{
    [Fact]
    public void AssertNoException() => GeneratorVerifier.Construct<SharpMeasuresGenerator>(Text, GeneratorVerifierSettings.NoAssertions).AssertNoDiagnosticsReported();

    private static string Text => $$"""
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        
        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }
        
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }

        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;
}
