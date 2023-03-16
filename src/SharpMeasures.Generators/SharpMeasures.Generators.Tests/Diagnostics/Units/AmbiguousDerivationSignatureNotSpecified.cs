namespace SharpMeasures.Generators.Tests.Diagnostics.Units;

using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Tests.Utility;
using SharpMeasures.Generators.Tests.Verify;

using System.Collections.Generic;
using System.Threading.Tasks;

using VerifyXunit;

using Xunit;

[UsesVerify]
public class AmbiguousDerivationSignatureNotSpecified
{
    [Fact]
    public Task VerifyAmbiguousDerivationSignatureNotSpecifiedDiagnosticsMessage() => Assert(IgnoredName).VerifyDiagnostics();

    [Theory]
    [MemberData(nameof(InvalidNames))]
    public void ExactList(string derivationID) => Assert(derivationID);

    public static IEnumerable<object[]> InvalidNames => new object[][]
    {
        new[] { IgnoredName },
        new[] { NullName },
        new[] { EmptyName }
    };

    private static string IgnoredName { get; } = string.Empty;
    private static string NullName { get; } = "null, ";
    private static string EmptyName { get; } = "\"\", ";

    private static GeneratorVerifier AssertExactlyAmbiguousDerivationSignatureNotSpecifiedDiagnostics(string source) => GeneratorVerifier.Construct<SharpMeasuresGenerator>(source, GeneratorVerifierSettings.TestCodeAssertions).AssertExactlyListedDiagnosticsIDsReported(AmbiguousDerivationSignatureNotSpecifiedDiagnostics);
    private static IReadOnlyCollection<string> AmbiguousDerivationSignatureNotSpecifiedDiagnostics { get; } = new string[] { DiagnosticIDs.AmbiguousDerivationSignatureNotSpecified };

    private static string Text(string derivationIDWithoutComma) => $$"""
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
        [DerivedUnitInstance("MetrePerSecond", "MetresPerSecond", {{derivationIDWithoutComma}}new[] { "Metre", "Second" })]
        [Unit(typeof(Speed))]
        public partial class UnitOfSpeed { }
        """;

    private static GeneratorVerifier Assert(string derivationIDWithoutComma)
    {
        var source = Text(derivationIDWithoutComma);
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(source, target: "DerivedUnitInstance");

        return AssertExactlyAmbiguousDerivationSignatureNotSpecifiedDiagnostics(source).AssertDiagnosticsLocation(expectedLocation);
    }
}
