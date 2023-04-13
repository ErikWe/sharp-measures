namespace SharpMeasures.Generators.Parsing.Quantities;

using Microsoft.CodeAnalysis;

using System.Collections.Generic;

/// <summary>Represents syntactical information about a parsed <see cref="ConvertibleQuantityAttribute"/>.</summary>
public interface IConvertibleQuantitySyntax
{
    /// <summary>The <see cref="Location"/> of the argument for the target quantities.</summary>
    public abstract Location QuantitiesCollection { get; }

    /// <summary>The <see cref="Location"/> of each individual element in the argument for the target quantities.</summary>
    public abstract IReadOnlyList<Location> QuantitiesElements { get; }

    /// <summary>The <see cref="Location"/> of the argument for how the conversion operators from the provided quantities to this quantity are implemented. May be <see cref="Location.None"/> if no argument was provided.</summary>
    public abstract Location ForwardsBehaviour { get; }

    /// <summary>The <see cref="Location"/> of the argument for how the conversion operators from this quantity to the provided quantities are implemented. May be <see cref="Location.None"/> if no argument was provided.</summary>
    public abstract Location BackwardsBehaviour { get; }
}
