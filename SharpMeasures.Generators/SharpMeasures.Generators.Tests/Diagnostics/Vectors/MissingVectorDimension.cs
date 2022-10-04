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
        using SharpMeasures.Generators;

        [VectorQuantity(typeof(UnitOfLength))]
        public partial class {{vectorName}} { }

        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }

        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertVector(string vectorName)
    {
        var source = VectorText(vectorName);
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(source, target: "VectorQuantity");

        return AssertExactlyMissingVectorDimensionDiagnostics(source).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(VectorIdentical);
    }

    private static string VectorGroupMemberText(string vectorName) => $$"""
        using SharpMeasures.Generators;

        [VectorGroupMember(typeof(Position))]
        public partial class {{vectorName}} { }

        [VectorGroup(typeof(UnitOfLength))]
        public static partial class Position { }

        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }

        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertVectorGroupMember(string vectorName)
    {
        var source = VectorGroupMemberText(vectorName);
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(source, target: "VectorGroupMember");

        return AssertExactlyMissingVectorDimensionDiagnostics(source).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(VectorGroupMemberIdentical);
    }

    private static GeneratorVerifier VectorIdentical => GeneratorVerifier.Construct<SharpMeasuresGenerator>(VectorIdenticalText);
    private static GeneratorVerifier VectorGroupMemberIdentical => GeneratorVerifier.Construct<SharpMeasuresGenerator>(VectorGroupMemberIdenticalText);

    private static string VectorIdenticalText => """
        using SharpMeasures.Generators;

        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }

        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static string VectorGroupMemberIdenticalText => """
        using SharpMeasures.Generators;

        [VectorGroup(typeof(UnitOfLength))]
        public static partial class Position { }

        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }

        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;
}
