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
        string source = Common.Biased(bias: "null");

        return AssertExactlyBiasedUnitExpressionDiagnostics(source).VerifyDiagnostics();
    }

    [Fact]
    public Task Empty_ExactListAndVerify()
    {
        string source = Common.Biased(bias: "\"\"");

        return AssertExactlyBiasedUnitExpressionDiagnostics(source).VerifyDiagnostics();
    }

    private static GeneratorVerifier AssertExactlyBiasedUnitExpressionDiagnostics(string source)
        => GeneratorVerifier.Construct<SharpMeasuresGenerator>(source).AssertExactlyListedDiagnosticsIDsReported(InvalidBiasedUnitExpressionDiagnostics);

    private static IReadOnlyCollection<string> InvalidBiasedUnitExpressionDiagnostics { get; } = new string[] { DiagnosticIDs.InvalidBiasedUnitExpression };
}
