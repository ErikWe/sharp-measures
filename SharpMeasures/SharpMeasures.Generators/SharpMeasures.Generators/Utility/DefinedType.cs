namespace SharpMeasures.Generators.Utility;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

internal readonly record struct DefinedType(NamedType Name, Accessibility Accessibility, bool IsReadOnly, bool IsRecord, bool IsValueType)
{
    public static DefinedType FromSymbol(INamedTypeSymbol symbol)
        => new(NamedType.FromSymbol(symbol), symbol.DeclaredAccessibility, symbol.IsReadOnly, symbol.IsRecord, symbol.IsValueType);

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

        return $"{accessibilityText}{readonlyText} {SyntaxFacts.GetText(SyntaxKind.PartialKeyword)} {recordText}{typeText} {Name.Name}";
    }
}