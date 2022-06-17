namespace SharpMeasures.Generators.Units.Refinement.DerivableUnit;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.SourceBuilding;
using SharpMeasures.Generators.Units.Parsing.DerivableUnit;

using System.Collections.Generic;
using System.Globalization;

internal interface IDerivableUnitRefinementDiagnostics
{
    public abstract Diagnostic? SignatureElementNotUnit(IDerivableUnitRefinementContext context, DerivableUnitDefinition definition, int index);
}

internal interface IDerivableUnitRefinementContext : IProcessingContext
{
    public abstract UnitPopulation UnitPopulation { get; }
}

internal class DerivableUnitRefiner : IProcesser<IDerivableUnitRefinementContext, DerivableUnitDefinition, RefinedDerivableUnitDefinition>
{
    private IDerivableUnitRefinementDiagnostics Diagnostics { get; }

    public DerivableUnitRefiner(IDerivableUnitRefinementDiagnostics diagnostics)
    {
        Diagnostics = diagnostics;
    }

    public IOptionalWithDiagnostics<RefinedDerivableUnitDefinition> Process(IDerivableUnitRefinementContext context, DerivableUnitDefinition definition)
    {
        NamedType[] quantitiesOfSignatureUnits = new NamedType[definition.Signature.Count];

        int index = 0;
        foreach (NamedType type in definition.Signature)
        {
            if (context.UnitPopulation.TryGetValue(type, out UnitInterface unit) is false)
            {
                return OptionalWithDiagnostics.Empty<RefinedDerivableUnitDefinition>(Diagnostics.SignatureElementNotUnit(context, definition, index));
            }

            quantitiesOfSignatureUnits[index] = unit.QuantityType;

            index += 1;
        }

        var signatureParameterNames = GetSignatureParameterNames(definition.Signature);
        string processedExpression = ProcessExpression(definition, signatureParameterNames, quantitiesOfSignatureUnits);

        RefinedDerivableUnitDefinition product = new(processedExpression, definition.Signature, signatureParameterNames);
        return OptionalWithDiagnostics.Result(product);
    }

    private static IReadOnlyList<string> GetSignatureParameterNames(DerivableSignature signature)
    {
        Dictionary<string, int> counts = new();

        foreach (NamedType signatureComponent in signature)
        {
            countParameter(signatureComponent);
        }

        string[] parameterNames = new string[signature.Count];

        int index = 0;
        foreach (NamedType type in signature)
        {
            string name = SourceBuildingUtility.ToParameterName(type.Name);
            name = appendParameterNumber(name, type);

            parameterNames[index++] = name;
        }

        return parameterNames;

        void countParameter(NamedType signatureComponent)
        {
            if (counts.TryGetValue(signatureComponent.Name, out int count))
            {
                counts[signatureComponent.Name] = count - 1;
            }
            else
            {
                counts[signatureComponent.Name] = -1;
            }
        }

        string appendParameterNumber(string text, NamedType signatureComponent)
        {
            int count = counts[signatureComponent.Name];

            if (count == -1)
            {
                return text;
            }
            else if (count < 0)
            {
                counts[signatureComponent.Name] = 1;
                return $"{text}1";
            }
            else
            {
                counts[signatureComponent.Name] += 1;
                return $"{text}{counts[signatureComponent.Name]}";
            }
        }
    }

    private static string ProcessExpression(DerivableUnitDefinition definition, IReadOnlyCollection<string> parameterNames,
        IReadOnlyCollection<NamedType> quantitiesOfSignatureUnits)
    {
        string[] parameterNameAndQuantity = new string[parameterNames.Count];

        IEnumerator<string> parameterNameEnumerator = parameterNames.GetEnumerator();
        IEnumerator<NamedType> quantityEnumerator = quantitiesOfSignatureUnits.GetEnumerator();

        int index = 0;
        while (parameterNameEnumerator.MoveNext() && quantityEnumerator.MoveNext())
        {
            parameterNameAndQuantity[index] = $"{parameterNameEnumerator.Current}.{quantityEnumerator.Current.Name}.Magnitude";

            index += 1;
        }

        return string.Format(CultureInfo.InvariantCulture, definition.Expression, parameterNameAndQuantity);
    }
}
