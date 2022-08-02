namespace SharpMeasures.Generators.Tests.Diagnostics.Quantities;

using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Tests.Verify;

using System.Collections.Generic;
using System.Threading.Tasks;

using VerifyXunit;

using Xunit;

[UsesVerify]
public class DifferenceDisabledButQuantitySpecified
{
    [Fact]
    public Task Scalar_ExactListAndVerify()
    {
        string source = """
            using SharpMeasures.Generators.Scalars;
            using SharpMeasures.Generators.Units;

            [SharpMeasuresScalar(typeof(UnitOfLength), ImplementDifference = false, Difference = typeof(Length))]
            public partial class Length { }

            [SharpMeasuresUnit(typeof(Length))]
            public partial class UnitOfLength { }
            """;

        return AssertExactlyDifferenceDisabledButQuantitySpecifiedDiagnosticsWithValidLocation(source).VerifyDiagnostics();
    }

    [Fact]
    public Task Vector_ExactListAndVerify()
    {
        string source = """
            using SharpMeasures.Generators.Scalars;
            using SharpMeasures.Generators.Units;
            using SharpMeasures.Generators.Vectors;

            [SharpMeasuresVector(typeof(UnitOfLength), ImplementDifference = false, Difference = typeof(Displacement3))]
            public partial class Position3 { }

            [SharpMeasuresVector(typeof(UnitOfLength))]
            public partial class Displacement3 { }

            [SharpMeasuresScalar(typeof(UnitOfLength))]
            public partial class Length { }

            [SharpMeasuresUnit(typeof(Length))]
            public partial class UnitOfLength { }
            """;

        return AssertExactlyDifferenceDisabledButQuantitySpecifiedDiagnosticsWithValidLocation(source).VerifyDiagnostics();
    }

    private static GeneratorVerifier AssertExactlyDifferenceDisabledButQuantitySpecifiedDiagnosticsWithValidLocation(string source) => GeneratorVerifier.Construct<SharpMeasuresGenerator>(source).AssertExactlyListedDiagnosticsIDsReported(DifferenceDisabledButQuantitySpecifiedDiagnostics).AssertAllDiagnosticsValidLocation();
    private static IReadOnlyCollection<string> DifferenceDisabledButQuantitySpecifiedDiagnostics { get; } = new string[] { DiagnosticIDs.DifferenceDisabledButQuantitySpecified };
}
