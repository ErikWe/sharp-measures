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
public class TypeNotVectorGroup
{
    [Fact]
    public Task VerifyTypeNotVectorGroupDiagnosticsMessage_Null() => AssertAndVerifyVectorGroupMemberVector(NullSubtext);

    [Fact]
    public Task VerifyTypeNotVectorGroupDiagnosticsMessage_Int() => AssertAndVerifyVectorGroupMemberVector(IntSubtext);

    [Theory]
    [MemberData(nameof(NonVectorGroupTypes))]
    public void VectorGroupMemberVector(SourceSubtext vectorGroupType) => AssertVectorGroupMemberVector(vectorGroupType);

    [Theory]
    [MemberData(nameof(NonVectorGroupTypes))]
    public void VectorGroupDifference(SourceSubtext differenceVectorGroupType) => AssertVectorGroupDifference(differenceVectorGroupType);

    [Theory]
    [MemberData(nameof(NonVectorGroupTypes))]
    public void SpecializedVectorGroupOriginalVectorGroup(SourceSubtext originalVectorGroupType) => AssertSpecializedVectorGroupOriginalVectorGroup(originalVectorGroupType);

    private static IEnumerable<object[]> NonVectorGroupTypes() => new object[][]
    {
        new object[] { NullSubtext },
        new object[] { IntSubtext },
        new object[] { UnitOfLengthSubtext },
        new object[] { LengthSubtext },
        new object[] { Length3Subtext },
        new object[] { Offset3Subtext },
        new object[] { Displacement3Subtext }
    };

    private static SourceSubtext NullSubtext { get; } = new("null");
    private static SourceSubtext IntSubtext { get; } = SourceSubtext.Typeof("int");
    private static SourceSubtext UnitOfLengthSubtext { get; } = SourceSubtext.Typeof("UnitOfLength");
    private static SourceSubtext LengthSubtext { get; } = SourceSubtext.Typeof("Length");
    private static SourceSubtext Length3Subtext { get; } = SourceSubtext.Typeof("Length3");
    private static SourceSubtext Offset3Subtext { get; } = SourceSubtext.Typeof("Offset3");
    private static SourceSubtext Displacement3Subtext { get; } = SourceSubtext.Typeof("Displacement3");

    private static GeneratorVerifier AssertExactlyTypeNotVectorGroupDiagnostics(string source) => GeneratorVerifier.Construct<SharpMeasuresGenerator>(source).AssertExactlyListedDiagnosticsIDsReported(TypeNotVectorGroupDiagnostics);
    private static IReadOnlyCollection<string> TypeNotVectorGroupDiagnostics { get; } = new string[] { DiagnosticIDs.TypeNotVectorGroup };

    private static string VectorGroupMemberVectorText(SourceSubtext vectorGroupType) => $$"""
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Vectors;

        [SharpMeasuresVectorGroupMember({{vectorGroupType}})]
        public partial class Position3 { }

        [SharpMeasuresVectorGroupMember(typeof(Displacement))]
        public partial class Displacement3 { }

        [SharpMeasuresVectorGroup(typeof(UnitOfLength))]
        public static partial class Displacement { }

        [SpecializedSharpMeasuresVector(typeof(Length3))]
        public partial class Offset3 { }

        [SharpMeasuresVector(typeof(UnitOfLength))]
        public partial class Length3 { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }

        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static TextSpan VectorGroupMemberVectorLocation(SourceSubtext vectorGroupType) => ExpectedDiagnosticsLocation.TextSpan(VectorGroupMemberVectorText(vectorGroupType), vectorGroupType, prefix: "SharpMeasuresVectorGroupMember(");

    private static GeneratorVerifier AssertVectorGroupMemberVector(SourceSubtext vectorGroupType)
    {
        var source = VectorGroupMemberVectorText(vectorGroupType);
        var expectedLocation = VectorGroupMemberVectorLocation(vectorGroupType);

        return AssertExactlyTypeNotVectorGroupDiagnostics(source).AssertDiagnosticsLocation(expectedLocation, source);
    }

    private static Task AssertAndVerifyVectorGroupMemberVector(SourceSubtext vectorGroupType) => AssertVectorGroupMemberVector(vectorGroupType).VerifyDiagnostics();

    private static string VectorGroupDifferenceText(SourceSubtext differenceVectorGroupType) => $$"""
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Vectors;

        [SharpMeasuresVectorGroup(typeof(UnitOfLength), Difference = {{differenceVectorGroupType}})]
        public static partial class Position { }

        [SharpMeasuresVectorGroupMember(typeof(Displacement))]
        public partial class Displacement3 { }

        [SharpMeasuresVectorGroup(typeof(UnitOfLength))]
        public static partial class Displacement { }

        [SpecializedSharpMeasuresVector(typeof(Length3))]
        public partial class Offset3 { }

        [SharpMeasuresVector(typeof(UnitOfLength))]
        public partial class Length3 { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }

        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static TextSpan VectorGroupDifferenceLocation(SourceSubtext differenceVectorGroupType) => ExpectedDiagnosticsLocation.TextSpan(VectorGroupDifferenceText(differenceVectorGroupType), differenceVectorGroupType, prefix: "Difference = ");

    private static GeneratorVerifier AssertVectorGroupDifference(SourceSubtext differenceVectorGroupType)
    {
        var source = VectorGroupDifferenceText(differenceVectorGroupType);
        var expectedLocation = VectorGroupDifferenceLocation(differenceVectorGroupType);

        return AssertExactlyTypeNotVectorGroupDiagnostics(source).AssertDiagnosticsLocation(expectedLocation, source);
    }

    private static string SpecializedVectorGroupOriginalVectorGroupText(SourceSubtext originalVectorGroupType) => $$"""
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Vectors;

        [SpecializedSharpMeasuresVectorGroup({{originalVectorGroupType}})]
        public static partial class Position { }

        [SharpMeasuresVectorGroupMember(typeof(Displacement))]
        public partial class Displacement3 { }

        [SharpMeasuresVectorGroup(typeof(UnitOfLength))]
        public static partial class Displacement { }

        [SpecializedSharpMeasuresVector(typeof(Length3))]
        public partial class Offset3 { }

        [SharpMeasuresVector(typeof(UnitOfLength))]
        public partial class Length3 { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }

        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static TextSpan specializedVectorGroupOriginalVectorGroupLocation(SourceSubtext originalVectorGroupType) => ExpectedDiagnosticsLocation.TextSpan(SpecializedVectorGroupOriginalVectorGroupText(originalVectorGroupType), originalVectorGroupType, prefix: "SpecializedSharpMeasuresVectorGroup(");

    private static GeneratorVerifier AssertSpecializedVectorGroupOriginalVectorGroup(SourceSubtext originalVectorGroupType)
    {
        var source = SpecializedVectorGroupOriginalVectorGroupText(originalVectorGroupType);
        var expectedLocation = specializedVectorGroupOriginalVectorGroupLocation(originalVectorGroupType);

        return AssertExactlyTypeNotVectorGroupDiagnostics(source).AssertDiagnosticsLocation(expectedLocation, source);
    }
}
