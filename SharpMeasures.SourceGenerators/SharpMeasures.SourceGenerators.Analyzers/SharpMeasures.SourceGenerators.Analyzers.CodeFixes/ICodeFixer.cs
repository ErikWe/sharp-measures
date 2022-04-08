namespace ErikWe.SharpMeasures.SourceGenerators.Analyzers.CodeFixes;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CodeFixes;

using System.Threading.Tasks;

internal interface ICodeFixer
{
    public abstract string RuleID { get; }

    public abstract Task Register(CodeFixContext context, SyntaxNode syntaxRoot, Diagnostic diagnostic);
}
