namespace SharpMeasures.Generators.Attributes.Parsing.Units.Caching;

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

public readonly record struct CachedDerivedUnitAttributeParameters(string Name, string Plural, string Symbol, bool IsSIUnit, bool IsConstant,
    ReadOnlyCollection<string?> Signature, ReadOnlyCollection<string> Units)
    : IUnitAttributeParameters
{
    public static CachedDerivedUnitAttributeParameters From(Units.DerivedUnitAttributeParameters parameters)
    {
        string?[] signature = new string[parameters.Signature.Count];

        for (int i = 0; i < parameters.Signature.Count; i++)
        {
            signature[i] = parameters.Signature[i]?.ToDisplayString();
        }

        return new(parameters.Name, parameters.Plural, parameters.Symbol, parameters.IsSIUnit, parameters.IsConstant,
            Array.AsReadOnly(signature), parameters.Units);
    }

    public static IEnumerable<CachedDerivedUnitAttributeParameters> From(IEnumerable<Units.DerivedUnitAttributeParameters> parameters)
    {
        if (parameters is null)
        {
            yield break;
        }

        foreach (Units.DerivedUnitAttributeParameters parameterSet in parameters)
        {
            yield return From(parameterSet);
        }
    }
}