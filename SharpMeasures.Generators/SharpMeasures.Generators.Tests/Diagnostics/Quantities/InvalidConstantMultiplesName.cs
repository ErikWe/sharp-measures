namespace SharpMeasures.Generators.Tests.Diagnostics.Quantities;

using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Tests.Utility;
using SharpMeasures.Generators.Tests.Verify;

using System.Collections.Generic;
using System.Threading.Tasks;

using VerifyXunit;

using Xunit;

[UsesVerify]
public class InvalidConstantMultiplesName
{
    [Fact]
    public Task VerifyInvalidConstantMultiplesNameDiagnosticsMessage_Null() => AssertScalar(NullName).VerifyDiagnostics();

    [Fact]
    public Task VerifyInvalidConstantMultiplesNameDiagnosticsMessage_Empty() => AssertScalar(EmptyName).VerifyDiagnostics();

    [Theory]
    [MemberData(nameof(InvalidConstantMultiplesNames))]
    public void Scalar(SourceSubtext constantMultiplesName) => AssertScalar(constantMultiplesName);

    [Fact]
    public void ScalarUnmatchedRegex() => AssertScalarUnmatchedRegex();

    [Theory]
    [MemberData(nameof(InvalidConstantMultiplesNames))]
    public void SpecializedScalar(SourceSubtext constantMultiplesName) => AssertSpecializedScalar(constantMultiplesName);

    [Fact]
    public void SpecializedScalarUnmatchedRegex() => AssertSpecializedScalarUnmatchedRegex();

    [Theory]
    [MemberData(nameof(InvalidConstantMultiplesNames))]
    public void Vector(SourceSubtext constantMultiplesName) => AssertVector(constantMultiplesName);

    [Fact]
    public void VectorUnmatchedRegex() => AssertVectorUnmatchedRegex();

    [Theory]
    [MemberData(nameof(InvalidConstantMultiplesNames))]
    public void SpecializedVector(SourceSubtext constantMultiplesName) => AssertSpecializedVector(constantMultiplesName);

    [Fact]
    public void SpecializedVectorUnmatchedRegex() => AssertSpecializedVectorUnmatchedRegex();

    [Theory]
    [MemberData(nameof(InvalidConstantMultiplesNames))]
    public void VectorGroupMember(SourceSubtext constantMultiplesName) => AssertVectorGroupMember(constantMultiplesName);

    [Fact]
    public void VectorGroupMemberUnmatchedRegex() => AssertVectorGroupMemberUnmatchedRegex();

    public static IEnumerable<object[]> InvalidConstantMultiplesNames() => new object[][]
    {
        new object[] { NullName },
        new object[] { EmptyName }
    };

    private static SourceSubtext NullName { get; } = SourceSubtext.Covered("null");
    private static SourceSubtext EmptyName { get; } = SourceSubtext.Covered("\"\"");

    private static GeneratorVerifier AssertExactlyInvalidConstantMultiplesNameDiagnostics(string source) => GeneratorVerifier.Construct<SharpMeasuresGenerator>(source).AssertExactlyListedDiagnosticsIDsReported(InvalidConstantMultiplesNameDiagnostics);
    private static IReadOnlyCollection<string> InvalidConstantMultiplesNameDiagnostics { get; } = new string[] { DiagnosticIDs.InvalidConstantMultiplesName };

    private static string ScalarText(SourceSubtext constantMultiplesName) => $$"""
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;

        [ScalarConstant("Planck", "Metre", 1.616255E-35, Multiples = {{constantMultiplesName}})]
        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }
            
        [FixedUnitInstance("Metre", "Metres")]
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertScalar(SourceSubtext constantMultiplesName)
    {
        var source = ScalarText(constantMultiplesName);
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(source, constantMultiplesName.Context.With(outerPrefix: "Multiples = "));

        return AssertExactlyInvalidConstantMultiplesNameDiagnostics(source).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(ScalarIdentical);
    }

    private static string ScalarUnmatchedRegexText => """
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;

