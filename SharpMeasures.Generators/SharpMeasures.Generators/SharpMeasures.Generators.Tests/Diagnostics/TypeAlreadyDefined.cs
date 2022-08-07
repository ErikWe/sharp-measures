namespace SharpMeasures.Generators.Tests.Diagnostics;

using Microsoft.CodeAnalysis.Text;

using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Tests.Utility;
using SharpMeasures.Generators.Tests.Verify;

using System.Collections.Generic;
using System.Threading.Tasks;

using VerifyXunit;

using Xunit;

[UsesVerify]
public class TypeAlreadyDefined
{
    [Fact]
    public Task Scalar_DefinedAsUnit_Verify() => AssertAndVerifyScalar(UnitDefinition);

    [Fact]
    public Task SpecializedScalar_DefinedAsUnit_Verify() => AssertAndVerifySpecializedScalar(UnitDefinition);

    [Fact]
    public Task SpecializedScalar_DefinedAsScalar_Verify() => AssertAndVerifySpecializedScalar(ScalarDefinition);

    [Fact]
    public Task Vector_DefinedAsUnit_Verify() => AssertAndVerifyVector(UnitDefinition);

    [Fact]
    public Task Vector_DefinedAsScalar_Verify() => AssertAndVerifyVector(ScalarDefinition);

    [Fact]
    public void Vector_DefinedAsSpecializedScalar() => AssertAndVerifyVector(SpecializedScalarDefinition);

    [Fact]
    public Task SpecializedVector_DefinedAsUnit_Verify() => AssertAndVerifySpecializedVector(UnitDefinition);

    [Fact]
    public Task SpecializedVector_DefinedAsScalar_Verify() => AssertAndVerifySpecializedVector(ScalarDefinition);

    [Fact]
    public void SpecializedVector_DefinedAsSpecializedScalar() => AssertAndVerifySpecializedVector(SpecializedScalarDefinition);

    [Fact]
    public Task SpecializedVector_DefinedAsVector_Verify() => AssertAndVerifySpecializedVector(VectorDefinition);

    [Fact]
    public Task SpecializedVectorGroup_DefinedAsVectorGroup_Verify() => AssertAndVerifySpecializedVectorGroup(VectorGroupDefinition);

    [Fact]
    public Task VectorGroupMember_DefinedAsUnit_Verify() => AssertAndVerifyVectorGroupMember(UnitDefinition);

    [Fact]
    public Task VectorGroupMember_DefinedAsScalar_Verify() => AssertAndVerifyVectorGroupMember(ScalarDefinition);

    [Fact]
    public void VectorGroupMember_DefinedAsSpecializedScalar() => AssertAndVerifyVectorGroupMember(SpecializedScalarDefinition);

    [Fact]
    public Task VectorGroupMember_DefinedAsVector_Verify() => AssertAndVerifyVectorGroupMember(VectorDefinition);

    [Fact]
    public void VectorGroupMember_DefinedAsSpecializedVector() => AssertAndVerifyVectorGroupMember(SpecializedVectorDefinition);

    private static SourceSubtext UnitDefinition { get; } = SourceSubtext.Typeof("Length", prefix: "SharpMeasuresUnit(", postfix: ")");
    private static SourceSubtext ScalarDefinition { get; } = SourceSubtext.Typeof("UnitOfLength", prefix: "SharpMeasuresScalar(", postfix: ")");
    private static SourceSubtext SpecializedScalarDefinition { get; } = SourceSubtext.Typeof("Length", prefix: "SpecializedSharpMeasuresScalar(", postfix: ")");
    private static SourceSubtext VectorDefinition { get; } = SourceSubtext.Typeof("UnitOfLength", prefix: "SharpMeasuresVector(", postfix: ")");
    private static SourceSubtext SpecializedVectorDefinition { get; } = SourceSubtext.Typeof("Displacement2", prefix: "SpecializedSharpMeasuresVector(", postfix: ")");
    private static SourceSubtext VectorGroupDefinition { get; } = SourceSubtext.Typeof("UnitOfLength", prefix: "SharpMeasuresVectorGroup(", postfix: ")");

    private static GeneratorVerifier AssertExactDiagnostics(string source, IEnumerable<string> expectedDiagnostics) => GeneratorVerifier.Construct<SharpMeasuresGenerator>(source).AssertExactlyListedDiagnosticsIDsReported(expectedDiagnostics);
    private static GeneratorVerifier AssertExactlyTypeAlreadyDefinedDiagnostics(string source) => AssertExactDiagnostics(source, TypeAlreadyDefinedDiagnostics);
    private static IReadOnlyCollection<string> TypeAlreadyDefinedDiagnostics { get; } = new string[] { DiagnosticIDs.TypeAlreadyDefined };

    private static string ScalarText(SourceSubtext otherDefinition) => $$"""
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        [{{otherDefinition}}]
        public partial class Distance { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }

        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static TextSpan ScalarLocation(SourceSubtext otherDefinition) => ExpectedDiagnosticsLocation.TextSpan(ScalarText(otherDefinition), "SharpMeasuresScalar", postfix: "(typeof(UnitOfLength))]");

    private static GeneratorVerifier AssertScalar(SourceSubtext otherDefinition)
    {
        var source = ScalarText(otherDefinition);
        var expectedLocation = ScalarLocation(otherDefinition);

        return AssertExactlyTypeAlreadyDefinedDiagnostics(source).AssertDiagnosticsLocation(expectedLocation, source);
    }

    private static Task AssertAndVerifyScalar(SourceSubtext otherDefinition) => AssertScalar(otherDefinition).VerifyDiagnostics();

    private static string SpecializedScalarText(SourceSubtext otherDefinition) => $$"""
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Vectors;

