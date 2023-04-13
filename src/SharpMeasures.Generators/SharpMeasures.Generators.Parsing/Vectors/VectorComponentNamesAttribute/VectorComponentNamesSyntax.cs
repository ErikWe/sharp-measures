namespace SharpMeasures.Generators.Parsing.Vectors.VectorComponentNamesAttribute;

using Microsoft.CodeAnalysis;

using SharpMeasures;

using System.Collections.Generic;

/// <inheritdoc cref="IVectorComponentNamesSyntax"/>
internal sealed record class VectorComponentNamesSyntax : IVectorComponentNamesSyntax
{
    private Location NamesCollection { get; }
    private IReadOnlyList<Location> NamesElements { get; }

    /// <summary>Instantiates a <see cref="VectorComponentNamesSyntax"/>, representing syntactical information about a parsed <see cref="VectorComponentNamesAttribute"/>.</summary>
    /// <param name="namesCollection"><inheritdoc cref="IVectorComponentNamesSyntax.NamesCollection" path="/summary"/></param>
    /// <param name="namesElements"><inheritdoc cref="IVectorComponentNamesSyntax.NamesElements" path="/summary"/></param>
    public VectorComponentNamesSyntax(Location namesCollection, IReadOnlyList<Location> namesElements)
    {
        NamesCollection = namesCollection;
        NamesElements = namesElements;
    }

    Location IVectorComponentNamesSyntax.NamesCollection => NamesCollection;
    IReadOnlyList<Location> IVectorComponentNamesSyntax.NamesElements => NamesElements;
}
