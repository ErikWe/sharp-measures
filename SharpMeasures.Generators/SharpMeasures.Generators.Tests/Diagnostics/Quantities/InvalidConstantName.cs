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

    [Fact]
    public Task VerifyInvalidConstantNameDiagnosticsMessage_Empt() => AssertScalar(EmptyName).VerifyDiagnostics();

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
    public void VectorGroupMember(SourceSubtext constantName) => AssertSpecializedVector(constantName);

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
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;

        [ScalarConstant({{constantName}}, "Metre", 1.616255E-35)]
        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }
            
        [FixedUnitInstance("Metre", "Metres")]
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertScalar(SourceSubtext constantName)
    {
        var source = ScalarText(constantName);
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(source, constantName.Context.With(outerPrefix: "ScalarConstant("));

        return AssertExactlyInvalidConstantNameDiagnostics(source).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(ScalarIdentical);
    }

    private static string SpecializedScalarText(SourceSubtext constantName) => $$"""
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;

        [ScalarConstant({{constantName}}, "Metre", 1.616255E-35)]
        [SpecializedSharpMeasuresScalar(typeof(Length))]
        public partial class Distance { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }

        [FixedUnitInstance("Metre", "Metres")]
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertSpecializedScalar(SourceSubtext constantName)
    {
        var source = SpecializedScalarText(constantName);
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(source, constantName.Context.With(outerPrefix: "ScalarConstant("));

        return AssertExactlyInvalidConstantNameDiagnostics(source).AssertDiagnosticsLocation(expectedLocation);
    }

    private static string VectorText(SourceSubtext constantName) => $$"""
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Vectors;

        [VectorConstant({{constantName}}, "Metre", 1, 1, 1)]
        [SharpMeasuresVector(typeof(UnitOfLength))]
        public partial class Position3 { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }

        [FixedUnitInstance("Metre", "Metres")]
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertVector(SourceSubtext constantName)
    {
        var source = VectorText(constantName);
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(source, constantName.Context.With(outerPrefix: "VectorConstant("));

        return AssertExactlyInvalidConstantNameDiagnostics(source).AssertDiagnosticsLocation(expectedLocation);
    }

    private static string SpecializedVectorText(SourceSubtext constantName) => $$"""
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Vectors;

        [VectorConstant({{constantName}}, "Metre", 1, 1, 1)]
        [SpecializedSharpMeasuresVector(typeof(Position3))]
        public partial class Displacement3 { }

        [SharpMeasuresVector(typeof(UnitOfLength))]
        public partial class Position3 { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }

        [FixedUnitInstance("Metre", "Metres")]
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertSpecializedVector(SourceSubtext constantName)
    {
        var source = SpecializedVectorText(constantName);
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(source, constantName.Context.With(outerPrefix: "VectorConstant("));

        return AssertExactlyInvalidConstantNameDiagnostics(source).AssertDiagnosticsLocation(expectedLocation);
    }

    private static string VectorGroupMemberText(SourceSubtext constantName) => $$"""
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Vectors;

        [VectorConstant({{constantName}}, "Metre", 1, 1, 1)]
        [SharpMeasuresVectorGroupMember(typeof(Position))]
        public partial class Position3 { }

        [SharpMeasuresVectorGroup(typeof(UnitOfLength))]
        public static partial class Position { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }

        [FixedUnitInstance("Metre", "Metres")]
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertVectorGroupMember(SourceSubtext constantName)
    {
        var source = VectorGroupMemberText(constantName);
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(source, constantName.Context.With(outerPrefix: "VectorConstant("));

        return AssertExactlyInvalidConstantNameDiagnostics(source).AssertDiagnosticsLocation(expectedLocation);
    }

    private static GeneratorVerifier ScalarIdentical => GeneratorVerifier.Construct<SharpMeasuresGenerator>(ScalarIdenticalText);
    private static GeneratorVerifier SpecializedScalarIdentical => GeneratorVerifier.Construct<SharpMeasuresGenerator>(SpecializedScalarIdenticalText);
    private static GeneratorVerifier VectorIdentical => GeneratorVerifier.Construct<SharpMeasuresGenerator>(VectorIdenticalText);
    private static GeneratorVerifier SpecializedVectorIdentical => GeneratorVerifier.Construct<SharpMeasuresGenerator>(SpecializedVectorIdenticalText);
    private static GeneratorVerifier VectorGroupMemberIdentical => GeneratorVerifier.Construct<SharpMeasuresGenerator>(VectorGroupMemberIdenticalText);

    private static string ScalarIdenticalText => """
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }
            
        [FixedUnitInstance("Metre", "Metres")]
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static string SpecializedScalarIdenticalText => """
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;

        [SpecializedSharpMeasuresScalar(typeof(Length))]
        public partial class Distance { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }

        [FixedUnitInstance("Metre", "Metres")]
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static string VectorIdenticalText => """
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Vectors;

        [SharpMeasuresVector(typeof(UnitOfLength))]
        public partial class Position3 { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }

        [FixedUnitInstance("Metre", "Metres")]
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static string SpecializedVectorIdenticalText => """
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Vectors;

        [SpecializedSharpMeasuresVector(typeof(Position3))]
        public partial class Displacement3 { }

        [SharpMeasuresVector(typeof(UnitOfLength))]
        public partial class Position3 { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }

        [FixedUnitInstance("Metre", "Metres")]
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static string VectorGroupMemberIdenticalText => """
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Vectors;

        [SharpMeasuresVectorGroupMember(typeof(Position))]
        public partial class Position3 { }

        [SharpMeasuresVectorGroup(typeof(UnitOfLength))]
        public static partial class Position { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }

        [FixedUnitInstance("Metre", "Metres")]
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;
}
