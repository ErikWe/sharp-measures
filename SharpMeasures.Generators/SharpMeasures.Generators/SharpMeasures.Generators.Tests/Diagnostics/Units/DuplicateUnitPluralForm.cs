namespace SharpMeasures.Generators.Tests.Diagnostics.Units;

using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Tests.Verify;

using System.Collections.Generic;
using System.Threading.Tasks;

using VerifyXunit;

using Xunit;

[UsesVerify]
public class DuplicateUnitPluralForm
{
    [Fact]
    public Task UnitAlias_ExactListAndVerify()
    {
        string source = """
            using SharpMeasures.Generators.Scalars;
            using SharpMeasures.Generators.Units;

            [SharpMeasuresScalar(typeof(UnitOfLength))]
            public partial class Length { }

            [FixedUnit("Metre", "Metres")]
            [UnitAlias("Meter1", "Meters", "Metre")]
            [UnitAlias("Meter2", "Meters", "Metre")]
            [SharpMeasuresUnit(typeof(Length))]
            public partial class UnitOfLength { }
            """;

        return AssertExactlyDuplicateUnitPluralFormDiagnosticsWithValidLocation(source).VerifyDiagnostics();
    }

    [Fact]
    public void BiasedUnit_ExactList()
    {
        string source = """
            using SharpMeasures.Generators.Scalars;
            using SharpMeasures.Generators.Units;
            using SharpMeasures.Generators.Units.Utility;

            [SharpMeasuresScalar(typeof(UnitOfTemperature))]
            public partial class TemperatureDifference { }

            [FixedUnit("Kelvin", "Kelvin")]
            [BiasedUnit("Celsius1", "Celsius", "Kelvin", -273.15)]
            [BiasedUnit("Celsius2", "Celsius", "Kelvin", -273.15)]
            [SharpMeasuresUnit(typeof(TemperatureDifference), BiasTerm = true)]
            public partial class UnitOfTemperature { }
            """;

        AssertExactlyDuplicateUnitPluralFormDiagnosticsWithValidLocation(source);
    }

    [Fact]
    public void DerivedUnit_ExactList()
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

            [FixedUnit("Metre", "Metres")]
            [SharpMeasuresUnit(typeof(Length))]
            public partial class UnitOfLength { }

            [FixedUnit("Second", "Seconds")]
            [SharpMeasuresUnit(typeof(Time))]
            public partial class UnitOfTime { }

            [DerivableUnit("{0} / {1}", typeof(UnitOfLength), typeof(UnitOfTime))]
            [DerivedUnit("MetrePerSecond1", "MetresPerSecond", new[] { "Metre", "Second" })]
            [DerivedUnit("MetrePerSecond2", "MetresPerSecond", new[] { "Metre", "Second" })]
            [SharpMeasuresUnit(typeof(Speed))]
            public partial class UnitOfSpeed { }
            """;

        AssertExactlyDuplicateUnitPluralFormDiagnosticsWithValidLocation(source);
    }

    [Fact]
    public Task PrefixedUnit_ExactListAndVerify()
    {
        string source = """
            using SharpMeasures.Generators.Scalars;
            using SharpMeasures.Generators.Units;
            using SharpMeasures.Generators.Units.Utility;

            [SharpMeasuresScalar(typeof(UnitOfLength))]
            public partial class Length { }

            [FixedUnit("Metre", "Metres")]
            [PrefixedUnit("Kilometre1", "Kilometres", "Metre", MetricPrefixName.Kilo)]
            [PrefixedUnit("Kilometre2", "Kilometres", "Metre", MetricPrefixName.Kilo)]
            [SharpMeasuresUnit(typeof(Length))]
            public partial class UnitOfLength { }
            """;

        return AssertExactlyDuplicateUnitPluralFormDiagnosticsWithValidLocation(source).VerifyDiagnostics();
    }

    [Fact]
    public Task ScaledUnit_ExactListAndVerify()
    {
        string source = """
            using SharpMeasures.Generators.Scalars;
            using SharpMeasures.Generators.Units;

            [SharpMeasuresScalar(typeof(UnitOfLength))]
            public partial class Length { }

            [FixedUnit("Metre", "Metres")]
            [ScaledUnit("Kilometre1", "Kilometres", "Metre", 1000)]
            [ScaledUnit("Kilometre2", "Kilometres", "Metre", 1000)]
            [SharpMeasuresUnit(typeof(Length))]
            public partial class UnitOfLength { }
            """;

        return AssertExactlyDuplicateUnitPluralFormDiagnosticsWithValidLocation(source).VerifyDiagnostics();
    }

    private static GeneratorVerifier AssertExactlyDuplicateUnitPluralFormDiagnosticsWithValidLocation(string source) => GeneratorVerifier.Construct<SharpMeasuresGenerator>(source).AssertExactlyListedDiagnosticsIDsReported(DuplicateUnitFormDiagnostics).AssertAllDiagnosticsValidLocation();
    private static IReadOnlyCollection<string> DuplicateUnitFormDiagnostics { get; } = new string[] { DiagnosticIDs.DuplicateUnitPluralForm };
}
