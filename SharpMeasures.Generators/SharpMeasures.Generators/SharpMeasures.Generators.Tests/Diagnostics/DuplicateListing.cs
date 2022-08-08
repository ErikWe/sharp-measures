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
public class DuplicateListing
{
    [Fact]
    public Task VerifyDuplicateListingDiagnosticsMessage_Scalar() => AssertConvertibleScalar_SingleAttribute().VerifyDiagnostics();

    [Fact]
    public void ConvertibleScalar_MultipleAttributes() => AssertConvertibleScalar_MultipleAttributes();

    [Fact]
    public void ConvertibleSpecializedScalar_SingleAttribute() => AssertConvertibleSpecializedScalar_SingleAttribute();

    [Fact]
    public void ConvertibleSpecializedScalar_MultipleAttributes() => AssertConvertibleSpecializedScalar_MultipleAttributes();

    [Fact]
    public void ConvertibleSpecializedScalar_InheritedAttribute() => AssertConvertibleSpecializedScalar_InheritedAttribute();

    [Fact]
    public void ConvertibleVector_SingleAttribute() => AssertConvertibleVector_SingleAttribute();

    [Fact]
    public void ConvertibleVector_MultipleAttributes() => AssertConvertibleVector_MultipleAttributes();

    [Fact]
    public void ConvertibleSpecializedVector_SingleAttribute() => AssertConvertibleSpecializedVector_SingleAttribute();

    [Fact]
    public void ConvertibleSpecializedVector_MultipleAttributes() => AssertConvertibleSpecializedVector_MultipleAttributes();

    [Fact]
    public void ConvertibleSpecializedVector_InheritedAttribute() => AssertConvertibleSpecializedVector_InheritedAttribute();

    [Fact]
    public void ConvertibleVectorGroup_SingleAttribute() => AssertConvertibleVectorGroup_SingleAttribute();

    [Fact]
    public void ConvertibleVectorGroup_MultipleAttributes() => AssertConvertibleVectorGroup_MultipleAttributes();

    [Fact]
    public void ConvertibleSpecializedVectorGroup_SingleAttribute() => AssertConvertibleSpecializedVectorGroup_SingleAttribute();

    [Fact]
    public void ConvertibleSpecializedVectorGroup_MultipleAttributes() => AssertConvertibleSpecializedVectorGroup_MultipleAttributes();

    [Fact]
    public void ConvertibleSpecializedVectorGroup_InheritedAttribute() => AssertConvertibleSpecializedVectorGroup_InheritedAttribute();

    private static GeneratorVerifier AssertExactlyDuplicateListingDiagnostics(string source) => GeneratorVerifier.Construct<SharpMeasuresGenerator>(source).AssertExactlyListedDiagnosticsIDsReported(DuplicateListingDiagnostics);
    private static IReadOnlyCollection<string> DuplicateListingDiagnostics { get; } = new string[] { DiagnosticIDs.DuplicateListing };

    private static string ConvertibleScalarText_SingleAttribute => """
        using SharpMeasures.Generators.Quantities;
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;

        [ConvertibleQuantity(typeof(Length), typeof(Length))]
        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Distance { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }

        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static TextSpan ConvertibleScalarLocation_SingleAttribute => ExpectedDiagnosticsLocation.AsTypeofArgumentTextSpan(ConvertibleScalarText_SingleAttribute, target: "Length", prefix: "ConvertibleQuantity(typeof(Length), ");

    private static GeneratorVerifier AssertConvertibleScalar_SingleAttribute() => AssertExactlyDuplicateListingDiagnostics(ConvertibleScalarText_SingleAttribute).AssertDiagnosticsLocation(ConvertibleScalarLocation_SingleAttribute, ConvertibleScalarText_SingleAttribute);

    private static string ConvertibleScalarText_MultipleAttributes => """
        using SharpMeasures.Generators.Quantities;
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;

