namespace SharpMeasures.Generators.Tests.Units.Derivable;

using SharpMeasures.Generators.Tests.Verify;

using VerifyXunit;

using Xunit;

[UsesVerify]
public class InvalidPermutationsValue
{
    [Fact]
    public void Truee() => AssertIdentical(TrueeText);

    private static GeneratorVerifier AssertIdentical(string source) => GeneratorVerifier.Construct<SharpMeasuresGenerator>(source, GeneratorVerifierSettings.GeneratedCodeAssertions).AssertIdenticalSources(Identical);

    private static string TrueeText => """
        using SharpMeasures.Generators;
            
        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }
            
        [ScalarQuantity(typeof(UnitOfTime))]
        public partial class Time { }
            
        [ScalarQuantity(typeof(UnitOfSpeed))]
        public partial class Speed { }
            
        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
            
        [Unit(typeof(Time))]
        public partial class UnitOfTime { }
            
        [DerivableUnit("{0} / {1}", typeof(UnitOfLength), typeof(UnitOfTime), Permutations = truee)]
        [Unit(typeof(Speed))]
        public partial class UnitOfSpeed { }
        """;

    private static GeneratorVerifier Identical { get; } = GeneratorVerifier.Construct<SharpMeasuresGenerator>(IdenticalText);

    private static string IdenticalText => """
        using SharpMeasures.Generators;
            
        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }
            
        [ScalarQuantity(typeof(UnitOfTime))]
        public partial class Time { }
            
        [ScalarQuantity(typeof(UnitOfSpeed))]
        public partial class Speed { }
            
        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
            
        [Unit(typeof(Time))]
        public partial class UnitOfTime { }
            
        [DerivableUnit("{0} / {1}", typeof(UnitOfLength), typeof(UnitOfTime))]
        [Unit(typeof(Speed))]
        public partial class UnitOfSpeed { }
        """;
}
