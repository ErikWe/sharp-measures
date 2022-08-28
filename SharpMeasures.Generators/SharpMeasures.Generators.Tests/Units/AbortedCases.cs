namespace SharpMeasures.Generators.Tests.Units;

using SharpMeasures.Generators.Tests.Verify;

using VerifyXunit;

using Xunit;

[UsesVerify]
public class AbortedCases
{
    [Fact]
    public void TypeNotPartial() => GeneratorVerifier.Construct<SharpMeasuresGenerator>(TypeNotPartialText).AssertSomeDiagnosticsReported().AssertNoSourceGenerated();

    [Fact]
    public void QuantityNotScalar() => GeneratorVerifier.Construct<SharpMeasuresGenerator>(QuantityNotScalarText).AssertSomeDiagnosticsReported().AssertNoSourceGenerated();

    [Fact]
    public void QuantityNotUnbiased() => GeneratorVerifier.Construct<SharpMeasuresGenerator>(QuantityNotUnbiasedText).AssertSomeDiagnosticsReported().AssertNoSourceGenerated();

    private static string TypeNotPartialText => """
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        
        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }
        
        [SharpMeasuresUnit(typeof(Length))]
        public class UnitOfLength { }
        """;

    private static string QuantityNotScalarText => """
        using SharpMeasures.Generators.Units;

        public partial class Length { }

        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static string QuantityNotUnbiasedText => """
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;

        [SharpMeasuresScalar(typeof(UnitOfTemperature), UseUnitBias = true)]
        public partial class Temperature { }

        [SharpMeasuresUnit(typeof(Temperature), BiasTerm = false)]
        public partial class UnitOfTemperature { }
        """;
}
