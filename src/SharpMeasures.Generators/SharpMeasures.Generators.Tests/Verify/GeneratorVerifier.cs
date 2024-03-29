﻿namespace SharpMeasures.Generators.Tests.Verify;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;

using SharpMeasures.Generators.DriverUtility;

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using VerifyXunit;

using Xunit;

internal readonly record struct GeneratorVerifierSettings(bool AssertNoDiagnosticsFromGeneratedCode, bool AssertNoErrorsOrWarningsFromTestCode)
{
    public static GeneratorVerifierSettings AllAssertions { get; } = new(true, true);
    public static GeneratorVerifierSettings TestCodeAssertions { get; } = new(false, true);
    public static GeneratorVerifierSettings GeneratedCodeAssertions { get; } = new(true, false);
    public static GeneratorVerifierSettings NoAssertions { get; } = new(false, false);
}

[UsesVerify]
internal sealed class GeneratorVerifier
{
    public static GeneratorVerifier Construct<TGenerator>(string source) where TGenerator : IIncrementalGenerator, new() => Construct<TGenerator>(source, DriverConstructionConfiguration.Empty);
    public static GeneratorVerifier Construct<TGenerator>(string source, DriverConstructionConfiguration configuration) where TGenerator : IIncrementalGenerator, new() => Construct(source, new TGenerator(), configuration);
    public static GeneratorVerifier Construct<TGenerator>(string source, GeneratorVerifierSettings settings) where TGenerator : IIncrementalGenerator, new() => Construct<TGenerator>(source, settings, DriverConstructionConfiguration.Empty);
    public static GeneratorVerifier Construct<TGenerator>(string source, GeneratorVerifierSettings settings, DriverConstructionConfiguration configuration) where TGenerator : IIncrementalGenerator, new() => Construct(source, new TGenerator(), settings, configuration);

    public static GeneratorVerifier Construct(string source, IIncrementalGenerator generator) => Construct(source, generator, DriverConstructionConfiguration.Empty);
    public static GeneratorVerifier Construct(string source, IIncrementalGenerator generator, DriverConstructionConfiguration configuration) => Construct(source, generator, GeneratorVerifierSettings.AllAssertions, configuration);
    public static GeneratorVerifier Construct(string source, IIncrementalGenerator generator, GeneratorVerifierSettings settings) => Construct(source, generator, settings, DriverConstructionConfiguration.Empty);
    public static GeneratorVerifier Construct(string source, IIncrementalGenerator generator, GeneratorVerifierSettings settings, DriverConstructionConfiguration configuration)
    {
        var driver = DriverConstruction.ConstructAndRun(source, generator, configuration, out var compilation);

        return new(source, driver, compilation, settings);
    }

    public static GeneratorVerifier Construct(string source, GeneratorDriver driver, Compilation compilation) => Construct(source, driver, compilation, GeneratorVerifierSettings.AllAssertions);
    public static GeneratorVerifier Construct(string source, GeneratorDriver driver, Compilation compilation, GeneratorVerifierSettings settings) => new(source, driver, compilation, settings);

    private string Source { get; }

    private GeneratorDriverRunResult RunResult { get; }
    private Compilation Compilation { get; }

    private IEnumerable<GeneratedSourceResult> Output { get; }
    private int OutputCount { get; }

    private ImmutableArray<Diagnostic> Diagnostics => RunResult.Diagnostics;

    private GeneratorVerifier(string source, GeneratorDriver driver, Compilation compilation, GeneratorVerifierSettings settings)
    {
        Source = source;

        RunResult = driver.GetRunResult();
        Compilation = compilation;

        Output = RunResult.Results.SelectMany(static (result) => result.GeneratedSources);

        OutputCount = Output.Count();

        AssertNoGeneratorExceptions();

        if (settings.AssertNoDiagnosticsFromGeneratedCode)
        {
            AssertNoDiagnosticsFromGeneratedCode();
        }

        if (settings.AssertNoErrorsOrWarningsFromTestCode)
        {
            AssertNoErrorsOrWarningsFromTestCode();
        }
    }

    private void AssertNoGeneratorExceptions() => Assert.Empty(RunResult.Results.Select(static (result) => result.Exception).Where(static (exception) => exception is not null));
    private void AssertNoDiagnosticsFromGeneratedCode() => Assert.Empty(Compilation.GetDiagnostics().Where(static (diagnostics) => diagnostics.Location.SourceTree?.FilePath.Length > 0));
    private void AssertNoErrorsOrWarningsFromTestCode() => Assert.Empty(Compilation.GetDiagnostics().Where(static (diagnostics) => diagnostics.Severity is DiagnosticSeverity.Error or DiagnosticSeverity.Warning && string.IsNullOrEmpty(diagnostics.Location.SourceTree?.FilePath)));

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

