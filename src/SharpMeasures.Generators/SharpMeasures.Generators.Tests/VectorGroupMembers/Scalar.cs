namespace SharpMeasures.Generators.Tests.VectorGroupMembers;

using SharpMeasures.Generators.Tests.Verify;

using System.Threading.Tasks;

using VerifyXunit;

using Xunit;

[UsesVerify]
public class Scalar
{
    [Fact]
    public Task Verify() => GeneratorVerifier.Construct<SharpMeasuresGenerator>(Text).VerifyMatchingSourceNames("Position2.Common.g.cs", "Position3.Common.g.cs");

    private static string Text => """
        using SharpMeasures.Generators;

        [VectorGroup(typeof(UnitOfLength), Scalar = typeof(Length))]
        public static partial class Position { }

        [VectorGroupMember(typeof(Position))]
        public partial class Position2 { }

        [VectorGroupMember(typeof(Position))]
        public partial class Position3 { }

        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }

        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;
}
