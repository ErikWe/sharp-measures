namespace SharpMeasures.Generators.Tests.SpecializedScalars.Operation;

using SharpMeasures.Generators.Tests.Verify;

using System.Threading.Tasks;

using VerifyXunit;

using Xunit;

[UsesVerify]
public class Overwrite
{
    [Fact]
    public Task Verify() => GeneratorVerifier.Construct<SharpMeasuresGenerator>(Text).AssertNoDiagnosticsReported().VerifyMatchingSourceNames("B.Operations.g.cs");

    private static string Text => """
        using SharpMeasures;
        using SharpMeasures.Generators;

        [QuantityOperation(typeof(B), typeof(Scalar), OperatorType.Division, OperatorPosition.Right)]
        [SpecializedScalarQuantity(typeof(A))]
        public partial class B { }

        [QuantityOperation(typeof(A), typeof(Scalar), OperatorType.Division, OperatorPosition.Right)]
        [ScalarQuantity(typeof(UnitOfA))]
        public partial class A { }

        [Unit(typeof(A))]
        public partial class UnitOfA { }
        """;
}
