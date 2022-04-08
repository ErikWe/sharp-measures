namespace ErikWe.SharpMeasures.SourceGenerators.Analyzers.CodeFixes;

using ErikWe.SharpMeasures.SourceGenerators.Analyzers.CodeFixes.CodeFixers;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CodeFixes;

using System.Collections.Generic;
using System.Collections.Immutable;
using System.Composition;
using System.Linq;
using System.Threading.Tasks;

[ExportCodeFixProvider(LanguageNames.CSharp, Name = nameof(SharpMeasuresSourceGeneratorsCodeFixProvider)), Shared]
public class SharpMeasuresSourceGeneratorsCodeFixProvider : CodeFixProvider
{
    private static ICodeFixer[] CodeFixers { get; } = new[]
    {
        new TypeIsNotPartialCodeFixer()
    };

    private static Dictionary<string, ICodeFixer> CodeFixerLookup { get; } = CodeFixers.ToDictionary(static (codeFixer) => codeFixer.RuleID);
    public sealed override ImmutableArray<string> FixableDiagnosticIds { get; } = ImmutableArray.CreateRange(CodeFixerLookup.Keys);

    public sealed override FixAllProvider GetFixAllProvider() => WellKnownFixAllProviders.BatchFixer;

    public sealed override async Task RegisterCodeFixesAsync(CodeFixContext context)
    {
        if (await context.Document.GetSyntaxRootAsync(context.CancellationToken).ConfigureAwait(false) is not SyntaxNode syntaxRoot)
        {
            return;
        }

        Task[] tasks = new Task[context.Diagnostics.Length];

        for (int i = 0; i < context.Diagnostics.Length; i++)
        {
            if (CodeFixerLookup.TryGetValue(context.Diagnostics[i].Id, out ICodeFixer codeFixer))
            {
                tasks[i] = codeFixer.Register(context, syntaxRoot, context.Diagnostics[i]);
            }
            else
            {
                tasks[i] = Task.CompletedTask;
            }
        }

        await Task.WhenAll(tasks).ConfigureAwait(false);
    }
}
