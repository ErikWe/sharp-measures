namespace SharpMeasures.Generators.Tests.Diagnostics.Units;

using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Tests.Verify;

using System.Collections.Generic;
using System.Threading.Tasks;

using VerifyXunit;

using Xunit;

[UsesVerify]
public class InvalidScaledUnitExpression
{
    [Fact]
    public Task Null_ExactListAndVerify()
    {
        string source = SourceTexts.Scaled(value: "null");

        return AssertExactlyScaledUnitExpressionDiagnostics(source).VerifyDiagnostics();
    }

    [Fact]
    public Task Empty_ExactListAndVerify()
    {
        string source = SourceTexts.Scaled(value: "\"\"");

        return AssertExactlyScaledUnitExpressionDiagnostics(source).VerifyDiagnostics();
    }

    private static GeneratorVerifier AssertExactlyScaledUnitExpressionDiagnostics(string source)
        => GeneratorVerifier.Construct<SharpMeasuresGenerator>(source).AssertExactlyListedDiagnosticsIDsReported(InvalidScaledUnitExpressionDiagnostics);

    private static IReadOnlyCollection<string> InvalidScaledUnitExpressionDiagnostics { get; } = new string[] { DiagnosticIDs.InvalidScaledUnitExpression };
}
