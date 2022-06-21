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

            [FixedUnit("Metre", "Metres", 1)]
            [UnitAlias("Meter1", "Meters", "Metre")]
            [UnitAlias("Meter2", "Meters", "Metre")]
            [SharpMeasuresUnit(typeof(Length))]
            public partial class UnitOfLength { }
            """;

        return AssertExactlyDuplicateUnitPluralFormDiagnostics(source).VerifyDiagnostics();
    }

    [Fact]
    public Task BiasedUnit_ExactListAndVerify()
    {
        string source = """
            using SharpMeasures.Generators.Scalars;
            using SharpMeasures.Generators.Units;
            using SharpMeasures.Generators.Units.Utility;

            [SharpMeasuresScalar(typeof(UnitOfTemperature))]
            public partial class TemperatureDifference { }

            [FixedUnit("Kelvin", "Kelvin", 1, Bias = 0)]
            [BiasedUnit("Celsius1", "Celsius", "Kelvin", -273.15)]
            [BiasedUnit("Celsius2", "Celsius", "Kelvin", -273.15)]
            [SharpMeasuresUnit(typeof(TemperatureDifference), BiasTerm = true)]
            public partial class UnitOfTemperature { }
            """;

        return AssertExactlyDuplicateUnitPluralFormDiagnostics(source).VerifyDiagnostics();
    }

    [Fact]
    public Task DerivedUnit_ExactListAndVerify()
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
            [DerivedUnit("MetrePerSecond1", "MetresPerSecond", new[] { "Metre", "Second" })]
            [DerivedUnit("MetrePerSecond2", "MetresPerSecond", new[] { "Metre", "Second" })]
            [SharpMeasuresUnit(typeof(Speed))]
            public partial class UnitOfSpeed { }
            """;

        return AssertExactlyDuplicateUnitPluralFormDiagnostics(source).VerifyDiagnostics();
    }

    [Fact]
    public Task FixedUnit_ExactListAndVerify()
    {
        string source = """
            using SharpMeasures.Generators.Scalars;
            using SharpMeasures.Generators.Units;

            [SharpMeasuresScalar(typeof(UnitOfLength))]
            public partial class Length { }

            [FixedUnit("Metre1", "Metres", 1)]
            [FixedUnit("Metre2", "Metres", 1)]
            [SharpMeasuresUnit(typeof(Length))]
            public partial class UnitOfLength { }
            """;

        return AssertExactlyDuplicateUnitPluralFormDiagnostics(source).VerifyDiagnostics();
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

            [FixedUnit("Metre", "Metres", 1)]
            [PrefixedUnit("Kilometre1", "Kilometres", "Metre", MetricPrefixName.Kilo)]
            [PrefixedUnit("Kilometre2", "Kilometres", "Metre", MetricPrefixName.Kilo)]
            [SharpMeasuresUnit(typeof(Length))]
            public partial class UnitOfLength { }
            """;

        return AssertExactlyDuplicateUnitPluralFormDiagnostics(source).VerifyDiagnostics();
    }

    [Fact]
    public Task ScaledUnit_ExactListAndVerify()
    {
        string source = """
            using SharpMeasures.Generators.Scalars;
            using SharpMeasures.Generators.Units;

            [SharpMeasuresScalar(typeof(UnitOfLength))]
            public partial class Length { }

            [FixedUnit("Metre", "Metres", 1)]
            [ScaledUnit("Kilometre1", "Kilometres", "Metre", 1000)]
            [ScaledUnit("Kilometre2", "Kilometres", "Metre", 1000)]
            [SharpMeasuresUnit(typeof(Length))]
            public partial class UnitOfLength { }
            """;

        return AssertExactlyDuplicateUnitPluralFormDiagnostics(source).VerifyDiagnostics();
    }

    private static GeneratorVerifier AssertExactlyDuplicateUnitPluralFormDiagnostics(string source)
        => GeneratorVerifier.Construct<SharpMeasuresGenerator>(source).AssertExactlyListedDiagnosticsIDsReported(DuplicateUnitFormDiagnostics);

    private static IReadOnlyCollection<string> DuplicateUnitFormDiagnostics { get; } = new string[] { DiagnosticIDs.DuplicateUnitPluralForm };
}
