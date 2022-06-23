namespace SharpMeasures.Generators.Tests.Diagnostics.Vectors;

using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Tests.Verify;

using System.Collections.Generic;
using System.Threading.Tasks;

using VerifyXunit;

using Xunit;

[UsesVerify]
public class DuplicateVectorDimension
{
    [Fact]
    public Task Implicit_ExactListAndVerify()
    {
        string source = """
            using SharpMeasures.Generators.Scalars;
            using SharpMeasures.Generators.Units;
            using SharpMeasures.Generators.Vectors;

            [ResizedSharpMeasuresVector(typeof(Position3))]
            public partial class Length3 { }

            [SharpMeasuresVector(typeof(UnitOfLength))]
            public partial class Position3 { }

            [SharpMeasuresScalar(typeof(UnitOfLength))]
            public partial class Length { }

            [SharpMeasuresUnit(typeof(Length))]
            public partial class UnitOfLength { }
            """;

        return AssertExactlyDuplicateVectorDimensionDiagnostics(source).VerifyDiagnostics();
    }

    [Fact]
    public Task Explicit_ExactListAndVerify()
    {
        string source = """
            using SharpMeasures.Generators.Scalars;
            using SharpMeasures.Generators.Units;
            using SharpMeasures.Generators.Vectors;

            [ResizedSharpMeasuresVector(typeof(Position3), Dimension = 3)]
            public partial class PositionVector { }

            [SharpMeasuresVector(typeof(UnitOfLength))]
            public partial class Position3 { }

            [SharpMeasuresScalar(typeof(UnitOfLength))]
            public partial class Length { }

            [SharpMeasuresUnit(typeof(Length))]
            public partial class UnitOfLength { }
            """;

        return AssertExactlyDuplicateVectorDimensionDiagnostics(source).VerifyDiagnostics();
    }

    [Fact]
    public Task Indirect_ExactListAndVerify()
    {
        string source = """
            using SharpMeasures.Generators.Scalars;
            using SharpMeasures.Generators.Units;
            using SharpMeasures.Generators.Vectors;

            [ResizedSharpMeasuresVector(typeof(Position3))]
            public partial class Length2 { }

            [ResizedSharpMeasuresVector(typeof(Position3))]
            public partial class Position2 { }

            [SharpMeasuresVector(typeof(UnitOfLength))]
            public partial class Position3 { }

            [SharpMeasuresScalar(typeof(UnitOfLength))]
            public partial class Length { }

            [SharpMeasuresUnit(typeof(Length))]
            public partial class UnitOfLength { }
            """;

        return AssertExactlyDuplicateVectorDimensionDiagnostics(source).VerifyDiagnostics();
    }

    private static GeneratorVerifier AssertExactlyDuplicateVectorDimensionDiagnostics(string source)
        => GeneratorVerifier.Construct<SharpMeasuresGenerator>(source).AssertExactlyListedDiagnosticsIDsReported(DuplicateVectorDimensionDiagnostics);

    private static IReadOnlyCollection<string> DuplicateVectorDimensionDiagnostics { get; } = new string[] { DiagnosticIDs.DuplicateVectorDimension };
}
