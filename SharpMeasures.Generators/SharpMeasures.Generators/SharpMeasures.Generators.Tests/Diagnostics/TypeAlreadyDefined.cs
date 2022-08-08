namespace SharpMeasures.Generators.Tests.Diagnostics;

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
    public Task Scalar_DefinedAsUnit() => AssertScalar(UnitDefinition).VerifyDiagnostics();

    [Fact]
    public Task SpecializedScalar_DefinedAsUnit() => AssertSpecializedScalar(UnitDefinition).VerifyDiagnostics();

    [Fact]
    public Task SpecializedScalar_DefinedAsScalar() => AssertSpecializedScalar(ScalarDefinition).VerifyDiagnostics();

    [Fact]
    public Task Vector_DefinedAsUnit() => AssertVector(UnitDefinition).VerifyDiagnostics();

    [Fact]
    public Task Vector_DefinedAsScalar() => AssertVector(ScalarDefinition).VerifyDiagnostics();

    [Fact]
    public Task Vector_DefinedAsSpecializedScalar() => AssertVector(SpecializedScalarDefinition).VerifyDiagnostics();

    [Fact]
    public Task SpecializedVector_DefinedAsUnit() => AssertSpecializedVector(UnitDefinition).VerifyDiagnostics();

    [Fact]
    public Task SpecializedVector_DefinedAsScalar() => AssertSpecializedVector(ScalarDefinition).VerifyDiagnostics();

    [Fact]
    public Task SpecializedVector_DefinedAsSpecializedScalar() => AssertSpecializedVector(SpecializedScalarDefinition).VerifyDiagnostics();

    [Fact]
    public Task SpecializedVector_DefinedAsVector() => AssertSpecializedVector(VectorDefinition).VerifyDiagnostics();

    [Fact]
    public Task SpecializedVectorGroup_DefinedAsVectorGroup() => AssertSpecializedVectorGroup(VectorGroupDefinition).VerifyDiagnostics();

    [Fact]
    public Task VectorGroupMember_DefinedAsUnit() => AssertVectorGroupMember(UnitDefinition).VerifyDiagnostics();

    [Fact]
    public Task VectorGroupMember_DefinedAsScalar() => AssertVectorGroupMember(ScalarDefinition).VerifyDiagnostics();

    [Fact]
    public Task VectorGroupMember_DefinedAsSpecializedScalar() => AssertVectorGroupMember(SpecializedScalarDefinition).VerifyDiagnostics();

    [Fact]
    public Task VectorGroupMember_DefinedAsVector() => AssertVectorGroupMember(VectorDefinition).VerifyDiagnostics();

    [Fact]
    public Task VectorGroupMember_DefinedAsSpecializedVector() => AssertVectorGroupMember(SpecializedVectorDefinition).VerifyDiagnostics();

    private static SourceSubtext UnitDefinition { get; } = SourceSubtext.Covered("SharpMeasuresUnit", postfix: "(typeof(Length))");
    private static SourceSubtext ScalarDefinition { get; } = SourceSubtext.Covered("SharpMeasuresScalar", postfix: "(typeof(UnitOfLength))");
    private static SourceSubtext SpecializedScalarDefinition { get; } = SourceSubtext.Covered("SpecializedSharpMeasuresScalar", postfix: "(typeof(Length))");
    private static SourceSubtext VectorDefinition { get; } = SourceSubtext.Covered("SharpMeasuresVector", postfix: "(typeof(UnitOfLength))");
    private static SourceSubtext SpecializedVectorDefinition { get; } = SourceSubtext.Covered("SpecializedSharpMeasuresVector", postfix: "(typeof(Displacement2))");
    private static SourceSubtext VectorGroupDefinition { get; } = SourceSubtext.Covered("SharpMeasuresVectorGroup", postfix: "(typeof(UnitOfLength))");

    private static GeneratorVerifier AssertExactDiagnostics(string source, IEnumerable<string> expectedDiagnostics) => GeneratorVerifier.Construct<SharpMeasuresGenerator>(source).AssertExactlyListedDiagnosticsIDsReported(expectedDiagnostics);
    private static GeneratorVerifier AssertExactlyTypeAlreadyDefinedDiagnostics(string source) => AssertExactDiagnostics(source, TypeAlreadyDefinedDiagnostics);
    private static IReadOnlyCollection<string> TypeAlreadyDefinedDiagnostics { get; } = new string[] { DiagnosticIDs.TypeAlreadyDefined };

    private static string ScalarText(SourceSubtext otherDefinition) => $$"""
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;

        [SharpMeasuresScalar(typeof(UnitOfLength))] // <-
        [{{otherDefinition}}]
        public partial class Distance { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }

        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertScalar(SourceSubtext otherDefinition)
    {
        var source = ScalarText(otherDefinition);
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(source, target: "SharpMeasuresScalar", postfix: "(typeof(UnitOfLength))] // <-");

        return AssertExactlyTypeAlreadyDefinedDiagnostics(source).AssertDiagnosticsLocation(expectedLocation, source);
    }

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

    private static GeneratorVerifier AssertSpecializedScalar(SourceSubtext otherDefinition)
    {
        var source = SpecializedScalarText(otherDefinition);
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(source, target: "SpecializedSharpMeasuresScalar");

        return AssertExactlyTypeAlreadyDefinedDiagnostics(source).AssertDiagnosticsLocation(expectedLocation, source);
    }

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

    private static GeneratorVerifier AssertVector(SourceSubtext otherDefinition)
    {
        var source = VectorText(otherDefinition);
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(source, target: "SharpMeasuresVector");

        return AssertExactlyTypeAlreadyDefinedDiagnostics(source).AssertDiagnosticsLocation(expectedLocation, source);
    }

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

    private static GeneratorVerifier AssertSpecializedVector(SourceSubtext otherDefinition)
    {
        var source = SpecializedVectorText(otherDefinition);
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(source, target: "SpecializedSharpMeasuresVector");

        return AssertExactlyTypeAlreadyDefinedDiagnostics(source).AssertDiagnosticsLocation(expectedLocation, source);
    }

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

    private static GeneratorVerifier AssertSpecializedVectorGroup(SourceSubtext otherDefinition)
    {
        var source = SpecializedVectorGroupText(otherDefinition);
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(source, target: "SpecializedSharpMeasuresVectorGroup");

        return AssertExactlyTypeAlreadyDefinedDiagnostics(source).AssertDiagnosticsLocation(expectedLocation, source);
    }

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

    private static GeneratorVerifier AssertVectorGroupMember(SourceSubtext otherDefinition)
    {
        var source = VectorGroupMemberText(otherDefinition);
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(source, target: "SharpMeasuresVectorGroupMember");

        return AssertExactlyTypeAlreadyDefinedDiagnostics(source).AssertDiagnosticsLocation(expectedLocation, source);
    }
}
