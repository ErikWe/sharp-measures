namespace SharpMeasures.Generators.Tests.Units.Definitions.Prefixed;

using SharpMeasures.Generators.Tests.Verify;

using System.Threading.Tasks;

using VerifyXunit;

using Xunit;

[UsesVerify]
public class NormalCases
{
    [Fact]
    public Task MetricPrefix() => Verify(MetricPrefixText);

    [Fact]
    public Task BinaryPrefix() => Verify(BinaryPrefixText);

    private static Task Verify(string source) => GeneratorVerifier.Construct<SharpMeasuresGenerator>(source).AssertNoDiagnosticsReported().VerifyMatchingSourceNames("UnitOfLength.Instances.g.cs");

    private static string MetricPrefixText => """
        using SharpMeasures.Generators;
            
        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }
            
        [FixedUnitInstance("Metre", "Metres")]
        [PrefixedUnitInstance("Kilometer", "Kilometers", "Metre", MetricPrefixName.Kilo)]
        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static string BinaryPrefixText => """
        using SharpMeasures.Generators;
            
        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }
            
        [FixedUnitInstance("Metre", "Metres")]
        [PrefixedUnitInstance("Kibimeter", "Kibimeters", "Metre", BinaryPrefixName.Kibi)]
        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;
}
