namespace SharpMeasures.SourceGeneration.Units.SourceBuilding;

using SharpMeasures.SourceGeneration.Attributes.Parsing.Units;
using SharpMeasures.SourceGeneration.Units.Pipeline;
using SharpMeasures.SourceGeneration.Utility;

using Microsoft.CodeAnalysis;

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;

internal static class DerivationsComposer
{
    public static void Append(StringBuilder source, FifthStage.Result data, CancellationToken _)
    {
        bool anyDerivations = false;

        foreach (DerivedUnitAttributeParameters derivation in data.Derivations)
        {
            anyDerivations = true;

            string[] parameterNames = GetSignatureParameterNames(derivation.Signature);

            IEnumerable<string> signatureComponents()
            {
                for (int i = 0; i < derivation.Signature.Count; i++)
                {
                    yield return $"{derivation.Signature[i]?.ToDisplayString()} {parameterNames[i]}";
                }
            }

            source.Append($"\tpublic static {data.TypeSymbol.Name} From(");
            SourceBuildingUtility.AppendEnumerable(source, signatureComponents(), ", ");
            source.Append($") => new({ParseExpression(derivation, parameterNames)});{Environment.NewLine}");
        }

        if (anyDerivations)
        {
            source.Append(Environment.NewLine);
        }
    }

    private static string[] GetSignatureParameterNames(IReadOnlyList<INamedTypeSymbol?> signature)
    {
        static void incrementCount(Dictionary<INamedTypeSymbol, int> counts, INamedTypeSymbol symbol)
        {
            if (counts.TryGetValue(symbol, out int count))
            {
                counts[symbol] = count + (count == 0 ? 2 : 1);
            }
            else
            {
                counts[symbol] = 0;
            }
        }

        static string getParameterNumber(Dictionary<INamedTypeSymbol, int> counts, INamedTypeSymbol symbol)
        {
            int count = counts[symbol];
            counts[symbol] = count - 1;
            return count > 0 ? count.ToString(CultureInfo.InvariantCulture) : "";
        }

        Dictionary<INamedTypeSymbol, int> counts = new(signature.Count, SymbolEqualityComparer.Default); 

        foreach (INamedTypeSymbol? unit in signature)
        {
            if (unit is not null)
            {
                incrementCount(counts, unit);
            }
        }

        string[] names = new string[signature.Count];
        for (int i = signature.Count - 1; i >= 0; i--)
        {
            if (signature[i] is INamedTypeSymbol unit)
            {
                names[i] = $"{SourceBuildingUtility.ToParameterName(unit.Name)}{getParameterNumber(counts, unit)}";
            }
        }

        return names;
    }

    private static string ParseExpression(DerivedUnitAttributeParameters parameters, string[] parameterNames)
    {
        string expression = parameters.Expression;
        for (int i = 0; i < parameters.Signature.Count; i++)
        {
            Regex regex = new($@"\{{{i}\}}");
            expression = regex.Replace(expression, $"{parameterNames[i]}.{parameters.Quantities[i]?.Name}");
        }

        return expression;
    }
}
