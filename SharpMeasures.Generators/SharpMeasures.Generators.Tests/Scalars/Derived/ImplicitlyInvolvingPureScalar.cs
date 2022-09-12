namespace SharpMeasures.Generators.Tests.Scalars.Derived;

using SharpMeasures.Generators.Tests.Verify;

using VerifyXunit;

using Xunit;

[UsesVerify]
public class ImplicitlyInvolvingPureScalar
{
    [Fact]
    public void Defaults() => GeneratorVerifier.Construct<SharpMeasuresGenerator>(DefaultsText).AssertIdenticalSources(Identical);

    private static string DefaultsText => """
        using SharpMeasures.Generators.Quantities;
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;

        [DerivedQuantity("1 / {0}", typeof(Frequency))]
        [SharpMeasuresScalar(typeof(UnitOfTime))]
        public partial class Time { }

        [SharpMeasuresScalar(typeof(UnitOfFrequency))]
        public partial class Frequency { }
        
        [SharpMeasuresUnit(typeof(Time))]
        public partial class UnitOfTime { }

        [SharpMeasuresUnit(typeof(Frequency))]
        public partial class UnitOfFrequency { }
        """;

    private static GeneratorVerifier Identical => GeneratorVerifier.Construct<SharpMeasuresGenerator>(IdenticalText);

    private static string IdenticalText => """
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
