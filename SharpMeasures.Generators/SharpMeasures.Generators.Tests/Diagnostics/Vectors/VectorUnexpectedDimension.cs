namespace SharpMeasures.Generators.Tests.Diagnostics.Vectors;

using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Tests.Utility;
using SharpMeasures.Generators.Tests.Verify;

using System.Collections.Generic;
using System.Threading.Tasks;

using VerifyXunit;

using Xunit;

[UsesVerify]
public class VectorUnexpectedDimension
{
    [Fact]
    public Task VectorDifference() => AssertVectorDifference().VerifyDiagnostics();

    [Fact]
    public void SpecializedVectorDifference() => AssertSpecializedVectorDifference();

    [Fact]
    public void ConvertibleVector() => AssertConvertibleVector();

    [Fact]
    public void ConvertibleSpecializedVector() => AssertConvertibleSpecializedVector();

    [Fact]
    public void ConvertibleVectorGroupMember() => AssertConvertibleVectorGroupMember();

    private static GeneratorVerifier AssertExactlyVectorUnexpectedDimensionDiagnostics(string source) => GeneratorVerifier.Construct<SharpMeasuresGenerator>(source).AssertExactlyListedDiagnosticsIDsReported(VectorUnexpectedDimensionDiagnostics);
    private static IReadOnlyCollection<string> VectorUnexpectedDimensionDiagnostics { get; } = new string[] { DiagnosticIDs.VectorUnexpectedDimension };

    private static string VectorDifferenceText => """
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Vectors;
        
        [SharpMeasuresVector(typeof(UnitOfLength), Difference = typeof(Position2))]
        public partial class Displacement3 { }

        [SharpMeasuresVector(typeof(UnitOfLength))]
        public partial class Position2 { }
        
        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }
        
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertVectorDifference()
    {
        var expectedLocation = ExpectedDiagnosticsLocation.AsTypeofArgumentTextSpan(VectorDifferenceText, target: "Position2", prefix: "Difference = ");

        return AssertExactlyVectorUnexpectedDimensionDiagnostics(VectorDifferenceText).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(VectorDifferenceIdentical);
    }

    private static string SpecializedVectorDifferenceText => """
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Vectors;
        
        [SpecializedSharpMeasuresVector(typeof(Position3), Difference = typeof(Position2))]
        public partial class Displacement3 { }

        [SharpMeasuresVector(typeof(UnitOfLength))]
        public partial class Position3 { }

