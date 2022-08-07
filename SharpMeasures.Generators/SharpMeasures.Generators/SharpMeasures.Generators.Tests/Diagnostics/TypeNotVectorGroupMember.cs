namespace SharpMeasures.Generators.Tests.Diagnostics;

using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Tests.Utility;
using SharpMeasures.Generators.Tests.Verify;

using System.Collections.Generic;
using System.Threading.Tasks;

using VerifyXunit;

using Xunit;

[UsesVerify]
public class TypeNotVectorGroupMember
{
    [Fact]
    public Task VerifyTypeNotVectorGroupMemberDiagnosticsMessage_Null() => AssertAndVerifyRegisterVectorGroupMember(NullType);

    [Fact]
    public Task VerifyTypeNotVectorGroupDiagnosticsMessage_Int() => AssertAndVerifyRegisterVectorGroupMember(IntType);

    [Theory]
    [MemberData(nameof(NonVectorGroupMemberTypes))]
    public void VectorGroupMemberVector(SourceSubtext vectorGroupMemberType) => AssertRegisterVectorGroupMember(vectorGroupMemberType);

    private static IEnumerable<object[]> NonVectorGroupMemberTypes() => new object[][]
    {
        new object[] { NullType },
        new object[] { IntType },
        new object[] { UnitOfLengthType },
        new object[] { LengthType },
        new object[] { Length3Type },
        new object[] { Offset3Type },
        new object[] { DisplacementType }
    };

    private static SourceSubtext NullType { get; } = SourceSubtext.Covered("null", postfix: ", Dimension = 2");
    private static SourceSubtext IntType { get; } = SourceSubtext.AsTypeof("int", postfix: ", Dimension = 2");
    private static SourceSubtext UnitOfLengthType { get; } = SourceSubtext.AsTypeof("UnitOfLength", postfix: ", Dimension = 2");
    private static SourceSubtext LengthType { get; } = SourceSubtext.AsTypeof("Length", postfix: ", Dimension = 2");
    private static SourceSubtext Length3Type { get; } = SourceSubtext.AsTypeof("Length3");
    private static SourceSubtext Offset3Type { get; } = SourceSubtext.AsTypeof("Offset3");
    private static SourceSubtext DisplacementType { get; } = SourceSubtext.AsTypeof("Displacement", postfix: ", Dimension = 2");

    private static GeneratorVerifier AssertExactlyTypeNotVectorGroupMemberDiagnostics(string source) => GeneratorVerifier.Construct<SharpMeasuresGenerator>(source).AssertExactlyListedDiagnosticsIDsReported(TypeNotVectorGroupMemberDiagnostics);
    private static IReadOnlyCollection<string> TypeNotVectorGroupMemberDiagnostics { get; } = new string[] { DiagnosticIDs.TypeNotVectorGroupMember };

    private static string RegisterVectorGroupMemberText(SourceSubtext vectorGroupMemberType) => $$"""
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Vectors;

        [RegisterVectorGroupMember({{vectorGroupMemberType}})]
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

    private static GeneratorVerifier AssertRegisterVectorGroupMember(SourceSubtext vectorGroupMemberType)
    {
        var source = RegisterVectorGroupMemberText(vectorGroupMemberType);
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(source, vectorGroupMemberType.Context.With(outerPrefix: "RegisterVectorGroupMember("));

        return AssertExactlyTypeNotVectorGroupMemberDiagnostics(source).AssertDiagnosticsLocation(expectedLocation, source);
    }

    private static Task AssertAndVerifyRegisterVectorGroupMember(SourceSubtext vectorGroupMemberType) => AssertRegisterVectorGroupMember(vectorGroupMemberType).VerifyDiagnostics();
}
