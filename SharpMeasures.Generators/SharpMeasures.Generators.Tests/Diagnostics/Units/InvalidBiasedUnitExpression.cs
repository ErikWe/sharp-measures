namespace SharpMeasures.Generators.Tests.Diagnostics.Units;

using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Tests.Utility;
using SharpMeasures.Generators.Tests.Verify;

using System.Collections.Generic;
using System.Threading.Tasks;

using VerifyXunit;

using Xunit;

[UsesVerify]
public class InvalidBiasedUnitExpression
{
    [Fact]
    public Task VerifyInvalidBiasedUnitExpressionDiagnosticsMessage_Null() => Assert(NullExpression).VerifyDiagnostics();

    [Fact]
    public Task VerifyInvalidBiasedUnitExpressionDiagnosticsMessage_Empty() => Assert(EmptyExpression).VerifyDiagnostics();

    private static SourceSubtext NullExpression { get; } = SourceSubtext.Covered("null");
    private static SourceSubtext EmptyExpression { get; } = SourceSubtext.Covered("\"\"");

    private static GeneratorVerifier AssertExactlyInvalidBiasedUnitExpressionDiagnostics(string source) => GeneratorVerifier.Construct<SharpMeasuresGenerator>(source).AssertExactlyListedDiagnosticsIDsReported(InvalidBiasedUnitExpressionDiagnostics);
    private static IReadOnlyCollection<string> InvalidBiasedUnitExpressionDiagnostics { get; } = new string[] { DiagnosticIDs.InvalidBiasedUnitExpression };

    private static GeneratorVerifier Assert(SourceSubtext expression)
    {
        var source = SourceTexts.Biased(bias: expression.ToString());
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(source, expression.Context.With(outerPrefix: "[BiasedUnit(\"Celsius\", \"Celsius\", \"Kelvin\", "));

        return AssertExactlyInvalidBiasedUnitExpressionDiagnostics(source).AssertDiagnosticsLocation(expectedLocation, source);
    }
}
