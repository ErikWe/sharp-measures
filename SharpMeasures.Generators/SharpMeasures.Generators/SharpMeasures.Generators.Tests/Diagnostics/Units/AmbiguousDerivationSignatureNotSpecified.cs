namespace SharpMeasures.Generators.Tests.Diagnostics.Units;

using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Tests.Verify;

using System.Collections.Generic;
using System.Threading.Tasks;

using VerifyXunit;

using Xunit;

[UsesVerify]
public class AmbiguousDerivationSignatureNotSpecified
{
    [Fact]
    public Task VerifyAmbiguousDerivationSignatureNotSpecifiedDiagnosticsMessage()
    {
        string source = SourceText("");

        return AssertExactlyAmbiguousDerivationSignatureNotSpecifiedDiagnosticsWithValidLocation(source).VerifyDiagnostics();
    }

    [Theory]
    [InlineData("")]
    [InlineData("null, ")]
    [InlineData("\"\", ")]
    public void ExactList(string id)
    {
        string source = SourceText(id);

        AssertExactlyAmbiguousDerivationSignatureNotSpecifiedDiagnosticsWithValidLocation(source);
    }

    private static string SourceText(string derivationIDWithoutComma) => $$"""
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }

        [SharpMeasuresScalar(typeof(UnitOfTime))]
        public partial class Time { }

        [SharpMeasuresScalar(typeof(UnitOfSpeed))]
        public partial class Speed { }

        [FixedUnit("Metre", "Metres", 1)]
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }

        [FixedUnit("Second", "Seconds", 1)]
        [SharpMeasuresUnit(typeof(Time))]
        public partial class UnitOfTime { }

        [DerivableUnit("1", "{0} / {1}", typeof(UnitOfLength), typeof(UnitOfTime))]
        [DerivableUnit("2", "{1} / {0}", typeof(UnitOfTime), typeof(UnitOfLength))]
        [DerivedUnit("MetrePerSecond", "MetresPerSecond", {{derivationIDWithoutComma}}new[] { "Metre", "Second" })]
        [SharpMeasuresUnit(typeof(Speed))]
        public partial class UnitOfSpeed { }
        """;

    private static GeneratorVerifier AssertExactlyAmbiguousDerivationSignatureNotSpecifiedDiagnosticsWithValidLocation(string source) => GeneratorVerifier.Construct<SharpMeasuresGenerator>(source).AssertExactlyListedDiagnosticsIDsReported(AmbiguousDerivationSignatureNotSpecifiedDiagnostics).AssertAllDiagnosticsValidLocation();
    private static IReadOnlyCollection<string> AmbiguousDerivationSignatureNotSpecifiedDiagnostics { get; } = new string[] { DiagnosticIDs.AmbiguousDerivationSignatureNotSpecified };
}
