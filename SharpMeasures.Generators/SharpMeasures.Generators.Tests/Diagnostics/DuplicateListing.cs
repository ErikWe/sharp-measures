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

    [Theory]
    [InlineData("ForwardsCastOperatorBehaviour")]
    [InlineData("BackwardsCastOperatorBehaviour")]
    public void ConvertibleSpecializedScalar_Property(string property) => AssertConvertibleSpecializedScalar_Property(property);

    [Fact]
    public void ConvertibleVector_SingleAttribute() => AssertConvertibleVector_SingleAttribute();

    [Fact]
    public void ConvertibleVector_MultipleAttributes() => AssertConvertibleVector_MultipleAttributes();

    [Fact]
    public void ConvertibleSpecializedVector_SingleAttribute() => AssertConvertibleSpecializedVector_SingleAttribute();

    [Fact]
    public void ConvertibleSpecializedVector_MultipleAttributes() => AssertConvertibleSpecializedVector_MultipleAttributes();

    [Theory]
    [InlineData("ForwardsCastOperatorBehaviour")]
    [InlineData("BackwardsCastOperatorBehaviour")]
    public void ConvertibleSpecializedVector_Property(string property) => AssertConvertibleSpecializedVector_Property(property);

    [Fact]
    public void ConvertibleVectorGroup_SingleAttribute() => AssertConvertibleVectorGroup_SingleAttribute();

    [Fact]
    public void ConvertibleVectorGroup_MultipleAttributes() => AssertConvertibleVectorGroup_MultipleAttributes();

    [Fact]
    public void ConvertibleSpecializedVectorGroup_SingleAttribute() => AssertConvertibleSpecializedVectorGroup_SingleAttribute();

    [Fact]
    public void ConvertibleSpecializedVectorGroup_MultipleAttributes() => AssertConvertibleSpecializedVectorGroup_MultipleAttributes();

    [Theory]
    [InlineData("ForwardsCastOperatorBehaviour")]
    [InlineData("BackwardsCastOperatorBehaviour")]
    public void ConvertibleSpecializedVectorGroup_Property(string property) => AssertConvertibleSpecializedVectorGroup_Property(property);

    [Fact]
    public void ConvertibleVectorGroupMember_SingleAttribute() => AssertConvertibleVectorGroupMember_SingleAttribute();

    [Fact]
    public void ConvertibleVectorGroupMember_MultipleAttributes() => AssertConvertibleVectorGroupMember_MultipleAttributes();

    private static GeneratorVerifier AssertExactlyDuplicateListingDiagnostics(string source) => GeneratorVerifier.Construct<SharpMeasuresGenerator>(source).AssertExactlyListedDiagnosticsIDsReported(DuplicateListingDiagnostics);
    private static IReadOnlyCollection<string> DuplicateListingDiagnostics { get; } = new string[] { DiagnosticIDs.DuplicateListing };

    private static string ConvertibleScalarText_SingleAttribute => """
        using SharpMeasures.Generators;

        [ConvertibleQuantity(typeof(Length), typeof(Length))]
        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Distance { }

        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }

        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertConvertibleScalar_SingleAttribute()
    {
        var expectedLocation = ExpectedDiagnosticsLocation.AsTypeofArgumentTextSpan(ConvertibleScalarText_SingleAttribute, target: "Length", prefix: "ConvertibleQuantity(typeof(Length), ");

        return AssertExactlyDuplicateListingDiagnostics(ConvertibleScalarText_SingleAttribute).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(ConvertibleScalarIdentical);
    }

    private static string ConvertibleScalarText_MultipleAttributes => """
        using SharpMeasures.Generators;

        [ConvertibleQuantity(typeof(Length))]
        [ConvertibleQuantity(typeof(Length))] // <-
        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Distance { }

        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }

        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertConvertibleScalar_MultipleAttributes()
    {
        var expectedLocation = ExpectedDiagnosticsLocation.AsTypeofArgumentTextSpan(ConvertibleScalarText_MultipleAttributes, target: "Length", prefix: "ConvertibleQuantity(", postfix: ")] // <-");

        return AssertExactlyDuplicateListingDiagnostics(ConvertibleScalarText_MultipleAttributes).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(ConvertibleScalarIdentical);
    }

    private static string ConvertibleSpecializedScalarText_SingleAttribute => """
        using SharpMeasures.Generators;

        [ConvertibleQuantity(typeof(Length), typeof(Length))]
        [SpecializedScalarQuantity(typeof(Length))]
        public partial class Distance { }

        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }

        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertConvertibleSpecializedScalar_SingleAttribute()
    {
        var expectedLocation = ExpectedDiagnosticsLocation.AsTypeofArgumentTextSpan(ConvertibleSpecializedScalarText_SingleAttribute, target: "Length", prefix: "ConvertibleQuantity(typeof(Length), ");

        return AssertExactlyDuplicateListingDiagnostics(ConvertibleSpecializedScalarText_SingleAttribute).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(ConvertibleSpecializedScalarIdentical_InSpecialized);
    }

    private static string ConvertibleSpecializedScalarText_MultipleAttributes => """
        using SharpMeasures.Generators;

        [ConvertibleQuantity(typeof(Length))]
        [ConvertibleQuantity(typeof(Length))] // <-
        [SpecializedScalarQuantity(typeof(Length))]
        public partial class Distance { }

        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }

        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertConvertibleSpecializedScalar_MultipleAttributes()
    {
        var expectedLocation = ExpectedDiagnosticsLocation.AsTypeofArgumentTextSpan(ConvertibleSpecializedScalarText_MultipleAttributes, target: "Length", prefix: "ConvertibleQuantity(", postfix: ")] // <-");

        return AssertExactlyDuplicateListingDiagnostics(ConvertibleSpecializedScalarText_MultipleAttributes).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(ConvertibleSpecializedScalarIdentical_InSpecialized);
    }

    private static string ConvertibleSpecializedScalarText_Property(string property) => $$"""
        using SharpMeasures.Generators;
        
        [ConvertibleQuantity(typeof(Length), ConversionDirection = QuantityConversionDirection.Bidirectional, CastOperatorBehaviour = ConversionOperatorBehaviour.Implicit)]
        [SpecializedScalarQuantity(typeof(Length), {{property}} = ConversionOperatorBehaviour.Explicit)]
        public partial class Distance { }
        
        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }
        
        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertConvertibleSpecializedScalar_Property(string property)
    {
        var source = ConvertibleSpecializedScalarText_Property(property);
        var expectedLocation = ExpectedDiagnosticsLocation.AsTypeofArgumentTextSpan(source, target: "Length", prefix: "ConvertibleQuantity(");

        return AssertExactlyDuplicateListingDiagnostics(source).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(ConvertibleSpecializedScalarIdentical_Property(property));
    }

    private static string ConvertibleVectorText_SingleAttribute => """
        using SharpMeasures.Generators;

        [ConvertibleQuantity(typeof(Position3), typeof(Position3))]
        [VectorQuantity(typeof(UnitOfLength))]
        public partial class Displacement3 { }

        [VectorQuantity(typeof(UnitOfLength))]
        public partial class Position3 { }

        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }

        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertConvertibleVector_SingleAttribute()
    {
        var expectedLocation = ExpectedDiagnosticsLocation.AsTypeofArgumentTextSpan(ConvertibleVectorText_SingleAttribute, target: "Position3", prefix: "ConvertibleQuantity(typeof(Position3), ");

        return AssertExactlyDuplicateListingDiagnostics(ConvertibleVectorText_SingleAttribute).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(ConvertibleVectorIdentical);
    }

    private static string ConvertibleVectorText_MultipleAttributes => """
        using SharpMeasures.Generators;

        [ConvertibleQuantity(typeof(Position3))]
        [ConvertibleQuantity(typeof(Position3))] // <-
        [VectorQuantity(typeof(UnitOfLength))]
        public partial class Displacement3 { }

        [VectorQuantity(typeof(UnitOfLength))]
        public partial class Position3 { }

        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }

        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertConvertibleVector_MultipleAttributes()
    {
        var expectedLocation = ExpectedDiagnosticsLocation.AsTypeofArgumentTextSpan(ConvertibleVectorText_MultipleAttributes, target: "Position3", prefix: "ConvertibleQuantity(", postfix: ")] // <-");

        return AssertExactlyDuplicateListingDiagnostics(ConvertibleVectorText_MultipleAttributes).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(ConvertibleVectorIdentical);
    }

    private static string ConvertibleSpecializedVectorText_SingleAttribute => """
        using SharpMeasures.Generators;

        [ConvertibleQuantity(typeof(Position3), typeof(Position3))]
        [SpecializedVectorQuantity(typeof(Position3))]
        public partial class Displacement3 { }

        [VectorQuantity(typeof(UnitOfLength))]
        public partial class Position3 { }

        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }

        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertConvertibleSpecializedVector_SingleAttribute()
    {
        var expectedLocation = ExpectedDiagnosticsLocation.AsTypeofArgumentTextSpan(ConvertibleSpecializedVectorText_SingleAttribute, target: "Position3", prefix: "ConvertibleQuantity(typeof(Position3), ");

        return AssertExactlyDuplicateListingDiagnostics(ConvertibleSpecializedVectorText_SingleAttribute).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(ConvertibleSpecializedVectorIdentical_InSpecialized);
    }

    private static string ConvertibleSpecializedVectorText_MultipleAttributes => """
        using SharpMeasures.Generators;

        [ConvertibleQuantity(typeof(Position3))]
        [ConvertibleQuantity(typeof(Position3))] // <-
        [SpecializedVectorQuantity(typeof(Position3))]
        public partial class Displacement3 { }

        [VectorQuantity(typeof(UnitOfLength))]
        public partial class Position3 { }

        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }

        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertConvertibleSpecializedVector_MultipleAttributes()
    {
        var expectedLocation = ExpectedDiagnosticsLocation.AsTypeofArgumentTextSpan(ConvertibleSpecializedVectorText_MultipleAttributes, target: "Position3", prefix: "ConvertibleQuantity(", postfix: ")] // <-");

        return AssertExactlyDuplicateListingDiagnostics(ConvertibleSpecializedVectorText_MultipleAttributes).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(ConvertibleSpecializedVectorIdentical_InSpecialized);
    }

    private static string ConvertibleSpecializedVectorText_Property(string property) => $$"""
        using SharpMeasures.Generators;
        
        [ConvertibleQuantity(typeof(Position3), ConversionDirection = QuantityConversionDirection.Bidirectional, CastOperatorBehaviour = ConversionOperatorBehaviour.Explicit)]
        [SpecializedVectorQuantity(typeof(Position3), {{property}} = ConversionOperatorBehaviour.Implicit)]
        public partial class Displacement3 { }

        [VectorQuantity(typeof(UnitOfLength))]
        public partial class Position3 { }
        
        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }
        
        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertConvertibleSpecializedVector_Property(string property)
    {
        var source = ConvertibleSpecializedVectorText_Property(property);
        var expectedLocation = ExpectedDiagnosticsLocation.AsTypeofArgumentTextSpan(source, target: "Position3", prefix: "ConvertibleQuantity(");

        return AssertExactlyDuplicateListingDiagnostics(source).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(ConvertibleSpecializedVectorIdentical_Property(property));
    }

    private static string ConvertibleVectorGroupText_SingleAttribute => """
        using SharpMeasures.Generators;

        [ConvertibleQuantity(typeof(Position), typeof(Position))]
        [VectorGroup(typeof(UnitOfLength))]
        public static partial class Displacement { }

        [VectorGroup(typeof(UnitOfLength))]
        public static partial class Position { }

        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }

        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertConvertibleVectorGroup_SingleAttribute()
    {
        var expectedLocation = ExpectedDiagnosticsLocation.AsTypeofArgumentTextSpan(ConvertibleVectorGroupText_SingleAttribute, target: "Position", prefix: "ConvertibleQuantity(typeof(Position), ");

        return AssertExactlyDuplicateListingDiagnostics(ConvertibleVectorGroupText_SingleAttribute).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(ConvertibleVectorGroupIdentical);
    }

    private static string ConvertibleVectorGroupText_MultipleAttributes => """
        using SharpMeasures.Generators;

        [ConvertibleQuantity(typeof(Position))]
        [ConvertibleQuantity(typeof(Position))] // <-
        [VectorGroup(typeof(UnitOfLength))]
        public static partial class Displacement { }

        [VectorGroup(typeof(UnitOfLength))]
        public static partial class Position { }

        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }

        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertConvertibleVectorGroup_MultipleAttributes()
    {
        var expectedLocation = ExpectedDiagnosticsLocation.AsTypeofArgumentTextSpan(ConvertibleVectorGroupText_MultipleAttributes, target: "Position", prefix: "ConvertibleQuantity(", postfix: ")] // <-");

        return AssertExactlyDuplicateListingDiagnostics(ConvertibleVectorGroupText_MultipleAttributes).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(ConvertibleVectorGroupIdentical);
    }

    private static string ConvertibleSpecializedVectorGroupText_SingleAttribute => """
        using SharpMeasures.Generators;

        [ConvertibleQuantity(typeof(Position), typeof(Position))]
        [SpecializedVectorGroup(typeof(Position))]
        public static partial class Displacement { }
        
        [VectorGroup(typeof(UnitOfLength))]
        public static partial class Position { }

        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }

        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertConvertibleSpecializedVectorGroup_SingleAttribute()
    {
        var expectedLocation = ExpectedDiagnosticsLocation.AsTypeofArgumentTextSpan(ConvertibleSpecializedVectorGroupText_SingleAttribute, target: "Position", prefix: "ConvertibleQuantity(typeof(Position), ");

        return AssertExactlyDuplicateListingDiagnostics(ConvertibleSpecializedVectorGroupText_SingleAttribute).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(ConvertibleSpecializedVectorGroupIdentical_InSpecialized);
    }

    private static string ConvertibleSpecializedVectorGroupText_MultipleAttributes => """
        using SharpMeasures.Generators;

        [ConvertibleQuantity(typeof(Position))]
        [ConvertibleQuantity(typeof(Position))] // <-
        [SpecializedVectorGroup(typeof(Position))]
        public static partial class Displacement { }
        
        [VectorGroup(typeof(UnitOfLength))]
        public static partial class Position { }

        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }

        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertConvertibleSpecializedVectorGroup_MultipleAttributes()
    {
        var expectedLocation = ExpectedDiagnosticsLocation.AsTypeofArgumentTextSpan(ConvertibleSpecializedVectorGroupText_MultipleAttributes, target: "Position", prefix: "ConvertibleQuantity(", postfix: ")] // <-");

        return AssertExactlyDuplicateListingDiagnostics(ConvertibleSpecializedVectorGroupText_MultipleAttributes).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(ConvertibleSpecializedVectorGroupIdentical_InSpecialized);
    }

    private static string ConvertibleSpecializedVectorGroupText_Property(string property) => $$"""
        using SharpMeasures.Generators;
        
        [ConvertibleQuantity(typeof(Position), ConversionDirection = QuantityConversionDirection.Bidirectional, CastOperatorBehaviour = ConversionOperatorBehaviour.Explicit)]
        [SpecializedVectorGroup(typeof(Position), {{property}} = ConversionOperatorBehaviour.Implicit)]
        public static partial class Displacement { }

        [VectorGroup(typeof(UnitOfLength))]
        public static partial class Position { }
        
        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }
        
        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertConvertibleSpecializedVectorGroup_Property(string property)
    {
        var source = ConvertibleSpecializedVectorGroupText_Property(property);
        var expectedLocation = ExpectedDiagnosticsLocation.AsTypeofArgumentTextSpan(source, target: "Position", prefix: "ConvertibleQuantity(");

        return AssertExactlyDuplicateListingDiagnostics(source).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(ConvertibleSpecializedVectorGroupIdentical_Property(property));
    }

    private static string ConvertibleVectorGroupMemberText_SingleAttribute => """
        using SharpMeasures.Generators;

        [ConvertibleQuantity(typeof(Displacement3), typeof(Displacement3))]
        [VectorGroupMember(typeof(Position))]
        public partial class Position3 { }
        
        [VectorGroup(typeof(UnitOfLength))]
        public static partial class Position { }
        
        [VectorQuantity(typeof(UnitOfLength))]
        public partial class Displacement3 { }

        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }
        
        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertConvertibleVectorGroupMember_SingleAttribute()
    {
        var expectedLocation = ExpectedDiagnosticsLocation.AsTypeofArgumentTextSpan(ConvertibleVectorGroupMemberText_SingleAttribute, target: "Displacement3", prefix: "ConvertibleQuantity(typeof(Displacement3), ");

        return AssertExactlyDuplicateListingDiagnostics(ConvertibleVectorGroupMemberText_SingleAttribute).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(ConvertibleVectorGroupMemberIdentical_InMember);
    }

    private static string ConvertibleVectorGroupMemberText_MultipleAttributes => """
        using SharpMeasures.Generators;

        [ConvertibleQuantity(typeof(Displacement3))]
        [ConvertibleQuantity(typeof(Displacement3))] // <-
        [VectorGroupMember(typeof(Position))]
        public partial class Position3 { }
        
        [VectorGroup(typeof(UnitOfLength))]
        public static partial class Position { }

        [VectorQuantity(typeof(UnitOfLength))]
        public partial class Displacement3 { }

        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }

        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertConvertibleVectorGroupMember_MultipleAttributes()
    {
        var expectedLocation = ExpectedDiagnosticsLocation.AsTypeofArgumentTextSpan(ConvertibleVectorGroupMemberText_MultipleAttributes, target: "Displacement3", postfix: ")] // <-");

        return AssertExactlyDuplicateListingDiagnostics(ConvertibleVectorGroupMemberText_MultipleAttributes).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(ConvertibleVectorGroupMemberIdentical_InMember);
    }

    private static GeneratorVerifier ConvertibleScalarIdentical => GeneratorVerifier.Construct<SharpMeasuresGenerator>(ConvertibleScalarIdenticalText);
    private static GeneratorVerifier ConvertibleSpecializedScalarIdentical_InSpecialized => GeneratorVerifier.Construct<SharpMeasuresGenerator>(ConvertibleSpecializedScalarIdenticalText_InSpecialized);
    private static GeneratorVerifier ConvertibleSpecializedScalarIdentical_Property(string property) => GeneratorVerifier.Construct<SharpMeasuresGenerator>(ConvertibleSpecializedScalarIdenticalText_Property(property));
    private static GeneratorVerifier ConvertibleVectorIdentical => GeneratorVerifier.Construct<SharpMeasuresGenerator>(ConvertibleVectorIdenticalText);
    private static GeneratorVerifier ConvertibleSpecializedVectorIdentical_InSpecialized => GeneratorVerifier.Construct<SharpMeasuresGenerator>(ConvertibleSpecializedVectorIdenticalText_InSpecialized);
    private static GeneratorVerifier ConvertibleSpecializedVectorIdentical_Property(string property) => GeneratorVerifier.Construct<SharpMeasuresGenerator>(ConvertibleSpecializedVectorIdenticalText_Property(property));
    private static GeneratorVerifier ConvertibleVectorGroupIdentical => GeneratorVerifier.Construct<SharpMeasuresGenerator>(ConvertibleVectorGroupIdenticalText);
    private static GeneratorVerifier ConvertibleSpecializedVectorGroupIdentical_InSpecialized => GeneratorVerifier.Construct<SharpMeasuresGenerator>(ConvertibleSpecializedVectorGroupIdenticalText_InSpecialized);
    private static GeneratorVerifier ConvertibleSpecializedVectorGroupIdentical_Property(string property) => GeneratorVerifier.Construct<SharpMeasuresGenerator>(ConvertibleSpecializedVectorGroupIdenticalText_Property(property));
    private static GeneratorVerifier ConvertibleVectorGroupMemberIdentical_InMember => GeneratorVerifier.Construct<SharpMeasuresGenerator>(ConvertibleVectorGroupMemberIdenticalText_InMember);

    private static string ConvertibleScalarIdenticalText => """
        using SharpMeasures.Generators;

        [ConvertibleQuantity(typeof(Length))]
        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Distance { }

        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }

        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static string ConvertibleSpecializedScalarIdenticalText_InSpecialized => """
        using SharpMeasures.Generators;

        [ConvertibleQuantity(typeof(Length))]
        [SpecializedScalarQuantity(typeof(Length))]
        public partial class Distance { }

        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }

        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static string ConvertibleSpecializedScalarIdenticalText_Property(string property) => $$"""
        using SharpMeasures.Generators;

        [SpecializedScalarQuantity(typeof(Length), {{property}} = ConversionOperatorBehaviour.Explicit)]
        public partial class Distance { }

        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }

        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static string ConvertibleVectorIdenticalText => """
        using SharpMeasures.Generators;

        [ConvertibleQuantity(typeof(Position3))]
        [VectorQuantity(typeof(UnitOfLength))]
        public partial class Displacement3 { }

        [VectorQuantity(typeof(UnitOfLength))]
        public partial class Position3 { }

        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }

        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static string ConvertibleSpecializedVectorIdenticalText_InSpecialized => """
        using SharpMeasures.Generators;

        [ConvertibleQuantity(typeof(Position3))]
        [SpecializedVectorQuantity(typeof(Position3))]
        public partial class Displacement3 { }

        [VectorQuantity(typeof(UnitOfLength))]
        public partial class Position3 { }

        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }

        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static string ConvertibleSpecializedVectorIdenticalText_Property(string property) => $$"""
        using SharpMeasures.Generators;

        [SpecializedVectorQuantity(typeof(Position3), {{property}} = ConversionOperatorBehaviour.Implicit)]
        public partial class Displacement3 { }

        [VectorQuantity(typeof(UnitOfLength))]
        public partial class Position3 { }

        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }

        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static string ConvertibleVectorGroupIdenticalText => """
        using SharpMeasures.Generators;

        [ConvertibleQuantity(typeof(Position))]
        [VectorGroup(typeof(UnitOfLength))]
        public static partial class Displacement { }

        [VectorGroup(typeof(UnitOfLength))]
        public static partial class Position { }

        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }

        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static string ConvertibleSpecializedVectorGroupIdenticalText_InSpecialized => """
        using SharpMeasures.Generators;

        [ConvertibleQuantity(typeof(Position))]
        [SpecializedVectorGroup(typeof(Position))]
        public static partial class Displacement { }
        
        [VectorGroup(typeof(UnitOfLength))]
        public static partial class Position { }

        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }

        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static string ConvertibleSpecializedVectorGroupIdenticalText_Property(string property) => $$"""
        using SharpMeasures.Generators;

        [SpecializedVectorGroup(typeof(Position), {{property}} = ConversionOperatorBehaviour.Implicit)]
        public static partial class Displacement { }

        [VectorGroup(typeof(UnitOfLength))]
        public static partial class Position { }

        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }

        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static string ConvertibleVectorGroupMemberIdenticalText_InMember => """
        using SharpMeasures.Generators;

        [ConvertibleQuantity(typeof(Displacement3))]
        [VectorGroupMember(typeof(Position))]
        public partial class Position3 { }
        
        [VectorGroup(typeof(UnitOfLength))]
        public static partial class Position { }

        [VectorQuantity(typeof(UnitOfLength))]
        public partial class Displacement3 { }

        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }

        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;
}
