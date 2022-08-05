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
public class TypeNotScalar
{
    [Fact]
    public Task VerifyTypeNotScalarDiagnosticsMessage_Null()
    {
        var source = UnitQuantityText("null");
        var expectedLocation = UnitQuantityLocation("null");

        return AssertExactlyTypeNotScalarDiagnosticsWithValidLocation(source).AssertDiagnosticsLocation(expectedLocation).VerifyDiagnostics();
    }

    [Fact]
    public Task VerifyTypeNotScalarDiagnosticsMessage_Int()
    {
        var source = UnitQuantityText("typeof(int)");
        var expectedLocation = UnitQuantityLocation("typeof(int)");

        return AssertExactlyTypeNotScalarDiagnosticsWithValidLocation(source).AssertDiagnosticsLocation(expectedLocation).VerifyDiagnostics();
    }

    [Theory]
    [MemberData(nameof(NonScalarTypes))]
    public void UnitQuantity(string value)
    {
        var source = UnitQuantityText(value);
        var expectedLocation = UnitQuantityLocation(value);

        AssertExactlyTypeNotScalarDiagnosticsWithValidLocation(source).AssertDiagnosticsLocation(expectedLocation);
    }

    [Theory]
    [MemberData(nameof(NonScalarTypes))]
    public void ScalarReciprocal(string value)
    {
        var source = ScalarArgumentText("Reciprocal", value);
        var expectedLocation = ScalarArgumentLocation("Reciprocal", value);

        AssertExactlyTypeNotScalarDiagnosticsWithValidLocation(source).AssertDiagnosticsLocation(expectedLocation);
    }

    [Theory]
    [MemberData(nameof(NonScalarTypes))]
    public void ScalarSquare(string value)
    {
        var source = ScalarArgumentText("Square", value);
        var expectedLocation = ScalarArgumentLocation("Square", value);
        
        AssertExactlyTypeNotScalarDiagnosticsWithValidLocation(source).AssertDiagnosticsLocation(expectedLocation);
    }

    [Theory]
    [MemberData(nameof(NonScalarTypes))]
    public void ScalarCube(string value)
    {
        var source = ScalarArgumentText("Cube", value);
        var expectedLocation = ScalarArgumentLocation("Cube", value);

        AssertExactlyTypeNotScalarDiagnosticsWithValidLocation(source).AssertDiagnosticsLocation(expectedLocation);
    }

    [Theory]
    [MemberData(nameof(NonScalarTypes))]
    public void ScalarSquareRoot(string value)
    {
        var source = ScalarArgumentText("SquareRoot", value);
        var expectedLocation = ScalarArgumentLocation("SquareRoot", value);

        AssertExactlyTypeNotScalarDiagnosticsWithValidLocation(source).AssertDiagnosticsLocation(expectedLocation);
    }

    [Theory]
    [MemberData(nameof(NonScalarTypes))]
    public void ScalarCubeRoot(string value)
    {
        var source = ScalarArgumentText("CubeRoot", value);
        var expectedLocation = ScalarArgumentLocation("CubeRoot", value);

        AssertExactlyTypeNotScalarDiagnosticsWithValidLocation(source).AssertDiagnosticsLocation(expectedLocation);
    }

    [Theory]
    [MemberData(nameof(NonScalarTypes))]
    public void ScalarDifference(string value)
    {
        var source = ScalarArgumentText("Difference", value);
        var expectedLocation = ScalarArgumentLocation("Difference", value);

        AssertExactlyTypeNotScalarDiagnosticsWithValidLocation(source).AssertDiagnosticsLocation(expectedLocation);
    }

    [Theory]
    [MemberData(nameof(NonScalarTypes))]
    public void SpecializedScalarOriginalScalar(string value)
    {
        var source = SpecializedScalarOriginalScalarText(value);
        var expectedLocation = SpecializedScalarOriginalScalarLocation(value);

        AssertExactlyTypeNotScalarDiagnosticsWithValidLocation(source).AssertDiagnosticsLocation(expectedLocation);
    }

    [Theory]
    [MemberData(nameof(NonScalarTypes))]
    public void SpecializedScalarReciprocal(string value)
    {
        var source = SpecializedScalarArgumentText("Reciprocal", value);
        var expectedLocation = SpecializedScalarArgumentLocation("Reciprocal", value);

        AssertExactlyTypeNotScalarDiagnosticsWithValidLocation(source).AssertDiagnosticsLocation(expectedLocation);
    }

