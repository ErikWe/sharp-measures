namespace SharpMeasures.Generators.Tests.Diagnostics.Quantities;

using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Tests.Utility;
using SharpMeasures.Generators.Tests.Verify;

using System.Collections.Generic;
using System.Threading.Tasks;

using VerifyXunit;

using Xunit;

[UsesVerify]
public class InvalidConstantName
{
    [Fact]
    public Task VerifyInvalidConstantNameDiagnosticsMessage_Null() => AssertScalar(NullName).VerifyDiagnostics();

    [Theory]
    [MemberData(nameof(InvalidConstantNames))]
    public void Scalar(SourceSubtext constantName) => AssertScalar(constantName);

    [Theory]
    [MemberData(nameof(InvalidConstantNames))]
    public void SpecializedScalar(SourceSubtext constantName) => AssertSpecializedScalar(constantName);

    [Theory]
    [MemberData(nameof(InvalidConstantNames))]
    public void Vector(SourceSubtext constantName) => AssertVector(constantName);

    [Theory]
    [MemberData(nameof(InvalidConstantNames))]
    public void SpecializedVector(SourceSubtext constantName) => AssertSpecializedVector(constantName);

    [Theory]
    [MemberData(nameof(InvalidConstantNames))]
    public void VectorGroupMember(SourceSubtext constantName) => AssertVectorGroupMember(constantName);

    public static IEnumerable<object[]> InvalidConstantNames() => new object[][]
    {
        new object[] { NullName },
        new object[] { EmptyName }
    };

    private static SourceSubtext NullName { get; } = SourceSubtext.Covered("null");
    private static SourceSubtext EmptyName { get; } = SourceSubtext.Covered("\"\"");

    private static GeneratorVerifier AssertExactlyInvalidConstantNameDiagnostics(string source) => GeneratorVerifier.Construct<SharpMeasuresGenerator>(source).AssertExactlyListedDiagnosticsIDsReported(InvalidConstantNameDiagnostics);
    private static IReadOnlyCollection<string> InvalidConstantNameDiagnostics { get; } = new string[] { DiagnosticIDs.InvalidConstantName };

    private static string ScalarText(SourceSubtext constantName) => $$"""
        using SharpMeasures.Generators;

        [ScalarConstant({{constantName}}, "Metre", 1.616255E-35)]
        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }
            
        [FixedUnitInstance("Metre", "Metres")]
        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertScalar(SourceSubtext constantName)
    {
        var source = ScalarText(constantName);
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(source, constantName.Context.With(outerPrefix: "ScalarConstant("));

        return AssertExactlyInvalidConstantNameDiagnostics(source).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(ScalarIdentical);
    }

    private static string SpecializedScalarText(SourceSubtext constantName) => $$"""
        using SharpMeasures.Generators;

        [ScalarConstant({{constantName}}, "Metre", 1.616255E-35)]
        [SpecializedScalarQuantity(typeof(Length))]
        public partial class Distance { }

        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }

        [FixedUnitInstance("Metre", "Metres")]
        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertSpecializedScalar(SourceSubtext constantName)
    {
        var source = SpecializedScalarText(constantName);
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(source, constantName.Context.With(outerPrefix: "ScalarConstant("));

        return AssertExactlyInvalidConstantNameDiagnostics(source).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(SpecializedScalarIdentical);
    }

    private static string VectorText(SourceSubtext constantName) => $$"""
        using SharpMeasures.Generators;

        [VectorConstant({{constantName}}, "Metre", 1, 1, 1)]
        [VectorQuantity(typeof(UnitOfLength))]
        public partial class Position3 { }

        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }

        [FixedUnitInstance("Metre", "Metres")]
        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertVector(SourceSubtext constantName)
    {
        var source = VectorText(constantName);
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(source, constantName.Context.With(outerPrefix: "VectorConstant("));

        return AssertExactlyInvalidConstantNameDiagnostics(source).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(VectorIdentical);
    }

    private static string SpecializedVectorText(SourceSubtext constantName) => $$"""
        using SharpMeasures.Generators;

        [VectorConstant({{constantName}}, "Metre", 1, 1, 1)]
        [SpecializedVectorQuantity(typeof(Position3))]
        public partial class Displacement3 { }

        [VectorQuantity(typeof(UnitOfLength))]
        public partial class Position3 { }

        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }

        [FixedUnitInstance("Metre", "Metres")]
        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertSpecializedVector(SourceSubtext constantName)
    {
        var source = SpecializedVectorText(constantName);
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(source, constantName.Context.With(outerPrefix: "VectorConstant("));

        return AssertExactlyInvalidConstantNameDiagnostics(source).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(SpecializedVectorIdentical);
    }

    private static string VectorGroupMemberText(SourceSubtext constantName) => $$"""
        using SharpMeasures.Generators;

        [VectorConstant({{constantName}}, "Metre", 1, 1, 1)]
        [VectorGroupMember(typeof(Position))]
        public partial class Position3 { }

        [VectorGroup(typeof(UnitOfLength))]
        public static partial class Position { }

        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }

        [FixedUnitInstance("Metre", "Metres")]
        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertVectorGroupMember(SourceSubtext constantName)
    {
        var source = VectorGroupMemberText(constantName);
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(source, constantName.Context.With(outerPrefix: "VectorConstant("));

        return AssertExactlyInvalidConstantNameDiagnostics(source).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(VectorGroupMemberIdentical);
    }

    private static GeneratorVerifier ScalarIdentical => GeneratorVerifier.Construct<SharpMeasuresGenerator>(ScalarIdenticalText);
    private static GeneratorVerifier SpecializedScalarIdentical => GeneratorVerifier.Construct<SharpMeasuresGenerator>(SpecializedScalarIdenticalText);
    private static GeneratorVerifier VectorIdentical => GeneratorVerifier.Construct<SharpMeasuresGenerator>(VectorIdenticalText);
    private static GeneratorVerifier SpecializedVectorIdentical => GeneratorVerifier.Construct<SharpMeasuresGenerator>(SpecializedVectorIdenticalText);
    private static GeneratorVerifier VectorGroupMemberIdentical => GeneratorVerifier.Construct<SharpMeasuresGenerator>(VectorGroupMemberIdenticalText);

    private static string ScalarIdenticalText => """
        using SharpMeasures.Generators;

        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }
            
        [FixedUnitInstance("Metre", "Metres")]
        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static string SpecializedScalarIdenticalText => """
        using SharpMeasures.Generators;

        [SpecializedScalarQuantity(typeof(Length))]
        public partial class Distance { }

        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }

        [FixedUnitInstance("Metre", "Metres")]
        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static string VectorIdenticalText => """
        using SharpMeasures.Generators;

        [VectorQuantity(typeof(UnitOfLength))]
        public partial class Position3 { }

        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }

        [FixedUnitInstance("Metre", "Metres")]
        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static string SpecializedVectorIdenticalText => """
        using SharpMeasures.Generators;

        [SpecializedVectorQuantity(typeof(Position3))]
        public partial class Displacement3 { }

        [VectorQuantity(typeof(UnitOfLength))]
        public partial class Position3 { }

        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }

        [FixedUnitInstance("Metre", "Metres")]
        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static string VectorGroupMemberIdenticalText => """
        using SharpMeasures.Generators;

        [VectorGroupMember(typeof(Position))]
        public partial class Position3 { }

        [VectorGroup(typeof(UnitOfLength))]
        public static partial class Position { }

        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }

        [FixedUnitInstance("Metre", "Metres")]
        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;
}
