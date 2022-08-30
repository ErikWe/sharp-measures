namespace SharpMeasures.Generators.Tests.Diagnostics.Units;

using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Tests.Utility;
using SharpMeasures.Generators.Tests.Verify;

using System.Collections.Generic;
using System.Threading.Tasks;

using VerifyXunit;

using Xunit;

[UsesVerify]
public class AmbiguousDerivationSignatureNotSpecified
{
    [Fact]
    public Task VerifyAmbiguousDerivationSignatureNotSpecifiedDiagnosticsMessage() => Assert(IgnoredName).VerifyDiagnostics();

    [Theory]
    [MemberData(nameof(InvalidNames))]
    public void ExactList(string derivationID) => Assert(derivationID);

    public static IEnumerable<object[]> InvalidNames => new object[][]
    {
        new[] { IgnoredName },
        new[] { NullName },
        new[] { EmptyName }
    };

    private static string IgnoredName { get; } = string.Empty;
    private static string NullName { get; } = "null, ";
    private static string EmptyName { get; } = "\"\", ";

    private static GeneratorVerifier AssertExactlyAmbiguousDerivationSignatureNotSpecifiedDiagnostics(string source) => GeneratorVerifier.Construct<SharpMeasuresGenerator>(source).AssertExactlyListedDiagnosticsIDsReported(AmbiguousDerivationSignatureNotSpecifiedDiagnostics);
    private static IReadOnlyCollection<string> AmbiguousDerivationSignatureNotSpecifiedDiagnostics { get; } = new string[] { DiagnosticIDs.AmbiguousDerivationSignatureNotSpecified };

    private static string Text(string derivationIDWithoutComma) => $$"""
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

    private static GeneratorVerifier Assert(string derivationIDWithoutComma)
    {
        var source = Text(derivationIDWithoutComma);
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(source, target: "DerivedUnit");

        return AssertExactlyAmbiguousDerivationSignatureNotSpecifiedDiagnostics(source).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(Identical);
    }

    private static GeneratorVerifier Identical => GeneratorVerifier.Construct<SharpMeasuresGenerator>(IdenticalText);

    private static string IdenticalText => """
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
        [SharpMeasuresUnit(typeof(Speed))]
        public partial class UnitOfSpeed { }
        """;
}
