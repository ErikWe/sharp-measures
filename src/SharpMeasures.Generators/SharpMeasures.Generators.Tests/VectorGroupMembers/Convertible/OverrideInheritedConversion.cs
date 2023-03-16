namespace SharpMeasures.Generators.Tests.VectorGroupMembers.Convertible;

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

    private static Task Verify(string source) => GeneratorVerifier.Construct<SharpMeasuresGenerator>(source).AssertNoDiagnosticsReported().VerifyMatchingSourceNames("Position3.Conversions.g.cs");

    private static string BidirectionalText => """
        using SharpMeasures.Generators;

        [ConvertibleQuantity(typeof(Size3), CastOperatorBehaviour = ConversionOperatorBehaviour.Implicit, ConversionDirection = QuantityConversionDirection.Bidirectional)]
        [VectorGroupMember(typeof(Position))]
        public partial class Position3 { }

        [ConvertibleQuantity(typeof(Size), CastOperatorBehaviour = ConversionOperatorBehaviour.Explicit, ConversionDirection = QuantityConversionDirection.Bidirectional)]
        [VectorGroup(typeof(UnitOfLength))]
        public static partial class Position { }

        [VectorGroupMember(typeof(Size))]
        public partial class Size3 { }

        [VectorGroup(typeof(UnitOfLength))]
        public static partial class Size { }

        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }

        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static string ModifyOnlyOneDirectionText => """
        using SharpMeasures.Generators;
        
        [ConvertibleQuantity(typeof(Size3), CastOperatorBehaviour = ConversionOperatorBehaviour.Implicit, ConversionDirection = QuantityConversionDirection.Onedirectional)]
        [VectorGroupMember(typeof(Position))]
        public partial class Position3 { }

        [ConvertibleQuantity(typeof(Size), CastOperatorBehaviour = ConversionOperatorBehaviour.Explicit, ConversionDirection = QuantityConversionDirection.Bidirectional)]
        [VectorGroup(typeof(UnitOfLength))]
        public static partial class Position { }
        
        [VectorGroupMember(typeof(Size))]
        public partial class Size3 { }
        
        [VectorGroup(typeof(UnitOfLength))]
        public static partial class Size { }
        
        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }

        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;
}
