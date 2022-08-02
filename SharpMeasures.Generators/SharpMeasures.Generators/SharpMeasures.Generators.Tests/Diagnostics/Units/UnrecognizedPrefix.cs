namespace SharpMeasures.Generators.Tests.Diagnostics.Units;

using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Tests.Verify;

using System.Collections.Generic;
using System.Threading.Tasks;

using VerifyXunit;

using Xunit;

[UsesVerify]
public class UnrecognizedPrefix
{
    [Fact]
    public Task VerifyUnrecognizedPrefixDiagnosticsMessage_Metric()
    {
        string source = SourceText("(MetricPrefixName)(-1)");

        return AssertExactlyUnrecognizedPrefixDiagnosticsWithValidLocation(source).VerifyDiagnostics();
    }

    [Fact]
    public Task VerifyUnrecognizedPrefixDiagnosticsMessage_Binary()
    {
        string source = SourceText("(BinaryPrefixName)(-1)");

        return AssertExactlyUnrecognizedPrefixDiagnosticsWithValidLocation(source).VerifyDiagnostics();
    }

    [Theory]
    [InlineData("(MetricPrefixName)(-1)")]
    [InlineData("(MetricPrefixName)int.MaxValue")]
    [InlineData("(BinaryPrefixName)(-1)")]
    [InlineData("(BinaryPrefixName)int.MaxValue")]
    public void ExactList(string value)
    {
        string source = SourceText(value);

        AssertExactlyUnrecognizedPrefixDiagnosticsWithValidLocation(source);
    }

    private static string SourceText(string expression) => $$"""
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Units.Utility;

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }
            
        [FixedUnit("Metre", "Metres", 1)]
        [PrefixedUnit("Kilometre", "Kilometres", "Metre", {{expression}})]
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertExactlyUnrecognizedPrefixDiagnosticsWithValidLocation(string source) => GeneratorVerifier.Construct<SharpMeasuresGenerator>(source).AssertExactlyListedDiagnosticsIDsReported(UnrecognizedPrefixDiagnostics).AssertAllDiagnosticsValidLocation();
    private static IReadOnlyCollection<string> UnrecognizedPrefixDiagnostics { get; } = new string[] { DiagnosticIDs.UnrecognizedPrefix };
}
