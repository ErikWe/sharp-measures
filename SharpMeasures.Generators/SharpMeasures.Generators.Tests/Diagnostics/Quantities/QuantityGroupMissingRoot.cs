namespace SharpMeasures.Generators.Tests.Diagnostics.Quantities;

using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Tests.Utility;
using SharpMeasures.Generators.Tests.Verify;

using System.Collections.Generic;
using System.Threading.Tasks;

using VerifyXunit;

using Xunit;

[UsesVerify]
public class QuantityGroupMissingRoot
{
    [Fact]
    public Task SpecializedScalar_Self() => AssertSpecializedScalar_Self().VerifyDiagnostics();

    [Fact]
    public void SpecializedScalar_Loop() => AssertSpecializedScalar_Loop();

    [Fact]
    public void SpecializedScalar_BranchedLoop() => AssertSpecializedScalar_BranchedLoop();

    [Fact]
    public void SpecializedVector_Self() => AssertSpecializedVector_Self();

    [Fact]
    public void SpecializedVector_Loop() => AssertSpecializedVector_Loop();

    [Fact]
    public void SpecializedVector_BranchedLoop() => AssertSpecializedVector_BranchedLoop();

    [Fact]
    public void SpecializedVectorGroup_Self() => AssertSpecializedVectorGroup_Self();

    [Fact]
    public void SpecializedVectorGroup_Loop() => AssertSpecializedVectorGroup_Loop();

    [Fact]
    public void SpecializedVectorGroup_BranchedLoop() => AssertSpecializedVectorGroup_BranchedLoop();

    private static GeneratorVerifier AssertExactlyQuantityGroupMissingRootDiagnostics(string source, int diagnosticsCount)
    {
        return GeneratorVerifier.Construct<SharpMeasuresGenerator>(source).AssertExactlyListedDiagnosticsIDsReported(ExpectedDiagnostics(diagnosticsCount));
    }

    private static IEnumerable<string> ExpectedDiagnostics(int diagnosticsCount)
    {
        for (int i = 0; i < diagnosticsCount; i++)
        {
            yield return DiagnosticIDs.QuantityGroupMissingRoot;
        }
    }

    private static string SpecializedScalarText_Self => """
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;

        [SpecializedSharpMeasuresScalar(typeof(Distance))]
        public partial class Distance { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }
            
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertSpecializedScalar_Self()
    {
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(SpecializedScalarText_Self, target: "SpecializedSharpMeasuresScalar");

        return AssertExactlyQuantityGroupMissingRootDiagnostics(SpecializedScalarText_Self, 1).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(Identical);
    }

    private static string SpecializedScalarText_Loop => """
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;

        [SpecializedSharpMeasuresScalar(typeof(Width))]
        public partial class Depth { }

        [SpecializedSharpMeasuresScalar(typeof(Height))]
        public partial class Width { }

        [SpecializedSharpMeasuresScalar(typeof(Depth))]
        public partial class Height { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }
            
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertSpecializedScalar_Loop()
    {
        var expectedLocations = new[]
        {
            ExpectedDiagnosticsLocation.TextSpan(SpecializedScalarText_Loop, target: "SpecializedSharpMeasuresScalar", postfix: "(typeof(Width)"),
            ExpectedDiagnosticsLocation.TextSpan(SpecializedScalarText_Loop, target: "SpecializedSharpMeasuresScalar", postfix: "(typeof(Height)"),
            ExpectedDiagnosticsLocation.TextSpan(SpecializedScalarText_Loop, target: "SpecializedSharpMeasuresScalar", postfix: "(typeof(Depth)")
        };

        return AssertExactlyQuantityGroupMissingRootDiagnostics(SpecializedScalarText_Loop, 3).AssertDiagnosticsLocation(expectedLocations).AssertIdenticalSources(Identical);
    }

