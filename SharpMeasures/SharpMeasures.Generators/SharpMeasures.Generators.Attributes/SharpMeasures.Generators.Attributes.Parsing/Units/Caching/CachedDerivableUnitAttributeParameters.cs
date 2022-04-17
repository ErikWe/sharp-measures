namespace SharpMeasures.Generators.Attributes.Parsing.Units.Caching;

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

public readonly record struct CachedDerivableUnitAttributeParameters(ReadOnlyCollection<string?> Signature, ReadOnlyCollection<string?> Quantities,
    string Expression)
{
    public static CachedDerivableUnitAttributeParameters From(Units.DerivableUnitAttributeParameters parameters)
    {
        string?[] signature = new string[parameters.Signature.Count];

        for (int i = 0; i < parameters.Signature.Count; i++)
        {
            signature[i] = parameters.Signature[i]?.ToDisplayString();
        }

        string?[] quantities = new string[parameters.Quantities.Count];

        for (int i = 0; i < parameters.Quantities.Count; i++)
        {
            quantities[i] = parameters.Quantities[i]?.ToDisplayString();
        }

        return new(Array.AsReadOnly(signature), Array.AsReadOnly(quantities), parameters.Expression);
    }

    public static IEnumerable<CachedDerivableUnitAttributeParameters> From(IEnumerable<Units.DerivableUnitAttributeParameters> parameters)
    {
        if (parameters is null)
        {
            yield break;
        }

        foreach (Units.DerivableUnitAttributeParameters parameterSet in parameters)
        {
            yield return From(parameterSet);
        }
    }
}