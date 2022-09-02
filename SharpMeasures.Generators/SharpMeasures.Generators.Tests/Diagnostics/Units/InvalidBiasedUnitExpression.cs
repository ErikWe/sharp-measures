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

    private static string Text(SourceSubtext bias) => $$"""
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;

        [SharpMeasuresScalar(typeof(UnitOfTemperature))]
        public partial class TemperatureDifference { }

        [FixedUnitInstance("Kelvin", "Kelvin")]
        [BiasedUnitInstance("Celsius", "Celsius", "Kelvin", {{bias}})]
        [SharpMeasuresUnit(typeof(TemperatureDifference), BiasTerm = true)]
        public partial class UnitOfTemperature { }
        """;

    private static GeneratorVerifier Assert(SourceSubtext bias)
    {
        var source = Text(bias);
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(source, bias.Context.With(outerPrefix: "[BiasedUnitInstance(\"Celsius\", \"Celsius\", \"Kelvin\", "));

        return AssertExactlyInvalidBiasedUnitExpressionDiagnostics(source).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(Identical);
    }

    private static GeneratorVerifier Identical => GeneratorVerifier.Construct<SharpMeasuresGenerator>(IdenticalText);

    private static string IdenticalText => """
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;

        [SharpMeasuresScalar(typeof(UnitOfTemperature))]
        public partial class TemperatureDifference { }

        [FixedUnitInstance("Kelvin", "Kelvin")]
        [SharpMeasuresUnit(typeof(TemperatureDifference), BiasTerm = true)]
        public partial class UnitOfTemperature { }
        """;
}
