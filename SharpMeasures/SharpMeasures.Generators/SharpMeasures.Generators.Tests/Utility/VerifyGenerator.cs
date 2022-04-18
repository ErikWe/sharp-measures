namespace SharpMeasures.Generators.Tests.Utility;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

using VerifyXunit;

using Xunit;

internal static class VerifyGenerator
{
    public static void AssertNoOutput<TGenerator>(string source) where TGenerator : IIncrementalGenerator, new()
        => AssertNoOutput(source, new TGenerator());

    public static void AssertNoOutput(string source, IIncrementalGenerator generator)
        => AssertNoOutput(ConstructDriver(source, generator));

    public static void AssertNoOutput(GeneratorDriver driver)
    {
        GeneratorDriverRunResult results = driver.GetRunResult();

        foreach (GeneratorRunResult result in results.Results)
        {
            Assert.Empty(result.GeneratedSources);
        }
    }

    public static void AssertSomeOutput<TGenerator>(string source) where TGenerator : IIncrementalGenerator, new()
        => AssertSomeOutput(source, new TGenerator());

    public static void AssertSomeOutput(string source, IIncrementalGenerator generator)
        => AssertSomeOutput(ConstructDriver(source, generator));

    public static void AssertSomeOutput(GeneratorDriver driver)
    {
        GeneratorDriverRunResult results = driver.GetRunResult();

        bool anyOutput = false;

        foreach (GeneratorRunResult result in results.Results)
        {
            if (result.GeneratedSources.Length > 0)
            {
                anyOutput = true;
                break;
            }
        }

        Assert.True(anyOutput);
    }

    public static Task VerifyMatch<TGenerator>(string source) where TGenerator : IIncrementalGenerator, new()
        => VerifyMatch(source, new TGenerator());

    public static Task VerifyMatch(string source, IIncrementalGenerator generator)
        => VerifyMatch(ConstructDriver(source, generator));

    public static Task VerifyMatch(GeneratorDriver driver)
    {
        AssertSomeOutput(driver);
        return Verifier.Verify(driver);
    }

    public static GeneratorDriver ConstructDriver<TGenerator>(string source) where TGenerator : IIncrementalGenerator, new()
        => ConstructDriver(source, new TGenerator());

    public static GeneratorDriver ConstructDriver(string sourceCode, IIncrementalGenerator generator)
    {
        SyntaxTree syntaxTree = CSharpSyntaxTree.ParseText(sourceCode);

        IEnumerable<MetadataReference> references = AppDomain.CurrentDomain.GetAssemblies()
            .Where(static (assembly) => !assembly.IsDynamic)
            .Select(static (assembly) => MetadataReference.CreateFromFile(assembly.Location))
            .Cast<MetadataReference>();

        CSharpCompilation compilation = CSharpCompilation.Create("SharpMeasuresTests", new[] { syntaxTree }, references,
            new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary));

        ImmutableArray<AdditionalText> additionalFiles = GetAdditionalFiles();

        return CSharpGeneratorDriver.Create(generator).AddAdditionalTexts(additionalFiles).RunGenerators(compilation);
    }

    private static ImmutableArray<AdditionalText> GetAdditionalFiles()
    {
        static IEnumerable<string> GetDocumentationFiles()
        {
            try
            {
                return Directory.GetFiles(@"..\..\..\Documentation", "*.txt", SearchOption.AllDirectories);
            }
            catch (DirectoryNotFoundException)
            {
                return Enumerable.Empty<string>();
            }
        }

        ImmutableArray<AdditionalText>.Builder builder = ImmutableArray.CreateBuilder<AdditionalText>();

        foreach (string additionalTextPath in GetDocumentationFiles())
        {
            builder.Add(new CustomAdditionalText(additionalTextPath));
        }

        return builder.ToImmutable();
    }
}
