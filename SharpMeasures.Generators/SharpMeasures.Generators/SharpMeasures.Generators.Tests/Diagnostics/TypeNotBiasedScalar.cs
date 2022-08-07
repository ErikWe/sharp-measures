namespace SharpMeasures.Generators.Tests.Diagnostics;

using Microsoft.CodeAnalysis.Text;

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
    public Task ConvertibleScalar() => AssertAndVerifyConvertibleScalar();

    private static GeneratorVerifier AssertExactlyTypeNotBiasedScalarDiagnostics(string source) => GeneratorVerifier.Construct<SharpMeasuresGenerator>(source).AssertExactlyListedDiagnosticsIDsReported(TypeNotBiasedDiagnostics);
    private static IReadOnlyCollection<string> TypeNotBiasedDiagnostics { get; } = new string[] { DiagnosticIDs.TypeNotBiasedScalar };

    private static string ConvertibleScalarText => """
        using SharpMeasures.Generators.Quantities;
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;

        [ConvertibleQuantity(typeof(TemperatureDifference))]
        [SharpMeasuresScalar(typeof(UnitOfTemperature), UseUnitBias = true)]
        public partial class Temperature { }
            
        [SharpMeasuresScalar(typeof(UnitOfTemperature))]
        public partial class TemperatureDifference { }

        [SharpMeasuresUnit(typeof(TemperatureDifference), BiasTerm = true)]
        public partial class UnitOfTemperature { }
        """;

    private static TextSpan ConvertibleScalarLocation => ExpectedDiagnosticsLocation.TypeofArgumentTextSpan(ConvertibleScalarText, "typeof(TemperatureDifference)", prefix: "ConvertibleQuantity(");

    private static GeneratorVerifier AssertConvertibleScalar() => AssertExactlyTypeNotBiasedScalarDiagnostics(ConvertibleScalarText).AssertDiagnosticsLocation(ConvertibleScalarLocation, ConvertibleScalarText);
    private static Task AssertAndVerifyConvertibleScalar() => AssertConvertibleScalar().VerifyDiagnostics();
}
