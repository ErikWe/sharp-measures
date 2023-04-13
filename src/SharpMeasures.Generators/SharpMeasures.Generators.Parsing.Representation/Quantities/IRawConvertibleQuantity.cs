namespace SharpMeasures.Generators.Parsing.Quantities;

using Microsoft.CodeAnalysis;

using System.Collections.Generic;

/// <summary>Represents a parsed <see cref="ConvertibleQuantityAttribute"/>.</summary>
public interface IRawConvertibleQuantity
{
    /// <summary>The set of quantities to and/or from which the implementing quantity supports conversion.</summary>
    public abstract IReadOnlyList<ITypeSymbol?>? Quantities { get; }

    /// <summary>Determines how the conversion operators from the provided quantities to this quantity are implemented.</summary>
    public abstract ConversionOperatorBehaviour? ForwardsBehaviour { get; }

    /// <summary>Determines how the conversion operators from this quantity to the provided quantities are implemented.</summary>
    public abstract ConversionOperatorBehaviour? BackwardsBehaviour { get; }

    /// <summary>Provides syntactic information about the parsed <see cref="ConvertibleQuantityAttribute"/>.</summary>
    public abstract IConvertibleQuantitySyntax? Syntax { get; }
}
