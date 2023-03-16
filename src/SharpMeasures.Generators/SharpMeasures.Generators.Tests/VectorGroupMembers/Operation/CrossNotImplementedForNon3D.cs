namespace SharpMeasures.Generators.Tests.VectorGroupMembers.Operation;

using SharpMeasures.Generators.Tests.Verify;

using VerifyXunit;

using Xunit;

[UsesVerify]
public class CrossNotImplementedForNon3D
{
    [Fact]
    public void Left() => Assert(LeftText);

    [Fact]
    public void Right() => Assert(RightText);

    private static GeneratorVerifier Assert(string text) => GeneratorVerifier.Construct<SharpMeasuresGenerator>(text).AssertAllListedSourceNamesGenerated("Position3.Operations.g.cs").AssertNoMatchingSourceNameGenerated("Position2.Operations.g.cs");

    private static string LeftText => """
        using SharpMeasures.Generators;

        [VectorGroupMember(typeof(Position))]
        public partial class Position2 { }

        [VectorGroupMember(typeof(Position))]
        public partial class Position3 { }

        [VectorOperation(typeof(Position), typeof(Position), VectorOperatorType.Cross, OperatorPosition.Left)]
        [VectorGroup(typeof(UnitOfLength))]
        public static partial class Position { }

        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }
        
        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static string RightText => """
        using SharpMeasures.Generators;

        [VectorGroupMember(typeof(Position))]
        public partial class Position2 { }

        [VectorGroupMember(typeof(Position))]
        public partial class Position3 { }

        [VectorOperation(typeof(Position), typeof(Position), VectorOperatorType.Cross, OperatorPosition.Right)]
        [VectorGroup(typeof(UnitOfLength))]
        public static partial class Position { }

        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }
        
        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;
}
