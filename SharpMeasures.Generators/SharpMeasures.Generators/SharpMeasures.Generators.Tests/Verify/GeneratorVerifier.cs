﻿namespace SharpMeasures.Generators.Tests.Verify;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.DriverUtility;

using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using VerifyXunit;

using Xunit;

[UsesVerify]
internal class GeneratorVerifier
{
    public static GeneratorVerifier Construct<TGenerator>(string source) where TGenerator : IIncrementalGenerator, new()
        => Construct(source, new TGenerator());

    public static GeneratorVerifier Construct(string source, IIncrementalGenerator generator)
        => new(DriverConstruction.ConstructAndRun(source, generator, ProjectPath.Path + @"\Documentation"));

    private GeneratorDriverRunResult RunResult { get; }

    private IEnumerable<GeneratedSourceResult> Output { get; }
    private int OutputCount { get; }

    private ImmutableArray<Diagnostic> Diagnostics => RunResult.Diagnostics;

    private GeneratorVerifier(GeneratorDriver driver)
    {
        RunResult = driver.GetRunResult();

        Output = RunResult.Results.SelectMany(static (result) => result.GeneratedSources);

        OutputCount = Output.Count();
    }

    public GeneratorVerifier AssertNoSourceGenerated()
    {
        Assert.Empty(Output);

        return this;
    }
    public GeneratorVerifier AssertNoDiagnosticsReported()
    {
        Assert.Empty(Diagnostics);

        return this;
    }

    public GeneratorVerifier AssertSomeSourceGenerated()
    {
        Assert.NotEmpty(Output);

        return this;
    }

    public GeneratorVerifier AssertSomeDiagnosticsReported()
    {
        Assert.NotEmpty(Diagnostics);

        return this;
    }

    public GeneratorVerifier AssertSourceCount(int expectedSourceCount)
    {
        Assert.Equal(expectedSourceCount, OutputCount);

        return this;
    }

    public GeneratorVerifier AssertDiagnosticsCount(int expectedDiagnosticsCount)
    {
        Assert.Equal(expectedDiagnosticsCount, Diagnostics.Length);

        return this;
    }

    public GeneratorVerifier AssertAllListedSourcesNamesGenerated(IEnumerable<string> expectedSourceNames)
    {
        foreach (string file in expectedSourceNames)
        {
            Assert.Contains(file, Output.Select(static (result) => result.HintName));
        }

        return this;
    }

    public GeneratorVerifier AssertAllListedDiagnosticsIDsReported(IEnumerable<string> expectedDiagnosticIDs)
    {
        foreach (string diagnosticID in expectedDiagnosticIDs)
        {
            Assert.Contains(diagnosticID, Diagnostics.Select(static (diagnostic) => diagnostic.Id));
        }

        return this;
    }

    public GeneratorVerifier AssertNoListedSourceNameGenerated(IEnumerable<string> forbiddenSourceNames)
    {
        foreach (string file in forbiddenSourceNames)
        {
            Assert.DoesNotContain(file, Output.Select(static (result) => result.HintName));
        }

        return this;
    }

    public GeneratorVerifier AssertNoListedDiagnosticIDsReported(IEnumerable<string> forbiddenDiagnosticIDs)
    {
        foreach (string diagnosticID in forbiddenDiagnosticIDs)
        {
            Assert.DoesNotContain(diagnosticID, Diagnostics.Select(static (diagnostic) => diagnostic.Id));
        }

        return this;
    }

    public GeneratorVerifier AssertExactlyListedSourceNamesGenerated(IEnumerable<string> expectedSourceNames)
    {
        AssertAllListedSourcesNamesGenerated(expectedSourceNames);
        Assert.Equal(OutputCount, expectedSourceNames.Count());

        return this;
    }

    public GeneratorVerifier AssertExactlyListedDiagnosticsIDsReported(IEnumerable<string> expectedDiagnosticIDs)
    {
        AssertAllListedDiagnosticsIDsReported(expectedDiagnosticIDs);
        Assert.Equal(Diagnostics.Length, expectedDiagnosticIDs.Count());

        return this;
    }

    public GeneratorVerifier AssertIdenticalSources(GeneratorVerifier expectedSources)
    {
        HashSet<(string, string, string)> unmatchedOutput = new();

        foreach (GeneratorRunResult result in RunResult.Results)
        {
            foreach (GeneratedSourceResult output in result.GeneratedSources)
            {
                unmatchedOutput.Add(createIdentifier(result, output));
            }
        }

        foreach (GeneratorRunResult result in expectedSources.RunResult.Results)
        {
            foreach (GeneratedSourceResult output in result.GeneratedSources)
            {
                (string, string, string) identifier = createIdentifier(result, output);

                Assert.Contains(identifier, unmatchedOutput);

                unmatchedOutput.Remove(identifier);
            }
        }

        Assert.Empty(unmatchedOutput);

        return this;

        static (string, string, string) createIdentifier(GeneratorRunResult result, GeneratedSourceResult output)
            => (result.Generator.GetGeneratorType().ToString(), output.HintName, removeStamp(output));

        static string removeStamp(GeneratedSourceResult generatedSource) => StampRegex.Replace(generatedSource.SourceText.ToString(), StampReplacement);
    }

    public GeneratorVerifier AssertIdenticalDiagnostics(GeneratorVerifier expectedDiagnostics)
    {
        HashSet<(string, Diagnostic)> unmatchedDiagnostic = new();

        foreach (GeneratorRunResult result in RunResult.Results)
        {
            foreach (Diagnostic diagnostic in result.Diagnostics)
            {
                unmatchedDiagnostic.Add(createDiagnosticIdentifier(result, diagnostic));
            }
        }

        foreach (GeneratorRunResult result in expectedDiagnostics.RunResult.Results)
        {
            foreach (Diagnostic diagnostic in result.Diagnostics)
            {
                (string, Diagnostic) identifier = createDiagnosticIdentifier(result, diagnostic);

                Assert.Contains(identifier, unmatchedDiagnostic);

                unmatchedDiagnostic.Remove(identifier);
            }
        }

        Assert.Empty(unmatchedDiagnostic);

        return this;

        static (string, Diagnostic) createDiagnosticIdentifier(GeneratorRunResult result, Diagnostic diagnostic)
            => (result.Generator.GetGeneratorType().ToString(), diagnostic);
    }

    public GeneratorVerifier AssertIdenticalSourcesAndDiagnostics(GeneratorVerifier expected)
    {
        AssertIdenticalSources(expected);
        AssertIdenticalDiagnostics(expected);

        return this;
    }

    public async Task Verify()
    {
        await Verifier.Verify(RunResult);
    }

    private static Regex StampRegex { get; } = new(@"(?<header>This file was generated by SharpMeasures\.Generators ).+", RegexOptions.ExplicitCapture);
    private static string StampReplacement { get; } = "${header}<stamp>";
}