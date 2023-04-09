namespace SharpMeasures;

using System;

/// <summary>Applied to SharpMeasures quantities, indicating that the quantity supports conversion to and/or from the listed quantities.</summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, AllowMultiple = true)]
public sealed class ConvertibleQuantityAttribute : Attribute
{
    /// <summary>The set of quantities to and/or from which the implementing quantity supports conversion.</summary>
    public Type[] Quantities { get; }

    /// <summary>Determines how the conversion operators from the provided quantities to this quantity are implemented. The default behaviour is <see cref="ConversionOperatorBehaviour.Explicit"/>.</summary>
    public ConversionOperatorBehaviour ForwardsBehaviour { get; init; }

    /// <summary>Determines how the conversion operators from this quantity to the provided quantities are implemented. The default behaviour is <see cref="ConversionOperatorBehaviour.Implicit"/>.</summary>
    public ConversionOperatorBehaviour BackwardsBehaviour { get; init; }

    /// <inheritdoc cref="ConvertibleQuantityAttribute"/>
    /// <param name="quantities"><inheritdoc cref="Quantities" path="/summary"/></param>
    public ConvertibleQuantityAttribute(params Type[] quantities)
    {
        Quantities = quantities;
    }
}
