namespace SharpMeasures.Generators.Tests.Diagnostics.Quantities;

using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Tests.Utility;
using SharpMeasures.Generators.Tests.Verify;

using System.Collections.Generic;
using System.Threading.Tasks;

using VerifyXunit;

using Xunit;

[UsesVerify]
public class InvalidExpressionForConstant
{
    [Fact]
    public Task VerifyInvalidConstantNameDiagnosticsMessage_Null() => AssertScalar(NullExpression).VerifyDiagnostics();

    [Theory]
    [MemberData(nameof(InvalidExpressions))]
    public void Scalar(SourceSubtext expression) => AssertScalar(expression);

    [Theory]
    [MemberData(nameof(InvalidExpressions))]
    public void Vector(SourceSubtext expression) => AssertVector(expression);

    public static IEnumerable<object[]> InvalidExpressions() => new object[][]
    {
        new object[] { NullExpression },
        new object[] { EmptyExpression }
    };

    private static SourceSubtext NullExpression { get; } = SourceSubtext.Covered("null");
    private static SourceSubtext EmptyExpression { get; } = SourceSubtext.Covered("\"\"");

    private static GeneratorVerifier AssertExactlyInvalidExpressionForDiagnostics(string source) => GeneratorVerifier.Construct<SharpMeasuresGenerator>(source).AssertExactlyListedDiagnosticsIDsReported(InvalidExpressionForConstantDiagnostics);
    private static IReadOnlyCollection<string> InvalidExpressionForConstantDiagnostics { get; } = new string[] { DiagnosticIDs.InvalidExpressionForConstant };

    private static string ScalarText(SourceSubtext expression) => $$"""
        using SharpMeasures.Generators;

        [ScalarConstant("PlanckLength", "Metre", {{expression}})]
        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }
            
        [FixedUnitInstance("Metre", "Metres")]
        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertScalar(SourceSubtext expression)
    {
        var source = ScalarText(expression);
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(source, expression.Context.With(outerPrefix: "\"Metre\", "));

        return AssertExactlyInvalidExpressionForDiagnostics(source).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(ScalarIdentical);
    }

    private static string VectorText(SourceSubtext expression) => $$"""
        using SharpMeasures.Generators;

        [VectorConstant("OneMetre", "Metre", new[] { "1", {{expression}} })]
        [VectorQuantity(typeof(UnitOfLength))]
        public partial class Position2 { }

        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }
            
        [FixedUnitInstance("Metre", "Metres")]
        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertVector(SourceSubtext expression)
    {
        var source = VectorText(expression);
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(source, expression.Context.With(outerPrefix: "\"1\", "));

        return AssertExactlyInvalidExpressionForDiagnostics(source).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(VectorIdentical);
    }

    private static GeneratorVerifier ScalarIdentical => GeneratorVerifier.Construct<SharpMeasuresGenerator>(ScalarIdenticalText);
    private static GeneratorVerifier VectorIdentical => GeneratorVerifier.Construct<SharpMeasuresGenerator>(VectorIdenticalText);

    private static string ScalarIdenticalText => """
        using SharpMeasures.Generators;

        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }
            
        [FixedUnitInstance("Metre", "Metres")]
        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static string VectorIdenticalText => """
        using SharpMeasures.Generators;

        [VectorQuantity(typeof(UnitOfLength))]
        public partial class Position2 { }

        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }
            
        [FixedUnitInstance("Metre", "Metres")]
        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;
}
