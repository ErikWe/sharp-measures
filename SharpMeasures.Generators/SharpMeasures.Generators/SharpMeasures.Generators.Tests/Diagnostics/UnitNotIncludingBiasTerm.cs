namespace SharpMeasures.Generators.Tests.Diagnostics;

using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Tests.Verify;

using System.Collections.Generic;
using System.Threading.Tasks;

using VerifyXunit;

using Xunit;

[UsesVerify]
public class UnitNotIncludingBiasTerm
{
    [Fact]
    public Task BiasedScalar_ExactListAndVerify()
    {
        string source = $$"""
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

        return AssertExactlyUnitNotIncludingBiasTermDiagnosticsWithValidLocation(source).VerifyDiagnostics();
    }

    private static GeneratorVerifier AssertExactlyUnitNotIncludingBiasTermDiagnosticsWithValidLocation(string source) => GeneratorVerifier.Construct<SharpMeasuresGenerator>(source).AssertExactlyListedDiagnosticsIDsReported(UnitNotIncludingBiasTermDiagnostics).AssertAllDiagnosticsValidLocation();
    private static IReadOnlyCollection<string> UnitNotIncludingBiasTermDiagnostics { get; } = new string[] { DiagnosticIDs.UnitNotIncludingBiasTerm };
}
