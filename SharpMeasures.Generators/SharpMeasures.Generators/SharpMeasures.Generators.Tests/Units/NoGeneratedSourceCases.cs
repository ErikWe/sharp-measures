namespace SharpMeasures.Generators.Tests.Units;

using SharpMeasures.Generators.Tests.Verify;

using VerifyXunit;

using Xunit;

[UsesVerify]
public class NoGeneratedSourceCases
{
    [Fact]
    public void TypeNotPartial()
    {
        string source = """
            using SharpMeasures.Generators.Scalars;
            using SharpMeasures.Generators.Units;

            [SharpMeasuresScalar(typeof(UnitOfLength))]
            public partial class Length { }

            [SharpMeasuresUnit(typeof(Length))]
            public class UnitOfLength { }
            """;

        GeneratorVerifier.Construct<SharpMeasuresGenerator>(source).AssertSomeDiagnosticsReported().AssertNoSourceGenerated();
    }

    [Fact]
    public void InvalidTypeName()
    {
        string source = """
            using SharpMeasures.Generators.Scalars;
            using SharpMeasures.Generators.Units;

            [SharpMeasuresScalar(typeof(UnitOfLength))]
            public partial class Length { }

            [SharpMeasuresUnit(typeof(Length))]
            public partial class struct { }
            """;

        GeneratorVerifier.Construct<SharpMeasuresGenerator>(source).AssertSomeDiagnosticsReported().AssertNoSourceGenerated();
    }

    [Fact]
    public void QuantityNotScalar()
    {
        string source = """
            using SharpMeasures.Generators.Units;

            public partial class Length { }

            [SharpMeasuresUnit(typeof(Length))]
            public partial class UnitOfLength { }
            """;

        GeneratorVerifier.Construct<SharpMeasuresGenerator>(source).AssertSomeDiagnosticsReported().AssertNoSourceGenerated();
    }

    [Fact]
    public void QuantityNotScalar_Null()
    {
        string source = """
            using SharpMeasures.Generators.Units;

            [SharpMeasuresUnit(null)]
            public partial class UnitOfLength { }
            """;

        GeneratorVerifier.Construct<SharpMeasuresGenerator>(source).AssertSomeDiagnosticsReported().AssertNoSourceGenerated();
    }

    [Fact]
    public void QuantityNotUnbiased()
    {
        string source = """
            using SharpMeasures.Generators.Scalars;
            using SharpMeasures.Generators.Units;

            [SharpMeasuresScalar(typeof(UnitOfTemperature), UseUnitBias = true)]
            public partial class Temperature { }

            [SharpMeasuresUnit(typeof(Temperature), BiasTerm = false)]
            public partial class UnitOfTemperature { }
            """;

        GeneratorVerifier.Construct<SharpMeasuresGenerator>(source).AssertSomeDiagnosticsReported().AssertNoSourceGenerated();
    }
}