        [ConvertibleQuantity(typeof(Length))]
        [ConvertibleQuantity(typeof(Length))] // <-
        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Distance { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }

        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static TextSpan ConvertibleScalarLocation_MultipleAttributes => ExpectedDiagnosticsLocation.AsTypeofArgumentTextSpan(ConvertibleScalarText_MultipleAttributes, target: "Length", prefix: "ConvertibleQuantity(", postfix: ")] // <-");

    private static GeneratorVerifier AssertConvertibleScalar_MultipleAttributes() => AssertExactlyDuplicateListingDiagnostics(ConvertibleScalarText_MultipleAttributes).AssertDiagnosticsLocation(ConvertibleScalarLocation_MultipleAttributes, ConvertibleScalarText_MultipleAttributes);

    private static string ConvertibleSpecializedScalarText_SingleAttribute => """
        using SharpMeasures.Generators.Quantities;
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;

        [ConvertibleQuantity(typeof(Length), typeof(Length))]
        [SpecializedSharpMeasuresScalar(typeof(Length))]
        public partial class Distance { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }

        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static TextSpan ConvertibleSpecializedScalarLocation_SingleAttribute => ExpectedDiagnosticsLocation.AsTypeofArgumentTextSpan(ConvertibleSpecializedScalarText_SingleAttribute, target: "Length", prefix: "ConvertibleQuantity(typeof(Length), ");

    private static GeneratorVerifier AssertConvertibleSpecializedScalar_SingleAttribute() => AssertExactlyDuplicateListingDiagnostics(ConvertibleSpecializedScalarText_SingleAttribute).AssertDiagnosticsLocation(ConvertibleSpecializedScalarLocation_SingleAttribute, ConvertibleScalarText_SingleAttribute);

    private static string ConvertibleSpecializedScalarText_MultipleAttributes => """
        using SharpMeasures.Generators.Quantities;
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;

        [ConvertibleQuantity(typeof(Length))]
        [ConvertibleQuantity(typeof(Length))] // <-
        [SpecializedSharpMeasuresScalar(typeof(Length))]
        public partial class Distance { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }

        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static TextSpan ConvertibleSpecializedScalarLocation_MultipleAttributes => ExpectedDiagnosticsLocation.AsTypeofArgumentTextSpan(ConvertibleSpecializedScalarText_MultipleAttributes, target: "Length", prefix: "ConvertibleQuantity(", postfix: ")] // <-");

    private static GeneratorVerifier AssertConvertibleSpecializedScalar_MultipleAttributes() => AssertExactlyDuplicateListingDiagnostics(ConvertibleSpecializedScalarText_MultipleAttributes).AssertDiagnosticsLocation(ConvertibleSpecializedScalarLocation_MultipleAttributes, ConvertibleSpecializedScalarText_MultipleAttributes);

    private static string ConvertibleSpecializedScalarText_InheritedAttribute => """
        using SharpMeasures.Generators.Quantities;
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;

        [ConvertibleQuantity(typeof(Height))] // <-
        [SpecializedSharpMeasuresScalar(typeof(Length))]
        public partial class Distance { }

        [ConvertibleQuantity(typeof(Height))]
        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Height { }

        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static TextSpan ConvertibleSpecializedScalarLocation_InheritedAttribute => ExpectedDiagnosticsLocation.AsTypeofArgumentTextSpan(ConvertibleSpecializedScalarText_InheritedAttribute, target: "Height", prefix: "ConvertibleQuantity(", postfix: ")] // <-");

    private static GeneratorVerifier AssertConvertibleSpecializedScalar_InheritedAttribute() => AssertExactlyDuplicateListingDiagnostics(ConvertibleSpecializedScalarText_InheritedAttribute).AssertDiagnosticsLocation(ConvertibleSpecializedScalarLocation_InheritedAttribute, ConvertibleSpecializedScalarText_InheritedAttribute);

    private static string ConvertibleVectorText_SingleAttribute => """
        using SharpMeasures.Generators.Quantities;
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Vectors;

