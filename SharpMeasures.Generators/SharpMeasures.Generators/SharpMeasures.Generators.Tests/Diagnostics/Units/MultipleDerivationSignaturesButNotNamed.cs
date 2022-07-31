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
    public Task VerifyMultipleDerivationSignaturesButNotNamedDiagnosticsMessage()
    {
        string source = SourceText("", "\"2\", ");

        return AssertExactlyOneMultipleDerivationSignaturesButNotNamedDiagnostics(source).VerifyDiagnostics();
    }

    [Theory]
    [InlineData("", "\"2\", ")]
    [InlineData("null, ", "\"2\", ")]
    [InlineData("\"\", ", "\"2\", ")]
    public void ExactlyOneDiagnostic(string firstIDWithoutComma, string secondIDWithoutComma)
    {
        string source = SourceText(firstIDWithoutComma, secondIDWithoutComma);

        AssertExactlyOneMultipleDerivationSignaturesButNotNamedDiagnostics(source);
    }

    [Fact]
    public void ExactlyTwoDiagnostic()
    {
        string source = SourceText("", "");

        AssertExactlyTwoMultipleDerivationSignaturesButNotNamedDiagnostics(source);
    }

    private static string SourceText(string firstIDWithoutComma, string secondIDWithoutComma) => $$"""
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

            [DerivableUnit({{firstIDWithoutComma}}"{0} / {1}", typeof(UnitOfLength), typeof(UnitOfTime))]
            [DerivableUnit({{secondIDWithoutComma}}"{1} / {0}", typeof(UnitOfTime), typeof(UnitOfLength))]
            [SharpMeasuresUnit(typeof(Speed))]
            public partial class UnitOfSpeed { }
        """;

    private static GeneratorVerifier AssertExactlyOneMultipleDerivationSignaturesButNotNamedDiagnostics(string source) => GeneratorVerifier.Construct<SharpMeasuresGenerator>(source).AssertExactlyListedDiagnosticsIDsReported(OneMultipleDerivationSignaturesButNotNamedDiagnostics);
    private static GeneratorVerifier AssertExactlyTwoMultipleDerivationSignaturesButNotNamedDiagnostics(string source) => GeneratorVerifier.Construct<SharpMeasuresGenerator>(source).AssertExactlyListedDiagnosticsIDsReported(TwoMultipleDerivationSignaturesButNotNamedDiagnostics);
    private static IReadOnlyCollection<string> OneMultipleDerivationSignaturesButNotNamedDiagnostics { get; } = new string[] { DiagnosticIDs.MultipleDerivationSignaturesButNotNamed };
    private static IReadOnlyCollection<string> TwoMultipleDerivationSignaturesButNotNamedDiagnostics { get; } = new string[] { DiagnosticIDs.MultipleDerivationSignaturesButNotNamed, DiagnosticIDs.MultipleDerivationSignaturesButNotNamed };
}