    [Theory]
    [MemberData(nameof(NonScalarTypes))]
    public void SpecializedScalarSquare(string value)
    {
        var source = SpecializedScalarArgumentText("Square", value);
        var expectedLocation = SpecializedScalarArgumentLocation("Square", value);

        AssertExactlyTypeNotScalarDiagnosticsWithValidLocation(source).AssertDiagnosticsLocation(expectedLocation);
    }

    [Theory]
    [MemberData(nameof(NonScalarTypes))]
    public void SpecializedScalarCube_ExactList(string value)
    {
        var source = SpecializedScalarArgumentText("Cube", value);
        var expectedLocation = SpecializedScalarArgumentLocation("Cube", value);

        AssertExactlyTypeNotScalarDiagnosticsWithValidLocation(source).AssertDiagnosticsLocation(expectedLocation);
    }

    [Theory]
    [MemberData(nameof(NonScalarTypes))]
    public void SpecializedScalarSquareRoot_ExactList(string value)
    {
        var source = SpecializedScalarArgumentText("SquareRoot", value);
        var expectedLocation = SpecializedScalarArgumentLocation("SquareRoot", value);

        AssertExactlyTypeNotScalarDiagnosticsWithValidLocation(source).AssertDiagnosticsLocation(expectedLocation);
    }

    [Theory]
    [MemberData(nameof(NonScalarTypes))]
    public void SpecializedScalarCubeRoot_ExactList(string value)
    {
        var source = SpecializedScalarArgumentText("CubeRoot", value);
        var expectedLocation = SpecializedScalarArgumentLocation("CubeRoot", value);

        AssertExactlyTypeNotScalarDiagnosticsWithValidLocation(source).AssertDiagnosticsLocation(expectedLocation);
    }

    [Theory]
    [MemberData(nameof(NonScalarTypes))]
    public void SpecializedScalarDifference_ExactList(string value)
    {
        var source = SpecializedScalarArgumentText("Difference", value);
        var expectedLocation = SpecializedScalarArgumentLocation("Difference", value);

        AssertExactlyTypeNotScalarDiagnosticsWithValidLocation(source).AssertDiagnosticsLocation(expectedLocation);
    }

    [Theory]
    [MemberData(nameof(NonScalarTypes))]
    public void VectorScalar_ExactList(string value)
    {
        var source = VectorScalarText(value);
        var expectedLocation = VectorScalarLocation(value);

        AssertExactlyTypeNotScalarDiagnosticsWithValidLocation(source).AssertDiagnosticsLocation(expectedLocation);
    }

    [Theory]
    [MemberData(nameof(NonScalarTypes))]
    public void SpecializedVectorScalar_ExactList(string value)
    {
        var source = SpecializedVectorScalarText(value);
        var expectedLocation = SpecializedVectorScalarLocation(value);

        AssertExactlyTypeNotScalarDiagnosticsWithValidLocation(source).AssertDiagnosticsLocation(expectedLocation);
    }

    [Theory]
    [MemberData(nameof(NonScalarTypes))]
    public void VectorGroupScalar_ExactList(string value)
    {
        var source = VectorGroupScalarText(value);
        var expectedLocation = VectorGrouoScalarLocation(value);

        AssertExactlyTypeNotScalarDiagnosticsWithValidLocation(source).AssertDiagnosticsLocation(expectedLocation);
    }

    [Theory]
    [MemberData(nameof(NonScalarTypes))]
    public void SpecializedVectorGroupScalar_ExactList(string value)
    {
        var source = SpecializedVectorGroupScalarText(value);
        var expectedLocation = SpecializedVectorGrouoScalarLocation(value);

        AssertExactlyTypeNotScalarDiagnosticsWithValidLocation(source).AssertDiagnosticsLocation(expectedLocation);
    }

    [Theory]
    [MemberData(nameof(NonScalarTypes))]
    public void ConvertibleQuantity_ExactList(string value)
    {
        if (value is "null")
        {
            value = "(System.Type)null";
        }

        var source = ConvertibleQuantityText(value);
        var expectedLocation = ConvertibleQuantityLocation(value);

        AssertExactlyTypeNotScalarDiagnosticsWithValidLocation(source).AssertDiagnosticsLocation(expectedLocation);
    }

    private static IEnumerable<object[]> NonScalarTypes() => new object[][]
    {
        new[] { "null" },
        new[] { "typeof(int)" },
        new[] { "typeof(UnitOfLength)" },
        new[] { "typeof(Position3)" },
        new[] { "typeof(Displacement3)" },
        new[] { "typeof(Position)" },
        new[] { "typeof(Displacement)" },
        new[] { "typeof(Position2)" }
    };

