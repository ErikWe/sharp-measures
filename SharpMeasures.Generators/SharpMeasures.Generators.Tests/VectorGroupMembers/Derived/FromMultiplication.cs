namespace SharpMeasures.Generators.Tests.VectorGroupMembers.Derived;

using SharpMeasures.Generators.Tests.Verify;

using System.Threading.Tasks;

using VerifyXunit;

using Xunit;

[UsesVerify]
public class FromMultiplication
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

        [SharpMeasuresVectorGroupMember(typeof(D))]
        public partial class D3 { }

        [DerivedQuantity("{0} * {1}", typeof(B{{dimension}}), typeof(C{{dimension}}))]
        [SharpMeasuresVectorGroup(typeof(Unit))]
        public static partial class D { }

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

        [SharpMeasuresVectorGroup(typeof(Unit))]
        public static partial class C { }

        [SharpMeasuresVectorGroupMember(typeof(D))]
        public partial class D3 { }
        
        [DerivedQuantity("{0} * {1}", typeof(B), typeof(C))]
        [SharpMeasuresVectorGroup(typeof(Unit))]
        public static partial class D { }

        [SharpMeasuresScalar(typeof(Unit))]
        public static partial class B { }

        [SharpMeasuresScalar(typeof(Unit))]
        public partial class A { }
        
        [SharpMeasuresUnit(typeof(A))]
        public partial class Unit { }
        """;
}
