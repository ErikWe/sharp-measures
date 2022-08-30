namespace SharpMeasures.Generators.Tests.Diagnostics.Quantities;

using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Tests.Utility;
using SharpMeasures.Generators.Tests.Verify;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using VerifyXunit;

using Xunit;

[UsesVerify]
public class InclusionOrExclusionHadNoEffect
{
    [Fact]
    public Task VerifyInclusionOrExclusionHadNoEffectDiagnosticsMessage_AlreadyIncluded() => AssertScalar_SameAttribute(IncludeBasesAttribute).VerifyDiagnostics();

    [Fact]
    public Task VerifyInclusionOrExclusionHadNoEffectDiagnosticsMessage_NotExcluded() => AssertSpecializedScalar_Inherited(IncludeBasesAttribute).VerifyDiagnostics();

    [Fact]
    public Task VerifyInclusionOrExclusionHadNoEffectDiagnosticsMessage_AlreadyExcluded() => AssertScalar_SameAttribute(ExcludeBasesAttribute).VerifyDiagnostics();

    [Theory]
    [MemberData(nameof(ScalarAttributes))]
    public void Scalar_SameAttribute(string attribute) => AssertScalar_SameAttribute(attribute);

    [Theory]
    [MemberData(nameof(ScalarAttributes))]
    public void Scalar_MultipleAttributes(string attribute) => AssertScalar_MultipleAttributes(attribute);

    [Theory]
    [MemberData(nameof(ScalarAttributes))]
    public void SpecializedScalar_SameAttribute(string attribute) => AssertSpecializedScalar_SameAttribute(attribute);

    [Theory]
    [MemberData(nameof(ScalarAttributes))]
    public void SpecializedScalar_MultipleAttributes(string attribute) => AssertSpecializedScalar_MultipleAttributes(attribute);

    [Theory]
    [MemberData(nameof(ScalarAttributes))]
    public void SpecializedScalar_Inherited(string attribute) => AssertSpecializedScalar_Inherited(attribute);

    [Theory]
    [MemberData(nameof(VectorAttributes))]
    public void Vector_SameAttribute(string attribute) => AssertVector_SameAttribute(attribute);

    [Theory]
    [MemberData(nameof(VectorAttributes))]
    public void Vector_MultipleAttributes(string attribute) => AssertVector_MultipleAttributes(attribute);

    [Theory]
    [MemberData(nameof(VectorAttributes))]
    public void SpecializedVector_SameAttribute(string attribute) => AssertSpecializedVector_SameAttribute(attribute);

    [Theory]
    [MemberData(nameof(VectorAttributes))]
    public void SpecializedVector_MultipleAttributes(string attribute) => AssertSpecializedVector_MultipleAttributes(attribute);

    [Theory]
    [MemberData(nameof(VectorAttributes))]
    public void SpecializedVector_Inherited(string attribute) => AssertSpecializedVector_Inherited(attribute);

    [Theory]
    [MemberData(nameof(VectorAttributes))]
    public void VectorGroup_SameAttribute(string attribute) => AssertVectorGroup_SameAttribute(attribute);

    [Theory]
    [MemberData(nameof(VectorAttributes))]
    public void VectorGroup_MultipleAttributes(string attribute) => AssertVectorGroup_MultipleAttributes(attribute);

    [Theory]
    [MemberData(nameof(VectorAttributes))]
    public void SpecializedVectorGroup_SameAttribute(string attribute) => AssertSpecializedVectorGroup_SameAttribute(attribute);

    [Theory]
    [MemberData(nameof(VectorAttributes))]
    public void SpecializedVectorGroup_MultipleAttributes(string attribute) => AssertSpecializedVectorGroup_MultipleAttributes(attribute);

    [Theory]
    [MemberData(nameof(VectorAttributes))]
    public void SpecializedVectorGroup_Inherited(string attribute) => AssertSpecializedVectorGroup_Inherited(attribute);

    [Theory]
    [MemberData(nameof(VectorAttributes))]
    public void VectorGroupMember_SameAttribute(string attribute) => AssertVectorGroupMember_SameAttribute(attribute);

    [Theory]
    [MemberData(nameof(VectorAttributes))]
    public void VectorGroupMember_MultipleAttributes(string attribute) => AssertVectorGroupMember_MultipleAttributes(attribute);
    
    [Theory]
    [MemberData(nameof(VectorAttributes))]
    public void VectorGroupMember_InheritedFromGroup(string attribute) => AssertVectorGroupMember_InheritedFromGroup(attribute);

