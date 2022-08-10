namespace SharpMeasures.Generators.Tests.Diagnostics.Quantities;

using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Tests.Utility;
using SharpMeasures.Generators.Tests.Verify;

using System.Collections.Generic;
using System.Threading.Tasks;

using VerifyXunit;

using Xunit;

[UsesVerify]
public class UnrecognizedCastOperatorBehaviour
{
    [Fact]
    public Task VerifyUnrecognizedCastOperatorBehaviourDiagnosticsMessage() => AssertScalar(NegativeCastOperatorBehaviour).VerifyDiagnostics();

    [Theory]
    [MemberData(nameof(UnrecognizedCastOperatorBehaviours))]
    public void Scalar(SourceSubtext castOperatorBehaviour) => AssertScalar(castOperatorBehaviour);

    public static IEnumerable<object[]> UnrecognizedCastOperatorBehaviours => new object[][]
    {
        new object[] { NegativeCastOperatorBehaviour },
        new object[] { MaxIntegerCastOperatorBehaviour }
    };

    private static SourceSubtext NegativeCastOperatorBehaviour { get; } = SourceSubtext.Covered("-1", prefix: "(ConversionOperatorBehaviour)(", postfix: ")");
    private static SourceSubtext MaxIntegerCastOperatorBehaviour { get; } = SourceSubtext.Covered("int.MaxValue", prefix: "(ConversionOperatorBehaviour)");

    private static GeneratorVerifier AssertExactlyUnrecognizedCastOperatorBehaviourDiagnostics(string source) => GeneratorVerifier.Construct<SharpMeasuresGenerator>(source).AssertExactlyListedDiagnosticsIDsReported(UnrecognizedCastOperatorBehaviourDiagnostics);
    private static IReadOnlyCollection<string> UnrecognizedCastOperatorBehaviourDiagnostics { get; } = new string[] { DiagnosticIDs.UnrecognizedCastOperatorBehaviour };

    private static string ScalarText(SourceSubtext castOperatorBehaviour) => $$"""
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Utility;
        using SharpMeasures.Generators.Quantities;

        [ConvertibleQuantity(typeof(Length), CastOperatorBehaviour = {{castOperatorBehaviour}})]
        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Distance { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }

        [FixedUnit("Metre", "Metres")]
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertScalar(SourceSubtext castOperatorBehaviour)
    {
        var source = ScalarText(castOperatorBehaviour);
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(source, castOperatorBehaviour.Context.With(outerPrefix: "CastOperatorBehaviour = "));

        return AssertExactlyUnrecognizedCastOperatorBehaviourDiagnostics(source).AssertDiagnosticsLocation(expectedLocation, source);
    }
}
