namespace SharpMeasures.Generators.Tests.Diagnostics.Quantities;

using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Tests.Verify;

using System.Collections.Generic;
using System.Threading.Tasks;

using VerifyXunit;

using Xunit;

[UsesVerify]
public class ContradictoryAttributes
{
    [Fact]
    public Task IncludeAndExcludeUnit_ExactListAndVerify()
    {
        string source = """
            using SharpMeasures.Generators.Quantities;
            using SharpMeasures.Generators.Scalars;
            using SharpMeasures.Generators.Units;
            using SharpMeasures.Generators.Units.Utility;

            [IncludeUnits("Metre")]
            [ExcludeUnits("Kilometre")]
            [SharpMeasuresScalar(typeof(UnitOfLength))]
            public partial class Length { }
            
            [FixedUnit("Metre", "Metres")]
            [PrefixedUnit("Kilometre", "Kilometres", "Metre", MetricPrefixName.Kilo)]
            [SharpMeasuresUnit(typeof(Length))]
            public partial class UnitOfLength { }
            """;

        return AssertExactlyContradictoryAttributesDiagnosticsWithValidLocation(source).VerifyDiagnostics();
    }

    [Fact]
    public Task IncludeAndExcludeBase_ExactListAndVerify()
    {
        string source = """
            using SharpMeasures.Generators.Scalars;
            using SharpMeasures.Generators.Units;
            using SharpMeasures.Generators.Units.Utility;

            [IncludeBases("Metre")]
            [ExcludeBases("Kilometre")]
            [SharpMeasuresScalar(typeof(UnitOfLength))]
            public partial class Length { }
            
            [FixedUnit("Metre", "Metres")]
            [PrefixedUnit("Kilometre", "Kilometres", "Metre", MetricPrefixName.Kilo)]
            [SharpMeasuresUnit(typeof(Length))]
            public partial class UnitOfLength { }
            """;

        return AssertExactlyContradictoryAttributesDiagnosticsWithValidLocation(source).VerifyDiagnostics();
    }

    private static GeneratorVerifier AssertExactlyContradictoryAttributesDiagnosticsWithValidLocation(string source) => GeneratorVerifier.Construct<SharpMeasuresGenerator>(source).AssertExactlyListedDiagnosticsIDsReported(ContradictoryAttributesDiagnostics).AssertAllDiagnosticsValidLocation();
    private static IReadOnlyCollection<string> ContradictoryAttributesDiagnostics { get; } = new string[] { DiagnosticIDs.ContradictoryAttributes };
}
