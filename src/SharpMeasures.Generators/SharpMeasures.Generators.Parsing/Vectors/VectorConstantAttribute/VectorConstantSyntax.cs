namespace SharpMeasures.Generators.Parsing.Vectors.VectorConstantAttribute;

using Microsoft.CodeAnalysis;

using SharpMeasures;

using System.Collections.Generic;

/// <inheritdoc cref="IVectorConstantSyntax"/>
internal sealed record class VectorConstantSyntax : IVectorConstantSyntax
{
    private Location Name { get; }
    private Location UnitInstanceName { get; }
    private Location ValueCollection { get; }
    private IReadOnlyList<Location> ValueElements { get; }

    /// <summary>Instantiates a <see cref="VectorConstantSyntax"/>, representing syntactical information about a parsed <see cref="VectorConstantAttribute"/>.</summary>
    /// <param name="name"><inheritdoc cref="IVectorConstantSyntax.Name" path="/summary"/></param>
    /// <param name="unitInstanceName"><inheritdoc cref="IVectorConstantSyntax.UnitInstanceName" path="/summary"/></param>
    /// <param name="valueCollection"><inheritdoc cref="IVectorConstantSyntax.ValueCollection" path="/summary"/></param>
    /// <param name="valueElements"><inheritdoc cref="IVectorConstantSyntax.ValueElements" path="/summary"/></param>
    public VectorConstantSyntax(Location name, Location unitInstanceName, Location valueCollection, IReadOnlyList<Location> valueElements)
    {
        Name = name;
        UnitInstanceName = unitInstanceName;
        ValueCollection = valueCollection;
        ValueElements = valueElements;
    }

    Location IVectorConstantSyntax.Name => Name;
    Location IVectorConstantSyntax.UnitInstanceName => UnitInstanceName;
    Location IVectorConstantSyntax.ValueCollection => ValueCollection;
    IReadOnlyList<Location> IVectorConstantSyntax.ValueElements => ValueElements;
}
