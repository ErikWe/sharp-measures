namespace SharpMeasures.Generators.Tests.Diagnostics;

using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Tests.Verify;

using System.Collections.Generic;

using VerifyXunit;

using Xunit;

[UsesVerify]
public class TypeNotBiasedScalar
{
    [Fact]
    public void ConvertibleScalar_ExactList()
    {
        string source = """
            using SharpMeasures.Generators.Quantities;
            using SharpMeasures.Generators.Scalars;
            using SharpMeasures.Generators.Units;

            [ConvertibleQuantity(typeof(TemperatureDifference))]
            [SharpMeasuresScalar(typeof(UnitOfTemperature), UseUnitBias = true)]
            public partial class Temperature { }
            
            [SharpMeasuresScalar(typeof(UnitOfTemperature))]
            public partial class TemperatureDifference { }

            [SharpMeasuresUnit(typeof(TemperatureDifference), BiasTerm = true)]
            public partial class UnitOfTemperature { }
            """;

        AssertExactlyTypeNotBiasedScalarDiagnostics(source);
    }

    private static GeneratorVerifier AssertExactlyTypeNotBiasedScalarDiagnostics(string source) => GeneratorVerifier.Construct<SharpMeasuresGenerator>(source).AssertExactlyListedDiagnosticsIDsReported(TypeNotBiasedDiagnostics);
    private static IReadOnlyCollection<string> TypeNotBiasedDiagnostics { get; } = new string[] { DiagnosticIDs.TypeNotBiasedScalar };
}
