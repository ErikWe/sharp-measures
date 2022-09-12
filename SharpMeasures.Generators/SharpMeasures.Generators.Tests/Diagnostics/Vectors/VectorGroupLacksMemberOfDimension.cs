namespace SharpMeasures.Generators.Tests.Diagnostics.Vectors;

using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Tests.Utility;
using SharpMeasures.Generators.Tests.Verify;

using System.Collections.Generic;
using System.Threading.Tasks;

using VerifyXunit;

using Xunit;

[UsesVerify]
public class VectorGroupLacksMemberOfDimension
{
    [Fact]
    public Task VectorDifference_NonEmptyGroup() => AssertVectorDifference_NonEmptyGroup().VerifyDiagnostics();

    [Fact]
    public void VectorDifference_EmptyGroup() => AssertVectorDifference_EmptyGroup();

    [Fact]
    public void SpecializedVectorDifference_NonEmptyGroup() => AssertSpecializedVectorDifference_NonEmptyGroup();

    [Fact]
    public void SpecializedVectorDifference_EmptyGroup() => AssertSpecializedVectorDifference_EmptyGroup();

    [Fact]
    public void ConvertibleVector() => AssertConvertibleVector();

    [Fact]
    public void ConvertibleSpecializedVector() => AssertConvertibleSpecializedVector();

    [Fact]
    public void ConvertibleVectorGroupMember() => AssertConvertibleVectorGroupMember();

    private static GeneratorVerifier AssertExactlyVectorGroupLacksMemberOfDimensionDiagnostics(string source) => GeneratorVerifier.Construct<SharpMeasuresGenerator>(source).AssertExactlyListedDiagnosticsIDsReported(VectorGroupLacksMemberOfDimensionDiagnostics);
    private static IReadOnlyCollection<string> VectorGroupLacksMemberOfDimensionDiagnostics { get; } = new string[] { DiagnosticIDs.VectorGroupLacksMemberOfDimension };

    private static string VectorDifferenceText_NonEmptyGroup => """
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Vectors;
        
        [SharpMeasuresVector(typeof(UnitOfLength), Difference = typeof(Displacement))]
        public partial class Position3 { }

        [SharpMeasuresVectorGroupMember(typeof(Displacement))]
        public partial class Displacement2 { }
        
        [SharpMeasuresVectorGroup(typeof(UnitOfLength))]
        public static partial class Displacement { }
        
        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }
        
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertVectorDifference_NonEmptyGroup()
    {
        var expectedLocation = ExpectedDiagnosticsLocation.AsTypeofArgumentTextSpan(VectorDifferenceText_NonEmptyGroup, target: "Displacement", prefix: "Difference = ");

        return AssertExactlyVectorGroupLacksMemberOfDimensionDiagnostics(VectorDifferenceText_NonEmptyGroup).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(VectorDifferenceIdentical_NonEmptyGroup);
    }

    private static string VectorDifferenceText_EmptyGroup => """
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Vectors;
        
        [SharpMeasuresVector(typeof(UnitOfLength), Difference = typeof(Displacement))]
        public partial class Position3 { }
        
