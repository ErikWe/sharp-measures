namespace SharpMeasures.Generators.Tests.Diagnostics;

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
    public Task VerifyTypeNotScalarDiagnosticsMessage_Null() => AssertUnitQuantity(NullType).VerifyDiagnostics();

    [Fact]
    public Task VerifyTypeNotScalarDiagnosticsMessage_Int() => AssertUnitQuantity(IntType).VerifyDiagnostics();

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

    public static IEnumerable<object[]> NonScalarTypes() => new object[][]
    {
        new object[] { NullType },
        new object[] { IntType },
        new object[] { UnitOfLengthType },
        new object[] { PositionType },
        new object[] { DisplacementType },
        new object[] { Position2Type },
        new object[] { Position3Type },
        new object[] { Displacement3Type }
    };

    private static SourceSubtext NullType { get; } = SourceSubtext.Covered("null", prefix: "(System.Type)");
    private static SourceSubtext IntType { get; } = SourceSubtext.AsTypeof("int");
    private static SourceSubtext UnitOfLengthType { get; } = SourceSubtext.AsTypeof("UnitOfLength");
    private static SourceSubtext PositionType { get; } = SourceSubtext.AsTypeof("Position");
    private static SourceSubtext DisplacementType { get; } = SourceSubtext.AsTypeof("Displacement");
    private static SourceSubtext Position2Type { get; } = SourceSubtext.AsTypeof("Position2");
    private static SourceSubtext Position3Type { get; } = SourceSubtext.AsTypeof("Position3");
    private static SourceSubtext Displacement3Type { get; } = SourceSubtext.AsTypeof("Displacement3");

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

    private static GeneratorVerifier AssertUnitQuantity(SourceSubtext scalarType)
    {
        var source = UnitQuantityText(scalarType);
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(source, scalarType.Context.With(outerPrefix: "SharpMeasuresUnit("));

        return AssertExactlyTypeNotScalarDiagnostics(source).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(UnitQuantityIdentical);
    }

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

    private static GeneratorVerifier AssertScalarArgument(string argument, SourceSubtext argumentValue)
    {
        var source = ScalarArgumentText(argument, argumentValue);
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(source, argumentValue.Context.With(outerPrefix: $"{argument} = "));

        return AssertExactlyTypeNotScalarDiagnostics(source).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(ScalarArgumentIdentical(argument));
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

    private static GeneratorVerifier AssertSpecializedScalarOriginalScalar(SourceSubtext originalScalarType)
    {
        var source = SpecializedScalarOriginalScalarText(originalScalarType);
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(source, originalScalarType.Context.With(outerPrefix: "SpecializedSharpMeasuresScalar("));

        return AssertExactlyTypeNotScalarDiagnostics(source).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(SpecializedScalarOriginalScalarIdentical);
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

    private static GeneratorVerifier AssertSpecializedScalarArgument(string argument, SourceSubtext scalarType)
    {
        var source = SpecializedScalarArgumentText(argument, scalarType);
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(source, scalarType.Context.With(outerPrefix: $"{argument} = "));

        return AssertExactlyTypeNotScalarDiagnostics(source).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(SpecializedScalarArgumentIdentical(argument));
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

    private static GeneratorVerifier AssertVectorScalar(SourceSubtext scalarType)
    {
        var source = VectorScalarText(scalarType);
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(source, scalarType.Context.With(outerPrefix: "Scalar = "));

        return AssertExactlyTypeNotScalarDiagnostics(source).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(VectorScalarIdentical);
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

    private static GeneratorVerifier AssertSpecializedVectorScalar(SourceSubtext scalarType)
    {
        var source = SpecializedVectorScalarText(scalarType);
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(source, scalarType.Context.With(outerPrefix: "Scalar = "));

        return AssertExactlyTypeNotScalarDiagnostics(source).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(SpecializedVectorScalarIdentical);
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

    private static GeneratorVerifier AssertVectorGroupScalar(SourceSubtext scalarType)
    {
        var source = VectorGroupScalarText(scalarType);
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(source, scalarType.Context.With(outerPrefix: "Scalar = "));

        return AssertExactlyTypeNotScalarDiagnostics(source).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(VectorGroupScalarIdentical);
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

    private static GeneratorVerifier AssertSpecializedVectorGroupScalar(SourceSubtext scalarType)
    {
        var source = SpecializedVectorGroupScalarText(scalarType);
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(source, scalarType.Context.With(outerPrefix: "Scalar = "));

        return AssertExactlyTypeNotScalarDiagnostics(source).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(SpecializedVectorGroupScalarIdentical);
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

    private static GeneratorVerifier AssertConvertibleQuantity(SourceSubtext quantityType)
    {
        var source = ConvertibleQuantityText(quantityType);
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(source, quantityType.Context.With(outerPrefix: "ConvertibleQuantity("));

        return AssertExactlyTypeNotScalarDiagnostics(source).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(ConvertibleQuantityIdentical);
    }

    private static GeneratorVerifier UnitQuantityIdentical => GeneratorVerifier.Construct<SharpMeasuresGenerator>(UnitQuantityIdenticalText);
    private static GeneratorVerifier ScalarArgumentIdentical(string argument) => GeneratorVerifier.Construct<SharpMeasuresGenerator>(ScalarArgumentIdenticalText(argument));
    private static GeneratorVerifier SpecializedScalarOriginalScalarIdentical => GeneratorVerifier.Construct<SharpMeasuresGenerator>(SpecializedScalarOriginalScalarIdenticalText);
    private static GeneratorVerifier SpecializedScalarArgumentIdentical(string argument) => GeneratorVerifier.Construct<SharpMeasuresGenerator>(SpecializedScalarArgumentIdenticalText(argument));
    private static GeneratorVerifier VectorScalarIdentical => GeneratorVerifier.Construct<SharpMeasuresGenerator>(VectorScalarIdenticalText);
    private static GeneratorVerifier SpecializedVectorScalarIdentical => GeneratorVerifier.Construct<SharpMeasuresGenerator>(SpecializedVectorScalarIdenticalText);
    private static GeneratorVerifier VectorGroupScalarIdentical => GeneratorVerifier.Construct<SharpMeasuresGenerator>(VectorGroupScalarIdenticalText);
    private static GeneratorVerifier SpecializedVectorGroupScalarIdentical => GeneratorVerifier.Construct<SharpMeasuresGenerator>(SpecializedVectorGroupScalarIdenticalText);
    private static GeneratorVerifier ConvertibleQuantityIdentical => GeneratorVerifier.Construct<SharpMeasuresGenerator>(ConvertibleQuantityIdenticalText);

    private static string UnitQuantityIdenticalText => """
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Vectors;

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

    private static string ScalarArgumentIdenticalText(string argument) => $$"""
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Vectors;

        [SharpMeasuresScalar(typeof(UnitOfLength){{(argument is "Difference" ? ", ImplementDifference = false" : string.Empty)}})]
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

    private static string SpecializedScalarOriginalScalarIdenticalText => """
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Vectors;

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

    private static string SpecializedScalarArgumentIdenticalText(string argument) => $$"""
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Vectors;

        [SpecializedSharpMeasuresScalar(typeof(Length){{(argument is "Difference" ? ", ImplementDifference = false" : string.Empty)}})]
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

    private static string VectorScalarIdenticalText => """
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Vectors;

        [SharpMeasuresVector(typeof(UnitOfLength))]
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

    private static string SpecializedVectorScalarIdenticalText => """
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Vectors;

        [SpecializedSharpMeasuresVector(typeof(Position3))]
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

    private static string VectorGroupScalarIdenticalText => """
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Vectors;

        [SharpMeasuresVectorGroup(typeof(UnitOfLength))]
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

    private static string SpecializedVectorGroupScalarIdenticalText => """
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Vectors;

        [SpecializedSharpMeasuresVectorGroup(typeof(Position))]
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

    private static string ConvertibleQuantityIdenticalText => """
        using SharpMeasures.Generators.Quantities;
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Vectors;

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
