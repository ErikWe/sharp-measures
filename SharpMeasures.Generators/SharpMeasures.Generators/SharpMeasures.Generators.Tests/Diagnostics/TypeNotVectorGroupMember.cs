namespace SharpMeasures.Generators.Tests.Diagnostics;

using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Tests.Verify;

using System.Collections.Generic;
using System.Threading.Tasks;

using VerifyXunit;

using Xunit;

[UsesVerify]
public class TypeNotVectorGroupMember
{
    [Fact]
    public Task VerifyTypeNotVectorGroupMemberDiagnosticsMessage_Null()
    {
        var source = RegisterVectorGroupMemberText("null, Dimension = 2");

        return AssertExactlyTypeNotVectorGroupMemberDiagnostics(source).VerifyDiagnostics();
    }

    [Fact]
    public Task VerifyTypeNotVectorGroupDiagnosticsMessage_Int()
    {
        var source = RegisterVectorGroupMemberText("typeof(int), Dimension = 2");

        return AssertExactlyTypeNotVectorGroupMemberDiagnostics(source).VerifyDiagnostics();
    }

    [Theory]
    [MemberData(nameof(NonVectorGroupMemberTypes))]
    public void VectorGroupMemberVector_ExactList(string value)
    {
        var source = RegisterVectorGroupMemberText(value);

        AssertExactlyTypeNotVectorGroupMemberDiagnostics(source);
    }

    private static IEnumerable<object[]> NonVectorGroupMemberTypes() => new object[][]
    {
        new[] { "null, Dimension = 2" },
        new[] { "typeof(int), Dimension = 2" },
        new[] { "typeof(UnitOfLength), Dimension = 2" },
        new[] { "typeof(Length), Dimension = 2" },
        new[] { "typeof(Length3)" },
        new[] { "typeof(Offset3)" },
        new[] { "typeof(Displacement), Dimension = 2" }
    };

    private static GeneratorVerifier AssertExactlyTypeNotVectorGroupMemberDiagnostics(string source) => GeneratorVerifier.Construct<SharpMeasuresGenerator>(source).AssertExactlyListedDiagnosticsIDsReported(TypeNotVectorGroupMemberDiagnostics);
    private static IReadOnlyCollection<string> TypeNotVectorGroupMemberDiagnostics { get; } = new string[] { DiagnosticIDs.TypeNotVectorGroup };

    private static string RegisterVectorGroupMemberText(string value) => $$"""
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Vectors;

        [RegisterVectorGroupMember({{value}})]
        [SharpMeasuresVectorGroup(typeof(UnitOfLength))]
        public static partial class Position { }

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
