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
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }

        [SharpMeasuresScalar(typeof(UnitOfTime))]
        public partial class Time { }

        [SharpMeasuresScalar(typeof(UnitOfSpeed))]
        public partial class Speed { }

        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }

        [SharpMeasuresUnit(typeof(Time))]
        public partial class UnitOfTime { }

        [DerivableUnit("1", "{0} / {1}", null, typeof(UnitOfTime), typeof(UnitOfLength))]
        [SharpMeasuresUnit(typeof(Speed))]
        public partial class UnitOfSpeed { }
        """;

    private static GeneratorVerifier AssertDerivableUnitSignature()
    {
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(DerivationUnitSignatureText, target: "null");

        return GeneratorVerifier.Construct<SharpMeasuresGenerator>(DerivationUnitSignatureText).AssertSpecificDiagnosticsLocation(DiagnosticIDs.TypeNotUnit, expectedLocation);
    }
}
