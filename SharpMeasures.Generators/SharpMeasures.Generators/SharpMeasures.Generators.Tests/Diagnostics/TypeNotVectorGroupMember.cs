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
public class TypeNotVectorGroupMember
{
    [Fact]
    public Task VerifyTypeNotVectorGroupMemberDiagnosticsMessage_Null() => AssertAndVerifyRegisterVectorGroupMember(NullSubtext);

    [Fact]
    public Task VerifyTypeNotVectorGroupDiagnosticsMessage_Int() => AssertAndVerifyRegisterVectorGroupMember(IntSubtext);

    [Theory]
    [MemberData(nameof(NonVectorGroupMemberTypes))]
    public void VectorGroupMemberVector(SourceSubtext vectorGroupMemberType) => AssertRegisterVectorGroupMember(vectorGroupMemberType);

    private static IEnumerable<object[]> NonVectorGroupMemberTypes() => new object[][]
    {
        new object[] { NullSubtext },
        new object[] { IntSubtext },
        new object[] { UnitOfLengthSubtext },
        new object[] { LengthSubtext },
        new object[] { Length3Subtext },
        new object[] { Offset3Subtext },
        new object[] { DisplacementSubtext }
    };

    private static SourceSubtext NullSubtext { get; } = new("null", postfix: ", Dimension = 2");
    private static SourceSubtext IntSubtext { get; } = SourceSubtext.Typeof("int", postfix: ", Dimension = 2");
    private static SourceSubtext UnitOfLengthSubtext { get; } = SourceSubtext.Typeof("UnitOfLength", postfix: ", Dimension = 2");
    private static SourceSubtext LengthSubtext { get; } = SourceSubtext.Typeof("Length", postfix: ", Dimension = 2");
    private static SourceSubtext Length3Subtext { get; } = SourceSubtext.Typeof("Length3");
    private static SourceSubtext Offset3Subtext { get; } = SourceSubtext.Typeof("Offset3");
    private static SourceSubtext DisplacementSubtext { get; } = SourceSubtext.Typeof("Displacement", postfix: ", Dimension = 2");

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

    private static TextSpan RegisterVectorGroupMemberLocation(SourceSubtext vectorGroupMemberType) => ExpectedDiagnosticsLocation.TextSpan(RegisterVectorGroupMemberText(vectorGroupMemberType), vectorGroupMemberType, prefix: "RegisterVectorGroupMember(");

    private static GeneratorVerifier AssertRegisterVectorGroupMember(SourceSubtext vectorGroupMemberType)
    {
        var source = RegisterVectorGroupMemberText(vectorGroupMemberType);
        var expectedLocation = RegisterVectorGroupMemberLocation(vectorGroupMemberType);

        return AssertExactlyTypeNotVectorGroupMemberDiagnostics(source).AssertDiagnosticsLocation(expectedLocation, source);
    }

    private static Task AssertAndVerifyRegisterVectorGroupMember(SourceSubtext vectorGroupMemberType) => AssertRegisterVectorGroupMember(vectorGroupMemberType).VerifyDiagnostics();
}
