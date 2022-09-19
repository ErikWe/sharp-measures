namespace SharpMeasures.Generators.Tests.VectorGroupMembers.Derived;

using SharpMeasures.Generators.Tests.Verify;

using System.Threading.Tasks;

using VerifyXunit;

using Xunit;

[UsesVerify]
public class FromCrossProduct
{
    [Fact]
    public Task InvolvingVectors() => AssertInvolvingVectors().VerifyMatchingSourceNames("D3.Derivations.g.cs");

    [Fact]
    public Task InvolvingGroups() => AssertInvolvingGroups().VerifyMatchingSourceNames("D3.Derivations.g.cs");

    private static GeneratorVerifier AssertInvolvingVectors() => GeneratorVerifier.Construct<SharpMeasuresGenerator>(InvolvingVectorsText).AssertNoDiagnosticsReported();
    private static GeneratorVerifier AssertInvolvingGroups() => GeneratorVerifier.Construct<SharpMeasuresGenerator>(InvolvingGroupsText).AssertNoDiagnosticsReported();

    private static string InvolvingVectorsText => """
        using SharpMeasures.Generators.Quantities;
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Vectors;

        [SharpMeasuresVector(typeof(Unit))]
        public partial class C3 { }

        [SharpMeasuresVector(typeof(Unit))]
        public partial class B3 { }

        [SharpMeasuresVectorGroupMember(typeof(D))]
        public partial class D3 { }

        [DerivedQuantity("{0} x {1}", typeof(B3), typeof(C3))]
        [SharpMeasuresVectorGroup(typeof(Unit))]
        public static partial class D { }

        [SharpMeasuresScalar(typeof(Unit))]
        public partial class A { }
        
        [SharpMeasuresUnit(typeof(A))]
        public partial class Unit { }
        """;

    private static string InvolvingGroupsText => """
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
        
        [SharpMeasuresVectorGroupMember(typeof(D))]
        public partial class D3 { }

        [DerivedQuantity("{0} x {1}", typeof(B), typeof(C))]
        [SharpMeasuresVectorGroup(typeof(Unit))]
        public static partial class D { }

        [SharpMeasuresScalar(typeof(Unit))]
        public partial class A { }
        
        [SharpMeasuresUnit(typeof(A))]
        public partial class Unit { }
        """;
}
