namespace SharpMeasures.Generators.Tests.Diagnostics.Units;

using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Tests.Utility;
using SharpMeasures.Generators.Tests.Verify;

using System.Collections.Generic;
using System.Threading.Tasks;

using VerifyXunit;

using Xunit;

[UsesVerify]
public class DuplicateUnitDerivationSignature
{
    [Fact]
    public Task VerifyDuplicateUnitDerivationSignatureDiagnosticsMessage() => Assert().VerifyDiagnostics();

    private static GeneratorVerifier AssertExactlyDuplicateUnitDerivationSignatureDiagnostics(string source) => GeneratorVerifier.Construct<SharpMeasuresGenerator>(source).AssertExactlyListedDiagnosticsIDsReported(DuplicateUnitderivationSignatureDiagnostics);
    private static IReadOnlyCollection<string> DuplicateUnitderivationSignatureDiagnostics { get; } = new string[] { DiagnosticIDs.DuplicateUnitDerivationSignature };

    private static string Text => """
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

        [DerivableUnit("1", "{0} / {1}", typeof(UnitOfLength), typeof(UnitOfTime))]
        [DerivableUnit("2", "{0} / {1}", typeof(UnitOfLength), typeof(UnitOfTime))]
        [SharpMeasuresUnit(typeof(Speed))]
        public partial class UnitOfSpeed { }
        """;

    private static GeneratorVerifier Assert()
    {
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(Text, target: "typeof(UnitOfLength), typeof(UnitOfTime)", prefix: "\"2\", \"{0} / {1}\", ");

        return AssertExactlyDuplicateUnitDerivationSignatureDiagnostics(Text).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(Identical);
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

        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }

        [SharpMeasuresUnit(typeof(Time))]
        public partial class UnitOfTime { }

        [DerivableUnit("{0} / {1}", new[] { typeof(UnitOfLength), typeof(UnitOfTime) })]
        [SharpMeasuresUnit(typeof(Speed))]
        public partial class UnitOfSpeed { }
        """;
}
