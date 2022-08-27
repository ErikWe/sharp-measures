namespace SharpMeasures.Generators.Tests.Diagnostics.Vectors;

using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Tests.Utility;
using SharpMeasures.Generators.Tests.Verify;

using System.Collections.Generic;
using System.Threading.Tasks;

using VerifyXunit;

using Xunit;

[UsesVerify]
public class MissingVectorDimension
{
    [Fact]
    public Task VerifyMissingVectorDimensionDiagnosticsMessage() => AssertVector(NoNumberName).VerifyDiagnostics();

    [Theory]
    [MemberData(nameof(MissingVectorDimensionNames))]
    public void Vector(string vectorName) => AssertVector(vectorName);

    [Theory]
    [MemberData(nameof(MissingVectorDimensionNames))]
    public void VectorGroupMember(string vectorName) => AssertVectorGroupMember(vectorName);

    public static IEnumerable<object[]> MissingVectorDimensionNames => new object[][]
    {
        new[] { NoNumberName },
        new[] { NumberInMiddleName }
    };

    private static string NoNumberName { get; } = "LengthVector";
    private static string NumberInMiddleName { get; } = "Position2Vector";

    private static GeneratorVerifier AssertExactlyMissingVectorDimensionDiagnostics(string source) => GeneratorVerifier.Construct<SharpMeasuresGenerator>(source).AssertExactlyListedDiagnosticsIDsReported(MissingVectorDimensionDiagnostics);
    private static IReadOnlyCollection<string> MissingVectorDimensionDiagnostics { get; } = new string[] { DiagnosticIDs.MissingVectorDimension };

    private static string VectorText(string vectorName) => $$"""
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Vectors;

        [SharpMeasuresVector(typeof(UnitOfLength))]
        public partial class {{vectorName}} { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }

        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertVector(string vectorName)
    {
        var source = VectorText(vectorName);
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(source, target: "SharpMeasuresVector");

        return AssertExactlyMissingVectorDimensionDiagnostics(source).AssertDiagnosticsLocation(expectedLocation, source);
    }

    private static string VectorGroupMemberText(string vectorName) => $$"""
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Vectors;

        [SharpMeasuresVectorGroupMember(typeof(Position))]
        public partial class {{vectorName}} { }

        [SharpMeasuresVectorGroup(typeof(UnitOfLength))]
        public static partial class Position { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }

        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertVectorGroupMember(string vectorName)
    {
        var source = VectorGroupMemberText(vectorName);
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(source, target: "SharpMeasuresVectorGroupMember");

        return AssertExactlyMissingVectorDimensionDiagnostics(source).AssertDiagnosticsLocation(expectedLocation, source);
    }
}
