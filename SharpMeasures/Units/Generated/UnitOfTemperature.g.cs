#nullable enable

namespace ErikWe.SharpMeasures.Units;

using ErikWe.SharpMeasures.Quantities;

/// <summary>Describes a unit of the quantities <see cref="Quantities.Temperature"/> and <see cref="Quantities.TemperatureDifference"/>.</summary>
/// <remarks>Common <see cref="UnitOfTemperature"/> exists as static properties, and from these custom <see cref="UnitOfTemperature"/> may be derived using
/// the instance-methods <see cref="WithPrefix(MetricPrefix)"/>, <see cref="ScaledBy(Scalar)"/>, or <see cref="OffsetBy(Scalar)"/>.</remarks>
public readonly record struct UnitOfTemperature
{
    /// <summary>The SI unit of <see cref="Quantities.Temperature"/>, with 0 representing absolute zero. Usually written as [K].</summary>
    /// <remarks>When used to express <see cref="Quantities.TemperatureDifference"/>, this is equivalent to <see cref="Celsius"/>.</remarks>
    public static UnitOfTemperature Kelvin { get; } = new(new TemperatureDifference(1), new Scalar(0));
    /// <summary>Expresses <see cref="Quantities.Temperature"/> according to { <see cref="Kelvin"/> + 273.15 }. Usually written as [°C]</summary>
    /// <remarks>When used to express <see cref="Quantities.TemperatureDifference"/>, the offset of [273.15] has no effect - and the unit is
    /// equivalent to <see cref="Kelvin"/>.</remarks>
    public static UnitOfTemperature Celsius { get; } = new(Kelvin.TemperatureDifference, new Scalar(-273.15));

    /// <summary>Expresses <see cref="Quantities.Temperature"/> according to { 1.8 ∙ <see cref="Kelvin"/> }. Usually written as [°R or °Ra].</summary>
    /// <remarks>When used to express <see cref="Quantities.TemperatureDifference"/>, this is equivalent to <see cref="Fahrenheit"/>.</remarks>
    public static UnitOfTemperature Rankine { get; } = Kelvin.ScaledBy(5/9);
    /// <summary>Expresses <see cref="Quantities.Temperature"/> according to { <see cref="Rankine"/> + 459.67 }. Usually written as [°F]</summary>
    /// <remarks>When used to express <see cref="Quantities.TemperatureDifference"/>, the offset of [459.67] has no effect - and the unit is
    /// equivalent to <see cref="Rankine"/>.</remarks>
    public static UnitOfTemperature Fahrenheit { get; } = new(Rankine.TemperatureDifference, new Scalar(-459.67));

    /// <summary>The <see cref="TemperatureDifference"/> that a unit step of <see cref="UnitOfTemperature"/> represents.</summary>
    public TemperatureDifference TemperatureDifference { get; private init; }

    /// <summary>The value of the <see cref="UnitOfTemperature"/> at absolute zero.</summary>
    public Scalar Offset { get; private init; }

    /// <summary>Constructs a new <see cref="UnitOfTemperature"/>, where a unit step represents <paramref name="temperatureDifference"/>, and
    /// the value at absolute zero is <paramref name="offset"/>.</summary>
    /// <param name="temperatureDifference">The <see cref="TemperatureDifference"/> that a unit step of <see cref="UnitOfTemperature"/> represents.</param>
    /// <param name="offset">The value of the <see cref="UnitOfTemperature"/> at absolute zero.</param>
    private UnitOfTemperature(TemperatureDifference temperatureDifference, Scalar offset)
    {
        TemperatureDifference = temperatureDifference;
        Offset = offset;
    }

    /// <summary>Derives a new <see cref="UnitOfTemperature"/> from this instance, by prefixing the <see cref="MetricPrefix"/> <paramref name="prefix"/>.</summary>
    /// <param name="prefix">The <see cref="MetricPrefix"/> with which the new <see cref="UnitOfTemperature"/> is expressed.</param>
    /// <remarks>Any <see cref="MetricPrefix"/> applied to the original instance will be retained, essentially stacking the prefixes.</remarks>
    public UnitOfTemperature WithPrefix(MetricPrefix prefix) => new(TemperatureDifference * prefix.Scale, Offset);
    /// <summary>Derives a new <see cref="UnitOfTemperature"/> from this instance, through scaling by <paramref name="scale"/>.</summary>
    /// <param name="scale">The original <see cref="UnitOfTemperature"/> is scaled by this value.</param>
    public UnitOfTemperature ScaledBy(Scalar scale) => new(TemperatureDifference * scale, Offset);
    /// <summary>Derives a new <see cref="UnitOfTemperature"/> from this instance, through scaling by <paramref name="scale"/>.</summary>
    /// <param name="scale">The original <see cref="UnitOfTemperature"/> is scaled by this value.</param>
    public UnitOfTemperature ScaledBy(double scale) => new(TemperatureDifference * scale, Offset);
    /// <summary>Derives a new <see cref="UnitOfTemperature"/> from this instance, with an offset of <paramref name="offset"/>.</summary>
    /// <param name="offset">The original <see cref="UnitOfTemperature"/> is offset by this value.</param>
    public UnitOfTemperature OffsetBy(Scalar offset) => new(TemperatureDifference, Offset + offset);
    /// <summary>Derives a new <see cref="UnitOfTemperature"/> from this instance, with an offset of <paramref name="offset"/>.</summary>
    /// <param name="offset">The original <see cref="UnitOfTemperature"/> is offset by this value.</param>
    public UnitOfTemperature OffsetBy(double offset) => new(TemperatureDifference, Offset + offset);

    /// <summary>Produces a formatted string constisting of the type followed by the represented <see cref="Quantities.Temperature"/>.</summary>
    public override string ToString() => $"{GetType()}: {TemperatureDifference} + {Offset}";
}
