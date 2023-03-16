namespace SharpMeasures.Generators.Tests.Vectors.Convertible;

using SharpMeasures.Generators.Tests.Verify;

using System.Threading.Tasks;

using VerifyXunit;

using Xunit;

[UsesVerify]
public class Bidirectional
{
    [Fact]
    public void Defaults() => GeneratorVerifier.Construct<SharpMeasuresGenerator>(DefaultsText).AssertNoDiagnosticsReported().AssertIdenticalSources<SharpMeasuresGenerator>(SpecifiedBehaviourText("Explicit"));

    [Fact]
    public Task Implicit() => GeneratorVerifier.Construct<SharpMeasuresGenerator>(SpecifiedBehaviourText("Implicit")).AssertNoDiagnosticsReported().VerifyMatchingSourceNames("Displacement3.Conversions.g.cs");

    [Fact]
    public Task Explicit() => GeneratorVerifier.Construct<SharpMeasuresGenerator>(SpecifiedBehaviourText("Explicit")).AssertNoDiagnosticsReported().VerifyMatchingSourceNames("Displacement3.Conversions.g.cs");

    private static string DefaultsText => """
        using SharpMeasures.Generators;

        [ConvertibleQuantity(typeof(Position3), ConversionDirection = QuantityConversionDirection.Bidirectional)]
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

        [ConvertibleQuantity(typeof(Position3), ConversionDirection = QuantityConversionDirection.Bidirectional, CastOperatorBehaviour = ConversionOperatorBehaviour.{{behaviour}})]
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
