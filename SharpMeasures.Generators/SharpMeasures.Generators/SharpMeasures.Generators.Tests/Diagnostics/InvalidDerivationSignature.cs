namespace SharpMeasures.Generators.Tests.Diagnostics;

using Microsoft.CodeAnalysis.Text;

using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Tests.Utility;
using SharpMeasures.Generators.Tests.Verify;

using System.Collections.Generic;
using System.Threading.Tasks;

using VerifyXunit;

using Xunit;

[UsesVerify]
public class InvalidDerivationSignature
{
    [Fact]
    public Task VerifyInvalidDerivationExpressionDiagnosticsMessage_Null() => AssertAndVerifyDerivableUnit(NullSubtext);

    [Theory]
    [MemberData(nameof(InvalidSignatures))]
    public void DerivableUnit(SourceSubtext signature) => AssertDerivableUnit(signature);

    [Theory]
    [MemberData(nameof(InvalidSignatures))]
    public void DerivedQuantity(SourceSubtext signature) => AssertDerivedQuantity(signature);

    private static IEnumerable<object[]> InvalidSignatures() => new object[][]
    {
        new object[] { NullSubtext },
        new object[] { EmptySubtext }
    };

    private static SourceSubtext NullSubtext { get; } = new("null", "(System.Type[])");
    private static SourceSubtext EmptySubtext { get; } = new("System.Type[0]", "new ");

    private static GeneratorVerifier AssertExactlyInvalidDerivationSignatureDiagnosticsWithValidLocation(string source) => GeneratorVerifier.Construct<SharpMeasuresGenerator>(source).AssertExactlyListedDiagnosticsIDsReported(InvalidDerivationSignatureDiagnostics).AssertAllDiagnosticsValidLocation();
    private static IReadOnlyCollection<string> InvalidDerivationSignatureDiagnostics { get; } = new string[] { DiagnosticIDs.InvalidDerivationSignature };

    private static string DerivableUnitText(SourceSubtext signature) => $$"""
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;

        [DerivableUnit("{0} / {1}", {{signature}})]
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

    private static TextSpan DerivableUnitLocation(SourceSubtext signature) => ExpectedDiagnosticsLocation.TextSpan(DerivableUnitText(signature), signature, prefix: $"DerivableUnit(\"{{0}} / {{1}}\", ");

    private static GeneratorVerifier AssertDerivableUnit(SourceSubtext signature)
    {
        var source = DerivableUnitText(signature);
        var expectedLocation = DerivableUnitLocation(signature);

        return AssertExactlyInvalidDerivationSignatureDiagnosticsWithValidLocation(source).AssertDiagnosticsLocation(expectedLocation, source);
    }

    private static Task AssertAndVerifyDerivableUnit(SourceSubtext signature) => AssertDerivableUnit(signature).VerifyDiagnostics();

    private static string DerivedQuantityText(SourceSubtext signature) => $$"""
        using SharpMeasures.Generators.Quantities;
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;

        [DerivedQuantity("{0} / {1}", {{signature}})]
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

    private static TextSpan DerivedQuantityLocation(SourceSubtext signature) => ExpectedDiagnosticsLocation.TextSpan(DerivedQuantityText(signature), signature, prefix: $"DerivedQuantity(\"{{0}} / {{1}}\", ");

    private static GeneratorVerifier AssertDerivedQuantity(SourceSubtext signature)
    {
        var source = DerivedQuantityText(signature);
        var expectedLocation = DerivedQuantityLocation(signature);

        return AssertExactlyInvalidDerivationSignatureDiagnosticsWithValidLocation(source).AssertDiagnosticsLocation(expectedLocation, source);
    }
}
