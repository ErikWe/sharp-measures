namespace SharpMeasures.Generators.Tests.Diagnostics;

using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Tests.Utility;
using SharpMeasures.Generators.Tests.Verify;

using System.Collections.Generic;
using System.Threading.Tasks;

using VerifyXunit;

using Xunit;

[UsesVerify]
public class UnitNotIncludingBiasTerm
{
    [Fact]
    public Task BiasedScalar() => AssertBiasedScalar().VerifyDiagnostics();

    private static GeneratorVerifier AssertExactlyUnitNotIncludingBiasTermDiagnostics(string source) => GeneratorVerifier.Construct<SharpMeasuresGenerator>(source).AssertExactlyListedDiagnosticsIDsReported(UnitNotIncludingBiasTermDiagnostics);
    private static IReadOnlyCollection<string> UnitNotIncludingBiasTermDiagnostics { get; } = new string[] { DiagnosticIDs.UnitNotIncludingBiasTerm };

    private static string BiasedScalarText => """
        using SharpMeasures.Generators.Quantities;
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;

        [SharpMeasuresScalar(typeof(UnitOfLength), UseUnitBias = true)]
        public partial class Temperature { }
            
        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }

        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertBiasedScalar()
    {
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(BiasedScalarText, target: "true", prefix: "UseUnitBias = ");

        return AssertExactlyUnitNotIncludingBiasTermDiagnostics(BiasedScalarText).AssertDiagnosticsLocation(expectedLocation, BiasedScalarText);
    }
}
