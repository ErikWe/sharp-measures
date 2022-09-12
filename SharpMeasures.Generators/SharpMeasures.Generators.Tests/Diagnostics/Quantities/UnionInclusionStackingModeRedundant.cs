namespace SharpMeasures.Generators.Tests.Diagnostics.Quantities;

using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Tests.Utility;
using SharpMeasures.Generators.Tests.Verify;

using System.Collections.Generic;
using System.Threading.Tasks;

using VerifyXunit;

using Xunit;

[UsesVerify]
public class UnionInclusionStackingModeRedundant
{
    [Fact]
    public Task VerifyUnionInclusionStackingModeRedundantDiagnosticsMessage() => AssertScalar("IncludeUnits").VerifyListedDiagnostics(DiagnosticIDs.UnionInclusionStackingModeRedundant);

    [Theory]
    [MemberData(nameof(ScalarAttributes))]
    public void Scalar(string attribute) => AssertScalar(attribute);

    [Theory]
    [MemberData(nameof(ScalarAttributes))]
    public void SpecializedScalar_Inherit(string attribute) => AssertSpecializedScalar(attribute, true);

    [Theory]
    [MemberData(nameof(ScalarAttributes))]
    public void SpecializedScalar_NonInherit(string attribute) => AssertSpecializedScalar(attribute, false);

    [Fact]
    public void Vector() => AssertVector();

    [Fact]
    public void SpecializedVector_Inherit() => AssertSpecializedVector(true);

    [Fact]
    public void SpecializedVector_NonInherit() => AssertSpecializedVector(false);

    [Fact]
    public void VectorGroup() => AssertVectorGroup();

    [Fact]
    public void SpecializedVectorGroup_Inherit() => AssertSpecializedVectorGroup(true);

    [Fact]
    public void SpecializedVectorGroup_NonInherit() => AssertSpecializedVectorGroup(false);

    [Fact]
    public void VectorGroupMember_Both() => AssertVectorGroupMember(true, true);

    [Fact]
    public void VectorGroupMember_Members() => AssertVectorGroupMember(true, false);

    [Fact]
    public void VectorGroupMember_Groups() => AssertVectorGroupMember(false, true);

    [Fact]
    public void VectorGroupMember_NonInherit() => AssertVectorGroupMember(false, false);

    public static IEnumerable<object[]> ScalarAttributes => new object[][]
    {
        new[] { IncludeUnitsAttribute },
        new[] { IncludeUnitBasesAttribute }
    };

    private static string IncludeUnitsAttribute { get; } = "IncludeUnits";
    private static string IncludeUnitBasesAttribute { get; } = "IncludeUnitBases";

    private static GeneratorVerifier AssertExactlyUnionInclusionStackingModeRedundantAndUnrecognizedUnitNameDiagnostics(string source) => GeneratorVerifier.Construct<SharpMeasuresGenerator>(source).AssertExactlyListedDiagnosticsIDsReported(UnionInclusionStackingModeRedundantAndUnrecognizedUnitNameDiagnostics);
    private static IReadOnlyCollection<string> UnionInclusionStackingModeRedundantAndUnrecognizedUnitNameDiagnostics { get; } = new string[] { DiagnosticIDs.UnrecognizedUnitInstanceName, DiagnosticIDs.UnionInclusionStackingModeRedundant };

    private static string ScalarText(string attribute) => $$"""
        using SharpMeasures.Generators;
        using SharpMeasures.Generators.Quantities;
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;

        [{{attribute}}("Kilometre", StackingMode = InclusionStackingMode.Union)]
        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }

        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertScalar(string attribute)
    {
        var source = ScalarText(attribute);
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(source, target: "InclusionStackingMode.Union");

        return AssertExactlyUnionInclusionStackingModeRedundantAndUnrecognizedUnitNameDiagnostics(source).AssertSpecificDiagnosticsLocation(DiagnosticIDs.UnionInclusionStackingModeRedundant, expectedLocation).AssertIdenticalSources(ScalarIdentical);
    }

    private static string SpecializedScalarText(string attribute, bool inherit) => $$"""
        using SharpMeasures.Generators;
        using SharpMeasures.Generators.Quantities;
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        
        [{{attribute}}("Kilometre", StackingMode = InclusionStackingMode.Union)]
        [SpecializedSharpMeasuresScalar(typeof(Length), InheritUnits = {{inherit.ToString().ToLowerInvariant()}}, InheritBases = {{inherit.ToString().ToLowerInvariant()}})]
        public partial class Distance { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }
        
