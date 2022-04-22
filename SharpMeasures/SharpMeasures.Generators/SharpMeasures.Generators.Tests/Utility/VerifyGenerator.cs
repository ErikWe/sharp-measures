namespace SharpMeasures.Generators.Tests.Utility;

using Microsoft.CodeAnalysis;

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
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
        => AssertNoOutput(DriverUtility.ConstructAndRunDriver(source, generator).GetRunResult());

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
        => AssertSomeOutput(DriverUtility.ConstructAndRunDriver(source, generator).GetRunResult());

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
        => AssertNumberOfGeneratedFiles(DriverUtility.ConstructAndRunDriver(source, generator).GetRunResult(), expected);

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
        => AssertAllListedFilesGenerated(DriverUtility.ConstructAndRunDriver(source, generator).GetRunResult(), files);

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
        => AssertOnlyListedFilesGenerated(DriverUtility.ConstructAndRunDriver(source, generator).GetRunResult(), files);

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
        => AssertNoListedFileGenerated(DriverUtility.ConstructAndRunDriver(source, generator).GetRunResult(), files);

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
        => VerifyMatch(DriverUtility.ConstructAndRunDriver(source, generator).GetRunResult());

    public static Task VerifyMatch(GeneratorDriverRunResult results)
    {
        AssertSomeOutput(results);
        return Verifier.Verify(results);
    }

    public static Task VerifyMatch<TGenerator>(string source, params string[] files) where TGenerator : IIncrementalGenerator, new()
        => VerifyMatch(source, new TGenerator(), files);

    public static Task VerifyMatch(string source, IIncrementalGenerator generator, params string[] files)
        => VerifyMatch(DriverUtility.ConstructAndRunDriver(source, generator).GetRunResult(), files);

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
        => VerifyIdentical(DriverUtility.ConstructAndRunDriver(source1, generator).GetRunResult(), source2, generator);

    public static void VerifyIdentical<TGenerator>(GeneratorDriverRunResult results1, string source2) where TGenerator : IIncrementalGenerator, new()
        => VerifyIdentical(results1, source2, new TGenerator());

    public static void VerifyIdentical(GeneratorDriverRunResult results1, string source2, IIncrementalGenerator generator)
        => VerifyIdentical(results1, DriverUtility.ConstructAndRunDriver(source2, generator).GetRunResult());

    public static void VerifyIdentical(GeneratorDriverRunResult results1, GeneratorDriverRunResult results2)
    {
        IDictionary<(string, string), string> unmatchedGeneratedSources = new Dictionary<(string, string), string>();

        foreach (GeneratorRunResult result in results1.Results)
        {
            foreach (GeneratedSourceResult generatedSource in result.GeneratedSources)
            {
                unmatchedGeneratedSources[(result.Generator.GetGeneratorType().ToString(), generatedSource.HintName)] = removeStamp(generatedSource);
            }
        }

        foreach (GeneratorRunResult result in results2.Results)
        {
            foreach (GeneratedSourceResult generatedSource in result.GeneratedSources)
            {
                (string, string) identifier = (result.Generator.GetGeneratorType().ToString(), generatedSource.HintName);

                Assert.Contains(identifier, unmatchedGeneratedSources);
                Assert.Equal(removeStamp(generatedSource), unmatchedGeneratedSources[identifier]);

                unmatchedGeneratedSources.Remove(identifier);
            }
        }

        Assert.Empty(unmatchedGeneratedSources);

        static string removeStamp(GeneratedSourceResult generatedSource) => StampRegex.Replace(generatedSource.SourceText.ToString(), StampReplacement);
    }

    public static void VerifyIdentical(GeneratorRunResult result1, GeneratorRunResult result2)
    {
        IDictionary<string, string> unmatchedGeneratedSources = new Dictionary<string, string>();

        foreach (GeneratedSourceResult generatedSource in result1.GeneratedSources)
        {
            unmatchedGeneratedSources[generatedSource.HintName] = generatedSource.SourceText.ToString();
        }

        foreach (GeneratedSourceResult generatedSource in result2.GeneratedSources)
        {
            Assert.Contains(generatedSource.HintName, unmatchedGeneratedSources);
            Assert.Equal(generatedSource.SourceText.ToString(), unmatchedGeneratedSources[generatedSource.HintName]);

            unmatchedGeneratedSources.Remove(generatedSource.HintName);
        }

        Assert.Equal(result1.GeneratedSources.Length, result2.GeneratedSources.Length);
    }

    private static Regex StampRegex { get; } = new(@"(?<header>This file was generated by SharpMeasures\.Generators ).+", RegexOptions.ExplicitCapture);
    private static string StampReplacement { get; } = "${header}<stamp>";
}
