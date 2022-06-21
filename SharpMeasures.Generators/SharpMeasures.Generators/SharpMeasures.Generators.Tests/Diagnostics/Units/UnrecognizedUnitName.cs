namespace SharpMeasures.Generators.Tests.Diagnostics.Units;

using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Tests.Verify;

using System.Collections.Generic;
using System.Threading.Tasks;

using VerifyXunit;

using Xunit;

[UsesVerify]
public class UnrecognizedUnitName
{
    [Fact]
    public Task ScalarDefaultUnit_ExactListAndVerify()
    {
        string source = """
            using SharpMeasures.Generators.Scalars;
            using SharpMeasures.Generators.Units;

            [SharpMeasuresScalar(typeof(UnitOfLength), DefaultUnitName = "Metre", DefaultUnitSymbol = "m")]
            public partial class Length { }
            
            [SharpMeasuresUnit(typeof(Length))]
            public partial class UnitOfLength { }
            """;

        return AssertExactlyUnrecognizedUnitNameDiagnostics(source).VerifyDiagnostics();
    }
    
    [Fact]
    public Task ScalarConstant_ExactListAndVerify()
    {
        string source = """
            using SharpMeasures.Generators.Scalars;
            using SharpMeasures.Generators.Units;

            [SharpMeasuresScalar(typeof(UnitOfLength))]
            [ScalarConstant("Planck", "Metre", 1.616255E-35)]
            public partial class Length { }
            
            [SharpMeasuresUnit(typeof(Length))]
            public partial class UnitOfLength { }
            """;

        return AssertExactlyUnrecognizedUnitNameDiagnostics(source).VerifyDiagnostics();
    }

    [Fact]
    public Task UnitAlias_ExactListAndVerify()
    {
        string source = """
            using SharpMeasures.Generators.Scalars;
            using SharpMeasures.Generators.Units;

            [SharpMeasuresScalar(typeof(UnitOfLength))]
            public partial class Length { }

            [UnitAlias("Meter", "Meters", "Metre")]
            [SharpMeasuresUnit(typeof(Length))]
            public partial class UnitOfLength { }
            """;

        return AssertExactlyUnrecognizedUnitNameDiagnostics(source).VerifyDiagnostics();
    }

    [Fact]
    public Task BiasedUnit_ExactListAndVerify()
    {
        string source = """
            using SharpMeasures.Generators.Scalars;
            using SharpMeasures.Generators.Units;

            [SharpMeasuresScalar(typeof(UnitOfTemperature))]
            public partial class TemperatureDifference { }

            [BiasedUnit("Celsius", "Celsius", "Kelvin", -273.15)]
            [SharpMeasuresUnit(typeof(TemperatureDifference), BiasTerm = true)]
            public partial class UnitOfTemperature { }
            """;

        return AssertExactlyUnrecognizedUnitNameDiagnostics(source).VerifyDiagnostics();
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

            [SharpMeasuresUnit(typeof(Time))]
            public partial class UnitOfTime { }

            [DerivableUnit("1", "{0} / {1}", typeof(UnitOfLength), typeof(UnitOfTime))]
            [DerivedUnit("MetrePerSecond", "MetresPerSecond", new[] { "Metre", "Second" })]
            [SharpMeasuresUnit(typeof(Speed))]
            public partial class UnitOfSpeed { }
            """;

        return AssertExactlyUnrecognizedUnitNameDiagnostics(source).VerifyDiagnostics();
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

            [PrefixedUnit("Kilometre", "Kilometres", "Metre", MetricPrefixName.Kilo)]
            [SharpMeasuresUnit(typeof(Length))]
            public partial class UnitOfLength { }
            """;

        return AssertExactlyUnrecognizedUnitNameDiagnostics(source).VerifyDiagnostics();
    }

    [Fact]
    public Task ScaledUnit_ExactListAndVerify()
    {
        string source = """
            using SharpMeasures.Generators.Scalars;
            using SharpMeasures.Generators.Units;

            [SharpMeasuresScalar(typeof(UnitOfLength))]
            public partial class Length { }

            [ScaledUnit("Kilometre", "Kilometres", "Metre", 1000)]
            [SharpMeasuresUnit(typeof(Length))]
            public partial class UnitOfLength { }
            """;

        return AssertExactlyUnrecognizedUnitNameDiagnostics(source).VerifyDiagnostics();
    }

    [Fact]
    public Task VectorDefaultUnit_ExactListAndVerify()
    {
        string source = """
            using SharpMeasures.Generators.Scalars;
            using SharpMeasures.Generators.Units;
            using SharpMeasures.Generators.Vectors;

            [SharpMeasuresVector(typeof(UnitOfLength), DefaultUnitName = "Metre", DefaultUnitSymbol = "m")]
            public partial class Position3 { }

            [SharpMeasuresScalar(typeof(UnitOfLength))]
            public partial class Length { }
            
            [SharpMeasuresUnit(typeof(Length))]
            public partial class UnitOfLength { }
            """;

        return AssertExactlyUnrecognizedUnitNameDiagnostics(source).VerifyDiagnostics();
    }

    [Fact]
    public Task VectorConstant_ExactListAndVerify()
    {
        string source = """
            using SharpMeasures.Generators.Scalars;
            using SharpMeasures.Generators.Units;
            using SharpMeasures.Generators.Vectors;

            [VectorConstant("MetreOnes", "Metre", 1, 1, 1)]
            [SharpMeasuresVector(typeof(UnitOfLength))]
            public partial class Position3 { }

            [SharpMeasuresScalar(typeof(UnitOfLength))]
            public partial class Length { }
            
            [SharpMeasuresUnit(typeof(Length))]
            public partial class UnitOfLength { }
            """;

        return AssertExactlyUnrecognizedUnitNameDiagnostics(source).VerifyDiagnostics();
    }

    private static GeneratorVerifier AssertExactlyUnrecognizedUnitNameDiagnostics(string source)
        => GeneratorVerifier.Construct<SharpMeasuresGenerator>(source).AssertExactlyListedDiagnosticsIDsReported(UnrecognizedUnitNameDiagnostics);

    private static IReadOnlyCollection<string> UnrecognizedUnitNameDiagnostics { get; } = new string[] { DiagnosticIDs.UnrecognizedUnitName };
}
