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
        using SharpMeasures.Generators.Quantities;
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Vectors;

        [ConvertibleQuantity(typeof(Position3))]
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

        [ConvertibleQuantity(typeof(Position3), CastOperatorBehaviour = ConversionOperatorBehaviour.{{behaviour}})]
        [SharpMeasuresVector(typeof(UnitOfLength))]
        public partial class Displacement3 { }
        
        [SharpMeasuresVector(typeof(UnitOfLength))]
        public partial class Position3 { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }
        
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static string IdenticalText(string behaviour) => $$"""
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
