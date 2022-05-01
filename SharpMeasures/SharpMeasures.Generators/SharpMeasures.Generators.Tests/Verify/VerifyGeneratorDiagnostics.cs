namespace SharpMeasures.Generators.Tests.Verify;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Tests.Utility;

using System.Collections.Generic;
using System.Threading.Tasks;

using VerifyXunit;

using Xunit;

[UsesVerify]
internal class VerifyGeneratorDiagnostics
{
    public static void AssertNoDiagnostics<TGenerator>(string source) where TGenerator : IIncrementalGenerator, new()
        => AssertNoDiagnostics(source, new TGenerator());

    public static void AssertNoDiagnostics(string source, IIncrementalGenerator generator)
        => AssertNoDiagnostics(GeneratorDriverUtility.ConstructAndRunDriver(source, generator).GetRunResult());

    public static void AssertNoDiagnostics(GeneratorDriverRunResult results)
    {
        foreach (GeneratorRunResult result in results.Results)
        {
            AssertNoDiagnostics(result);
        }
    }

    public static void AssertNoDiagnostics(GeneratorRunResult result)
    {
        if (result.Exception is null)
        {
            Assert.Empty(result.Diagnostics);
        }
    }

    public static void AssertSomeDiagnostics<TGenerator>(string source) where TGenerator : IIncrementalGenerator, new()
        => AssertSomeDiagnostics(source, new TGenerator());

    public static void AssertSomeDiagnostics(string source, IIncrementalGenerator generator)
        => AssertSomeDiagnostics(GeneratorDriverUtility.ConstructAndRunDriver(source, generator).GetRunResult());

    public static void AssertSomeDiagnostics(GeneratorDriverRunResult results)
    {
        bool anyDiagnostics = false;

        foreach (GeneratorRunResult result in results.Results)
        {
            if (result.Exception is not null)
            {
                continue;
            }

            if (result.Diagnostics.Length > 0)
            {
                anyDiagnostics = true;
                break;
            }
        }

        Assert.True(anyDiagnostics);
    }

    public static void AssertSomeDiagnostics(GeneratorRunResult result)
    {
        if (result.Exception is null)
        {
            Assert.NotEmpty(result.Diagnostics);
        }
    }

    public static void AssertNumberOfDiagnostics<TGenerator>(string source, int expected) where TGenerator : IIncrementalGenerator, new()
        => AssertNumberOfDiagnostics(source, new TGenerator(), expected);

    public static void AssertNumberOfDiagnostics(string source, IIncrementalGenerator generator, int expected)
        => AssertNumberOfDiagnostics(GeneratorDriverUtility.ConstructAndRunDriver(source, generator).GetRunResult(), expected);

