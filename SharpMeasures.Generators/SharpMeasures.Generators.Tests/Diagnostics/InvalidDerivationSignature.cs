namespace SharpMeasures.Generators.Tests.Diagnostics;

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
    public Task VerifyInvalidDerivationExpressionDiagnosticsMessage_Null() => AssertDerivableUnit(NullSignature).VerifyDiagnostics();

    [Theory]
    [MemberData(nameof(InvalidSignatures))]
    public void DerivableUnit(SourceSubtext signature) => AssertDerivableUnit(signature);

    [Theory]
    [MemberData(nameof(InvalidSignatures))]
    public void DerivedQuantity(SourceSubtext signature) => AssertDerivedQuantity(signature);

    public static IEnumerable<object[]> InvalidSignatures() => new object[][]
    {
        new object[] { NullSignature },
        new object[] { EmptySignature }
    };

    private static SourceSubtext NullSignature { get; } = SourceSubtext.Covered("null", prefix: "(System.Type[])");
    private static SourceSubtext EmptySignature { get; } = SourceSubtext.Covered("System.Type[0]", prefix: "new ");

    private static GeneratorVerifier AssertExactlyInvalidDerivationSignatureDiagnostics(string source) => GeneratorVerifier.Construct<SharpMeasuresGenerator>(source).AssertExactlyListedDiagnosticsIDsReported(InvalidDerivationSignatureDiagnostics);
    private static IReadOnlyCollection<string> InvalidDerivationSignatureDiagnostics { get; } = new string[] { DiagnosticIDs.InvalidDerivationSignature };

    private static string DerivableUnitText(SourceSubtext signature) => $$"""
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;

        [DerivableUnit("42", {{signature}})]
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

    private static GeneratorVerifier AssertDerivableUnit(SourceSubtext signature)
    {
        var source = DerivableUnitText(signature);
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(source, signature.Context.With(outerPrefix: "DerivableUnit(\"42\", "));

        return AssertExactlyInvalidDerivationSignatureDiagnostics(source).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(DerivableUnitIdentical);
    }

    private static string DerivedQuantityText(SourceSubtext signature) => $$"""
        using SharpMeasures.Generators.Quantities;
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;

        [DerivedQuantity("42", {{signature}})]
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

    private static GeneratorVerifier AssertDerivedQuantity(SourceSubtext signature)
    {
        var source = DerivedQuantityText(signature);
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(source, signature.Context.With(outerPrefix: "DerivedQuantity(\"42\", "));

        return AssertExactlyInvalidDerivationSignatureDiagnostics(source).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(DerivableQuantityIdentical);
    }

    private static GeneratorVerifier DerivableUnitIdentical => GeneratorVerifier.Construct<SharpMeasuresGenerator>(DerivableUnitIdenticalText);
    private static GeneratorVerifier DerivableQuantityIdentical => GeneratorVerifier.Construct<SharpMeasuresGenerator>(DerivedQuantityIdenticalText);

    private static string DerivableUnitIdenticalText => """
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        
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

    private static string DerivedQuantityIdenticalText => $$"""
        using SharpMeasures.Generators.Quantities;
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;

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
