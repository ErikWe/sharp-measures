namespace SharpMeasures.Generators.Tests.Diagnostics.Units;

using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Tests.Utility;
using SharpMeasures.Generators.Tests.Verify;

using System.Collections.Generic;
using System.Threading.Tasks;

using VerifyXunit;

using Xunit;

[UsesVerify]
public class BiasedUnitDefinedButUnitNotBiased
{
    [Fact]
    public Task VerifyBiasedUnitDefinedButUnitNotBiasedDiagnosticsMessage() => Assert().VerifyDiagnostics();

    private static GeneratorVerifier AssertExactlyBiasedUnitDefinedButUnitNotBiasedDiagnostics(string source) => GeneratorVerifier.Construct<SharpMeasuresGenerator>(source, GeneratorVerifierSettings.TestCodeAssertions).AssertExactlyListedDiagnosticsIDsReported(BiasedUnitDefinedButUnitNotBiasedDiagnostics);
    private static IReadOnlyCollection<string> BiasedUnitDefinedButUnitNotBiasedDiagnostics { get; } = new string[] { DiagnosticIDs.BiasedUnitDefinedButUnitNotBiased };

    private static string Text => """
        using SharpMeasures.Generators;

        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }

        [FixedUnitInstance("Metre", "Metres")]
        [BiasedUnitInstance("Metre2", "Metres2", "Metre", 1)]
        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier Assert()
    {
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(Text, target: "BiasedUnitInstance", postfix: "(\"Metre2\", \"Metres2\", \"Metre\", 1)]");

        return AssertExactlyBiasedUnitDefinedButUnitNotBiasedDiagnostics(Text).AssertDiagnosticsLocation(expectedLocation);
    }
}
