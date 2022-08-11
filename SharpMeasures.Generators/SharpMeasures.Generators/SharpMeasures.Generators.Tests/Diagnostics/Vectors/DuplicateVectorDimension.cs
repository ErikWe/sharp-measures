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
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Vectors;

        [SharpMeasuresVectorGroupMember(typeof(Position))]
        public partial class Length3 { }

        [SharpMeasuresVectorGroupMember(typeof(Position))] // <-
        public partial class Position3 { }

        [SharpMeasuresVectorGroup(typeof(UnitOfLength))]
        public static partial class Position { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }

        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertBothImplicit()
    {
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(BothImplicitText, target: "SharpMeasuresVectorGroupMember", postfix: "(typeof(Position))] // <-");

        return AssertExactlyDuplicateVectorDimensionDiagnostics(BothImplicitText).AssertDiagnosticsLocation(expectedLocation, BothImplicitText);
    }

    private static string FirstExplicitText => """
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Vectors;
        
        [SharpMeasuresVectorGroupMember(typeof(Position), Dimension = 3)]
        public partial class PositionN { }
        
        [SharpMeasuresVectorGroupMember(typeof(Position))]
        public partial class Position3 { }
        
        [SharpMeasuresVectorGroup(typeof(UnitOfLength))]
        public static partial class Position { }
        
        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }
        
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertFirstExplicit()
    {
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(FirstExplicitText, target: "SharpMeasuresVectorGroupMember", postfix: "(typeof(Position))]");

        return AssertExactlyDuplicateVectorDimensionDiagnostics(FirstExplicitText).AssertDiagnosticsLocation(expectedLocation, FirstExplicitText);
    }

    private static string SecondExplicitText => """
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Vectors;
        
        [SharpMeasuresVectorGroupMember(typeof(Position))]
        public partial class Position3 { }
        
        [SharpMeasuresVectorGroupMember(typeof(Position), Dimension = 3)]
        public partial class PositionN { }
        
        [SharpMeasuresVectorGroup(typeof(UnitOfLength))]
        public static partial class Position { }
        
        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }
        
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertSecondExplicit()
    {
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(SecondExplicitText, target: "3", prefix: "Dimension = ");

        return AssertExactlyDuplicateVectorDimensionDiagnostics(SecondExplicitText).AssertDiagnosticsLocation(expectedLocation, SecondExplicitText);
    }

    private static string BothExplicitText => """
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Vectors;
        
        [SharpMeasuresVectorGroupMember(typeof(Position), Dimension = 3)]
        public partial class PositionX { }
        
        [SharpMeasuresVectorGroupMember(typeof(Position), Dimension = 3)] // <-
        public partial class PositionN { }
        
        [SharpMeasuresVectorGroup(typeof(UnitOfLength))]
        public static partial class Position { }
        
        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }
        
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertBothExplicit()
    {
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(BothExplicitText, target: "3", prefix: "Dimension = ", postfix: ")] // <-");

        return AssertExactlyDuplicateVectorDimensionDiagnostics(BothExplicitText).AssertDiagnosticsLocation(expectedLocation, BothExplicitText);
    }
}