    private static GeneratorVerifier AssertExactlyTypeNotScalarDiagnosticsWithValidLocation(string source) => GeneratorVerifier.Construct<SharpMeasuresGenerator>(source).AssertExactlyListedDiagnosticsIDsReported(TypeNotScalarDiagnostics).AssertAllDiagnosticsValidLocation();
    private static IReadOnlyCollection<string> TypeNotScalarDiagnostics { get; } = new string[] { DiagnosticIDs.TypeNotScalar };

    private static string UnitQuantityText(string value) => $$"""
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Vectors;

        [SharpMeasuresUnit({{value}})]
        public partial class UnitOfDistance { }

        [SpecializedSharpMeasuresVectorGroup(typeof(Position))]
        public static partial class Displacement { }

        [SharpMeasuresVectorGroupMember(typeof(Position))]
        public partial class Position2 { }

        [SharpMeasuresVectorGroup(typeof(UnitOfLength))]
        public static partial class Position { }

        [SpecializedSharpMeasuresVector(typeof(Position3))]
        public partial class Displacement3 { }

        [SharpMeasuresVector(typeof(UnitOfLength))]
        public partial class Position3 { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }

        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static IEnumerable<TextSpan> UnitQuantityLocation(string value) => ExpectedDiagnosticsLocation.TypeofArgumentTextSpan(UnitQuantityText(value), value, prefix: "SharpMeasuresUnit(");

    private static string ScalarArgumentText(string argument, string value) => $$"""
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Vectors;

        [SharpMeasuresScalar(typeof(UnitOfLength), {{argument}} = {{value}})]
        public partial class Distance { }

        [SpecializedSharpMeasuresVectorGroup(typeof(Position))]
        public static partial class Displacement { }

        [SharpMeasuresVectorGroupMember(typeof(Position))]
        public partial class Position2 { }

        [SharpMeasuresVectorGroup(typeof(UnitOfLength))]
        public static partial class Position { }

        [SpecializedSharpMeasuresVector(typeof(Position3))]
        public partial class Displacement3 { }

        [SharpMeasuresVector(typeof(UnitOfLength))]
        public partial class Position3 { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }

        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static IEnumerable<TextSpan> ScalarArgumentLocation(string argument, string value)
        => ExpectedDiagnosticsLocation.TypeofArgumentTextSpan(ScalarArgumentText(argument, value), value, prefix: $"{argument} = ");

    private static string SpecializedScalarOriginalScalarText(string value) => $$"""
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Vectors;

        [SpecializedSharpMeasuresScalar({{value}})]
        public partial class Distance { }

        [SpecializedSharpMeasuresVectorGroup(typeof(Position))]
        public static partial class Displacement { }

        [SharpMeasuresVectorGroupMember(typeof(Position))]
        public partial class Position2 { }

        [SharpMeasuresVectorGroup(typeof(UnitOfLength))]
        public static partial class Position { }

        [SpecializedSharpMeasuresVector(typeof(Position3))]
        public partial class Displacement3 { }

        [SharpMeasuresVector(typeof(UnitOfLength))]
        public partial class Position3 { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }

        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static IEnumerable<TextSpan> SpecializedScalarOriginalScalarLocation(string value)
        => ExpectedDiagnosticsLocation.TypeofArgumentTextSpan(SpecializedScalarOriginalScalarText(value), value, prefix: "SpecializedSharpMeasuresScalar(");

    private static string SpecializedScalarArgumentText(string argument, string value) => $$"""
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Vectors;

        [SpecializedSharpMeasuresScalar(typeof(Length), {{argument}} = {{value}})]
        public partial class Distance { }

        [SpecializedSharpMeasuresVectorGroup(typeof(Position))]
        public static partial class Displacement { }

        [SharpMeasuresVectorGroupMember(typeof(Position))]
        public partial class Position2 { }

        [SharpMeasuresVectorGroup(typeof(UnitOfLength))]
        public static partial class Position { }

        [SpecializedSharpMeasuresVector(typeof(Position3))]
        public partial class Displacement3 { }

        [SharpMeasuresVector(typeof(UnitOfLength))]
        public partial class Position3 { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }

        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static IEnumerable<TextSpan> SpecializedScalarArgumentLocation(string argument, string value)
        => ExpectedDiagnosticsLocation.TypeofArgumentTextSpan(SpecializedScalarArgumentText(argument, value), value, prefix: $"{argument} = ");

