namespace SharpMeasures.Generators.Attributes.Parsing.Units.Caching;

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

public readonly record struct CachedDerivedUnitAttributeParameters(string Name, string Plural, ReadOnlyCollection<string>? Signature,
    ReadOnlyCollection<string> Units)
    : IUnitAttributeParameters
{
    public static CachedDerivedUnitAttributeParameters From(DerivedUnitAttributeParameters parameters)
    {
        if (parameters.Signature is null)
        {
            return new(string.Empty, string.Empty, null, Array.AsReadOnly(Array.Empty<string>()));
        }

        string[] signature = new string[parameters.Signature.Count];

        for (int i = 0; i < parameters.Signature.Count; i++)
        {
            signature[i] = parameters.Signature[i].ToDisplayString();
        }

        return new(parameters.Name, parameters.Plural, Array.AsReadOnly(signature), parameters.Units);
    }

    public static IEnumerable<CachedDerivedUnitAttributeParameters> From(IEnumerable<DerivedUnitAttributeParameters> parameters)
    {
        if (parameters is null)
        {
            yield break;
        }

        foreach (DerivedUnitAttributeParameters parameterSet in parameters)
        {
            yield return From(parameterSet);
        }
    }
}