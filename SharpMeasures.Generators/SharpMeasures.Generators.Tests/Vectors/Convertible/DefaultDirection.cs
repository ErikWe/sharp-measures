namespace SharpMeasures.Generators.Tests.Vectors.Convertible;

using SharpMeasures.Generators.Tests.Verify;

using VerifyXunit;

using Xunit;

[UsesVerify]
public class DefaultDirection
{
    [Fact]
    public void Defaults() => GeneratorVerifier.Construct<SharpMeasuresGenerator>(DefaultsText).AssertNoDiagnosticsReported().AssertIdenticalSources<SharpMeasuresGenerator>(IdenticalText("Explicit"));

    [Fact]
    public void Implicit() => GeneratorVerifier.Construct<SharpMeasuresGenerator>(SpecifiedBehaviourText("Implicit")).AssertNoDiagnosticsReported().AssertIdenticalSources<SharpMeasuresGenerator>(IdenticalText("Implicit"));

    [Fact]
    public void Explicit() => GeneratorVerifier.Construct<SharpMeasuresGenerator>(SpecifiedBehaviourText("Explicit")).AssertNoDiagnosticsReported().AssertIdenticalSources<SharpMeasuresGenerator>(IdenticalText("Explicit"));

    private static string DefaultsText => """
        using SharpMeasures.Generators;

        [ConvertibleQuantity(typeof(Position3))]
        [VectorQuantity(typeof(UnitOfLength))]
        public partial class Displacement3 { }
        
        [VectorQuantity(typeof(UnitOfLength))]
        public partial class Position3 { }

        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }
        
        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static string SpecifiedBehaviourText(string behaviour) => $$"""
        using SharpMeasures.Generators;

        [ConvertibleQuantity(typeof(Position3), CastOperatorBehaviour = ConversionOperatorBehaviour.{{behaviour}})]
        [VectorQuantity(typeof(UnitOfLength))]
        public partial class Displacement3 { }
        
        [VectorQuantity(typeof(UnitOfLength))]
        public partial class Position3 { }

        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }
        
        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static string IdenticalText(string behaviour) => $$"""
        using SharpMeasures.Generators;

        [ConvertibleQuantity(typeof(Position3), ConversionDirection = QuantityConversionDirection.Onedirectional, CastOperatorBehaviour = ConversionOperatorBehaviour.{{behaviour}})]
        [VectorQuantity(typeof(UnitOfLength))]
        public partial class Displacement3 { }
        
        [VectorQuantity(typeof(UnitOfLength))]
        public partial class Position3 { }

        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }
        
        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;
}