    [Theory]
    [MemberData(nameof(VectorAttributes))]
    public void VectorGroupMember_InheritedFromMember(string attribute) => AssertVectorGroupMember_InheritedFromMember(attribute);

    public static IEnumerable<object[]> ScalarAttributes => new object[][]
    {
        new[] { IncludeUnitsAttribute },
        new[] { ExcludeUnitsAttribute },
        new[] { IncludeBasesAttribute },
        new[] { ExcludeBasesAttribute }
    };

    public static IEnumerable<object[]> VectorAttributes => new object[][]
    {
        new[] { IncludeUnitsAttribute },
        new[] { ExcludeUnitsAttribute }
    };

    private static string IncludeUnitsAttribute { get; } = "IncludeUnits";
    private static string ExcludeUnitsAttribute { get; } = "ExcludeUnits";
    private static string IncludeBasesAttribute { get; } = "IncludeBases";
    private static string ExcludeBasesAttribute { get; } = "ExcludeBases";

    private static GeneratorVerifier AssertExactlyInclusionOrExclusionHadNoEffectDiagnostics(string source) => GeneratorVerifier.Construct<SharpMeasuresGenerator>(source).AssertExactlyListedDiagnosticsIDsReported(InclusionOrExclusionHadNoEffectDiagnostics);
    private static IReadOnlyCollection<string> InclusionOrExclusionHadNoEffectDiagnostics { get; } = new string[] { DiagnosticIDs.InclusionOrExclusionHadNoEffect };

    private static string ScalarText_SameAttribute(string attribute) => $$"""
        using SharpMeasures.Generators.Quantities;
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;

        [{{attribute}}("Metre", "Metre")]
        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }
            
        [FixedUnit("Metre", "Metres")]
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertScalar_SameAttribute(string attribute)
    {
        var source = ScalarText_SameAttribute(attribute);
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(source, target: "\"Metre\"", prefix: $"{attribute}(\"Metre\", ");

        return AssertExactlyInclusionOrExclusionHadNoEffectDiagnostics(source).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(ScalarIdentical_SameAttribute(attribute));
    }

    private static string ScalarText_MultipleAttributes(string attribute) => $$"""
        using SharpMeasures.Generators.Quantities;
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;

