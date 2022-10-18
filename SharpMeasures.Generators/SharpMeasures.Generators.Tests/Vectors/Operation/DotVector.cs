namespace SharpMeasures.Generators.Tests.Vectors.Operation;

using SharpMeasures.Generators.Tests.Verify;

using System.Threading.Tasks;

using VerifyXunit;

using Xunit;

[UsesVerify]
public class DotVector
{
    [Fact]
    public Task Verify() => GeneratorVerifier.Construct<SharpMeasuresGenerator>(Text).AssertNoDiagnosticsReported().VerifyMatchingSourceNames("Position3.Operations.g.cs");

    private static string Text => """
        using SharpMeasures.Generators;

        [VectorOperation(typeof(Length), typeof(Displacement3), VectorOperatorType.Dot)]
        [VectorQuantity(typeof(UnitOfLength))]
        public partial class Position3 { }

        [VectorQuantity(typeof(UnitOfLength))]
        public partial class Displacement3 { }
        
        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }

        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;
}
