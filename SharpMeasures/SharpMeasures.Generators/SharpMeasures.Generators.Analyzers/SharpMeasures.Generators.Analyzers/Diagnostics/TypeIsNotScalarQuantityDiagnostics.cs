namespace SharpMeasures.Generators.Analyzers.Diagnostics;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;

using SharpMeasures.Generators.Attributes.Parsing.Units;

internal static class TypeIsNotScalarQuantityDiagnostics
{
    private static string AttributeFullName { get; } = typeof(GeneratedScalarQuantityAttribute).FullName;

    public static void AnalyzeNamedType(SymbolAnalysisContext context, AttributeData attributeData, GeneratedUnitAttributeParameters parameters)
    {
        if (parameters.Quantity is not INamedTypeSymbol argumentSymbol
            || argumentSymbol.GetAttributeOfType<GeneratedScalarQuantityAttribute>() is not null
            || attributeData.ApplicationSyntaxReference?.GetSyntax() is not AttributeSyntax attributeSyntax)
        {
            return;
        }

        int attributeArgumentIndex = GeneratedUnitAttributeParameters.Parser.ParseIndices(attributeData)[nameof(GeneratedUnitAttributeParameters.Quantity)];

        if (attributeSyntax.ArgumentList?.Arguments[attributeArgumentIndex] is not AttributeArgumentSyntax argumentSyntax)
        {
            return;
        }

        foreach (SyntaxNode childSyntaxNode in argumentSyntax.ChildNodes())
        {
            if (childSyntaxNode is TypeOfExpressionSyntax typeOfExpression)
            {        
                Diagnostic diagnostics = Diagnostic.Create(DiagnosticRules.TypeIsNotScalarQuantity, typeOfExpression.Type.GetLocation(),
                    AttributeFullName, argumentSymbol.Name);

                context.ReportDiagnostic(diagnostics);
            }
        }

    }
}
