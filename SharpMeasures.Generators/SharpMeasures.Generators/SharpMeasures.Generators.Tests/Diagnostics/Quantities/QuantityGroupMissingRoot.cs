namespace SharpMeasures.Generators.Tests.Diagnostics.Quantities;

using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Tests.Verify;

using System.Collections.Generic;
using System.Threading.Tasks;

using VerifyXunit;

using Xunit;

[UsesVerify]
public class QuantityGroupMissingRoot
{
    [Fact]
    public Task ResizedVector_Self_ExactListAndVerify()
    {
        string source = """
            using SharpMeasures.Generators.Scalars;
            using SharpMeasures.Generators.Units;
            using SharpMeasures.Generators.Vectors;

            [ResizedSharpMeasuresVector(typeof(Position3))]
            public partial class Position3 { }

            [SharpMeasuresScalar(typeof(UnitOfLength))]
            public partial class Length { }
            
            [SharpMeasuresUnit(typeof(Length))]
            public partial class UnitOfLength { }
            """;

        return AssertExactlyQuantityGroupMissingRootDiagnostics(source, 1).VerifyDiagnostics();
    }

    [Fact]
    public Task ResizedVector_TwoInLoop_ExactListAndVerify()
    {
        string source = """
            using SharpMeasures.Generators.Scalars;
            using SharpMeasures.Generators.Units;
            using SharpMeasures.Generators.Vectors;

            [ResizedSharpMeasuresVector(typeof(Position2))]
            public partial class Position3 { }

            [ResizedSharpMeasuresVector(typeof(Position3))]
            public partial class Position2 { }

            [SharpMeasuresScalar(typeof(UnitOfLength))]
            public partial class Length { }
            
            [SharpMeasuresUnit(typeof(Length))]
            public partial class UnitOfLength { }
            """;

        return AssertExactlyQuantityGroupMissingRootDiagnostics(source, 2).VerifyDiagnostics();
    }

    [Fact]
    public Task ResizedVector_ThreeInLoop_ExactListAndVerify()
    {
        string source = """
            using SharpMeasures.Generators.Scalars;
            using SharpMeasures.Generators.Units;
            using SharpMeasures.Generators.Vectors;

            [ResizedSharpMeasuresVector(typeof(Position2))]
            public partial class Position4 { }

            [ResizedSharpMeasuresVector(typeof(Position4))]
            public partial class Position3 { }

            [ResizedSharpMeasuresVector(typeof(Position3))]
            public partial class Position2 { }

            [SharpMeasuresScalar(typeof(UnitOfLength))]
            public partial class Length { }
            
            [SharpMeasuresUnit(typeof(Length))]
            public partial class UnitOfLength { }
            """;

        return AssertExactlyQuantityGroupMissingRootDiagnostics(source, 3).VerifyDiagnostics();
    }

    [Fact]
    public Task ResizedVector_TwoInLoopWithBranch_ExactListAndVerify()
    {
        string source = """
            using SharpMeasures.Generators.Scalars;
            using SharpMeasures.Generators.Units;
            using SharpMeasures.Generators.Vectors;

            [ResizedSharpMeasuresVector(typeof(Position3))]
            public partial class Position4 { }

            [ResizedSharpMeasuresVector(typeof(Position4))]
            public partial class Position3 { }

            [ResizedSharpMeasuresVector(typeof(Position3))]
            public partial class Position2 { }

            [SharpMeasuresScalar(typeof(UnitOfLength))]
            public partial class Length { }
            
            [SharpMeasuresUnit(typeof(Length))]
            public partial class UnitOfLength { }
            """;

        return AssertExactlyQuantityGroupMissingRootDiagnostics(source, 3).VerifyDiagnostics();
    }

    private static GeneratorVerifier AssertExactlyQuantityGroupMissingRootDiagnostics(string source, int diagnosticsCount)
    {
        return GeneratorVerifier.Construct<SharpMeasuresGenerator>(source).AssertExactlyListedDiagnosticsIDsReported(expectedDiagnostics());

        IEnumerable<string> expectedDiagnostics()
        {
            for (int i = 0; i < diagnosticsCount; i++)
            {
                yield return DiagnosticIDs.QuantityGroupMissingRoot;
            }
        }
    }
}
