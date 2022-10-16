namespace SharpMeasures.Generators.Tests.Incremental.Documentation;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.DriverUtility;
using SharpMeasures.Generators.Tests.Verify;

using System.Collections.Generic;
using System.Collections.Immutable;
using System.Threading.Tasks;

using VerifyXunit;

using Xunit;

[UsesVerify]
public class FixMissingTagBySettingUtility
{
    [Fact]
    public async Task Assert()
    {
        var driver = DriverConstruction.Construct<SharpMeasuresGenerator>().AddAdditionalTexts(ImmutableArray.Create(UnitOfLengthAdditionalText, InitialUtilityAdditionalText));

        var compilation = DriverConstruction.CreateCompilation(SourceText);

        driver = driver.RunGeneratorsAndUpdateCompilation(compilation, out compilation, out _);

        GeneratorVerifier.Construct(SourceText, driver, compilation).AssertAllListedDiagnosticsIDsReported(UnresolvedDocumentationDependencyDiagnostics);

        driver = driver.ReplaceAdditionalText(InitialUtilityAdditionalText, UpdatedUtilityAdditionalText);

        driver = driver.RunGeneratorsAndUpdateCompilation(compilation, out compilation, out _);

        await GeneratorVerifier.Construct(SourceText, driver, compilation, GeneratorVerifierSettings.NoAssertions).AssertNoDiagnosticsReported().VerifyMatchingSourceNames("UnitOfLength.Common.g.cs").ConfigureAwait(false);
    }

    private static IReadOnlyCollection<string> UnresolvedDocumentationDependencyDiagnostics { get; } = new string[] { DiagnosticIDs.DocumentationFileMissingRequestedTag };

    private static string SourceText => """
        using SharpMeasures.Generators;

        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }

        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static AdditionalText UnitOfLengthAdditionalText => new CustomAdditionalText("UnitOfLength.doc.txt", UnitOfLengthDocumentationText);
    private static AdditionalText InitialUtilityAdditionalText => new CustomAdditionalText("Test.doc.txt", InitialUtilityDocumentationText);
    private static AdditionalText UpdatedUtilityAdditionalText => new CustomAdditionalText("Test.doc.txt", UpdatedUtilityDocumentationText);

    private static string UnitOfLengthDocumentationText => """
        # Requires: Test

        # CustomTag
        <summary>Hej!</summary>
        /#
        """;

    private static string InitialUtilityDocumentationText => """
        # Header
        #CustomTag/#
        /#
        """;

    private static string UpdatedUtilityDocumentationText => """
        # Utility: true

        # Header
        #CustomTag/#
        /#
        """;
}
