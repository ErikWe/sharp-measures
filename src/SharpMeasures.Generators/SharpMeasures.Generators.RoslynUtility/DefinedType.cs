namespace SharpMeasures.Generators;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

public readonly record struct DefinedType(string Name, string Namespace, string Assembly, Accessibility Accessibility, bool IsSealed, bool IsReadOnly, bool IsRecord, bool IsValueType)
{
    public static DefinedType Empty { get; } = new(string.Empty, string.Empty, string.Empty, Accessibility.NotApplicable, false, false, false, false);

    public static DefinedType FromSymbol(INamedTypeSymbol symbol)
    {
        if (symbol.Kind is SymbolKind.ErrorType)
        {
            return Empty;
        }

        var namedType = NamedType.FromSymbol(symbol);

        return new(namedType.Name, namedType.Namespace, symbol.ContainingAssembly.Name, symbol.DeclaredAccessibility, symbol.IsReferenceType && symbol.IsSealed, symbol.IsReadOnly, symbol.IsRecord, symbol.IsValueType);
    }

    public bool IsReferenceType => IsValueType is false;

    public string QualifiedName => $"{(string.IsNullOrEmpty(Namespace) ? string.Empty : $"{Namespace}.")}{Name}";
    public string FullyQualifiedName => $"global::{QualifiedName}";

    public NamedType AsNamedType() => new(Name, Namespace, Assembly, IsValueType);

    public string ComposeDeclaration()
    {
        var accessibilityText = Accessibility switch
        {
            Accessibility.NotApplicable => SyntaxFacts.GetText(Accessibility.Private),
            _ => SyntaxFacts.GetText(Accessibility)
        };

        var sealedText = IsSealed ? $" {SyntaxFacts.GetText(SyntaxKind.SealedKeyword)}" : string.Empty;
        var readonlyText = IsReadOnly ? $" {SyntaxFacts.GetText(SyntaxKind.ReadOnlyKeyword)}" : string.Empty;
        var recordText = IsRecord ? $"{SyntaxFacts.GetText(SyntaxKind.RecordKeyword)} " : string.Empty;
        var typeText = IsValueType ? SyntaxFacts.GetText(SyntaxKind.StructKeyword) : SyntaxFacts.GetText(SyntaxKind.ClassKeyword);

        return $"{accessibilityText}{sealedText}{readonlyText} {SyntaxFacts.GetText(SyntaxKind.PartialKeyword)} {recordText}{typeText} {Name}";
    }
}
