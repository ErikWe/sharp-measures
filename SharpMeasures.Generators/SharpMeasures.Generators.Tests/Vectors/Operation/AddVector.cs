namespace SharpMeasures.Generators.Tests.Vectors.Operation;

using SharpMeasures.Generators.Tests.Verify;

using System.Threading.Tasks;

using VerifyXunit;

using Xunit;

[UsesVerify]
public class AddVector
{
    [Fact]
    public Task Verify() => GeneratorVerifier.Construct<SharpMeasuresGenerator>(Text).AssertNoDiagnosticsReported().VerifyMatchingSourceNames("Position3.Operations.g.cs");

    private static string Text => """
        using SharpMeasures.Generators;

        [QuantityOperation(typeof(Position3), typeof(Displacement3), OperatorType.Addition)]
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
