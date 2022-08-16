namespace SharpMeasures.Generators.Units.Parsing.DerivableUnit;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.SourceBuilding;
using SharpMeasures.Generators.Raw.Units;

using System.Collections.Generic;
using System.Globalization;

internal interface IDerivableUnitResolutionDiagnostics
{
    public abstract Diagnostic? SignatureElementNotUnit(IDerivableUnitResolutionContext context, UnresolvedDerivableUnitDefinition definition, int index);
}

internal interface IDerivableUnitResolutionContext : IProcessingContext
{
    public abstract IRawUnitPopulation UnitPopulation { get; }
}

internal class DerivableUnitResolver : AProcesser<IDerivableUnitResolutionContext, UnresolvedDerivableUnitDefinition, DerivableUnitDefinition>
{
    private IDerivableUnitResolutionDiagnostics Diagnostics { get; }

    public DerivableUnitResolver(IDerivableUnitResolutionDiagnostics diagnostics)
    {
        Diagnostics = diagnostics;
    }

    public override IOptionalWithDiagnostics<DerivableUnitDefinition> Process(IDerivableUnitResolutionContext context, UnresolvedDerivableUnitDefinition definition)
    {
        var processedSignature = ProcessSignature(context, definition);
        var allDiagnostics = processedSignature.Diagnostics;

        if (processedSignature.LacksResult)
        {
            return OptionalWithDiagnostics.Empty<DerivableUnitDefinition>(allDiagnostics);
        }

        var parameterNames = GetSignatureParameterNames(processedSignature.Result);
        var processedExpression = ProcessExpression(context, definition, processedSignature.Result, parameterNames);

        DerivableUnitDefinition product = new(definition.DerivationID, processedExpression, processedSignature.Result, parameterNames, definition.Locations);
        return OptionalWithDiagnostics.Result(product, allDiagnostics);
    }

    private IOptionalWithDiagnostics<UnitDerivationSignature> ProcessSignature(IDerivableUnitResolutionContext context, UnresolvedDerivableUnitDefinition definition)
    {
        var units = new IRawUnitType[definition.Signature.Count];

        for (int i = 0; i < definition.Signature.Count; i++)
        {
            if (context.UnitPopulation.Units.TryGetValue(definition.Signature[i], out IRawUnitType unit) is false)
            {
                return OptionalWithDiagnostics.Empty<UnitDerivationSignature>(Diagnostics.SignatureElementNotUnit(context, definition, i));
            }

            units[i] = unit;
        }

        UnitDerivationSignature signature = new(units);
        return OptionalWithDiagnostics.Result(signature);
    }

    private static string ProcessExpression(IDerivableUnitResolutionContext context, UnresolvedDerivableUnitDefinition definition, UnitDerivationSignature resolvedSignature,
        IEnumerable<string> parameterNames)
    {
        string[] parameterNameAndQuantity = new string[resolvedSignature.Count];

        IEnumerator<string> parameterNameEnumerator = parameterNames.GetEnumerator();
        IEnumerator<NamedType> quantityEnumerator = GetQuantitiesOfSignatureUnits(context, resolvedSignature).GetEnumerator();

        int index = 0;
        while (parameterNameEnumerator.MoveNext() && quantityEnumerator.MoveNext())
        {
            parameterNameAndQuantity[index] = $"{parameterNameEnumerator.Current}.{quantityEnumerator.Current.Name}.Magnitude";

            index += 1;
        }

        return string.Format(CultureInfo.InvariantCulture, definition.Expression, parameterNameAndQuantity);
    }

    private static IReadOnlyList<string> GetSignatureParameterNames(UnitDerivationSignature signature)
    {
        Dictionary<string, int> counts = new();

        foreach (var signatureComponent in signature)
        {
            countParameter(signatureComponent);
        }

        var parameterNames = new string[signature.Count];

        int index = 0;
        foreach (var signatureComponent in signature)
        {
            string name = SourceBuildingUtility.ToParameterName(signatureComponent.Type.Name);
            name = appendParameterNumber(name, signatureComponent);

            parameterNames[index] = name;
            index += 1;
        }

        return parameterNames;

        void countParameter(IRawUnitType signatureComponent)
        {
            if (counts.TryGetValue(signatureComponent.Type.Name, out int count))
            {
                counts[signatureComponent.Type.Name] = count - 1;
            }
            else
            {
                counts[signatureComponent.Type.Name] = -1;
            }
        }

        string appendParameterNumber(string text, IRawUnitType signatureComponent)
        {
            int count = counts[signatureComponent.Type.Name];

            if (count == -1)
            {
                return text;
            }
            else if (count < 0)
            {
                counts[signatureComponent.Type.Name] = 1;
                return $"{text}1";
            }
            else
            {
                counts[signatureComponent.Type.Name] += 1;
                return $"{text}{counts[signatureComponent.Type.Name]}";
            }
        }
    }

    private static IEnumerable<NamedType> GetQuantitiesOfSignatureUnits(IDerivableUnitResolutionContext context, UnitDerivationSignature signature)
    {
        foreach (var signatureElement in signature)
        {
            yield return context.UnitPopulation.Units[signatureElement.Type.AsNamedType()].Definition.Quantity;
        }
    }
}
