namespace SharpMeasures.Generators.Units.Processing;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Attributes.Parsing.Units;
using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.SourceBuilding;
using SharpMeasures.Generators.Units;
using SharpMeasures.Generators.Units.Processing.Diagnostics;

using System.Collections.Generic;
using System.Globalization;

internal class DerivableUnitProcessingContext : IProcessingContext
{
    public DefinedType Type { get; }

    public NamedTypePopulation<UnitInterface> UnitPopulation { get; }

    public DerivableUnitProcessingContext(DefinedType type, NamedTypePopulation<UnitInterface> unitPopulation)
    {
        Type = type;
        UnitPopulation = unitPopulation;
    }
}

internal class DerivableUnitProcesser : IProcesser<DerivableUnitProcessingContext, DerivableUnitDefinition, ProcessedDerivableUnit>
{
    public static DerivableUnitProcesser Instance { get; } = new();

    private DerivableUnitProcesser() { }

    public IOptionalWithDiagnostics<ProcessedDerivableUnit> Process(DerivableUnitProcessingContext context, DerivableUnitDefinition input)
    {
        NamedType[] quantitiesOfSignatureUnits = new NamedType[input.Signature.Types.Count];

        int index = 0;
        foreach (NamedType type in input.Signature.Types)
        {
            if (context.UnitPopulation.Population.TryGetValue(type, out UnitInterface unit) is false)
            {
                return OptionalWithDiagnostics.Empty<ProcessedDerivableUnit>(DerivableUnitDiagnostics.TypeNotUnit(input, index, type));
            }

            quantitiesOfSignatureUnits[index] = unit.QuantityType;

            index += 1;
        }

        var signatureParameterNames = GetSignatureParameterNames(input.Signature);
        string processedExpression = ProcessExpression(input, signatureParameterNames, quantitiesOfSignatureUnits);

        ProcessedDerivableUnit product = new(processedExpression, input.Signature, signatureParameterNames);
        return OptionalWithDiagnostics.Result(product);
    }

    private static IReadOnlyList<string> GetSignatureParameterNames(DerivableSignature signature)
    {
        Dictionary<string, int> counts = new();

        foreach (NamedType signatureComponent in signature.Types)
        {
            countParameter(signatureComponent);
        }

        string[] parameterNames = new string[signature.Types.Count];

        int index = 0;
        foreach (NamedType type in signature.Types)
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