        [SharpMeasuresVector(typeof(UnitOfLength))]
        public partial class Position2 { }
        
        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }
        
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertSpecializedVectorDifference()
    {
        var expectedLocation = ExpectedDiagnosticsLocation.AsTypeofArgumentTextSpan(SpecializedVectorDifferenceText, target: "Position2", prefix: "Difference = ");

        return AssertExactlyVectorUnexpectedDimensionDiagnostics(SpecializedVectorDifferenceText).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(SpecializedVectorDifferenceIdentical);
    }

    private static string ConvertibleVectorText => """
        using SharpMeasures.Generators.Quantities;
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Vectors;
        
        [ConvertibleQuantity(typeof(Position2))]
        [SharpMeasuresVector(typeof(UnitOfLength))]
        public partial class Displacement3 { }
        
        [SharpMeasuresVector(typeof(UnitOfLength))]
        public partial class Position2 { }
        
        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }
        
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertConvertibleVector()
    {
        var expectedLocation = ExpectedDiagnosticsLocation.AsTypeofArgumentTextSpan(ConvertibleVectorText, target: "Position2", prefix: "ConvertibleQuantity(");

        return AssertExactlyVectorUnexpectedDimensionDiagnostics(ConvertibleVectorText).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(ConvertibleVectorIdentical);
    }

    private static string ConvertibleSpecializedVectorText => """
        using SharpMeasures.Generators.Quantities;
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Vectors;
        
        [ConvertibleQuantity(typeof(Position2))]
        [SpecializedSharpMeasuresVector(typeof(Position3))]
        public partial class Displacement3 { }

        [SharpMeasuresVector(typeof(UnitOfLength))]
        public partial class Position3 { }
        
        [SharpMeasuresVector(typeof(UnitOfLength))]
        public partial class Position2 { }
        
        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }
        
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertConvertibleSpecializedVector()
    {
        var expectedLocation = ExpectedDiagnosticsLocation.AsTypeofArgumentTextSpan(ConvertibleSpecializedVectorText, target: "Position2", prefix: "ConvertibleQuantity(");

        return AssertExactlyVectorUnexpectedDimensionDiagnostics(ConvertibleSpecializedVectorText).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(ConvertibleSpecializedVectorIdentical);
    }

    private static string ConvertibleVectorGroupMemberText => """
        using SharpMeasures.Generators.Quantities;
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Vectors;
        
        [ConvertibleQuantity(typeof(Position2))]
        [SharpMeasuresVectorGroupMember(typeof(Position))]
        public partial class Position3 { }

        [SharpMeasuresVectorGroup(typeof(UnitOfLength))]
        public static partial class Position { }
        
        [SharpMeasuresVector(typeof(UnitOfLength))]
        public partial class Position2 { }
        
        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }
        
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertConvertibleVectorGroupMember()
    {
        var expectedLocation = ExpectedDiagnosticsLocation.AsTypeofArgumentTextSpan(ConvertibleVectorGroupMemberText, target: "Position2", prefix: "ConvertibleQuantity(");

        return AssertExactlyVectorUnexpectedDimensionDiagnostics(ConvertibleVectorGroupMemberText).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(ConvertibleVectorGroupMemberIdentical);
    }

    private static GeneratorVerifier VectorDifferenceIdentical => GeneratorVerifier.Construct<SharpMeasuresGenerator>(VectorDifferenceIdenticalText);
    private static GeneratorVerifier SpecializedVectorDifferenceIdentical => GeneratorVerifier.Construct<SharpMeasuresGenerator>(SpecializedVectorDifferenceIdenticalText);
    private static GeneratorVerifier ConvertibleVectorIdentical => GeneratorVerifier.Construct<SharpMeasuresGenerator>(ConvertibleVectorIdenticalText);
    private static GeneratorVerifier ConvertibleSpecializedVectorIdentical => GeneratorVerifier.Construct<SharpMeasuresGenerator>(ConvertibleSpecializedVectorIdenticalText);
    private static GeneratorVerifier ConvertibleVectorGroupMemberIdentical => GeneratorVerifier.Construct<SharpMeasuresGenerator>(ConvertibleVectorGroupMemberIdenticalText);

    private static string VectorDifferenceIdenticalText => """
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Vectors;
        
        [SharpMeasuresVector(typeof(UnitOfLength), ImplementDifference = false)]
        public partial class Displacement3 { }

        [SharpMeasuresVector(typeof(UnitOfLength))]
        public partial class Position2 { }
        
        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }
        
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static string SpecializedVectorDifferenceIdenticalText => """
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Vectors;
        
        [SpecializedSharpMeasuresVector(typeof(Position3), ImplementDifference = false)]
        public partial class Displacement3 { }

        [SharpMeasuresVector(typeof(UnitOfLength))]
        public partial class Position3 { }

        [SharpMeasuresVector(typeof(UnitOfLength))]
        public partial class Position2 { }
        
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
        
        [SharpMeasuresVector(typeof(UnitOfLength))]
        public partial class Position2 { }
        
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
        
        [SharpMeasuresVector(typeof(UnitOfLength))]
        public partial class Position2 { }
        
        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }
        
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static string ConvertibleVectorGroupMemberIdenticalText => """
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Vectors;
        
        [SharpMeasuresVectorGroupMember(typeof(Position))]
        public partial class Position3 { }

        [SharpMeasuresVectorGroup(typeof(UnitOfLength))]
        public static partial class Position { }
        
        [SharpMeasuresVector(typeof(UnitOfLength))]
        public partial class Position2 { }
        
        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }
        
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;
}
