namespace SharpMeasures.Generators.Tests.Diagnostics;

using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Tests.Verify;

using System.Collections.Generic;
using System.Threading.Tasks;

using VerifyXunit;

using Xunit;

[UsesVerify]
public class InvalidDerivationSignature
{
    [Fact]
    public Task VerifyInvalidDerivationExpressionDiagnosticsMessage_Null()
    {
        string source = DerivableUnitText("(System.Type[])null");

        return AssertExactlyInvalidDerivationSignatureDiagnostics(source).VerifyDiagnostics();
    }

    [Theory]
    [MemberData(nameof(InvalidSignatures))]
    public void DerivableUnit_ExactList(string expression)
    {
        string source = DerivableUnitText(expression);

        AssertExactlyInvalidDerivationSignatureDiagnostics(source);
    }

    [Theory]
    [MemberData(nameof(InvalidSignatures))]
    public void DerivedQuantity_ExactList(string expression)
    {
        string source = DerivedQuantityText(expression);

        AssertExactlyInvalidDerivationSignatureDiagnostics(source);
    }

    private static IEnumerable<object[]> InvalidSignatures() => new object[][]
    {
        new[] { "(System.Type[])null" },
        new[] { "new System.Type[] { }" }
    };

    private static GeneratorVerifier AssertExactlyInvalidDerivationSignatureDiagnostics(string source) => GeneratorVerifier.Construct<SharpMeasuresGenerator>(source).AssertExactlyListedDiagnosticsIDsReported(InvalidDerivationSignatureDiagnostics);
    private static IReadOnlyCollection<string> InvalidDerivationSignatureDiagnostics { get; } = new string[] { DiagnosticIDs.InvalidDerivationSignature };

    private static string DerivableUnitText(string value) => $$"""
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;

        [DerivableUnit("{0} / {1}", {{value}})]
        [SharpMeasuresUnit(typeof(Speed))]
        public partial class UnitOfSpeed { }

        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }

        [SharpMeasuresUnit(typeof(Time))]
        public partial class UnitOfTime { }

        [SharpMeasuresScalar(typeof(UnitOfSpeed))]
        public partial class Speed { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }

        [SharpMeasuresScalar(typeof(UnitOfTime))]
        public partial class Time { }
        """;

    private static string DerivedQuantityText(string value) => $$"""
        using SharpMeasures.Generators.Quantities;
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;

        [DerivedQuantity("{0} / {1}", {{value}})]
        [SharpMeasuresScalar(typeof(UnitOfSpeed))]
        public partial class Speed { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }

        [SharpMeasuresScalar(typeof(UnitOfTime))]
        public partial class Time { }

        [SharpMeasuresUnit(typeof(Speed))]
        public partial class UnitOfSpeed { }

        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }

        [SharpMeasuresUnit(typeof(Time))]
        public partial class UnitOfTime { }
        """;
}