        [ScalarConstant("Planck", "Metre", 1.616255E-35, Multiples = "Missing", MultiplesRegexSubstitution = "")]
        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }
            
        [FixedUnitInstance("Metre", "Metres")]
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertScalarUnmatchedRegex()
    {
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(ScalarUnmatchedRegexText, target: "\"Missing\"");

        return AssertExactlyInvalidConstantMultiplesNameDiagnostics(ScalarUnmatchedRegexText).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(ScalarIdentical);
    }

    private static string SpecializedScalarText(SourceSubtext constantMultiplesName) => $$"""
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;

        [ScalarConstant("Planck", "Metre", 1.616255E-35, Multiples = {{constantMultiplesName}})]
        [SpecializedSharpMeasuresScalar(typeof(Length))]
        public partial class Distance { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }
            
        [FixedUnitInstance("Metre", "Metres")]
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertSpecializedScalar(SourceSubtext constantMultiplesName)
    {
        var source = SpecializedScalarText(constantMultiplesName);
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(source, constantMultiplesName.Context.With(outerPrefix: "Multiples = "));

        return AssertExactlyInvalidConstantMultiplesNameDiagnostics(source).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(SpecializedScalarIdentical);
    }

    private static string SpecializedScalarUnmatchedRegexText => """
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;

        [ScalarConstant("Planck", "Metre", 1.616255E-35, Multiples = "Missing", MultiplesRegexSubstitution = "")]
        [SpecializedSharpMeasuresScalar(typeof(Length))]
        public partial class Distance { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }
            
        [FixedUnitInstance("Metre", "Metres")]
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertSpecializedScalarUnmatchedRegex()
    {
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(SpecializedScalarUnmatchedRegexText, target: "\"Missing\"");

        return AssertExactlyInvalidConstantMultiplesNameDiagnostics(SpecializedScalarUnmatchedRegexText).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(SpecializedScalarIdentical);
    }

    private static string VectorText(SourceSubtext constantMultiplesName) => $$"""
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Vectors;

        [VectorConstant("MetreOnes", "Metre", 1, 1, 1, Multiples = {{constantMultiplesName}})]
        [SharpMeasuresVector(typeof(UnitOfLength))]
        public partial class Position3 { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }
            
        [FixedUnitInstance("Metre", "Metres")]
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertVector(SourceSubtext constantMultiplesName)
    {
        var source = VectorText(constantMultiplesName);
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(source, constantMultiplesName.Context.With(outerPrefix: "Multiples = "));

        return AssertExactlyInvalidConstantMultiplesNameDiagnostics(source).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(VectorIdentical);
    }

    private static string VectorUnmatchedRegexText => """
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Vectors;

        [VectorConstant("MetreOnes", "Metre", 1, 1, 1, Multiples = "Missing", MultiplesRegexSubstitution = "")]
        [SharpMeasuresVector(typeof(UnitOfLength))]
        public partial class Position3 { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }
            
        [FixedUnitInstance("Metre", "Metres")]
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertVectorUnmatchedRegex()
    {
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(VectorUnmatchedRegexText, target: "\"Missing\"");

        return AssertExactlyInvalidConstantMultiplesNameDiagnostics(VectorUnmatchedRegexText).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(VectorIdentical);
    }

    private static string SpecializedVectorText(SourceSubtext constantMultiplesName) => $$"""
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Vectors;

        [VectorConstant("MetreOnes", "Metre", 1, 1, 1, Multiples = {{constantMultiplesName}})]
        [SharpMeasuresVector(typeof(UnitOfLength))]
        public partial class Position3 { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }
            
        [FixedUnitInstance("Metre", "Metres")]
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertSpecializedVector(SourceSubtext constantMultiplesName)
    {
        var source = SpecializedVectorText(constantMultiplesName);
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(source, constantMultiplesName.Context.With(outerPrefix: "Multiples = "));

        return AssertExactlyInvalidConstantMultiplesNameDiagnostics(source).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(SpecializedVectorIdentical);
    }

    private static string SpecializedVectorUnmatchedRegexText => """
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Vectors;

        [VectorConstant("MetreOnes", "Metre", 1, 1, 1, Multiples = "Missing", MultiplesRegexSubstitution = "")]
        [SharpMeasuresVector(typeof(UnitOfLength))]
        public partial class Position3 { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }
            
        [FixedUnitInstance("Metre", "Metres")]
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertSpecializedVectorUnmatchedRegex()
    {
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(SpecializedVectorUnmatchedRegexText, target: "\"Missing\"");

        return AssertExactlyInvalidConstantMultiplesNameDiagnostics(SpecializedVectorUnmatchedRegexText).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(SpecializedVectorIdentical);
    }

    private static string VectorGroupMemberText(SourceSubtext constantMultiplesName) => $$"""
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Vectors;

        [VectorConstant("MetreOnes", "Metre", 1, 1, 1, Multiples = {{constantMultiplesName}})]
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

    private static GeneratorVerifier AssertVectorGroupMember(SourceSubtext constantMultiplesName)
    {
        var source = VectorGroupMemberText(constantMultiplesName);
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(source, constantMultiplesName.Context.With(outerPrefix: "Multiples = "));

        return AssertExactlyInvalidConstantMultiplesNameDiagnostics(source).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(VectorGroupMemberIdentical);
    }

    private static string VectorGroupMemberUnmatchedRegexText => """
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Vectors;

        [VectorConstant("MetreOnes", "Metre", 1, 1, 1, Multiples = "Missing", MultiplesRegexSubstitution = "")]
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

    private static GeneratorVerifier AssertVectorGroupMemberUnmatchedRegex()
    {
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(VectorGroupMemberUnmatchedRegexText, target: "\"Missing\"");

        return AssertExactlyInvalidConstantMultiplesNameDiagnostics(VectorGroupMemberUnmatchedRegexText).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(VectorGroupMemberIdentical);
    }

    private static GeneratorVerifier ScalarIdentical => GeneratorVerifier.Construct<SharpMeasuresGenerator>(ScalarIdenticalText);
    private static GeneratorVerifier SpecializedScalarIdentical => GeneratorVerifier.Construct<SharpMeasuresGenerator>(SpecializedScalarIdenticalText);
    private static GeneratorVerifier VectorIdentical => GeneratorVerifier.Construct<SharpMeasuresGenerator>(VectorIdenticalText);
    private static GeneratorVerifier SpecializedVectorIdentical => GeneratorVerifier.Construct<SharpMeasuresGenerator>(SpecializedVectorIdenticalText);
    private static GeneratorVerifier VectorGroupMemberIdentical => GeneratorVerifier.Construct<SharpMeasuresGenerator>(VectorGroupMemberIdenticalText);

    private static string ScalarIdenticalText => """
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;

        [ScalarConstant("Planck", "Metre", 1.616255E-35, GenerateMultiplesProperty = false)]
        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }
            
        [FixedUnitInstance("Metre", "Metres")]
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static string SpecializedScalarIdenticalText => """
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;

        [ScalarConstant("Planck", "Metre", 1.616255E-35, GenerateMultiplesProperty = false)]
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

        [VectorConstant("MetreOnes", "Metre", 1, 1, 1, GenerateMultiplesProperty = false)]
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

        [VectorConstant("MetreOnes", "Metre", 1, 1, 1, GenerateMultiplesProperty = false)]
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

        [VectorConstant("MetreOnes", "Metre", 1, 1, 1, GenerateMultiplesProperty = false)]
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
