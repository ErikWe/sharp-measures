namespace SharpMeasures.Generators.Tests.Diagnostics.Quantities;

using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Tests.Verify;

using System.Collections.Generic;
using System.Threading.Tasks;

using VerifyXunit;

using Xunit;

[UsesVerify]
public class InclusionOrExclusionHadNoEffect
{
    [Fact]
    public Task IncludeUnit_SameAttribute_ExactListAndVerify()
    {
        string source = """
            using SharpMeasures.Generators.Quantities;
            using SharpMeasures.Generators.Scalars;
            using SharpMeasures.Generators.Units;

            [IncludeUnits("Metre", "Metre")]
            [SharpMeasuresScalar(typeof(UnitOfLength))]
            public partial class Length { }
            
            [FixedUnit("Metre", "Metres")]
            [SharpMeasuresUnit(typeof(Length))]
            public partial class UnitOfLength { }
            """;

        return AssertExactlyInclusionOrExclusionHadNoEffectDiagnosticsWithValidLocation(source).VerifyDiagnostics();
    }

    [Fact]
    public Task ExcludeUnit_SameAttribute_ExactListAndVerify()
    {
        string source = """
            using SharpMeasures.Generators.Quantities;
            using SharpMeasures.Generators.Scalars;
            using SharpMeasures.Generators.Units;

            [ExcludeUnits("Metre", "Metre")]
            [SharpMeasuresScalar(typeof(UnitOfLength))]
            public partial class Length { }
            
            [FixedUnit("Metre", "Metres")]
            [SharpMeasuresUnit(typeof(Length))]
            public partial class UnitOfLength { }
            """;

        return AssertExactlyInclusionOrExclusionHadNoEffectDiagnosticsWithValidLocation(source).VerifyDiagnostics();
    }

    [Fact]
    public Task IncludeBase_SameAttribute_ExactListAndVerify()
    {
        string source = """
            using SharpMeasures.Generators.Quantities;
            using SharpMeasures.Generators.Scalars;
            using SharpMeasures.Generators.Units;

            [IncludeBases("Metre", "Metre")]
            [SharpMeasuresScalar(typeof(UnitOfLength))]
            public partial class Length { }
            
            [FixedUnit("Metre", "Metres")]
            [SharpMeasuresUnit(typeof(Length))]
            public partial class UnitOfLength { }
            """;

        return AssertExactlyInclusionOrExclusionHadNoEffectDiagnosticsWithValidLocation(source).VerifyDiagnostics();
    }

    [Fact]
    public Task ExcludeBase_SameAttribute_ExactListAndVerify()
    {
        string source = """
            using SharpMeasures.Generators.Quantities;
            using SharpMeasures.Generators.Scalars;
            using SharpMeasures.Generators.Units;

            [ExcludeBases("Metre", "Metre")]
            [SharpMeasuresScalar(typeof(UnitOfLength))]
            public partial class Length { }
            
            [FixedUnit("Metre", "Metres")]
            [SharpMeasuresUnit(typeof(Length))]
            public partial class UnitOfLength { }
            """;

        return AssertExactlyInclusionOrExclusionHadNoEffectDiagnosticsWithValidLocation(source).VerifyDiagnostics();
    }

    [Fact]
    public Task IncludeUnit_AlreadyIncluded_ResizedVector_ExactListAndVerify()
    {
        string source = """
            using SharpMeasures.Generators.Quantities;
            using SharpMeasures.Generators.Scalars;
            using SharpMeasures.Generators.Units;
            using SharpMeasures.Generators.Vectors;

            [IncludeUnits("Metre")]
            [ResizedSharpMeasuresVector(typeof(Position3))]
            public partial class Position2 { }

            [IncludeUnits("Metre")]
            [SharpMeasuresVector(typeof(UnitOfLength))]
            public partial class Position3 { }

            [SharpMeasuresScalar(typeof(UnitOfLength))]
            public partial class Length { }
            
            [FixedUnit("Metre", "Metres")]
            [SharpMeasuresUnit(typeof(Length))]
            public partial class UnitOfLength { }
            """;

        return AssertExactlyInclusionOrExclusionHadNoEffectDiagnosticsWithValidLocation(source).VerifyDiagnostics();
    }