    private static string VectorScalarText(string value) => $$"""
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Vectors;

        [SharpMeasuresVector(typeof(UnitOfLength), Scalar = {{value}})]
        public partial class Position4 { }

        [SpecializedSharpMeasuresVectorGroup(typeof(Position))]
        public static partial class Displacement { }

        [SharpMeasuresVectorGroupMember(typeof(Position))]
        public partial class Position2 { }

        [SharpMeasuresVectorGroup(typeof(UnitOfLength))]
        public static partial class Position { }

        [SpecializedSharpMeasuresVector(typeof(Position3))]
        public partial class Displacement3 { }

        [SharpMeasuresVector(typeof(UnitOfLength))]
        public partial class Position3 { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }

        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static IEnumerable<TextSpan> VectorScalarLocation(string value) => ExpectedDiagnosticsLocation.TypeofArgumentTextSpan(VectorScalarText(value), value, prefix: "Scalar = ");

    private static string SpecializedVectorScalarText(string value) => $$"""
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Vectors;

        [SpecializedSharpMeasuresVector(typeof(Position3), Scalar = {{value}})]
        public partial class Size3 { }

        [SpecializedSharpMeasuresVectorGroup(typeof(Position))]
        public static partial class Displacement { }

        [SharpMeasuresVectorGroupMember(typeof(Position))]
        public partial class Position2 { }

        [SharpMeasuresVectorGroup(typeof(UnitOfLength))]
        public static partial class Position { }

        [SpecializedSharpMeasuresVector(typeof(Position3))]
        public partial class Displacement3 { }

        [SharpMeasuresVector(typeof(UnitOfLength))]
        public partial class Position3 { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }

        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static IEnumerable<TextSpan> SpecializedVectorScalarLocation(string value) => ExpectedDiagnosticsLocation.TypeofArgumentTextSpan(SpecializedVectorScalarText(value), value, prefix: "Scalar = ");

    private static string VectorGroupScalarText(string value) => $$"""
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Vectors;

        [SharpMeasuresVectorGroup(typeof(UnitOfLength), Scalar = {{value}})]
        public static partial class Size { }

        [SpecializedSharpMeasuresVectorGroup(typeof(Position))]
        public static partial class Displacement { }

        [SharpMeasuresVectorGroupMember(typeof(Position))]
        public partial class Position2 { }

        [SharpMeasuresVectorGroup(typeof(UnitOfLength))]
        public static partial class Position { }

        [SpecializedSharpMeasuresVector(typeof(Position3))]
        public partial class Displacement3 { }

        [SharpMeasuresVector(typeof(UnitOfLength))]
        public partial class Position3 { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }

        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static IEnumerable<TextSpan> VectorGrouoScalarLocation(string value) => ExpectedDiagnosticsLocation.TypeofArgumentTextSpan(VectorGroupScalarText(value), value, prefix: "Scalar = ");

    private static string SpecializedVectorGroupScalarText(string value) => $$"""
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Vectors;

        [SpecializedSharpMeasuresVectorGroup(typeof(Position), Scalar = {{value}})]
        public static partial class Size { }

        [SpecializedSharpMeasuresVectorGroup(typeof(Position))]
        public static partial class Displacement { }

        [SharpMeasuresVectorGroupMember(typeof(Position))]
        public partial class Position2 { }

        [SharpMeasuresVectorGroup(typeof(UnitOfLength))]
        public static partial class Position { }

        [SpecializedSharpMeasuresVector(typeof(Position3))]
        public partial class Displacement3 { }

        [SharpMeasuresVector(typeof(UnitOfLength))]
        public partial class Position3 { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }

        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static IEnumerable<TextSpan> SpecializedVectorGrouoScalarLocation(string value)
        => ExpectedDiagnosticsLocation.TypeofArgumentTextSpan(SpecializedVectorGroupScalarText(value), value, prefix: "Scalar = ");

    private static string ConvertibleQuantityText(string value) => $$"""
        using SharpMeasures.Generators.Quantities;
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Vectors;

        [ConvertibleQuantity({{value}})]
        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Distance { }

        [SpecializedSharpMeasuresVectorGroup(typeof(Position))]
        public static partial class Displacement { }

        [SharpMeasuresVectorGroupMember(typeof(Position))]
        public partial class Position2 { }

        [SharpMeasuresVectorGroup(typeof(UnitOfLength))]
        public static partial class Position { }

        [SpecializedSharpMeasuresVector(typeof(Position3))]
        public partial class Displacement3 { }

        [SharpMeasuresVector(typeof(UnitOfLength))]
        public partial class Position3 { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }

        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static IEnumerable<TextSpan> ConvertibleQuantityLocation(string value)
        => ExpectedDiagnosticsLocation.TypeofArgumentTextSpan(ConvertibleQuantityText(value), value, prefix: "ConvertibleQuantity(");
}
