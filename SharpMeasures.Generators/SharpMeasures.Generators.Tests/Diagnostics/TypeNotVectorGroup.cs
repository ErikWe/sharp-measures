namespace SharpMeasures.Generators.Tests.Diagnostics;

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
    public Task VerifyTypeNotVectorGroupDiagnosticsMessage_Null() => AssertVectorGroupMemberVector(NullType).VerifyDiagnostics();

    [Fact]
    public Task VerifyTypeNotVectorGroupDiagnosticsMessage_Int() => AssertVectorGroupMemberVector(IntType).VerifyDiagnostics();

    [Theory]
    [MemberData(nameof(NonVectorGroupTypes))]
    public void VectorGroupMemberVector(SourceSubtext vectorGroupType) => AssertVectorGroupMemberVector(vectorGroupType);

    [Theory]
    [MemberData(nameof(NonVectorGroupTypes))]
    public void VectorGroupDifference(SourceSubtext differenceVectorGroupType) => AssertVectorGroupDifference(differenceVectorGroupType);

    [Theory]
    [MemberData(nameof(NonVectorGroupTypes))]
    public void SpecializedVectorGroupOriginalVectorGroup(SourceSubtext originalVectorGroupType) => AssertSpecializedVectorGroupOriginalVectorGroup(originalVectorGroupType);

    public static IEnumerable<object[]> NonVectorGroupTypes() => new object[][]
    {
        new object[] { NullType },
        new object[] { IntType },
        new object[] { UnitOfLengthType },
        new object[] { LengthType },
        new object[] { Length3Type },
        new object[] { Offset3Type },
        new object[] { Displacement3Type }
    };

    private static SourceSubtext NullType { get; } = SourceSubtext.Covered("null", prefix: "(System.Type)");
    private static SourceSubtext IntType { get; } = SourceSubtext.AsTypeof("int");
    private static SourceSubtext UnitOfLengthType { get; } = SourceSubtext.AsTypeof("UnitOfLength");
    private static SourceSubtext LengthType { get; } = SourceSubtext.AsTypeof("Length");
    private static SourceSubtext Length3Type { get; } = SourceSubtext.AsTypeof("Length3");
    private static SourceSubtext Offset3Type { get; } = SourceSubtext.AsTypeof("Offset3");
    private static SourceSubtext Displacement3Type { get; } = SourceSubtext.AsTypeof("Displacement3");

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

    private static GeneratorVerifier AssertVectorGroupMemberVector(SourceSubtext vectorGroupType)
    {
        var source = VectorGroupMemberVectorText(vectorGroupType);
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(source, vectorGroupType.Context.With(outerPrefix: "SharpMeasuresVectorGroupMember("));

        return AssertExactlyTypeNotVectorGroupDiagnostics(source).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(VectorGroupMemberVectorIdentical);
    }

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

    private static GeneratorVerifier AssertVectorGroupDifference(SourceSubtext differenceVectorGroupType)
    {
        var source = VectorGroupDifferenceText(differenceVectorGroupType);
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(source, differenceVectorGroupType.Context.With(outerPrefix: "Difference = "));

        return AssertExactlyTypeNotVectorGroupDiagnostics(source).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(VectorGroupDifferenceIdentical);
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

    private static GeneratorVerifier AssertSpecializedVectorGroupOriginalVectorGroup(SourceSubtext originalVectorGroupType)
    {
        var source = SpecializedVectorGroupOriginalVectorGroupText(originalVectorGroupType);
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(source, originalVectorGroupType.Context.With(outerPrefix: "SpecializedSharpMeasuresVectorGroup("));

        return AssertExactlyTypeNotVectorGroupDiagnostics(source).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(SpecializedVectorGroupOriginalVectorGroupIdentical);
    }

    private static GeneratorVerifier VectorGroupMemberVectorIdentical => GeneratorVerifier.Construct<SharpMeasuresGenerator>(VectorGroupMemberVectorIdenticalText);
    private static GeneratorVerifier VectorGroupDifferenceIdentical => GeneratorVerifier.Construct<SharpMeasuresGenerator>(VectorGroupDifferenceIdenticalText);
    private static GeneratorVerifier SpecializedVectorGroupOriginalVectorGroupIdentical => GeneratorVerifier.Construct<SharpMeasuresGenerator>(SpecializedVectorGroupOriginalVectorGroupIdenticalText);

    private static string VectorGroupMemberVectorIdenticalText => """
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Vectors;

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

    private static string VectorGroupDifferenceIdenticalText => """
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Vectors;

        [SharpMeasuresVectorGroup(typeof(UnitOfLength))]
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

    private static string SpecializedVectorGroupOriginalVectorGroupIdenticalText => """
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Vectors;

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
}