    public GeneratorVerifier AssertAllDiagnosticsValidLocation()
    {
        foreach (var diagnostic in Diagnostics)
        {
            Assert.False(diagnostic.Location.SourceSpan.IsEmpty);
        }

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

    public GeneratorVerifier AssertAllListedSourceNamesGenerated(IEnumerable<string> expectedSourceNames)
    {
        foreach (var file in expectedSourceNames)
        {
            Assert.Contains(file, Output.Select(static (result) => result.HintName));
        }

        return this;
    }

    public GeneratorVerifier AssertAllListedSourceNamesGenerated(params string[] expectedSourceNames) => AssertAllListedSourceNamesGenerated(expectedSourceNames as IEnumerable<string>);

    public GeneratorVerifier AssertAllListedDiagnosticsIDsReported(IEnumerable<string> expectedDiagnosticIDs)
    {
        foreach (var diagnosticID in expectedDiagnosticIDs)
        {
            Assert.Contains(diagnosticID, Diagnostics.Select(static (diagnostic) => diagnostic.Id));
        }

        return this;
    }

    public GeneratorVerifier AssertAllListedDiagnosticsIDsReported(params string[] expectedDiagnosticIDs) => AssertAllListedDiagnosticsIDsReported(expectedDiagnosticIDs as IEnumerable<string>);

    public GeneratorVerifier AssertNoListedSourceNameGenerated(IEnumerable<string> forbiddenSourceNames)
    {
        foreach (var file in forbiddenSourceNames)
        {
            Assert.DoesNotContain(file, Output.Select(static (result) => result.HintName));
        }

        return this;
    }

    public GeneratorVerifier AssertNoListedSourceNameGenerated(params string[] forbiddenSourceNames) => AssertNoListedSourceNameGenerated(forbiddenSourceNames as IEnumerable<string>);
    public GeneratorVerifier AssertNoMatchingSourceNameGenerated(string forbiddenSourceNameRegexPattern) => AssertNoMatchingSourceNameGenerated(new Regex(forbiddenSourceNameRegexPattern));

    public GeneratorVerifier AssertNoMatchingSourceNameGenerated(Regex forbiddenSourceNamePattern)
    {
        Assert.Empty(Output.Where((result) => forbiddenSourceNamePattern.IsMatch(result.HintName)));

        return this;
    }

    public GeneratorVerifier AssertNoListedDiagnosticIDsReported(IEnumerable<string> forbiddenDiagnosticIDs)
    {
        foreach (var diagnosticID in forbiddenDiagnosticIDs)
        {
            Assert.DoesNotContain(diagnosticID, Diagnostics.Select(static (diagnostic) => diagnostic.Id));
        }

        return this;
    }

    public GeneratorVerifier AssertExactlyListedSourceNamesGenerated(IEnumerable<string> expectedSourceNames)
    {
        AssertAllListedSourceNamesGenerated(expectedSourceNames);
        Assert.Equal(expectedSourceNames.Count(), OutputCount);

        return this;
    }

    public GeneratorVerifier AssertExactlyListedDiagnosticsIDsReported(IEnumerable<string> expectedDiagnosticIDs)
    {
        AssertAllListedDiagnosticsIDsReported(expectedDiagnosticIDs);
        Assert.Equal(expectedDiagnosticIDs.Count(), Diagnostics.Length);

        return this;
    }

    public GeneratorVerifier AssertSpecificDiagnosticsLocation(string diagnosticsID, TextSpan expectedLocation)
    {
        Assert.Contains(diagnosticsID, Diagnostics.Select(static (diagnostic) => diagnostic.Id));

        var specifiedDiagnostics = Diagnostics.Where((diagnostic) => diagnostic.Id == diagnosticsID).Single();

        var expectedText = Source[expectedLocation.Start..expectedLocation.End];
        var actualText = Source[specifiedDiagnostics.Location.SourceSpan.Start..specifiedDiagnostics.Location.SourceSpan.End];

        Assert.Equal(expectedText, actualText);
        Assert.Equal(expectedLocation, specifiedDiagnostics.Location.SourceSpan);

        return this;
    }

    public GeneratorVerifier AssertDiagnosticsLocation(TextSpan expectedLocation)
    {
        if (Diagnostics.Length is not 1)
        {
            throw new NotSupportedException("More than 1 diagnostics detected, specify multiple locations");
        }

        var expectedText = Source[expectedLocation.Start..expectedLocation.End];
        var actualText = Source[Diagnostics[0].Location.SourceSpan.Start..Diagnostics[0].Location.SourceSpan.End];

        Assert.Equal(expectedText, actualText);
        Assert.Equal(expectedLocation, Diagnostics[0].Location.SourceSpan);

        return this;
    }

    public GeneratorVerifier AssertDiagnosticsLocation(IEnumerable<TextSpan> expectedLocations)
    {
        var index = 0;

        foreach (var expectedLocation in expectedLocations)
        {
            var expectedText = Source[expectedLocation.Start..expectedLocation.End];
            var actualText = Source[Diagnostics[index].Location.SourceSpan.Start..Diagnostics[index].Location.SourceSpan.End];

            Assert.Equal(expectedText, actualText);
            Assert.Equal(expectedLocation, Diagnostics[index].Location.SourceSpan);

            index += 1;
        }

        return this;
    }

    public GeneratorVerifier AssertDiagnosticsLocation(params TextSpan[] expectedLocations) => AssertDiagnosticsLocation(expectedLocations as IEnumerable<TextSpan>);

    public GeneratorVerifier AssertIdenticalSpecifiedSources(GeneratorVerifier expectedSources, Regex sourceNamePattern)
    {
        HashSet<(string, string, string)> unmatchedOutput = new();

        foreach (var result in RunResult.Results)
        {
            foreach (var output in result.GeneratedSources.Where((source) => sourceNamePattern.IsMatch(Source)))
            {
                unmatchedOutput.Add(createIdentifier(result, output));
            }
        }

        foreach (var result in expectedSources.RunResult.Results)
        {
            foreach (var output in result.GeneratedSources.Where((source) => sourceNamePattern.IsMatch(Source)))
            {
                var identifier = createIdentifier(result, output);

                Assert.Contains(identifier, unmatchedOutput);

                unmatchedOutput.Remove(identifier);
            }
        }

        Assert.Empty(unmatchedOutput);

        return this;

        static (string, string, string) createIdentifier(GeneratorRunResult result, GeneratedSourceResult output) => (result.Generator.GetGeneratorType().ToString(), output.HintName, removeStamp(output));

        static string removeStamp(GeneratedSourceResult generatedSource) => StampRegex.Replace(generatedSource.SourceText.ToString(), StampReplacement);
    }

    public GeneratorVerifier AssertIdenticalSpecifiedSources(GeneratorVerifier expectedSources, params string[] sourceNames)
    {
        return AssertIdenticalSpecifiedSources(expectedSources, sourceNames as IEnumerable<string>);
    }

    public GeneratorVerifier AssertIdenticalSpecifiedSources(GeneratorVerifier expectedSources, IEnumerable<string> sourceNames)
    {
        HashSet<string> includedSourceNames = new(sourceNames);

        HashSet<(string, string, string)> unmatchedOutput = new();

        foreach (var result in RunResult.Results)
        {
            foreach (var output in result.GeneratedSources.Where((source) => sourceNames.Contains(source.HintName)))
            {
                unmatchedOutput.Add(createIdentifier(result, output));
            }
        }

        foreach (var result in expectedSources.RunResult.Results)
        {
            foreach (var output in result.GeneratedSources.Where((source) => sourceNames.Contains(source.HintName)))
            {
                var identifier = createIdentifier(result, output);

                Assert.Contains(identifier, unmatchedOutput);

                unmatchedOutput.Remove(identifier);
            }
        }

        Assert.Empty(unmatchedOutput);

        return this;

        static (string, string, string) createIdentifier(GeneratorRunResult result, GeneratedSourceResult output) => (result.Generator.GetGeneratorType().ToString(), output.HintName, removeStamp(output));

        static string removeStamp(GeneratedSourceResult generatedSource) => StampRegex.Replace(generatedSource.SourceText.ToString(), StampReplacement);
    }

    public GeneratorVerifier AssertIdenticalSources(GeneratorVerifier expectedSources)
    {
        return AssertIdenticalSpecifiedSources(expectedSources, new Regex(".*"));
    }

    public GeneratorVerifier AssertIdenticalSources<TGenerator>(string identical) where TGenerator : IIncrementalGenerator, new() => AssertIdenticalSources(Construct<TGenerator>(identical));

    public GeneratorVerifier AssertIdenticalDiagnostics(GeneratorVerifier expectedDiagnostics)
    {
        HashSet<(string, Diagnostic)> unmatchedDiagnostic = new();

        foreach (var result in RunResult.Results)
        {
            foreach (var diagnostic in result.Diagnostics)
            {
                unmatchedDiagnostic.Add(createDiagnosticIdentifier(result, diagnostic));
            }
        }

        foreach (var result in expectedDiagnostics.RunResult.Results)
        {
            foreach (var diagnostic in result.Diagnostics)
            {
                var identifier = createDiagnosticIdentifier(result, diagnostic);

                Assert.Contains(identifier, unmatchedDiagnostic);

                unmatchedDiagnostic.Remove(identifier);
            }
        }

        Assert.Empty(unmatchedDiagnostic);

        return this;

        static (string, Diagnostic) createDiagnosticIdentifier(GeneratorRunResult result, Diagnostic diagnostic) => (result.Generator.GetGeneratorType().ToString(), diagnostic);
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

    public async Task VerifySource()
    {
        await Verifier.Verify(Output.Select(static (result) => result.SourceText));
    }

    private async Task VerifyListedSourceNames(IEnumerable<string> sourceNames)
    {
        HashSet<string> includedNames = new(sourceNames);

        await Verifier.Verify(Output.Where((result) => includedNames.Contains(result.HintName)));
    }

    public async Task VerifyMatchingSourceNames(IEnumerable<string> sourceNames)
    {
        var sourceNamesList = sourceNames.ToList();

        HashSet<int> unmatchedNamesIndices = new(sourceNamesList.Count);

        for (var i = 0; i < sourceNamesList.Count; i++)
        {
            unmatchedNamesIndices.Add(i);
        }

        var matchingSourceNames = Output.Select(static (result) => result.HintName).Where(matches).ToList();

        var unmatchedSourceNames = unmatchedNamesIndices.Select((index) => sourceNamesList[index]).ToList();

        Assert.Empty(unmatchedSourceNames);
        Assert.NotEmpty(matchingSourceNames);

        await VerifyListedSourceNames(matchingSourceNames).ConfigureAwait(false);

        bool matches(string sourceName)
        {
            for (var i = 0; i < sourceNamesList.Count; i++)
            {
                if (sourceNamesList[i] == sourceName)
                {
                    unmatchedNamesIndices.Remove(i);

                    return true;
                }
            }

            return false;
        }
    }

    public async Task VerifyMatchingSourceNames(params string[] sourceNames)
    {
        await VerifyMatchingSourceNames(sourceNames as IEnumerable<string>).ConfigureAwait(false);
    }

    public async Task VerifyMatchingSourceNames(IEnumerable<Regex> patterns)
    {
        var patternsList = patterns.ToList();

        HashSet<int> unmatchedPatternIndices = new(patternsList.Count);

        for (var i = 0; i < patternsList.Count; i++)
        {
            unmatchedPatternIndices.Add(i);
        }

        var matchingSourceNames = Output.Select(static (result) => result.HintName).Where(matches).ToList();

        var unmatchedPatterns = unmatchedPatternIndices.Select((index) => patternsList[index]).ToList();

        Assert.Empty(unmatchedPatterns);
        Assert.NotEmpty(matchingSourceNames);

        await VerifyListedSourceNames(matchingSourceNames).ConfigureAwait(false);

        bool matches(string sourceName)
        {
            for (var i = 0; i < patternsList.Count; i++)
            {
                if (patternsList[i].IsMatch(sourceName))
                {
                    unmatchedPatternIndices.Remove(i);

                    return true;
                }
            }

            return false;
        }
    }

    public async Task VerifyMatchingSourceNames(params Regex[] patterns) => await VerifyMatchingSourceNames(patterns as IEnumerable<Regex>).ConfigureAwait(false);

    public async Task VerifyMatchingSourceNames(Regex pattern)
    {
        var matchingSourceNames = Output.Select(static (result) => result.HintName).Where((sourceName) => pattern.IsMatch(sourceName));

        Assert.NotEmpty(matchingSourceNames);

        await VerifyListedSourceNames(matchingSourceNames).ConfigureAwait(false);
    }

    public async Task VerifyDiagnostics() => await Verifier.Verify(Diagnostics);
    public async Task VerifyDiagnostics(object parameter) => await VerifyDiagnostics(new[] { parameter }).ConfigureAwait(false);
    public async Task VerifyDiagnostics(object?[] parameter) => await Verifier.Verify(Diagnostics).UseParameters(parameter);

    public async Task VerifyListedDiagnostics(IEnumerable<string> diagnosticIDs)
    {
        HashSet<string> includedIDs = new(diagnosticIDs);

        var filteredDiagnostics = Diagnostics.Where((diagnostics) => includedIDs.Contains(diagnostics.Id));

        await Verifier.Verify(filteredDiagnostics);
    }

    public async Task VerifyListedDiagnostics(params string[] diagnosticIDs) => await VerifyListedDiagnostics(diagnosticIDs as IEnumerable<string>).ConfigureAwait(false);

    private static Regex StampRegex { get; } = new(@"(?<header>This file was generated by SharpMeasures\.Generators(?:(\.[a-zA-Z]*)?) ).+", RegexOptions.ExplicitCapture);
    private static string StampReplacement { get; } = "${header}<stamp>";
}