        [FixedUnitInstance("Metre", "Metres")]
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertSpecializedScalar(string attribute, bool inherit)
    {
        var source = SpecializedScalarText(attribute, inherit);
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(source, target: "InclusionStackingMode.Union");

        return AssertExactlyUnionInclusionStackingModeRedundantAndUnrecognizedUnitNameDiagnostics(source).AssertSpecificDiagnosticsLocation(DiagnosticIDs.UnionInclusionStackingModeRedundant, expectedLocation).AssertIdenticalSources(SpecializedScalarIdentical);
    }

    private static string VectorText => """
        using SharpMeasures.Generators;
        using SharpMeasures.Generators.Quantities;
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Vectors;

        [IncludeUnits("Kilometre", StackingMode = InclusionStackingMode.Union)]
        [SharpMeasuresVector(typeof(UnitOfLength))]
        public partial class Position3 { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }
        
        [FixedUnitInstance("Metre", "Metres")]
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertVector()
    {
        var source = VectorText;
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(source, target: "InclusionStackingMode.Union");

        return AssertExactlyUnionInclusionStackingModeRedundantAndUnrecognizedUnitNameDiagnostics(source).AssertSpecificDiagnosticsLocation(DiagnosticIDs.UnionInclusionStackingModeRedundant, expectedLocation).AssertIdenticalSources(VectorIdentical);
    }

    private static string SpecializedVectorText(bool inherit) => $$"""
        using SharpMeasures.Generators;
        using SharpMeasures.Generators.Quantities;
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Vectors;

        [IncludeUnits("Kilometre", StackingMode = InclusionStackingMode.Union)]
        [SpecializedSharpMeasuresVector(typeof(Position3), InheritUnits = {{inherit.ToString().ToLowerInvariant()}})]
        public partial class Displacement3 { }

        [SharpMeasuresVector(typeof(UnitOfLength))]
        public partial class Position3 { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }
        
        [FixedUnitInstance("Metre", "Metres")]
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertSpecializedVector(bool inherit)
    {
        var source = SpecializedVectorText(inherit);
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(source, target: "InclusionStackingMode.Union");

        return AssertExactlyUnionInclusionStackingModeRedundantAndUnrecognizedUnitNameDiagnostics(source).AssertSpecificDiagnosticsLocation(DiagnosticIDs.UnionInclusionStackingModeRedundant, expectedLocation).AssertIdenticalSources(SpecializedVectorIdentical);
    }

    private static string VectorGroupText => """
        using SharpMeasures.Generators;
        using SharpMeasures.Generators.Quantities;
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Vectors;

        [IncludeUnits("Kilometre", StackingMode = InclusionStackingMode.Union)]
        [SharpMeasuresVectorGroup(typeof(UnitOfLength))]
        public static partial class Position { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }
        
        [FixedUnitInstance("Metre", "Metres")]
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertVectorGroup()
    {
        var source = VectorGroupText;
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(source, target: "InclusionStackingMode.Union");

        return AssertExactlyUnionInclusionStackingModeRedundantAndUnrecognizedUnitNameDiagnostics(source).AssertSpecificDiagnosticsLocation(DiagnosticIDs.UnionInclusionStackingModeRedundant, expectedLocation).AssertIdenticalSources(VectorGroupIdentical);
    }

    private static string SpecializedVectorGroupText(bool inherit) => $$"""
        using SharpMeasures.Generators;
        using SharpMeasures.Generators.Quantities;
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Vectors;

        [IncludeUnits("Kilometre", StackingMode = InclusionStackingMode.Union)]
        [SpecializedSharpMeasuresVectorGroup(typeof(Position), InheritUnits = {{inherit.ToString().ToLowerInvariant()}})]
        public static partial class Displacement { }

        [SharpMeasuresVectorGroup(typeof(UnitOfLength))]
        public static partial class Position { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }
        
        [FixedUnitInstance("Metre", "Metres")]
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertSpecializedVectorGroup(bool inherit)
    {
        var source = SpecializedVectorGroupText(inherit);
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(source, target: "InclusionStackingMode.Union");

        return AssertExactlyUnionInclusionStackingModeRedundantAndUnrecognizedUnitNameDiagnostics(source).AssertSpecificDiagnosticsLocation(DiagnosticIDs.UnionInclusionStackingModeRedundant, expectedLocation).AssertIdenticalSources(SpecializedVectorGroupIdentical);
    }

