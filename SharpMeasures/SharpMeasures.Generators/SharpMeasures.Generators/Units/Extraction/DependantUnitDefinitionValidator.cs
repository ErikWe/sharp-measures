namespace SharpMeasures.Generators.Units.Extraction;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using SharpMeasures.Generators.Attributes.Parsing.Extraction;
using SharpMeasures.Generators.Attributes.Parsing.Units;
using SharpMeasures.Generators.Diagnostics.UnitDefinitions;

internal abstract class DependantUnitDefinitionValidator<TParameters> : UnitDefinitionValidator<TParameters>
    where TParameters : IDependantUnitDefinitionParameters
{
    protected INamedTypeSymbol UnitType { get; }

    public DependantUnitDefinitionValidator(INamedTypeSymbol unitType)
    {
        UnitType = unitType;
    }

    public override ExtractionValidity Check(AttributeData attributeData, TParameters parameters)
    {
        if (base.Check(attributeData, parameters) is ExtractionValidity { IsInvalid: true } invalidUnit)
        {
            return invalidUnit;
        }

        if (string.IsNullOrEmpty(parameters.DependantOn))
        {
            return ExtractionValidity.Invalid(CreateUnitNameNotRecognizedDiagnostics(attributeData));
        }

        return ExtractionValidity.Valid;
    }

    private Diagnostic? CreateUnitNameNotRecognizedDiagnostics(AttributeData attributeData)
    {
        if (DependantOnArgumentSyntax(attributeData)?.GetFirstChildOfKind<LiteralExpressionSyntax>(SyntaxKind.StringLiteralExpression)
            is LiteralExpressionSyntax expressionSyntax)
        {
            return UnitNameNotRecognizedDiagnostics.Create(expressionSyntax, UnitType);
        }

        return null;
    }

    protected AttributeArgumentSyntax? DependantOnArgumentSyntax(AttributeData attributeData)
        => attributeData.GetArgumentSyntax(DependantOnArgumentIndex(attributeData));

    protected abstract int DependantOnArgumentIndex(AttributeData attributeData);
}
