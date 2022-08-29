namespace SharpMeasures.Generators.Tests.Diagnostics;

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

    [Fact]
    public void ConvertiblVectorGroupMember_SingleAttribute() => AssertConvertibleVectorGroupMember_SingleAttribute();

    [Fact]
    public void ConvertiblVectorGroupMember_MultipleAttributes() => AssertConvertibleVectorGroupMember_MultipleAttributes();

    [Fact]
    public void ConvertiblVectorGroupMember_InheritedAttributeFromGroup() => AssertConvertibleVectorGroupMember_InheritedAttributeFromGroup();

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

    private static GeneratorVerifier AssertConvertibleScalar_SingleAttribute()
    {
        var expectedLocation = ExpectedDiagnosticsLocation.AsTypeofArgumentTextSpan(ConvertibleScalarText_SingleAttribute, target: "Length", prefix: "ConvertibleQuantity(typeof(Length), ");

        return AssertExactlyDuplicateListingDiagnostics(ConvertibleScalarText_SingleAttribute).AssertDiagnosticsLocation(expectedLocation, ConvertibleScalarText_SingleAttribute)
            .AssertIdenticalSources(ConvertibleScalarIdentical);
    }

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

    private static GeneratorVerifier AssertConvertibleScalar_MultipleAttributes()
    {
        var expectedLocation = ExpectedDiagnosticsLocation.AsTypeofArgumentTextSpan(ConvertibleScalarText_MultipleAttributes, target: "Length", prefix: "ConvertibleQuantity(", postfix: ")] // <-");

        return AssertExactlyDuplicateListingDiagnostics(ConvertibleScalarText_MultipleAttributes).AssertDiagnosticsLocation(expectedLocation, ConvertibleScalarText_MultipleAttributes)
            .AssertIdenticalSources(ConvertibleScalarIdentical);
    }

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

    private static GeneratorVerifier AssertConvertibleSpecializedScalar_SingleAttribute()
    {
        var expectedLocation = ExpectedDiagnosticsLocation.AsTypeofArgumentTextSpan(ConvertibleSpecializedScalarText_SingleAttribute, target: "Length", prefix: "ConvertibleQuantity(typeof(Length), ");

        return AssertExactlyDuplicateListingDiagnostics(ConvertibleSpecializedScalarText_SingleAttribute).AssertDiagnosticsLocation(expectedLocation, ConvertibleScalarText_SingleAttribute)
            .AssertIdenticalSources(ConvertibleSpecializedScalarIdentical_InSpecialized);
    }

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

    private static GeneratorVerifier AssertConvertibleSpecializedScalar_MultipleAttributes()
    {
        var expectedLocation = ExpectedDiagnosticsLocation.AsTypeofArgumentTextSpan(ConvertibleSpecializedScalarText_MultipleAttributes, target: "Length", prefix: "ConvertibleQuantity(", postfix: ")] // <-");

        return AssertExactlyDuplicateListingDiagnostics(ConvertibleSpecializedScalarText_MultipleAttributes).AssertDiagnosticsLocation(expectedLocation, ConvertibleSpecializedScalarText_MultipleAttributes)
            .AssertIdenticalSources(ConvertibleSpecializedScalarIdentical_InSpecialized);
    }

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

    private static GeneratorVerifier AssertConvertibleSpecializedScalar_InheritedAttribute()
    {
        var expectedLocation = ExpectedDiagnosticsLocation.AsTypeofArgumentTextSpan(ConvertibleSpecializedScalarText_InheritedAttribute, target: "Height", prefix: "ConvertibleQuantity(", postfix: ")] // <-");

        return AssertExactlyDuplicateListingDiagnostics(ConvertibleSpecializedScalarText_InheritedAttribute).AssertDiagnosticsLocation(expectedLocation, ConvertibleSpecializedScalarText_InheritedAttribute)
            .AssertIdenticalSources(ConvertibleSpecializedScalarIdentical_InOriginal);
    }

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

    private static GeneratorVerifier AssertConvertibleVector_SingleAttribute()
    {
        var expectedLocation = ExpectedDiagnosticsLocation.AsTypeofArgumentTextSpan(ConvertibleVectorText_SingleAttribute, target: "Position3", prefix: "ConvertibleQuantity(typeof(Position3), ");

        return AssertExactlyDuplicateListingDiagnostics(ConvertibleVectorText_SingleAttribute).AssertDiagnosticsLocation(expectedLocation, ConvertibleVectorText_SingleAttribute)
            .AssertIdenticalSources(ConvertibleVectorIdentical);
    }

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

    private static GeneratorVerifier AssertConvertibleVector_MultipleAttributes()
    {
        var expectedLocation = ExpectedDiagnosticsLocation.AsTypeofArgumentTextSpan(ConvertibleVectorText_MultipleAttributes, target: "Position3", prefix: "ConvertibleQuantity(", postfix: ")] // <-");

        return AssertExactlyDuplicateListingDiagnostics(ConvertibleVectorText_MultipleAttributes).AssertDiagnosticsLocation(expectedLocation, ConvertibleVectorText_MultipleAttributes)
            .AssertIdenticalSources(ConvertibleVectorIdentical);
    }

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

    private static GeneratorVerifier AssertConvertibleSpecializedVector_SingleAttribute()
    {
        var expectedLocation = ExpectedDiagnosticsLocation.AsTypeofArgumentTextSpan(ConvertibleSpecializedVectorText_SingleAttribute, target: "Position3", prefix: "ConvertibleQuantity(typeof(Position3), ");

        return AssertExactlyDuplicateListingDiagnostics(ConvertibleSpecializedVectorText_SingleAttribute).AssertDiagnosticsLocation(expectedLocation, ConvertibleSpecializedVectorText_SingleAttribute)
            .AssertIdenticalSources(ConvertibleSpecializedVectorIdentical_InSpecialized);
    }

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

    private static GeneratorVerifier AssertConvertibleSpecializedVector_MultipleAttributes()
    {
        var expectedLocation = ExpectedDiagnosticsLocation.AsTypeofArgumentTextSpan(ConvertibleSpecializedVectorText_MultipleAttributes, target: "Position3", prefix: "ConvertibleQuantity(", postfix: ")] // <-");

        return AssertExactlyDuplicateListingDiagnostics(ConvertibleSpecializedVectorText_MultipleAttributes).AssertDiagnosticsLocation(expectedLocation, ConvertibleSpecializedVectorText_MultipleAttributes)
            .AssertIdenticalSources(ConvertibleSpecializedVectorIdentical_InSpecialized);
    }

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

    private static GeneratorVerifier AssertConvertibleSpecializedVector_InheritedAttribute()
    {
        var expectedLocation = ExpectedDiagnosticsLocation.AsTypeofArgumentTextSpan(ConvertibleSpecializedVectorText_InheritedAttribute, target: "Size3", prefix: "ConvertibleQuantity(", postfix: ")] // <-");

        return AssertExactlyDuplicateListingDiagnostics(ConvertibleSpecializedVectorText_InheritedAttribute).AssertDiagnosticsLocation(expectedLocation, ConvertibleSpecializedVectorText_InheritedAttribute)
            .AssertIdenticalSources(ConvertibleSpecializedVectorIdentical_InOriginal);
    }

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

    private static GeneratorVerifier AssertConvertibleVectorGroup_SingleAttribute()
    {
        var expectedLocation = ExpectedDiagnosticsLocation.AsTypeofArgumentTextSpan(ConvertibleVectorGroupText_SingleAttribute, target: "Position", prefix: "ConvertibleQuantity(typeof(Position), ");

        return AssertExactlyDuplicateListingDiagnostics(ConvertibleVectorGroupText_SingleAttribute).AssertDiagnosticsLocation(expectedLocation, ConvertibleVectorGroupText_SingleAttribute)
            .AssertIdenticalSources(ConvertibleVectorGroupIdentical);
    }

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

    private static GeneratorVerifier AssertConvertibleVectorGroup_MultipleAttributes()
    {
        var expectedLocation = ExpectedDiagnosticsLocation.AsTypeofArgumentTextSpan(ConvertibleVectorGroupText_MultipleAttributes, target: "Position", prefix: "ConvertibleQuantity(", postfix: ")] // <-");

        return AssertExactlyDuplicateListingDiagnostics(ConvertibleVectorGroupText_MultipleAttributes).AssertDiagnosticsLocation(expectedLocation, ConvertibleVectorGroupText_MultipleAttributes)
            .AssertIdenticalSources(ConvertibleVectorGroupIdentical);
    }

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

    private static GeneratorVerifier AssertConvertibleSpecializedVectorGroup_SingleAttribute()
    {
        var expectedLocation = ExpectedDiagnosticsLocation.AsTypeofArgumentTextSpan(ConvertibleSpecializedVectorGroupText_SingleAttribute, target: "Position", prefix: "ConvertibleQuantity(typeof(Position), ");

        return AssertExactlyDuplicateListingDiagnostics(ConvertibleSpecializedVectorGroupText_SingleAttribute).AssertDiagnosticsLocation(expectedLocation, ConvertibleSpecializedVectorGroupText_SingleAttribute)
            .AssertIdenticalSources(ConvertibleSpecializedVectorGroupIdentical_InSpecialized);
    }

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

    private static GeneratorVerifier AssertConvertibleSpecializedVectorGroup_MultipleAttributes()
    {
        var expectedLocation = ExpectedDiagnosticsLocation.AsTypeofArgumentTextSpan(ConvertibleSpecializedVectorGroupText_MultipleAttributes, target: "Position", prefix: "ConvertibleQuantity(", postfix: ")] // <-");

        return AssertExactlyDuplicateListingDiagnostics(ConvertibleSpecializedVectorGroupText_MultipleAttributes).AssertDiagnosticsLocation(expectedLocation, ConvertibleSpecializedVectorGroupText_MultipleAttributes)
            .AssertIdenticalSources(ConvertibleSpecializedVectorGroupIdentical_InSpecialized);
    }

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

    private static GeneratorVerifier AssertConvertibleSpecializedVectorGroup_InheritedAttribute()
    {
        var expectedLocation = ExpectedDiagnosticsLocation.AsTypeofArgumentTextSpan(ConvertibleSpecializedVectorGroupText_InheritedAttribute, target: "Size", prefix: "ConvertibleQuantity(", postfix: ")] // <-");

        return AssertExactlyDuplicateListingDiagnostics(ConvertibleSpecializedVectorGroupText_InheritedAttribute).AssertDiagnosticsLocation(expectedLocation, ConvertibleSpecializedVectorGroupText_InheritedAttribute)
            .AssertIdenticalSources(ConvertibleSpecializedVectorGroupIdentical_InOriginal);
    }

    private static string ConvertibleVectorGroupMemberText_SingleAttribute => """
        using SharpMeasures.Generators.Quantities;
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Vectors;

        [ConvertibleQuantity(typeof(Displacement3), typeof(Displacement3))]
        [SharpMeasuresVectorGroupMember(typeof(Position))]
        public partial class Position3 { }
        
        [SharpMeasuresVectorGroup(typeof(UnitOfLength))]
        public static partial class Position { }
        
        [SharpMeasuresVector(typeof(UnitOfLength))]
        public partial class Displacement3 { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }
        
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertConvertibleVectorGroupMember_SingleAttribute()
    {
        var expectedLocation = ExpectedDiagnosticsLocation.AsTypeofArgumentTextSpan(ConvertibleVectorGroupMemberText_SingleAttribute, target: "Displacement3", prefix: "ConvertibleQuantity(typeof(Displacement3), ");

        return AssertExactlyDuplicateListingDiagnostics(ConvertibleVectorGroupMemberText_SingleAttribute).AssertDiagnosticsLocation(expectedLocation, ConvertibleVectorGroupMemberText_SingleAttribute)
            .AssertIdenticalSources(ConvertibleVectorGroupMemberIdentical_InMember);
    }

    private static string ConvertibleVectorGroupMemberText_MultipleAttributes => """
        using SharpMeasures.Generators.Quantities;
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Vectors;

        [ConvertibleQuantity(typeof(Displacement3))]
        [ConvertibleQuantity(typeof(Displacement3))] // <-
        [SharpMeasuresVectorGroupMember(typeof(Position))]
        public partial class Position3 { }
        
        [SharpMeasuresVectorGroup(typeof(UnitOfLength))]
        public static partial class Position { }

        [SharpMeasuresVector(typeof(UnitOfLength))]
        public partial class Displacement3 { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }

        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertConvertibleVectorGroupMember_MultipleAttributes()
    {
        var expectedLocation = ExpectedDiagnosticsLocation.AsTypeofArgumentTextSpan(ConvertibleVectorGroupMemberText_MultipleAttributes, target: "Displacement3", postfix: ")] // <-");

        return AssertExactlyDuplicateListingDiagnostics(ConvertibleVectorGroupMemberText_MultipleAttributes).AssertDiagnosticsLocation(expectedLocation, ConvertibleVectorGroupMemberText_MultipleAttributes)
            .AssertIdenticalSources(ConvertibleVectorGroupMemberIdentical_InMember);
    }

    private static string ConvertibleVectorGroupMemberText_InheritedAttributeFromGroup => """
        using SharpMeasures.Generators.Quantities;
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Vectors;

        [ConvertibleQuantity(typeof(Displacement3))]
        [SharpMeasuresVectorGroupMember(typeof(Position))]
        public partial class Position3 { }
        
        [ConvertibleQuantity(typeof(Displacement))]
        [SharpMeasuresVectorGroup(typeof(UnitOfLength))]
        public static partial class Position { }

        [SharpMeasuresVectorGroupMember(typeof(Displacement))]
        public partial class Displacement3 { }

        [SharpMeasuresVectorGroup(typeof(UnitOfLength))]
        public static partial class Displacement { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }

        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertConvertibleVectorGroupMember_InheritedAttributeFromGroup()
    {
        var expectedLocation = ExpectedDiagnosticsLocation.AsTypeofArgumentTextSpan(ConvertibleVectorGroupMemberText_InheritedAttributeFromGroup, target: "Displacement3");

        return AssertExactlyDuplicateListingDiagnostics(ConvertibleVectorGroupMemberText_InheritedAttributeFromGroup).AssertDiagnosticsLocation(expectedLocation, ConvertibleVectorGroupMemberText_InheritedAttributeFromGroup)
            .AssertIdenticalSources(ConvertibleVectorGroupMemberIdentical_InGroup);
    }

    private static GeneratorVerifier ConvertibleScalarIdentical => GeneratorVerifier.Construct<SharpMeasuresGenerator>(ConvertibleScalarIdenticalText);
    private static GeneratorVerifier ConvertibleSpecializedScalarIdentical_InSpecialized => GeneratorVerifier.Construct<SharpMeasuresGenerator>(ConvertibleSpecializedScalarIdenticalText_InSpecialized);
    private static GeneratorVerifier ConvertibleSpecializedScalarIdentical_InOriginal => GeneratorVerifier.Construct<SharpMeasuresGenerator>(ConvertibleSpecializedScalarIdenticalText_InOriginal);
    private static GeneratorVerifier ConvertibleVectorIdentical => GeneratorVerifier.Construct<SharpMeasuresGenerator>(ConvertibleVectorIdenticalText);
    private static GeneratorVerifier ConvertibleSpecializedVectorIdentical_InSpecialized => GeneratorVerifier.Construct<SharpMeasuresGenerator>(ConvertibleSpecializedVectorIdenticalText_InSpecialized);
    private static GeneratorVerifier ConvertibleSpecializedVectorIdentical_InOriginal => GeneratorVerifier.Construct<SharpMeasuresGenerator>(ConvertibleSpecializedVectorIdenticalText_InOriginal);
    private static GeneratorVerifier ConvertibleVectorGroupIdentical => GeneratorVerifier.Construct<SharpMeasuresGenerator>(ConvertibleVectorGroupIdenticalText);
    private static GeneratorVerifier ConvertibleSpecializedVectorGroupIdentical_InSpecialized => GeneratorVerifier.Construct<SharpMeasuresGenerator>(ConvertibleSpecializedVectorGroupIdenticalText_InSpecialized);
    private static GeneratorVerifier ConvertibleSpecializedVectorGroupIdentical_InOriginal => GeneratorVerifier.Construct<SharpMeasuresGenerator>(ConvertibleSpecializedVectorGroupIdenticalText_InOriginal);
    private static GeneratorVerifier ConvertibleVectorGroupMemberIdentical_InMember => GeneratorVerifier.Construct<SharpMeasuresGenerator>(ConvertibleVectorGroupMemberIdenticalText_InMember);
    private static GeneratorVerifier ConvertibleVectorGroupMemberIdentical_InGroup => GeneratorVerifier.Construct<SharpMeasuresGenerator>(ConvertibleVectorGroupMemberIdenticalText_InGroup);

    private static string ConvertibleScalarIdenticalText => """
        using SharpMeasures.Generators.Quantities;
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;

        [ConvertibleQuantity(typeof(Length))]
        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Distance { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }

        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static string ConvertibleSpecializedScalarIdenticalText_InSpecialized => """
        using SharpMeasures.Generators.Quantities;
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;

        [ConvertibleQuantity(typeof(Length))]
        [SpecializedSharpMeasuresScalar(typeof(Length))]
        public partial class Distance { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }

        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static string ConvertibleSpecializedScalarIdenticalText_InOriginal => """
        using SharpMeasures.Generators.Quantities;
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;

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

    private static string ConvertibleVectorIdenticalText => """
        using SharpMeasures.Generators.Quantities;
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Vectors;

        [ConvertibleQuantity(typeof(Position3))]
        [SharpMeasuresVector(typeof(UnitOfLength))]
        public partial class Displacement3 { }

        [SharpMeasuresVector(typeof(UnitOfLength))]
        public partial class Position3 { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }

        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static string ConvertibleSpecializedVectorIdenticalText_InSpecialized => """
        using SharpMeasures.Generators.Quantities;
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Vectors;

        [ConvertibleQuantity(typeof(Position3))]
        [SpecializedSharpMeasuresVector(typeof(Position3))]
        public partial class Displacement3 { }

        [SharpMeasuresVector(typeof(UnitOfLength))]
        public partial class Position3 { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }

        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static string ConvertibleSpecializedVectorIdenticalText_InOriginal => """
        using SharpMeasures.Generators.Quantities;
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Vectors;

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

    private static string ConvertibleVectorGroupIdenticalText => """
        using SharpMeasures.Generators.Quantities;
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Vectors;

        [ConvertibleQuantity(typeof(Position))]
        [SharpMeasuresVectorGroup(typeof(UnitOfLength))]
        public static partial class Displacement { }

        [SharpMeasuresVectorGroup(typeof(UnitOfLength))]
        public static partial class Position { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }

        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static string ConvertibleSpecializedVectorGroupIdenticalText_InSpecialized => """
        using SharpMeasures.Generators.Quantities;
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Vectors;

        [ConvertibleQuantity(typeof(Position))]
        [SpecializedSharpMeasuresVectorGroup(typeof(Position))]
        public static partial class Displacement { }
        
        [SharpMeasuresVectorGroup(typeof(UnitOfLength))]
        public static partial class Position { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }

        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static string ConvertibleSpecializedVectorGroupIdenticalText_InOriginal => """
        using SharpMeasures.Generators.Quantities;
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Vectors;
        
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

    private static string ConvertibleVectorGroupMemberIdenticalText_InMember => """
        using SharpMeasures.Generators.Quantities;
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Vectors;

        [ConvertibleQuantity(typeof(Displacement3))]
        [SharpMeasuresVectorGroupMember(typeof(Position))]
        public partial class Position3 { }
        
        [SharpMeasuresVectorGroup(typeof(UnitOfLength))]
        public static partial class Position { }

        [SharpMeasuresVector(typeof(UnitOfLength))]
        public partial class Displacement3 { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }

        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static string ConvertibleVectorGroupMemberIdenticalText_InGroup => """
        using SharpMeasures.Generators.Quantities;
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Vectors;
        
        [SharpMeasuresVectorGroupMember(typeof(Position))]
        public partial class Position3 { }
        
        [ConvertibleQuantity(typeof(Displacement))]
        [SharpMeasuresVectorGroup(typeof(UnitOfLength))]
        public static partial class Position { }
        
        [SharpMeasuresVectorGroupMember(typeof(Displacement))]
        public partial class Displacement3 { }
        
        [SharpMeasuresVectorGroup(typeof(UnitOfLength))]
        public static partial class Displacement { }
        
        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }
        
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;
}
