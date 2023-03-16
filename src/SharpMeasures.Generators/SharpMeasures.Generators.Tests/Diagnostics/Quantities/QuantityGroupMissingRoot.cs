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
        for (var i = 0; i < diagnosticsCount; i++)
        {
            yield return DiagnosticIDs.QuantityGroupMissingRoot;
        }
    }

    private static string SpecializedScalarText_Self => """
        using SharpMeasures.Generators;

        [SpecializedScalarQuantity(typeof(Distance))]
        public partial class Distance { }

        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }
            
        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertSpecializedScalar_Self()
    {
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(SpecializedScalarText_Self, target: "SpecializedScalarQuantity");

        return AssertExactlyQuantityGroupMissingRootDiagnostics(SpecializedScalarText_Self, 1).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(Identical);
    }

    private static string SpecializedScalarText_Loop => """
        using SharpMeasures.Generators;

        [SpecializedScalarQuantity(typeof(Width))]
        public partial class Depth { }

        [SpecializedScalarQuantity(typeof(Height))]
        public partial class Width { }

        [SpecializedScalarQuantity(typeof(Depth))]
        public partial class Height { }

        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }
            
        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertSpecializedScalar_Loop()
    {
        var expectedLocations = new[]
        {
            ExpectedDiagnosticsLocation.TextSpan(SpecializedScalarText_Loop, target: "SpecializedScalarQuantity", postfix: "(typeof(Width)"),
            ExpectedDiagnosticsLocation.TextSpan(SpecializedScalarText_Loop, target: "SpecializedScalarQuantity", postfix: "(typeof(Height)"),
            ExpectedDiagnosticsLocation.TextSpan(SpecializedScalarText_Loop, target: "SpecializedScalarQuantity", postfix: "(typeof(Depth)")
        };

        return AssertExactlyQuantityGroupMissingRootDiagnostics(SpecializedScalarText_Loop, 3).AssertDiagnosticsLocation(expectedLocations).AssertIdenticalSources(Identical);
    }

    private static string SpecializedScalarText_BranchedLoop => """
        using SharpMeasures.Generators;

        [SpecializedScalarQuantity(typeof(Width))]
        public partial class Depth { }

        [SpecializedScalarQuantity(typeof(Height))]
        public partial class Width { }

        [SpecializedScalarQuantity(typeof(Width))] // 2nd
        public partial class Height { }

        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }
            
        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertSpecializedScalar_BranchedLoop()
    {
        var expectedLocations = new[]
        {
            ExpectedDiagnosticsLocation.TextSpan(SpecializedScalarText_BranchedLoop, target: "SpecializedScalarQuantity", postfix: "(typeof(Width)"),
            ExpectedDiagnosticsLocation.TextSpan(SpecializedScalarText_BranchedLoop, target: "SpecializedScalarQuantity", postfix: "(typeof(Height)"),
            ExpectedDiagnosticsLocation.TextSpan(SpecializedScalarText_BranchedLoop, target: "SpecializedScalarQuantity", postfix: "(typeof(Width))] // 2nd")
        };

        return AssertExactlyQuantityGroupMissingRootDiagnostics(SpecializedScalarText_BranchedLoop, 3).AssertDiagnosticsLocation(expectedLocations).AssertIdenticalSources(Identical);
    }

    private static string SpecializedVectorText_Self => """
        using SharpMeasures.Generators;

        [SpecializedVectorQuantity(typeof(Position3))]
        public partial class Position3 { }

        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }
            
        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertSpecializedVector_Self()
    {
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(SpecializedVectorText_Self, target: "SpecializedVectorQuantity");

        return AssertExactlyQuantityGroupMissingRootDiagnostics(SpecializedVectorText_Self, 1).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(Identical);
    }

    private static string SpecializedVectorText_Loop => """
        using SharpMeasures.Generators;

        [SpecializedVectorQuantity(typeof(Displacement3))]
        public partial class Offset3 { }

        [SpecializedVectorQuantity(typeof(Position3))]
        public partial class Displacement3 { }

        [SpecializedVectorQuantity(typeof(Offset3))]
        public partial class Position3 { }

        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }
            
        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertSpecializedVector_Loop()
    {
        var expectedLocations = new[]
        {
            ExpectedDiagnosticsLocation.TextSpan(SpecializedVectorText_Loop, target: "SpecializedVectorQuantity", postfix: "(typeof(Displacement3)"),
            ExpectedDiagnosticsLocation.TextSpan(SpecializedVectorText_Loop, target: "SpecializedVectorQuantity", postfix: "(typeof(Position3)"),
            ExpectedDiagnosticsLocation.TextSpan(SpecializedVectorText_Loop, target: "SpecializedVectorQuantity", postfix: "(typeof(Offset3)")
        };

        return AssertExactlyQuantityGroupMissingRootDiagnostics(SpecializedVectorText_Loop, 3).AssertDiagnosticsLocation(expectedLocations).AssertIdenticalSources(Identical);
    }

    private static string SpecializedVectorText_BranchedLoop => """
        using SharpMeasures.Generators;

        [SpecializedVectorQuantity(typeof(Displacement3))]
        public partial class Offset3 { }

        [SpecializedVectorQuantity(typeof(Position3))]
        public partial class Displacement3 { }

        [SpecializedVectorQuantity(typeof(Displacement3))] // 2nd
        public partial class Position3 { }

        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }
            
        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertSpecializedVector_BranchedLoop()
    {
        var expectedLocations = new[]
        {
            ExpectedDiagnosticsLocation.TextSpan(SpecializedVectorText_BranchedLoop, target: "SpecializedVectorQuantity", postfix: "(typeof(Displacement3)"),
            ExpectedDiagnosticsLocation.TextSpan(SpecializedVectorText_BranchedLoop, target: "SpecializedVectorQuantity", postfix: "(typeof(Position3)"),
            ExpectedDiagnosticsLocation.TextSpan(SpecializedVectorText_BranchedLoop, target: "SpecializedVectorQuantity", postfix: "(typeof(Displacement3))] // 2nd")
        };

        return AssertExactlyQuantityGroupMissingRootDiagnostics(SpecializedVectorText_BranchedLoop, 3).AssertDiagnosticsLocation(expectedLocations).AssertIdenticalSources(Identical);
    }

    private static string SpecializedVectorGroupText_Self => """
        using SharpMeasures.Generators;

        [SpecializedVectorGroup(typeof(Position))]
        public static partial class Position { }

        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }
            
        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertSpecializedVectorGroup_Self()
    {
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(SpecializedVectorGroupText_Self, target: "SpecializedVectorGroup");

        return AssertExactlyQuantityGroupMissingRootDiagnostics(SpecializedVectorGroupText_Self, 1).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(Identical);
    }

    private static string SpecializedVectorGroupText_Loop => """
        using SharpMeasures.Generators;

        [SpecializedVectorGroup(typeof(Displacement))]
        public static partial class Offset { }

        [SpecializedVectorGroup(typeof(Position))]
        public static partial class Displacement { }

        [SpecializedVectorGroup(typeof(Offset))]
        public static partial class Position { }

        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }
            
        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertSpecializedVectorGroup_Loop()
    {
        var expectedLocations = new[]
        {
            ExpectedDiagnosticsLocation.TextSpan(SpecializedVectorGroupText_Loop, target: "SpecializedVectorGroup", postfix: "(typeof(Displacement)"),
            ExpectedDiagnosticsLocation.TextSpan(SpecializedVectorGroupText_Loop, target: "SpecializedVectorGroup", postfix: "(typeof(Position)"),
            ExpectedDiagnosticsLocation.TextSpan(SpecializedVectorGroupText_Loop, target: "SpecializedVectorGroup", postfix: "(typeof(Offset)")
        };

        return AssertExactlyQuantityGroupMissingRootDiagnostics(SpecializedVectorGroupText_Loop, 3).AssertDiagnosticsLocation(expectedLocations).AssertIdenticalSources(Identical);
    }

    private static string SpecializedVectorGroupText_BranchedLoop => """
        using SharpMeasures.Generators;

        [SpecializedVectorGroup(typeof(Displacement))]
        public static partial class Offset { }

        [SpecializedVectorGroup(typeof(Position))]
        public static partial class Displacement { }

        [SpecializedVectorGroup(typeof(Displacement))] // 2nd
        public static partial class Position { }

        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }
            
        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertSpecializedVectorGroup_BranchedLoop()
    {
        var expectedLocations = new[]
        {
            ExpectedDiagnosticsLocation.TextSpan(SpecializedVectorGroupText_BranchedLoop, target: "SpecializedVectorGroup", postfix: "(typeof(Displacement)"),
            ExpectedDiagnosticsLocation.TextSpan(SpecializedVectorGroupText_BranchedLoop, target: "SpecializedVectorGroup", postfix: "(typeof(Position)"),
            ExpectedDiagnosticsLocation.TextSpan(SpecializedVectorGroupText_BranchedLoop, target: "SpecializedVectorGroup", postfix: "(typeof(Displacement))] // 2nd")
        };

        return AssertExactlyQuantityGroupMissingRootDiagnostics(SpecializedVectorGroupText_BranchedLoop, 3).AssertDiagnosticsLocation(expectedLocations).AssertIdenticalSources(Identical);
    }

    private static GeneratorVerifier Identical => GeneratorVerifier.Construct<SharpMeasuresGenerator>(IdenticalText);

    private static string IdenticalText => """
        using SharpMeasures.Generators;

        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }
            
        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;
}
