namespace SharpMeasures.Generators.Tests.Diagnostics.Misc;

using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Tests.Utility;
using SharpMeasures.Generators.Tests.Verify;

using System.Collections.Generic;

using VerifyXunit;

using Xunit;

[UsesVerify]
public class FilteredListLocation
{
    [Theory]
    [MemberData(nameof(UnitListAttributes))]
    public void UnitList(string attribute) => AssertUnitList(attribute);

    [Fact]
    public void ConvertibleScalar() => AssertConvertibleScalar();

    [Fact]
    public void ConvertibleSpecializedScalar() => AssertConvertibleSpecializedScalar();

    [Fact]
    public void ConvertibleVector() => AssertConvertibleVector();

    [Fact]
    public void ConvertibleSpecializedVector() => AssertConvertibleSpecializedVector();

    [Fact]
    public void ConvertibleVectorGroup() => AssertConvertibleVectorGroup();

    [Fact]
    public void ConvertibleSpecializedVectorGroup() => AssertConvertibleSpecializedVectorGroup();

    public static IEnumerable<object[]> UnitListAttributes => new object[][]
    {
        new[] { "IncludeBases" },
        new[] { "ExcludeBases" },
        new[] { "IncludeUnits" },
        new[] { "ExcludeUnits" }
    };

    private static string UnitListText(string attribute) => $$"""
        using SharpMeasures.Generators.Quantities;
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;

        [{{attribute}}("Metre", "Metre", "Kilometre")]
        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }

        [FixedUnit("Metre", "Metres")]
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertUnitList(string attribute)
    {
        var source = UnitListText(attribute);
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(source, target: "\"Kilometre\"");

        return GeneratorVerifier.Construct<SharpMeasuresGenerator>(source).AssertSpecificDiagnosticsLocation(DiagnosticIDs.UnrecognizedUnitName, expectedLocation);
    }

    private static string ConvertibleScalarText => """
        using SharpMeasures.Generators.Quantities;
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;

        [ConvertibleQuantity(typeof(Length), typeof(Length), typeof(UnitOfLength))]
        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Distance { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }

        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertConvertibleScalar()
    {
        var expectedLocation = ExpectedDiagnosticsLocation.AsTypeofArgumentTextSpan(ConvertibleScalarText, target: "UnitOfLength");

        return GeneratorVerifier.Construct<SharpMeasuresGenerator>(ConvertibleScalarText).AssertSpecificDiagnosticsLocation(DiagnosticIDs.TypeNotScalar, expectedLocation);
    }

    private static string ConvertibleSpecializedScalarText => """
        using SharpMeasures.Generators.Quantities;
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;

        [ConvertibleQuantity(typeof(Length), typeof(Length), typeof(UnitOfLength))]
        [SpecializedSharpMeasuresScalar(typeof(Length))]
        public partial class Distance { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }

        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertConvertibleSpecializedScalar()
    {
        var expectedLocation = ExpectedDiagnosticsLocation.AsTypeofArgumentTextSpan(ConvertibleSpecializedScalarText, target: "UnitOfLength");

        return GeneratorVerifier.Construct<SharpMeasuresGenerator>(ConvertibleSpecializedScalarText).AssertSpecificDiagnosticsLocation(DiagnosticIDs.TypeNotScalar, expectedLocation);
    }

    private static string ConvertibleVectorText => """
        using SharpMeasures.Generators.Quantities;
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Vectors;

        [ConvertibleQuantity(typeof(Position3), typeof(Position3), typeof(UnitOfLength))]
        [SharpMeasuresVector(typeof(UnitOfLength))]
        public partial class Displacement3 { }

        [SharpMeasuresVector(typeof(UnitOfLength))]
        public partial class Position3 { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }

        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertConvertibleVector()
    {
        var expectedLocation = ExpectedDiagnosticsLocation.AsTypeofArgumentTextSpan(ConvertibleVectorText, target: "UnitOfLength");

        return GeneratorVerifier.Construct<SharpMeasuresGenerator>(ConvertibleVectorText).AssertSpecificDiagnosticsLocation(DiagnosticIDs.TypeNotVector, expectedLocation);
    }

    private static string ConvertibleSpecializedVectorText => """
        using SharpMeasures.Generators.Quantities;
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Vectors;

        [ConvertibleQuantity(typeof(Position3), typeof(Position3), typeof(UnitOfLength))]
        [SpecializedSharpMeasuresVector(typeof(Position3))]
        public partial class Displacement3 { }

        [SharpMeasuresVector(typeof(UnitOfLength))]
        public partial class Position3 { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }

        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertConvertibleSpecializedVector()
    {
        var expectedLocation = ExpectedDiagnosticsLocation.AsTypeofArgumentTextSpan(ConvertibleSpecializedVectorText, target: "UnitOfLength");

        return GeneratorVerifier.Construct<SharpMeasuresGenerator>(ConvertibleSpecializedVectorText).AssertSpecificDiagnosticsLocation(DiagnosticIDs.TypeNotVector, expectedLocation);
    }

    private static string ConvertibleVectorGroupText => """
        using SharpMeasures.Generators.Quantities;
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Vectors;

        [ConvertibleQuantity(typeof(Position), typeof(Position), typeof(UnitOfLength))]
        [SharpMeasuresVectorGroup(typeof(UnitOfLength))]
        public static partial class Displacement { }

        [SharpMeasuresVectorGroup(typeof(UnitOfLength))]
        public static partial class Position { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }

        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertConvertibleVectorGroup()
    {
        var expectedLocation = ExpectedDiagnosticsLocation.AsTypeofArgumentTextSpan(ConvertibleVectorGroupText, target: "UnitOfLength");

        return GeneratorVerifier.Construct<SharpMeasuresGenerator>(ConvertibleVectorGroupText).AssertSpecificDiagnosticsLocation(DiagnosticIDs.TypeNotVectorGroup, expectedLocation);
    }

    private static string ConvertibleSpecializedVectorGroupText => """
        using SharpMeasures.Generators.Quantities;
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Vectors;

        [ConvertibleQuantity(typeof(Position), typeof(Position), typeof(UnitOfLength))]
        [SpecializedSharpMeasuresVectorGroup(typeof(Position))]
        public static partial class Displacement { }

        [SharpMeasuresVectorGroup(typeof(UnitOfLength))]
        public static partial class Position { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }

        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertConvertibleSpecializedVectorGroup()
    {
        var expectedLocation = ExpectedDiagnosticsLocation.AsTypeofArgumentTextSpan(ConvertibleSpecializedVectorGroupText, target: "UnitOfLength");

        return GeneratorVerifier.Construct<SharpMeasuresGenerator>(ConvertibleSpecializedVectorGroupText).AssertSpecificDiagnosticsLocation(DiagnosticIDs.TypeNotVectorGroup, expectedLocation);
    }
}
