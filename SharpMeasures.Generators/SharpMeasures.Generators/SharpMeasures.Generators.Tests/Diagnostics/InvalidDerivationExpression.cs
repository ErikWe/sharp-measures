namespace SharpMeasures.Generators.Tests.Diagnostics;

using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Tests.Verify;

using System.Collections.Generic;
using System.Threading.Tasks;

using VerifyXunit;

using Xunit;

[UsesVerify]
public class InvalidDerivationExpression
{
    [Fact]
    public Task VerifyInvalidDerivationExpressionDiagnosticsMessage_Null()
    {
        string source = DerivableUnitText("null");

        return AssertExactlyInvalidDerivationExpressionDiagnostics(source).VerifyDiagnostics();
    }

    [Theory]
    [MemberData(nameof(InvalidExpressions))]
    public void DerivableUnit_ExactList(string expression)
    {
        string source = DerivableUnitText(expression);

        AssertExactlyInvalidDerivationExpressionDiagnostics(source);
    }

    [Theory]
    [MemberData(nameof(InvalidExpressions))]
    public void DerivedQuantity_ExactList(string expression)
    {
        string source = DerivedQuantityText(expression);

        AssertExactlyInvalidDerivationExpressionDiagnostics(source);
    }

    private static IEnumerable<object[]> InvalidExpressions() => new object[][]
    {
        new[] { "null" },
        new[] { "\"\"" }
    };

    private static GeneratorVerifier AssertExactlyInvalidDerivationExpressionDiagnostics(string source) => GeneratorVerifier.Construct<SharpMeasuresGenerator>(source).AssertExactlyListedDiagnosticsIDsReported(InvalidDerivationExpressionDiagnostics);
    private static IReadOnlyCollection<string> InvalidDerivationExpressionDiagnostics { get; } = new string[] { DiagnosticIDs.InvalidDerivationExpression };

    private static string DerivableUnitText(string value) => $$"""
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;

        [DerivableUnit({{value}}, new[] { typeof(UnitOfLength), typeof(UnitOfSpeed) })]
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

    private static string DerivedQuantityText(string value) => $$"""
        using SharpMeasures.Generators.Quantities;
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;

        [DerivedQuantity({{value}}, new[] { typeof(Length), typeof(Time) })]
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
}
