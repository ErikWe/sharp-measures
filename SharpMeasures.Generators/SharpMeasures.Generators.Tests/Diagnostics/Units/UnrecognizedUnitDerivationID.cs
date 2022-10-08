namespace SharpMeasures.Generators.Tests.Diagnostics.Units;

using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Tests.Utility;
using SharpMeasures.Generators.Tests.Verify;

using System.Collections.Generic;
using System.Threading.Tasks;

using VerifyXunit;

using Xunit;

[UsesVerify]
public class UnrecognizedUnitDerivationID
{
    [Fact]
    public Task MultipleDerivations() => AssertMultipleDerivations().VerifyDiagnostics();

    [Fact]
    public void SingleDerivation() => AssertSingleDerivation();

    private static GeneratorVerifier AssertExactlyUnrecognizedUnitDerivationIDDiagnostics(string source) => GeneratorVerifier.Construct<SharpMeasuresGenerator>(source, GeneratorVerifierSettings.TestCodeAssertions).AssertExactlyListedDiagnosticsIDsReported(UnrecognizedUnitderivationIDDiagnostics);
    private static IReadOnlyCollection<string> UnrecognizedUnitderivationIDDiagnostics { get; } = new string[] { DiagnosticIDs.UnrecognizedUnitDerivationID };

    private static string MultipleDerivationsText => """
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

        [DerivableUnit("1", "{0} / {1}", typeof(UnitOfLength), typeof(UnitOfTime))]
        [DerivableUnit("2", "{1} / {0}", typeof(UnitOfTime), typeof(UnitOfLength))]
        [DerivedUnitInstance("MetrePerSecond", "MetresPerSecond", "3", new[] { "Metre", "Second" })]
        [Unit(typeof(Speed))]
        public partial class UnitOfSpeed { }
        """;

    private static GeneratorVerifier AssertMultipleDerivations()
    {
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(MultipleDerivationsText, target: "\"3\"", postfix: ", new[] { \"Metre\", \"Second\" })]");

        return AssertExactlyUnrecognizedUnitDerivationIDDiagnostics(MultipleDerivationsText).AssertDiagnosticsLocation(expectedLocation);
    }

    private static string SingleDerivationText => """
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

        [DerivableUnit("1", "{0} / {1}", typeof(UnitOfLength), typeof(UnitOfTime))]
        [DerivedUnitInstance("MetrePerSecond", "MetresPerSecond", "2", new[] { "Metre", "Second" })]
        [Unit(typeof(Speed))]
        public partial class UnitOfSpeed { }
        """;

    private static GeneratorVerifier AssertSingleDerivation()
    {
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(SingleDerivationText, target: "\"2\"", postfix: ", new[] { \"Metre\", \"Second\" })]");

        return AssertExactlyUnrecognizedUnitDerivationIDDiagnostics(SingleDerivationText).AssertDiagnosticsLocation(expectedLocation);
    }
}
