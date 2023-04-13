namespace SharpMeasures.Generators.Parsing.Quantities.ConvertibleQuantityAttribute;

using Microsoft.CodeAnalysis;

using SharpMeasures;

using System.Collections.Generic;

/// <inheritdoc cref="IRawConvertibleQuantity"/>
internal sealed record class RawConvertibleQuantity : IRawConvertibleQuantity
{
    private IReadOnlyList<ITypeSymbol?>? Quantities { get; }

    private ConversionOperatorBehaviour? ForwardsBehaviour { get; }
    private ConversionOperatorBehaviour? BackwardsBehaviour { get; }

    private IConvertibleQuantitySyntax? Syntax { get; }

    /// <summary>Instantiates a <see cref="RawConvertibleQuantity"/>, representing a parsed <see cref="ConvertibleQuantityAttribute"/>.</summary>
    /// <param name="quantities"><inheritdoc cref="IRawConvertibleQuantity.Quantities" path="/summary"/></param>
    /// <param name="forwardsBehaviour"><inheritdoc cref="IRawConvertibleQuantity.ForwardsBehaviour" path="/summary"/></param>
    /// <param name="backwardsBehaviour"><inheritdoc cref="IRawConvertibleQuantity.BackwardsBehaviour" path="/summary"/></param>
    /// <param name="syntax"><inheritdoc cref="IRawConvertibleQuantity.Syntax" path="/summary"/></param>
    public RawConvertibleQuantity(IReadOnlyList<ITypeSymbol?>? quantities, ConversionOperatorBehaviour? forwardsBehaviour, ConversionOperatorBehaviour? backwardsBehaviour, IConvertibleQuantitySyntax? syntax)
    {
        Quantities = quantities;

        ForwardsBehaviour = forwardsBehaviour;
        BackwardsBehaviour = backwardsBehaviour;

        Syntax = syntax;
    }

    IReadOnlyList<ITypeSymbol?>? IRawConvertibleQuantity.Quantities => Quantities;

    ConversionOperatorBehaviour? IRawConvertibleQuantity.ForwardsBehaviour => ForwardsBehaviour;
    ConversionOperatorBehaviour? IRawConvertibleQuantity.BackwardsBehaviour => BackwardsBehaviour;

    IConvertibleQuantitySyntax? IRawConvertibleQuantity.Syntax => Syntax;
}
