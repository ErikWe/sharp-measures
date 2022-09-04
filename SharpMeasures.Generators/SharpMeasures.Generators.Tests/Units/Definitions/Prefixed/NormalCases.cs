namespace SharpMeasures.Generators.Tests.Units.Definitions.Prefixed;

using SharpMeasures.Generators.Tests.Verify;

using System.Threading.Tasks;

using VerifyXunit;

using Xunit;

[UsesVerify]
public class NormalCases
{
    [Fact]
    public Task MetricPrefix()
    {
        var source = """
            using SharpMeasures.Generators.Scalars;
            using SharpMeasures.Generators.Units;
            
            [SharpMeasuresScalar(typeof(UnitOfLength))]
            public partial class Length { }
            
            [FixedUnitInstance("Metre", "Metres")]
            [PrefixedUnitInstance("Kilometer", "Kilometers", "Metre", MetricPrefixName.Kilo)]
            [SharpMeasuresUnit(typeof(Length))]
            public partial class UnitOfLength { }
            """;

        return GeneratorVerifier.Construct<SharpMeasuresGenerator>(source).VerifyMatchingSourceNames("UnitOfLength_Instances.g.cs");
    }

    [Fact]
    public Task BinaryPrefix()
    {
        var source = """
            using SharpMeasures.Generators.Scalars;
            using SharpMeasures.Generators.Units;
            
            [SharpMeasuresScalar(typeof(UnitOfLength))]
            public partial class Length { }
            
            [FixedUnitInstance("Metre", "Metres")]
            [PrefixedUnitInstance("Kibimeter", "Kibimeters", "Metre", BinaryPrefixName.Kibi)]
            [SharpMeasuresUnit(typeof(Length))]
            public partial class UnitOfLength { }
            """;

        return GeneratorVerifier.Construct<SharpMeasuresGenerator>(source).VerifyMatchingSourceNames("UnitOfLength_Instances.g.cs");
    }
}
