namespace SharpMeasures.Generators.Quantities;

using System;

/// <summary>Indicates that a quantity supports conversion to and/or from the listed quantities.</summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, AllowMultiple = true)]
public sealed class ConvertibleQuantityAttribute : Attribute
{
    /// <summary>The set of quantities to and/or from which this quantity supports conversion.</summary>
    public Type[] Quantities { get; }

    /// <summary>Determines the direction in which the conversion is implemented. The default behaviour is <see cref="QuantityConversionDirection.Onedirectional"/>.</summary>
    public QuantityConversionDirection ConversionDirection { get; init; }

    /// <summary>Determines the behaviour of the conversion operators. The default behaviour is <see cref="ConversionOperatorBehaviour.Explicit"/>.</summary>
    public ConversionOperatorBehaviour CastOperatorBehaviour { get; init; }

    /// <inheritdoc cref="ConvertibleQuantityAttribute"/>
    /// <param name="quantities"><inheritdoc cref="Quantities" path="/summary"/><para><inheritdoc cref="Quantities" path="/remarks"/></para></param>
    public ConvertibleQuantityAttribute(params Type[] quantities)
    {
        Quantities = quantities;
    }
}
