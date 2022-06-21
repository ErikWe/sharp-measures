namespace SharpMeasures.Generators.Tests.Diagnostics.Units;

using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Tests.Verify;

using System.Collections.Generic;
using System.Threading.Tasks;

using VerifyXunit;

using Xunit;

[UsesVerify]
public class CyclicUnitDependency
{
    [Fact]
    public Task UnitAlias_Self_ExactListAndVerify()
    {
        string source = """
            using SharpMeasures.Generators.Scalars;
            using SharpMeasures.Generators.Units;

            [SharpMeasuresScalar(typeof(UnitOfLength))]
            public partial class Length { }
            
            [UnitAlias("Metre", "Metres", "Metre")]
            [SharpMeasuresUnit(typeof(Length))]
            public partial class UnitOfLength { }
            """;

        return AssertExactlyOneCyclicUnitDependencyDiagnostics(source).VerifyDiagnostics();
    }

    [Fact]
    public Task UnitAlias_Multiple_ExactListAndVerify()
    {
        string source = """
            using SharpMeasures.Generators.Scalars;
            using SharpMeasures.Generators.Units;

            [SharpMeasuresScalar(typeof(UnitOfLength))]
            public partial class Length { }
            
            [UnitAlias("Metre", "Metres", "Meter")]
            [UnitAlias("Meter", "Meters", "Metre")]
            [SharpMeasuresUnit(typeof(Length))]
            public partial class UnitOfLength { }
            """;

        return AssertExactlyTwoCyclicUnitDependencyDiagnostics(source).VerifyDiagnostics();
    }

    [Fact]
    public Task BiasedUnit_Self_ExactListAndVerify()
    {
        string source = """
            using SharpMeasures.Generators.Scalars;
            using SharpMeasures.Generators.Units;

            [SharpMeasuresScalar(typeof(UnitOfTemperature))]
            public partial class TemperatureDifference { }

            [BiasedUnit("Kelvin", "Kelvin", "Kelvin", 0)]
            [SharpMeasuresUnit(typeof(TemperatureDifference), BiasTerm = true)]
            public partial class UnitOfTemperature { }
            """;

        return AssertExactlyOneCyclicUnitDependencyDiagnostics(source).VerifyDiagnostics();
    }

    [Fact]
    public Task BiasedUnit_Multiple_ExactListAndVerify()
    {
        string source = """
            using SharpMeasures.Generators.Scalars;
            using SharpMeasures.Generators.Units;

            [SharpMeasuresScalar(typeof(UnitOfTemperature))]
            public partial class TemperatureDifference { }

            [BiasedUnit("Kelvin", "Kelvin", "Celsius", 273.15)]
            [BiasedUnit("Celsius", "Celsius", "Kelvin", -273.15)]
            [SharpMeasuresUnit(typeof(TemperatureDifference), BiasTerm = true)]
            public partial class UnitOfTemperature { }
            """;

        return AssertExactlyTwoCyclicUnitDependencyDiagnostics(source).VerifyDiagnostics();
    }

    [Fact]
    public Task PrefixedUnit_Self_ExactListAndVerify()
    {
        string source = """
            using SharpMeasures.Generators.Scalars;
            using SharpMeasures.Generators.Units;
            using SharpMeasures.Generators.Units.Utility;

            [SharpMeasuresScalar(typeof(UnitOfLength))]
            public partial class Length { }
            
            [PrefixedUnit("Metre", "Metres", "Metre", MetricPrefixName.Identity)]
            [SharpMeasuresUnit(typeof(Length))]
            public partial class UnitOfLength { }
            """;

        return AssertExactlyOneCyclicUnitDependencyDiagnostics(source).VerifyDiagnostics();
    }

    [Fact]
    public Task PrefixedUnit_Multiple_ExactListAndVerify()
    {
        string source = """
            using SharpMeasures.Generators.Scalars;
            using SharpMeasures.Generators.Units;
            using SharpMeasures.Generators.Units.Utility;

            [SharpMeasuresScalar(typeof(UnitOfLength))]
            public partial class Length { }
            
            [PrefixedUnit("Metre", "Metres", "Kilometre", MetricPrefixName.Milli)]
            [PrefixedUnit("Kilometre", "Kilometres", "Metre", MetricPrefixName.Kilo)]
            [SharpMeasuresUnit(typeof(Length))]
            public partial class UnitOfLength { }
            """;

        return AssertExactlyTwoCyclicUnitDependencyDiagnostics(source).VerifyDiagnostics();
    }

    [Fact]
    public Task ScaledUnit_Self_ExactListAndVerify()
    {
        string source = """
            using SharpMeasures.Generators.Scalars;
            using SharpMeasures.Generators.Units;

            [SharpMeasuresScalar(typeof(UnitOfLength))]
            public partial class Length { }
            
            [ScaledUnit("Metre", "Metres", "Metre", 1)]
            [SharpMeasuresUnit(typeof(Length))]
            public partial class UnitOfLength { }
            """;

        return AssertExactlyOneCyclicUnitDependencyDiagnostics(source).VerifyDiagnostics();
    }

    [Fact]
    public Task ScaledUnit_Multiple_ExactListAndVerify()
    {
        string source = """
            using SharpMeasures.Generators.Scalars;
            using SharpMeasures.Generators.Units;

            [SharpMeasuresScalar(typeof(UnitOfLength))]
            public partial class Length { }
            
            [ScaledUnit("Metre", "Metres", "Kilometre", 0.001)]
            [ScaledUnit("Kilometre", "Kilometres", "Metre", 1000)]
            [SharpMeasuresUnit(typeof(Length))]
            public partial class UnitOfLength { }
            """;

        return AssertExactlyTwoCyclicUnitDependencyDiagnostics(source).VerifyDiagnostics();
    }

    private static GeneratorVerifier AssertExactlyOneCyclicUnitDependencyDiagnostics(string source)
        => GeneratorVerifier.Construct<SharpMeasuresGenerator>(source).AssertExactlyListedDiagnosticsIDsReported(OneCyclicUnitDependencyDiagnostics);

    private static GeneratorVerifier AssertExactlyTwoCyclicUnitDependencyDiagnostics(string source)
        => GeneratorVerifier.Construct<SharpMeasuresGenerator>(source).AssertExactlyListedDiagnosticsIDsReported(TwoCyclicUnitDependencyDiagnostics);

    private static IReadOnlyCollection<string> OneCyclicUnitDependencyDiagnostics { get; } = new string[] { DiagnosticIDs.CyclicUnitDependency };
    private static IReadOnlyCollection<string> TwoCyclicUnitDependencyDiagnostics { get; } = new string[]
    {
        DiagnosticIDs.CyclicUnitDependency,
        DiagnosticIDs.CyclicUnitDependency
    };
}
