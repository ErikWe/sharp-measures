namespace SharpMeasures.Generators.Tests.Diagnostics.Quantities;

using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Tests.Verify;

using System.Collections.Generic;
using System.Threading.Tasks;

using VerifyXunit;

using Xunit;

[UsesVerify]
public class UnrecognizedCastOperatorBehaviour
{
    [Fact]
    public Task Negative_ExactListAndVerify()
    {
        string source = """
            using SharpMeasures.Generators.Scalars;
            using SharpMeasures.Generators.Units;
            using SharpMeasures.Generators.Quantities;
            using SharpMeasures.Generators.Quantities.Utility;

            [DimensionalEquivalence(typeof(Distance), CastOperatorBehaviour = (ConversionOperationBehaviour)(-1))]
            [SharpMeasuresScalar(typeof(UnitOfLength))]
            public partial class Length { }

            [SharpMeasuresScalar(typeof(UnitOfLength))]
            public partial class Distance { }

            [FixedUnit("Metre", "Metres", 1)]
            [SharpMeasuresUnit(typeof(Length))]
            public partial class UnitOfLength { }
            """;

        return AssertExactlyUnrecognizedCastOperatorBehaviourDiagnostics(source).VerifyDiagnostics();
    }

    [Fact]
    public Task TooLarge_ExactListAndVerify()
    {
        string source = """
            using SharpMeasures.Generators.Scalars;
            using SharpMeasures.Generators.Units;
            using SharpMeasures.Generators.Quantities;
            using SharpMeasures.Generators.Quantities.Utility;

            [DimensionalEquivalence(typeof(Distance), CastOperatorBehaviour = (ConversionOperationBehaviour)int.MaxValue)]
            [SharpMeasuresScalar(typeof(UnitOfLength))]
            public partial class Length { }

            [SharpMeasuresScalar(typeof(UnitOfLength))]
            public partial class Distance { }

            [FixedUnit("Metre", "Metres", 1)]
            [SharpMeasuresUnit(typeof(Length))]
            public partial class UnitOfLength { }
            """;

        return AssertExactlyUnrecognizedCastOperatorBehaviourDiagnostics(source).VerifyDiagnostics();
    }

    private static GeneratorVerifier AssertExactlyUnrecognizedCastOperatorBehaviourDiagnostics(string source)
        => GeneratorVerifier.Construct<SharpMeasuresGenerator>(source).AssertExactlyListedDiagnosticsIDsReported(UnrecognizedCastOperatorBehaviourDiagnostics);

    private static IReadOnlyCollection<string> UnrecognizedCastOperatorBehaviourDiagnostics { get; } = new string[]
    {
        DiagnosticIDs.UnrecognizedCastOperatorBehaviour
    };
}
