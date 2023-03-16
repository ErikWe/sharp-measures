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
        using SharpMeasures.Generators;

        [DerivableUnit("42", {{signature}})]
        [Unit(typeof(Speed))]
        public partial class UnitOfSpeed { }

        [Unit(typeof(Length))]
        public partial class UnitOfLength { }

        [Unit(typeof(Time))]
        public partial class UnitOfTime { }

        [ScalarQuantity(typeof(UnitOfSpeed))]
        public partial class Speed { }

        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }

        [ScalarQuantity(typeof(UnitOfTime))]
        public partial class Time { }
        """;

    private static GeneratorVerifier AssertDerivableUnit(SourceSubtext signature)
    {
        var source = DerivableUnitText(signature);
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(source, signature.Context.With(outerPrefix: "DerivableUnit(\"42\", "));

        return AssertExactlyInvalidDerivationSignatureDiagnostics(source).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(DerivableUnitIdentical);
    }

    private static GeneratorVerifier DerivableUnitIdentical => GeneratorVerifier.Construct<SharpMeasuresGenerator>(DerivableUnitIdenticalText);

    private static string DerivableUnitIdenticalText => """
        using SharpMeasures.Generators;
        
        [Unit(typeof(Speed))]
        public partial class UnitOfSpeed { }

        [Unit(typeof(Length))]
        public partial class UnitOfLength { }

        [Unit(typeof(Time))]
        public partial class UnitOfTime { }

        [ScalarQuantity(typeof(UnitOfSpeed))]
        public partial class Speed { }

        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }

        [ScalarQuantity(typeof(UnitOfTime))]
        public partial class Time { }
        """;
}
