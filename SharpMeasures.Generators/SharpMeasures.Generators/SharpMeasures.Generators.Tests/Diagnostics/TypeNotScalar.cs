namespace SharpMeasures.Generators.Tests.Diagnostics;

using SharpMeasures.Generators.Diagnostics;
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

        return AssertExactlyTypeNotScalarDiagnostics(source).VerifyDiagnostics();
    }

    [Fact]
    public Task VerifyTypeNotScalarDiagnosticsMessage_Int()
    {
        var source = UnitQuantityText("typeof(int)");

        return AssertExactlyTypeNotScalarDiagnostics(source).VerifyDiagnostics();
    }

    [Theory]
    [MemberData(nameof(NonScalarTypes))]
    public void UnitQuantity_ExactList(string value)
    {
        var source = UnitQuantityText(value);

        AssertExactlyTypeNotScalarDiagnostics(source);
    }

    [Theory]
    [MemberData(nameof(NonScalarTypes))]
    public void ScalarReciprocal_ExactList(string value)
    {
        var source = ScalarArgumentText("Reciprocal", value);

        AssertExactlyTypeNotScalarDiagnostics(source);
    }

    [Theory]
    [MemberData(nameof(NonScalarTypes))]
    public void ScalarSquare_ExactList(string value)
    {
        var source = ScalarArgumentText("Square", value);

        AssertExactlyTypeNotScalarDiagnostics(source);
    }

    [Theory]
    [MemberData(nameof(NonScalarTypes))]
    public void ScalarCube_ExactList(string value)
    {
        var source = ScalarArgumentText("Cube", value);

        AssertExactlyTypeNotScalarDiagnostics(source);
    }

    [Theory]
    [MemberData(nameof(NonScalarTypes))]
    public void ScalarSquareRoot_ExactList(string value)
    {
        var source = ScalarArgumentText("SquareRoot", value);

        AssertExactlyTypeNotScalarDiagnostics(source);
    }

    [Theory]
    [MemberData(nameof(NonScalarTypes))]
    public void ScalarCubeRoot_ExactList(string value)
    {
        var source = ScalarArgumentText("CubeRoot", value);

        AssertExactlyTypeNotScalarDiagnostics(source);
    }

    [Theory]
    [MemberData(nameof(NonScalarTypes))]
    public void ScalarDifference_ExactList(string value)
    {
        var source = ScalarArgumentText("Difference", value);

        AssertExactlyTypeNotScalarDiagnostics(source);
    }

    [Theory]
    [MemberData(nameof(NonScalarTypes))]
    public void SpecializedScalarOriginalScalar_ExactList(string value)
    {
        var source = SpecializedScalarOriginalScalarText(value);

        AssertExactlyTypeNotScalarDiagnostics(source);
    }

    [Theory]
    [MemberData(nameof(NonScalarTypes))]
    public void SpecializedScalarReciprocal_ExactList(string value)
    {
        var source = SpecializedScalarArgumentText("Reciprocal", value);

        AssertExactlyTypeNotScalarDiagnostics(source);
    }

    [Theory]
    [MemberData(nameof(NonScalarTypes))]
    public void SpecializedScalarSquare_ExactList(string value)
    {
        var source = SpecializedScalarArgumentText("Square", value);

        AssertExactlyTypeNotScalarDiagnostics(source);
    }

    [Theory]
    [MemberData(nameof(NonScalarTypes))]
    public void SpecializedScalarCube_ExactList(string value)
    {
        var source = SpecializedScalarArgumentText("Cube", value);

        AssertExactlyTypeNotScalarDiagnostics(source);
    }

    [Theory]
    [MemberData(nameof(NonScalarTypes))]
    public void SpecializedScalarSquareRoot_ExactList(string value)
    {
        var source = SpecializedScalarArgumentText("SquareRoot", value);

        AssertExactlyTypeNotScalarDiagnostics(source);
    }

    [Theory]
    [MemberData(nameof(NonScalarTypes))]
    public void SpecializedScalarCubeRoot_ExactList(string value)
    {
        var source = SpecializedScalarArgumentText("CubeRoot", value);

        AssertExactlyTypeNotScalarDiagnostics(source);
    }

    [Theory]
    [MemberData(nameof(NonScalarTypes))]
    public void SpecializedScalarDifference_ExactList(string value)
    {
        var source = SpecializedScalarArgumentText("Difference", value);

        AssertExactlyTypeNotScalarDiagnostics(source);
    }

    [Theory]
    [MemberData(nameof(NonScalarTypes))]
    public void VectorScalar_ExactList(string value)
    {
        var source = VectorScalarText(value);

        AssertExactlyTypeNotScalarDiagnostics(source);
    }

    [Theory]
    [MemberData(nameof(NonScalarTypes))]
    public void SpecializedVectorScalar_ExactList(string value)
    {
        var source = SpecializedVectorScalarText(value);

        AssertExactlyTypeNotScalarDiagnostics(source);
    }

    [Theory]
    [MemberData(nameof(NonScalarTypes))]
    public void VectorGroupScalar_ExactList(string value)
    {
        var source = VectorGroupScalarText(value);

        AssertExactlyTypeNotScalarDiagnostics(source);
    }

    [Theory]
    [MemberData(nameof(NonScalarTypes))]
    public void SpecializedVectorGroupScalar_ExactList(string value)
    {
        var source = SpecializedVectorGroupScalarText(value);

        AssertExactlyTypeNotScalarDiagnostics(source);
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

        AssertExactlyTypeNotScalarDiagnostics(source);
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

    private static GeneratorVerifier AssertExactlyTypeNotScalarDiagnostics(string source) => GeneratorVerifier.Construct<SharpMeasuresGenerator>(source).AssertExactlyListedDiagnosticsIDsReported(TypeNotScalarDiagnostics);
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
}
