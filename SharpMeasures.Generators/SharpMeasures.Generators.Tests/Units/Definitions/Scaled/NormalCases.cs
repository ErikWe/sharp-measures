namespace SharpMeasures.Generators.Tests.Units.Definitions.Scaled;

using SharpMeasures.Generators.Tests.Verify;

using System.Threading.Tasks;

using VerifyXunit;

using Xunit;

[UsesVerify]
public class NormalCases
{
    [Fact]
    public Task Value() => GeneratorVerifier.Construct<SharpMeasuresGenerator>(ValueText).VerifyMatchingSourceNames("UnitOfLength.Instances.g.cs");

    [Fact]
    public Task Expression() => GeneratorVerifier.Construct<SharpMeasuresGenerator>(ExpressionText).VerifyMatchingSourceNames("UnitOfLength.Instances.g.cs");

    private static string ValueText => """
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
            
        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }
            
        [FixedUnitInstance("Metre", "Metres")]
        [ScaledUnitInstance("Kilometer", "Kilometers", "Metre", 1000)]
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static string ExpressionText => """
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
            
        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }
            
        [FixedUnitInstance("Metre", "Metres")]
        [ScaledUnitInstance("Kilometer", "Kilometers", "Metre", "1000")]
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;
}
