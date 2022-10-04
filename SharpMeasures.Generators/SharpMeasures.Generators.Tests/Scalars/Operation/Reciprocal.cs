namespace SharpMeasures.Generators.Tests.Scalars.Operation;

using SharpMeasures.Generators.Tests.Verify;

using System.Threading.Tasks;

using VerifyXunit;

using Xunit;

[UsesVerify]
public class Reciprocal
{
    [Fact]
    public Task Verify() => GeneratorVerifier.Construct<SharpMeasuresGenerator>(Text).AssertNoDiagnosticsReported().VerifyMatchingSourceNames("Time.Operations.g.cs");

    private static string Text => """
        using SharpMeasures;
        using SharpMeasures.Generators;

        [QuantityOperation(typeof(Frequency), typeof(Scalar), OperatorType.Division, OperatorPosition.Right)]
        [ScalarQuantity(typeof(UnitOfTime))]
        public partial class Time { }
        
        [ScalarQuantity(typeof(UnitOfFrequency))]
        public partial class Frequency { }

        [Unit(typeof(Frequency))]
        public partial class UnitOfFrequency { }

        [Unit(typeof(Time))]
        public partial class UnitOfTime { }
        """;
}
