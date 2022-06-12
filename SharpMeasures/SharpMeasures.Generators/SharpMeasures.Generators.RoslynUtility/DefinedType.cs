namespace SharpMeasures.Generators;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

public readonly record struct DefinedType(string Name, string Namespace, Accessibility Accessibility, bool IsReadOnly, bool IsRecord, bool IsValueType)
{
    public static DefinedType Empty { get; } = new(string.Empty, string.Empty, Accessibility.NotApplicable, false, false, false);

    internal static DefinedType FromSymbol(INamedTypeSymbol symbol)
    {
        NamedType namedType = NamedType.FromSymbol(symbol);

        return new(namedType.Name, namedType.Namespace, symbol.DeclaredAccessibility, symbol.IsReadOnly, symbol.IsRecord, symbol.IsValueType);
    }

    public NamedType AsNamedType() => new(Name, Namespace);

    public string ComposeDeclaration()
    {
        string accessibilityText = Accessibility switch
        {
            Accessibility.NotApplicable => SyntaxFacts.GetText(Accessibility.Private),
            _ => SyntaxFacts.GetText(Accessibility)
        };

        string readonlyText = IsReadOnly ? $" {SyntaxFacts.GetText(SyntaxKind.ReadOnlyKeyword)}" : string.Empty;
        string recordText = IsRecord ? $"{SyntaxFacts.GetText(SyntaxKind.RecordKeyword)} " : string.Empty;
        string typeText = IsValueType ? SyntaxFacts.GetText(SyntaxKind.StructKeyword) : SyntaxFacts.GetText(SyntaxKind.ClassKeyword);

        return $"{accessibilityText}{readonlyText} {SyntaxFacts.GetText(SyntaxKind.PartialKeyword)} {recordText}{typeText} {Name}";
    }
}
