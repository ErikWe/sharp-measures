#nullable enable

namespace ErikWe.SharpMeasures.Units;

using ErikWe.SharpMeasures.Quantities;

using System;

/// <summary>Describes a unit of the quantity <see cref="Quantities.TimeSquared"/>.</summary>
/// <remarks>Common <see cref="UnitOfTimeSquared"/> exists as static properties, and from these custom <see cref="UnitOfTimeSquared"/> may be derived using
/// the instance-methods <see cref="WithPrefix(MetricPrefix)"/> or <see cref="ScaledBy(Scalar)"/>. Custom <see cref="UnitOfTimeSquared"/> can also be derived from
/// other units using the static <see cref="From(UnitOfTime)"/>, or an overload.</remarks>
public readonly record struct UnitOfTimeSquared :
    IComparable<UnitOfTimeSquared>
{
    /// <summary>Derives a <see cref="UnitOfTimeSquared"/> according to { <paramref name="unitOfTime"/>² }.</summary>
    /// <param name="unitOfTime">A <see cref="UnitOfTimeSquared"/> is derived from squaring this <see cref="UnitOfTime"/>.</param>
    public static UnitOfTimeSquared From(UnitOfTime unitOfTime) => new(TimeSquared.From(unitOfTime.Time));
    /// <summary>Derives a <see cref="UnitOfTimeSquared"/> according to { <paramref name="unitOfTime1"/> ∙ <paramref name="unitOfTime2"/> }.</summary>
    /// <param name="unitOfTime1">A <see cref="UnitOfTimeSquared"/> is derived from multiplication of this <see cref="UnitOfTime"/> by <paramref name="unitOfTime2"/>.</param>
    /// <param name="unitOfTime2">A <see cref="UnitOfTimeSquared"/> is derived from multiplication of this <see cref="UnitOfTime"/> by <paramref name="unitOfTime1"/>.</param>
    public static UnitOfTimeSquared From(UnitOfTime unitOfTime1, UnitOfTime unitOfTime2) => new(TimeSquared.From(unitOfTime1.Time, unitOfTime2.Time));

    /// <summary>The SI unit of <see cref="Quantities.TimeSquared"/>, derived according to { <see cref="UnitOfTime.Second"/>² }. Usually written as [s²].</summary>
    public static UnitOfTimeSquared SquareSecond { get; } = From(UnitOfTime.Second);

    /// <summary>The <see cref="Quantities.TimeSquared"/> that the <see cref="UnitOfTimeSquared"/> represents.</summary>
    public TimeSquared TimeSquared { get; private init; }

    /// <summary>Constructs a new <see cref="UnitOfTimeSquared"/>, representing the <see cref="Quantities.TimeSquared"/> <paramref name="timeSquared"/>.</summary>
    /// <param name="timeSquared">The <see cref="Quantities.TimeSquared"/> that the new <see cref="UnitOfTimeSquared"/> represents.</param>
    private UnitOfTimeSquared(TimeSquared timeSquared)
    {
        TimeSquared = timeSquared;
    }

    /// <summary>Derives a new <see cref="UnitOfTimeSquared"/> from this instance, by prefixing the <see cref="MetricPrefix"/> <paramref name="prefix"/>.</summary>
    /// <param name="prefix">The <see cref="MetricPrefix"/> with which the new <see cref="UnitOfTimeSquared"/> is expressed.</param>
    /// <remarks>Any <see cref="MetricPrefix"/> applied to the original instance will be retained, essentially stacking the prefixes.</remarks>
    public UnitOfTimeSquared WithPrefix(MetricPrefix prefix) => new(TimeSquared * prefix.Scale);
    /// <summary>Derives a new <see cref="UnitOfTimeSquared"/> from this instance, through scaling by <paramref name="scale"/>.</summary>
    /// <param name="scale">The original <see cref="UnitOfTimeSquared"/> is scaled by this value.</param>
    public UnitOfTimeSquared ScaledBy(Scalar scale) => new(TimeSquared * scale);
    /// <summary>Derives a new <see cref="UnitOfTimeSquared"/> from this instance, through scaling by <paramref name="scale"/>.</summary>
    /// <param name="scale">The original <see cref="UnitOfTimeSquared"/> is scaled by this value.</param>
    public UnitOfTimeSquared ScaledBy(double scale) => new(TimeSquared * scale);

    /// <inheritdoc/>
    public int CompareTo(UnitOfTimeSquared other) => TimeSquared.CompareTo(other.TimeSquared);
    /// <summary>Produces a formatted string constisting of the type followed by the represented <see cref="Quantities.TimeSquared"/>.</summary>
    public override string ToString() => $"{GetType()}: {TimeSquared}";

    /// <summary>Determines whether the <see cref="Quantities.TimeSquared"/> represented by <paramref name="x"/> is
    /// less than that of <paramref name="y"/>.</summary>
    /// <param name="x">The method determines whether the <see cref="Quantities.TimeSquared"/> represented by this <see cref="UnitOfTimeSquared"/> is
    /// less than that of <paramref name="y"/>.</param>
    /// <param name="y">The method determines whether the <see cref="Quantities.TimeSquared"/> represented by <paramref name="x"/> is
    /// less than that of this <see cref="UnitOfTimeSquared"/>.</param>
    public static bool operator <(UnitOfTimeSquared x, UnitOfTimeSquared y) => x.TimeSquared < y.TimeSquared;
    /// <summary>Determines whether the <see cref="Quantities.TimeSquared"/> represented by <paramref name="x"/> is
    /// greater than that of <paramref name="y"/>.</summary>
    /// <param name="x">The method determines whether the <see cref="Quantities.TimeSquared"/> represented by this <see cref="UnitOfTimeSquared"/> is
    /// greater than that of <paramref name="y"/>.</param>
    /// <param name="y">The method determines whether the <see cref="Quantities.TimeSquared"/> represented by <paramref name="x"/> is
    /// greater than that of this <see cref="UnitOfTimeSquared"/>.</param>
    public static bool operator >(UnitOfTimeSquared x, UnitOfTimeSquared y) => x.TimeSquared > y.TimeSquared;
    /// <summary>Determines whether the <see cref="Quantities.TimeSquared"/> represented by <paramref name="x"/> is
    /// less than or equal to that of <paramref name="y"/>.</summary>
    /// <param name="x">The method determines whether the <see cref="Quantities.TimeSquared"/> represented by this <see cref="UnitOfTimeSquared"/> is
    /// less than or equal to that of <paramref name="y"/>.</param>
    /// <param name="y">The method determines whether the <see cref="Quantities.TimeSquared"/> represented by <paramref name="x"/> is
    /// less than or equal to that of this <see cref="UnitOfTimeSquared"/>.</param>
    public static bool operator <=(UnitOfTimeSquared x, UnitOfTimeSquared y) => x.TimeSquared <= y.TimeSquared;
    /// <summary>Determines whether the <see cref="Quantities.TimeSquared"/> represented by <paramref name="x"/> is
    /// greater than or equal to that of <paramref name="y"/>.</summary>
    /// <param name="x">The method determines whether the <see cref="Quantities.TimeSquared"/> represented by this <see cref="UnitOfTimeSquared"/> is
    /// greater than or equal to that of <paramref name="y"/>.</param>
    /// <param name="y">The method determines whether the <see cref="Quantities.TimeSquared"/> represented by <paramref name="x"/> is
    /// greater than or equal to that of this <see cref="UnitOfTimeSquared"/>.</param>
    public static bool operator >=(UnitOfTimeSquared x, UnitOfTimeSquared y) => x.TimeSquared >= y.TimeSquared;
}
