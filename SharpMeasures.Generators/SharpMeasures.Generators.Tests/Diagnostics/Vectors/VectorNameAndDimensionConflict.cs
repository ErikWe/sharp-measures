namespace SharpMeasures.Generators.Tests.Diagnostics.Vectors;

using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Tests.Utility;
using SharpMeasures.Generators.Tests.Verify;

using System.Collections.Generic;
using System.Threading.Tasks;

using VerifyXunit;

using Xunit;

[UsesVerify]
public class VectorNameAndDimensionConflict
{
    [Fact]
    public Task Vector() => AssertVector().VerifyDiagnostics();

    [Fact]
    public void SpecializedVector() => AssertSpecializedVector();

    [Fact]
    public void VectorGroupMember() => AssertVectorGroupMember();

    private static GeneratorVerifier AssertExactlyVectorNameAndDimensionConflictDiagnostics(string source) => GeneratorVerifier.Construct<SharpMeasuresGenerator>(source).AssertExactlyListedDiagnosticsIDsReported(VectorNameAndDimensionConflictDiagnostics);
    private static IReadOnlyCollection<string> VectorNameAndDimensionConflictDiagnostics { get; } = new string[] { DiagnosticIDs.VectorNameAndDimensionConflict };

    private static string VectorText => $$"""
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Vectors;
        
        [SharpMeasuresVector(typeof(UnitOfLength), Dimension = 2)]
        public partial class Position3 { }
        
        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }
        
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertVector()
    {
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(VectorText, target: "SharpMeasuresVector");

        return AssertExactlyVectorNameAndDimensionConflictDiagnostics(VectorText).AssertDiagnosticsLocation(expectedLocation);
    }

    private static string SpecializedVectorText => $$"""
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Vectors;
        
        [SpecializedSharpMeasuresVector(typeof(Position3))]
        public partial class Displacement2 { }

        [SharpMeasuresVector(typeof(UnitOfLength))]
        public partial class Position3 { }
        
        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }
        
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertSpecializedVector()
    {
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(SpecializedVectorText, target: "SpecializedSharpMeasuresVector");

        return AssertExactlyVectorNameAndDimensionConflictDiagnostics(SpecializedVectorText).AssertDiagnosticsLocation(expectedLocation);
    }

    private static string VectorGroupMemberText => $$"""
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Vectors;
        
        [SharpMeasuresVectorGroupMember(typeof(Position), Dimension = 2)]
        public partial class Position3 { }
        
        [SharpMeasuresVectorGroup(typeof(UnitOfLength))]
        public static partial class Position { }
        
        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }
        
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertVectorGroupMember()
    {
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(VectorGroupMemberText, target: "SharpMeasuresVectorGroupMember");

        return AssertExactlyVectorNameAndDimensionConflictDiagnostics(VectorGroupMemberText).AssertDiagnosticsLocation(expectedLocation);
    }
}