    private static string VectorGroupMemberText(bool inheritFromMembers, bool inheritFromGroups) => $$"""
        using SharpMeasures.Generators;
        using SharpMeasures.Generators.Quantities;
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Vectors;

        [IncludeUnits("Kilometre", StackingMode = InclusionStackingMode.Union)]
        [SharpMeasuresVectorGroupMember(typeof(Displacement), InheritUnits = {{inheritFromGroups.ToString().ToLowerInvariant()}}, InheritUnitsFromMembers = {{inheritFromMembers.ToString().ToLowerInvariant()}})]
        public partial class Displacement3 { }

        [SpecializedSharpMeasuresVectorGroup(typeof(Position))]
        public static partial class Displacement { }

        [SharpMeasuresVectorGroupMember(typeof(Position))]
        public partial class Posiiton3 { }

        [SharpMeasuresVectorGroup(typeof(UnitOfLength))]
        public static partial class Position { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }
        
        [FixedUnitInstance("Metre", "Metres")]
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertVectorGroupMember(bool inheritFromMembers, bool inheritFromGroups)
    {
        var source = VectorGroupMemberText(inheritFromMembers, inheritFromGroups);
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(source, target: "InclusionStackingMode.Union");

        return AssertExactlyUnionInclusionStackingModeRedundantAndUnrecognizedUnitNameDiagnostics(source).AssertSpecificDiagnosticsLocation(DiagnosticIDs.UnionInclusionStackingModeRedundant, expectedLocation).AssertIdenticalSources(VectorGroupMemberIdentical);
    }

    private static GeneratorVerifier ScalarIdentical => GeneratorVerifier.Construct<SharpMeasuresGenerator>(ScalarIdenticalText);
    private static GeneratorVerifier SpecializedScalarIdentical => GeneratorVerifier.Construct<SharpMeasuresGenerator>(SpecializedScalarIdenticalText);
    private static GeneratorVerifier VectorIdentical => GeneratorVerifier.Construct<SharpMeasuresGenerator>(VectorIdenticalText);
    private static GeneratorVerifier SpecializedVectorIdentical => GeneratorVerifier.Construct<SharpMeasuresGenerator>(SpecializedVectorIdenticalText);
    private static GeneratorVerifier VectorGroupIdentical => GeneratorVerifier.Construct<SharpMeasuresGenerator>(VectorGroupIdenticalText);
    private static GeneratorVerifier SpecializedVectorGroupIdentical => GeneratorVerifier.Construct<SharpMeasuresGenerator>(SpecializedVectorGroupIdenticalText);
    private static GeneratorVerifier VectorGroupMemberIdentical => GeneratorVerifier.Construct<SharpMeasuresGenerator>(VectorGroupMemberIdenticalText);

    private static string ScalarIdenticalText => """
        using SharpMeasures.Generators;
        using SharpMeasures.Generators.Quantities;
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }

        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static string SpecializedScalarIdenticalText => """
        using SharpMeasures.Generators;
        using SharpMeasures.Generators.Quantities;
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        
        [SpecializedSharpMeasuresScalar(typeof(Length))]
        public partial class Distance { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }
        
        [FixedUnitInstance("Metre", "Metres")]
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static string VectorIdenticalText => """
        using SharpMeasures.Generators;
        using SharpMeasures.Generators.Quantities;
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Vectors;

        [SharpMeasuresVector(typeof(UnitOfLength))]
        public partial class Position3 { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }
        
        [FixedUnitInstance("Metre", "Metres")]
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static string SpecializedVectorIdenticalText => """
        using SharpMeasures.Generators;
        using SharpMeasures.Generators.Quantities;
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Vectors;

        [SpecializedSharpMeasuresVector(typeof(Position3))]
        public partial class Displacement3 { }

        [SharpMeasuresVector(typeof(UnitOfLength))]
        public partial class Position3 { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }
        
        [FixedUnitInstance("Metre", "Metres")]
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static string VectorGroupIdenticalText => """
        using SharpMeasures.Generators;
        using SharpMeasures.Generators.Quantities;
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Vectors;

        [SharpMeasuresVectorGroup(typeof(UnitOfLength))]
        public static partial class Position { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }
        
        [FixedUnitInstance("Metre", "Metres")]
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static string SpecializedVectorGroupIdenticalText => """
        using SharpMeasures.Generators;
        using SharpMeasures.Generators.Quantities;
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Vectors;

        [SpecializedSharpMeasuresVectorGroup(typeof(Position))]
        public static partial class Displacement { }

        [SharpMeasuresVectorGroup(typeof(UnitOfLength))]
        public static partial class Position { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }
        
        [FixedUnitInstance("Metre", "Metres")]
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static string VectorGroupMemberIdenticalText => """
        using SharpMeasures.Generators;
        using SharpMeasures.Generators.Quantities;
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Vectors;

        [SharpMeasuresVectorGroupMember(typeof(Displacement))]
        public partial class Displacement3 { }

        [SpecializedSharpMeasuresVectorGroup(typeof(Position))]
        public static partial class Displacement { }

        [SharpMeasuresVectorGroupMember(typeof(Position))]
        public partial class Posiiton3 { }

        [SharpMeasuresVectorGroup(typeof(UnitOfLength))]
        public static partial class Position { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }
        
        [FixedUnitInstance("Metre", "Metres")]
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;
}
