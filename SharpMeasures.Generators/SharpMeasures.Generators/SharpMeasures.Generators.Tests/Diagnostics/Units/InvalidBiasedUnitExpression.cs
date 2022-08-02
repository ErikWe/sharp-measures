namespace SharpMeasures.Generators.Tests.Diagnostics.Units;

using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Tests.Verify;

using System.Collections.Generic;
using System.Threading.Tasks;

using VerifyXunit;

using Xunit;

[UsesVerify]
public class InvalidBiasedUnitExpression
{
    [Fact]
    public Task Null_ExactListAndVerify()
    {
        string source = SourceTexts.Biased(bias: "null");

        return AssertExactlyBiasedUnitExpressionDiagnosticsWithValidLocation(source).VerifyDiagnostics();
    }

    [Fact]
    public Task Empty_ExactListAndVerify()
    {
        string source = SourceTexts.Biased(bias: "\"\"");

        return AssertExactlyBiasedUnitExpressionDiagnosticsWithValidLocation(source).VerifyDiagnostics();
    }

    private static GeneratorVerifier AssertExactlyBiasedUnitExpressionDiagnosticsWithValidLocation(string source) => GeneratorVerifier.Construct<SharpMeasuresGenerator>(source).AssertExactlyListedDiagnosticsIDsReported(InvalidBiasedUnitExpressionDiagnostics).AssertAllDiagnosticsValidLocation();
    private static IReadOnlyCollection<string> InvalidBiasedUnitExpressionDiagnostics { get; } = new string[] { DiagnosticIDs.InvalidBiasedUnitExpression };
}
