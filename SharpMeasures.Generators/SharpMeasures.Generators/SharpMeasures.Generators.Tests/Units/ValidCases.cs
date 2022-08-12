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

            [SharpMeasuresUnit(typeof(Length))]
            public partial class UnitOfLength { }
            """;

        return GeneratorVerifier.Construct<SharpMeasuresGenerator>(source).AssertNoCompilationDiagnostics().VerifyMatchingSourceNames(@"UnitOfLength_\S+\.g\.cs");
    }

    [Fact]
    public Task BiasedUnit_Verify()
    {
        string source = """
            using SharpMeasures.Generators.Scalars;
            using SharpMeasures.Generators.Units;

            [SharpMeasuresScalar(typeof(UnitOfTemperature))]
            public partial class TemperatureDifference { }

            [SharpMeasuresUnit(typeof(TemperatureDifference), BiasTerm = true)]
            public partial class UnitOfTemperature { }
            """;

        return GeneratorVerifier.Construct<SharpMeasuresGenerator>(source).VerifyMatchingSourceNames(@"UnitOfTemperature_\S+\.g\.cs");
    }
}
