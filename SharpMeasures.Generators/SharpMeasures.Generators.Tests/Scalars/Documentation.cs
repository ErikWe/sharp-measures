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
    public Task DefaultExtension() => Verify("DocTest");

    [Fact]
    public Task ExplicitDocTxt() => Verify("doc.txt", "DocTest");

    [Fact]
    public Task DocTxtWithDot() => Verify(".doc.txt", "DocTest");

    [Fact]
    public Task DocumentationTxt() => Verify("documentation.txt", "DocTest");

    [Fact]
    public Task SelfType() => Verify("doc.txt", "SelfType");

    [Fact]
    public Task OverridenSelfType() => Verify("doc.txt", "OverridenSelfType");

    private static Task Verify(string typeName) => GeneratorVerifier.Construct<SharpMeasuresGenerator>(Text(typeName), Settings).AssertNoDiagnosticsReported().VerifyMatchingSourceNames($"{typeName}.Common.g.cs");
    private static Task Verify(string extension, string typeName) => GeneratorVerifier.Construct<SharpMeasuresGenerator>(Text(typeName), Settings, Options(extension)).AssertNoDiagnosticsReported().VerifyMatchingSourceNames($"{typeName}.Common.g.cs");

    private static string Text(string typeName) => $$"""
        using SharpMeasures.Generators;

        [ScalarQuantity(typeof(UnitOfDocTest))]
        public partial class {{typeName}} { }

        [Unit(typeof({{typeName}}))]
        public partial class UnitOfDocTest { }
        """;

    private static GeneratorVerifierSettings Settings => GeneratorVerifierSettings.AllAssertions with { DocumentationPath = @"\Documentation" };

    private static AnalyzerConfigOptionsProvider Options(string extension)
    {
        var builder = ImmutableDictionary.CreateBuilder<string, string>();

        builder.Add("SharpMeasures_DocumentationFileExtension", extension);

        return new CustomAnalyzerConfigOptionsProvider(new CustomAnalyzerConfigOptions(builder.ToImmutable()));
    }
}
