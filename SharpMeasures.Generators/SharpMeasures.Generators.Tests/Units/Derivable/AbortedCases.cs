namespace SharpMeasures.Generators.Tests.Units.Derivable;

using SharpMeasures.Generators.Tests.Verify;

using VerifyXunit;

using Xunit;

[UsesVerify]
public class AbortedCases
{
    [Fact]
    public void EmptyDerivationID() => AssertUnbiased(derivationID: "\"\"");

    [Fact]
    public void NullDerivationID() => AssertUnbiased(derivationID: "null");

    [Fact]
    public void DuplicateDerivationID() => AssertUnbiased(derivationID: "\"Length / Time\"");

    [Fact]
    public void EmptyExpression() => AssertUnbiased(expression: "\"\"");

    [Fact]
    public void NullExpression() => AssertUnbiased(expression: "(string)null");

    [Fact]
    public void EmptySignature() => AssertUnbiased(signature: string.Empty);

    [Fact]
    public void NullSignatureElement() => AssertUnbiased(signature: ", null, typeof(UnitOfTime)");

    [Fact]
    public void NonUnitSignatureElement() => AssertUnbiased(signature: ", typeof(Length), typeof(UnitOfTime)");

    [Fact]
    public void BiasedUnit() => AssertBiased();

    private static string UnbiasedText(string derivationID, string expression, string signature) => $$"""
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }

        [SharpMeasuresScalar(typeof(UnitOfTime))]
        public partial class Time { }

        [SharpMeasuresScalar(typeof(UnitOfSpeed))]
        public partial class Speed { }

        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }

        [SharpMeasuresUnit(typeof(Time))]
        public partial class UnitOfTime { }

        [DerivableUnit("Length / Time", "{0} / {1}", typeof(UnitOfLength), typeof(UnitOfTime))]
        [DerivableUnit({{derivationID}}, {{expression}}{{signature}})]
        [SharpMeasuresUnit(typeof(Speed))]
        public partial class UnitOfSpeed { }
        """;

    private static GeneratorVerifier AssertUnbiased(string derivationID = "\"1\"", string expression = "\"{0} / {1}\"", string signature = ", typeof(UnitOfLength), typeof(UnitOfTime)")
    {
        string source = UnbiasedText(derivationID, expression, signature);

        return GeneratorVerifier.Construct<SharpMeasuresGenerator>(source).AssertSomeDiagnosticsReported().AssertIdenticalSources(CommonResults.LengthTimeSpeed_LengthOverTimeDerivation);
    }

    private static string BiasedText => """
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }

        [SharpMeasuresScalar(typeof(UnitOfTime))]
        public partial class Time { }

        [SharpMeasuresScalar(typeof(UnitOfTemperature))]
        public partial class TemperatureDifference { }

        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }

        [SharpMeasuresUnit(typeof(Time))]
        public partial class UnitOfTime { }

        [DerivableUnit("Length / Time", "{0} / {1}", typeof(UnitOfLength), typeof(UnitOfTime))]
        [SharpMeasuresUnit(typeof(TemperatureDifference), BiasTerm = true)]
        public partial class UnitOfTemperature { }
        """;

    private static GeneratorVerifier AssertBiased()
    {
        return GeneratorVerifier.Construct<SharpMeasuresGenerator>(BiasedText).AssertSomeDiagnosticsReported().AssertNoMatchingSourceNameGenerated("UnitOfTemperature_Derivable.g.cs");
    }
}
