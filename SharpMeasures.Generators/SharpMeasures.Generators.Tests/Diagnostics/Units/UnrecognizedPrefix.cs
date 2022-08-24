namespace SharpMeasures.Generators.Tests.Diagnostics.Units;

using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Tests.Utility;
using SharpMeasures.Generators.Tests.Verify;

using System.Collections.Generic;
using System.Threading.Tasks;

using VerifyXunit;

using Xunit;

[UsesVerify]
public class UnrecognizedPrefix
{
    [Fact]
    public Task VerifyUnrecognizedPrefixDiagnosticsMessage_Metric() => AssertPrefixedUnit(NegativeMetricPrefix).VerifyDiagnostics();

    [Fact]
    public Task VerifyUnrecognizedPrefixDiagnosticsMessage_Binary() => AssertPrefixedUnit(NegativeBinaryPrefix).VerifyDiagnostics();

    [Theory]
    [MemberData(nameof(UnrecognizedPrefixes))]
    public void PrefixedUnit(SourceSubtext prefix) => AssertPrefixedUnit(prefix);

    public static IEnumerable<object[]> UnrecognizedPrefixes => new object[][]
    {
        new object[] { NegativeMetricPrefix },
        new object[] { TooLargeMetricPrefix },
        new object[] { NegativeBinaryPrefix },
        new object[] { TooLargeBinaryPrefix }
    };

    private static SourceSubtext NegativeMetricPrefix { get; } = SourceSubtext.Covered("-1", prefix: "(MetricPrefixName)(", postfix: ")");
    private static SourceSubtext TooLargeMetricPrefix { get; } = SourceSubtext.Covered("int.MaxValue", prefix: "(MetricPrefixName)");
    private static SourceSubtext NegativeBinaryPrefix { get; } = SourceSubtext.Covered("-1", prefix: "(BinaryPrefixName)(", postfix: ")");
    private static SourceSubtext TooLargeBinaryPrefix { get; } = SourceSubtext.Covered("int.MaxValue", prefix: "(BinaryPrefixName)");

    private static GeneratorVerifier AssertExactlyUnrecognizedPrefixDiagnostics(string source) => GeneratorVerifier.Construct<SharpMeasuresGenerator>(source).AssertExactlyListedDiagnosticsIDsReported(UnrecognizedPrefixDiagnostics);
    private static IReadOnlyCollection<string> UnrecognizedPrefixDiagnostics { get; } = new string[] { DiagnosticIDs.UnrecognizedPrefix };

    private static string PrefixedUnitText(SourceSubtext prefix) => $$"""
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Units.Utility;

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }
            
        [FixedUnit("Metre", "Metres", 1)]
        [PrefixedUnit("Kilometre", "Kilometres", "Metre", {{prefix}})]
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertPrefixedUnit(SourceSubtext prefix)
    {
        var source = PrefixedUnitText(prefix);
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(source, prefix.Context.With(outerPrefix: "PrefixedUnit(\"Kilometre\", \"Kilometres\", \"Metre\", "));

        return AssertExactlyUnrecognizedPrefixDiagnostics(source).AssertDiagnosticsLocation(expectedLocation, source);
    }
}