        [ConvertibleQuantity(typeof(Position3), typeof(Position3))]
        [SharpMeasuresVector(typeof(UnitOfLength))]
        public partial class Displacement3 { }

        [SharpMeasuresVector(typeof(UnitOfLength))]
        public partial class Position3 { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }

        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static TextSpan ConvertibleVectorLocation_SingleAttribute => ExpectedDiagnosticsLocation.AsTypeofArgumentTextSpan(ConvertibleVectorText_SingleAttribute, target: "Position3", prefix: "ConvertibleQuantity(typeof(Position3), ");

    private static GeneratorVerifier AssertConvertibleVector_SingleAttribute() => AssertExactlyDuplicateListingDiagnostics(ConvertibleVectorText_SingleAttribute).AssertDiagnosticsLocation(ConvertibleVectorLocation_SingleAttribute, ConvertibleVectorText_SingleAttribute);

    private static string ConvertibleVectorText_MultipleAttributes => """
        using SharpMeasures.Generators.Quantities;
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Vectors;

        [ConvertibleQuantity(typeof(Position3))]
        [ConvertibleQuantity(typeof(Position3))] // <-
        [SharpMeasuresVector(typeof(UnitOfLength))]
        public partial class Displacement3 { }

        [SharpMeasuresVector(typeof(UnitOfLength))]
        public partial class Position3 { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }

        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static TextSpan ConvertibleVectorLocation_MultipleAttributes => ExpectedDiagnosticsLocation.AsTypeofArgumentTextSpan(ConvertibleVectorText_MultipleAttributes, target: "Position3", prefix: "ConvertibleQuantity(", postfix: ")] // <-");

    private static GeneratorVerifier AssertConvertibleVector_MultipleAttributes() => AssertExactlyDuplicateListingDiagnostics(ConvertibleVectorText_MultipleAttributes).AssertDiagnosticsLocation(ConvertibleVectorLocation_MultipleAttributes, ConvertibleVectorText_MultipleAttributes);

    private static string ConvertibleSpecializedVectorText_SingleAttribute => """
        using SharpMeasures.Generators.Quantities;
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Vectors;

        [ConvertibleQuantity(typeof(Position3), typeof(Position3))]
        [SpecializedSharpMeasuresVector(typeof(Position3))]
        public partial class Displacement3 { }

        [SharpMeasuresVector(typeof(UnitOfLength))]
        public partial class Position3 { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }

        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static TextSpan ConvertibleSpecializedVectorLocation_SingleAttribute => ExpectedDiagnosticsLocation.AsTypeofArgumentTextSpan(ConvertibleSpecializedVectorText_SingleAttribute, target: "Position3", prefix: "ConvertibleQuantity(typeof(Position3), ");

    private static GeneratorVerifier AssertConvertibleSpecializedVector_SingleAttribute() => AssertExactlyDuplicateListingDiagnostics(ConvertibleSpecializedVectorText_SingleAttribute).AssertDiagnosticsLocation(ConvertibleSpecializedVectorLocation_SingleAttribute, ConvertibleSpecializedVectorText_SingleAttribute);

    private static string ConvertibleSpecializedVectorText_MultipleAttributes => """
        using SharpMeasures.Generators.Quantities;
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Vectors;

        [ConvertibleQuantity(typeof(Position3))]
        [ConvertibleQuantity(typeof(Position3))] // <-
        [SpecializedSharpMeasuresVector(typeof(Position3))]
        public partial class Displacement3 { }

        [SharpMeasuresVector(typeof(UnitOfLength))]
        public partial class Position3 { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }

        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static TextSpan ConvertibleSpecializedVectorLocation_MultipleAttributes => ExpectedDiagnosticsLocation.AsTypeofArgumentTextSpan(ConvertibleSpecializedVectorText_MultipleAttributes, target: "Position3", prefix: "ConvertibleQuantity(", postfix: ")] // <-");

    private static GeneratorVerifier AssertConvertibleSpecializedVector_MultipleAttributes() => AssertExactlyDuplicateListingDiagnostics(ConvertibleSpecializedVectorText_MultipleAttributes).AssertDiagnosticsLocation(ConvertibleSpecializedVectorLocation_MultipleAttributes, ConvertibleSpecializedVectorText_MultipleAttributes);

    private static string ConvertibleSpecializedVectorText_InheritedAttribute => """
        using SharpMeasures.Generators.Quantities;
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Vectors;

