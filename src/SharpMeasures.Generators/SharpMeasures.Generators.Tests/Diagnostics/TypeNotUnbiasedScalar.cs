namespace SharpMeasures.Generators.Tests.Diagnostics;

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
    public Task UnbiasedUnitQuantity() => AssertUnbiasedUnitQuantity().VerifyDiagnostics();

    [Fact]
    public void BiasedUnitQuantity() => AssertBiasedUnitQuantity();

    [Fact]
    public void ConvertibleScalar() => AssertConvertibleScalar();

    private static GeneratorVerifier AssertExactlyTypeNotUnbiasedScalarDiagnostics(string source) => GeneratorVerifier.Construct<SharpMeasuresGenerator>(source, GeneratorVerifierSettings.TestCodeAssertions).AssertExactlyListedDiagnosticsIDsReported(TypeNotUnbiasedDiagnostics);
    private static IReadOnlyCollection<string> TypeNotUnbiasedDiagnostics { get; } = new string[] { DiagnosticIDs.TypeNotUnbiasedScalar };

    private static string UnbiasedUnitQuantityText => """
        using SharpMeasures.Generators;

        [Unit(typeof(Temperature))]
        public partial class UnitOfTemperature2 { }

        [ScalarQuantity(typeof(UnitOfTemperature), UseUnitBias = true)]
        public partial class Temperature { }

        [ScalarQuantity(typeof(UnitOfTemperature))]
        public partial class TemperatureDifference { }

        [Unit(typeof(TemperatureDifference), BiasTerm = true)]
        public partial class UnitOfTemperature { }
        """;

    private static GeneratorVerifier AssertUnbiasedUnitQuantity()
    {
        var expectedLocation = ExpectedDiagnosticsLocation.FromTypeofArgumentTextSpan(UnbiasedUnitQuantityText, target: "typeof(Temperature)", prefix: "Unit(");

        return AssertExactlyTypeNotUnbiasedScalarDiagnostics(UnbiasedUnitQuantityText).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(UnbiasedUnitQuantityIdentical);
    }

    private static string BiasedUnitQuantityText => """
        using SharpMeasures.Generators;

        [Unit(typeof(Temperature), BiasTerm = true)]
        public partial class UnitOfTemperature2 { }

        [ScalarQuantity(typeof(UnitOfTemperature), UseUnitBias = true)]
        public partial class Temperature { }
        
        [ScalarQuantity(typeof(UnitOfTemperature))]
        public partial class TemperatureDifference { }

        [Unit(typeof(TemperatureDifference), BiasTerm = true)]
        public partial class UnitOfTemperature { }
        """;

    private static GeneratorVerifier AssertBiasedUnitQuantity()
    {
        var expectedLocation = ExpectedDiagnosticsLocation.FromTypeofArgumentTextSpan(BiasedUnitQuantityText, target: "typeof(Temperature)", prefix: "Unit(");

        return AssertExactlyTypeNotUnbiasedScalarDiagnostics(BiasedUnitQuantityText).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(BiasedUnitQuantityIdentical);
    }

    private static string ConvertibleScalarText => """
        using SharpMeasures.Generators;

        [ConvertibleQuantity(typeof(Temperature))]
        [ScalarQuantity(typeof(UnitOfTemperature))]
        public partial class TemperatureDifference { }

        [ScalarQuantity(typeof(UnitOfTemperature), UseUnitBias = true)]
        public partial class Temperature { }

        [Unit(typeof(TemperatureDifference), BiasTerm = true)]
        public partial class UnitOfTemperature { }
        """;

    private static GeneratorVerifier AssertConvertibleScalar()
    {
        var expectedLocation = ExpectedDiagnosticsLocation.FromTypeofArgumentTextSpan(ConvertibleScalarText, target: "typeof(Temperature)", prefix: "ConvertibleQuantity(");

        return AssertExactlyTypeNotUnbiasedScalarDiagnostics(ConvertibleScalarText).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(ConvertibleScalarIdentical);
    }

    private static GeneratorVerifier UnbiasedUnitQuantityIdentical => GeneratorVerifier.Construct<SharpMeasuresGenerator>(UnbiasedUnitQuantityIdenticalText);
    private static GeneratorVerifier BiasedUnitQuantityIdentical => GeneratorVerifier.Construct<SharpMeasuresGenerator>(BiasedUnitQuantityIdenticalText);
    private static GeneratorVerifier ConvertibleScalarIdentical => GeneratorVerifier.Construct<SharpMeasuresGenerator>(ConvertibleScalarIdenticalText);

    private static string UnbiasedUnitQuantityIdenticalText => """
        using SharpMeasures.Generators;

        [ScalarQuantity(typeof(UnitOfTemperature), UseUnitBias = true)]
        public partial class Temperature { }

        [ScalarQuantity(typeof(UnitOfTemperature))]
        public partial class TemperatureDifference { }

        [Unit(typeof(TemperatureDifference), BiasTerm = true)]
        public partial class UnitOfTemperature { }
        """;

    private static string BiasedUnitQuantityIdenticalText => """
        using SharpMeasures.Generators;
        
        [ScalarQuantity(typeof(UnitOfTemperature), UseUnitBias = true)]
        public partial class Temperature { }
        
        [ScalarQuantity(typeof(UnitOfTemperature))]
        public partial class TemperatureDifference { }
        
        [Unit(typeof(TemperatureDifference), BiasTerm = true)]
        public partial class UnitOfTemperature { }
        """;

    private static string ConvertibleScalarIdenticalText => """
        using SharpMeasures.Generators;

        [ScalarQuantity(typeof(UnitOfTemperature))]
        public partial class TemperatureDifference { }

        [ScalarQuantity(typeof(UnitOfTemperature), UseUnitBias = true)]
        public partial class Temperature { }

        [Unit(typeof(TemperatureDifference), BiasTerm = true)]
        public partial class UnitOfTemperature { }
        """;
}