    private static string SpecializedScalarText_BranchedLoop => """
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;

        [SpecializedSharpMeasuresScalar(typeof(Width))]
        public partial class Depth { }

        [SpecializedSharpMeasuresScalar(typeof(Height))]
        public partial class Width { }

        [SpecializedSharpMeasuresScalar(typeof(Width))] // 2nd
        public partial class Height { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }
            
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertSpecializedScalar_BranchedLoop()
    {
        var expectedLocations = new[]
        {
            ExpectedDiagnosticsLocation.TextSpan(SpecializedScalarText_BranchedLoop, target: "SpecializedSharpMeasuresScalar", postfix: "(typeof(Width)"),
            ExpectedDiagnosticsLocation.TextSpan(SpecializedScalarText_BranchedLoop, target: "SpecializedSharpMeasuresScalar", postfix: "(typeof(Height)"),
            ExpectedDiagnosticsLocation.TextSpan(SpecializedScalarText_BranchedLoop, target: "SpecializedSharpMeasuresScalar", postfix: "(typeof(Width))] // 2nd")
        };

        return AssertExactlyQuantityGroupMissingRootDiagnostics(SpecializedScalarText_BranchedLoop, 3).AssertDiagnosticsLocation(expectedLocations).AssertIdenticalSources(Identical);
    }

    private static string SpecializedVectorText_Self => """
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Vectors;

        [SpecializedSharpMeasuresVector(typeof(Position3))]
        public partial class Position3 { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }
            
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertSpecializedVector_Self()
    {
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(SpecializedVectorText_Self, target: "SpecializedSharpMeasuresVector");

        return AssertExactlyQuantityGroupMissingRootDiagnostics(SpecializedVectorText_Self, 1).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(Identical);
    }

    private static string SpecializedVectorText_Loop => """
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Vectors;

        [SpecializedSharpMeasuresVector(typeof(Displacement3))]
        public partial class Offset3 { }

        [SpecializedSharpMeasuresVector(typeof(Position3))]
        public partial class Displacement3 { }

        [SpecializedSharpMeasuresVector(typeof(Offset3))]
        public partial class Position3 { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }
            
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertSpecializedVector_Loop()
    {
        var expectedLocations = new[]
        {
            ExpectedDiagnosticsLocation.TextSpan(SpecializedVectorText_Loop, target: "SpecializedSharpMeasuresVector", postfix: "(typeof(Displacement3)"),
            ExpectedDiagnosticsLocation.TextSpan(SpecializedVectorText_Loop, target: "SpecializedSharpMeasuresVector", postfix: "(typeof(Position3)"),
            ExpectedDiagnosticsLocation.TextSpan(SpecializedVectorText_Loop, target: "SpecializedSharpMeasuresVector", postfix: "(typeof(Offset3)")
        };

        return AssertExactlyQuantityGroupMissingRootDiagnostics(SpecializedVectorText_Loop, 3).AssertDiagnosticsLocation(expectedLocations).AssertIdenticalSources(Identical);
    }

    private static string SpecializedVectorText_BranchedLoop => """
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Vectors;

        [SpecializedSharpMeasuresVector(typeof(Displacement3))]
        public partial class Offset3 { }

        [SpecializedSharpMeasuresVector(typeof(Position3))]
        public partial class Displacement3 { }

        [SpecializedSharpMeasuresVector(typeof(Displacement3))] // 2nd
        public partial class Position3 { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }
            
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertSpecializedVector_BranchedLoop()
    {
        var expectedLocations = new[]
        {
            ExpectedDiagnosticsLocation.TextSpan(SpecializedVectorText_BranchedLoop, target: "SpecializedSharpMeasuresVector", postfix: "(typeof(Displacement3)"),
            ExpectedDiagnosticsLocation.TextSpan(SpecializedVectorText_BranchedLoop, target: "SpecializedSharpMeasuresVector", postfix: "(typeof(Position3)"),
            ExpectedDiagnosticsLocation.TextSpan(SpecializedVectorText_BranchedLoop, target: "SpecializedSharpMeasuresVector", postfix: "(typeof(Displacement3))] // 2nd")
        };

        return AssertExactlyQuantityGroupMissingRootDiagnostics(SpecializedVectorText_BranchedLoop, 3).AssertDiagnosticsLocation(expectedLocations).AssertIdenticalSources(Identical);
    }

