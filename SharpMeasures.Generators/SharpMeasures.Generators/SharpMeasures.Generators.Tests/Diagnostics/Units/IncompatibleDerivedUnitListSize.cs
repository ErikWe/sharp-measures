namespace SharpMeasures.Generators.Tests.Diagnostics.Units;

using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Tests.Verify;

using System.Collections.Generic;
using System.Threading.Tasks;

using VerifyXunit;

using Xunit;

[UsesVerify]
public class IncompatibleDerivedUnitListSize
{
    [Fact]
    public Task VerifyIncompatibleDerivedUnitListSizeDiagnosticsMessage()
    {
        string source = SourceText("\"Metre\"");

        return AssertExactlyIncompatibleDerivedUnitListSizeDiagnosticsWithValidLocation(source).VerifyDiagnostics();
    }

    [Theory]
    [InlineData("\"Metre\"")]
    [InlineData("new[] { \"Metre\" }")]
    [InlineData("\"Metre\", \"Metre\", \"Metre\"")]
    public void ExactList(string units)
    {
        string source = SourceText(units);

        AssertExactlyIncompatibleDerivedUnitListSizeDiagnosticsWithValidLocation(source);
    }

    private static string SourceText(string units) => $$"""
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

    private static GeneratorVerifier AssertExactlyIncompatibleDerivedUnitListSizeDiagnosticsWithValidLocation(string source) => GeneratorVerifier.Construct<SharpMeasuresGenerator>(source).AssertExactlyListedDiagnosticsIDsReported(IncompatibleDerivedUnitListSizeDiagnostics).AssertAllDiagnosticsValidLocation();
    private static IReadOnlyCollection<string> IncompatibleDerivedUnitListSizeDiagnostics { get; } = new string[] { DiagnosticIDs.IncompatibleDerivedUnitListSize };
}
