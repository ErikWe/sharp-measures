namespace SharpMeasures.Generators.Tests.SpecializedScalars.Convertible;

using SharpMeasures.Generators.Tests.Verify;

using System.Threading.Tasks;

using VerifyXunit;

using Xunit;

[UsesVerify]
public class OverrideInheritedConversion
{
    [Fact]
    public Task Bidirectional() => Verify(BidirectionalText);

    [Fact]
    public Task ModifyOnlyOneDirection() => Verify(ModifyOnlyOneDirectionText);

    private static Task Verify(string source) => GeneratorVerifier.Construct<SharpMeasuresGenerator>(source).AssertNoDiagnosticsReported().VerifyMatchingSourceNames("Distance.Conversions.g.cs");

    private static string BidirectionalText => """
        using SharpMeasures.Generators;
        using SharpMeasures.Generators.Quantities;
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        
        [ConvertibleQuantity(typeof(Height), CastOperatorBehaviour = ConversionOperatorBehaviour.Implicit, ConversionDirection = QuantityConversionDirection.Bidirectional)]
        [SpecializedSharpMeasuresScalar(typeof(Length))]
        public partial class Distance { }

        [ConvertibleQuantity(typeof(Height), CastOperatorBehaviour = ConversionOperatorBehaviour.Explicit, ConversionDirection = QuantityConversionDirection.Bidirectional)]
        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }
        
        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Height { }

        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static string ModifyOnlyOneDirectionText => """
        using SharpMeasures.Generators;
        using SharpMeasures.Generators.Quantities;
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        
        [ConvertibleQuantity(typeof(Height), CastOperatorBehaviour = ConversionOperatorBehaviour.Implicit, ConversionDirection = QuantityConversionDirection.Onedirectional)]
        [SpecializedSharpMeasuresScalar(typeof(Length))]
        public partial class Distance { }

        [ConvertibleQuantity(typeof(Height), CastOperatorBehaviour = ConversionOperatorBehaviour.Explicit, ConversionDirection = QuantityConversionDirection.Bidirectional)]
        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }
        
        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Height { }

        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;
}
