namespace SharpMeasures.Generators.Tests.Diagnostics.Misc;

using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Tests.Utility;
using SharpMeasures.Generators.Tests.Verify;

using VerifyXunit;

using Xunit;

[UsesVerify]
public class NullFirstParamsArgument
{
    [Fact]
    public void DerivableUnitSignature() => AssertDerivableUnitSignature();

    private static string DerivationUnitSignatureText => """
        using SharpMeasures.Generators;

        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }

        [ScalarQuantity(typeof(UnitOfTime))]
        public partial class Time { }

        [ScalarQuantity(typeof(UnitOfSpeed))]
        public partial class Speed { }

        [Unit(typeof(Length))]
        public partial class UnitOfLength { }

        [Unit(typeof(Time))]
        public partial class UnitOfTime { }

        [DerivableUnit("1", "{0} + {1} + {2}", null, typeof(UnitOfTime), typeof(UnitOfLength))]
        [Unit(typeof(Speed))]
        public partial class UnitOfSpeed { }
        """;

    private static GeneratorVerifier AssertDerivableUnitSignature()
    {
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(DerivationUnitSignatureText, target: "null");

        return GeneratorVerifier.Construct<SharpMeasuresGenerator>(DerivationUnitSignatureText).AssertSpecificDiagnosticsLocation(DiagnosticIDs.TypeNotUnit, expectedLocation);
    }
}
