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
        using SharpMeasures.Generators;

        [DerivableUnit({{expression}}, new[] { typeof(UnitOfLength), typeof(UnitOfSpeed) })]
        [Unit(typeof(Speed))]
        public partial class UnitOfSpeed { }

        [Unit(typeof(Length))]
        public partial class UnitOfLength { }

        [Unit(typeof(Time))]
        public partial class UnitOfTime { }

        [ScalarQuantity(typeof(UnitOfSpeed))]
        public partial class Speed { }

        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }

        [ScalarQuantity(typeof(UnitOfTime))]
        public partial class Time { }
        """;

    private static GeneratorVerifier AssertDerivableUnit(SourceSubtext expression)
    {
        var source = DerivableUnitText(expression);
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(source, expression.Context.With(outerPrefix: "DerivableUnit("));

        return AssertExactlyInvalidDerivationExpressionDiagnostics(source).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(DerivableUnitIdentical);
    }

    private static GeneratorVerifier DerivableUnitIdentical => GeneratorVerifier.Construct<SharpMeasuresGenerator>(DerivableUnitIdenticalText);

    private static string DerivableUnitIdenticalText => """
        using SharpMeasures.Generators;

        [Unit(typeof(Speed))]
        public partial class UnitOfSpeed { }

        [Unit(typeof(Length))]
        public partial class UnitOfLength { }

        [Unit(typeof(Time))]
        public partial class UnitOfTime { }

        [ScalarQuantity(typeof(UnitOfSpeed))]
        public partial class Speed { }

        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }

        [ScalarQuantity(typeof(UnitOfTime))]
        public partial class Time { }
        """;
}
