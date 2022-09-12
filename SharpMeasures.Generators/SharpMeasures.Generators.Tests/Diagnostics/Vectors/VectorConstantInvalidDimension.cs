namespace SharpMeasures.Generators.Tests.Diagnostics.Vectors;

using Microsoft.CodeAnalysis.Text;

using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Tests.Utility;
using SharpMeasures.Generators.Tests.Verify;

using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

using VerifyXunit;

using Xunit;

[UsesVerify]
public class VectorConstantInvalidDimension
{
    [Fact]
    public Task VerifyVectorConstantInvalidDimensionDiagnosticsMessage() => AssertVector(ZeroValues).VerifyDiagnostics();

    [Theory]
    [MemberData(nameof(InvalidConstantValues))]
    public void Vector(TextConfig config) => AssertVector(config);

    [Theory]
    [MemberData(nameof(InvalidConstantValues))]
    public void SpecializedVector(TextConfig config) => AssertSpecializedVector(config);

    [Theory]
    [MemberData(nameof(InvalidConstantValues))]
    public void VectorGroupMember(TextConfig config) => AssertVectorGroupMember(config);
        
    public static IEnumerable<object[]> InvalidConstantValues => new object[][]
    {
        new object[] { ZeroValues },
        new object[] { TwoValues },
        new object[] { FourValues }
    };

    private static TextConfig ZeroValues { get; } = new(SourceSubtext.Covered(string.Empty), true);
    private static TextConfig TwoValues { get; } = new(SourceSubtext.Covered("1, 1", prefix: ", "), false);
    private static TextConfig FourValues { get; } = new(SourceSubtext.Covered("1, 1, 1, 1", prefix: ", "), false);

    [SuppressMessage("Design", "CA1034", Justification = "Test-method argument")]
    public readonly record struct TextConfig(SourceSubtext Text, bool TargetAttribute);

    private static GeneratorVerifier AssertExactlyVectorConstantInvalidDimensionDiagnostics(string source) => GeneratorVerifier.Construct<SharpMeasuresGenerator>(source).AssertExactlyListedDiagnosticsIDsReported(VectorConstantInvalidDimensionDiagnostics);
    private static IReadOnlyCollection<string> VectorConstantInvalidDimensionDiagnostics { get; } = new string[] { DiagnosticIDs.VectorConstantInvalidDimension };

    private static TextSpan ParseExpectedLocation(string source, TextConfig config)
    {
        if (config.TargetAttribute)
        {
            return ExpectedDiagnosticsLocation.TextSpan(source, target: "VectorConstant");
        }

        return ExpectedDiagnosticsLocation.TextSpan(source, config.Text.Context);
    }

    private static string VectorText(SourceSubtext vectorConstant) => $$"""
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Vectors;

        [VectorConstant("MetreOnes", "Metre"{{vectorConstant}})]
        [SharpMeasuresVector(typeof(UnitOfLength))]
        public partial class Position3 { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }

        [FixedUnitInstance("Metre", "Metres")]
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertVector(TextConfig config)
    {
        var source = VectorText(config.Text);
        var expectedLocation = ParseExpectedLocation(source, config);

        return AssertExactlyVectorConstantInvalidDimensionDiagnostics(source).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(VectorIdentical);
    }

    private static string SpecializedVectorText(SourceSubtext vectorConstant) => $$"""
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Vectors;
        
        [VectorConstant("MetreOnes", "Metre"{{vectorConstant}})]
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

    private static GeneratorVerifier AssertSpecializedVector(TextConfig config)
    {
        var source = SpecializedVectorText(config.Text);
        var expectedLocation = ParseExpectedLocation(source, config);

        return AssertExactlyVectorConstantInvalidDimensionDiagnostics(source).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(SpecializedVectorIdentical);
    }

    private static string VectorGroupMemberText(SourceSubtext vectorConstant) => $$"""
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Vectors;
        
        [VectorConstant("MetreOnes", "Metre"{{vectorConstant}})]
        [SharpMeasuresVectorGroupMember(typeof(Position))]
        public partial class Displacement3 { }

        [SharpMeasuresVectorGroup(typeof(UnitOfLength))]
        public static partial class Position { }
        
        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }
        
        [FixedUnitInstance("Metre", "Metres")]
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertVectorGroupMember(TextConfig config)
    {
        var source = VectorGroupMemberText(config.Text);
        var expectedLocation = ParseExpectedLocation(source, config);

        return AssertExactlyVectorConstantInvalidDimensionDiagnostics(source).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(VectorGroupMemberIdentical);
    }

    private static GeneratorVerifier VectorIdentical => GeneratorVerifier.Construct<SharpMeasuresGenerator>(VectorIdenticalText);
    private static GeneratorVerifier SpecializedVectorIdentical => GeneratorVerifier.Construct<SharpMeasuresGenerator>(SpecializedVectorIdenticalText);
    private static GeneratorVerifier VectorGroupMemberIdentical => GeneratorVerifier.Construct<SharpMeasuresGenerator>(VectorGroupMemberIdenticalText);

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
        public partial class Displacement3 { }

        [SharpMeasuresVectorGroup(typeof(UnitOfLength))]
        public static partial class Position { }
        
        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }
        
        [FixedUnitInstance("Metre", "Metres")]
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;
}
