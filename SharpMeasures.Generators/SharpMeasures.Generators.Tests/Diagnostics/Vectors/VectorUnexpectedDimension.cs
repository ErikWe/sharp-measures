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

    private static GeneratorVerifier AssertExactlyVectorUnexpectedDimensionDiagnostics(string source) => GeneratorVerifier.Construct<SharpMeasuresGenerator>(source).AssertExactlyListedDiagnosticsIDsReported(VectorUnexpectedDimensionDiagnostics);
    private static IReadOnlyCollection<string> VectorUnexpectedDimensionDiagnostics { get; } = new string[] { DiagnosticIDs.VectorUnexpectedDimension };

    private static string VectorDifferenceText => """
        using SharpMeasures.Generators.Quantities;
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

        return AssertExactlyVectorUnexpectedDimensionDiagnostics(VectorDifferenceText).AssertDiagnosticsLocation(expectedLocation, VectorDifferenceText);
    }

    private static string SpecializedVectorDifferenceText => """
        using SharpMeasures.Generators.Quantities;
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

        return AssertExactlyVectorUnexpectedDimensionDiagnostics(SpecializedVectorDifferenceText).AssertDiagnosticsLocation(expectedLocation, SpecializedVectorDifferenceText);
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

        return AssertExactlyVectorUnexpectedDimensionDiagnostics(ConvertibleVectorText).AssertDiagnosticsLocation(expectedLocation, ConvertibleVectorText);
    }
}
