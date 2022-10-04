namespace SharpMeasures.Generators.Tests.Diagnostics;

using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Tests.Utility;
using SharpMeasures.Generators.Tests.Verify;

using System.Collections.Generic;
using System.Threading.Tasks;

using VerifyXunit;

using Xunit;

[UsesVerify]
public class TypeNotBiasedScalar
{
    [Fact]
    public Task ConvertibleScalar() => AssertConvertibleScalar().VerifyDiagnostics();

    private static GeneratorVerifier AssertExactlyTypeNotBiasedScalarDiagnostics(string source) => GeneratorVerifier.Construct<SharpMeasuresGenerator>(source).AssertExactlyListedDiagnosticsIDsReported(TypeNotBiasedDiagnostics);
    private static IReadOnlyCollection<string> TypeNotBiasedDiagnostics { get; } = new string[] { DiagnosticIDs.TypeNotBiasedScalar };

    private static string ConvertibleScalarText => """
        using SharpMeasures.Generators;

        [ConvertibleQuantity(typeof(TemperatureDifference))]
        [ScalarQuantity(typeof(UnitOfTemperature), UseUnitBias = true)]
        public partial class Temperature { }
            
        [ScalarQuantity(typeof(UnitOfTemperature))]
        public partial class TemperatureDifference { }

        [Unit(typeof(TemperatureDifference), BiasTerm = true)]
        public partial class UnitOfTemperature { }
        """;

    private static GeneratorVerifier AssertConvertibleScalar()
    {
        var expectedLocation = ExpectedDiagnosticsLocation.FromTypeofArgumentTextSpan(ConvertibleScalarText, target: "typeof(TemperatureDifference)", prefix: "ConvertibleQuantity(");

        return AssertExactlyTypeNotBiasedScalarDiagnostics(ConvertibleScalarText).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(ConvertibleScalarIdentical);
    }

    private static GeneratorVerifier ConvertibleScalarIdentical => GeneratorVerifier.Construct<SharpMeasuresGenerator>(ConvertibleScalarIdenticalText);

    private static string ConvertibleScalarIdenticalText => """
        using SharpMeasures.Generators;

        [ScalarQuantity(typeof(UnitOfTemperature), UseUnitBias = true)]
        public partial class Temperature { }
            
        [ScalarQuantity(typeof(UnitOfTemperature))]
        public partial class TemperatureDifference { }

        [Unit(typeof(TemperatureDifference), BiasTerm = true)]
        public partial class UnitOfTemperature { }
        """;
}
