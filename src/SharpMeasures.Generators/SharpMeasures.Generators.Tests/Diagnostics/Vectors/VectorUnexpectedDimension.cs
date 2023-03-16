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
        using SharpMeasures.Generators;
        
        [VectorQuantity(typeof(UnitOfLength), Difference = typeof(Position2))]
        public partial class Displacement3 { }

        [VectorQuantity(typeof(UnitOfLength))]
        public partial class Position2 { }
        
        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }
        
        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertVectorDifference()
    {
        var expectedLocation = ExpectedDiagnosticsLocation.AsTypeofArgumentTextSpan(VectorDifferenceText, target: "Position2", prefix: "Difference = ");

        return AssertExactlyVectorUnexpectedDimensionDiagnostics(VectorDifferenceText).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(VectorDifferenceIdentical);
    }

    private static string SpecializedVectorDifferenceText => """
        using SharpMeasures.Generators;
        
        [SpecializedVectorQuantity(typeof(Position3), Difference = typeof(Position2))]
        public partial class Displacement3 { }

        [VectorQuantity(typeof(UnitOfLength))]
        public partial class Position3 { }

        [VectorQuantity(typeof(UnitOfLength))]
        public partial class Position2 { }
        
        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }
        
        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertSpecializedVectorDifference()
    {
        var expectedLocation = ExpectedDiagnosticsLocation.AsTypeofArgumentTextSpan(SpecializedVectorDifferenceText, target: "Position2", prefix: "Difference = ");

        return AssertExactlyVectorUnexpectedDimensionDiagnostics(SpecializedVectorDifferenceText).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(SpecializedVectorDifferenceIdentical);
    }

    private static string ConvertibleVectorText => """
        using SharpMeasures.Generators;
        
        [ConvertibleQuantity(typeof(Position2))]
        [VectorQuantity(typeof(UnitOfLength))]
        public partial class Displacement3 { }
        
        [VectorQuantity(typeof(UnitOfLength))]
        public partial class Position2 { }
        
        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }
        
        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertConvertibleVector()
    {
        var expectedLocation = ExpectedDiagnosticsLocation.AsTypeofArgumentTextSpan(ConvertibleVectorText, target: "Position2", prefix: "ConvertibleQuantity(");

        return AssertExactlyVectorUnexpectedDimensionDiagnostics(ConvertibleVectorText).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(ConvertibleVectorIdentical);
    }

    private static string ConvertibleSpecializedVectorText => """
        using SharpMeasures.Generators;
        
        [ConvertibleQuantity(typeof(Position2))]
        [SpecializedVectorQuantity(typeof(Position3))]
        public partial class Displacement3 { }

        [VectorQuantity(typeof(UnitOfLength))]
        public partial class Position3 { }
        
        [VectorQuantity(typeof(UnitOfLength))]
        public partial class Position2 { }
        
        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }
        
        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertConvertibleSpecializedVector()
    {
        var expectedLocation = ExpectedDiagnosticsLocation.AsTypeofArgumentTextSpan(ConvertibleSpecializedVectorText, target: "Position2", prefix: "ConvertibleQuantity(");

        return AssertExactlyVectorUnexpectedDimensionDiagnostics(ConvertibleSpecializedVectorText).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(ConvertibleSpecializedVectorIdentical);
    }

    private static string ConvertibleVectorGroupMemberText => """
        using SharpMeasures.Generators;
        
        [ConvertibleQuantity(typeof(Position2))]
        [VectorGroupMember(typeof(Position))]
        public partial class Position3 { }

        [VectorGroup(typeof(UnitOfLength))]
        public static partial class Position { }
        
        [VectorQuantity(typeof(UnitOfLength))]
        public partial class Position2 { }
        
        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }
        
        [Unit(typeof(Length))]
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
        using SharpMeasures.Generators;
        
        [VectorQuantity(typeof(UnitOfLength), ImplementDifference = false)]
        public partial class Displacement3 { }

        [VectorQuantity(typeof(UnitOfLength))]
        public partial class Position2 { }
        
        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }
        
        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static string SpecializedVectorDifferenceIdenticalText => """
        using SharpMeasures.Generators;
        
        [SpecializedVectorQuantity(typeof(Position3), ImplementDifference = false)]
        public partial class Displacement3 { }

        [VectorQuantity(typeof(UnitOfLength))]
        public partial class Position3 { }

        [VectorQuantity(typeof(UnitOfLength))]
        public partial class Position2 { }
        
        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }
        
        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static string ConvertibleVectorIdenticalText => """
        using SharpMeasures.Generators;
        
        [VectorQuantity(typeof(UnitOfLength))]
        public partial class Displacement3 { }
        
        [VectorQuantity(typeof(UnitOfLength))]
        public partial class Position2 { }
        
        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }
        
        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static string ConvertibleSpecializedVectorIdenticalText => """
        using SharpMeasures.Generators;
        
        [SpecializedVectorQuantity(typeof(Position3))]
        public partial class Displacement3 { }

        [VectorQuantity(typeof(UnitOfLength))]
        public partial class Position3 { }
        
        [VectorQuantity(typeof(UnitOfLength))]
        public partial class Position2 { }
        
        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }
        
        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static string ConvertibleVectorGroupMemberIdenticalText => """
        using SharpMeasures.Generators;
        
        [VectorGroupMember(typeof(Position))]
        public partial class Position3 { }

        [VectorGroup(typeof(UnitOfLength))]
        public static partial class Position { }
        
        [VectorQuantity(typeof(UnitOfLength))]
        public partial class Position2 { }
        
        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }
        
        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;
}
