namespace SharpMeasures.Generators.Tests.VectorGroupMembers;

using SharpMeasures.Generators.Tests.Verify;

using VerifyXunit;

using Xunit;

[UsesVerify]
public class DiscardedDifference
{
    [Fact]
    public void Verify() => GeneratorVerifier.Construct<SharpMeasuresGenerator>(Text, GeneratorVerifierSettings.NoAssertions);

    private static string Text => """
        using SharpMeasures.Generators;
        
        [VectorGroup(typeof(UnitOfLength), Difference = typeof(Displacement))]
        public static partial class Position { }

        [VectorGroupMember(typeof(Position))]
        public partial class Position3 { }

        [VectorGroup(typeof(UnitOfDistance))]
        public static partial class Displacement { }
        
        [VectorGroupMember(typeof(Displacement))]
        public partial class Displacement3 { }
        
        public partial class UnitOfDistance { }

        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;
}
