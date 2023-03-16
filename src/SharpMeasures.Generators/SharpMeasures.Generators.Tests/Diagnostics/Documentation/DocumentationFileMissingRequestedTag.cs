namespace SharpMeasures.Generators.Tests.Diagnostics.Documentation;

using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.DriverUtility;
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

    private static GeneratorVerifier AssertExactlyDocumentationFileMissingRequestedTagDiagnostics() => GeneratorVerifier.Construct<SharpMeasuresGenerator>(string.Empty, DriverConstructionConfiguration.Empty with { DocumentationFiles = DocumentationDictionary }).AssertExactlyListedDiagnosticsIDsReported(DocumentationFileMissingRequestedTagDiagnostics);
    private static IReadOnlyCollection<string> DocumentationFileMissingRequestedTagDiagnostics { get; } = new string[] { DiagnosticIDs.DocumentationFileMissingRequestedTag };

    private static Dictionary<string, string> DocumentationDictionary => new() { { "A.doc.txt", DocumentationText } };

    private static string DocumentationText => """
        # Header
        <summary>#Test/#</summary>
        /#
        """;
}
