namespace SharpMeasures.Generators.Parsing.Quantities.ConvertibleQuantityAttribute;

using Microsoft.CodeAnalysis;

using SharpMeasures;

using System.Collections.Generic;

/// <inheritdoc cref="IConvertibleQuantitySyntax"/>
internal sealed record class ConvertibleQuantitySyntax : IConvertibleQuantitySyntax
{
    private Location QuantitiesCollection { get; }
    private IReadOnlyList<Location> QuantitiesElements { get; }

    private Location ForwardsBehaviour { get; }
    private Location BackwardsBehaviour { get; }

    /// <summary>Instantiates a <see cref="ConvertibleQuantitySyntax"/>, representing syntactical information about a parsed <see cref="ConvertibleQuantityAttribute"/>.</summary>
    /// <param name="quantitiesCollection"><inheritdoc cref="IConvertibleQuantitySyntax.QuantitiesCollection" path="/summary"/></param>
    /// <param name="quantitiesElements"><inheritdoc cref="IConvertibleQuantitySyntax.QuantitiesElements" path="/summary"/></param>
    /// <param name="forwardsBehaviour"><inheritdoc cref="IConvertibleQuantitySyntax.ForwardsBehaviour" path="/summary"/></param>
    /// <param name="backwardsBehaviour"><inheritdoc cref="IConvertibleQuantitySyntax.BackwardsBehaviour" path="/summary"/></param>
    public ConvertibleQuantitySyntax(Location quantitiesCollection, IReadOnlyList<Location> quantitiesElements, Location forwardsBehaviour, Location backwardsBehaviour)
    {
        QuantitiesCollection = quantitiesCollection;
        QuantitiesElements = quantitiesElements;

        ForwardsBehaviour = forwardsBehaviour;
        BackwardsBehaviour = backwardsBehaviour;
    }

    Location IConvertibleQuantitySyntax.QuantitiesCollection => QuantitiesCollection;
    IReadOnlyList<Location> IConvertibleQuantitySyntax.QuantitiesElements => QuantitiesElements;

    Location IConvertibleQuantitySyntax.ForwardsBehaviour => ForwardsBehaviour;
    Location IConvertibleQuantitySyntax.BackwardsBehaviour => BackwardsBehaviour;
}
