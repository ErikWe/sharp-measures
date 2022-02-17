namespace ErikWe.SharpMeasures.Units;

using ErikWe.SharpMeasures.Quantities;

using System;

/// <summary>Describes a unit of the quantity <see cref="Quantities.Frequency"/>.</summary>
/// <remarks>Common <see cref="UnitOfFrequency"/> exists as static properties, and from these custom <see cref="UnitOfFrequency"/> may be derived using
/// the instance-methods <see cref="WithPrefix(MetricPrefix)"/> or <see cref="ScaledBy(Scalar)"/>. Custom <see cref="UnitOfFrequency"/> can also be derived from
/// other units using the static <see cref="From(UnitOfTime)"/>.</remarks>
public readonly record struct UnitOfFrequency :
    IComparable<UnitOfFrequency>
{
    /// <summary>Derives a <see cref="UnitOfFrequency"/> according to { 1 / <paramref name="unitOfTime"/> }.</summary>
    /// <param name="unitOfTime">A <see cref="UnitOfFrequency"/> is derived from the inverse of this <see cref="UnitOfTime"/>.</param>
    public static UnitOfFrequency From(UnitOfTime unitOfTime) => new(Frequency.From(unitOfTime.Time));

    /// <summary>Expresses <see cref="Quantities.Frequency"/> according to { 1 / <see cref="UnitOfTime.Second"/> }. Usually written as [1/s] or [s⁻¹].</summary>
    /// <remarks>This is equivalent to <see cref="Hertz"/>.</remarks>
    public static UnitOfFrequency PerSecond { get; } = From(UnitOfTime.Second);
    /// <summary>Expresses <see cref="Quantities.Frequency"/> according to { 1 / <see cref="UnitOfTime.Minute"/> }. Usually written as [1/min] or [min⁻¹].</summary>
    public static UnitOfFrequency PerMinute { get; } = From(UnitOfTime.Minute);
    /// <summary>Expresses <see cref="Quantities.Frequency"/> according to { 1 / <see cref="UnitOfTime.Hour"/> }. Usually written as [1/hr].</summary>
    public static UnitOfFrequency PerHour { get; } = From(UnitOfTime.Hour);
    /// <summary>The SI unit of <see cref="Quantities.Frequency"/>, derived according to { 1 / <see cref="UnitOfTime.Second"/> }. Usually written as [Hz].</summary>
    /// <remarks>This is equivalent to <see cref="PerSecond"/>.</remarks>
    public static UnitOfFrequency Hertz { get; } = PerSecond;
    /// <summary>Expresses <see cref="Quantities.Frequency"/> as one thousand <see cref="Hertz"/>. Usually written as [kHz].</summary>
    public static UnitOfFrequency Kilohertz { get; } = Hertz.WithPrefix(MetricPrefix.Kilo);
    /// <summary>Expresses <see cref="Quantities.Frequency"/> as one million <see cref="Hertz"/>. Usually written as [MHz].</summary>
    public static UnitOfFrequency Megahertz { get; } = Hertz.WithPrefix(MetricPrefix.Mega);
    /// <summary>Expresses <see cref="Quantities.Frequency"/> as one billion [10^9] <see cref="Hertz"/>. Usually written as [GHz].</summary>
    public static UnitOfFrequency Gigahertz { get; } = Hertz.WithPrefix(MetricPrefix.Giga);
    /// <summary>Expresses <see cref="Quantities.Frequency"/> as one trillion [10^12] <see cref="Hertz"/>. Usually written as [THz].</summary>
    public static UnitOfFrequency Terahertz { get; } = Hertz.WithPrefix(MetricPrefix.Tera);

    /// <summary>The <see cref="Quantities.Frequency"/> that the <see cref="UnitOfFrequency"/> represents.</summary>
    public Frequency Frequency { get; private init; }

    /// <summary>Constructs a new <see cref="UnitOfFrequency"/>, representing the <see cref="Quantities.Frequency"/> <paramref name="frequency"/>.</summary>
    /// <param name="frequency">The <see cref="Quantities.Frequency"/> that the new <see cref="UnitOfFrequency"/> represents.</param>
    private UnitOfFrequency(Frequency frequency)
    {
        Frequency = frequency;
    }

    /// <summary>Derives a new <see cref="UnitOfFrequency"/> from this instance, by prefixing the <see cref="MetricPrefix"/> <paramref name="prefix"/>.</summary>
    /// <param name="prefix">The <see cref="MetricPrefix"/> with which the new <see cref="UnitOfFrequency"/> is expressed.</param>
    /// <remarks>Any <see cref="MetricPrefix"/> applied to the original instance will be retained, essentially stacking the prefixes.</remarks>
    public UnitOfFrequency WithPrefix(MetricPrefix prefix) => new(Frequency * prefix.Scale);
    /// <summary>Derives a new <see cref="UnitOfFrequency"/> from this instance, through scaling by <paramref name="scale"/>.</summary>
    /// <param name="scale">The original <see cref="UnitOfFrequency"/> is scaled by this value.</param>
    public UnitOfFrequency ScaledBy(Scalar scale) => new(Frequency * scale);
    /// <summary>Derives a new <see cref="UnitOfFrequency"/> from this instance, through scaling by <paramref name="scale"/>.</summary>
    /// <param name="scale">The original <see cref="UnitOfFrequency"/> is scaled by this value.</param>
    public UnitOfFrequency ScaledBy(double scale) => new(Frequency * scale);

    /// <inheritdoc/>
    public int CompareTo(UnitOfFrequency other) => Frequency.CompareTo(other.Frequency);
    /// <summary>Produces a formatted string constisting of the type followed by the represented <see cref="Quantities.Frequency"/>.</summary>
    public override string ToString() => $"{GetType()}: {Frequency}";

    /// <summary>Determines whether the <see cref="Quantities.Frequency"/> represented by <paramref name="x"/> is
    /// less than that of <paramref name="y"/>.</summary>
    /// <param name="x">The method determines whether the <see cref="Quantities.Frequency"/> represented by this <see cref="UnitOfFrequency"/> is
    /// less than that of <paramref name="y"/>.</param>
    /// <param name="y">The method determines whether the <see cref="Quantities.Frequency"/> represented by <paramref name="x"/> is
    /// less than that of this <see cref="UnitOfFrequency"/>.</param>
    public static bool operator <(UnitOfFrequency x, UnitOfFrequency y) => x.Frequency < y.Frequency;
    /// <summary>Determines whether the <see cref="Quantities.Frequency"/> represented by <paramref name="x"/> is
    /// greater than that of <paramref name="y"/>.</summary>
    /// <param name="x">The method determines whether the <see cref="Quantities.Frequency"/> represented by this <see cref="UnitOfFrequency"/> is
    /// greater than that of <paramref name="y"/>.</param>
    /// <param name="y">The method determines whether the <see cref="Quantities.Frequency"/> represented by <paramref name="x"/> is
    /// greater than that of this <see cref="UnitOfFrequency"/>.</param>
    public static bool operator >(UnitOfFrequency x, UnitOfFrequency y) => x.Frequency > y.Frequency;
    /// <summary>Determines whether the <see cref="Quantities.Frequency"/> represented by <paramref name="x"/> is
    /// less than or equal to that of <paramref name="y"/>.</summary>
    /// <param name="x">The method determines whether the <see cref="Quantities.Frequency"/> represented by this <see cref="UnitOfFrequency"/> is
    /// less than or equal to that of <paramref name="y"/>.</param>
    /// <param name="y">The method determines whether the <see cref="Quantities.Frequency"/> represented by <paramref name="x"/> is
    /// less than or equal to that of this <see cref="UnitOfFrequency"/>.</param>
    public static bool operator <=(UnitOfFrequency x, UnitOfFrequency y) => x.Frequency <= y.Frequency;
    /// <summary>Determines whether the <see cref="Quantities.Frequency"/> represented by <paramref name="x"/> is
    /// greater than or equal to that of <paramref name="y"/>.</summary>
    /// <param name="x">The method determines whether the <see cref="Quantities.Frequency"/> represented by this <see cref="UnitOfFrequency"/> is
    /// greater than or equal to that of <paramref name="y"/>.</param>
    /// <param name="y">The method determines whether the <see cref="Quantities.Frequency"/> represented by <paramref name="x"/> is
    /// greater than or equal to that of this <see cref="UnitOfFrequency"/>.</param>
    public static bool operator >=(UnitOfFrequency x, UnitOfFrequency y) => x.Frequency >= y.Frequency;
}
