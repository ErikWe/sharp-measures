namespace SharpMeasures.Generators.Tests.Scalars;

using Microsoft.CodeAnalysis.Diagnostics;

using SharpMeasures.Generators.DriverUtility;
using SharpMeasures.Generators.Tests.Verify;

using System.Collections.Generic;
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

    private static Task Verify(string typeName) => GeneratorVerifier.Construct<SharpMeasuresGenerator>(Text(typeName), Configuration()).AssertNoDiagnosticsReported().VerifyMatchingSourceNames($"{typeName}.Common.g.cs");
    private static Task Verify(string extension, string typeName) => GeneratorVerifier.Construct<SharpMeasuresGenerator>(Text(typeName), Configuration(extension)).AssertNoDiagnosticsReported().VerifyMatchingSourceNames($"{typeName}.Common.g.cs");

    private static string Text(string typeName) => $$"""
        using SharpMeasures.Generators;

        [ScalarQuantity(typeof(UnitOfDocTest))]
        public partial class {{typeName}} { }

        [Unit(typeof({{typeName}}))]
        public partial class UnitOfDocTest { }
        """;

    private static Dictionary<string, string> DocumentationFiles { get; } = new()
    {
        { "DocTest.doc.txt", DocTest_Doc_Txt },
        { "DocTest.documentation.txt", DocTest_Documentation_Txt },
        { "OverridenSelfType.doc.txt", OverridenSelfType_Doc_Txt },
        { "SelfType.doc.txt", SelfType_Doc_Txt }
    };

    private static string DocTest_Doc_Txt => """
        # Header
        <summary>Used extension .doc.txt</summary>
        /#
        """;

    private static string DocTest_Documentation_Txt => """
        # Header
        <summary>Used extension .documentation.txt</summary>
        /#
        """;

    private static string OverridenSelfType_Doc_Txt => """
        # Header
        <summary>This is a cref="#SelfType/#"/>.</summary>
        /#

        # SelfType
        int
        /#
        """;

    private static string SelfType_Doc_Txt => """
        # Header
        <summary>This is a cref="#SelfType/#"/>.</summary>
        /#
        """;

    private static DriverConstructionConfiguration Configuration() => DriverConstructionConfiguration.Empty with { DocumentationFiles = DocumentationFiles };
    private static DriverConstructionConfiguration Configuration(string extension)
    {
        var builder = ImmutableDictionary.CreateBuilder<string, string>();

        builder.Add("SharpMeasures_DocumentationFileExtension", extension);

        AnalyzerConfigOptionsProvider provider = new CustomAnalyzerConfigOptionsProvider(new CustomAnalyzerConfigOptions(builder.ToImmutable()));

        return Configuration() with { OptionsProvider = provider };
    }
}
