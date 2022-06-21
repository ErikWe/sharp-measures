namespace SharpMeasures.Generators.Tests.Diagnostics.Units;

using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Tests.Verify;

using System.Collections.Generic;
using System.Threading.Tasks;

using VerifyXunit;

using Xunit;

[UsesVerify]
public class EmptyUnitDerivationSignature
{
    private static string Text(string signature) => $$"""
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

        [DerivableUnit("id", "{0} / {1}"{{signature}})]
        [SharpMeasuresUnit(typeof(Speed))]
        public partial class UnitOfSpeed { }
        """;

    [Fact]
    public Task Params_ExactListAndVerify()
    {
        string source = Text("");

        return AssertExactlyEmptyUnitDerivationSignatureDiagnostics(source).VerifyDiagnostics();
    }

    [Fact]
    public Task Array_ExactListAndVerify()
    {
        string source = Text(", new System.Type[] { }");

        return AssertExactlyEmptyUnitDerivationSignatureDiagnostics(source).VerifyDiagnostics();
    }

    private static GeneratorVerifier AssertExactlyEmptyUnitDerivationSignatureDiagnostics(string source)
        => GeneratorVerifier.Construct<SharpMeasuresGenerator>(source).AssertExactlyListedDiagnosticsIDsReported(EmptyUnitDerivationSignatureDiagnostics);

    private static IReadOnlyCollection<string> EmptyUnitDerivationSignatureDiagnostics { get; } = new string[] { DiagnosticIDs.EmptyUnitDerivationSignature };
}
