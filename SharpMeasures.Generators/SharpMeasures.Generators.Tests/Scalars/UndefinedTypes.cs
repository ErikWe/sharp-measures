namespace SharpMeasures.Generators.Tests.Scalars;

using SharpMeasures.Generators.Tests.Verify;

using VerifyXunit;

using Xunit;

[UsesVerify]
public class UndefinedTypes
{
    [Fact]
    public void Unit() => GeneratorVerifier.Construct<SharpMeasuresGenerator>(UnitText, GeneratorVerifierSettings.GeneratedCodeAssertions).AssertNoDiagnosticsReported();

    private static string UnitText => """
        using SharpMeasures.Generators;
        
        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }
        """;
}
