namespace SharpMeasures.Generators.Units.Extraction;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing.Extraction;
using SharpMeasures.Generators.Attributes.Parsing.Units;
using SharpMeasures.Generators.Diagnostics;

using System.Collections.Generic;
using System.Globalization;

internal class DerivedUnitValidator : UnitDefinitionValidator<DerivedUnitDefinition>
{
    private INamedTypeSymbol UnitSymbol { get; }
    private IEnumerable<DerivableUnitDefinition> DerivableDefinitions { get; }
    private Dictionary<INamedTypeSymbol, HashSet<string>> UnitDefinitionsOnTypes { get; }

    public DerivedUnitValidator(INamedTypeSymbol unitSymbol)
    {
        UnitSymbol = unitSymbol;
        DerivableDefinitions = DerivableUnitParser.Parser.Parse(unitSymbol);
        UnitDefinitionsOnTypes = PreComputeUnitsOnRelevantTypes();
    }

    public override ExtractionValidity Check(AttributeData attributeData, DerivedUnitDefinition definition)
    {
        if (base.Check(attributeData, definition) is ExtractionValidity { IsInvalid: true } invalidUnit)
        {
            return invalidUnit;
        }

        if (definition.ParsingData.SignatureCouldBeParsed is false)
        {
            return ExtractionValidity.InvalidWithoutDiagnostics;
        }

        if (definition.Signature.Count is 0)
        {
            return ExtractionValidity.Invalid(CreateEmptySignatureDiagnostics(definition));
        }

        if (definition.Signature.Count != definition.Units.Count)
        {
            return ExtractionValidity.Invalid(CreateUnitListNotMatchingSignatureDiagnostics(definition));
        }

        if (!ExistsMatchingDerivableSignature(definition, DerivableDefinitions))
        {
            return ExtractionValidity.Invalid(CreateSignatureNotRecognizedDiagnostics(definition, UnitSymbol));
        }

        if (GetMissingUnitIndex(definition, UnitDefinitionsOnTypes) is int missingIndex and >= 0)
        {
            return ExtractionValidity.Invalid(CreateUnitNameNotRecognizedDiagnostics(definition, missingIndex, UnitSymbol));
        }

        return ExtractionValidity.Valid;
    }

    private static bool ExistsMatchingDerivableSignature(DerivedUnitDefinition parameters, IEnumerable<DerivableUnitDefinition> derivableDefinitions)
    {
        foreach (DerivableUnitDefinition derivable in derivableDefinitions)
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

    private static int GetMissingUnitIndex(DerivedUnitDefinition parameters, IDictionary<INamedTypeSymbol, HashSet<string>> unitDefinitionsOnTarget)
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

    private static Diagnostic CreateEmptySignatureDiagnostics(DerivedUnitDefinition definition)
    {
        return Diagnostic.Create(DiagnosticRules.EmptyUnitDerivationSignature, definition.Locations.Units);
    }

    private static Diagnostic CreateUnitListNotMatchingSignatureDiagnostics(DerivedUnitDefinition definition)
    {
        return Diagnostic.Create(DiagnosticRules.UnitListNotMatchingSignature, definition.Locations.Units,
            definition.Signature.Count.ToString(CultureInfo.InvariantCulture), definition.Units.Count.ToString(CultureInfo.InvariantCulture));
    }

    private static Diagnostic? CreateSignatureNotRecognizedDiagnostics(DerivedUnitDefinition definition, INamedTypeSymbol unitSymbol)
    {
        return Diagnostic.Create(DiagnosticRules.DerivedUnitSignatureNotRecognized, definition.Locations.Signature, unitSymbol.Name);
    }

    private static Diagnostic? CreateUnitNameNotRecognizedDiagnostics(DerivedUnitDefinition definition, int index, INamedTypeSymbol unitSymbol)
    {
        return Diagnostic.Create(DiagnosticRules.InvalidUnitName, definition.Locations.UnitComponents[index], definition.Units[index], unitSymbol.Name);
    }

    private Dictionary<INamedTypeSymbol, HashSet<string>> PreComputeUnitsOnRelevantTypes()
    {
        Dictionary<INamedTypeSymbol, HashSet<string>> unitDefinitionsOnTypes = new(SymbolEqualityComparer.Default);

        foreach (DerivableUnitDefinition derivableDefinition in DerivableDefinitions)
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
