namespace SharpMeasures.Generators.Tests.Scalars;

using SharpMeasures.Generators.Tests.Verify;

using System.Text.RegularExpressions;
using System.Threading.Tasks;

using VerifyXunit;

using Xunit;

[UsesVerify]
public class NormalCases
{
    [Fact]
    public Task UnbiasedScalar() => GeneratorVerifier.Construct<SharpMeasuresGenerator>(UnbiasedScalarText).AssertNoDiagnosticsReported().VerifyMatchingSourceNames(new Regex(@"^Length\.\S+\.g\.cs"));

    [Fact]
    public Task UnbiasedScalarWithDefault() => GeneratorVerifier.Construct<SharpMeasuresGenerator>(UnbiasedScalarWithDefaultText).AssertNoDiagnosticsReported().VerifyMatchingSourceNames("Length.Common.g.cs");

    [Fact]
    public Task BiasedScalar() => GeneratorVerifier.Construct<SharpMeasuresGenerator>(BiasedScalarText).AssertNoDiagnosticsReported().VerifyMatchingSourceNames(new Regex(@"^Temperature\.\S+\.g\.cs"));

    [Fact]
    public Task BiasedScalarWithDefault() => GeneratorVerifier.Construct<SharpMeasuresGenerator>(BiasedScalarWithDefaultText).AssertNoDiagnosticsReported().VerifyMatchingSourceNames("Temperature.Common.g.cs");

    private static string UnbiasedScalarText => """
        using SharpMeasures.Generators;
        
        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }
        
        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static string UnbiasedScalarWithDefaultText => """
        using SharpMeasures.Generators;
        
        [ScalarQuantity(typeof(UnitOfLength), DefaultUnitInstanceName = "Metre", DefaultUnitInstanceSymbol = "m")]
        public partial class Length { }
        
        [FixedUnitInstance("Metre", "Metres")]
        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static string BiasedScalarText => """
        using SharpMeasures.Generators;

        [ScalarQuantity(typeof(UnitOfTemperature), UseUnitBias = true)]
        public partial class Temperature { }
        
        [ScalarQuantity(typeof(UnitOfTemperature))]
        public partial class TemperatureDifference { }
        
        [Unit(typeof(TemperatureDifference), BiasTerm = true)]
        public partial class UnitOfTemperature { }
        """;

    private static string BiasedScalarWithDefaultText => """
        using SharpMeasures.Generators;

        [ScalarQuantity(typeof(UnitOfTemperature), UseUnitBias = true, DefaultUnitInstanceName = "Kelvin", DefaultUnitInstanceSymbol = "K")]
        public partial class Temperature { }
        
        [ScalarQuantity(typeof(UnitOfTemperature))]
        public partial class TemperatureDifference { }
        
        [FixedUnitInstance("Kelvin", "Kelvin")]
        [Unit(typeof(TemperatureDifference), BiasTerm = true)]
        public partial class UnitOfTemperature { }
        """;
}
