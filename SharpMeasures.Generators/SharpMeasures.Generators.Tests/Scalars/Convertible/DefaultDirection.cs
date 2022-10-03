namespace SharpMeasures.Generators.Tests.Scalars.Convertible;

using SharpMeasures.Generators.Tests.Verify;

using VerifyXunit;

using Xunit;

[UsesVerify]
public class DefaultDirection
{
    [Fact]
    public void UnbiasedScalar_Default() => GeneratorVerifier.Construct<SharpMeasuresGenerator>(UnbiasedScalarText).AssertNoDiagnosticsReported().AssertIdenticalSources<SharpMeasuresGenerator>(UnbiasedScalarIdenticalText("Explicit"));

    [Fact]
    public void UnbiasedScalar_Implicit() => GeneratorVerifier.Construct<SharpMeasuresGenerator>(UnbiasedScalarText_SpecifiedBehaviour("Implicit")).AssertNoDiagnosticsReported().AssertIdenticalSources<SharpMeasuresGenerator>(UnbiasedScalarIdenticalText("Implicit"));

    [Fact]
    public void UnbiasedScalar_Explicit() => GeneratorVerifier.Construct<SharpMeasuresGenerator>(UnbiasedScalarText_SpecifiedBehaviour("Explicit")).AssertNoDiagnosticsReported().AssertIdenticalSources<SharpMeasuresGenerator>(UnbiasedScalarIdenticalText("Explicit"));

    [Fact]
    public void BiasedScalar_Default() => GeneratorVerifier.Construct<SharpMeasuresGenerator>(BiasedScalarText).AssertNoDiagnosticsReported().AssertIdenticalSources<SharpMeasuresGenerator>(BiasedScalarIdenticalText("Explicit"));

    [Fact]
    public void BiasedScalar_Implicit() => GeneratorVerifier.Construct<SharpMeasuresGenerator>(BiasedScalarText_SpecifiedBehaviour("Implicit")).AssertNoDiagnosticsReported().AssertIdenticalSources<SharpMeasuresGenerator>(BiasedScalarIdenticalText("Implicit"));

    [Fact]
    public void BiasedScalar_Explicit() => GeneratorVerifier.Construct<SharpMeasuresGenerator>(BiasedScalarText_SpecifiedBehaviour("Explicit")).AssertNoDiagnosticsReported().AssertIdenticalSources<SharpMeasuresGenerator>(BiasedScalarIdenticalText("Explicit"));

    private static string UnbiasedScalarText => """
        using SharpMeasures.Generators;

        [ConvertibleQuantity(typeof(Length))]
        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Distance { }

        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }
        
        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static string UnbiasedScalarText_SpecifiedBehaviour(string behaviour) => $$"""
        using SharpMeasures.Generators;

        [ConvertibleQuantity(typeof(Length), CastOperatorBehaviour = ConversionOperatorBehaviour.{{behaviour}})]
        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Distance { }

        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }
        
        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static string BiasedScalarText => """
        using SharpMeasures.Generators;

        [ConvertibleQuantity(typeof(Temperature))]
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

        [ConvertibleQuantity(typeof(Temperature), CastOperatorBehaviour = ConversionOperatorBehaviour.{{behaviour}})]
        [ScalarQuantity(typeof(UnitOfTemperature), UseUnitBias = true)]
        public partial class Temperature2 { }

        [ScalarQuantity(typeof(UnitOfTemperature), UseUnitBias = true)]
        public partial class Temperature { }
        
        [ScalarQuantity(typeof(UnitOfTemperature))]
        public partial class TemperatureDifference { }
        
        [Unit(typeof(TemperatureDifference), BiasTerm = true)]
        public partial class UnitOfTemperature { }
        """;

    private static string UnbiasedScalarIdenticalText(string behaviour) => $$"""
        using SharpMeasures.Generators;

        [ConvertibleQuantity(typeof(Length), ConversionDirection = QuantityConversionDirection.Onedirectional, CastOperatorBehaviour = ConversionOperatorBehaviour.{{behaviour}})]
        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Distance { }

        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }
        
        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static string BiasedScalarIdenticalText(string behaviour) => $$"""
        using SharpMeasures.Generators;

        [ConvertibleQuantity(typeof(Temperature), ConversionDirection = QuantityConversionDirection.Onedirectional, CastOperatorBehaviour = ConversionOperatorBehaviour.{{behaviour}})]
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
