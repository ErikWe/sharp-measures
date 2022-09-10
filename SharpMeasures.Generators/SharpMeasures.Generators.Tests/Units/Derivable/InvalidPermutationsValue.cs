namespace SharpMeasures.Generators.Tests.Units.Derivable;

using SharpMeasures.Generators.Tests.Verify;

using VerifyXunit;

using Xunit;

[UsesVerify]
public class ÏnvalidPermutationsValue
{
    [Fact]
    public void Truee() => AssertIdentical(TrueeText);

    private static GeneratorVerifier AssertIdentical(string source) => GeneratorVerifier.Construct<SharpMeasuresGenerator>(source, new GeneratorVerifierSettings(true, false)).AssertIdenticalSources(Identical);

    private static string TrueeText => """
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
            
        [DerivableUnit("{0} / {1}", typeof(UnitOfLength), typeof(UnitOfTime), Permutations = truee)]
        [SharpMeasuresUnit(typeof(Speed))]
        public partial class UnitOfSpeed { }
        """;

    private static GeneratorVerifier Identical { get; } = GeneratorVerifier.Construct<SharpMeasuresGenerator>(IdenticalText);

    private static string IdenticalText => """
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
            
        [DerivableUnit("{0} / {1}", typeof(UnitOfLength), typeof(UnitOfTime))]
        [SharpMeasuresUnit(typeof(Speed))]
        public partial class UnitOfSpeed { }
        """;
}
