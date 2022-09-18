namespace SharpMeasures.Generators.Tests.Diagnostics.Quantities;

using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Tests.Utility;
using SharpMeasures.Generators.Tests.Verify;

using System.Collections.Generic;
using System.Threading.Tasks;

using VerifyXunit;

using Xunit;

[UsesVerify]
public class MalformedDerivationExpression
{
    [Fact]
    public Task VerifyMalformedDerivationExpressionDiagnosticsMessage() => AssertScalar(StartingWithOperator).VerifyDiagnostics();

    [Theory]
    [MemberData(nameof(MalformedExpressions))]
    public void Scalar(TextConfig config) => AssertScalar(config);

    [Theory]
    [MemberData(nameof(MalformedExpressions))]
    public void SpecializedScalar(TextConfig config) => AssertSpecializedScalar(config);

    [Theory]
    [MemberData(nameof(MalformedExpressions))]
    public void Vector(TextConfig config) => AssertVector(config);

    [Theory]
    [MemberData(nameof(MalformedExpressions))]
    public void SpecializedVector(TextConfig config) => AssertSpecializedVector(config);

    [Theory]
    [MemberData(nameof(MalformedExpressions))]
    public void VectorGroup(TextConfig config) => AssertVectorGroup(config);

    [Theory]
    [MemberData(nameof(MalformedExpressions))]
    public void SpecializedVectorGroup(TextConfig config) => AssertSpecializedVectorGroup(config);

    [Theory]
    [MemberData(nameof(MalformedExpressions))]
    public void VectorGroupMember(TextConfig config) => AssertVectorGroupMember(config);

    public static IEnumerable<object[]> MalformedExpressions => new object[][]
    {
        new object[] { StartingWithOperator },
        new object[] { EndingWithOperator },
        new object[] { UnclosedParenthesis },
        new object[] { UnopenedParenthesis },
        new object[] { UnclosedFormatting },
        new object[] { UnopenedFormatting },
        new object[] { OperatorInFormatting },
        new object[] { EmptyParenthesis },
        new object[] { EmptyFormatting },
        new object[] { UnsupportedSymbol },
        new object[] { OperatorAfterOpeningBracket },
        new object[] { FormattingAfterClosingBracket },
    };

    private static TextConfig StartingWithOperator { get; } = new("+ {0}", "typeof(Length)");
    private static TextConfig EndingWithOperator { get; } = new("{0} +", "typeof(Length)");
    private static TextConfig UnclosedParenthesis { get; } = new("{0} + ({0} * {1}", "typeof(Length), typeof(Distance)");
    private static TextConfig UnopenedParenthesis { get; } = new("{0} * {1})", "typeof(Length), typeof(Distance)");
    private static TextConfig UnclosedFormatting { get; } = new("{0} + {1", "typeof(Length)");
    private static TextConfig UnopenedFormatting { get; } = new("{0} + }", "typeof(Length)");
    private static TextConfig OperatorInFormatting { get; } = new("{0} + {1*}", "typeof(Length), typeof(Distance)");
    private static TextConfig EmptyParenthesis { get; } = new("{0} * ()", "typeof(Length)");
    private static TextConfig EmptyFormatting { get; } = new("{0} * {}", "typeof(Length)");
    private static TextConfig UnsupportedSymbol { get; } = new("{0} * a", "typeof(Length)");
    private static TextConfig OperatorAfterOpeningBracket { get; } = new("{0} + (* {1})", "typeof(Length), typeof(Distance)");
    private static TextConfig FormattingAfterClosingBracket { get; } = new("({0}) {1}", "typeof(Length), typeof(Distance)");

    public readonly record struct TextConfig(string Expressiom, string Signature);

    private static GeneratorVerifier AssertExactlyMalformedDerivationExpressionDiagnostics(string source) => GeneratorVerifier.Construct<SharpMeasuresGenerator>(source).AssertExactlyListedDiagnosticsIDsReported(MalformedDerivationExpressionDiagnostics);
    private static IReadOnlyCollection<string> MalformedDerivationExpressionDiagnostics { get; } = new string[] { DiagnosticIDs.MalformedDerivationExpression };

    private static string ScalarText(TextConfig config) => $$"""
        using SharpMeasures.Generators.Quantities;
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;

        [DerivedQuantity("{{config.Expressiom}}", {{config.Signature}})]
        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Height { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Distance { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }
            
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertScalar(TextConfig config)
    {
        var source = ScalarText(config);
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(source, target: $"\"{config.Expressiom}\"", prefix: "[DerivedQuantity(");

        return AssertExactlyMalformedDerivationExpressionDiagnostics(source).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(ScalarIdentical);
    }

    private static string SpecializedScalarText(TextConfig config) => $$"""
        using SharpMeasures.Generators.Quantities;
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        
        [DerivedQuantity("{{config.Expressiom}}", {{config.Signature}})]
        [SpecializedSharpMeasuresScalar(typeof(Length))]
        public partial class Height { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Distance { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }
            
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertSpecializedScalar(TextConfig config)
    {
        var source = SpecializedScalarText(config);
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(source, target: $"\"{config.Expressiom}\"", prefix: "[DerivedQuantity(");

        return AssertExactlyMalformedDerivationExpressionDiagnostics(source).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(SpecializedScalarIdentical);
    }

