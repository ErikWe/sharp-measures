namespace SharpMeasures.Generators.Tests.Scalars;

using Microsoft.CodeAnalysis.Diagnostics;

using SharpMeasures.Generators.DriverUtility;
using SharpMeasures.Generators.Tests.Verify;

using System.Collections.Immutable;
using System.Threading.Tasks;

using VerifyXunit;

using Xunit;

[UsesVerify]
public class Documentation
{
    [Fact]
    public Task DefaultExtension() => Verify();

    [Fact]
    public Task ExplicitDocTxt() => Verify("doc.txt");

    [Fact]
    public Task DocTxtWithDot() => Verify(".doc.txt");

    [Fact]
    public Task DocumentationTxt() => Verify("documentation.txt");

    private static Task Verify() => GeneratorVerifier.Construct<SharpMeasuresGenerator>(Text).AssertNoDiagnosticsReported().VerifyMatchingSourceNames("DocTest.Common.g.cs");
    private static Task Verify(string level) => GeneratorVerifier.Construct<SharpMeasuresGenerator>(Text, Options(level)).AssertNoDiagnosticsReported().VerifyMatchingSourceNames("DocTest.Common.g.cs");

    private static string Text => """
        using SharpMeasures.Generators;

        [ScalarQuantity(typeof(UnitOfDocTest))]
        public partial class DocTest { }

        [Unit(typeof(DocTest))]
        public partial class UnitOfDocTest { }
        """;

    private static AnalyzerConfigOptionsProvider Options(string extension)
    {
        var builder = ImmutableDictionary.CreateBuilder<string, string>();

        builder.Add("SharpMeasures_DocumentationFileExtension", extension);

        return new CustomAnalyzerConfigOptionsProvider(new CustomAnalyzerConfigOptions(builder.ToImmutable()));
    }
}