        [SharpMeasuresVectorGroup(typeof(UnitOfLength))]
        public static partial class Displacement { }
        
        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }
        
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertVectorDifference_EmptyGroup()
    {
        var expectedLocation = ExpectedDiagnosticsLocation.AsTypeofArgumentTextSpan(VectorDifferenceText_EmptyGroup, target: "Displacement", prefix: "Difference = ");

        return AssertExactlyVectorGroupLacksMemberOfDimensionDiagnostics(VectorDifferenceText_EmptyGroup).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(VectorDifferenceIdentical_EmptyGroup);
    }

    private static string SpecializedVectorDifferenceText_NonEmptyGroup => """
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Vectors;
        
        [SpecializedSharpMeasuresVector(typeof(Length3), Difference = typeof(Displacement))]
        public partial class Position3 { }

        [SharpMeasuresVector(typeof(UnitOfLength))]
        public partial class Length3 { }

        [SharpMeasuresVectorGroupMember(typeof(Displacement))]
        public partial class Displacement2 { }
        
        [SharpMeasuresVectorGroup(typeof(UnitOfLength))]
        public static partial class Displacement { }
        
        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }
        
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertSpecializedVectorDifference_NonEmptyGroup()
    {
        var expectedLocation = ExpectedDiagnosticsLocation.AsTypeofArgumentTextSpan(SpecializedVectorDifferenceText_NonEmptyGroup, target: "Displacement", prefix: "Difference = ");

        return AssertExactlyVectorGroupLacksMemberOfDimensionDiagnostics(SpecializedVectorDifferenceText_NonEmptyGroup).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(SpecializedVectorDifferenceIdentical_NonEmptyGroup);
    }

    private static string SpecializedVectorDifferenceText_EmptyGroup => """
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Vectors;
        
        [SpecializedSharpMeasuresVector(typeof(Length3), Difference = typeof(Displacement))]
        public partial class Position3 { }

        [SharpMeasuresVector(typeof(UnitOfLength))]
        public partial class Length3 { }
        
        [SharpMeasuresVectorGroup(typeof(UnitOfLength))]
        public static partial class Displacement { }
        
        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }
        
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertSpecializedVectorDifference_EmptyGroup()
    {
        var expectedLocation = ExpectedDiagnosticsLocation.AsTypeofArgumentTextSpan(SpecializedVectorDifferenceText_EmptyGroup, target: "Displacement", prefix: "Difference = ");

        return AssertExactlyVectorGroupLacksMemberOfDimensionDiagnostics(SpecializedVectorDifferenceText_EmptyGroup).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(SpecializedVectorDifferenceIdentical_EmptyGroup);
    }

    private static string ConvertibleVectorText => """
        using SharpMeasures.Generators.Quantities;
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Vectors;
        
        [ConvertibleQuantity(typeof(Position))]
        [SharpMeasuresVector(typeof(UnitOfLength))]
        public partial class Displacement3 { }
        
        [SharpMeasuresVectorGroup(typeof(UnitOfLength))]
        public static partial class Position { }
        
        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }
        
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertConvertibleVector()
    {
        var expectedLocation = ExpectedDiagnosticsLocation.AsTypeofArgumentTextSpan(ConvertibleVectorText, target: "Position", prefix: "ConvertibleQuantity(");

        return AssertExactlyVectorGroupLacksMemberOfDimensionDiagnostics(ConvertibleVectorText).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(ConvertibleVectorIdentical);
    }

    private static string ConvertibleSpecializedVectorText => """
        using SharpMeasures.Generators.Quantities;
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Vectors;
        
        [ConvertibleQuantity(typeof(Position))]
        [SpecializedSharpMeasuresVector(typeof(Position3))]
        public partial class Displacement3 { }

        [SharpMeasuresVector(typeof(UnitOfLength))]
        public partial class Position3 { }
        
        [SharpMeasuresVectorGroup(typeof(UnitOfLength))]
        public static partial class Position { }
        
        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }
        
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertConvertibleSpecializedVector()
    {
        var expectedLocation = ExpectedDiagnosticsLocation.AsTypeofArgumentTextSpan(ConvertibleSpecializedVectorText, target: "Position", prefix: "ConvertibleQuantity(");

        return AssertExactlyVectorGroupLacksMemberOfDimensionDiagnostics(ConvertibleSpecializedVectorText).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(ConvertibleSpecializedVectorIdentical);
    }

    private static string ConvertibleVectorGroupMemberText => """
        using SharpMeasures.Generators.Quantities;
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Vectors;
        
        [ConvertibleQuantity(typeof(Position))]
        [SharpMeasuresVectorGroupMember(typeof(Displacement))]
        public partial class Displacement3 { }

        [SharpMeasuresVectorGroup(typeof(UnitOfLength))]
        public static partial class Displacement { }
        
        [SharpMeasuresVectorGroup(typeof(UnitOfLength))]
        public static partial class Position { }
        
        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }
        
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertConvertibleVectorGroupMember()
    {
        var expectedLocation = ExpectedDiagnosticsLocation.AsTypeofArgumentTextSpan(ConvertibleVectorGroupMemberText, target: "Position", prefix: "ConvertibleQuantity(");

        return AssertExactlyVectorGroupLacksMemberOfDimensionDiagnostics(ConvertibleVectorGroupMemberText).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(ConvertibleVectorGroupMemberIdentical);
    }

    private static GeneratorVerifier VectorDifferenceIdentical_NonEmptyGroup => GeneratorVerifier.Construct<SharpMeasuresGenerator>(VectorDifferenceIdenticalText_NonEmptyGroup);
    private static GeneratorVerifier VectorDifferenceIdentical_EmptyGroup => GeneratorVerifier.Construct<SharpMeasuresGenerator>(VectorDifferenceIdenticalText_EmptyGroup);
    private static GeneratorVerifier SpecializedVectorDifferenceIdentical_NonEmptyGroup => GeneratorVerifier.Construct<SharpMeasuresGenerator>(SpecializedVectorDifferenceIdenticalText_NonEmptyGroup);
    private static GeneratorVerifier SpecializedVectorDifferenceIdentical_EmptyGroup => GeneratorVerifier.Construct<SharpMeasuresGenerator>(SpecializedVectorDifferenceIdenticalText_EmptyGroup);
    private static GeneratorVerifier ConvertibleVectorIdentical => GeneratorVerifier.Construct<SharpMeasuresGenerator>(ConvertibleVectorIdenticalText);
    private static GeneratorVerifier ConvertibleSpecializedVectorIdentical => GeneratorVerifier.Construct<SharpMeasuresGenerator>(ConvertibleSpecializedVectorIdenticalText);
    private static GeneratorVerifier ConvertibleVectorGroupMemberIdentical => GeneratorVerifier.Construct<SharpMeasuresGenerator>(ConvertibleVectorGroupMemberIdenticalText);

    private static string VectorDifferenceIdenticalText_NonEmptyGroup => """
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Vectors;
        
        [SharpMeasuresVector(typeof(UnitOfLength), ImplementDifference = false)]
        public partial class Position3 { }

        [SharpMeasuresVectorGroupMember(typeof(Displacement))]
        public partial class Displacement2 { }
        
        [SharpMeasuresVectorGroup(typeof(UnitOfLength))]
        public static partial class Displacement { }
        
        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }
        
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static string VectorDifferenceIdenticalText_EmptyGroup => """
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Vectors;
        
        [SharpMeasuresVector(typeof(UnitOfLength), ImplementDifference = false)]
        public partial class Position3 { }
        
        [SharpMeasuresVectorGroup(typeof(UnitOfLength))]
        public static partial class Displacement { }
        
        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }
        
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static string SpecializedVectorDifferenceIdenticalText_NonEmptyGroup => """
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Vectors;
        
        [SpecializedSharpMeasuresVector(typeof(Length3), ImplementDifference = false)]
        public partial class Position3 { }

        [SharpMeasuresVector(typeof(UnitOfLength))]
        public partial class Length3 { }

        [SharpMeasuresVectorGroupMember(typeof(Displacement))]
        public partial class Displacement2 { }
        
        [SharpMeasuresVectorGroup(typeof(UnitOfLength))]
        public static partial class Displacement { }
        
        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }
        
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static string SpecializedVectorDifferenceIdenticalText_EmptyGroup => """
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Vectors;
        
        [SpecializedSharpMeasuresVector(typeof(Length3), ImplementDifference = false)]
        public partial class Position3 { }

        [SharpMeasuresVector(typeof(UnitOfLength))]
        public partial class Length3 { }
        
        [SharpMeasuresVectorGroup(typeof(UnitOfLength))]
        public static partial class Displacement { }
        
        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }
        
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static string ConvertibleVectorIdenticalText => """
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Vectors;
        
        [SharpMeasuresVector(typeof(UnitOfLength))]
        public partial class Displacement3 { }
        
        [SharpMeasuresVectorGroup(typeof(UnitOfLength))]
        public static partial class Position { }
        
        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }
        
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static string ConvertibleSpecializedVectorIdenticalText => """
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Vectors;
        
        [SpecializedSharpMeasuresVector(typeof(Position3))]
        public partial class Displacement3 { }

        [SharpMeasuresVector(typeof(UnitOfLength))]
        public partial class Position3 { }
        
        [SharpMeasuresVectorGroup(typeof(UnitOfLength))]
        public static partial class Position { }
        
        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }
        
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static string ConvertibleVectorGroupMemberIdenticalText => """
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Vectors;
        
        [SharpMeasuresVectorGroupMember(typeof(Displacement))]
        public partial class Displacement3 { }

        [SharpMeasuresVectorGroup(typeof(UnitOfLength))]
        public static partial class Displacement { }
        
        [SharpMeasuresVectorGroup(typeof(UnitOfLength))]
        public static partial class Position { }
        
        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }
        
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;
}
