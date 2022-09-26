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
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Scalars;
        
        [SharpMeasuresScalar(typeof(UnitOfA))]
        public partial class A { }
        
        [SharpMeasuresScalar(typeof(UnitOfB)]
        public partial class B { }

        [SharpMeasuresVector(typeof(UnitOfB), DefaultUnitInstanceName = "e", DefaultUnitInstanceSymbol = "f")]
        public partial class C3 { }
        
        [FixedUnitInstance("g", "[*]")]
        [SharpMeasuresUnit(typeof(A))]
        public partial class UnitOfA { }
        
        [DerivedUnitInstance("e", "[*]", new[] { "d" })]
        [DerivableUnit("1 / {0}", typeof(UnitOfA))]
        [SharpMeasuresUnit(typeof(B))]
        public partial class UnitOfB { }
        """;

    private static string VectorGroupText => """
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Scalars;
        
        [SharpMeasuresScalar(typeof(UnitOfA))]
        public partial class A { }
        
        [SharpMeasuresScalar(typeof(UnitOfB)]
        public partial class B { }

        [SharpMeasuresVectorGroup(typeof(UnitOfB), DefaultUnitInstanceName = "e", DefaultUnitInstanceSymbol = "f")]
        public static partial class C { }
        
        [SharpMeasuresVectorGroupMember(typeof(C))]
        public partial class C3 { }

        [FixedUnitInstance("g", "[*]")]
        [SharpMeasuresUnit(typeof(A))]
        public partial class UnitOfA { }
        
        [DerivedUnitInstance("e", "[*]", new[] { "d" })]
        [DerivableUnit("1 / {0}", typeof(UnitOfA))]
        [SharpMeasuresUnit(typeof(B))]
        public partial class UnitOfB { }
        """;
}