        [{{attribute}}("Metre")]
        [{{attribute}}("Metre")] // <-
        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }
            
        [FixedUnit("Metre", "Metres")]
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertScalar_MultipleAttributes(string attribute)
    {
        var source = ScalarText_MultipleAttributes(attribute);
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(source, target: "\"Metre\"", postfix: ")] // <-");

        return AssertExactlyInclusionOrExclusionHadNoEffectDiagnostics(source).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(ScalarIdentical_MultipleAttributes(attribute));
    }

    private static string SpecializedScalarText_SameAttribute(string attribute) => $$"""
        using SharpMeasures.Generators.Quantities;
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;

        [{{attribute}}("Metre", "Metre")]
        [SpecializedSharpMeasuresScalar(typeof(Length))]
        public partial class Distance { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }
            
        [FixedUnit("Metre", "Metres")]
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertSpecializedScalar_SameAttribute(string attribute)
    {
        var source = SpecializedScalarText_SameAttribute(attribute);
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(source, target: "\"Metre\"", prefix: $"{attribute}(\"Metre\", ");

        return AssertExactlyInclusionOrExclusionHadNoEffectDiagnostics(source).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(SpecializedScalarIdentical_SameAttribute(attribute));
    }

    private static string SpecializedScalarText_MultipleAttributes(string attribute) => $$"""
        using SharpMeasures.Generators.Quantities;
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;

        [{{attribute}}("Metre")]
        [{{attribute}}("Metre")] // <-
        [SpecializedSharpMeasuresScalar(typeof(Length))]
        public partial class Distance { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }
            
        [FixedUnit("Metre", "Metres")]
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertSpecializedScalar_MultipleAttributes(string attribute)
    {
        var source = SpecializedScalarText_MultipleAttributes(attribute);
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(source, target: "\"Metre\"", postfix: ")] // <-");

        return AssertExactlyInclusionOrExclusionHadNoEffectDiagnostics(source).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(SpecializedScalarIdentical_MultipleAttributes(attribute));
    }

    private static string SpecializedScalarText_Inherited(string attribute) => $$"""
        using SharpMeasures.Generators.Quantities;
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Units.Utility;
        using SharpMeasures.Generators.Utility;

        [{{attribute}}("Metre"{{(attribute.StartsWith("Include", StringComparison.InvariantCulture) ? ", StackingMode = InclusionStackingMode.Union" : string.Empty)}})] // <-
        [SpecializedSharpMeasuresScalar(typeof(Length))]
        public partial class Distance { }

        [{{attribute}}("Metre")]
        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }
        
        [FixedUnit("Metre", "Metres")]
        [PrefixedUnit("Kilometre", "Kilometres", "Metre", MetricPrefixName.Kilo)]
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertSpecializedScalar_Inherited(string attribute)
    {
        var source = SpecializedScalarText_Inherited(attribute);
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(source, target: "\"Metre\"", postfix: $"{(attribute.StartsWith("Include", StringComparison.InvariantCulture) ? ", StackingMode = InclusionStackingMode.Union" : string.Empty)})] // <-");

        return AssertExactlyInclusionOrExclusionHadNoEffectDiagnostics(source).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(SpecializedScalarIdentical_Inherited(attribute));
    }

    private static string VectorText_SameAttribute(string attribute) => $$"""
        using SharpMeasures.Generators.Quantities;
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Vectors;

        [{{attribute}}("Metre", "Metre")]
        [SharpMeasuresVector(typeof(UnitOfLength))]
        public partial class Position3 { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }
            
        [FixedUnit("Metre", "Metres")]
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertVector_SameAttribute(string attribute)
    {
        var source = VectorText_SameAttribute(attribute);
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(source, target: "\"Metre\"", prefix: $"{attribute}(\"Metre\", ");

        return AssertExactlyInclusionOrExclusionHadNoEffectDiagnostics(source).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(VectorIdentical_SameAttribute(attribute));
    }

    private static string VectorText_MultipleAttributes(string attribute) => $$"""
        using SharpMeasures.Generators.Quantities;
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Vectors;

        [{{attribute}}("Metre")]
        [{{attribute}}("Metre")] // <-
        [SharpMeasuresVector(typeof(UnitOfLength))]
        public partial class Position3 { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }
            
        [FixedUnit("Metre", "Metres")]
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertVector_MultipleAttributes(string attribute)
    {
        var source = VectorText_MultipleAttributes(attribute);
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(source, target: "\"Metre\"", postfix: ")] // <-");

        return AssertExactlyInclusionOrExclusionHadNoEffectDiagnostics(source).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(VectorIdentical_MultipleAttributes(attribute));
    }

    private static string SpecializedVectorText_SameAttribute(string attribute) => $$"""
        using SharpMeasures.Generators.Quantities;
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Vectors;

        [{{attribute}}("Metre", "Metre")]
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

    private static GeneratorVerifier AssertSpecializedVector_SameAttribute(string attribute)
    {
        var source = SpecializedVectorText_SameAttribute(attribute);
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(source, target: "\"Metre\"", prefix: $"{attribute}(\"Metre\", ");

        return AssertExactlyInclusionOrExclusionHadNoEffectDiagnostics(source).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(SpecializedVectorIdentical_SameAttribute(attribute));
    }

    private static string SpecializedVectorText_MultipleAttributes(string attribute) => $$"""
        using SharpMeasures.Generators.Quantities;
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Vectors;

        [{{attribute}}("Metre")]
        [{{attribute}}("Metre")] // <-
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

    private static GeneratorVerifier AssertSpecializedVector_MultipleAttributes(string attribute)
    {
        var source = SpecializedVectorText_MultipleAttributes(attribute);
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(source, target: "\"Metre\"", postfix: ")] // <-");

        return AssertExactlyInclusionOrExclusionHadNoEffectDiagnostics(source).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(SpecializedVectorIdentical_MultipleAttributes(attribute));
    }

    private static string SpecializedVectorText_Inherited(string attribute) => $$"""
        using SharpMeasures.Generators.Quantities;
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Units.Utility;
        using SharpMeasures.Generators.Utility;
        using SharpMeasures.Generators.Vectors;

        [{{attribute}}("Metre"{{(attribute.StartsWith("Include", StringComparison.InvariantCulture) ? ", StackingMode = InclusionStackingMode.Union" : string.Empty)}})] // <-
        [SpecializedSharpMeasuresVector(typeof(Position3))]
        public partial class Displacement3 { }

        [{{attribute}}("Metre")]
        [SharpMeasuresVector(typeof(UnitOfLength))]
        public partial class Position3 { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }
            
        [FixedUnit("Metre", "Metres")]
        [PrefixedUnit("Kilometre", "Kilometres", "Metre", MetricPrefixName.Kilo)]
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertSpecializedVector_Inherited(string attribute)
    {
        var source = SpecializedVectorText_Inherited(attribute);
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(source, target: "\"Metre\"", postfix: $"{(attribute.StartsWith("Include", StringComparison.InvariantCulture) ? ", StackingMode = InclusionStackingMode.Union" : string.Empty)})] // <-");

        return AssertExactlyInclusionOrExclusionHadNoEffectDiagnostics(source).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(SpecializedVectorIdentical_Inherited(attribute));
    }

    private static string VectorGroupText_SameAttribute(string attribute) => $$"""
        using SharpMeasures.Generators.Quantities;
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Vectors;

        [{{attribute}}("Metre", "Metre")]
        [SharpMeasuresVectorGroup(typeof(UnitOfLength))]
        public static partial class Position { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }
            
        [FixedUnit("Metre", "Metres")]
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertVectorGroup_SameAttribute(string attribute)
    {
        var source = VectorGroupText_SameAttribute(attribute);
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(source, target: "\"Metre\"", prefix: $"{attribute}(\"Metre\", ");

        return AssertExactlyInclusionOrExclusionHadNoEffectDiagnostics(source).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(VectorGroupIdentical_SameAttribute(attribute));
    }

    private static string VectorGroupText_MultipleAttributes(string attribute) => $$"""
        using SharpMeasures.Generators.Quantities;
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Vectors;

        [{{attribute}}("Metre")]
        [{{attribute}}("Metre")] // <-
        [SharpMeasuresVectorGroup(typeof(UnitOfLength))]
        public static partial class Position { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }
            
        [FixedUnit("Metre", "Metres")]
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertVectorGroup_MultipleAttributes(string attribute)
    {
        var source = VectorGroupText_MultipleAttributes(attribute);
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(source, target: "\"Metre\"", postfix: ")] // <-");

        return AssertExactlyInclusionOrExclusionHadNoEffectDiagnostics(source).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(VectorGroupIdentical_MultipleAttributes(attribute));
    }

    private static string SpecializedVectorGroupText_SameAttribute(string attribute) => $$"""
        using SharpMeasures.Generators.Quantities;
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Vectors;

        [{{attribute}}("Metre", "Metre")]
        [SpecializedSharpMeasuresVectorGroup(typeof(Position))]
        public static partial class Displacement { }

        [SharpMeasuresVectorGroup(typeof(UnitOfLength))]
        public static partial class Position { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }
            
        [FixedUnit("Metre", "Metres")]
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertSpecializedVectorGroup_SameAttribute(string attribute)
    {
        var source = SpecializedVectorGroupText_SameAttribute(attribute);
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(source, target: "\"Metre\"", prefix: $"{attribute}(\"Metre\", ");

        return AssertExactlyInclusionOrExclusionHadNoEffectDiagnostics(source).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(SpecializedVectorGroupIdentical_SameAttribute(attribute));
    }

    private static string SpecializedVectorGroupText_MultipleAttributes(string attribute) => $$"""
        using SharpMeasures.Generators.Quantities;
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Vectors;

        [{{attribute}}("Metre")]
        [{{attribute}}("Metre")] // <-
        [SpecializedSharpMeasuresVectorGroup(typeof(Position))]
        public static partial class Displacement { }

        [SharpMeasuresVectorGroup(typeof(UnitOfLength))]
        public static partial class Position { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }
            
        [FixedUnit("Metre", "Metres")]
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertSpecializedVectorGroup_MultipleAttributes(string attribute)
    {
        var source = SpecializedVectorGroupText_MultipleAttributes(attribute);
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(source, target: "\"Metre\"", postfix: ")] // <-");

        return AssertExactlyInclusionOrExclusionHadNoEffectDiagnostics(source).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(SpecializedVectorGroupIdentical_MultipleAttributes(attribute));
    }

    private static string SpecializedVectorGroupText_Inherited(string attribute) => $$"""
        using SharpMeasures.Generators.Quantities;
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Units.Utility;
        using SharpMeasures.Generators.Utility;
        using SharpMeasures.Generators.Vectors;

        [{{attribute}}("Metre"{{(attribute.StartsWith("Include", StringComparison.InvariantCulture) ? ", StackingMode = InclusionStackingMode.Union" : string.Empty)}})] // <-
        [SpecializedSharpMeasuresVectorGroup(typeof(Position))]
        public static partial class Displacement { }

        [{{attribute}}("Metre")]
        [SharpMeasuresVectorGroup(typeof(UnitOfLength))]
        public static partial class Position { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }
            
        [FixedUnit("Metre", "Metres")]
        [PrefixedUnit("Kilometre", "Kilometres", "Metre", MetricPrefixName.Kilo)]
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertSpecializedVectorGroup_Inherited(string attribute)
    {
        var source = SpecializedVectorGroupText_Inherited(attribute);
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(source, target: "\"Metre\"", postfix: $"{(attribute.StartsWith("Include", StringComparison.InvariantCulture) ? ", StackingMode = InclusionStackingMode.Union" : string.Empty)})] // <-");

        return AssertExactlyInclusionOrExclusionHadNoEffectDiagnostics(source).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(SpecializedVectorGroupIdentical_Inherited(attribute));
    }

    private static string VectorGroupMemberText_SameAttribute(string attribute) => $$"""
        using SharpMeasures.Generators.Quantities;
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Vectors;

        [{{attribute}}("Metre", "Metre")]
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

    private static GeneratorVerifier AssertVectorGroupMember_SameAttribute(string attribute)
    {
        var source = VectorGroupMemberText_SameAttribute(attribute);
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(source, target: "\"Metre\"", prefix: $"{attribute}(\"Metre\", ");

        return AssertExactlyInclusionOrExclusionHadNoEffectDiagnostics(source).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(VectorGroupMemberIdentical_SameAttribute(attribute));
    }

    private static string VectorGroupMemberText_MultipleAttributes(string attribute) => $$"""
        using SharpMeasures.Generators.Quantities;
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Vectors;

        [{{attribute}}("Metre")]
        [{{attribute}}("Metre")] // <-
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

    private static GeneratorVerifier AssertVectorGroupMember_MultipleAttributes(string attribute)
    {
        var source = VectorGroupMemberText_MultipleAttributes(attribute);
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(source, target: "\"Metre\"", postfix: ")] // <-");

        return AssertExactlyInclusionOrExclusionHadNoEffectDiagnostics(source).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(VectorGroupMemberIdentical_MultipleAttributes(attribute));
    }

    private static string VectorGroupMemberText_InheritedFromGroup(string attribute) => $$"""
        using SharpMeasures.Generators.Quantities;
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Units.Utility;
        using SharpMeasures.Generators.Utility;
        using SharpMeasures.Generators.Vectors;

        [{{attribute}}("Metre"{{(attribute.StartsWith("Include", StringComparison.InvariantCulture) ? ", StackingMode = InclusionStackingMode.Union" : string.Empty)}})] // <-
        [SharpMeasuresVectorGroupMember(typeof(Position))]
        public partial class Position3 { }

        [{{attribute}}("Metre")]
        [SharpMeasuresVectorGroup(typeof(UnitOfLength))]
        public static partial class Position { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }
            
        [FixedUnit("Metre", "Metres")]
        [PrefixedUnit("Kilometre", "Kilometres", "Metre", MetricPrefixName.Kilo)]
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertVectorGroupMember_InheritedFromGroup(string attribute)
    {
        var source = VectorGroupMemberText_InheritedFromGroup(attribute);
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(source, target: "\"Metre\"", postfix: $"{(attribute.StartsWith("Include", StringComparison.InvariantCulture) ? ", StackingMode = InclusionStackingMode.Union" : string.Empty)})] // <-");

        return AssertExactlyInclusionOrExclusionHadNoEffectDiagnostics(source).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(VectorGroupMemberIdentical_InheritedFromGroup(attribute));
    }

    private static string VectorGroupMemberText_InheritedFromMember(string attribute) => $$"""
        using SharpMeasures.Generators.Quantities;
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Units.Utility;
        using SharpMeasures.Generators.Utility;
        using SharpMeasures.Generators.Vectors;

        [{{attribute}}("Metre"{{(attribute.StartsWith("Include", StringComparison.InvariantCulture) ? ", StackingMode = InclusionStackingMode.Union" : string.Empty)}})] // <-
        [SharpMeasuresVectorGroupMember(typeof(Displacement))]
        public partial class Displacement3 { }

        [SpecializedSharpMeasuresVectorGroup(typeof(Position))]
        public static partial class Displacement { }

        [{{attribute}}("Metre")]
        [SharpMeasuresVectorGroupMember(typeof(Position))]
        public partial class Position3 { }

        [SharpMeasuresVectorGroup(typeof(UnitOfLength))]
        public static partial class Position { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }
            
        [FixedUnit("Metre", "Metres")]
        [PrefixedUnit("Kilometre", "Kilometres", "Metre", MetricPrefixName.Kilo)]
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertVectorGroupMember_InheritedFromMember(string attribute)
    {
        var source = VectorGroupMemberText_InheritedFromMember(attribute);
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(source, target: $"\"Metre\"", postfix: $"{(attribute.StartsWith("Include", StringComparison.InvariantCulture) ? ", StackingMode = InclusionStackingMode.Union" : string.Empty)})] // <-");

        return AssertExactlyInclusionOrExclusionHadNoEffectDiagnostics(source).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(VectorGroupMemberIdentical_InheritedFromMember(attribute));
    }

    private static GeneratorVerifier ScalarIdentical_SameAttribute(string attribute) => GeneratorVerifier.Construct<SharpMeasuresGenerator>(ScalarIdenticalText_SameAttribute(attribute));
    private static GeneratorVerifier ScalarIdentical_MultipleAttributes(string attribute) => GeneratorVerifier.Construct<SharpMeasuresGenerator>(ScalarIdenticalText_MultipleAttributes(attribute));
    private static GeneratorVerifier SpecializedScalarIdentical_SameAttribute(string attribute) => GeneratorVerifier.Construct<SharpMeasuresGenerator>(SpecializedScalarIdenticalText_SameAttribute(attribute));
    private static GeneratorVerifier SpecializedScalarIdentical_MultipleAttributes(string attribute) => GeneratorVerifier.Construct<SharpMeasuresGenerator>(SpecializedScalarIdenticalText_MultipleAttributes(attribute));
    private static GeneratorVerifier SpecializedScalarIdentical_Inherited(string attribute) => GeneratorVerifier.Construct<SharpMeasuresGenerator>(SpecializedScalarIdenticalText_Inherited(attribute));
    private static GeneratorVerifier VectorIdentical_SameAttribute(string attribute) => GeneratorVerifier.Construct<SharpMeasuresGenerator>(VectorIdenticalText_SameAttribute(attribute));
    private static GeneratorVerifier VectorIdentical_MultipleAttributes(string attribute) => GeneratorVerifier.Construct<SharpMeasuresGenerator>(VectorIdenticalText_MultipleAttributes(attribute));
    private static GeneratorVerifier SpecializedVectorIdentical_SameAttribute(string attribute) => GeneratorVerifier.Construct<SharpMeasuresGenerator>(SpecializedVectorIdenticalText_SameAttribute(attribute));
    private static GeneratorVerifier SpecializedVectorIdentical_MultipleAttributes(string attribute) => GeneratorVerifier.Construct<SharpMeasuresGenerator>(SpecializedVectorIdenticalText_MultipleAttributes(attribute));
    private static GeneratorVerifier SpecializedVectorIdentical_Inherited(string attribute) => GeneratorVerifier.Construct<SharpMeasuresGenerator>(SpecializedVectorIdenticalText_Inherited(attribute));
    private static GeneratorVerifier VectorGroupIdentical_SameAttribute(string attribute) => GeneratorVerifier.Construct<SharpMeasuresGenerator>(VectorGroupIdenticalText_SameAttribute(attribute));
    private static GeneratorVerifier VectorGroupIdentical_MultipleAttributes(string attribute) => GeneratorVerifier.Construct<SharpMeasuresGenerator>(VectorGroupIdenticalText_MultipleAttributes(attribute));
    private static GeneratorVerifier SpecializedVectorGroupIdentical_SameAttribute(string attribute) => GeneratorVerifier.Construct<SharpMeasuresGenerator>(SpecializedVectorGroupIdenticalText_SameAttribute(attribute));
    private static GeneratorVerifier SpecializedVectorGroupIdentical_MultipleAttributes(string attribute) => GeneratorVerifier.Construct<SharpMeasuresGenerator>(SpecializedVectorGroupIdenticalText_MultipleAttributes(attribute));
    private static GeneratorVerifier SpecializedVectorGroupIdentical_Inherited(string attribute) => GeneratorVerifier.Construct<SharpMeasuresGenerator>(SpecializedVectorGroupIdenticalText_Inherited(attribute));
    private static GeneratorVerifier VectorGroupMemberIdentical_SameAttribute(string attribute) => GeneratorVerifier.Construct<SharpMeasuresGenerator>(VectorGroupMemberIdenticalText_SameAttribute(attribute));
    private static GeneratorVerifier VectorGroupMemberIdentical_MultipleAttributes(string attribute) => GeneratorVerifier.Construct<SharpMeasuresGenerator>(VectorGroupMemberIdenticalText_MultipleAttributes(attribute));
    private static GeneratorVerifier VectorGroupMemberIdentical_InheritedFromGroup(string attribute) => GeneratorVerifier.Construct<SharpMeasuresGenerator>(VectorGroupMemberIdenticalText_InheritedFromGroup(attribute));
    private static GeneratorVerifier VectorGroupMemberIdentical_InheritedFromMember(string attribute) => GeneratorVerifier.Construct<SharpMeasuresGenerator>(VectorGroupMemberIdenticalText_InheritedFromMember(attribute));

    private static string ScalarIdenticalText_SameAttribute(string attribute) => $$"""
        using SharpMeasures.Generators.Quantities;
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;

        [{{attribute}}("Metre")]
        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }
            
        [FixedUnit("Metre", "Metres")]
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static string ScalarIdenticalText_MultipleAttributes(string attribute) => $$"""
        using SharpMeasures.Generators.Quantities;
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;

        [{{attribute}}("Metre")]
        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }
            
        [FixedUnit("Metre", "Metres")]
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static string SpecializedScalarIdenticalText_SameAttribute(string attribute) => $$"""
        using SharpMeasures.Generators.Quantities;
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;

        [{{attribute}}("Metre")]
        [SpecializedSharpMeasuresScalar(typeof(Length))]
        public partial class Distance { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }
            
        [FixedUnit("Metre", "Metres")]
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static string SpecializedScalarIdenticalText_MultipleAttributes(string attribute) => $$"""
        using SharpMeasures.Generators.Quantities;
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;

        [{{attribute}}("Metre")]
        [SpecializedSharpMeasuresScalar(typeof(Length))]
        public partial class Distance { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }
            
        [FixedUnit("Metre", "Metres")]
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static string SpecializedScalarIdenticalText_Inherited(string attribute) => $$"""
        using SharpMeasures.Generators.Quantities;
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Units.Utility;
        using SharpMeasures.Generators.Utility;

        [SpecializedSharpMeasuresScalar(typeof(Length))]
        public partial class Distance { }

        [{{attribute}}("Metre")]
        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }
        
        [FixedUnit("Metre", "Metres")]
        [PrefixedUnit("Kilometre", "Kilometres", "Metre", MetricPrefixName.Kilo)]
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static string VectorIdenticalText_SameAttribute(string attribute) => $$"""
        using SharpMeasures.Generators.Quantities;
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Vectors;

        [{{attribute}}("Metre")]
        [SharpMeasuresVector(typeof(UnitOfLength))]
        public partial class Position3 { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }
            
        [FixedUnit("Metre", "Metres")]
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static string VectorIdenticalText_MultipleAttributes(string attribute) => $$"""
        using SharpMeasures.Generators.Quantities;
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Vectors;

        [{{attribute}}("Metre")]
        [SharpMeasuresVector(typeof(UnitOfLength))]
        public partial class Position3 { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }
            
        [FixedUnit("Metre", "Metres")]
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static string SpecializedVectorIdenticalText_SameAttribute(string attribute) => $$"""
        using SharpMeasures.Generators.Quantities;
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Vectors;

        [{{attribute}}("Metre")]
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

    private static string SpecializedVectorIdenticalText_MultipleAttributes(string attribute) => $$"""
        using SharpMeasures.Generators.Quantities;
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Vectors;

        [{{attribute}}("Metre")]
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

    private static string SpecializedVectorIdenticalText_Inherited(string attribute) => $$"""
        using SharpMeasures.Generators.Quantities;
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Units.Utility;
        using SharpMeasures.Generators.Utility;
        using SharpMeasures.Generators.Vectors;

        [SpecializedSharpMeasuresVector(typeof(Position3))]
        public partial class Displacement3 { }

        [{{attribute}}("Metre")]
        [SharpMeasuresVector(typeof(UnitOfLength))]
        public partial class Position3 { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }
            
        [FixedUnit("Metre", "Metres")]
        [PrefixedUnit("Kilometre", "Kilometres", "Metre", MetricPrefixName.Kilo)]
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static string VectorGroupIdenticalText_SameAttribute(string attribute) => $$"""
        using SharpMeasures.Generators.Quantities;
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Vectors;

        [{{attribute}}("Metre")]
        [SharpMeasuresVectorGroup(typeof(UnitOfLength))]
        public static partial class Position { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }
            
        [FixedUnit("Metre", "Metres")]
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static string VectorGroupIdenticalText_MultipleAttributes(string attribute) => $$"""
        using SharpMeasures.Generators.Quantities;
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Vectors;

        [{{attribute}}("Metre")]
        [SharpMeasuresVectorGroup(typeof(UnitOfLength))]
        public static partial class Position { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }
            
        [FixedUnit("Metre", "Metres")]
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static string SpecializedVectorGroupIdenticalText_SameAttribute(string attribute) => $$"""
        using SharpMeasures.Generators.Quantities;
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Vectors;

        [{{attribute}}("Metre")]
        [SpecializedSharpMeasuresVectorGroup(typeof(Position))]
        public static partial class Displacement { }

        [SharpMeasuresVectorGroup(typeof(UnitOfLength))]
        public static partial class Position { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }
            
        [FixedUnit("Metre", "Metres")]
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static string SpecializedVectorGroupIdenticalText_MultipleAttributes(string attribute) => $$"""
        using SharpMeasures.Generators.Quantities;
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Vectors;

        [{{attribute}}("Metre")]
        [SpecializedSharpMeasuresVectorGroup(typeof(Position))]
        public static partial class Displacement { }

        [SharpMeasuresVectorGroup(typeof(UnitOfLength))]
        public static partial class Position { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }
            
        [FixedUnit("Metre", "Metres")]
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static string SpecializedVectorGroupIdenticalText_Inherited(string attribute) => $$"""
        using SharpMeasures.Generators.Quantities;
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Units.Utility;
        using SharpMeasures.Generators.Utility;
        using SharpMeasures.Generators.Vectors;

        [SpecializedSharpMeasuresVectorGroup(typeof(Position))]
        public static partial class Displacement { }

        [{{attribute}}("Metre")]
        [SharpMeasuresVectorGroup(typeof(UnitOfLength))]
        public static partial class Position { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }
            
        [FixedUnit("Metre", "Metres")]
        [PrefixedUnit("Kilometre", "Kilometres", "Metre", MetricPrefixName.Kilo)]
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static string VectorGroupMemberIdenticalText_SameAttribute(string attribute) => $$"""
        using SharpMeasures.Generators.Quantities;
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Vectors;

        [{{attribute}}("Metre")]
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

    private static string VectorGroupMemberIdenticalText_MultipleAttributes(string attribute) => $$"""
        using SharpMeasures.Generators.Quantities;
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Vectors;

        [{{attribute}}("Metre")]
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

    private static string VectorGroupMemberIdenticalText_InheritedFromGroup(string attribute) => $$"""
        using SharpMeasures.Generators.Quantities;
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Units.Utility;
        using SharpMeasures.Generators.Utility;
        using SharpMeasures.Generators.Vectors;

        [SharpMeasuresVectorGroupMember(typeof(Position))]
        public partial class Position3 { }

        [{{attribute}}("Metre")]
        [SharpMeasuresVectorGroup(typeof(UnitOfLength))]
        public static partial class Position { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }
            
        [FixedUnit("Metre", "Metres")]
        [PrefixedUnit("Kilometre", "Kilometres", "Metre", MetricPrefixName.Kilo)]
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static string VectorGroupMemberIdenticalText_InheritedFromMember(string attribute) => $$"""
        using SharpMeasures.Generators.Quantities;
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Units.Utility;
        using SharpMeasures.Generators.Utility;
        using SharpMeasures.Generators.Vectors;

        [SharpMeasuresVectorGroupMember(typeof(Displacement))]
        public partial class Displacement3 { }

        [SpecializedSharpMeasuresVectorGroup(typeof(Position))]
        public static partial class Displacement { }

        [{{attribute}}("Metre")]
        [SharpMeasuresVectorGroupMember(typeof(Position))]
        public partial class Position3 { }

        [SharpMeasuresVectorGroup(typeof(UnitOfLength))]
        public static partial class Position { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }
            
        [FixedUnit("Metre", "Metres")]
        [PrefixedUnit("Kilometre", "Kilometres", "Metre", MetricPrefixName.Kilo)]
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;
}
