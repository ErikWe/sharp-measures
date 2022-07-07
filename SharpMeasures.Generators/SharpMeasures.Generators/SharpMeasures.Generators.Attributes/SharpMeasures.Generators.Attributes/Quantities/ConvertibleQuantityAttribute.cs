namespace SharpMeasures.Generators.Quantities;

using SharpMeasures.Generators.Utility;

using System;

/// <summary>Indicates that a quantity supports conversion to the listed quantities.</summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, AllowMultiple = true)]
public sealed class ConvertibleQuantityAttribute : Attribute
{
    /// <summary>The set of quantities to which this quantity supports conversion.</summary>
    public Type[] Quantities { get; }

    /// <summary>Indicates that the quantities listed in <see cref="Quantities"/> should support conversion to this quantity, whenever possible. The default
    /// behaviour is <see langword="false"/>.</summary>
    /// <remarks>This is only possible if the listed quantity shares assembly with this quantity.</remarks>
    public bool Bidirectional { get; init; }

    /// <summary>Determines the behaviour of the conversion operators. The default behaviour is <see cref="ConversionOperatorBehaviour.Explicit"/>.</summary>
    public ConversionOperatorBehaviour CastOperatorBehaviour { get; init; } = ConversionOperatorBehaviour.Explicit;

    /// <inheritdoc cref="ConvertibleQuantityAttribute"/>
    /// <param name="quantities"><inheritdoc cref="Quantities" path="/summary"/><para><inheritdoc cref="Quantities" path="/remarks"/></para></param>
    public ConvertibleQuantityAttribute(params Type[] quantities)
    {
        Quantities = quantities;
    }
}
