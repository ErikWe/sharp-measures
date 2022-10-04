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

    public static string Text(SourceSubtext scale) => $$"""
        using SharpMeasures.Generators;

        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }

        [FixedUnitInstance("Metre", "Metres")]
        [ScaledUnitInstance("Kilometre", "Kilometres", "Metre", {{scale}})]
        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier Assert(SourceSubtext scale)
    {
        var source = Text(scale);
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(source, scale.Context.With(outerPrefix: "[ScaledUnitInstance(\"Kilometre\", \"Kilometres\", \"Metre\", "));

        return AssertExactlyInvalidScaledUnitExpressionDiagnostics(source).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(Identical);
    }

    private static GeneratorVerifier Identical => GeneratorVerifier.Construct<SharpMeasuresGenerator>(IdenticalText);

    public static string IdenticalText => """
        using SharpMeasures.Generators;

        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }

        [FixedUnitInstance("Metre", "Metres")]
        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;
}
