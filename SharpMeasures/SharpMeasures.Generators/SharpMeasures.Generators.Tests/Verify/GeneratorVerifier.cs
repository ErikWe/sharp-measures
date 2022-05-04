namespace SharpMeasures.Generators.Tests.Verify;

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

    public GeneratorVerifier NoOutput()
    {
        Assert.Empty(Output);

        return this;
    }
    public GeneratorVerifier NoDiagnostics()
    {
        Assert.Empty(Diagnostics);

        return this;
    }

    public GeneratorVerifier SomeOutput()
    {
        Assert.NotEmpty(Output);

        return this;
    }

    public GeneratorVerifier SomeDiagnostics()
    {
        Assert.NotEmpty(Diagnostics);

        return this;
    }

    public GeneratorVerifier ExactOutputCount(int expected)
    {
        Assert.Equal(expected, OutputCount);

        return this;
    }

    public GeneratorVerifier ExactDiagnosticsCount(int expected)
    {
        Assert.Equal(expected, Diagnostics.Length);

        return this;
    }

    public GeneratorVerifier AllListedFilesGenerated(IEnumerable<string> files)
    {
        foreach (string file in files)
        {
            Assert.Contains(file, Output.Select(static (result) => result.HintName));
        }

        return this;
    }

    public GeneratorVerifier AllListedDiagnosticIDsReported(IEnumerable<string> diagnosticIDs)
    {
        foreach (string diagnosticID in diagnosticIDs)
        {
            Assert.Contains(diagnosticID, Diagnostics.Select(static (diagnostic) => diagnostic.Id));
        }

        return this;
    }

    public GeneratorVerifier NoListedFileGenerated(IEnumerable<string> files)
    {
        foreach (string file in files)
        {
            Assert.DoesNotContain(file, Output.Select(static (result) => result.HintName));
        }

        return this;
    }

    public GeneratorVerifier NoListedDiagnosticIDReported(IEnumerable<string> diagnosticIDs)
    {
        foreach (string diagnosticID in diagnosticIDs)
        {
            Assert.DoesNotContain(diagnosticID, Diagnostics.Select(static (diagnostic) => diagnostic.Id));
        }

        return this;
    }

    public GeneratorVerifier AllAndOnlyListedFilesGenerated(IEnumerable<string> files)
    {
        AllListedFilesGenerated(files);
        Assert.Equal(OutputCount, files.Count());

        return this;
    }

    public GeneratorVerifier AllAndOnlyListedDiagnosticIDsReported(IEnumerable<string> diagnosticIDs)
    {
        AllListedDiagnosticIDsReported(diagnosticIDs);
        Assert.Equal(Diagnostics.Length, diagnosticIDs.Count());

        return this;
    }

    public GeneratorVerifier IdenticalOutputTo(GeneratorVerifier expected)
    {
        HashSet<(string, string, string)> unmatchedOutput = new();

        foreach (GeneratorRunResult result in RunResult.Results)
        {
            foreach (GeneratedSourceResult output in result.GeneratedSources)
            {
                unmatchedOutput.Add(createIdentifier(result, output));
            }
        }

        foreach (GeneratorRunResult result in expected.RunResult.Results)
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

    public GeneratorVerifier IdenticalDiagnosticsTo(GeneratorVerifier expected)
    {
        HashSet<(string, Diagnostic)> unmatchedDiagnostic = new();

        foreach (GeneratorRunResult result in RunResult.Results)
        {
            foreach (Diagnostic diagnostic in result.Diagnostics)
            {
                unmatchedDiagnostic.Add(createDiagnosticIdentifier(result, diagnostic));
            }
        }

        foreach (GeneratorRunResult result in expected.RunResult.Results)
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

    public GeneratorVerifier IdenticalTo(GeneratorVerifier expected)
    {
        IdenticalOutputTo(expected);
        IdenticalDiagnosticsTo(expected);

        return this;
    }

    public async Task Verify()
    {
        await Verifier.Verify(RunResult);
    }

    private static Regex StampRegex { get; } = new(@"(?<header>This file was generated by SharpMeasures\.Generators ).+", RegexOptions.ExplicitCapture);
    private static string StampReplacement { get; } = "${header}<stamp>";
}
