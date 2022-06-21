namespace SharpMeasures.Generators.Tests.Units.Derivable;

using SharpMeasures.Generators.Tests.Verify;

using VerifyXunit;

using Xunit;

[UsesVerify]
public class AbortedCases
{
    [Fact]
    public void NoMatchingConstructor_NoAdditionalSource()
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

            [SharpMeasuresUnit(typeof(Length))]
            public partial class UnitOfLength { }

            [SharpMeasuresUnit(typeof(Time))]
            public partial class UnitOfTime { }

            [DerivableUnit(7)]
            [SharpMeasuresUnit(typeof(Speed))]
            public partial class UnitOfSpeed { }
            """;

        GeneratorVerifier.Construct<SharpMeasuresGenerator>(source).AssertIdenticalSources(CommonResults.LengthTimeSpeed_NoDerivable);
    }

    [Fact]
    public void IDEmpty_NoAdditionalSource()
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

            [SharpMeasuresUnit(typeof(Length))]
            public partial class UnitOfLength { }

            [SharpMeasuresUnit(typeof(Time))]
            public partial class UnitOfTime { }

            [DerivableUnit("", "{0} / {1}", typeof(UnitOfLength), typeof(UnitOfTime))]
            [SharpMeasuresUnit(typeof(Speed))]
            public partial class UnitOfSpeed { }
            """;

        GeneratorVerifier.Construct<SharpMeasuresGenerator>(source).AssertIdenticalSources(CommonResults.LengthTimeSpeed_NoDerivable);
    }

    [Fact]
    public void IDNull_NoAdditionalSource()
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

            [SharpMeasuresUnit(typeof(Length))]
            public partial class UnitOfLength { }

            [SharpMeasuresUnit(typeof(Time))]
            public partial class UnitOfTime { }

            [DerivableUnit(null, "{0} / {1}", typeof(UnitOfLength), typeof(UnitOfTime))]
            [SharpMeasuresUnit(typeof(Speed))]
            public partial class UnitOfSpeed { }
            """;

        GeneratorVerifier.Construct<SharpMeasuresGenerator>(source).AssertIdenticalSources(CommonResults.LengthTimeSpeed_NoDerivable);
    }

    [Fact]
    public void ExpressionEmpty_NoAdditionalSource()
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

            [SharpMeasuresUnit(typeof(Length))]
            public partial class UnitOfLength { }

            [SharpMeasuresUnit(typeof(Time))]
            public partial class UnitOfTime { }

            [DerivableUnit("1", "", typeof(UnitOfLength), typeof(UnitOfTime))]
            [SharpMeasuresUnit(typeof(Speed))]
            public partial class UnitOfSpeed { }
            """;

        GeneratorVerifier.Construct<SharpMeasuresGenerator>(source).AssertIdenticalSources(CommonResults.LengthTimeSpeed_NoDerivable);
    }

    [Fact]
    public void ExpressionNull_NoAdditionalSource()
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

            [SharpMeasuresUnit(typeof(Length))]
            public partial class UnitOfLength { }

            [SharpMeasuresUnit(typeof(Time))]
            public partial class UnitOfTime { }

            [DerivableUnit("1", null, typeof(UnitOfLength), typeof(UnitOfTime))]
            [SharpMeasuresUnit(typeof(Speed))]
            public partial class UnitOfSpeed { }
            """;

        GeneratorVerifier.Construct<SharpMeasuresGenerator>(source).AssertIdenticalSources(CommonResults.LengthTimeSpeed_NoDerivable);
    }

    [Fact]
    public void SignatureTypeNotUnit_NoAdditionalSource()
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

            [SharpMeasuresUnit(typeof(Length))]
            public partial class UnitOfLength { }

            [SharpMeasuresUnit(typeof(Time))]
            public partial class UnitOfTime { }

            [DerivableUnit("1", "{0} / {1}", typeof(Length), typeof(UnitOfTime))]
            [SharpMeasuresUnit(typeof(Speed))]
            public partial class UnitOfSpeed { }
            """;

        GeneratorVerifier.Construct<SharpMeasuresGenerator>(source).AssertIdenticalSources(CommonResults.LengthTimeSpeed_NoDerivable);
    }

    [Fact]
    public void EmptySignature_NoAdditionalSource()
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

            [SharpMeasuresUnit(typeof(Length))]
            public partial class UnitOfLength { }

            [SharpMeasuresUnit(typeof(Time))]
            public partial class UnitOfTime { }

            [DerivableUnit("1", "{0} / {1}")]
            [SharpMeasuresUnit(typeof(Speed))]
            public partial class UnitOfSpeed { }
            """;

        GeneratorVerifier.Construct<SharpMeasuresGenerator>(source).AssertIdenticalSources(CommonResults.LengthTimeSpeed_NoDerivable);
    }

    [Fact]
    public void NullSignatureElement_NoAdditionalSource()
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

            [SharpMeasuresUnit(typeof(Length))]
            public partial class UnitOfLength { }

            [SharpMeasuresUnit(typeof(Time))]
            public partial class UnitOfTime { }

            [DerivableUnit("1", "{0} / {1}", null, typeof(UnitOfTime))]
            [SharpMeasuresUnit(typeof(Speed))]
            public partial class UnitOfSpeed { }
            """;

        GeneratorVerifier.Construct<SharpMeasuresGenerator>(source).AssertIdenticalSources(CommonResults.LengthTimeSpeed_NoDerivable);
    }
}
