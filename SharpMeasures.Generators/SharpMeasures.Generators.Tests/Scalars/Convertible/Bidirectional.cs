namespace SharpMeasures.Generators.Tests.Scalars.Convertible;

using SharpMeasures.Generators.Tests.Verify;

using System.Threading.Tasks;

using VerifyXunit;

using Xunit;

[UsesVerify]
public class Bidirectional
{
    [Fact]
    public void UnbiasedScalar_Default() => GeneratorVerifier.Construct<SharpMeasuresGenerator>(UnbiasedScalarText).AssertNoDiagnosticsReported().AssertIdenticalSources<SharpMeasuresGenerator>(UnbiasedScalarText_SpecifiedBehaviour("Explicit"));

    [Fact]
    public Task UnbiasedScalar_Implicit() => GeneratorVerifier.Construct<SharpMeasuresGenerator>(UnbiasedScalarText_SpecifiedBehaviour("Implicit")).AssertNoDiagnosticsReported().VerifyMatchingSourceNames("Distance.Conversions.g.cs");

    [Fact]
    public Task UnbiasedScalar_Explicit() => GeneratorVerifier.Construct<SharpMeasuresGenerator>(UnbiasedScalarText_SpecifiedBehaviour("Explicit")).AssertNoDiagnosticsReported().VerifyMatchingSourceNames("Distance.Conversions.g.cs");

    [Fact]
    public void BiasedScalar_Default() => GeneratorVerifier.Construct<SharpMeasuresGenerator>(BiasedScalarText).AssertNoDiagnosticsReported().AssertIdenticalSources<SharpMeasuresGenerator>(BiasedScalarText_SpecifiedBehaviour("Explicit"));

    [Fact]
    public Task BiasedScalar_Implicit() => GeneratorVerifier.Construct<SharpMeasuresGenerator>(BiasedScalarText_SpecifiedBehaviour("Implicit")).AssertNoDiagnosticsReported().VerifyMatchingSourceNames("Temperature2.Conversions.g.cs");

    [Fact]
    public Task BiasedScalar_Explicit() => GeneratorVerifier.Construct<SharpMeasuresGenerator>(BiasedScalarText_SpecifiedBehaviour("Explicit")).AssertNoDiagnosticsReported().VerifyMatchingSourceNames("Temperature2.Conversions.g.cs");

    private static string UnbiasedScalarText => """
        using SharpMeasures.Generators;

        [ConvertibleQuantity(typeof(Length), ConversionDirection = QuantityConversionDirection.Bidirectional)]
        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Distance { }

        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }
        
        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static string UnbiasedScalarText_SpecifiedBehaviour(string behaviour) => $$"""
        using SharpMeasures.Generators;

        [ConvertibleQuantity(typeof(Length), ConversionDirection = QuantityConversionDirection.Bidirectional, CastOperatorBehaviour = ConversionOperatorBehaviour.{{behaviour}})]
        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Distance { }

        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }
        
        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static string BiasedScalarText => """
        using SharpMeasures.Generators;

        [ConvertibleQuantity(typeof(Temperature), ConversionDirection = QuantityConversionDirection.Bidirectional)]
        [ScalarQuantity(typeof(UnitOfTemperature), UseUnitBias = true)]
        public partial class Temperature2 { }

        [ScalarQuantity(typeof(UnitOfTemperature), UseUnitBias = true)]
        public partial class Temperature { }
        
        [ScalarQuantity(typeof(UnitOfTemperature))]
        public partial class TemperatureDifference { }
        
        [Unit(typeof(TemperatureDifference), BiasTerm = true)]
        public partial class UnitOfTemperature { }
        """;

    private static string BiasedScalarText_SpecifiedBehaviour(string behaviour) => $$"""
        using SharpMeasures.Generators;

        [ConvertibleQuantity(typeof(Temperature), ConversionDirection = QuantityConversionDirection.Bidirectional, CastOperatorBehaviour = ConversionOperatorBehaviour.{{behaviour}})]
        [ScalarQuantity(typeof(UnitOfTemperature), UseUnitBias = true)]
        public partial class Temperature2 { }

        [ScalarQuantity(typeof(UnitOfTemperature), UseUnitBias = true)]
        public partial class Temperature { }
        
        [ScalarQuantity(typeof(UnitOfTemperature))]
        public partial class TemperatureDifference { }
        
        [Unit(typeof(TemperatureDifference), BiasTerm = true)]
        public partial class UnitOfTemperature { }
        """;
}
