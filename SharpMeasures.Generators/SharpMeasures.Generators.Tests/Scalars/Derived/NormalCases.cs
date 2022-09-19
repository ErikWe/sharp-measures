namespace SharpMeasures.Generators.Tests.Scalars.Derived;

using SharpMeasures.Generators.Tests.Verify;

using System.Threading.Tasks;

using VerifyXunit;

using Xunit;

[UsesVerify]
public class NormalCases
{
    [Fact]
    public Task Defaults() => GeneratorVerifier.Construct<SharpMeasuresGenerator>(DefaultsText).AssertNoDiagnosticsReported().VerifyMatchingSourceNames("Time.Derivations.g.cs");

    private static string DefaultsText => """
        using SharpMeasures;
        using SharpMeasures.Generators.Quantities;
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;

        [DerivedQuantity("{0} / {1}", typeof(Scalar), typeof(Frequency))]
        [SharpMeasuresScalar(typeof(UnitOfTime))]
        public partial class Time { }

        [SharpMeasuresScalar(typeof(UnitOfFrequency))]
        public partial class Frequency { }
        
        [SharpMeasuresUnit(typeof(Time))]
        public partial class UnitOfTime { }

        [SharpMeasuresUnit(typeof(Frequency))]
        public partial class UnitOfFrequency { }
        """;
}
