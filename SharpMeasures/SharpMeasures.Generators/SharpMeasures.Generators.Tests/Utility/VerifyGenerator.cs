namespace SharpMeasures.Generators.Tests.Utility;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using VerifyXunit;

using Xunit;

[UsesVerify]
internal static class VerifyGenerator
{
    public static void AssertNoOutput<TGenerator>(string source) where TGenerator : IIncrementalGenerator, new()
        => AssertNoOutput(source, new TGenerator());

    public static void AssertNoOutput(string source, IIncrementalGenerator generator)
        => AssertNoOutput(ConstructAndRunDriver(source, generator).GetRunResult());

    public static void AssertNoOutput(GeneratorDriverRunResult results)
    {
        foreach (GeneratorRunResult result in results.Results)
        {
            AssertNoOutput(result);
        }
    }

    public static void AssertNoOutput(GeneratorRunResult result) => Assert.Empty(result.GeneratedSources);

    public static void AssertSomeOutput<TGenerator>(string source) where TGenerator : IIncrementalGenerator, new()
        => AssertSomeOutput(source, new TGenerator());

    public static void AssertSomeOutput(string source, IIncrementalGenerator generator)
        => AssertSomeOutput(ConstructAndRunDriver(source, generator).GetRunResult());

    public static void AssertSomeOutput(GeneratorDriverRunResult results)
    {
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

    public static void AssertSomeOutput(GeneratorRunResult result) => Assert.NotEmpty(result.GeneratedSources);

    public static void AssertNumberOfGeneratedFiles<TGenerator>(string source, int expected) where TGenerator : IIncrementalGenerator, new()
        => AssertNumberOfGeneratedFiles(source, new TGenerator(), expected);

    public static void AssertNumberOfGeneratedFiles(string source, IIncrementalGenerator generator, int expected)
        => AssertNumberOfGeneratedFiles(ConstructAndRunDriver(source, generator).GetRunResult(), expected);

    public static void AssertNumberOfGeneratedFiles(GeneratorDriverRunResult results, int expected)
    {
        int actual = 0;

        foreach (GeneratorRunResult result in results.Results)
        {
            actual += result.GeneratedSources.Length;
        }

        Assert.Equal(expected, actual);
    }

    public static void AssertNumberOfGeneratedFiles(GeneratorRunResult result, int expected)
        => Assert.Equal(expected, result.GeneratedSources.Length);

    public static void AssertAllListedFilesGenerated<TGenerator>(string source, params string[] files) where TGenerator : IIncrementalGenerator, new()
        => AssertAllListedFilesGenerated(source, new TGenerator(), files);

    public static void AssertAllListedFilesGenerated(string source, IIncrementalGenerator generator, params string[] files)
        => AssertAllListedFilesGenerated(ConstructAndRunDriver(source, generator).GetRunResult(), files);

    public static void AssertAllListedFilesGenerated(GeneratorDriverRunResult results, params string[] files)
    {
        int matchingFiles = 0;

        foreach (GeneratorRunResult result in results.Results)
        {
            foreach (GeneratedSourceResult generatedSource in result.GeneratedSources)
            {
                if (files.Contains(generatedSource.HintName))
                {
                    matchingFiles++;
                }
            }
        }

        Assert.Equal(matchingFiles, files.Length);
    }

    public static void AssertAllListedFilesGenerated(GeneratorRunResult result, params string[] files)
    {
        int matchingFiles = 0;

        foreach (GeneratedSourceResult generatedSource in result.GeneratedSources)
        {
            if (files.Contains(generatedSource.HintName))
            {
                matchingFiles++;
            }
        }

        Assert.Equal(matchingFiles, files.Length);
    }

    public static void AssertOnlyListedFilesGenerated<TGenerator>(string source, params string[] files) where TGenerator : IIncrementalGenerator, new()
        => AssertOnlyListedFilesGenerated(source, new TGenerator(), files);

    public static void AssertOnlyListedFilesGenerated(string source, IIncrementalGenerator generator, params string[] files)
        => AssertOnlyListedFilesGenerated(ConstructAndRunDriver(source, generator).GetRunResult(), files);

    public static void AssertOnlyListedFilesGenerated(GeneratorDriverRunResult results, params string[] files)
    {
        foreach (GeneratorRunResult result in results.Results)
        {
            AssertOnlyListedFilesGenerated(result, files);
        }
    }

    public static void AssertOnlyListedFilesGenerated(GeneratorRunResult result, params string[] files)
    {
        foreach (GeneratedSourceResult generatedSource in result.GeneratedSources)
        {
            Assert.Contains(generatedSource.HintName, files);
        }
    }

    public static void AssertNoListedFileGenerated<TGenerator>(string source, params string[] files) where TGenerator : IIncrementalGenerator, new()
        => AssertNoListedFileGenerated(source, new TGenerator(), files);

    public static void AssertNoListedFileGenerated(string source, IIncrementalGenerator generator, params string[] files)
        => AssertNoListedFileGenerated(ConstructAndRunDriver(source, generator).GetRunResult(), files);

    public static void AssertNoListedFileGenerated(GeneratorDriverRunResult results, params string[] files)
    {
        foreach (GeneratorRunResult result in results.Results)
        {
            AssertNoListedFileGenerated(result, files);
        }
    }

    public static void AssertNoListedFileGenerated(GeneratorRunResult result, params string[] files)
    {
        foreach (GeneratedSourceResult generatedSource in result.GeneratedSources)
        {
            Assert.DoesNotContain(generatedSource.HintName, files);
        }
    }

    public static Task VerifyMatch<TGenerator>(string source) where TGenerator : IIncrementalGenerator, new()
        => VerifyMatch(source, new TGenerator());

    public static Task VerifyMatch(string source, IIncrementalGenerator generator)
        => VerifyMatch(ConstructAndRunDriver(source, generator).GetRunResult());

    public static Task VerifyMatch(GeneratorDriverRunResult results)
    {
        AssertSomeOutput(results);
        return Verifier.Verify(results);
    }

    public static Task VerifyMatch<TGenerator>(string source, params string[] files) where TGenerator : IIncrementalGenerator, new()
        => VerifyMatch(source, new TGenerator(), files);

    public static Task VerifyMatch(string source, IIncrementalGenerator generator, params string[] files)
        => VerifyMatch(ConstructAndRunDriver(source, generator).GetRunResult(), files);

    public static Task VerifyMatch(GeneratorDriverRunResult results, params string[] files)
    {
        List<Task> tasks = new();

        foreach (GeneratorRunResult result in results.Results)
        {
            foreach (GeneratedSourceResult generatedSource in result.GeneratedSources)
            {
                if (files.Contains(generatedSource.HintName))
                {
                    tasks.Add(Task.Run(() => Verifier.Verify(generatedSource.SourceText.ToString())));
                }
            }
        }

        Assert.Equal(files.Length, tasks.Count);
        return Task.WhenAll(tasks);
    }

    public static Task VerifyMatch(GeneratorRunResult result, params string[] files)
    {
        List<Task> tasks = new();

        foreach (GeneratedSourceResult generatedSource in result.GeneratedSources)
        {
            if (files.Contains(generatedSource.HintName))
            {
                tasks.Add(Task.Run(() => Verifier.Verify(generatedSource.SourceText.ToString())));
            }
        }

        Assert.Equal(files.Length, tasks.Count);
        return Task.WhenAll(tasks);
    }

    public static void VerifyIdentical<TGenerator>(string source1, string source2) where TGenerator : IIncrementalGenerator, new()
        => VerifyIdentical(source1, source2, new TGenerator());

    public static void VerifyIdentical(string source1, string source2, IIncrementalGenerator generator)
        => VerifyIdentical(ConstructAndRunDriver(source1, generator).GetRunResult(), source2, generator);

    public static void VerifyIdentical<TGenerator>(GeneratorDriverRunResult result1, string source2) where TGenerator : IIncrementalGenerator, new()
        => VerifyIdentical(result1, source2, new TGenerator());

    public static void VerifyIdentical(GeneratorDriverRunResult result1, string source2, IIncrementalGenerator generator)
        => VerifyIdentical(result1, ConstructAndRunDriver(source2, generator).GetRunResult());

    public static void VerifyIdentical(GeneratorDriverRunResult results1, GeneratorDriverRunResult results2)
    {
        IDictionary<string, string> files = new Dictionary<string, string>();
        int matched = 0;

        foreach (GeneratorRunResult result in results1.Results)
        {
            foreach (GeneratedSourceResult generatedSource in result.GeneratedSources)
            {
                files[fileIdentifier(result, generatedSource)] = removeStamp(generatedSource);
            }
        }

        foreach (GeneratorRunResult result in results2.Results)
        {
            foreach (GeneratedSourceResult generatedSource in result.GeneratedSources)
            {
                string identifier = fileIdentifier(result, generatedSource);

                matched += 1;
                Assert.Contains(identifier, files);
                Assert.Equal(removeStamp(generatedSource), files[identifier]);
            }
        }

        Assert.Equal(files.Count, matched);

        static string fileIdentifier(GeneratorRunResult result, GeneratedSourceResult generatedSource)
            => result.Generator.GetGeneratorType().ToString() + ':' + generatedSource.HintName;

        static string removeStamp(GeneratedSourceResult generatedSource) => StampRegex.Replace(generatedSource.SourceText.ToString(), StampReplacement);
    }

    public static void VerifyIdentical(GeneratorRunResult result1, GeneratorRunResult result2)
    {
        IDictionary<string, string> files = new Dictionary<string, string>();
        int matched = 0;

        foreach (GeneratedSourceResult generatedSource in result1.GeneratedSources)
        {
            files[generatedSource.HintName] = generatedSource.SourceText.ToString();
        }

        foreach (GeneratedSourceResult generatedSource in result2.GeneratedSources)
        {
            matched += 1;
            Assert.Contains(generatedSource.HintName, files);
            Assert.Equal(generatedSource.SourceText.ToString(), files[generatedSource.HintName]);
        }

        Assert.Equal(files.Count, matched);
    }

    public static GeneratorDriver ConstructAndRunDriver<TGenerator>(string source) where TGenerator : IIncrementalGenerator, new()
        => ConstructAndRunDriver(source, new TGenerator());

    public static GeneratorDriver ConstructAndRunDriver(string source, IIncrementalGenerator generator)
        => RunDriver(source, ConstructDriver(generator));

    public static GeneratorDriver ConstructDriver<TGenerator>() where TGenerator : IIncrementalGenerator, new()
        => ConstructDriver(new TGenerator());

    public static GeneratorDriver ConstructDriver(IIncrementalGenerator generator)
    {
        ImmutableArray<AdditionalText> additionalFiles = GetAdditionalFiles();

        return CSharpGeneratorDriver.Create(generator).AddAdditionalTexts(additionalFiles);
    }

    public static GeneratorDriver RunDriver(string source, GeneratorDriver driver)
    {
        SyntaxTree syntaxTree = CSharpSyntaxTree.ParseText(source);

        IEnumerable<MetadataReference> references = AppDomain.CurrentDomain.GetAssemblies()
            .Where(static (assembly) => !assembly.IsDynamic)
            .Select(static (assembly) => MetadataReference.CreateFromFile(assembly.Location))
            .Cast<MetadataReference>();

        CSharpCompilation compilation = CSharpCompilation.Create("SharpMeasuresTests", new[] { syntaxTree }, references,
            new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary));

        return driver.RunGenerators(compilation);
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

    private static Regex StampRegex { get; } = new(@"(?<header>This file was generated by SharpMeasures\.Generators ).+", RegexOptions.ExplicitCapture);
    private static string StampReplacement { get; } = "${header}<stamp>";
}
