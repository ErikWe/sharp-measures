namespace SharpMeasures.Generators.Units.Extraction;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using SharpMeasures.Generators.Attributes.Parsing.Extraction;
using SharpMeasures.Generators.Attributes.Parsing.Units;
using SharpMeasures.Generators.Diagnostics.DerivableUnits;
using SharpMeasures.Generators.Diagnostics.UnitDefinitions;

using System.Collections.Generic;

internal class DerivedUnitValidator : UnitDefinitionValidator<DerivedUnitParameters>
{
    private INamedTypeSymbol UnitSymbol { get; }
    private IEnumerable<DerivableUnitParameters> DerivableDefinitions { get; }
    private Dictionary<INamedTypeSymbol, HashSet<string>> UnitDefinitionsOnTypes { get; }

    public DerivedUnitValidator(INamedTypeSymbol unitSymbol)
    {
        UnitSymbol = unitSymbol;
        DerivableDefinitions = DerivableUnitParser.Parser.Parse(unitSymbol);
        UnitDefinitionsOnTypes = PreComputeUnitsOnRelevantTypes();
    }

    public override ExtractionValidity Check(AttributeData attributeData, DerivedUnitParameters parameters)
    {
        if (base.Check(attributeData, parameters) is ExtractionValidity { IsInvalid: true } invalidUnit)
        {
            return invalidUnit;
        }

        if (parameters.ParsingData.EmptySignature)
        {
            return ExtractionValidity.Invalid(CreateEmptySignatureDiagnostics(attributeData));
        }

        if (parameters.Signature.Count != parameters.Units.Count)
        {
            return ExtractionValidity.Invalid(CreateUnitListNotMatchingSignatureDiagnostics(attributeData, parameters));
        }

        if (!ExistsMatchingDerivableSignature(parameters, DerivableDefinitions))
        {
            return ExtractionValidity.Invalid(CreateSignatureNotRecognizedDiagnostics(attributeData, UnitSymbol));
        }

        if (CheckForMissingUnit(parameters, UnitDefinitionsOnTypes) is int missing and >= 0)
        {
            return ExtractionValidity.Invalid(CreateUnitNameNotRecognizedDiagnostics(attributeData, missing, UnitSymbol));
        }

        return ExtractionValidity.Valid;
    }

    private static bool ExistsMatchingDerivableSignature(DerivedUnitParameters parameters, IEnumerable<DerivableUnitParameters> derivableDefinitions)
    {
        foreach (DerivableUnitParameters derivable in derivableDefinitions)
        {
            IEnumerator<INamedTypeSymbol> targetSignatureEnumerator = parameters.Signature.GetEnumerator();
            IEnumerator<INamedTypeSymbol> candidateSignatureEnumerator = derivable.Signature.GetEnumerator();

            bool equality = false;

            while (targetSignatureEnumerator.MoveNext() && candidateSignatureEnumerator.MoveNext())
            {
                if (targetSignatureEnumerator.Current.Equals(candidateSignatureEnumerator.Current, SymbolEqualityComparer.Default))
                {
                    equality = true;
                }
                else
                {
                    equality = false;
                    break;
                }
            }

            if (equality)
            {
                return true;
            }
        }

        return false;
    }

    private static int CheckForMissingUnit(DerivedUnitParameters parameters, IDictionary<INamedTypeSymbol, HashSet<string>> unitDefinitionsOnTarget)
    {
        IEnumerator<INamedTypeSymbol> typeEnumerator = parameters.Signature.GetEnumerator();
        IEnumerator<string> unitEnumerator = parameters.Units.GetEnumerator();

        int index = 0;
        while (typeEnumerator.MoveNext() && unitEnumerator.MoveNext())
        {
            if (string.IsNullOrEmpty(unitEnumerator.Current))
            {
                return index;
            }

            foreach (string unitName in unitDefinitionsOnTarget[typeEnumerator.Current])
            {
                if (unitName == unitEnumerator.Current)
                {
                    index++;
                    continue;
                }
            }

            return index;
        }

        return -1;
    }

    private static Diagnostic? CreateEmptySignatureDiagnostics(AttributeData attributeData)
    {
        if (SignatureArgumentSyntax(attributeData)?.GetFirstChildOfKind<InitializerExpressionSyntax>(SyntaxKind.ArrayInitializerExpression)
            is InitializerExpressionSyntax initializerSyntax)
        {
            return EmptyUnitDerivationSignatureDiagnostics.Create(initializerSyntax);
        }

        return null;
    }

    private static Diagnostic? CreateUnitListNotMatchingSignatureDiagnostics(AttributeData attributeData, DerivedUnitParameters parameters)
    {
        if (UnitsArgumentSyntax(attributeData) is AttributeArgumentSyntax argumentSyntax)
        {
            return UnitListNotMatchingSignatureDiagnostics.Create(argumentSyntax, parameters.Signature.Count, parameters.Units.Count);
        }

        return null;
    }

    private static Diagnostic? CreateSignatureNotRecognizedDiagnostics(AttributeData attributeData, INamedTypeSymbol unitSymbol)
    {
        if (SignatureArgumentSyntax(attributeData) is AttributeArgumentSyntax argumentSyntax)
        {
            return SignatureNotRecognizedDiagnostics.Create(argumentSyntax, unitSymbol);
        }

        return null;
    }

    private static Diagnostic? CreateUnitNameNotRecognizedDiagnostics(AttributeData attributeData, int index, INamedTypeSymbol unitSymbol)
    {
        if (UnitsArgumentSyntax(attributeData)?.GetFirstChildOfKind<InitializerExpressionSyntax>(SyntaxKind.ArrayInitializerExpression)?.Expressions[index]
            is LiteralExpressionSyntax { RawKind: (int)SyntaxKind.StringLiteralExpression } stringLiteralExpression)
        {
            return UnitNameNotRecognizedDiagnostics.Create(stringLiteralExpression, unitSymbol);
        }

        return null;
    }

    protected static AttributeArgumentSyntax? SignatureArgumentSyntax(AttributeData attributeData)
        => attributeData.GetArgumentSyntax(SignatureArgumentIndex(attributeData));
    protected static AttributeArgumentSyntax? UnitsArgumentSyntax(AttributeData attributeData)
        => attributeData.GetArgumentSyntax(UnitsArgumentIndex(attributeData));

    protected override int NameArgumentIndex(AttributeData attributeData) => DerivedUnitParser.NameIndex(attributeData);
    protected override int PluralArgumentIndex(AttributeData attributeData) => DerivedUnitParser.PluralIndex(attributeData);
    protected static int SignatureArgumentIndex(AttributeData attributeData) => DerivedUnitParser.SignatureIndex(attributeData);
    protected static int UnitsArgumentIndex(AttributeData attributeData) => DerivedUnitParser.UnitsIndex(attributeData);

    private Dictionary<INamedTypeSymbol, HashSet<string>> PreComputeUnitsOnRelevantTypes()
    {
        Dictionary<INamedTypeSymbol, HashSet<string>> unitDefinitionsOnTypes = new(SymbolEqualityComparer.Default);

        foreach (DerivableUnitParameters derivableDefinition in DerivableDefinitions)
        {
            foreach (INamedTypeSymbol originUnitSymbol in derivableDefinition.Signature)
            {
                if (!unitDefinitionsOnTypes.ContainsKey(originUnitSymbol))
                {
                    unitDefinitionsOnTypes[originUnitSymbol] = new HashSet<string>(UnitDefinitionUtility.GetNamesOfAllUnits(originUnitSymbol));
                }
            }
        }

        return unitDefinitionsOnTypes;
    }
}
