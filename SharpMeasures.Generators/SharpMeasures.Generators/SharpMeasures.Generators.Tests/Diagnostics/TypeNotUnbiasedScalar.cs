namespace SharpMeasures.Generators.Tests.Diagnostics;

using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Tests.Verify;

using System.Collections.Generic;
using System.Threading.Tasks;

using VerifyXunit;

using Xunit;

[UsesVerify]
public class TypeNotUnbiasedScalar
{
    [Fact]
    public Task UnitQuantity_UnbiasedUnit_ExactListAndVerify()
    {
        string source = """
            using SharpMeasures.Generators.Scalars;
            using SharpMeasures.Generators.Units;

            [SharpMeasuresUnit(typeof(Temperature))]
            public partial class UnitOfTemperature2 { }

            [SharpMeasuresScalar(typeof(UnitOfTemperature), UseUnitBias = true)]
            public partial class Temperature { }

            [SharpMeasuresScalar(typeof(UnitOfTemperature))]
            public partial class TemperatureDifference { }

            [SharpMeasuresUnit(typeof(TemperatureDifference), BiasTerm = true)]
            public partial class UnitOfTemperature { }
            """;

        return AssertExactlyTypeNotUnbiasedScalarDiagnostics(source).VerifyDiagnostics();
    }

    [Fact]
    public void UnitQuantity_BiasedUnit_ExactList()
    {
        string source = """
            using SharpMeasures.Generators.Scalars;
            using SharpMeasures.Generators.Units;

            [SharpMeasuresScalar(typeof(UnitOfTemperature), UseUnitBias = true)]
            public partial class Temperature { }

            [SharpMeasuresUnit(typeof(Temperature), BiasTerm = true)]
            public partial class UnitOfTemperature { }
            """;

        AssertExactlyTypeNotUnbiasedScalarDiagnostics(source);
    }

    [Fact]
    public void ConvertibleScalar_ExactList()
    {
        string source = """
            using SharpMeasures.Generators.Quantities;
            using SharpMeasures.Generators.Scalars;
            using SharpMeasures.Generators.Units;

            [ConvertibleQuantity(typeof(Temperature))]
            [SharpMeasuresScalar(typeof(UnitOfTemperature))]
            public partial class TemperatureDifference { }

            [SharpMeasuresScalar(typeof(UnitOfTemperature), UseUnitBias = true)]
            public partial class Temperature { }

            [SharpMeasuresUnit(typeof(TemperatureDifference), BiasTerm = true)]
            public partial class UnitOfTemperature { }
            """;

        AssertExactlyTypeNotUnbiasedScalarDiagnostics(source);
    }

    private static GeneratorVerifier AssertExactlyTypeNotUnbiasedScalarDiagnostics(string source) => GeneratorVerifier.Construct<SharpMeasuresGenerator>(source).AssertExactlyListedDiagnosticsIDsReported(TypeNotUnbiasedDiagnostics);
    private static IReadOnlyCollection<string> TypeNotUnbiasedDiagnostics { get; } = new string[] { DiagnosticIDs.TypeNotUnbiasedScalar };
}
