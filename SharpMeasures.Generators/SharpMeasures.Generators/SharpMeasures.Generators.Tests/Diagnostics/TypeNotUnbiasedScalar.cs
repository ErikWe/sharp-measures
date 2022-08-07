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
public class TypeNotUnbiasedScalar
{
    [Fact]
    public Task UnbiasedUnitQuantity() => AssertAndVerifyUnbiasedUnitQuantity();

    [Fact]
    public void BiasedUnitQuantity() => AssertBiasedUnitQuantity();

    [Fact]
    public void ConvertibleScalar() => AssertConvertibleScalar();

    private static GeneratorVerifier AssertExactlyTypeNotUnbiasedScalarDiagnostics(string source) => GeneratorVerifier.Construct<SharpMeasuresGenerator>(source).AssertExactlyListedDiagnosticsIDsReported(TypeNotUnbiasedDiagnostics);
    private static IReadOnlyCollection<string> TypeNotUnbiasedDiagnostics { get; } = new string[] { DiagnosticIDs.TypeNotUnbiasedScalar };

    private static string UnbiasedUnitQuantityText => """
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

    private static TextSpan UnbiasedUnitQuantityLocation => ExpectedDiagnosticsLocation.FromTypeofArgumentTextSpan(UnbiasedUnitQuantityText, target: "typeof(Temperature)", prefix: "SharpMeasuresUnit(");

    private static GeneratorVerifier AssertUnbiasedUnitQuantity() => AssertExactlyTypeNotUnbiasedScalarDiagnostics(UnbiasedUnitQuantityText).AssertDiagnosticsLocation(UnbiasedUnitQuantityLocation, UnbiasedUnitQuantityText);
    private static Task AssertAndVerifyUnbiasedUnitQuantity() => AssertUnbiasedUnitQuantity().VerifyDiagnostics();

    private static string BiasedUnitQuantityText => """
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;

        [SharpMeasuresScalar(typeof(UnitOfTemperature), UseUnitBias = true)]
        public partial class Temperature { }

        [SharpMeasuresUnit(typeof(Temperature), BiasTerm = true)]
        public partial class UnitOfTemperature { }
        """;

    private static TextSpan BiasedUnitQuantityLocation => ExpectedDiagnosticsLocation.FromTypeofArgumentTextSpan(BiasedUnitQuantityText, target: "typeof(Temperature)", prefix: "SharpMeasuresUnit(");

    private static GeneratorVerifier AssertBiasedUnitQuantity() => AssertExactlyTypeNotUnbiasedScalarDiagnostics(BiasedUnitQuantityText).AssertDiagnosticsLocation(BiasedUnitQuantityLocation, BiasedUnitQuantityText);

    private static string ConvertibleScalarText => """
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

    private static TextSpan ConvertibleScalarLocation => ExpectedDiagnosticsLocation.FromTypeofArgumentTextSpan(ConvertibleScalarText, target: "typeof(Temperature)", prefix: "ConvertibleQuantity(");

    private static GeneratorVerifier AssertConvertibleScalar() => AssertExactlyTypeNotUnbiasedScalarDiagnostics(ConvertibleScalarText).AssertDiagnosticsLocation(ConvertibleScalarLocation, ConvertibleScalarText);
}
