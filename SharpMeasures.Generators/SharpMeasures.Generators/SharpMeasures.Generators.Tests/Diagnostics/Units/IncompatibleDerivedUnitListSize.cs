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
    private static string Text(string signature) => $$"""
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }

        [SharpMeasuresScalar(typeof(UnitOfTime))]
        public partial class Time { }

        [SharpMeasuresScalar(typeof(UnitOfSpeed))]
        public partial class Speed { }

        [FixedUnit("Metre", "Metres", 1)]
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }

        [FixedUnit("Second", "Seconds", 1)]
        [SharpMeasuresUnit(typeof(Time))]
        public partial class UnitOfTime { }

        [DerivableUnit("id", "{0} / {1}", typeof(UnitOfLength), typeof(UnitOfTime))]
        [DerivedUnit("MetrePerSecond", "MetresPerSecond", "id", {{signature}})]
        [SharpMeasuresUnit(typeof(Speed))]
        public partial class UnitOfSpeed { }
        """;

    [Fact]
    public Task Params_ExactListAndVerify()
    {
        string source = Text("\"Metre\"");

        return AssertExactlyIncompatibleDerivedUnitListSizeDiagnostics(source).VerifyDiagnostics();
    }

    [Fact]
    public Task Array_ExactListAndVerify()
    {
        string source = Text("new[] { \"Metre\" }");

        return AssertExactlyIncompatibleDerivedUnitListSizeDiagnostics(source).VerifyDiagnostics();
    }

    private static GeneratorVerifier AssertExactlyIncompatibleDerivedUnitListSizeDiagnostics(string source)
        => GeneratorVerifier.Construct<SharpMeasuresGenerator>(source).AssertExactlyListedDiagnosticsIDsReported(IncompatibleDerivedUnitListSizeDiagnostics);

    private static IReadOnlyCollection<string> IncompatibleDerivedUnitListSizeDiagnostics { get; } = new string[] { DiagnosticIDs.IncompatibleDerivedUnitListSize };
}
