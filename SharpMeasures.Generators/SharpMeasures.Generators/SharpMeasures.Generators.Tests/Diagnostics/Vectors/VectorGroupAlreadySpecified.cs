namespace SharpMeasures.Generators.Tests.Diagnostics.Vectors;

using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Tests.Verify;

using System.Collections.Generic;
using System.Threading.Tasks;

using VerifyXunit;

using Xunit;

[UsesVerify]
public class VectorGroupAlreadySpecified
{
    [Fact]
    public Task DimensionalEquivalence_SameAttribute_ExactListAndVerify()
    {
        string source = """
            using SharpMeasures.Generators.Quantities;
            using SharpMeasures.Generators.Scalars;
            using SharpMeasures.Generators.Units;
            using SharpMeasures.Generators.Vectors;

            [SharpMeasuresVector(typeof(UnitOfLength))]
            public partial class Displacement3 { }

            [ResizedSharpMeasuresVector(typeof(Displacement3))]
            public partial class Displacement2 { }

            [DimensionalEquivalence(typeof(Displacement2), typeof(Displacement3))]
            [SharpMeasuresVector(typeof(UnitOfLength))]
            public partial class Position3 { }

            [SharpMeasuresScalar(typeof(UnitOfLength))]
            public partial class Length { }

            [SharpMeasuresUnit(typeof(Length))]
            public partial class UnitOfLength { }
            """;

        return AssertExactlyVectorGroupAlreadySpecifiedDiagnosticsWithValidLocation(source).VerifyDiagnostics();
    }

    [Fact]
    public Task DimensionalEquivalence_SameVector_ExactListAndVerify()
    {
        string source = """
            using SharpMeasures.Generators.Quantities;
            using SharpMeasures.Generators.Scalars;
            using SharpMeasures.Generators.Units;
            using SharpMeasures.Generators.Vectors;

            [DimensionalEquivalence(typeof(Position3))]
            [SharpMeasuresVector(typeof(UnitOfLength))]
            public partial class Displacement3 { }

            [DimensionalEquivalence(typeof(Position3))]
            [ResizedSharpMeasuresVector(typeof(Displacement3))]
            public partial class Displacement2 { }

            [SharpMeasuresVector(typeof(UnitOfLength))]
            public partial class Position3 { }

            [SharpMeasuresScalar(typeof(UnitOfLength))]
            public partial class Length { }

            [SharpMeasuresUnit(typeof(Length))]
            public partial class UnitOfLength { }
            """;

        return AssertExactlyVectorGroupAlreadySpecifiedDiagnosticsWithValidLocation(source).VerifyDiagnostics();
    }

    [Fact]
    public Task DimensionalEquivalence_SeparateVectors_ExactListAndVerify()
    {
        string source = """
            using SharpMeasures.Generators.Quantities;
            using SharpMeasures.Generators.Scalars;
            using SharpMeasures.Generators.Units;
            using SharpMeasures.Generators.Vectors;

            [DimensionalEquivalence(typeof(Position3))]
            [SharpMeasuresVector(typeof(UnitOfLength))]
            public partial class Displacement3 { }

            [DimensionalEquivalence(typeof(Position2))]
            [ResizedSharpMeasuresVector(typeof(Displacement3))]
            public partial class Displacement2 { }

            [SharpMeasuresVector(typeof(UnitOfLength))]
            public partial class Position3 { }

            [ResizedSharpMeasuresVector(typeof(Position3))]
            public partial class Position2 { }

            [SharpMeasuresScalar(typeof(UnitOfLength))]
            public partial class Length { }

            [SharpMeasuresUnit(typeof(Length))]
            public partial class UnitOfLength { }
            """;

        return AssertExactlyVectorGroupAlreadySpecifiedDiagnosticsWithValidLocation(source).VerifyDiagnostics();
    }

    private static GeneratorVerifier AssertExactlyVectorGroupAlreadySpecifiedDiagnosticsWithValidLocation(string source) => GeneratorVerifier.Construct<SharpMeasuresGenerator>(source).AssertExactlyListedDiagnosticsIDsReported(VectorGroupAlreadySpecifiedDiagnostics).AssertAllDiagnosticsValidLocation();
    private static IReadOnlyCollection<string> VectorGroupAlreadySpecifiedDiagnostics { get; } = new string[] { DiagnosticIDs.VectorGroupAlreadySpecified };
}