        [SpecializedSharpMeasuresScalar(typeof(Length))]
        [{{otherDefinition}}]
        public partial class Distance { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }

        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static TextSpan SpecializedScalarLocation(SourceSubtext otherDefinition) => ExpectedDiagnosticsLocation.TextSpan(SpecializedScalarText(otherDefinition), "SpecializedSharpMeasuresScalar", postfix: "(typeof(Length))]");

    private static GeneratorVerifier AssertSpecializedScalar(SourceSubtext otherDefinition)
    {
        var source = SpecializedScalarText(otherDefinition);
        var expectedLocation = SpecializedScalarLocation(otherDefinition);

        return AssertExactlyTypeAlreadyDefinedDiagnostics(source).AssertDiagnosticsLocation(expectedLocation, source);
    }

    private static Task AssertAndVerifySpecializedScalar(SourceSubtext otherDefinition) => AssertSpecializedScalar(otherDefinition).VerifyDiagnostics();

    private static string VectorText(SourceSubtext otherDefinition) => $$"""
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Vectors;

        [SharpMeasuresVector(typeof(UnitOfLength))]
        [{{otherDefinition}}]
        public partial class Position2 { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }

        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static TextSpan VectorLocation(SourceSubtext otherDefinition) => ExpectedDiagnosticsLocation.TextSpan(VectorText(otherDefinition), "SharpMeasuresVector", postfix: "(typeof(UnitOfLength))");

    private static GeneratorVerifier AssertVector(SourceSubtext otherDefinition)
    {
        var source = VectorText(otherDefinition);
        var expectedLocation = VectorLocation(otherDefinition);

        return AssertExactlyTypeAlreadyDefinedDiagnostics(source).AssertDiagnosticsLocation(expectedLocation, source);
    }

    private static Task AssertAndVerifyVector(SourceSubtext otherDefinition) => AssertVector(otherDefinition).VerifyDiagnostics();

    private static string SpecializedVectorText(SourceSubtext otherDefinition) => $$"""
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Vectors;

        [SpecializedSharpMeasuresVector(typeof(UnitOfLength))]
        [{{otherDefinition}}]
        public partial class Displacement2 { }

        [SharpMeasuresVector(typeof(UnitOfLength))]
        public partial class Position2 { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }

        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static TextSpan SpecializedVectorLocation(SourceSubtext otherDefinition) => ExpectedDiagnosticsLocation.TextSpan(SpecializedVectorText(otherDefinition), "SpecializedSharpMeasuresVector", postfix: "(typeof(UnitOfLength))");

    private static GeneratorVerifier AssertSpecializedVector(SourceSubtext otherDefinition)
    {
        var source = SpecializedVectorText(otherDefinition);
        var expectedLocation = SpecializedVectorLocation(otherDefinition);

        return AssertExactlyTypeAlreadyDefinedDiagnostics(source).AssertDiagnosticsLocation(expectedLocation, source);
    }

    private static Task AssertAndVerifySpecializedVector(SourceSubtext otherDefinition) => AssertSpecializedVector(otherDefinition).VerifyDiagnostics();

    private static string SpecializedVectorGroupText(SourceSubtext otherDefinition) => $$"""
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Vectors;

        [SpecializedSharpMeasuresVectorGroup(typeof(Position))]
        [{{otherDefinition}}]
        public static partial class Displacement { }

        [SharpMeasuresVectorGroup(typeof(UnitOfLength))]
        public static partial class Position { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }

        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static TextSpan SpecializedVectorGroupLocation(SourceSubtext otherDefinition) => ExpectedDiagnosticsLocation.TextSpan(SpecializedVectorGroupText(otherDefinition), "SpecializedSharpMeasuresVectorGroup", postfix: "(typeof(Position))");

    private static GeneratorVerifier AssertSpecializedVectorGroup(SourceSubtext otherDefinition)
    {
        var source = SpecializedVectorGroupText(otherDefinition);
        var expectedLocation = SpecializedVectorGroupLocation(otherDefinition);

        return AssertExactlyTypeAlreadyDefinedDiagnostics(source).AssertDiagnosticsLocation(expectedLocation, source);
    }

    private static Task AssertAndVerifySpecializedVectorGroup(SourceSubtext otherDefinition) => AssertSpecializedVectorGroup(otherDefinition).VerifyDiagnostics();

    private static string VectorGroupMemberText(SourceSubtext otherDefinition) => $$"""
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Vectors;

        [SharpMeasuresVectorGroupMember(typeof(Position))]
        [{{otherDefinition}}]
        public partial class Position2 { }

        [SharpMeasuresVector(typeof(UnitOfLength))]
        public partial class Displacement2 { }

        [SharpMeasuresVectorGroup(typeof(UnitOfLength))]
        public static partial class Position { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }

        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static TextSpan VectorGroupMemberLocation(SourceSubtext otherDefinition) => ExpectedDiagnosticsLocation.TextSpan(VectorGroupMemberText(otherDefinition), "SharpMeasuresVectorGroupMember", postfix: "(typeof(Position))");

    private static GeneratorVerifier AssertVectorGroupMember(SourceSubtext otherDefinition)
    {
        var source = VectorGroupMemberText(otherDefinition);
        var expectedLocation = VectorGroupMemberLocation(otherDefinition);

        return AssertExactlyTypeAlreadyDefinedDiagnostics(source).AssertDiagnosticsLocation(expectedLocation, source);
    }

    private static Task AssertAndVerifyVectorGroupMember(SourceSubtext otherDefinition) => AssertVectorGroupMember(otherDefinition).VerifyDiagnostics();
}
