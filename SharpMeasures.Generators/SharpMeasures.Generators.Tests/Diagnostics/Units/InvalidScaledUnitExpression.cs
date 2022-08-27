namespace SharpMeasures.Generators.Tests.Diagnostics.Units;

using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Tests.Utility;
using SharpMeasures.Generators.Tests.Verify;

using System.Collections.Generic;
using System.Threading.Tasks;

using VerifyXunit;

using Xunit;

[UsesVerify]
public class InvalidScaledUnitExpression
{
    [Fact]
    public Task VerifyInvalidScaledUnitExpressionDiagnosticsMessage_Null() => Assert(NullExpression).VerifyDiagnostics();

    [Fact]
    public Task VerifyInvalidScaledUnitExpressionDiagnosticsMessage_Empty() => Assert(EmptyExpression).VerifyDiagnostics();

    private static SourceSubtext NullExpression { get; } = SourceSubtext.Covered("null");
    private static SourceSubtext EmptyExpression { get; } = SourceSubtext.Covered("\"\"");

    private static GeneratorVerifier AssertExactlyInvalidScaledUnitExpressionDiagnostics(string source) => GeneratorVerifier.Construct<SharpMeasuresGenerator>(source).AssertExactlyListedDiagnosticsIDsReported(InvalidScaledUnitExpressionDiagnostics);
    private static IReadOnlyCollection<string> InvalidScaledUnitExpressionDiagnostics { get; } = new string[] { DiagnosticIDs.InvalidScaledUnitExpression };

    private static GeneratorVerifier Assert(SourceSubtext expression)
    {
        var source = SourceTexts.Scaled(value: expression.ToString());
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(source, expression.Context.With(outerPrefix: "[ScaledUnit(\"Kilometre\", \"Kilometres\", \"Metre\", "));

        return AssertExactlyInvalidScaledUnitExpressionDiagnostics(source).AssertDiagnosticsLocation(expectedLocation, source);
    }
}
