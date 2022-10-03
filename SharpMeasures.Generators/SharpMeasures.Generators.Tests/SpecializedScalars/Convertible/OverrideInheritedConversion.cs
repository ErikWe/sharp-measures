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
        
        [ConvertibleQuantity(typeof(Height), CastOperatorBehaviour = ConversionOperatorBehaviour.Implicit, ConversionDirection = QuantityConversionDirection.Bidirectional)]
        [SpecializedScalarQuantity(typeof(Length))]
        public partial class Distance { }

        [ConvertibleQuantity(typeof(Height), CastOperatorBehaviour = ConversionOperatorBehaviour.Explicit, ConversionDirection = QuantityConversionDirection.Bidirectional)]
        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }
        
        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Height { }

        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static string ModifyOnlyOneDirectionText => """
        using SharpMeasures.Generators;
        
        [ConvertibleQuantity(typeof(Height), CastOperatorBehaviour = ConversionOperatorBehaviour.Implicit, ConversionDirection = QuantityConversionDirection.Onedirectional)]
        [SpecializedScalarQuantity(typeof(Length))]
        public partial class Distance { }

        [ConvertibleQuantity(typeof(Height), CastOperatorBehaviour = ConversionOperatorBehaviour.Explicit, ConversionDirection = QuantityConversionDirection.Bidirectional)]
        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }
        
        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Height { }

        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;
}