    private static string SpecializedVectorGroupText_Self => """
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Vectors;

        [SpecializedSharpMeasuresVectorGroup(typeof(Position))]
        public static partial class Position { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }
            
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertSpecializedVectorGroup_Self()
    {
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(SpecializedVectorGroupText_Self, target: "SpecializedSharpMeasuresVectorGroup");

        return AssertExactlyQuantityGroupMissingRootDiagnostics(SpecializedVectorGroupText_Self, 1).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(Identical);
    }

    private static string SpecializedVectorGroupText_Loop => """
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Vectors;

        [SpecializedSharpMeasuresVectorGroup(typeof(Displacement))]
        public static partial class Offset { }

        [SpecializedSharpMeasuresVectorGroup(typeof(Position))]
        public static partial class Displacement { }

        [SpecializedSharpMeasuresVectorGroup(typeof(Offset))]
        public static partial class Position { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }
            
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertSpecializedVectorGroup_Loop()
    {
        var expectedLocations = new[]
        {
            ExpectedDiagnosticsLocation.TextSpan(SpecializedVectorGroupText_Loop, target: "SpecializedSharpMeasuresVectorGroup", postfix: "(typeof(Displacement)"),
            ExpectedDiagnosticsLocation.TextSpan(SpecializedVectorGroupText_Loop, target: "SpecializedSharpMeasuresVectorGroup", postfix: "(typeof(Position)"),
            ExpectedDiagnosticsLocation.TextSpan(SpecializedVectorGroupText_Loop, target: "SpecializedSharpMeasuresVectorGroup", postfix: "(typeof(Offset)")
        };

        return AssertExactlyQuantityGroupMissingRootDiagnostics(SpecializedVectorGroupText_Loop, 3).AssertDiagnosticsLocation(expectedLocations).AssertIdenticalSources(Identical);
    }

    private static string SpecializedVectorGroupText_BranchedLoop => """
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Vectors;

        [SpecializedSharpMeasuresVectorGroup(typeof(Displacement))]
        public static partial class Offset { }

        [SpecializedSharpMeasuresVectorGroup(typeof(Position))]
        public static partial class Displacement { }

        [SpecializedSharpMeasuresVectorGroup(typeof(Displacement))] // 2nd
        public static partial class Position { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }
            
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertSpecializedVectorGroup_BranchedLoop()
    {
        var expectedLocations = new[]
        {
            ExpectedDiagnosticsLocation.TextSpan(SpecializedVectorGroupText_BranchedLoop, target: "SpecializedSharpMeasuresVectorGroup", postfix: "(typeof(Displacement)"),
            ExpectedDiagnosticsLocation.TextSpan(SpecializedVectorGroupText_BranchedLoop, target: "SpecializedSharpMeasuresVectorGroup", postfix: "(typeof(Position)"),
            ExpectedDiagnosticsLocation.TextSpan(SpecializedVectorGroupText_BranchedLoop, target: "SpecializedSharpMeasuresVectorGroup", postfix: "(typeof(Displacement))] // 2nd")
        };

        return AssertExactlyQuantityGroupMissingRootDiagnostics(SpecializedVectorGroupText_BranchedLoop, 3).AssertDiagnosticsLocation(expectedLocations).AssertIdenticalSources(Identical);
    }

    private static GeneratorVerifier Identical => GeneratorVerifier.Construct<SharpMeasuresGenerator>(IdenticalText);

    private static string IdenticalText => """
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }
            
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;
}
