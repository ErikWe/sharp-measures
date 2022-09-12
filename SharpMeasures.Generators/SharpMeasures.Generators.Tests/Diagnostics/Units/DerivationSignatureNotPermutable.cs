namespace SharpMeasures.Generators.Tests.Diagnostics.Units;

using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Tests.Utility;
using SharpMeasures.Generators.Tests.Verify;

using System.Collections.Generic;
using System.Threading.Tasks;

using VerifyXunit;

using Xunit;

[UsesVerify]
public class DerivationSignatureNotPermutable
{
    [Fact]
    public Task OneUnit() => AssertOneUnit().VerifyDiagnostics();

    [Fact]
    public void AllSameUnits() => AssertAllSameUnits();

    private static GeneratorVerifier AssertExactlyDerivationSignatureNotPermutableDiagnostics(string source) => GeneratorVerifier.Construct<SharpMeasuresGenerator>(source).AssertExactlyListedDiagnosticsIDsReported(DerivationSignatureNotPermutableDiagnostics);
    private static IReadOnlyCollection<string> DerivationSignatureNotPermutableDiagnostics { get; } = new string[] { DiagnosticIDs.DerivationSignatureNotPermutable };

    private static string OneUnitText => """
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;

        [SharpMeasuresScalar(typeof(UnitOfFrequency))]
        public partial class Frequency { }

        [SharpMeasuresScalar(typeof(UnitOfTime))]
        public partial class Time { }

        [SharpMeasuresUnit(typeof(Frequency))]
        public partial class UnitOfFrequency { }

        [DerivableUnit("1 / {0}", typeof(UnitOfFrequency), Permutations = true)]
        [SharpMeasuresUnit(typeof(Time))]
        public partial class UnitOfTime { }
        """;

    private static GeneratorVerifier AssertOneUnit()
    {
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(OneUnitText, target: "true", prefix: "Permutations = ");

        return AssertExactlyDerivationSignatureNotPermutableDiagnostics(OneUnitText).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(OneUnitIdentical);
    }

    private static string AllSameUnitsText => """
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }

        [SharpMeasuresScalar(typeof(UnitOfArea))]
        public partial class Area { }

        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }

        [DerivableUnit("{0} * {1}", typeof(UnitOfLength), typeof(UnitOfLength), Permutations = true)]
        [SharpMeasuresUnit(typeof(Area))]
        public partial class UnitOfArea { }
        """;

    private static GeneratorVerifier AssertAllSameUnits()
    {
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(AllSameUnitsText, target: "true", prefix: "Permutations = ");

        return AssertExactlyDerivationSignatureNotPermutableDiagnostics(AllSameUnitsText).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(AllSameUnitsIdentical);
    }

    private static GeneratorVerifier OneUnitIdentical => GeneratorVerifier.Construct<SharpMeasuresGenerator>(OneUnitIdenticalText);
    private static GeneratorVerifier AllSameUnitsIdentical => GeneratorVerifier.Construct<SharpMeasuresGenerator>(AllSameUnitsIdenticalText);

    private static string OneUnitIdenticalText => """
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        
        [SharpMeasuresScalar(typeof(UnitOfFrequency))]
        public partial class Frequency { }
        
        [SharpMeasuresScalar(typeof(UnitOfTime))]
        public partial class Time { }
        
        [SharpMeasuresUnit(typeof(Frequency))]
        public partial class UnitOfFrequency { }
        
        [DerivableUnit("1 / {0}", typeof(UnitOfFrequency))]
        [SharpMeasuresUnit(typeof(Time))]
        public partial class UnitOfTime { }
        """;

    private static string AllSameUnitsIdenticalText => """
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }

        [SharpMeasuresScalar(typeof(UnitOfArea))]
        public partial class Area { }

        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }

        [DerivableUnit("{0} * {1}", typeof(UnitOfLength), typeof(UnitOfLength))]
        [SharpMeasuresUnit(typeof(Area))]
        public partial class UnitOfArea { }
        """;
}
