namespace SharpMeasures.Generators.Tests.Diagnostics.Documentation;

using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.DriverUtility;
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

    private static GeneratorVerifier AssertExactlyDocumentationFileDuplicateTagDiagnostics() => GeneratorVerifier.Construct<SharpMeasuresGenerator>(string.Empty, DriverConstructionConfiguration.Empty with { DocumentationFiles = DocumentationDictionary }).AssertExactlyListedDiagnosticsIDsReported(DocumentationFileDuplicateTagDiagnostics);
    private static IReadOnlyCollection<string> DocumentationFileDuplicateTagDiagnostics { get; } = new string[] { DiagnosticIDs.DocumentationFileDuplicateTag };

    private static Dictionary<string, string> DocumentationDictionary => new() { { "A.doc.txt", DocumentationText } };

    private static string DocumentationText => """
        # Header
        <summary>Foo.</summary>
        /#

        # Header
        <summary>Bar.</summary>
        /#
        """;
}