    [Fact]
    public Task IncludeUnit_NotExcludedImplicit_ResizedVector_ExactListAndVerify()
    {
        string source = """
            using SharpMeasures.Generators.Quantities;
            using SharpMeasures.Generators.Scalars;
            using SharpMeasures.Generators.Units;
            using SharpMeasures.Generators.Vectors;

            [IncludeUnits("Metre")]
            [ResizedSharpMeasuresVector(typeof(Position3))]
            public partial class Position2 { }

            [SharpMeasuresVector(typeof(UnitOfLength))]
            public partial class Position3 { }

            [SharpMeasuresScalar(typeof(UnitOfLength))]
            public partial class Length { }
            
            [FixedUnit("Metre", "Metres")]
            [SharpMeasuresUnit(typeof(Length))]
            public partial class UnitOfLength { }
            """;

        return AssertExactlyInclusionOrExclusionHadNoEffectDiagnosticsWithValidLocation(source).VerifyDiagnostics();
    }

    [Fact]
    public Task IncludeUnit_NotExcludedExplicit_ResizedVector_ExactListAndVerify()
    {
        string source = """
            using SharpMeasures.Generators.Quantities;
            using SharpMeasures.Generators.Scalars;
            using SharpMeasures.Generators.Units;
            using SharpMeasures.Generators.Units.Utility;
            using SharpMeasures.Generators.Vectors;

            [IncludeUnits("Metre")]
            [ResizedSharpMeasuresVector(typeof(Position3))]
            public partial class Position2 { }

            [ExcludeUnits("Kilometre")]
            [SharpMeasuresVector(typeof(UnitOfLength))]
            public partial class Position3 { }

            [SharpMeasuresScalar(typeof(UnitOfLength))]
            public partial class Length { }
            
            [FixedUnit("Metre", "Metres")]
            [PrefixedUnit("Kilometre", "Kilometres", "Metre", MetricPrefixName.Kilo)]
            [SharpMeasuresUnit(typeof(Length))]
            public partial class UnitOfLength { }
            """;

        return AssertExactlyInclusionOrExclusionHadNoEffectDiagnosticsWithValidLocation(source).VerifyDiagnostics();
    }

    [Fact]
    public Task ExcludeUnit_AlreadyExcluded_ResizedVector_ExactListAndVerify()
    {
        string source = """
            using SharpMeasures.Generators.Quantities;
            using SharpMeasures.Generators.Scalars;
            using SharpMeasures.Generators.Units;
            using SharpMeasures.Generators.Vectors;

            [ExcludeUnits("Metre")]
            [ResizedSharpMeasuresVector(typeof(Position3))]
            public partial class Position2 { }

            [ExcludeUnits("Metre")]
            [SharpMeasuresVector(typeof(UnitOfLength))]
            public partial class Position3 { }

            [SharpMeasuresScalar(typeof(UnitOfLength))]
            public partial class Length { }
            
            [FixedUnit("Metre", "Metres")]
            [SharpMeasuresUnit(typeof(Length))]
            public partial class UnitOfLength { }
            """;

        return AssertExactlyInclusionOrExclusionHadNoEffectDiagnosticsWithValidLocation(source).VerifyDiagnostics();
    }

    [Fact]
    public Task IncludeUnit_NotIncluded_ResizedVector_ExactListAndVerify()
    {
        string source = """
            using SharpMeasures.Generators.Quantities;
            using SharpMeasures.Generators.Scalars;
            using SharpMeasures.Generators.Units;
            using SharpMeasures.Generators.Units.Utility;
            using SharpMeasures.Generators.Vectors;

            [ExcludeUnits("Metre")]
            [ResizedSharpMeasuresVector(typeof(Position3))]
            public partial class Position2 { }

            [IncludeUnits("Kilometre")]
            [SharpMeasuresVector(typeof(UnitOfLength))]
            public partial class Position3 { }

            [SharpMeasuresScalar(typeof(UnitOfLength))]
            public partial class Length { }
            
            [FixedUnit("Metre", "Metres")]
            [PrefixedUnit("Kilometre", "Kilometres", "Metre", MetricPrefixName.Kilo)]
            [SharpMeasuresUnit(typeof(Length))]
            public partial class UnitOfLength { }
            """;

        return AssertExactlyInclusionOrExclusionHadNoEffectDiagnosticsWithValidLocation(source).VerifyDiagnostics();
    }

    private static GeneratorVerifier AssertExactlyInclusionOrExclusionHadNoEffectDiagnosticsWithValidLocation(string source) => GeneratorVerifier.Construct<SharpMeasuresGenerator>(source).AssertExactlyListedDiagnosticsIDsReported(InclusionOrExclusionHadNoEffectDiagnostics).AssertAllDiagnosticsValidLocation();
    private static IReadOnlyCollection<string> InclusionOrExclusionHadNoEffectDiagnostics { get; } = new string[] { DiagnosticIDs.InclusionOrExclusionHadNoEffect };
}
