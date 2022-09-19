namespace SharpMeasures.Generators.Tests.Scalars;

using SharpMeasures.Generators.Tests.Verify;

using System.Threading.Tasks;

using VerifyXunit;

using Xunit;

[UsesVerify]
public class NormalCases
{
    [Fact]
    public Task UnbiasedScalar() => GeneratorVerifier.Construct<SharpMeasuresGenerator>(UnbiasedScalarText).AssertNoDiagnosticsReported().VerifyMatchingSourceNames(@"^Length.\S+\.g\.cs");

    [Fact]
    public Task UnbiasedScalarWithDefault() => GeneratorVerifier.Construct<SharpMeasuresGenerator>(UnbiasedScalarWithDefaultText).AssertNoDiagnosticsReported().VerifyMatchingSourceNames(@"^Length.Common.g.cs");

    [Fact]
    public Task BiasedScalar() => GeneratorVerifier.Construct<SharpMeasuresGenerator>(BiasedScalarText).AssertNoDiagnosticsReported().VerifyMatchingSourceNames(@"^Temperature.\S+\.g\.cs");

    [Fact]
    public Task BiasedScalarWithDefault() => GeneratorVerifier.Construct<SharpMeasuresGenerator>(BiasedScalarWithDefaultText).AssertNoDiagnosticsReported().VerifyMatchingSourceNames(@"^Temperature.Common.g.cs");

    private static string UnbiasedScalarText => """
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        
        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }
        
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static string UnbiasedScalarWithDefaultText => """
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        
        [SharpMeasuresScalar(typeof(UnitOfLength), DefaultUnitInstanceName = "Metre", DefaultUnitInstanceSymbol = "m")]
        public partial class Length { }
        
        [FixedUnitInstance("Metre", "Metres")]
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static string BiasedScalarText => """
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;

        [SharpMeasuresScalar(typeof(UnitOfTemperature), UseUnitBias = true)]
        public partial class Temperature { }
        
        [SharpMeasuresScalar(typeof(UnitOfTemperature))]
        public partial class TemperatureDifference { }
        
        [SharpMeasuresUnit(typeof(TemperatureDifference), BiasTerm = true)]
        public partial class UnitOfTemperature { }
        """;

    private static string BiasedScalarWithDefaultText => """
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;

        [SharpMeasuresScalar(typeof(UnitOfTemperature), UseUnitBias = true, DefaultUnitInstanceName = "Kelvin", DefaultUnitInstanceSymbol = "K")]
        public partial class Temperature { }
        
        [SharpMeasuresScalar(typeof(UnitOfTemperature))]
        public partial class TemperatureDifference { }
        
        [FixedUnitInstance("Kelvin", "Kelvin")]
        [SharpMeasuresUnit(typeof(TemperatureDifference), BiasTerm = true)]
        public partial class UnitOfTemperature { }
        """;
}
