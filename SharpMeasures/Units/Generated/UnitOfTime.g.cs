#nullable enable

namespace ErikWe.SharpMeasures.Units;

using ErikWe.SharpMeasures.Quantities;

using System;

/// <summary>Describes a unit of the quantity <see cref="Quantities.Time"/>.</summary>
/// <remarks>Common <see cref="UnitOfTime"/> exists as static properties, and from these custom <see cref="UnitOfTime"/> may be derived using
/// the instance-methods <see cref="WithPrefix(MetricPrefix)"/> or <see cref="ScaledBy(Scalar)"/>.</remarks>
public readonly record struct UnitOfTime :
    IComparable<UnitOfTime>
{
    /// <summary>The SI unit of <see cref="Quantities.Time"/>. Usually written as [s].</summary>
    public static UnitOfTime Second { get; } = new(new Time(1));
    /// <summary>Expresses <see cref="Quantities.Time"/> according to { 60 ∙ <see cref="Second"/> }. Usually written [min].</summary>
    public static UnitOfTime Minute { get; } = Second.ScaledBy(60);
    /// <summary>Expresses <see cref="Quantities.Time"/> according to { 60 ∙ <see cref="Minute"/> }. Usually written [hr].</summary>
    public static UnitOfTime Hour { get; } = Minute.ScaledBy(60);
    /// <summary>Expresses <see cref="Quantities.Time"/> according to { 24 ∙ <see cref="Hour"/> }.</summary>
    public static UnitOfTime Day { get; } = Hour.ScaledBy(24);
    /// <summary>Expresses <see cref="Quantities.Time"/> according to { 7 ∙ <see cref="Day"/> }.</summary>
    public static UnitOfTime Week { get; } = Day.ScaledBy(7);
    /// <summary>Expresses <see cref="Quantities.Time"/> in terms of the duration of non-leap year, according to { 365 ∙ <see cref="Day"/> }. Usually written [yr].</summary>
    public static UnitOfTime CommonYear { get; } = Day.ScaledBy(365);
    /// <summary>Expresses <see cref="Quantities.Time"/> in terms of the duration of year in the Julian calendar, according to { 365.25 ∙ <see cref="Day"/> }.</summary>
    public static UnitOfTime JulianYear { get; } = Day.ScaledBy(365.25);

    /// <summary>Expresses <see cref="Quantities.Time"/> as one quadrillionth [10^(-15)] of a <see cref="Second"/>. Usually written as [fs].</summary>
    public static UnitOfTime Femtosecond { get; } = Second.WithPrefix(MetricPrefix.Femto);
    /// <summary>Expresses <see cref="Quantities.Time"/> as one trillionth [10^(-12)] of a <see cref="Second"/>. Usually written as [ps].</summary>
    public static UnitOfTime Picosecond { get; } = Second.WithPrefix(MetricPrefix.Pico);
    /// <summary>Expresses <see cref="Quantities.Time"/> as one billionth [10^(-9)] of a <see cref="Second"/>. Usually written as [ns].</summary>
    public static UnitOfTime Nanosecond { get; } = Second.WithPrefix(MetricPrefix.Nano);
    /// <summary>Expresses <see cref="Quantities.Time"/> as one millionth of a <see cref="Second"/>. Usually written as [μs].</summary>
    public static UnitOfTime Microsecond { get; } = Second.WithPrefix(MetricPrefix.Micro);
    /// <summary>Expresses <see cref="Quantities.Time"/> as one thousandth of a <see cref="Second"/>. Usually written as [ms].</summary>
    public static UnitOfTime Millisecond { get; } = Second.WithPrefix(MetricPrefix.Milli);

    /// <summary>The <see cref="Quantities.Time"/> that the <see cref="UnitOfTime"/> represents.</summary>
    public Time Time { get; private init; }

    /// <summary>Constructs a new <see cref="UnitOfTime"/>, representing the <see cref="Quantities.Time"/> <paramref name="time"/>.</summary>
    /// <param name="time">The <see cref="Quantities.Time"/> that the new <see cref="UnitOfTime"/> represents.</param>
    private UnitOfTime(Time time)
    {
        Time = time;
    }

    /// <summary>Derives a new <see cref="UnitOfTime"/> from this instance, by prefixing the <see cref="MetricPrefix"/> <paramref name="prefix"/>.</summary>
    /// <param name="prefix">The <see cref="MetricPrefix"/> with which the new <see cref="UnitOfTime"/> is expressed.</param>
    /// <remarks>Any <see cref="MetricPrefix"/> applied to the original instance will be retained, essentially stacking the prefixes.</remarks>
    public UnitOfTime WithPrefix(MetricPrefix prefix) => new(Time * prefix.Scale);
    /// <summary>Derives a new <see cref="UnitOfTime"/> from this instance, through scaling by <paramref name="scale"/>.</summary>
    /// <param name="scale">The original <see cref="UnitOfTime"/> is scaled by this value.</param>
    public UnitOfTime ScaledBy(Scalar scale) => new(Time * scale);
    /// <summary>Derives a new <see cref="UnitOfTime"/> from this instance, through scaling by <paramref name="scale"/>.</summary>
    /// <param name="scale">The original <see cref="UnitOfTime"/> is scaled by this value.</param>
    public UnitOfTime ScaledBy(double scale) => new(Time * scale);

    /// <inheritdoc/>
    public int CompareTo(UnitOfTime other) => Time.CompareTo(other.Time);
    /// <summary>Produces a formatted string constisting of the type followed by the represented <see cref="Quantities.Time"/>.</summary>
    public override string ToString() => $"{GetType()}: {Time}";

    /// <summary>Determines whether the <see cref="Quantities.Time"/> represented by <paramref name="x"/> is
    /// less than that of <paramref name="y"/>.</summary>
    /// <param name="x">The method determines whether the <see cref="Quantities.Time"/> represented by this <see cref="UnitOfTime"/> is
    /// less than that of <paramref name="y"/>.</param>
    /// <param name="y">The method determines whether the <see cref="Quantities.Time"/> represented by <paramref name="x"/> is
    /// less than that of this <see cref="UnitOfTime"/>.</param>
    public static bool operator <(UnitOfTime x, UnitOfTime y) => x.Time < y.Time;
    /// <summary>Determines whether the <see cref="Quantities.Time"/> represented by <paramref name="x"/> is
    /// greater than that of <paramref name="y"/>.</summary>
    /// <param name="x">The method determines whether the <see cref="Quantities.Time"/> represented by this <see cref="UnitOfTime"/> is
    /// greater than that of <paramref name="y"/>.</param>
    /// <param name="y">The method determines whether the <see cref="Quantities.Time"/> represented by <paramref name="x"/> is
    /// greater than that of this <see cref="UnitOfTime"/>.</param>
    public static bool operator >(UnitOfTime x, UnitOfTime y) => x.Time > y.Time;
    /// <summary>Determines whether the <see cref="Quantities.Time"/> represented by <paramref name="x"/> is
    /// less than or equal to that of <paramref name="y"/>.</summary>
    /// <param name="x">The method determines whether the <see cref="Quantities.Time"/> represented by this <see cref="UnitOfTime"/> is
    /// less than or equal to that of <paramref name="y"/>.</param>
    /// <param name="y">The method determines whether the <see cref="Quantities.Time"/> represented by <paramref name="x"/> is
    /// less than or equal to that of this <see cref="UnitOfTime"/>.</param>
    public static bool operator <=(UnitOfTime x, UnitOfTime y) => x.Time <= y.Time;
    /// <summary>Determines whether the <see cref="Quantities.Time"/> represented by <paramref name="x"/> is
    /// greater than or equal to that of <paramref name="y"/>.</summary>
    /// <param name="x">The method determines whether the <see cref="Quantities.Time"/> represented by this <see cref="UnitOfTime"/> is
    /// greater than or equal to that of <paramref name="y"/>.</param>
    /// <param name="y">The method determines whether the <see cref="Quantities.Time"/> represented by <paramref name="x"/> is
    /// greater than or equal to that of this <see cref="UnitOfTime"/>.</param>
    public static bool operator >=(UnitOfTime x, UnitOfTime y) => x.Time >= y.Time;
}
