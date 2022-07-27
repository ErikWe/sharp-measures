namespace SharpMeasures.Generators.Tests.Units;

using SharpMeasures.Generators.Tests.Verify;

using System.Threading.Tasks;

using VerifyXunit;

using Xunit;

[UsesVerify]
public class ValidCases
{
    [Fact]
    public Task UnbiasedUnit_Verify()
    {
        string source = """
            using SharpMeasures.Generators.Scalars;
            using SharpMeasures.Generators.Units;

            [SharpMeasuresScalar(typeof(UnitOfLength))]
            public partial class Length { }

            [SharpMeasuresScalar(typeof(UnitOfTime))]
            public partial class Time { }

            [SharpMeasuresUnit(typeof(Length))]
            [FixedUnit("Metre", "Metres")]
            [PrefixedUnit("Kilometre", "Kilometres", MetricPrefix.Kilo)]
            public partial class UnitOfLength { }

            [DerivableUnit("1", typeof(UnitOfLength), typeof(UnitOfTime), "{0} / {1}")]
            [DerivedUnit("MetrePerSecond", "MetresPerSecond", "1", "Metre", "Second")]
            public partial class UnitOfSpeed { }
            """;

        string[] verifiedSources = new[] { "UnitOfLength_Common.g.cs" };

        return GeneratorVerifier.Construct<SharpMeasuresGenerator>(source).VerifyListedSourceNames(verifiedSources);
    }
}
