namespace SharpMeasures.Generators.Tests.Diagnostics.Units;

using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Tests.Utility;
using SharpMeasures.Generators.Tests.Verify;

using System.Collections.Generic;
using System.Threading.Tasks;

using VerifyXunit;

using Xunit;

[UsesVerify]
public class IncompatibleDerivedUnitListSize
{
    [Fact]
    public Task VerifyIncompatibleDerivedUnitListSizeDiagnosticsMessage() => AssertTwoElementSignature(OneElement).VerifyDiagnostics();

    [Theory]
    [MemberData(nameof(IncompatibleUnitLists))]
    public void TwoElementSignature(SourceSubtext units) => AssertTwoElementSignature(units);

    public static IEnumerable<object[]> IncompatibleUnitLists() => new object[][]
    {
        new object[] { OneElement },
        new object[] { ThreeElements }
    };

    private static SourceSubtext OneElement { get; } = SourceSubtext.Covered("{ \"Metre\" }", prefix: "new[] ");
    private static SourceSubtext ThreeElements { get; } = SourceSubtext.Covered("{ \"Metre\", \"Metre\", \"Metre\" }", prefix: "new[] ");

    private static GeneratorVerifier AssertExactlyIncompatibleDerivedUnitListSizeDiagnostics(string source) => GeneratorVerifier.Construct<SharpMeasuresGenerator>(source, GeneratorVerifierSettings.TestCodeAssertions).AssertExactlyListedDiagnosticsIDsReported(IncompatibleDerivedUnitListSizeDiagnostics);
    private static IReadOnlyCollection<string> IncompatibleDerivedUnitListSizeDiagnostics { get; } = new string[] { DiagnosticIDs.IncompatibleDerivedUnitListSize };

    private static string TwoElementSignatureText(SourceSubtext units) => $$"""
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

        [Unit(typeof(Time))]
        public partial class UnitOfTime { }

        [DerivableUnit("id", "{0} / {1}", typeof(UnitOfLength), typeof(UnitOfTime))]
        [DerivedUnitInstance("MetrePerSecond", "MetresPerSecond", "id", {{units}})]
        [Unit(typeof(Speed))]
        public partial class UnitOfSpeed { }
        """;

    private static GeneratorVerifier AssertTwoElementSignature(SourceSubtext units)
    {
        var source = TwoElementSignatureText(units);
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(source, units.Context.With(outerPrefix: "DerivedUnitInstance(\"MetrePerSecond\", \"MetresPerSecond\", \"id\", "));

        return AssertExactlyIncompatibleDerivedUnitListSizeDiagnostics(source).AssertDiagnosticsLocation(expectedLocation);
    }
}
