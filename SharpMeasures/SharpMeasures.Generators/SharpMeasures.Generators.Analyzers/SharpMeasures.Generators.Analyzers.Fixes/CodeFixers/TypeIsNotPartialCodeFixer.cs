namespace SharpMeasures.Generators.Analyzers.Fixes.CodeFixers;

using SharpMeasures.Generators.Diagnostics;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CodeActions;
using Microsoft.CodeAnalysis.CodeFixes;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using System.Threading;
using System.Threading.Tasks;
using System.Linq;

internal class TypeIsNotPartialCodeFixer : ICodeFixer
{
    public string RuleID { get; } = DiagnosticIDs.TypeNotPartial;

    public Task Register(CodeFixContext context, SyntaxNode syntaxRoot, Diagnostic diagnostic)
    {
        if (syntaxRoot.FindToken(diagnostic.Location.SourceSpan.Start).Parent?.AncestorsAndSelf().OfType<MemberDeclarationSyntax>().First()
            is not MemberDeclarationSyntax declaration)
        {
            return Task.CompletedTask;
        }

        context.RegisterCodeFix(CodeAction.Create("Add partial modifier", (token) => MakeTypePartial(context.Document, syntaxRoot, declaration, token)), diagnostic);
        return Task.CompletedTask;
    }

    private static Task<Document> MakeTypePartial(Document document, SyntaxNode syntaxRoot, MemberDeclarationSyntax declaration, CancellationToken _)
    {
        MemberDeclarationSyntax newDeclaration = declaration.WithModifiers(declaration.Modifiers.Add(SyntaxFactory.Token(SyntaxKind.PartialKeyword)));

        SyntaxNode newRoot = syntaxRoot.ReplaceNode(declaration, newDeclaration);
        Document newDocument = document.WithSyntaxRoot(newRoot);

        return Task.FromResult(newDocument);
    }
}
