namespace SharpMeasures.Generators.Tests.Diagnostics;

using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Tests.Verify;

using System.Collections.Generic;
using System.Threading.Tasks;

using VerifyXunit;

using Xunit;

[UsesVerify]
public class TypeNotUnbiasedScalar
{
    [Fact]
    public Task UnitQuantity_UnbiasedUnit_ExactListAndVerify()
    {
        string source = """
            using SharpMeasures.Generators.Scalars;
            using SharpMeasures.Generators.Units;

            [SharpMeasuresScalar(typeof(UnitOfTemperature), UseUnitBias = true)]
            public partial class Temperature { }

            [SharpMeasuresUnit(typeof(Temperature))]
            public partial class UnitOfTemperature { }
            """;

        string[] expectedDiagnostics = new[] { DiagnosticIDs.TypeNotUnbiasedScalar, DiagnosticIDs.UnitNotIncludingBiasTerm };

        return AssertExactlyExpectedDiagnostics(source, expectedDiagnostics).VerifyListedDiagnosticIDs(TypeNotUnbiasedDiagnostics);
    }

    [Fact]
    public Task UnitQuantity_BiasedUnit_ExactListAndVerify()
    {
        string source = """
            using SharpMeasures.Generators.Scalars;
            using SharpMeasures.Generators.Units;

            [SharpMeasuresScalar(typeof(UnitOfTemperature), UseUnitBias = true)]
            public partial class Temperature { }

            [SharpMeasuresUnit(typeof(Temperature), BiasTerm = true)]
            public partial class UnitOfTemperature { }
            """;

        return AssertExactlyTypeNotUnbiasedDiagnostics(source).VerifyDiagnostics();
    }

    private static GeneratorVerifier AssertExactlyTypeNotUnbiasedDiagnostics(string source) => AssertExactlyExpectedDiagnostics(source, TypeNotUnbiasedDiagnostics);

    private static GeneratorVerifier AssertExactlyExpectedDiagnostics(string source, IEnumerable<string> expectedDiagnostics)
        => GeneratorVerifier.Construct<SharpMeasuresGenerator>(source).AssertExactlyListedDiagnosticsIDsReported(expectedDiagnostics);

    private static IReadOnlyCollection<string> TypeNotUnbiasedDiagnostics { get; } = new string[] { DiagnosticIDs.TypeNotUnbiasedScalar };
}
