namespace SharpMeasures.Generators.Tests.Diagnostics;

using Microsoft.CodeAnalysis.Text;

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
    public Task VerifyInvalidDerivationExpressionDiagnosticsMessage_Null() => AssertAndVerifyDerivableUnit(NullSubtext);

    [Theory]
    [MemberData(nameof(InvalidExpressions))]
    public void DerivableUnit(SourceSubtext expression) => AssertDerivableUnit(expression);

    [Theory]
    [MemberData(nameof(InvalidExpressions))]
    public void DerivedQuantity(SourceSubtext expression) => AssertDerivedQuantity(expression);

    private static IEnumerable<object[]> InvalidExpressions() => new object[][]
    {
        new object[] { NullSubtext },
        new object[] { EmptySubtext }
    };

    private static SourceSubtext NullSubtext { get; } = new("null");
    private static SourceSubtext EmptySubtext { get; } = new("\"\"");

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

    private static TextSpan DerivableUnitLocation(SourceSubtext expression) => ExpectedDiagnosticsLocation.TextSpan(DerivableUnitText(expression), expression, prefix: "DerivableUnit(");

    private static GeneratorVerifier AssertDerivableUnit(SourceSubtext expression)
    {
        var source = DerivableUnitText(expression);
        var expectedLocation = DerivableUnitLocation(expression);

        return AssertExactlyInvalidDerivationExpressionDiagnostics(source).AssertDiagnosticsLocation(expectedLocation, source);
    }

    private static Task AssertAndVerifyDerivableUnit(SourceSubtext expression) => AssertDerivableUnit(expression).VerifyDiagnostics();

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

    private static TextSpan DerivedQuantityLocation(SourceSubtext expression) => ExpectedDiagnosticsLocation.TextSpan(DerivedQuantityText(expression), expression, prefix: "DerivedQuantity(");

    private static GeneratorVerifier AssertDerivedQuantity(SourceSubtext expression)
    {
        var source = DerivedQuantityText(expression);
        var expectedLocation = DerivedQuantityLocation(expression);

        return AssertExactlyInvalidDerivationExpressionDiagnostics(source).AssertDiagnosticsLocation(expectedLocation, source);
    }
}
