namespace SharpMeasures.Generators.Tests.Units.Definitions.Derived;

using SharpMeasures.Generators.Tests.Verify;

using System.Threading.Tasks;

using VerifyXunit;

using Xunit;

[UsesVerify]
public class NormalCases
{
    [Fact]
    public Task SimpleDerivedUnitInstance()
    {
        var source = """
            using SharpMeasures.Generators.Scalars;
            using SharpMeasures.Generators.Units;
            
            [SharpMeasuresScalar(typeof(UnitOfLength))]
            public partial class Length { }
            
            [SharpMeasuresScalar(typeof(UnitOfTime))]
            public partial class Time { }

            [SharpMeasuresScalar(typeof(UnitOfSpeed))]
            public partial class Speed { }

            [FixedUnitInstance("Metre", "Metres")]
            [SharpMeasuresUnit(typeof(Length))]
            public partial class UnitOfLength { }

            [FixedUnitInstance("Second", "Seconds")]
            [SharpMeasuresUnit(typeof(Time))]
            public partial class UnitOfTime { }

            [DerivableUnit("{0} / {1}", typeof(UnitOfLength), typeof(UnitOfTime))]
            [DerivedUnitInstance("MetrePerSecond", "MetresPerSecond", new[] { "Metre", "Second" })]
            [SharpMeasuresUnit(typeof(Speed))]
            public partial class UnitOfSpeed { }
            """;

        return GeneratorVerifier.Construct<SharpMeasuresGenerator>(source).AssertNoDiagnosticsReported().VerifyMatchingSourceNames("UnitOfSpeed.Instances.g.cs");
    }
}
