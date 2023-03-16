namespace SharpMeasures.Generators.Tests.Vectors;

using SharpMeasures.Generators.Tests.Verify;

using System.Threading.Tasks;

using VerifyXunit;

using Xunit;

[UsesVerify]
public class Scalar
{
    [Fact]
    public Task Verify() => GeneratorVerifier.Construct<SharpMeasuresGenerator>(Text).VerifyMatchingSourceNames("Position3.Common.g.cs");

    private static string Text => """
        using SharpMeasures.Generators;

        [VectorQuantity(typeof(UnitOfLength), Scalar = typeof(Length))]
        public partial class Position3 { }

        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }

        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;
}
