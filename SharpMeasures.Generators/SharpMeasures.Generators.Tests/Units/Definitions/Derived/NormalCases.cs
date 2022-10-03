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
            using SharpMeasures.Generators;
            
            [ScalarQuantity(typeof(UnitOfLength))]
            public partial class Length { }
            
            [ScalarQuantity(typeof(UnitOfTime))]
            public partial class Time { }

            [ScalarQuantity(typeof(UnitOfSpeed))]
            public partial class Speed { }

            [FixedUnitInstance("Metre", "Metres")]
            [Unit(typeof(Length))]
            public partial class UnitOfLength { }

            [FixedUnitInstance("Second", "Seconds")]
            [Unit(typeof(Time))]
            public partial class UnitOfTime { }

            [DerivableUnit("{0} / {1}", typeof(UnitOfLength), typeof(UnitOfTime))]
            [DerivedUnitInstance("MetrePerSecond", "MetresPerSecond", new[] { "Metre", "Second" })]
            [Unit(typeof(Speed))]
            public partial class UnitOfSpeed { }
            """;

        return GeneratorVerifier.Construct<SharpMeasuresGenerator>(source).AssertNoDiagnosticsReported().VerifyMatchingSourceNames("UnitOfSpeed.Instances.g.cs");
    }
}
