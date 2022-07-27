namespace SharpMeasures.Generators.Tests.Diagnostics.Units;

using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Tests.Verify;

using System.Collections.Generic;
using System.Threading.Tasks;

using VerifyXunit;

using Xunit;

[UsesVerify]
public class InvalidUnitDerivationExpression
{
    private static string Text(string expression) => $$"""
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }

        [SharpMeasuresScalar(typeof(UnitOfTime))]
        public partial class Time { }

        [SharpMeasuresScalar(typeof(UnitOfSpeed))]
        public partial class Speed { }

        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }

        [SharpMeasuresUnit(typeof(Time))]
        public partial class UnitOfTime { }

        [DerivableUnit("id", {{expression}}, typeof(UnitOfLength), typeof(UnitOfTime))]
        [SharpMeasuresUnit(typeof(Speed))]
        public partial class UnitOfSpeed { }
        """;

    [Fact]
    public Task Null_ExactListAndVerify()
    {
        string source = Text("null");

        return AssertExactlyInvalidUnitDerivationExpressionDiagnostics(source).VerifyDiagnostics();
    }

    [Fact]
    public Task Empty_ExactListAndVerify()
    {
        string source = Text("\"\"");

        return AssertExactlyInvalidUnitDerivationExpressionDiagnostics(source).VerifyDiagnostics();
    }

    private static GeneratorVerifier AssertExactlyInvalidUnitDerivationExpressionDiagnostics(string source)
        => GeneratorVerifier.Construct<SharpMeasuresGenerator>(source).AssertExactlyListedDiagnosticsIDsReported(InvalidUnitderivationExpressionDiagnostics);

    private static IReadOnlyCollection<string> InvalidUnitderivationExpressionDiagnostics { get; } = new string[] { DiagnosticIDs.InvalidDerivationExpression };
}
