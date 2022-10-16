namespace SharpMeasures.Generators.Tests.Diagnostics.Documentation;

using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.DriverUtility;
using SharpMeasures.Generators.Tests.Verify;

using System.Collections.Generic;
using System.Threading.Tasks;

using VerifyXunit;

using Xunit;

[UsesVerify]
public class UnresolvedDocumentationDependency
{
    [Fact]
    public Task Verify() => AssertExactlyUnresolvedDocumentationDependencyDiagnostics().VerifyDiagnostics();

    private static GeneratorVerifier AssertExactlyUnresolvedDocumentationDependencyDiagnostics() => GeneratorVerifier.Construct<SharpMeasuresGenerator>(string.Empty, DriverConstructionConfiguration.Empty with { DocumentationFiles = DocumentationDictionary }).AssertExactlyListedDiagnosticsIDsReported(UnresolvedDocumentationDependencyDiagnostics);
    private static IReadOnlyCollection<string> UnresolvedDocumentationDependencyDiagnostics { get; } = new string[] { DiagnosticIDs.UnresolvedDocumentationDependency };

    private static Dictionary<string, string> DocumentationDictionary => new() { { "A.doc.txt", DocumentationText } };

    private static string DocumentationText => """
        # Requires: Test
        """;
}
