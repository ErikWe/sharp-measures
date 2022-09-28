namespace SharpMeasures.Generators.Tests.Scalars.Process;

using SharpMeasures.Generators.Tests.Verify;

using System.Threading.Tasks;

using VerifyXunit;

using Xunit;

[UsesVerify]
public class AsMethod
{
    [Fact]
    public Task WithoutParameters() => GeneratorVerifier.Construct<SharpMeasuresGenerator>(WithoutParametersText).VerifyMatchingSourceNames("Length.Processes.g.cs");

    [Fact]
    public Task WithParameters() => GeneratorVerifier.Construct<SharpMeasuresGenerator>(WithParametersText).VerifyMatchingSourceNames("Length.Processes.g.cs");

    private static string WithoutParametersText => """
        using SharpMeasures.Generators.Quantities;
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;

        [ProcessedQuantity("Double", "new(2 * Magnitude)")]
        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }
        
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static string WithParametersText => """
        using SharpMeasures.Generators.Quantities;
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;

        [ProcessedQuantity("Scale", "new(Magnitude * x)", new[] { typeof(double) }, new[] { "x" })]
        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }
        
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;
}
