namespace SharpMeasures.Generators.Tests.Diagnostics.Units;

using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Tests.Verify;

using System.Collections.Generic;
using System.Threading.Tasks;

using VerifyXunit;

using Xunit;

[UsesVerify]
public class UnrecognizedUnitDerivationID
{
    [Fact]
    public Task Missing_ExactListAndVerify()
    {
        string source = """
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

        [DerivableUnit("1", "{0} / {1}", typeof(UnitOfLength), typeof(UnitOfTime))]
        [DerivableUnit("2", "{1} / {0}", typeof(UnitOfTime), typeof(UnitOfLength))]
        [DerivedUnit("MetrePerSecond", "MetresPerSecond", "3", "Metre", "Second")]
        [SharpMeasuresUnit(typeof(Speed))]
        public partial class UnitOfSpeed { }
        """;

        return AssertExactlyUnrecognizedUnitDerivationIDDiagnostics(source).VerifyDiagnostics();
    }

    private static GeneratorVerifier AssertExactlyUnrecognizedUnitDerivationIDDiagnostics(string source)
        => GeneratorVerifier.Construct<SharpMeasuresGenerator>(source).AssertExactlyListedDiagnosticsIDsReported(UnrecognizedUnitderivationIDDiagnostics);

    private static IReadOnlyCollection<string> UnrecognizedUnitderivationIDDiagnostics { get; } = new string[] { DiagnosticIDs.UnrecognizedUnitDerivationID };
}
