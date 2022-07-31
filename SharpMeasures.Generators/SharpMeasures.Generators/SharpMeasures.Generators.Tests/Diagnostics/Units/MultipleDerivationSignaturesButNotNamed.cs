namespace SharpMeasures.Generators.Tests.Diagnostics.Units;

using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Tests.Verify;

using System.Collections.Generic;
using System.Threading.Tasks;

using VerifyXunit;

using Xunit;

[UsesVerify]
public class MultipleDerivationSignaturesButNotNamed
{
    [Fact]
    public Task SingleUnnamed_ExactListAndVerify()
    {
        string source = $$"""
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

            [DerivableUnit("{0} / {1}", typeof(UnitOfLength), typeof(UnitOfTime))]
            [DerivableUnit("1", "{1} / {0}", typeof(UnitOfTime), typeof(UnitOfLength))]
            [SharpMeasuresUnit(typeof(Speed))]
            public partial class UnitOfSpeed { }
            """;

        return AssertExactlyOneMultipleDerivationSignaturesButNotNamedDiagnostics(source).VerifyDiagnostics();
    }

    [Fact]
    public void MultipleUnnamed_ExactList()
    {
        string source = $$"""
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

            [DerivableUnit("{0} / {1}", typeof(UnitOfLength), typeof(UnitOfTime))]
            [DerivableUnit("{1} / {0}", typeof(UnitOfTime), typeof(UnitOfLength))]
            [SharpMeasuresUnit(typeof(Speed))]
            public partial class UnitOfSpeed { }
            """;

        AssertExactlyTwoMultipleDerivationSignaturesButNotNamedDiagnostics(source);
    }

    private static GeneratorVerifier AssertExactlyOneMultipleDerivationSignaturesButNotNamedDiagnostics(string source) => GeneratorVerifier.Construct<SharpMeasuresGenerator>(source).AssertExactlyListedDiagnosticsIDsReported(OneMultipleDerivationSignaturesButNotNamedDiagnostics);
    private static GeneratorVerifier AssertExactlyTwoMultipleDerivationSignaturesButNotNamedDiagnostics(string source) => GeneratorVerifier.Construct<SharpMeasuresGenerator>(source).AssertExactlyListedDiagnosticsIDsReported(TwoMultipleDerivationSignaturesButNotNamedDiagnostics);
    private static IReadOnlyCollection<string> OneMultipleDerivationSignaturesButNotNamedDiagnostics { get; } = new string[] { DiagnosticIDs.MultipleDerivationSignaturesButNotNamed };
    private static IReadOnlyCollection<string> TwoMultipleDerivationSignaturesButNotNamedDiagnostics { get; } = new string[] { DiagnosticIDs.MultipleDerivationSignaturesButNotNamed, DiagnosticIDs.MultipleDerivationSignaturesButNotNamed };
}
