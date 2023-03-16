namespace SharpMeasures.Generators.Tests.Diagnostics.Vectors;

using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Tests.Utility;
using SharpMeasures.Generators.Tests.Verify;

using System.Collections.Generic;
using System.Threading.Tasks;

using VerifyXunit;

using Xunit;

[UsesVerify]
public class DuplicateVectorDimension
{
    [Fact]
    public Task BothImplicit() => AssertBothImplicit().VerifyDiagnostics();

    [Fact]
    public void FirstExplicit() => AssertFirstExplicit();

    [Fact]
    public void SecondExplicit() => AssertSecondExplicit();

    [Fact]
    public void BothExplicit() => AssertBothExplicit();

    private static GeneratorVerifier AssertExactlyDuplicateVectorDimensionDiagnostics(string source) => GeneratorVerifier.Construct<SharpMeasuresGenerator>(source).AssertExactlyListedDiagnosticsIDsReported(DuplicateVectorDimensionDiagnostics);
    private static IReadOnlyCollection<string> DuplicateVectorDimensionDiagnostics { get; } = new string[] { DiagnosticIDs.DuplicateVectorDimension };

    private static string BothImplicitText => """
        using SharpMeasures.Generators;

        [VectorGroupMember(typeof(Position))]
        public partial class Length3 { }

        [VectorGroupMember(typeof(Position))] // <-
        public partial class Position3 { }

        [VectorGroup(typeof(UnitOfLength))]
        public static partial class Position { }

        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }

        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertBothImplicit()
    {
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(BothImplicitText, target: "VectorGroupMember", postfix: "(typeof(Position))] // <-");

        return AssertExactlyDuplicateVectorDimensionDiagnostics(BothImplicitText).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(Identical("Length3"));
    }

    private static string FirstExplicitText => """
        using SharpMeasures.Generators;
        
        [VectorGroupMember(typeof(Position), Dimension = 3)]
        public partial class PositionN { }
        
        [VectorGroupMember(typeof(Position))]
        public partial class Position3 { }
        
        [VectorGroup(typeof(UnitOfLength))]
        public static partial class Position { }
        
        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }
        
        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertFirstExplicit()
    {
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(FirstExplicitText, target: "VectorGroupMember", postfix: "(typeof(Position))]");

        return AssertExactlyDuplicateVectorDimensionDiagnostics(FirstExplicitText).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(Identical("PositionN"));
    }

    private static string SecondExplicitText => """
        using SharpMeasures.Generators;
        
        [VectorGroupMember(typeof(Position))]
        public partial class Position3 { }
        
        [VectorGroupMember(typeof(Position), Dimension = 3)]
        public partial class PositionN { }
        
        [VectorGroup(typeof(UnitOfLength))]
        public static partial class Position { }
        
        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }
        
        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertSecondExplicit()
    {
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(SecondExplicitText, target: "3", prefix: "Dimension = ");

        return AssertExactlyDuplicateVectorDimensionDiagnostics(SecondExplicitText).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(Identical("Position3"));
    }

    private static string BothExplicitText => """
        using SharpMeasures.Generators;
        
        [VectorGroupMember(typeof(Position), Dimension = 3)]
        public partial class PositionX { }
        
        [VectorGroupMember(typeof(Position), Dimension = 3)] // <-
        public partial class PositionN { }
        
        [VectorGroup(typeof(UnitOfLength))]
        public static partial class Position { }
        
        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }
        
        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertBothExplicit()
    {
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(BothExplicitText, target: "3", prefix: "Dimension = ", postfix: ")] // <-");

        return AssertExactlyDuplicateVectorDimensionDiagnostics(BothExplicitText).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(Identical("PositionX"));
    }

    private static GeneratorVerifier Identical(string otherName) => GeneratorVerifier.Construct<SharpMeasuresGenerator>(IdenticalText(otherName));

    private static string IdenticalText(string otherName) => $$"""
        using SharpMeasures.Generators;

        [VectorGroupMember(typeof(Position), Dimension = 3)]
        public partial class {{otherName}} { }

        [VectorGroup(typeof(UnitOfLength))]
        public static partial class Position { }

        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }

        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;
}
