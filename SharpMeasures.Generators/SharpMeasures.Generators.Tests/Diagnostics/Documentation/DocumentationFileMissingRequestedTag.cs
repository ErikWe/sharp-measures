namespace SharpMeasures.Generators.Tests.Diagnostics.Documentation;

using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Tests.Verify;

using System.Collections.Generic;
using System.Threading.Tasks;

using VerifyXunit;

using Xunit;

[UsesVerify]
public class DocumentationFileMissingRequestedTag
{
    [Fact]
    public Task Verify() => AssertExactlyDocumentationFileMissingRequestedTagDiagnostics().VerifyDiagnostics();

    private static GeneratorVerifier AssertExactlyDocumentationFileMissingRequestedTagDiagnostics() => GeneratorVerifier.Construct<SharpMeasuresGenerator>(string.Empty, GeneratorVerifierSettings.AllAssertions with { DocumentationPath = @"\Diagnostics\Documentation\DocumentationFileMissingRequestedTagFiles" }).AssertExactlyListedDiagnosticsIDsReported(DocumentationFileMissingRequestedTagDiagnostics);
    private static IReadOnlyCollection<string> DocumentationFileMissingRequestedTagDiagnostics { get; } = new string[] { DiagnosticIDs.DocumentationFileMissingRequestedTag };
}
