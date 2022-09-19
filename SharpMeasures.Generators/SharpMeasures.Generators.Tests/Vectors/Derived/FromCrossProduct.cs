namespace SharpMeasures.Generators.Tests.Vectors.Derived;

using SharpMeasures.Generators.Tests.Verify;

using System.Threading.Tasks;

using VerifyXunit;

using Xunit;

[UsesVerify]
public class FromCrossProduct
{
    [Fact]
    public Task ThreeDimensional() => AssertNDimensional(3).VerifyMatchingSourceNames("D3.Derivations.g.cs");

    [Fact]
    public Task InvolvingGroup() => AssertInvolvingGroup().VerifyMatchingSourceNames("D3.Derivations.g.cs");

    [Fact]
    public void TwoDimensional() => AssertNDimensional(2);

    [Fact]
    public void FourDimensional() => AssertNDimensional(4);

    private static GeneratorVerifier AssertNDimensional(int dimension) => GeneratorVerifier.Construct<SharpMeasuresGenerator>(NDimensionalText(dimension)).AssertNoDiagnosticsReported();
    private static GeneratorVerifier AssertInvolvingGroup() => GeneratorVerifier.Construct<SharpMeasuresGenerator>(InvolvingGroupText).AssertNoDiagnosticsReported();

    private static string NDimensionalText(int dimension) => $$"""
        using SharpMeasures.Generators.Quantities;
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Vectors;

        [SharpMeasuresVector(typeof(Unit))]
        public partial class C{{dimension}} { }

        [SharpMeasuresVector(typeof(Unit))]
        public partial class B{{dimension}} { }

        [DerivedQuantity("{0} x {1}", typeof(B{{dimension}}), typeof(C{{dimension}}))]
        [SharpMeasuresVector(typeof(Unit))]
        public partial class D{{dimension}} { }

        [SharpMeasuresScalar(typeof(Unit))]
        public partial class A { }
        
        [SharpMeasuresUnit(typeof(A))]
        public partial class Unit { }
        """;

    private static string InvolvingGroupText => $$"""
        using SharpMeasures.Generators.Quantities;
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Vectors;
        
        [SharpMeasuresVectorGroupMember(typeof(C))]
        public partial class C3 { }

        [SharpMeasuresVectorGroupMember(typeof(C))]
        public partial class C2 { }

        [SharpMeasuresVectorGroupMember(typeof(B))]
        public partial class B4 { }
        
        [SharpMeasuresVectorGroupMember(typeof(B))]
        public partial class B3 { }
        
        [SharpMeasuresVectorGroupMember(typeof(B))]
        public partial class B2 { }

        [SharpMeasuresVectorGroup(typeof(Unit))]
        public static partial class C { }

        [SharpMeasuresVectorGroup(typeof(Unit))]
        public static partial class B { }
        
        [DerivedQuantity("{0} x {1}", typeof(B), typeof(C))]
        [SharpMeasuresVector(typeof(Unit))]
        public partial class D3 { }

        [SharpMeasuresScalar(typeof(Unit))]
        public partial class A { }
        
        [SharpMeasuresUnit(typeof(A))]
        public partial class Unit { }
        """;
}
