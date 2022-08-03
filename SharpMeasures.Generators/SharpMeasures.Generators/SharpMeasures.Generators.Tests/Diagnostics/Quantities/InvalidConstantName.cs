namespace SharpMeasures.Generators.Tests.Diagnostics.Quantities;

using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Tests.Verify;

using System.Collections.Generic;
using System.Threading.Tasks;

using VerifyXunit;

using Xunit;

[UsesVerify]
public class InvalidConstantName
{
    [Fact]
    public Task VerifyInvalidConstantNameDiagnosticsMessage_Null()
    {
        string source = ScalarText("null");

        return AssertExactlyInvalidConstantNameDiagnosticsWithValidLocation(source).VerifyDiagnostics();
    }

    [Fact]
    public Task VerifyInvalidConstantNameDiagnosticsMessage_Emptyl()
    {
        string source = ScalarText("\"\"");

        return AssertExactlyInvalidConstantNameDiagnosticsWithValidLocation(source).VerifyDiagnostics();
    }

    [Theory]
    [MemberData(nameof(InvalidConstantNames))]
    public void Scalar_ExactList(string constantName)
    {
        string source = ScalarText(constantName);

        AssertExactlyInvalidConstantNameDiagnosticsWithValidLocation(source);
    }

    [Theory]
    [MemberData(nameof(InvalidConstantNames))]
    public void SpecializedScalar_ExactList(string constantName)
    {
        string source = SpecializedScalarText(constantName);

        AssertExactlyInvalidConstantNameDiagnosticsWithValidLocation(source);
    }

    [Theory]
    [MemberData(nameof(InvalidConstantNames))]
    public void Vector_ExactList(string constantName)
    {
        string source = VectorText(constantName);

        AssertExactlyInvalidConstantNameDiagnosticsWithValidLocation(source);
    }

    [Theory]
    [MemberData(nameof(InvalidConstantNames))]
    public void SpecializedVector_ExactList(string constantName)
    {
        string source = SpecializedVectorText(constantName);

        AssertExactlyInvalidConstantNameDiagnosticsWithValidLocation(source);
    }

    [Theory]
    [MemberData(nameof(InvalidConstantNames))]
    public void VectorGroupMember_ExactList(string constantName)
    {
        string source = VectorGroupMemberText(constantName);

        AssertExactlyInvalidConstantNameDiagnosticsWithValidLocation(source);
    }

    private static IEnumerable<object[]> InvalidConstantNames() => new object[][]
    {
        new[] { "null" },
        new[] { "\"\"" }
    };

    private static GeneratorVerifier AssertExactlyInvalidConstantNameDiagnosticsWithValidLocation(string source) => GeneratorVerifier.Construct<SharpMeasuresGenerator>(source).AssertExactlyListedDiagnosticsIDsReported(InvalidConstantNameDiagnostics).AssertAllDiagnosticsValidLocation();
    private static IReadOnlyCollection<string> InvalidConstantNameDiagnostics { get; } = new string[] { DiagnosticIDs.InvalidConstantName };

    private static string ScalarText(string name) => $$"""
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;

        [ScalarConstant({{name}}, "Metre", 1.616255E-35)]
        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }
            
        [FixedUnit("Metre", "Metres")]
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static string SpecializedScalarText(string name) => $$"""
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;

        [ScalarConstant({{name}}, "Metre", 1.616255E-35)]
        [SpecializedSharpMeasuresScalar(typeof(Length))]
        public partial class Distance { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }

        [FixedUnit("Metre", "Metres")]
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static string VectorText(string name) => $$"""
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Vectors;

        [VectorConstant({{name}}, "Metre", 1, 1, 1)]
        [SharpMeasuresVector(typeof(UnitOfLength))]
        public partial class Position3 { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }

        [FixedUnit("Metre", "Metres")]
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static string SpecializedVectorText(string name) => $$"""
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Vectors;

        [VectorConstant({{name}}, "Metre", 1, 1, 1)]
        [SpecializedSharpMeasuresVector(typeof(Position3))]
        public partial class Displacement3 { }

        [SharpMeasuresVector(typeof(UnitOfLength))]
        public partial class Position3 { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }

        [FixedUnit("Metre", "Metres")]
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static string VectorGroupMemberText(string name) => $$"""
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Vectors;

        [VectorConstant({{name}}, "Metre", 1, 1, 1)]
        [SharpMeasuresVectorGroupMember(typeof(Position))]
        public partial class Position3 { }

        [SharpMeasuresVectorGroup(typeof(UnitOfLength))]
        public static partial class Position { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }

        [FixedUnit("Metre", "Metres")]
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;
}
