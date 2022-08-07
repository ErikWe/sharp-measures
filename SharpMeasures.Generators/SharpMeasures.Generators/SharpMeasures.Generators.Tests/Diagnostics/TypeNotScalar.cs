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
    public Task VerifyTypeNotScalarDiagnosticsMessage_Null() => AssertAndVerifyUnitQuantity(NullSubtext);

    [Fact]
    public Task VerifyTypeNotScalarDiagnosticsMessage_Int() => AssertAndVerifyUnitQuantity(IntSubtext);

    [Theory]
    [MemberData(nameof(NonScalarTypes))]
    public void UnitQuantity(SourceSubtext scalarType) => AssertUnitQuantity(scalarType);

    [Theory]
    [MemberData(nameof(NonScalarTypes))]
    public void ScalarReciprocal(SourceSubtext argumentValue) => AssertScalarArgument("Reciprocal", argumentValue);

    [Theory]
    [MemberData(nameof(NonScalarTypes))]
    public void ScalarSquare(SourceSubtext argumentValue) => AssertScalarArgument("Square", argumentValue);

    [Theory]
    [MemberData(nameof(NonScalarTypes))]
    public void ScalarCube(SourceSubtext argumentValue) => AssertScalarArgument("Cube", argumentValue);

    [Theory]
    [MemberData(nameof(NonScalarTypes))]
    public void ScalarSquareRoot(SourceSubtext argumentValue) => AssertScalarArgument("SquareRoot", argumentValue);

    [Theory]
    [MemberData(nameof(NonScalarTypes))]
    public void ScalarCubeRoot(SourceSubtext argumentValue) => AssertScalarArgument("CubeRoot", argumentValue);

    [Theory]
    [MemberData(nameof(NonScalarTypes))]
    public void ScalarDifference(SourceSubtext argumentValue) => AssertScalarArgument("Difference", argumentValue);

    [Theory]
    [MemberData(nameof(NonScalarTypes))]
    public void SpecializedScalarOriginalScalar(SourceSubtext originalScalarType) => AssertSpecializedScalarOriginalScalar(originalScalarType);

    [Theory]
    [MemberData(nameof(NonScalarTypes))]
    public void SpecializedScalarReciprocal(SourceSubtext argumentValue) => AssertSpecializedScalarArgument("Reciprocal", argumentValue);

    [Theory]
    [MemberData(nameof(NonScalarTypes))]
    public void SpecializedScalarSquare(SourceSubtext argumentValue) => AssertSpecializedScalarArgument("Square", argumentValue);

    [Theory]
    [MemberData(nameof(NonScalarTypes))]
    public void SpecializedScalarCube(SourceSubtext argumentValue) => AssertSpecializedScalarArgument("Cube", argumentValue);

    [Theory]
    [MemberData(nameof(NonScalarTypes))]
    public void SpecializedScalarSquareRoot(SourceSubtext argumentValue) => AssertSpecializedScalarArgument("SquareRoot", argumentValue);

    [Theory]
    [MemberData(nameof(NonScalarTypes))]
    public void SpecializedScalarCubeRoot(SourceSubtext argumentValue) => AssertSpecializedScalarArgument("CubeRoot", argumentValue);

    [Theory]
    [MemberData(nameof(NonScalarTypes))]
    public void SpecializedScalarDifference(SourceSubtext argumentValue) => AssertSpecializedScalarArgument("Difference", argumentValue);

    [Theory]
    [MemberData(nameof(NonScalarTypes))]
    public void VectorScalar(SourceSubtext scalarType) => AssertVectorScalar(scalarType);

    [Theory]
    [MemberData(nameof(NonScalarTypes))]
    public void SpecializedVectorScalar(SourceSubtext scalarType) => AssertSpecializedVectorScalar(scalarType);

    [Theory]
    [MemberData(nameof(NonScalarTypes))]
    public void VectorGroupScalar(SourceSubtext scalarType) => AssertVectorGroupScalar(scalarType);

    [Theory]
    [MemberData(nameof(NonScalarTypes))]
    public void SpecializedVectorGroupScalar(SourceSubtext scalarType) => AssertSpecializedVectorGroupScalar(scalarType);

    [Theory]
    [MemberData(nameof(NonScalarTypes))]
    public void ConvertibleQuantity(SourceSubtext quantityType) => AssertConvertibleQuantity(quantityType);

    private static IEnumerable<object[]> NonScalarTypes() => new object[][]
    {
        new object[] { NullSubtext },
        new object[] { IntSubtext },
        new object[] { UnitOfLengthSubtext },
        new object[] { PositionSubtext },
        new object[] { DisplacementSubtext },
        new object[] { Position2Subtext },
        new object[] { Position3Subtext },
        new object[] { Displacement3Subtext }
    };

    private static SourceSubtext NullSubtext { get; } = new("null");
    private static SourceSubtext IntSubtext { get; } = SourceSubtext.Typeof("int");
    private static SourceSubtext UnitOfLengthSubtext { get; } = SourceSubtext.Typeof("UnitOfLength");
    private static SourceSubtext PositionSubtext { get; } = SourceSubtext.Typeof("Position");
    private static SourceSubtext DisplacementSubtext { get; } = SourceSubtext.Typeof("Displacement");
    private static SourceSubtext Position2Subtext { get; } = SourceSubtext.Typeof("Position2");
    private static SourceSubtext Position3Subtext { get; } = SourceSubtext.Typeof("Position3");
    private static SourceSubtext Displacement3Subtext { get; } = SourceSubtext.Typeof("Displacement3");

    private static GeneratorVerifier AssertExactlyTypeNotScalarDiagnostics(string source) => GeneratorVerifier.Construct<SharpMeasuresGenerator>(source).AssertExactlyListedDiagnosticsIDsReported(TypeNotScalarDiagnostics);
    private static IReadOnlyCollection<string> TypeNotScalarDiagnostics { get; } = new string[] { DiagnosticIDs.TypeNotScalar };

    private static string UnitQuantityText(SourceSubtext scalarType) => $$"""
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Vectors;

        [SharpMeasuresUnit({{scalarType}})]
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

    private static TextSpan UnitQuantityLocation(SourceSubtext scalarType) => ExpectedDiagnosticsLocation.TextSpan(UnitQuantityText(scalarType), scalarType, prefix: "SharpMeasuresUnit(");

    private static GeneratorVerifier AssertUnitQuantity(SourceSubtext scalarType)
    {
        var source = UnitQuantityText(scalarType);
        var expectedLocation = UnitQuantityLocation(scalarType);

        return AssertExactlyTypeNotScalarDiagnostics(source).AssertDiagnosticsLocation(expectedLocation, source);
    }

    private static Task AssertAndVerifyUnitQuantity(SourceSubtext scalarType) => AssertUnitQuantity(scalarType).VerifyDiagnostics();

    private static string ScalarArgumentText(string argument, SourceSubtext argumentValue) => $$"""
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Vectors;

        [SharpMeasuresScalar(typeof(UnitOfLength), {{argument}} = {{argumentValue}})]
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

    private static TextSpan ScalarArgumentLocation(string argument, SourceSubtext argumentValue) => ExpectedDiagnosticsLocation.TextSpan(ScalarArgumentText(argument, argumentValue), argumentValue, prefix: $"{argument} = ");

    private static GeneratorVerifier AssertScalarArgument(string argument, SourceSubtext scalarType)
    {
        var source = ScalarArgumentText(argument, scalarType);
        var expectedLocation = ScalarArgumentLocation(argument, scalarType);

        return AssertExactlyTypeNotScalarDiagnostics(source).AssertDiagnosticsLocation(expectedLocation, source);
    }

    private static string SpecializedScalarOriginalScalarText(SourceSubtext originalScalarType) => $$"""
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Vectors;

        [SpecializedSharpMeasuresScalar({{originalScalarType}})]
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

    private static TextSpan SpecializedScalarOriginalScalarLocation(SourceSubtext originalScalarType) => ExpectedDiagnosticsLocation.TextSpan(SpecializedScalarOriginalScalarText(originalScalarType), originalScalarType, prefix: "SpecializedSharpMeasuresScalar(");

    private static GeneratorVerifier AssertSpecializedScalarOriginalScalar(SourceSubtext originalScalarType)
    {
        var source = SpecializedScalarOriginalScalarText(originalScalarType);
        var expectedLocation = SpecializedScalarOriginalScalarLocation(originalScalarType);

        return AssertExactlyTypeNotScalarDiagnostics(source).AssertDiagnosticsLocation(expectedLocation, source);
    }

    private static string SpecializedScalarArgumentText(string argument, SourceSubtext argumentValue) => $$"""
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Vectors;

        [SpecializedSharpMeasuresScalar(typeof(Length), {{argument}} = {{argumentValue}})]
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

    private static TextSpan SpecializedScalarArgumentLocation(string argument, SourceSubtext argumentValue) => ExpectedDiagnosticsLocation.TextSpan(SpecializedScalarArgumentText(argument, argumentValue), argumentValue, prefix: $"{argument} = ");

    private static GeneratorVerifier AssertSpecializedScalarArgument(string argument, SourceSubtext scalarType)
    {
        var source = SpecializedScalarArgumentText(argument, scalarType);
        var expectedLocation = SpecializedScalarArgumentLocation(argument, scalarType);

        return AssertExactlyTypeNotScalarDiagnostics(source).AssertDiagnosticsLocation(expectedLocation, source);
    }

    private static string VectorScalarText(SourceSubtext scalarType) => $$"""
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Vectors;

        [SharpMeasuresVector(typeof(UnitOfLength), Scalar = {{scalarType}})]
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

    private static TextSpan VectorScalarLocation(SourceSubtext scalarType) => ExpectedDiagnosticsLocation.TextSpan(VectorScalarText(scalarType), scalarType, prefix: "Scalar = ");

    private static GeneratorVerifier AssertVectorScalar(SourceSubtext scalarType)
    {
        var source = VectorScalarText(scalarType);
        var expectedLocation = VectorScalarLocation(scalarType);

        return AssertExactlyTypeNotScalarDiagnostics(source).AssertDiagnosticsLocation(expectedLocation, source);
    }

    private static string SpecializedVectorScalarText(SourceSubtext scalarType) => $$"""
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Vectors;

        [SpecializedSharpMeasuresVector(typeof(Position3), Scalar = {{scalarType}})]
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

    private static TextSpan SpecializedVectorScalarLocation(SourceSubtext scalarType) => ExpectedDiagnosticsLocation.TextSpan(SpecializedVectorScalarText(scalarType), scalarType, prefix: "Scalar = ");

    private static GeneratorVerifier AssertSpecializedVectorScalar(SourceSubtext scalarType)
    {
        var source = SpecializedVectorScalarText(scalarType);
        var expectedLocation = SpecializedVectorScalarLocation(scalarType);

        return AssertExactlyTypeNotScalarDiagnostics(source).AssertDiagnosticsLocation(expectedLocation, source);
    }

    private static string VectorGroupScalarText(SourceSubtext scalarType) => $$"""
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Vectors;

        [SharpMeasuresVectorGroup(typeof(UnitOfLength), Scalar = {{scalarType}})]
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

    private static TextSpan VectorGrouoScalarLocation(SourceSubtext scalarType) => ExpectedDiagnosticsLocation.TextSpan(VectorGroupScalarText(scalarType), scalarType, prefix: "Scalar = ");

    private static GeneratorVerifier AssertVectorGroupScalar(SourceSubtext scalarType)
    {
        var source = VectorGroupScalarText(scalarType);
        var expectedLocation = VectorGrouoScalarLocation(scalarType);

        return AssertExactlyTypeNotScalarDiagnostics(source).AssertDiagnosticsLocation(expectedLocation, source);
    }

    private static string SpecializedVectorGroupScalarText(SourceSubtext scalarType) => $$"""
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Vectors;

        [SpecializedSharpMeasuresVectorGroup(typeof(Position), Scalar = {{scalarType}})]
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

    private static TextSpan SpecializedVectorGrouoScalarLocation(SourceSubtext scalarType) => ExpectedDiagnosticsLocation.TextSpan(SpecializedVectorGroupScalarText(scalarType), scalarType, prefix: "Scalar = ");

    private static GeneratorVerifier AssertSpecializedVectorGroupScalar(SourceSubtext scalarType)
    {
        var source = SpecializedVectorGroupScalarText(scalarType);
        var expectedLocation = SpecializedVectorGrouoScalarLocation(scalarType);

        return AssertExactlyTypeNotScalarDiagnostics(source).AssertDiagnosticsLocation(expectedLocation, source);
    }

    private static string ConvertibleQuantityText(SourceSubtext quantityType) => $$"""
        using SharpMeasures.Generators.Quantities;
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Vectors;

        [ConvertibleQuantity({{quantityType}})]
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

    private static TextSpan ConvertibleQuantityLocation(SourceSubtext quantityType) => ExpectedDiagnosticsLocation.TextSpan(ConvertibleQuantityText(quantityType), quantityType, prefix: "ConvertibleQuantity(");

    private static GeneratorVerifier AssertConvertibleQuantity(SourceSubtext quantityType)
    {
        if (quantityType.Target is "null")
        {
            quantityType = quantityType with { Prefix = $"{quantityType.Prefix}(System.Type)" };
        }

        var source = ConvertibleQuantityText(quantityType);
        var expectedLocation = ConvertibleQuantityLocation(quantityType);

        return AssertExactlyTypeNotScalarDiagnostics(source).AssertDiagnosticsLocation(expectedLocation, source);
    }
}
