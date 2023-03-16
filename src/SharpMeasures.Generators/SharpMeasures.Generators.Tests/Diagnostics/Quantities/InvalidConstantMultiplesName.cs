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
        using SharpMeasures.Generators;

        [ScalarConstant("Planck", "Metre", 1.616255E-35, Multiples = {{constantMultiplesName}})]
        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }
            
        [FixedUnitInstance("Metre", "Metres")]
        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertScalar(SourceSubtext constantMultiplesName)
    {
        var source = ScalarText(constantMultiplesName);
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(source, constantMultiplesName.Context.With(outerPrefix: "Multiples = "));

        return AssertExactlyInvalidConstantMultiplesNameDiagnostics(source).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(ScalarIdentical);
    }

    private static string ScalarUnmatchedRegexText => """
        using SharpMeasures.Generators;

        [ScalarConstant("Planck", "Metre", 1.616255E-35, Multiples = "Missing", MultiplesRegexSubstitution = "")]
        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }
            
        [FixedUnitInstance("Metre", "Metres")]
        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertScalarUnmatchedRegex()
    {
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(ScalarUnmatchedRegexText, target: "\"Missing\"");

        return AssertExactlyInvalidConstantMultiplesNameDiagnostics(ScalarUnmatchedRegexText).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(ScalarIdentical);
    }

    private static string SpecializedScalarText(SourceSubtext constantMultiplesName) => $$"""
        using SharpMeasures.Generators;

        [ScalarConstant("Planck", "Metre", 1.616255E-35, Multiples = {{constantMultiplesName}})]
        [SpecializedScalarQuantity(typeof(Length))]
        public partial class Distance { }

        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }
            
        [FixedUnitInstance("Metre", "Metres")]
        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertSpecializedScalar(SourceSubtext constantMultiplesName)
    {
        var source = SpecializedScalarText(constantMultiplesName);
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(source, constantMultiplesName.Context.With(outerPrefix: "Multiples = "));

        return AssertExactlyInvalidConstantMultiplesNameDiagnostics(source).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(SpecializedScalarIdentical);
    }

    private static string SpecializedScalarUnmatchedRegexText => """
        using SharpMeasures.Generators;

        [ScalarConstant("Planck", "Metre", 1.616255E-35, Multiples = "Missing", MultiplesRegexSubstitution = "")]
        [SpecializedScalarQuantity(typeof(Length))]
        public partial class Distance { }

        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }
            
        [FixedUnitInstance("Metre", "Metres")]
        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertSpecializedScalarUnmatchedRegex()
    {
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(SpecializedScalarUnmatchedRegexText, target: "\"Missing\"");

        return AssertExactlyInvalidConstantMultiplesNameDiagnostics(SpecializedScalarUnmatchedRegexText).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(SpecializedScalarIdentical);
    }

    private static string VectorText(SourceSubtext constantMultiplesName) => $$"""
        using SharpMeasures.Generators;

        [VectorConstant("MetreOnes", "Metre", 1, 1, 1, Multiples = {{constantMultiplesName}})]
        [VectorQuantity(typeof(UnitOfLength))]
        public partial class Position3 { }

        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }
            
        [FixedUnitInstance("Metre", "Metres")]
        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertVector(SourceSubtext constantMultiplesName)
    {
        var source = VectorText(constantMultiplesName);
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(source, constantMultiplesName.Context.With(outerPrefix: "Multiples = "));

        return AssertExactlyInvalidConstantMultiplesNameDiagnostics(source).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(VectorIdentical);
    }

    private static string VectorUnmatchedRegexText => """
        using SharpMeasures.Generators;

        [VectorConstant("MetreOnes", "Metre", 1, 1, 1, Multiples = "Missing", MultiplesRegexSubstitution = "")]
        [VectorQuantity(typeof(UnitOfLength))]
        public partial class Position3 { }

        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }
            
        [FixedUnitInstance("Metre", "Metres")]
        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertVectorUnmatchedRegex()
    {
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(VectorUnmatchedRegexText, target: "\"Missing\"");

        return AssertExactlyInvalidConstantMultiplesNameDiagnostics(VectorUnmatchedRegexText).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(VectorIdentical);
    }

    private static string SpecializedVectorText(SourceSubtext constantMultiplesName) => $$"""
        using SharpMeasures.Generators;

        [VectorConstant("MetreOnes", "Metre", 1, 1, 1, Multiples = {{constantMultiplesName}})]
        [VectorQuantity(typeof(UnitOfLength))]
        public partial class Position3 { }

        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }
            
        [FixedUnitInstance("Metre", "Metres")]
        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertSpecializedVector(SourceSubtext constantMultiplesName)
    {
        var source = SpecializedVectorText(constantMultiplesName);
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(source, constantMultiplesName.Context.With(outerPrefix: "Multiples = "));

        return AssertExactlyInvalidConstantMultiplesNameDiagnostics(source).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(SpecializedVectorIdentical);
    }

    private static string SpecializedVectorUnmatchedRegexText => """
        using SharpMeasures.Generators;

        [VectorConstant("MetreOnes", "Metre", 1, 1, 1, Multiples = "Missing", MultiplesRegexSubstitution = "")]
        [VectorQuantity(typeof(UnitOfLength))]
        public partial class Position3 { }

        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }
            
        [FixedUnitInstance("Metre", "Metres")]
        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertSpecializedVectorUnmatchedRegex()
    {
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(SpecializedVectorUnmatchedRegexText, target: "\"Missing\"");

        return AssertExactlyInvalidConstantMultiplesNameDiagnostics(SpecializedVectorUnmatchedRegexText).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(SpecializedVectorIdentical);
    }

    private static string VectorGroupMemberText(SourceSubtext constantMultiplesName) => $$"""
        using SharpMeasures.Generators;

        [VectorConstant("MetreOnes", "Metre", 1, 1, 1, Multiples = {{constantMultiplesName}})]
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

    private static GeneratorVerifier AssertVectorGroupMember(SourceSubtext constantMultiplesName)
    {
        var source = VectorGroupMemberText(constantMultiplesName);
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(source, constantMultiplesName.Context.With(outerPrefix: "Multiples = "));

        return AssertExactlyInvalidConstantMultiplesNameDiagnostics(source).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(VectorGroupMemberIdentical);
    }

    private static string VectorGroupMemberUnmatchedRegexText => """
        using SharpMeasures.Generators;

        [VectorConstant("MetreOnes", "Metre", 1, 1, 1, Multiples = "Missing", MultiplesRegexSubstitution = "")]
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
        using SharpMeasures.Generators;

        [ScalarConstant("Planck", "Metre", 1.616255E-35, GenerateMultiplesProperty = false)]
        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }
            
        [FixedUnitInstance("Metre", "Metres")]
        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static string SpecializedScalarIdenticalText => """
        using SharpMeasures.Generators;

        [ScalarConstant("Planck", "Metre", 1.616255E-35, GenerateMultiplesProperty = false)]
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

        [VectorConstant("MetreOnes", "Metre", 1, 1, 1, GenerateMultiplesProperty = false)]
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

        [VectorConstant("MetreOnes", "Metre", 1, 1, 1, GenerateMultiplesProperty = false)]
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

        [VectorConstant("MetreOnes", "Metre", 1, 1, 1, GenerateMultiplesProperty = false)]
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
