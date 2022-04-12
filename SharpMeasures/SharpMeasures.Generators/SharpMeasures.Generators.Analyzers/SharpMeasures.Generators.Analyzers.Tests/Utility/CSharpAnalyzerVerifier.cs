namespace SharpMeasures.Generators.Analyzers.Tests.Utility;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Testing;
using Microsoft.CodeAnalysis.Diagnostics;
using Microsoft.CodeAnalysis.Testing;
using Microsoft.CodeAnalysis.Testing.Verifiers;

using System;
using System.Collections.Immutable;
using System.Threading;
using System.Threading.Tasks;

public static partial class CSharpAnalyzerVerifier
{
    public static DiagnosticResult Diagnostic<TAnalyzer>() where TAnalyzer : DiagnosticAnalyzer, new()
        => CSharpAnalyzerVerifier<TAnalyzer, XUnitVerifier>.Diagnostic();

    public static DiagnosticResult Diagnostic<TAnalyzer>(string diagnosticId) where TAnalyzer : DiagnosticAnalyzer, new()
        => CSharpAnalyzerVerifier<TAnalyzer, XUnitVerifier>.Diagnostic(diagnosticId);

    public static DiagnosticResult Diagnostic<TAnalyzer>(DiagnosticDescriptor descriptor) where TAnalyzer : DiagnosticAnalyzer, new()
        => CSharpAnalyzerVerifier<TAnalyzer, XUnitVerifier>.Diagnostic(descriptor);

    public static async Task VerifyNoDiagnostics<TAnalyzer>(string source) where TAnalyzer : DiagnosticAnalyzer, new()
        => await VerifyDiagnostics<TAnalyzer>(source, Array.Empty<DiagnosticResult>()).ConfigureAwait(false);

    public static async Task VerifyDiagnostics<TAnalyzer>(string source, params DiagnosticResult[] expected) where TAnalyzer : DiagnosticAnalyzer, new()
    {
        var test = new Test<TAnalyzer>
        {
            TestCode = source,
        };

        test.ExpectedDiagnostics.AddRange(expected);
        await test.RunAsync(CancellationToken.None).ConfigureAwait(false);
    }

    private class Test<TAnalyzer> : CSharpAnalyzerTest<TAnalyzer, XUnitVerifier> where TAnalyzer : DiagnosticAnalyzer, new()
    {
        public Test()
        {
            ReferenceAssemblies = ReferenceAssemblies.Default.AddPackages(
                ImmutableArray.Create(
                    new PackageIdentity("SharpMeasures.Generators", typeof(CSharpAnalyzerVerifier).Assembly.GetName().Version?.ToString() ?? string.Empty)
                )
            );

            SolutionTransforms.Add((solution, projectId) =>
            {
                if (solution.GetProject(projectId)?.CompilationOptions is not CompilationOptions compilationOptions)
                {
                    return solution;
                }
                
                compilationOptions = compilationOptions.WithSpecificDiagnosticOptions(
                    compilationOptions.SpecificDiagnosticOptions.SetItems(CSharpVerifierHelper.NullableWarnings));

                solution = solution.WithProjectCompilationOptions(projectId, compilationOptions);

                return solution;
            });
        }
    }
}