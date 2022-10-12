namespace SharpMeasures.Generators.Tests.Diagnostics.Documentation;

using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Tests.Verify;

using System.Collections.Generic;
using System.Threading.Tasks;

using VerifyXunit;

using Xunit;

[UsesVerify]
public class DocumentationFileDuplicateTag
{
    [Fact]
    public Task Verify() => AssertExactlyDocumentationFileDuplicateTagDiagnostics().VerifyDiagnostics();

    private static GeneratorVerifier AssertExactlyDocumentationFileDuplicateTagDiagnostics() => GeneratorVerifier.Construct<SharpMeasuresGenerator>(string.Empty, GeneratorVerifierSettings.AllAssertions with { DocumentationPath = @"\Diagnostics\Documentation\DocumentationFileDuplicateTagFiles" }).AssertExactlyListedDiagnosticsIDsReported(DocumentationFileDuplicateTagDiagnostics);
    private static IReadOnlyCollection<string> DocumentationFileDuplicateTagDiagnostics { get; } = new string[] { DiagnosticIDs.DocumentationFileDuplicateTag };
}
