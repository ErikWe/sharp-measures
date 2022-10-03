namespace SharpMeasures.Generators.Tests.Diagnostics.Units;

using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Tests.Utility;
using SharpMeasures.Generators.Tests.Verify;

using System.Collections.Generic;
using System.Threading.Tasks;

using VerifyXunit;

using Xunit;

[UsesVerify]
public class DerivableUnitShouldNotUseFixed
{
    [Fact]
    public Task VerifyDerivableUnitShouldNotUseFixedDiagnosticsMessage() => Assert().VerifyDiagnostics();

    private static GeneratorVerifier AssertExactlyDerivableUnitShouldNotUseFixedDiagnostics(string source) => GeneratorVerifier.Construct<SharpMeasuresGenerator>(source).AssertExactlyListedDiagnosticsIDsReported(DerivableUnitShouldNotUseFixedDiagnostics);
    private static IReadOnlyCollection<string> DerivableUnitShouldNotUseFixedDiagnostics { get; } = new string[] { DiagnosticIDs.DerivableUnitShouldNotUseFixed };

    private static string Text => """
        using SharpMeasures.Generators;

        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }

        [ScalarQuantity(typeof(UnitOfTime))]
        public partial class Time { }

        [ScalarQuantity(typeof(UnitOfSpeed))]
        public partial class Speed { }

        [Unit(typeof(Length))]
        public partial class UnitOfLength { }

        [Unit(typeof(Time))]
        public partial class UnitOfTime { }

        [DerivableUnit("{0} / {1}", typeof(UnitOfLength), typeof(UnitOfTime))]
        [FixedUnitInstance("MetrePerSecond", "MetresPerSecond")]
        [Unit(typeof(Speed))]
        public partial class UnitOfSpeed { }
        """;

    private static GeneratorVerifier Assert()
    {
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(Text, target: "FixedUnitInstance");

        return AssertExactlyDerivableUnitShouldNotUseFixedDiagnostics(Text).AssertDiagnosticsLocation(expectedLocation);
    }
}
