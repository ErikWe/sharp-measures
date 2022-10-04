namespace SharpMeasures.Generators.Tests.Vectors;

using SharpMeasures.Generators.Tests.Verify;

using VerifyXunit;

using Xunit;

[UsesVerify]
public class DiscardedUnitInstanceAsDefault
{
    [Fact]
    public void Vector() => Assert(VectorText);

    [Fact]
    public void VectorGroup() => Assert(VectorGroupText);

    private static GeneratorVerifier Assert(string source) => GeneratorVerifier.Construct<SharpMeasuresGenerator>(source, GeneratorVerifierSettings.NoAssertions);

    private static string VectorText => """
        using SharpMeasures.Generators;
        
        [ScalarQuantity(typeof(UnitOfA))]
        public partial class A { }
        
        [ScalarQuantity(typeof(UnitOfB)]
        public partial class B { }

        [VectorQuantity(typeof(UnitOfB), DefaultUnitInstanceName = "e", DefaultUnitInstanceSymbol = "f")]
        public partial class C3 { }
        
        [FixedUnitInstance("g", "[*]")]
        [Unit(typeof(A))]
        public partial class UnitOfA { }
        
        [DerivedUnitInstance("e", "[*]", new[] { "d" })]
        [DerivableUnit("1 / {0}", typeof(UnitOfA))]
        [Unit(typeof(B))]
        public partial class UnitOfB { }
        """;

    private static string VectorGroupText => """
        using SharpMeasures.Generators;
        
        [ScalarQuantity(typeof(UnitOfA))]
        public partial class A { }
        
        [ScalarQuantity(typeof(UnitOfB)]
        public partial class B { }

        [VectorGroup(typeof(UnitOfB), DefaultUnitInstanceName = "e", DefaultUnitInstanceSymbol = "f")]
        public static partial class C { }
        
        [VectorGroupMember(typeof(C))]
        public partial class C3 { }

        [FixedUnitInstance("g", "[*]")]
        [Unit(typeof(A))]
        public partial class UnitOfA { }
        
        [DerivedUnitInstance("e", "[*]", new[] { "d" })]
        [DerivableUnit("1 / {0}", typeof(UnitOfA))]
        [Unit(typeof(B))]
        public partial class UnitOfB { }
        """;
}
