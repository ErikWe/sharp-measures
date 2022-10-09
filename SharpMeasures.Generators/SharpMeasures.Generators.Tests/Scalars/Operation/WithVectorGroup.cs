namespace SharpMeasures.Generators.Tests.Scalars.Operation;

using SharpMeasures.Generators.Tests.Verify;

using System.Threading.Tasks;

using VerifyXunit;

using Xunit;

[UsesVerify]
public class WithVectorGroup
{
    [Fact]
    public Task Verify() => GeneratorVerifier.Construct<SharpMeasuresGenerator>(Text).AssertNoDiagnosticsReported().VerifyMatchingSourceNames("Time.Operations.g.cs");

    private static string Text => """
        using SharpMeasures.Generators;

        [VectorGroup(typeof(UnitOfSpeed))]
        public static partial class VelocityN { }

        [VectorGroupMember(typeof(VelocityN))]
        public partial class Velocity2 { }

        [VectorGroupMember(typeof(VelocityN))]
        public partial class Velocity3 { }

        [VectorGroup(typeof(UnitOfLength))]
        public static partial class DisplacementN { }
        
        [VectorGroupMember(typeof(DisplacementN))]
        public partial class Displacement2 { }
        
        [VectorGroupMember(typeof(DisplacementN))]
        public partial class Displacement3 { }

        [ScalarQuantity(typeof(UnitOfSpeed))]
        public partial class Speed { }
        
        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }
        
        [QuantityOperation(typeof(VelocityN), typeof(DisplacementN), OperatorType.Division, OperatorPosition.Right)]
        [ScalarQuantity(typeof(UnitOfTime))]
        public partial class Time { }

        [Unit(typeof(Speed))]
        public partial class UnitOfSpeed { }

        [Unit(typeof(Length))]
        public partial class UnitOfLength { }

        [Unit(typeof(Time))]
        public partial class UnitOfTime { }
        """;
}
