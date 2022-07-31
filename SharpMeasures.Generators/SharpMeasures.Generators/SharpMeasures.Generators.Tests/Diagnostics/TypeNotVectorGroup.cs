namespace SharpMeasures.Generators.Tests.Diagnostics;

using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Tests.Verify;

using System.Collections.Generic;
using System.Threading.Tasks;

using VerifyXunit;

using Xunit;

[UsesVerify]
public class TypeNotVectorGroup
{
    [Fact]
    public Task VerifyTypeNotVectorGroupDiagnosticsMessage_Null()
    {
        var source = VectorGroupMemberVectorText("null");

        return AssertExactlyTypeNotVectorGroupDiagnostics(source).VerifyDiagnostics();
    }

    [Fact]
    public Task VerifyTypeNotVectorGroupDiagnosticsMessage_Int()
    {
        var source = VectorGroupMemberVectorText("typeof(int)");

        return AssertExactlyTypeNotVectorGroupDiagnostics(source).VerifyDiagnostics();
    }

    [Theory]
    [MemberData(nameof(NonVectorGroupTypes))]
    public void VectorGroupMemberVector_ExactList(string value)
    {
        var source = VectorGroupMemberVectorText(value);

        AssertExactlyTypeNotVectorGroupDiagnostics(source);
    }

    [Theory]
    [MemberData(nameof(NonVectorGroupTypes))]
    public void VectorGroupDifference_ExactList(string value)
    {
        var source = VectorGroupDifferenceText(value);

        AssertExactlyTypeNotVectorGroupDiagnostics(source);
    }

    [Theory]
    [MemberData(nameof(NonVectorGroupTypes))]
    public void SpecializedVectorGroupOriginalVectorGroup_ExactList(string value)
    {
        var source = SpecializedVectorGroupOriginalVectorGroupText(value);

        AssertExactlyTypeNotVectorGroupDiagnostics(source);
    }

    private static IEnumerable<object[]> NonVectorGroupTypes() => new object[][]
    {
        new[] { "null" },
        new[] { "typeof(int)" },
        new[] { "typeof(UnitOfLength)" },
        new[] { "typeof(Length)" },
        new[] { "typeof(Length3)" },
        new[] { "typeof(Offset3)" },
        new[] { "typeof(Displacement3)" }
    };

    private static GeneratorVerifier AssertExactlyTypeNotVectorGroupDiagnostics(string source) => GeneratorVerifier.Construct<SharpMeasuresGenerator>(source).AssertExactlyListedDiagnosticsIDsReported(TypeNotVectorGroupDiagnostics);
    private static IReadOnlyCollection<string> TypeNotVectorGroupDiagnostics { get; } = new string[] { DiagnosticIDs.TypeNotVectorGroup };

    private static string VectorGroupMemberVectorText(string value) => $$"""
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Vectors;

        [SharpMeasuresVectorGroupMember({{value}})]
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

    private static string VectorGroupDifferenceText(string value) => $$"""
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Vectors;

        [SharpMeasuresVectorGroup(typeof(UnitOfLength), Difference = {{value}})]
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

    private static string SpecializedVectorGroupOriginalVectorGroupText(string value) => $$"""
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Vectors;

        [SpecializedSharpMeasuresVectorGroup({{value}})]
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
}
