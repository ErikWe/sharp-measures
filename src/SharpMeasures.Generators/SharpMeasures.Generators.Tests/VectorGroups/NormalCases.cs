namespace SharpMeasures.Generators.Tests.VectorGroups;

using SharpMeasures.Generators.Tests.Verify;

using System.Text.RegularExpressions;
using System.Threading.Tasks;

using VerifyXunit;

using Xunit;

[UsesVerify]
public class NormalCases
{
    [Fact]
    public Task Verify() => GeneratorVerifier.Construct<SharpMeasuresGenerator>(Text).AssertNoDiagnosticsReported().VerifyMatchingSourceNames(new Regex(@"^Position\.\S+\.g\.cs"));

    private static string Text => """
        using SharpMeasures.Generators;

        [VectorGroupMember(typeof(Position))]
        public partial class Position3 { }

        [VectorGroupMember(typeof(Position))]
        public partial class Position2 { }

        [VectorGroup(typeof(UnitOfLength))]
        public static partial class Position { }
        
        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }
        
        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;
}
