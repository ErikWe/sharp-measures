namespace SharpMeasures.Generators.Tests.SpecializedScalars.Constant;

using SharpMeasures.Generators.Tests.Verify;

using System.Threading.Tasks;

using VerifyXunit;

using Xunit;

[UsesVerify]
public class InheritedConstant
{
    [Fact]
    public Task Verify() => GeneratorVerifier.Construct<SharpMeasuresGenerator>(Text).VerifyMatchingSourceNames("Distance.Units.g.cs");

    private static string Text => """
        using SharpMeasures.Generators;
        
        [SpecializedScalarQuantity(typeof(Length))]
        public partial class Distance { }
        
        [ScalarConstant("Planck", "Metre", 1.616255E-35)]
        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }

        [FixedUnitInstance("Metre", "Metres")]
        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;
}