        [ConvertibleQuantity(typeof(Size3))] // <-
        [SpecializedSharpMeasuresVector(typeof(Position3))]
        public partial class Displacement3 { }
        
        [ConvertibleQuantity(typeof(Size3))]
        [SharpMeasuresVector(typeof(UnitOfLength))]
        public partial class Position3 { }

        [SharpMeasuresVector(typeof(UnitOfLength))]
        public partial class Size3 { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }

        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static TextSpan ConvertibleSpecializedVectorLocation_InheritedAttribute => ExpectedDiagnosticsLocation.AsTypeofArgumentTextSpan(ConvertibleSpecializedVectorText_InheritedAttribute, target: "Size3", prefix: "ConvertibleQuantity(", postfix: ")] // <-");

    private static GeneratorVerifier AssertConvertibleSpecializedVector_InheritedAttribute() => AssertExactlyDuplicateListingDiagnostics(ConvertibleSpecializedVectorText_InheritedAttribute).AssertDiagnosticsLocation(ConvertibleSpecializedVectorLocation_InheritedAttribute, ConvertibleSpecializedVectorText_InheritedAttribute);

    private static string ConvertibleVectorGroupText_SingleAttribute => """
        using SharpMeasures.Generators.Quantities;
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Vectors;

        [ConvertibleQuantity(typeof(Position), typeof(Position))]
        [SharpMeasuresVectorGroup(typeof(UnitOfLength))]
        public static partial class Displacement { }

        [SharpMeasuresVectorGroup(typeof(UnitOfLength))]
        public static partial class Position { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }

        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static TextSpan ConvertibleVectorGroupLocation_SingleAttribute => ExpectedDiagnosticsLocation.AsTypeofArgumentTextSpan(ConvertibleVectorGroupText_SingleAttribute, target: "Position", prefix: "ConvertibleQuantity(typeof(Position), ");

    private static GeneratorVerifier AssertConvertibleVectorGroup_SingleAttribute() => AssertExactlyDuplicateListingDiagnostics(ConvertibleVectorGroupText_SingleAttribute).AssertDiagnosticsLocation(ConvertibleVectorGroupLocation_SingleAttribute, ConvertibleVectorGroupText_SingleAttribute);

    private static string ConvertibleVectorGroupText_MultipleAttributes => """
        using SharpMeasures.Generators.Quantities;
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Vectors;

        [ConvertibleQuantity(typeof(Position))]
        [ConvertibleQuantity(typeof(Position))] // <-
        [SharpMeasuresVectorGroup(typeof(UnitOfLength))]
        public static partial class Displacement { }

        [SharpMeasuresVectorGroup(typeof(UnitOfLength))]
        public static partial class Position { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }

        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static TextSpan ConvertibleVectorGroupLocation_MultipleAttributes => ExpectedDiagnosticsLocation.AsTypeofArgumentTextSpan(ConvertibleVectorGroupText_MultipleAttributes, target: "Position", prefix: "ConvertibleQuantity(", postfix: ")] // <-");

    private static GeneratorVerifier AssertConvertibleVectorGroup_MultipleAttributes() => AssertExactlyDuplicateListingDiagnostics(ConvertibleVectorGroupText_MultipleAttributes).AssertDiagnosticsLocation(ConvertibleVectorGroupLocation_MultipleAttributes, ConvertibleVectorGroupText_MultipleAttributes);

