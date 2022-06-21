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
    private static string Text(string expression) => $$"""
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

    [Fact]
    public Task Metric_Negative_ExactListAndVerify()
    {
        string source = Text("(MetricPrefixName)(-1)");

        return AssertExactlyUnrecognizedPrefixDiagnostics(source).VerifyDiagnostics();
    }

    [Fact]
    public Task Metric_TooLarge_ExactListAndVerify()
    {
        string source = Text("(MetricPrefixName)int.MaxValue");

        return AssertExactlyUnrecognizedPrefixDiagnostics(source).VerifyDiagnostics();
    }

    [Fact]
    public Task Binary_Negative_ExactListAndVerify()
    {
        string source = Text("(BinaryPrefixName)(-1)");

        return AssertExactlyUnrecognizedPrefixDiagnostics(source).VerifyDiagnostics();
    }

    [Fact]
    public Task Binary_TooLarge_ExactListAndVerify()
    {
        string source = Text("(BinaryPrefixName)int.MaxValue");

        return AssertExactlyUnrecognizedPrefixDiagnostics(source).VerifyDiagnostics();
    }

    private static GeneratorVerifier AssertExactlyUnrecognizedPrefixDiagnostics(string source)
        => GeneratorVerifier.Construct<SharpMeasuresGenerator>(source).AssertExactlyListedDiagnosticsIDsReported(UnrecognizedPrefixDiagnostics);

    private static IReadOnlyCollection<string> UnrecognizedPrefixDiagnostics { get; } = new string[] { DiagnosticIDs.UnrecognizedPrefix };
}
