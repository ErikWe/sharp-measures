namespace SharpMeasures.Generators;

using System;
using System.Collections.Generic;
using System.Linq;

public readonly record struct NamedTypePopulation<T> : IEquatable<NamedTypePopulation<T>>
{
    public IReadOnlyDictionary<NamedType, T> Population { get; }

    public NamedTypePopulation(IReadOnlyDictionary<NamedType, T> population)
    {
        Population = population;
    }

    public NamedTypePopulation(IEnumerable<T> items, Func<T, NamedType> itemNameDelegate)
    {
        Population = items.ToDictionary((item) => itemNameDelegate(item));
    }

    public bool Equals(NamedTypePopulation<T> other)
    {
        if (Population.Count != other.Population.Count)
        {
            return false;
        }

        foreach (KeyValuePair<NamedType, T> entry in Population)
        {
            if (other.Population.TryGetValue(entry.Key, out T otherEntry) is false)
            {
                return false;
            }

            if (entry.Equals(otherEntry) is false)
            {
                return false;
            }
        }

        return true;
    }

    public override int GetHashCode()
    {
        return Population.GetSequenceHashCode();
    }
}
