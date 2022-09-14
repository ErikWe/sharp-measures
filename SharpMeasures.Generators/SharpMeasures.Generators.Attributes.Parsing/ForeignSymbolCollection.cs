namespace SharpMeasures.Generators.Attributes.Parsing;

using Microsoft.CodeAnalysis;

using SharpMeasures.Equatables;

using System;
using System.Collections;
using System.Collections.Generic;

public record class ForeignSymbolCollection : IReadOnlyCollection<INamedTypeSymbol>
{
    public static ForeignSymbolCollection Empty => new(Array.Empty<INamedTypeSymbol>());

    public int Count => symbols.Count;

    public IEnumerator<INamedTypeSymbol> GetEnumerator() => symbols.GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    private ReadOnlyEquatableCollection<INamedTypeSymbol> symbols { get; }

    public ForeignSymbolCollection(IReadOnlyCollection<INamedTypeSymbol> symbols)
    {
        this.symbols = symbols.AsReadOnlyEquatable();
    }
}
