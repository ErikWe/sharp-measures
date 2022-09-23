namespace SharpMeasures.Generators.Tests.Scalars.Derived;

using SharpMeasures.Generators.Tests.Verify;

using System.Threading.Tasks;

using VerifyXunit;

using Xunit;

[UsesVerify]
public class IncludingSameTypeTwice
{
    [Fact]
    public Task Verify() => GeneratorVerifier.Construct<SharpMeasuresGenerator>(Text).AssertNoDiagnosticsReported().VerifyMatchingSourceNames("Area.Derivations.g.cs");

    private static string Text => """
        using SharpMeasures;
        using SharpMeasures.Generators.Quantities;
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;

        [DerivedQuantity("{0} * {1}", typeof(Length), typeof(Length), OperatorImplementation = DerivationOperatorImplementation.All)]
        [SharpMeasuresScalar(typeof(UnitOfArea))]
        public partial class Area { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }
        
        [SharpMeasuresUnit(typeof(Area))]
        public partial class UnitOfArea { }

        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;
}
