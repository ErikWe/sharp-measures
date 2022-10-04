namespace SharpMeasures.Generators.Tests.VectorGroupMembers.Convertible;

using SharpMeasures.Generators.Tests.Verify;

using VerifyXunit;

using Xunit;

[UsesVerify]
public class DefinedInGroup
{
    [Fact]
    public void Assert() => GeneratorVerifier.Construct<SharpMeasuresGenerator>(Text).AssertNoDiagnosticsReported().AssertIdenticalSpecifiedSources(Identical, "Displacement3.Conversions.g.cs");

    private static string Text => """
        using SharpMeasures.Generators;

        [VectorGroupMember(typeof(Position))]
        public partial class Position3 { }

        [ConvertibleQuantity(typeof(Displacement), ConversionDirection = QuantityConversionDirection.Bidirectional)]
        [VectorGroup(typeof(UnitOfLength))]
        public static partial class Position { }

        [VectorGroupMember(typeof(Displacement))]
        public partial class Displacement3 { }

        [VectorGroup(typeof(UnitOfLength))]
        public static partial class Displacement { }

        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }
        
        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier Identical => GeneratorVerifier.Construct<SharpMeasuresGenerator>(IdenticalText);

    private static string IdenticalText => """
        using SharpMeasures.Generators;

        [ConvertibleQuantity(typeof(Displacement3), ConversionDirection = QuantityConversionDirection.Bidirectional)]
        [VectorGroupMember(typeof(Position))]
        public partial class Position3 { }
        
        [VectorGroup(typeof(UnitOfLength))]
        public static partial class Position { }
        
        [VectorGroupMember(typeof(Displacement))]
        public partial class Displacement3 { }
        
        [VectorGroup(typeof(UnitOfLength))]
        public static partial class Displacement { }

        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }
        
        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;
}
