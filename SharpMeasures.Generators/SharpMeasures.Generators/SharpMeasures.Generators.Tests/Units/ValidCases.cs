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

        string[] verifiedSources = new[] { "UnitOfLength_Common.g.cs" };

        return GeneratorVerifier.Construct<SharpMeasuresGenerator>(source).VerifyListedSourceNames(verifiedSources);
    }
}
