namespace SharpMeasures.Generators.Tests.Verify;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Tests.Utility;

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
        => AssertNoOutput(GeneratorDriverUtility.ConstructAndRunDriver(source, generator));

    public static void AssertNoOutput(GeneratorDriver driver)
    {
        foreach (GeneratorRunResult result in driver.GetRunResult().Results)
        {
            AssertNoOutput(result);
        }
    }

    public static void AssertNoOutput(GeneratorRunResult result) => Assert.Empty(result.GeneratedSources);

    public static void AssertSomeOutput<TGenerator>(string source) where TGenerator : IIncrementalGenerator, new()
        => AssertSomeOutput(source, new TGenerator());

    public static void AssertSomeOutput(string source, IIncrementalGenerator generator)
        => AssertSomeOutput(GeneratorDriverUtility.ConstructAndRunDriver(source, generator));

    public static void AssertSomeOutput(GeneratorDriver driver)
    {
        bool anyOutput = false;

        foreach (GeneratorRunResult result in driver.GetRunResult().Results)
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
        => AssertNumberOfGeneratedFiles(GeneratorDriverUtility.ConstructAndRunDriver(source, generator), expected);

    public static void AssertNumberOfGeneratedFiles(GeneratorDriver driver, int expected)
    {
        int actual = 0;

        foreach (GeneratorRunResult result in driver.GetRunResult().Results)
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
        => AssertAllListedFilesGenerated(GeneratorDriverUtility.ConstructAndRunDriver(source, generator), files);

    public static void AssertAllListedFilesGenerated(GeneratorDriver driver, params string[] files)
    {
        int matchingFiles = 0;

        foreach (GeneratorRunResult result in driver.GetRunResult().Results)
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
        => AssertOnlyListedFilesGenerated(GeneratorDriverUtility.ConstructAndRunDriver(source, generator), files);

    public static void AssertOnlyListedFilesGenerated(GeneratorDriver driver, params string[] files)
    {
        foreach (GeneratorRunResult result in driver.GetRunResult().Results)
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
        => AssertNoListedFileGenerated(GeneratorDriverUtility.ConstructAndRunDriver(source, generator), files);

    public static void AssertNoListedFileGenerated(GeneratorDriver driver, params string[] files)
    {
        foreach (GeneratorRunResult result in driver.GetRunResult().Results)
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
        => VerifyMatch(GeneratorDriverUtility.ConstructAndRunDriver(source, generator));

    public static Task VerifyMatch(GeneratorDriver driver)
    {
        AssertSomeOutput(driver);
        return Verifier.Verify(driver);
    }

    public static Task VerifyMatch<TGenerator>(string source, params string[] files) where TGenerator : IIncrementalGenerator, new()
        => VerifyMatch(source, new TGenerator(), files);

    public static Task VerifyMatch(string source, IIncrementalGenerator generator, params string[] files)
        => VerifyMatch(GeneratorDriverUtility.ConstructAndRunDriver(source, generator), files);

    public static Task VerifyMatch(GeneratorDriver driver, params string[] files)
    {
        List<Task> tasks = new();

        foreach (GeneratorRunResult result in driver.GetRunResult().Results)
        {
            foreach(GeneratedSourceResult generatedSource in result.GeneratedSources)
            {
                if (files.Contains(generatedSource.HintName))
                {
                    tasks.Add(Verifier.Verify(generatedSource.SourceText.ToString()));
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
                tasks.Add(Verifier.Verify(generatedSource.SourceText.ToString()));
            }
        }

        Assert.Equal(files.Length, tasks.Count);
        return Task.WhenAll(tasks);
    }

    public static void VerifyIdentical<TGenerator>(string source1, string source2) where TGenerator : IIncrementalGenerator, new()
        => VerifyIdentical(source1, source2, new TGenerator());

    public static void VerifyIdentical(string source1, string source2, IIncrementalGenerator generator)
        => VerifyIdentical(GeneratorDriverUtility.ConstructAndRunDriver(source1, generator), source2, generator);

    public static void VerifyIdentical<TGenerator>(GeneratorDriver driver1, string source2) where TGenerator : IIncrementalGenerator, new()
        => VerifyIdentical(driver1, source2, new TGenerator());

    public static void VerifyIdentical(GeneratorDriver driver1, string source2, IIncrementalGenerator generator)
        => VerifyIdentical(driver1, GeneratorDriverUtility.ConstructAndRunDriver(source2, generator).GetRunResult());

    public static void VerifyIdentical(GeneratorDriver driver1, GeneratorDriverRunResult results2)
    {
        IDictionary<(string, string), string> unmatchedGeneratedSources = new Dictionary<(string, string), string>();

        foreach (GeneratorRunResult result in driver1.GetRunResult().Results)
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
