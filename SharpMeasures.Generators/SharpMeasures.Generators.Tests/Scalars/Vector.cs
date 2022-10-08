namespace SharpMeasures.Generators.Tests.Scalars;

using SharpMeasures.Generators.Tests.Verify;

using System.Threading.Tasks;

using VerifyXunit;

using Xunit;

[UsesVerify]
public class Vector
{
    [Fact]
    public Task VerifyVector() => GeneratorVerifier.Construct<SharpMeasuresGenerator>(VectorText).AssertNoDiagnosticsReported().VerifyMatchingSourceNames("Length.Vectors.g.cs");

    [Fact]
    public Task VerifyGroup() => GeneratorVerifier.Construct<SharpMeasuresGenerator>(GroupText).AssertNoDiagnosticsReported().VerifyMatchingSourceNames("Length.Vectors.g.cs");

    [Fact]
    public void EmptyGroup() => GeneratorVerifier.Construct<SharpMeasuresGenerator>(EmptyGroupText).AssertNoListedSourceNameGenerated("Length.Vectors.g.cs");

    private static string VectorText => """
        using SharpMeasures.Generators;

        [VectorQuantity(typeof(UnitOfLength))]
        public partial class Position3 { }

        [ScalarQuantity(typeof(UnitOfLength), Vector = typeof(Position3))]
        public partial class Length { }

        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static string GroupText => """
        using SharpMeasures.Generators;

        [VectorGroup(typeof(UnitOfLength))]
        public static partial class Position { }

        [VectorGroupMember(typeof(Position))]
        public partial class Position3 { }

        [VectorGroupMember(typeof(Position))]
        public partial class Position2 { }

        [ScalarQuantity(typeof(UnitOfLength), Vector = typeof(Position))]
        public partial class Length { }

        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static string EmptyGroupText => """
        using SharpMeasures.Generators;
        
        [VectorGroup(typeof(UnitOfLength))]
        public static partial class Position { }
        
        [ScalarQuantity(typeof(UnitOfLength), Vector = typeof(Position))]
        public partial class Length { }
        
        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;
}
