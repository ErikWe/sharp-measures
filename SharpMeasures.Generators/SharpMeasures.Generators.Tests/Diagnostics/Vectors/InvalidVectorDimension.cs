namespace SharpMeasures.Generators.Tests.Diagnostics.Vectors;

using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Tests.Utility;
using SharpMeasures.Generators.Tests.Verify;

using System.Collections.Generic;
using System.Threading.Tasks;

using VerifyXunit;

using Xunit;

[UsesVerify]
public class InvalidVectorDimension
{
    [Fact]
    public Task VerifyInvalidVectorDimensionDiagnosticsMessage() => AssertImplicitVector("1").VerifyDiagnostics();

    [Theory]
    [MemberData(nameof(ImplicitInvalidVectorDimensions))]
    public void ImplicitVector(string dimension) => AssertImplicitVector(dimension);

    [Theory]
    [MemberData(nameof(ExplicitInvalidVectorDimensions))]
    public void ExplicitVector(string dimension) => AssertExplicitVector(dimension);

    [Theory]
    [MemberData(nameof(ImplicitInvalidVectorDimensions))]
    public void ImplicitVectorGroupMember(string dimension) => AssertImplicitVectorGroupMember(dimension);

    [Theory]
    [MemberData(nameof(ExplicitInvalidVectorDimensions))]
    public void ExplicitVectorGroupMember(string dimension) => AssertExplicitVectorGroupMember(dimension);

    public static IEnumerable<object[]> ImplicitInvalidVectorDimensions => new object[][]
    {
        new[] { "0" },
        new[] { "1" },
        new[] { "42" }
    };

    public static IEnumerable<object[]> ExplicitInvalidVectorDimensions => new object[][]
    {
        new[] { "0" },
        new[] { "1" },
        new[] { "42" },
        new[] { "-1" }
    };

    private static GeneratorVerifier AssertExactlyInvalidVectorDimensionDiagnostics(string source) => GeneratorVerifier.Construct<SharpMeasuresGenerator>(source).AssertExactlyListedDiagnosticsIDsReported(InvalidVectorDimensionDiagnostics);
    private static IReadOnlyCollection<string> InvalidVectorDimensionDiagnostics { get; } = new string[] { DiagnosticIDs.InvalidVectorDimension };

    private static string ImplicitVectorText(string dimension) => $$"""
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Vectors;

        [SharpMeasuresVector(typeof(UnitOfLength))]
        public partial class Position{{dimension}} { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }

        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertImplicitVector(string dimension)
    {
        var source = ImplicitVectorText(dimension);
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(source, target: "SharpMeasuresVector");

        return AssertExactlyInvalidVectorDimensionDiagnostics(source).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(VectorIdentical);
    }

    private static string ExplicitVectorText(string dimension) => $$"""
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Vectors;

        [SharpMeasuresVector(typeof(UnitOfLength), Dimension = {{dimension}})]
        public partial class PositionN { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }

        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertExplicitVector(string dimension)
    {
        var source = ExplicitVectorText(dimension);
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(source, target: dimension, prefix: "Dimension = ");

        return AssertExactlyInvalidVectorDimensionDiagnostics(source).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(VectorIdentical);
    }

    private static string ImplicitVectorGroupMemberText(string dimension) => $$"""
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Vectors;

        [SharpMeasuresVectorGroupMember(typeof(Position))]
        public partial class Position{{dimension}} { }

        [SharpMeasuresVectorGroup(typeof(UnitOfLength))]
        public static partial class Position { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }

        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertImplicitVectorGroupMember(string dimension)
    {
        var source = ImplicitVectorGroupMemberText(dimension);
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(source, target: "SharpMeasuresVectorGroupMember");

        return AssertExactlyInvalidVectorDimensionDiagnostics(source).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(VectorGroupMemberIdentical);
    }

    private static string ExplicitVectorGroupMemberText(string dimension) => $$"""
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Vectors;

        [SharpMeasuresVectorGroupMember(typeof(Position), Dimension = {{dimension}})]
        public partial class PositionN { }

        [SharpMeasuresVectorGroup(typeof(UnitOfLength))]
        public static partial class Position { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }

        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertExplicitVectorGroupMember(string dimension)
    {
        var source = ExplicitVectorGroupMemberText(dimension);
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(source, target: dimension, prefix: "Dimension = ");

        return AssertExactlyInvalidVectorDimensionDiagnostics(source).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(VectorGroupMemberIdentical);
    }

    private static GeneratorVerifier VectorIdentical => GeneratorVerifier.Construct<SharpMeasuresGenerator>(VectorIdenticalText);
    private static GeneratorVerifier VectorGroupMemberIdentical => GeneratorVerifier.Construct<SharpMeasuresGenerator>(VectorGroupMemberIdenticalText);

    private static string VectorIdenticalText => """
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Vectors;

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }

        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static string VectorGroupMemberIdenticalText => """
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Vectors;

        [SharpMeasuresVectorGroup(typeof(UnitOfLength))]
        public static partial class Position { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }

        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;
}