    private static string ConvertibleSpecializedVectorGroupText_SingleAttribute => """
        using SharpMeasures.Generators.Quantities;
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Vectors;

        [ConvertibleQuantity(typeof(Position), typeof(Position))]
        [SpecializedSharpMeasuresVectorGroup(typeof(Position))]
        public static partial class Displacement { }
        
        [SharpMeasuresVectorGroup(typeof(UnitOfLength))]
        public static partial class Position { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }

        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static TextSpan ConvertibleSpecializedVectorGroupLocation_SingleAttribute => ExpectedDiagnosticsLocation.AsTypeofArgumentTextSpan(ConvertibleSpecializedVectorGroupText_SingleAttribute, target: "Position", prefix: "ConvertibleQuantity(typeof(Position), ");

    private static GeneratorVerifier AssertConvertibleSpecializedVectorGroup_SingleAttribute() => AssertExactlyDuplicateListingDiagnostics(ConvertibleSpecializedVectorGroupText_SingleAttribute).AssertDiagnosticsLocation(ConvertibleSpecializedVectorGroupLocation_SingleAttribute, ConvertibleSpecializedVectorGroupText_SingleAttribute);

    private static string ConvertibleSpecializedVectorGroupText_MultipleAttributes => """
        using SharpMeasures.Generators.Quantities;
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Vectors;

        [ConvertibleQuantity(typeof(Position))]
        [ConvertibleQuantity(typeof(Position))] // <-
        [SpecializedSharpMeasuresVectorGroup(typeof(Position))]
        public static partial class Displacement { }
        
        [SharpMeasuresVectorGroup(typeof(UnitOfLength))]
        public static partial class Position { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }

        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static TextSpan ConvertibleSpecializedVectorGroupLocation_MultipleAttributes => ExpectedDiagnosticsLocation.AsTypeofArgumentTextSpan(ConvertibleSpecializedVectorGroupText_MultipleAttributes, target: "Position", prefix: "ConvertibleQuantity(", postfix: ")] // <-");

    private static GeneratorVerifier AssertConvertibleSpecializedVectorGroup_MultipleAttributes() => AssertExactlyDuplicateListingDiagnostics(ConvertibleSpecializedVectorGroupText_MultipleAttributes).AssertDiagnosticsLocation(ConvertibleSpecializedVectorGroupLocation_MultipleAttributes, ConvertibleSpecializedVectorGroupText_MultipleAttributes);

    private static string ConvertibleSpecializedVectorGroupText_InheritedAttribute => """
        using SharpMeasures.Generators.Quantities;
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Vectors;

        [ConvertibleQuantity(typeof(Size))] // <-
        [SpecializedSharpMeasuresVectorGroup(typeof(Position))]
        public static partial class Displacement { }
        
        [ConvertibleQuantity(typeof(Size))]
        [SharpMeasuresVectorGroup(typeof(UnitOfLength))]
        public static partial class Position { }
        
        [SharpMeasuresVectorGroup(typeof(UnitOfLength))]
        public static partial class Size { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }

        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static TextSpan ConvertibleSpecializedVectorGroupLocation_InheritedAttribute => ExpectedDiagnosticsLocation.AsTypeofArgumentTextSpan(ConvertibleSpecializedVectorGroupText_InheritedAttribute, target: "Size", prefix: "ConvertibleQuantity(", postfix: ")] // <-");

    private static GeneratorVerifier AssertConvertibleSpecializedVectorGroup_InheritedAttribute() => AssertExactlyDuplicateListingDiagnostics(ConvertibleSpecializedVectorGroupText_InheritedAttribute).AssertDiagnosticsLocation(ConvertibleSpecializedVectorGroupLocation_InheritedAttribute, ConvertibleSpecializedVectorGroupText_InheritedAttribute);
}
