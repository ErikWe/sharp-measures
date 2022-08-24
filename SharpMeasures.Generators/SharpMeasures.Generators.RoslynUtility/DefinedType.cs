﻿namespace SharpMeasures.Generators;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

public readonly record struct DefinedType(string Name, string Namespace, Accessibility Accessibility, bool IsSealed, bool IsReadOnly, bool IsRecord,
    bool IsValueType)
{
    public static DefinedType Empty { get; } = new(string.Empty, string.Empty, Accessibility.NotApplicable, false, false, false, false);

    internal static DefinedType FromSymbol(INamedTypeSymbol symbol)
    {
        NamedType namedType = NamedType.FromSymbol(symbol);

        return new(namedType.Name, namedType.Namespace, symbol.DeclaredAccessibility, symbol.IsReferenceType && symbol.IsSealed, symbol.IsReadOnly,
            symbol.IsRecord, symbol.IsValueType);
    }

    public bool IsReferenceType => IsValueType is false;

    public string FullyQualifiedName => $"global::{(string.IsNullOrEmpty(Namespace) ? string.Empty : $"{Namespace}.")}{Name}";

    public NamedType AsNamedType() => new(Name, Namespace, IsValueType);

    public string ComposeDeclaration()
    {
        string accessibilityText = Accessibility switch
        {
            Accessibility.NotApplicable => SyntaxFacts.GetText(Accessibility.Private),
            _ => SyntaxFacts.GetText(Accessibility)
        };

        string sealedText = IsSealed ? $" {SyntaxFacts.GetText(SyntaxKind.SealedKeyword)}" : string.Empty;
        string readonlyText = IsReadOnly ? $" {SyntaxFacts.GetText(SyntaxKind.ReadOnlyKeyword)}" : string.Empty;
        string recordText = IsRecord ? $"{SyntaxFacts.GetText(SyntaxKind.RecordKeyword)} " : string.Empty;
        string typeText = IsValueType ? SyntaxFacts.GetText(SyntaxKind.StructKeyword) : SyntaxFacts.GetText(SyntaxKind.ClassKeyword);

        return $"{accessibilityText}{sealedText}{readonlyText} {SyntaxFacts.GetText(SyntaxKind.PartialKeyword)} {recordText}{typeText} {Name}";
    }
}