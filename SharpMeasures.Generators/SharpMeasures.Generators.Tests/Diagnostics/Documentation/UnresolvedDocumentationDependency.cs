namespace SharpMeasures.Generators.Tests.Diagnostics.Documentation;

using SharpMeasures.Generators.Diagnostics;
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

    private static GeneratorVerifier AssertExactlyUnresolvedDocumentationDependencyDiagnostics() => GeneratorVerifier.Construct<SharpMeasuresGenerator>(string.Empty, GeneratorVerifierSettings.AllAssertions with { DocumentationPath = @"\Diagnostics\Documentation\UnresolvedDocumentationDependencyFiles" }).AssertExactlyListedDiagnosticsIDsReported(UnresolvedDocumentationDependencyDiagnostics);
    private static IReadOnlyCollection<string> UnresolvedDocumentationDependencyDiagnostics { get; } = new string[] { DiagnosticIDs.UnresolvedDocumentationDependency };
}