    private static string VectorText(TextConfig config) => $$"""
        using SharpMeasures.Generators.Quantities;
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Vectors;
        
        [DerivedQuantity("{{config.Expressiom}}", {{config.Signature}})]
        [SharpMeasuresVector(typeof(UnitOfLength))]
        public partial class Position3 { }
        
        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Distance { }
        
        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }
            
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertVector(TextConfig config)
    {
        var source = VectorText(config);
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(source, target: $"\"{config.Expressiom}\"", prefix: "[DerivedQuantity(");

        return AssertExactlyMalformedDerivationExpressionDiagnostics(source).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(VectorIdentical);
    }

    private static string SpecializedVectorText(TextConfig config) => $$"""
        using SharpMeasures.Generators.Quantities;
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Vectors;
        
        [DerivedQuantity("{{config.Expressiom}}", {{config.Signature}})]
        [SpecializedSharpMeasuresVector(typeof(Position3))]
        public partial class Displacement3 { }

        [SharpMeasuresVector(typeof(UnitOfLength))]
        public partial class Position3 { }
        
        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Distance { }
        
        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }
            
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertSpecializedVector(TextConfig config)
    {
        var source = SpecializedVectorText(config);
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(source, target: $"\"{config.Expressiom}\"", prefix: "[DerivedQuantity(");

        return AssertExactlyMalformedDerivationExpressionDiagnostics(source).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(SpecializedVectorIdentical);
    }

    private static string VectorGroupText(TextConfig config) => $$"""
        using SharpMeasures.Generators.Quantities;
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Vectors;
        
        [DerivedQuantity("{{config.Expressiom}}", {{config.Signature}})]
        [SharpMeasuresVectorGroup(typeof(UnitOfLength))]
        public static partial class Position { }
        
        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Distance { }
        
        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }
            
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertVectorGroup(TextConfig config)
    {
        var source = VectorGroupText(config);
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(source, target: $"\"{config.Expressiom}\"", prefix: "[DerivedQuantity(");

        return AssertExactlyMalformedDerivationExpressionDiagnostics(source).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(VectorGroupIdentical);
    }

    private static string SpecializedVectorGroupText(TextConfig config) => $$"""
        using SharpMeasures.Generators.Quantities;
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Vectors;
        
        [DerivedQuantity("{{config.Expressiom}}", {{config.Signature}})]
        [SpecializedSharpMeasuresVectorGroup(typeof(Position))]
        public static partial class Displacement { }

        [SharpMeasuresVectorGroup(typeof(UnitOfLength))]
        public static partial class Position { }
        
        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Distance { }
        
        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }
            
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertSpecializedVectorGroup(TextConfig config)
    {
        var source = SpecializedVectorGroupText(config);
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(source, target: $"\"{config.Expressiom}\"", prefix: "[DerivedQuantity(");

        return AssertExactlyMalformedDerivationExpressionDiagnostics(source).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(SpecializedVectorGroupIdentical);
    }

    private static string VectorGroupMemberText(TextConfig config) => $$"""
        using SharpMeasures.Generators.Quantities;
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Vectors;
        
        [DerivedQuantity("{{config.Expressiom}}", {{config.Signature}})]
        [SharpMeasuresVectorGroupMember(typeof(Position))]
        public partial class Position3 { }

        [SharpMeasuresVectorGroup(typeof(UnitOfLength))]
        public static partial class Position { }
        
        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Distance { }
        
        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }
            
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertVectorGroupMember(TextConfig config)
    {
        var source = VectorGroupMemberText(config);
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(source, target: $"\"{config.Expressiom}\"", prefix: "[DerivedQuantity(");

        return AssertExactlyMalformedDerivationExpressionDiagnostics(source).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(VectorGroupMemberIdentical);
    }

    private static GeneratorVerifier ScalarIdentical => GeneratorVerifier.Construct<SharpMeasuresGenerator>(ScalarIdenticalText);
    private static GeneratorVerifier SpecializedScalarIdentical => GeneratorVerifier.Construct<SharpMeasuresGenerator>(SpecializedScalarIdenticalText);
    private static GeneratorVerifier VectorIdentical => GeneratorVerifier.Construct<SharpMeasuresGenerator>(VectorIdenticalText);
    private static GeneratorVerifier SpecializedVectorIdentical => GeneratorVerifier.Construct<SharpMeasuresGenerator>(SpecializedVectorIdenticalText);
    private static GeneratorVerifier VectorGroupIdentical => GeneratorVerifier.Construct<SharpMeasuresGenerator>(VectorGroupIdenticalText);
    private static GeneratorVerifier SpecializedVectorGroupIdentical => GeneratorVerifier.Construct<SharpMeasuresGenerator>(SpecializedVectorGroupIdenticalText);
    private static GeneratorVerifier VectorGroupMemberIdentical => GeneratorVerifier.Construct<SharpMeasuresGenerator>(VectorGroupMemberIdenticalText);

    private static string ScalarIdenticalText => """
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        
        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Height { }
        
        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Distance { }
        
        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }
            
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static string SpecializedScalarIdenticalText => """
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        
        [SpecializedSharpMeasuresScalar(typeof(Length))]
        public partial class Height { }
        
        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Distance { }
        
        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }
            
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
        public partial class Distance { }
        
        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }
            
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
        public partial class Distance { }
        
        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }
            
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static string VectorGroupIdenticalText => """
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Vectors;
        
        [SharpMeasuresVectorGroup(typeof(UnitOfLength))]
        public static partial class Position { }
        
        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Distance { }
        
        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }
            
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static string SpecializedVectorGroupIdenticalText => """
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Vectors;
        
        [SpecializedSharpMeasuresVectorGroup(typeof(Position))]
        public static partial class Displacement { }
        
        [SharpMeasuresVectorGroup(typeof(UnitOfLength))]
        public static partial class Position { }
        
        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Distance { }
        
        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }
            
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
        public partial class Distance { }
        
        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }
            
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;
}
