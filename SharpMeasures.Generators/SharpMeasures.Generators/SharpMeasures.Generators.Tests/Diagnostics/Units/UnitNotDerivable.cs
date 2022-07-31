namespace SharpMeasures.Generators.Tests.Diagnostics.Units;

using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Tests.Verify;

using System.Collections.Generic;
using System.Threading.Tasks;

using VerifyXunit;

using Xunit;

[UsesVerify]
public class UnitNotDerivable
{
    [Fact]
    public Task WithoutID_ExactListAndVerify()
    {
        string source = """
            using SharpMeasures.Generators.Scalars;
            using SharpMeasures.Generators.Units;

            [SharpMeasuresScalar(typeof(UnitOfLength))]
            public partial class Length { }

            [DerivedUnit("Metre", "Metres", new[] { "MetrePerSecond", "Second" })]
            [SharpMeasuresUnit(typeof(Length))]
            public partial class UnitOfLength { }
            """;

        return AssertExactlyUnitNotDerivableDiagnostics(source).VerifyDiagnostics();
    }

    [Fact]
    public void WithID_ExactList()
    {
        string source = """
            using SharpMeasures.Generators.Scalars;
            using SharpMeasures.Generators.Units;

            [SharpMeasuresScalar(typeof(UnitOfLength))]
            public partial class Length { }

            [DerivedUnit("Metre", "Metres", "1", new[] { "MetrePerSecond", "Second" })]
            [SharpMeasuresUnit(typeof(Length))]
            public partial class UnitOfLength { }
            """;

        AssertExactlyUnitNotDerivableDiagnostics(source);
    }

    private static GeneratorVerifier AssertExactlyUnitNotDerivableDiagnostics(string source) => GeneratorVerifier.Construct<SharpMeasuresGenerator>(source).AssertExactlyListedDiagnosticsIDsReported(UnitNotDerivableDiagnostics);
    private static IReadOnlyCollection<string> UnitNotDerivableDiagnostics { get; } = new string[] { DiagnosticIDs.UnitNotDerivable };
}
