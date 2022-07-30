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
    public Task Scalar_DefinedAsUnit_ExactListAndVerify()
    {
        string source = """
            using SharpMeasures.Generators.Scalars;
            using SharpMeasures.Generators.Units;

            [SharpMeasuresScalar(typeof(UnitOfLength))]
            [SharpMeasuresUnit(typeof(Length))]
            public partial class UnitOfDistance { }

            [SharpMeasuresScalar(typeof(UnitOfLength))]
            public partial class Length { }

            [SharpMeasuresUnit(typeof(Length))]
            public partial class UnitOfLength { }
            """;

        return AssertExactlyTypeAlreadyDefinedDiagnostics(source).VerifyDiagnostics();
    }

    [Fact]
    public Task SpecializedScalar_DefinedAsUnit_ExactListAndVerify()
    {
        string source = """
            using SharpMeasures.Generators.Scalars;
            using SharpMeasures.Generators.Units;
            using SharpMeasures.Generators.Vectors;

            [SpecializedSharpMeasuresScalar(typeof(Length))]
            [SharpMeasuresUnit(typeof(Length))]
            public partial class UnitOfDistance { }

            [SharpMeasuresScalar(typeof(UnitOfLength))]
            public partial class Length { }

            [SharpMeasuresUnit(typeof(Length))]
            public partial class UnitOfLength { }
            """;

        return AssertExactlyTypeAlreadyDefinedDiagnostics(source).VerifyDiagnostics();
    }

    [Fact]
    public Task SpecializedScalar_DefinedAsScalar_ExactListAndVerify()
    {
        string source = """
            using SharpMeasures.Generators.Scalars;
            using SharpMeasures.Generators.Units;
            using SharpMeasures.Generators.Vectors;

            [SpecializedSharpMeasuresScalar(typeof(Length))]
            [SharpMeasuresScalar(typeof(UnitOfLength))]
            public partial class Distance { }

            [SharpMeasuresScalar(typeof(UnitOfLength))]
            public partial class Length { }

            [SharpMeasuresUnit(typeof(Length))]
            public partial class UnitOfLength { }
            """;

        return AssertExactlyTypeAlreadyDefinedDiagnostics(source).VerifyDiagnostics();
    }

    [Fact]
    public Task Vector_DefinedAsUnit_ExactListAndVerify()
    {
        string source = """
            using SharpMeasures.Generators.Scalars;
            using SharpMeasures.Generators.Units;
            using SharpMeasures.Generators.Vectors;

            [SharpMeasuresVector(typeof(UnitOfLength))]
            [SharpMeasuresUnit(typeof(Length))]
            public partial class Position2 { }

            [SharpMeasuresScalar(typeof(UnitOfLength))]
            public partial class Length { }

            [SharpMeasuresUnit(typeof(Length))]
            public partial class UnitOfLength { }
            """;

        return AssertExactlyTypeAlreadyDefinedDiagnostics(source).VerifyDiagnostics();
    }

    [Fact]
    public Task Vector_DefinedAsScalar_ExactListAndVerify()
    {
        string source = """
            using SharpMeasures.Generators.Scalars;
            using SharpMeasures.Generators.Units;
            using SharpMeasures.Generators.Vectors;

            [SharpMeasuresVector(typeof(UnitOfLength))]
            [SharpMeasuresScalar(typeof(UnitOfLength))]
            public partial class Position2 { }

            [SharpMeasuresScalar(typeof(UnitOfLength))]
            public partial class Length { }

            [SharpMeasuresUnit(typeof(Length))]
            public partial class UnitOfLength { }
            """;

        return AssertExactlyTypeAlreadyDefinedDiagnostics(source).VerifyDiagnostics();
    }

    [Fact]
    public Task Vector_DefinedAsSpecializedScalar_ExactListAndVerify()
    {
        string source = """
            using SharpMeasures.Generators.Scalars;
            using SharpMeasures.Generators.Units;
            using SharpMeasures.Generators.Vectors;

            [SharpMeasuresVector(typeof(UnitOfLength))]
            [SpecializedSharpMeasuresScalar(typeof(Length))]
            public partial class Position2 { }

            [SharpMeasuresScalar(typeof(UnitOfLength))]
            public partial class Length { }

            [SharpMeasuresUnit(typeof(Length))]
            public partial class UnitOfLength { }
            """;

        return AssertExactlyTypeAlreadyDefinedDiagnostics(source).VerifyDiagnostics();
    }

    [Fact]
    public Task SpecializedVector_DefinedAsUnit_ExactListAndVerify()
    {
        string source = """
            using SharpMeasures.Generators.Scalars;
            using SharpMeasures.Generators.Units;
            using SharpMeasures.Generators.Vectors;

            [SpecializedSharpMeasuresVector(typeof(UnitOfLength))]
            [SharpMeasuresUnit(typeof(Length))]
            public partial class Displacement2 { }

            [SharpMeasuresVector(typeof(UnitOfLength))]
            public partial class Position2 { }

            [SharpMeasuresScalar(typeof(UnitOfLength))]
            public partial class Length { }

            [SharpMeasuresUnit(typeof(Length))]
            public partial class UnitOfLength { }
            """;

        return AssertExactlyTypeAlreadyDefinedDiagnostics(source).VerifyDiagnostics();
    }

    [Fact]
    public Task SpecializedVector_DefinedAsScalar_ExactListAndVerify()
    {
        string source = """
            using SharpMeasures.Generators.Scalars;
            using SharpMeasures.Generators.Units;
            using SharpMeasures.Generators.Vectors;

            [SpecializedSharpMeasuresVector(typeof(UnitOfLength))]
            [SharpMeasuresScalar(typeof(UnitOfLength))]
            public partial class Displacement2 { }

            [SharpMeasuresVector(typeof(UnitOfLength))]
            public partial class Position2 { }

            [SharpMeasuresScalar(typeof(UnitOfLength))]
            public partial class Length { }

            [SharpMeasuresUnit(typeof(Length))]
            public partial class UnitOfLength { }
            """;

        return AssertExactlyTypeAlreadyDefinedDiagnostics(source).VerifyDiagnostics();
    }

    [Fact]
    public Task SpecializedVector_DefinedAsSpecializedScalar_ExactListAndVerify()
    {
        string source = """
            using SharpMeasures.Generators.Scalars;
            using SharpMeasures.Generators.Units;
            using SharpMeasures.Generators.Vectors;

            [SpecializedSharpMeasuresVector(typeof(UnitOfLength))]
            [SpecializedSharpMeasuresScalar(typeof(Length))]
            public partial class Displacement2 { }

            [SharpMeasuresVector(typeof(UnitOfLength))]
            public partial class Position2 { }

            [SharpMeasuresScalar(typeof(UnitOfLength))]
            public partial class Length { }

            [SharpMeasuresUnit(typeof(Length))]
            public partial class UnitOfLength { }
            """;

        return AssertExactlyTypeAlreadyDefinedDiagnostics(source).VerifyDiagnostics();
    }

    [Fact]
    public Task SpecializedVector_DefinedAsVector_ExactListAndVerify()
    {
        string source = """
            using SharpMeasures.Generators.Scalars;
            using SharpMeasures.Generators.Units;
            using SharpMeasures.Generators.Vectors;

            [SpecializedSharpMeasuresVector(typeof(UnitOfLength))]
            [SharpMeasuresVector(typeof(UnitOfLength))]
            public partial class Displacement2 { }

            [SharpMeasuresVector(typeof(UnitOfLength))]
            public partial class Position2 { }

            [SharpMeasuresScalar(typeof(UnitOfLength))]
            public partial class Length { }

            [SharpMeasuresUnit(typeof(Length))]
            public partial class UnitOfLength { }
            """;

        return AssertExactlyTypeAlreadyDefinedDiagnostics(source).VerifyDiagnostics();
    }

    [Fact]
    public Task SpecializedVectorGroup_DefinedAsVectorGroup_ExactListAndVerify()
    {
        string source = """
            using SharpMeasures.Generators.Scalars;
            using SharpMeasures.Generators.Units;
            using SharpMeasures.Generators.Vectors;

            [SpecializedSharpMeasuresVectorGroup(typeof(Position))]
            [SharpMeasuresVectorGroup(typeof(UnitOfLength))]
            public static partial class Displacement { }

            [SharpMeasuresVectorGroup(typeof(UnitOfLength))]
            public static partial class Position { }

            [SharpMeasuresScalar(typeof(UnitOfLength))]
            public partial class Length { }

            [SharpMeasuresUnit(typeof(Length))]
            public partial class UnitOfLength { }
            """;

        return AssertExactlyTypeAlreadyDefinedDiagnostics(source).VerifyDiagnostics();
    }

    [Fact]
    public Task VectorGroupMember_DefinedAsUnit_ExactListAndVerify()
    {
        string source = """
            using SharpMeasures.Generators.Scalars;
            using SharpMeasures.Generators.Units;
            using SharpMeasures.Generators.Vectors;

            [SharpMeasuresVectorGroupMember(typeof(Position))]
            [SharpMeasuresUnit(typeof(Length))]
            public partial class Position2 { }

            [SharpMeasuresVectorGroup(typeof(UnitOfLength))]
            public static partial class Position { }

            [SharpMeasuresScalar(typeof(UnitOfLength))]
            public partial class Length { }

            [SharpMeasuresUnit(typeof(Length))]
            public partial class UnitOfLength { }
            """;

        return AssertExactlyTypeAlreadyDefinedDiagnostics(source).VerifyDiagnostics();
    }

    [Fact]
    public Task VectorGroupMember_DefinedAsScalar_ExactListAndVerify()
    {
        string source = """
            using SharpMeasures.Generators.Scalars;
            using SharpMeasures.Generators.Units;
            using SharpMeasures.Generators.Vectors;

            [SharpMeasuresVectorGroupMember(typeof(Position))]
            [SharpMeasuresScalar(typeof(UnitOfLength))]
            public partial class Position2 { }

            [SharpMeasuresVectorGroup(typeof(UnitOfLength))]
            public static partial class Position { }

            [SharpMeasuresScalar(typeof(UnitOfLength))]
            public partial class Length { }

            [SharpMeasuresUnit(typeof(Length))]
            public partial class UnitOfLength { }
            """;

        return AssertExactlyTypeAlreadyDefinedDiagnostics(source).VerifyDiagnostics();
    }

    [Fact]
    public Task VectorGroupMember_DefinedAsSpecializedScalar_ExactListAndVerify()
    {
        string source = """
            using SharpMeasures.Generators.Scalars;
            using SharpMeasures.Generators.Units;
            using SharpMeasures.Generators.Vectors;

            [SharpMeasuresVectorGroupMember(typeof(Position))]
            [SpecializedSharpMeasuresScalar(typeof(Length))]
            public partial class Position2 { }

            [SharpMeasuresVectorGroup(typeof(UnitOfLength))]
            public static partial class Position { }

            [SharpMeasuresScalar(typeof(UnitOfLength))]
            public partial class Length { }

            [SharpMeasuresUnit(typeof(Length))]
            public partial class UnitOfLength { }
            """;

        return AssertExactlyTypeAlreadyDefinedDiagnostics(source).VerifyDiagnostics();
    }

    [Fact]
    public Task VectorGroupMember_DefinedAsVector_ExactListAndVerify()
    {
        string source = """
            using SharpMeasures.Generators.Scalars;
            using SharpMeasures.Generators.Units;
            using SharpMeasures.Generators.Vectors;

            [SharpMeasuresVectorGroupMember(typeof(Position))]
            [SharpMeasuresVector(typeof(UnitOfLength))]
            public partial class Position2 { }

            [SharpMeasuresVectorGroup(typeof(UnitOfLength))]
            public static partial class Position { }

            [SharpMeasuresScalar(typeof(UnitOfLength))]
            public partial class Length { }

            [SharpMeasuresUnit(typeof(Length))]
            public partial class UnitOfLength { }
            """;

        return AssertExactlyTypeAlreadyDefinedDiagnostics(source).VerifyDiagnostics();
    }

    [Fact]
    public Task VectorGroupMember_DefinedAsSpecializedVector_ExactListAndVerify()
    {
        string source = """
            using SharpMeasures.Generators.Scalars;
            using SharpMeasures.Generators.Units;
            using SharpMeasures.Generators.Vectors;

            [SharpMeasuresVectorGroupMember(typeof(Displacement))]
            [SpecializedSharpMeasuresVector(typeof(Position2))]
            public partial class Displacement2 { }

            [SharpMeasuresVectorGroup(typeof(UnitOfLength))]
            public static partial class Displacement { }

            [SharpMeasuresVector(typeof(UnitOfLength))]
            public partial class Position2 { }

            [SharpMeasuresScalar(typeof(UnitOfLength))]
            public partial class Length { }

            [SharpMeasuresUnit(typeof(Length))]
            public partial class UnitOfLength { }
            """;

        return AssertExactlyTypeAlreadyDefinedDiagnostics(source).VerifyDiagnostics();
    }

    private static GeneratorVerifier AssertExactDiagnostics(string source, IEnumerable<string> expectedDiagnostics) => GeneratorVerifier.Construct<SharpMeasuresGenerator>(source).AssertExactlyListedDiagnosticsIDsReported(expectedDiagnostics);
    private static GeneratorVerifier AssertExactlyTypeAlreadyDefinedDiagnostics(string source) => AssertExactDiagnostics(source, TypeAlreadyDefinedDiagnostics);
    private static IReadOnlyCollection<string> TypeAlreadyDefinedDiagnostics { get; } = new string[] { DiagnosticIDs.TypeAlreadyDefined };
}