    public static void AssertNumberOfDiagnostics(GeneratorDriverRunResult results, int expected)
    {
        int actual = 0;

        foreach (GeneratorRunResult result in results.Results)
        {
            if (result.Exception is not null)
            {
                continue;
            }

            actual += result.Diagnostics.Length;
        }

        Assert.Equal(expected, actual);
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Assertions", "xUnit2000", Justification = "The constant value is the actual value.")]
    public static void AssertNumberOfDiagnostics(GeneratorRunResult result, int expected)
    {
        if (result.Exception is not null)
        {
            Assert.Equal(expected, 0);
        }
        else
        {
            Assert.Equal(expected, result.Diagnostics.Length);
        }
    }

    public static void AssertIncludesSpecifiedDiagnostics<TGenerator>(string source, IReadOnlyCollection<string> diagnosticIDs)
        where TGenerator : IIncrementalGenerator, new()
        => AssertIncludesSpecifiedDiagnostics(source, new TGenerator(), diagnosticIDs);

    public static void AssertIncludesSpecifiedDiagnostics(string source, IIncrementalGenerator generator, IReadOnlyCollection<string> diagnosticIDs)
        => AssertIncludesSpecifiedDiagnostics(GeneratorDriverUtility.ConstructAndRunDriver(source, generator).GetRunResult(), diagnosticIDs);

    public static void AssertIncludesSpecifiedDiagnostics(GeneratorDriverRunResult results, IReadOnlyCollection<string> diagnosticIDs)
    {
        HashSet<string> generatedIDs = new();

        foreach (GeneratorRunResult result in results.Results)
        {
            foreach (Diagnostic diagnostic in result.Diagnostics)
            {
                generatedIDs.Add(diagnostic.Id);
            }
        }

        foreach (string diagnosticID in diagnosticIDs)
        {
            Assert.Contains(diagnosticID, generatedIDs);
        }
    }

    public static void AssertIncludesSpecifiedDiagnostics(GeneratorRunResult result, IReadOnlyCollection<string> diagnosticIDs)
    {
        HashSet<string> generatedIDs = new(result.Diagnostics.Length);

        foreach (Diagnostic diagnostic in result.Diagnostics)
        {
            generatedIDs.Add(diagnostic.Id);
        }

        foreach (string diagnosticID in diagnosticIDs)
        {
            Assert.Contains(diagnosticID, generatedIDs);
        }
    }

    public static Task VerifyMatch<TGenerator>(string source) where TGenerator : IIncrementalGenerator, new()
        => VerifyMatch(source, new TGenerator());

    public static Task VerifyMatch(string source, IIncrementalGenerator generator)
        => VerifyMatch(GeneratorDriverUtility.ConstructAndRunDriver(source, generator).GetRunResult());

    public static Task VerifyMatch(GeneratorDriverRunResult results)
    {
        List<Task> tasks = new();

        foreach (GeneratorRunResult result in results.Results)
        {
            tasks.Add(VerifyMatch(result));
        }

        return Task.WhenAll(tasks);
    }

    public static Task VerifyMatch(GeneratorRunResult result)
    {
        AssertSomeDiagnostics(result);
        return Verifier.Verify(result.Diagnostics);
    }

    public static Task VerifyMatchAndIncludesSpecifiedDiagnostics<TGenerator>(string source, IReadOnlyCollection<string> diagnosticIDs)
        where TGenerator : IIncrementalGenerator, new()
        => VerifyMatchAndIncludesSpecifiedDiagnostics(source, new TGenerator(), diagnosticIDs);

    public static Task VerifyMatchAndIncludesSpecifiedDiagnostics(string source, IIncrementalGenerator generator, IReadOnlyCollection<string> diagnosticIDs)
        => VerifyMatchAndIncludesSpecifiedDiagnostics(GeneratorDriverUtility.ConstructAndRunDriver(source, generator).GetRunResult(), diagnosticIDs);

    public static Task VerifyMatchAndIncludesSpecifiedDiagnostics(GeneratorDriverRunResult results, IReadOnlyCollection<string> diagnosticIDs)
    {
        AssertIncludesSpecifiedDiagnostics(results, diagnosticIDs);
        return VerifyMatch(results);
    }

    public static Task VerifyMatchAndIncludesSpecifiedDiagnostics(GeneratorRunResult result, IReadOnlyCollection<string> diagnosticIDs)
    {
        AssertIncludesSpecifiedDiagnostics(result, diagnosticIDs);
        return VerifyMatch(result);
    }

    public static void VerifyIdentical<TGenerator>(GeneratorDriverRunResult results1, string source) where TGenerator : IIncrementalGenerator, new()
        => VerifyIdentical(results1, source, new TGenerator());

    public static void VerifyIdentical(GeneratorDriverRunResult results1, string source, IIncrementalGenerator generator)
        => VerifyIdentical(results1, GeneratorDriverUtility.ConstructAndRunDriver(source, generator).GetRunResult());

    public static void VerifyIdentical(GeneratorDriverRunResult results1, GeneratorDriverRunResult results2)
    {
        HashSet<(ISourceGenerator, Diagnostic)> unmatchedDiagnostics = new();

        foreach (GeneratorRunResult result in results1.Results)
        {
            foreach (Diagnostic diagnostic in result.Diagnostics)
            {
                Assert.DoesNotContain((result.Generator, diagnostic), unmatchedDiagnostics);

                unmatchedDiagnostics.Add((result.Generator, diagnostic));
            }
        }

        foreach (GeneratorRunResult result in results2.Results)
        {
            foreach (Diagnostic diagnostic in result.Diagnostics)
            {
                (ISourceGenerator, Diagnostic) identifier = (result.Generator, diagnostic);

                Assert.Contains(identifier, unmatchedDiagnostics);

                unmatchedDiagnostics.Remove(identifier);
            }
        }
    }

    public static void VerifyIdentical(GeneratorRunResult result1, GeneratorRunResult result2)
    {
        HashSet<Diagnostic> unmatchedDiagnostics = new(result1.Diagnostics);

        foreach (Diagnostic diagnostic in result2.Diagnostics)
        {
            Assert.Contains(diagnostic, unmatchedDiagnostics);

            unmatchedDiagnostics.Remove(diagnostic);
        }

        Assert.Equal(result1.Diagnostics.Length, result2.Diagnostics.Length);
    }
}
