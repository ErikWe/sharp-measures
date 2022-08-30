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
        new object[] { OneElementArray },
        new object[] { ThreeElements }
    };

    private static SourceSubtext OneElement { get; } = SourceSubtext.Covered("\"Metre\"");
    private static SourceSubtext OneElementArray { get; } = SourceSubtext.Covered("{ \"Metre\" }", prefix: "new[] ");
    private static SourceSubtext ThreeElements { get; } = SourceSubtext.Covered("\"Metre\", \"Metre\", \"Metre\"");

    private static GeneratorVerifier AssertExactlyIncompatibleDerivedUnitListSizeDiagnostics(string source) => GeneratorVerifier.Construct<SharpMeasuresGenerator>(source).AssertExactlyListedDiagnosticsIDsReported(IncompatibleDerivedUnitListSizeDiagnostics);
    private static IReadOnlyCollection<string> IncompatibleDerivedUnitListSizeDiagnostics { get; } = new string[] { DiagnosticIDs.IncompatibleDerivedUnitListSize };

    private static string TwoElementSignatureText(SourceSubtext units) => $$"""
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }

        [SharpMeasuresScalar(typeof(UnitOfTime))]
        public partial class Time { }

        [SharpMeasuresScalar(typeof(UnitOfSpeed))]
        public partial class Speed { }

        [FixedUnit("Metre", "Metres")]
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }

        [SharpMeasuresUnit(typeof(Time))]
        public partial class UnitOfTime { }

        [DerivableUnit("id", "{0} / {1}", typeof(UnitOfLength), typeof(UnitOfTime))]
        [DerivedUnit("MetrePerSecond", "MetresPerSecond", "id", {{units}})]
        [SharpMeasuresUnit(typeof(Speed))]
        public partial class UnitOfSpeed { }
        """;

    private static GeneratorVerifier AssertTwoElementSignature(SourceSubtext units)
    {
        var source = TwoElementSignatureText(units);
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(source, units.Context.With(outerPrefix: "DerivedUnit(\"MetrePerSecond\", \"MetresPerSecond\", \"id\", "));

        return AssertExactlyIncompatibleDerivedUnitListSizeDiagnostics(source).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(TwoElementSignatureIdentical);
    }

    private static GeneratorVerifier TwoElementSignatureIdentical => GeneratorVerifier.Construct<SharpMeasuresGenerator>(TwoElementSignatureIdenticalText);

    private static string TwoElementSignatureIdenticalText => """
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }

        [SharpMeasuresScalar(typeof(UnitOfTime))]
        public partial class Time { }

        [SharpMeasuresScalar(typeof(UnitOfSpeed))]
        public partial class Speed { }

        [FixedUnit("Metre", "Metres")]
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }

        [SharpMeasuresUnit(typeof(Time))]
        public partial class UnitOfTime { }

        [DerivableUnit("id", "{0} / {1}", typeof(UnitOfLength), typeof(UnitOfTime))]
        [SharpMeasuresUnit(typeof(Speed))]
        public partial class UnitOfSpeed { }
        """;
}
