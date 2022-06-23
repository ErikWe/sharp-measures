namespace SharpMeasures.Generators.Tests.Diagnostics;

using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Tests.Verify;

using System.Collections.Generic;
using System.Threading.Tasks;

using VerifyXunit;

using Xunit;

[UsesVerify]
public class TypeAlreadyDefined
{
    [Fact]
    public Task UnitAndScalar_ExactListAndVerify()
    {
        string source = """
            using SharpMeasures.Generators.Scalars;
            using SharpMeasures.Generators.Units;

            [SharpMeasuresScalar(typeof(UnitOfLength))]
            public partial class Length { }

            [SharpMeasuresUnit(typeof(Length))]
            public partial class UnitOfLength { }

            [SharpMeasuresUnit(typeof(Length))]
            [SharpMeasuresScalar(typeof(UnitOfLength))]
            public partial class UnitOfLength2 { }
            """;

        return AssertExactlyTypeAlreadyDefinedDiagnostics(source).VerifyDiagnostics();
    }

    [Fact]
    public Task UnitAndVector_ExactListAndVerify()
    {
        string source = """
            using SharpMeasures.Generators.Scalars;
            using SharpMeasures.Generators.Units;
            using SharpMeasures.Generators.Vectors;

            [SharpMeasuresScalar(typeof(UnitOfLength))]
            public partial class Length { }

            [SharpMeasuresUnit(typeof(Length))]
            public partial class UnitOfLength { }

            [SharpMeasuresUnit(typeof(Length))]
            [SharpMeasuresVector(typeof(UnitOfLength))]
            public partial class UnitOfLength2 { }
            """;

        return AssertExactlyTypeAlreadyDefinedDiagnostics(source).VerifyDiagnostics();
    }

    [Fact]
    public Task UnitAndResizedVector_ExactListAndVerify()
    {
        string source = """
            using SharpMeasures.Generators.Scalars;
            using SharpMeasures.Generators.Units;
            using SharpMeasures.Generators.Vectors;

            [SharpMeasuresScalar(typeof(UnitOfLength))]
            public partial class Length { }

            [SharpMeasuresUnit(typeof(Length))]
            public partial class UnitOfLength { }

            [SharpMeasuresVector(typeof(UnitOfLength))]
            public partial class Position3 { }

            [SharpMeasuresUnit(typeof(Length))]
            [ResizedSharpMeasuresVector(typeof(Position3))]
            public partial class UnitOfLength2 { }
            """;

        return AssertExactlyTypeAlreadyDefinedDiagnostics(source).VerifyDiagnostics();
    }

    [Fact]
    public Task ScalarAndVector_ExactListAndVerify()
    {
        string source = """
            using SharpMeasures.Generators.Scalars;
            using SharpMeasures.Generators.Units;
            using SharpMeasures.Generators.Vectors;

            [SharpMeasuresScalar(typeof(UnitOfLength))]
            public partial class Length { }

            [SharpMeasuresUnit(typeof(Length))]
            public partial class UnitOfLength { }

            [SharpMeasuresScalar(typeof(UnitOfLength))]
            [SharpMeasuresVector(typeof(UnitOfLength))]
            public partial class Length2 { }
            """;

        return AssertExactlyTypeAlreadyDefinedDiagnostics(source).VerifyDiagnostics();
    }

    [Fact]
    public Task ScalarAndResizedVector_ExactListAndVerify()
    {
        string source = """
            using SharpMeasures.Generators.Scalars;
            using SharpMeasures.Generators.Units;
            using SharpMeasures.Generators.Vectors;

            [SharpMeasuresScalar(typeof(UnitOfLength))]
            public partial class Length { }

            [SharpMeasuresUnit(typeof(Length))]
            public partial class UnitOfLength { }

            [SharpMeasuresVector(typeof(UnitOfLength))]
            public partial class Position3 { }

            [SharpMeasuresScalar(typeof(UnitOfLength))]
            [ResizedSharpMeasuresVector(typeof(Position3))]
            public partial class Length2 { }
            """;

        return AssertExactlyTypeAlreadyDefinedDiagnostics(source).VerifyDiagnostics();
    }

    [Fact]
    public Task VectorAndResizedVector_ExactListAndVerify()
    {
        string source = """
            using SharpMeasures.Generators.Scalars;
            using SharpMeasures.Generators.Units;
            using SharpMeasures.Generators.Vectors;

            [SharpMeasuresScalar(typeof(UnitOfLength))]
            public partial class Length { }

            [SharpMeasuresUnit(typeof(Length))]
            public partial class UnitOfLength { }

            [SharpMeasuresVector(typeof(UnitOfLength))]
            public partial class Position3 { }

            [SharpMeasuresVector(typeof(UnitOfLength))]
            [ResizedSharpMeasuresVector(typeof(Position3))]
            public partial class Length2 { }
            """;

        return AssertExactlyTypeAlreadyDefinedDiagnostics(source).VerifyDiagnostics();
    }

    private static GeneratorVerifier AssertExactlyTypeAlreadyDefinedDiagnostics(string source)
        => GeneratorVerifier.Construct<SharpMeasuresGenerator>(source).AssertExactlyListedDiagnosticsIDsReported(TypeAlreadyDefinedDiagnostics);

    private static IReadOnlyCollection<string> TypeAlreadyDefinedDiagnostics { get; } = new string[] { DiagnosticIDs.TypeAlreadyDefined };
}
