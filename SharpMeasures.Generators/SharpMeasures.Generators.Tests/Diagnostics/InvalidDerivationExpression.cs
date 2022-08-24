namespace SharpMeasures.Generators.Tests.Diagnostics;

using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Tests.Utility;
using SharpMeasures.Generators.Tests.Verify;

using System.Collections.Generic;
using System.Threading.Tasks;

using VerifyXunit;

using Xunit;

[UsesVerify]
public class InvalidDerivationExpression
{
    [Fact]
    public Task VerifyInvalidDerivationExpressionDiagnosticsMessage_Null() => AssertDerivableUnit(NullExpression).VerifyDiagnostics();

    [Theory]
    [MemberData(nameof(InvalidExpressions))]
    public void DerivableUnit(SourceSubtext expression) => AssertDerivableUnit(expression);

    [Theory]
    [MemberData(nameof(InvalidExpressions))]
    public void DerivedQuantity(SourceSubtext expression) => AssertDerivedQuantity(expression);

    public static IEnumerable<object[]> InvalidExpressions() => new object[][]
    {
        new object[] { NullExpression },
        new object[] { EmptyExpression }
    };

    private static SourceSubtext NullExpression { get; } = SourceSubtext.Covered("null");
    private static SourceSubtext EmptyExpression { get; } = SourceSubtext.Covered("\"\"");

    private static GeneratorVerifier AssertExactlyInvalidDerivationExpressionDiagnostics(string source) => GeneratorVerifier.Construct<SharpMeasuresGenerator>(source).AssertExactlyListedDiagnosticsIDsReported(InvalidDerivationExpressionDiagnostics);
    private static IReadOnlyCollection<string> InvalidDerivationExpressionDiagnostics { get; } = new string[] { DiagnosticIDs.InvalidDerivationExpression };

    private static string DerivableUnitText(SourceSubtext expression) => $$"""
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;

        [DerivableUnit({{expression}}, new[] { typeof(UnitOfLength), typeof(UnitOfSpeed) })]
        [SharpMeasuresUnit(typeof(Speed))]
        public partial class UnitOfSpeed { }

        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }

        [SharpMeasuresUnit(typeof(Time))]
        public partial class UnitOfTime { }

        [SharpMeasuresScalar(typeof(UnitOfSpeed))]
        public partial class Speed { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }

        [SharpMeasuresScalar(typeof(UnitOfTime))]
        public partial class Time { }
        """;

    private static GeneratorVerifier AssertDerivableUnit(SourceSubtext expression)
    {
        var source = DerivableUnitText(expression);
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(source, expression.Context.With(outerPrefix: "DerivableUnit("));

        return AssertExactlyInvalidDerivationExpressionDiagnostics(source).AssertDiagnosticsLocation(expectedLocation, source);
    }

    private static string DerivedQuantityText(SourceSubtext expression) => $$"""
        using SharpMeasures.Generators.Quantities;
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;

        [DerivedQuantity({{expression}}, new[] { typeof(Length), typeof(Time) })]
        [SharpMeasuresScalar(typeof(UnitOfSpeed))]
        public partial class Speed { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }

        [SharpMeasuresScalar(typeof(UnitOfTime))]
        public partial class Time { }

        [SharpMeasuresUnit(typeof(Speed))]
        public partial class UnitOfSpeed { }

        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }

        [SharpMeasuresUnit(typeof(Time))]
        public partial class UnitOfTime { }
        """;

    private static GeneratorVerifier AssertDerivedQuantity(SourceSubtext expression)
    {
        var source = DerivedQuantityText(expression);
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(source, expression.Context.With(outerPrefix: "DerivedQuantity("));

        return AssertExactlyInvalidDerivationExpressionDiagnostics(source).AssertDiagnosticsLocation(expectedLocation, source);
    }
}
