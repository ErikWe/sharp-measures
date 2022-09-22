namespace SharpMeasures.Generators.Tests.Vectors.Convertible;

using SharpMeasures.Generators.Tests.Verify;

using System.Threading.Tasks;

using VerifyXunit;

using Xunit;

[UsesVerify]
public class Onedirectional
{
    [Fact]
    public void Defaults() => GeneratorVerifier.Construct<SharpMeasuresGenerator>(DefaultsText).AssertNoDiagnosticsReported().AssertIdenticalSources<SharpMeasuresGenerator>(SpecifiedBehaviourText("Explicit"));

    [Fact]
    public Task Implicit() => GeneratorVerifier.Construct<SharpMeasuresGenerator>(SpecifiedBehaviourText("Implicit")).AssertNoDiagnosticsReported().VerifyMatchingSourceNames("Displacement3.Conversions.g.cs");

    [Fact]
    public Task Explicit() => GeneratorVerifier.Construct<SharpMeasuresGenerator>(SpecifiedBehaviourText("Explicit")).AssertNoDiagnosticsReported().VerifyMatchingSourceNames("Displacement3.Conversions.g.cs");

    private static string DefaultsText => """
        using SharpMeasures.Generators;
        using SharpMeasures.Generators.Quantities;
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Vectors;

        [ConvertibleQuantity(typeof(Position3), ConversionDirection = QuantityConversionDirection.Onedirectional)]
        [SharpMeasuresVector(typeof(UnitOfLength))]
        public partial class Displacement3 { }
        
        [SharpMeasuresVector(typeof(UnitOfLength))]
        public partial class Position3 { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }
        
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static string SpecifiedBehaviourText(string behaviour) => $$"""
        using SharpMeasures.Generators;
        using SharpMeasures.Generators.Quantities;
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Vectors;

        [ConvertibleQuantity(typeof(Position3), ConversionDirection = QuantityConversionDirection.Onedirectional, CastOperatorBehaviour = ConversionOperatorBehaviour.{{behaviour}})]
        [SharpMeasuresVector(typeof(UnitOfLength))]
        public partial class Displacement3 { }
        
        [SharpMeasuresVector(typeof(UnitOfLength))]
        public partial class Position3 { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }
        
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;
}
