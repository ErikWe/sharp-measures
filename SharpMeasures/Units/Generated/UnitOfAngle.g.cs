#nullable enable

namespace ErikWe.SharpMeasures.Units;

using ErikWe.SharpMeasures.Quantities;

using System;

/// <summary>Describes a unit of the quantity <see cref="Quantities.Angle"/>, and related quantities.</summary>
/// <remarks>Common <see cref="UnitOfAngle"/> exists as static properties, and from these custom <see cref="UnitOfAngle"/> may be derived using
/// the instance-methods <see cref="WithPrefix(MetricPrefix)"/> or <see cref="ScaledBy(Scalar)"/>.</remarks>
public readonly record struct UnitOfAngle :
    IComparable<UnitOfAngle>
{
    /// <summary>The SI unit of <see cref="Quantities.Angle"/>, equal to { 1/(2π) ∙ <see cref="Turn"/> }. Usually written as [rad].</summary>
    public static UnitOfAngle Radian { get; } = new(new Angle(1));
    /// <summary>Expresses <see cref="Quantities.Angle"/> as one thousandth of a <see cref="Radian"/>. Usually written as [mrad].</summary>
    public static UnitOfAngle Milliradian { get; } = Radian.WithPrefix(MetricPrefix.Milli);

    /// <summary>Expresses <see cref="Quantities.Angle"/> according to { 1/360 ∙ <see cref="Turn"/> }. Usually written as [°] or [deg].</summary>
    public static UnitOfAngle Degree { get; } = Radian.ScaledBy(Math.PI / 180);
    /// <summary>Expresses <see cref="Quantities.Angle"/> according to { 1/400 ∙ <see cref="Turn"/> }. Also known as the gon, and usually written as [grad] or [gon].</summary>
    public static UnitOfAngle Gradian { get; } = Radian.ScaledBy(Math.PI / 200);

    /// <summary>Expresses <see cref="Quantities.Angle"/> according to { 1/60 ∙ <see cref="Degree"/> }. Usually written as [′] or [arcmin].</summary>
    public static UnitOfAngle Arcminute { get; } = Degree.ScaledBy(1d / 60);
    /// <summary>Expresses <see cref="Quantities.Angle"/> according to { 1/60 ∙ <see cref="Arcminute"/> }. Usually written as [″] or [arcsec].</summary>
    public static UnitOfAngle Arcsecond { get; } = Arcminute.ScaledBy(1d / 60);
    /// <summary>Expresses <see cref="Quantities.Angle"/> as one thousandth of a <see cref="Arcsecond"/>. Usually written as [mas].</summary>
    public static UnitOfAngle Milliarcsecond { get; } = Arcsecond.WithPrefix(MetricPrefix.Milli);
    /// <summary>Expresses <see cref="Quantities.Angle"/> as one millionth of a <see cref="Arcsecond"/>. Usually written as [μas].</summary>
    public static UnitOfAngle Microarcsecond { get; } = Arcsecond.WithPrefix(MetricPrefix.Micro);

    /// <summary>Expresses <see cref="Quantities.Angle"/> in terms of the number of full turns. Also known as a cycle, revolution, or full rotation - and
    /// usually written as [tr], [rev.], [cyc.], or [rot.].[</summary>
    public static UnitOfAngle Turn { get; } = Radian.ScaledBy(Math.Tau);
    /// <summary>Expresses <see cref="Quantities.Angle"/> according to { 1/2 ∙ <see cref="Turn"/> }.</summary>
    public static UnitOfAngle HalfTurn { get; } = Turn.ScaledBy(.5d);
    /// <summary>Expresses <see cref="Quantities.Angle"/> according to { 1/4 ∙ <see cref="Turn"/> }.</summary>
    public static UnitOfAngle QuarterTurn { get; } = Turn.ScaledBy(.25d);
    /// <summary>Expresses <see cref="Quantities.Angle"/> as one hundredth of a <see cref="Turn"/>.</summary>
    public static UnitOfAngle Centiturn { get; } = Turn.WithPrefix(MetricPrefix.Centi);
    /// <summary>Expresses <see cref="Quantities.Angle"/> as one thousandth of a <see cref="Turn"/>.</summary>
    public static UnitOfAngle Milliturn { get; } = Turn.WithPrefix(MetricPrefix.Milli);

    /// <summary>Expresses <see cref="Quantities.Angle"/> according to { 1/256 ∙ <see cref="Turn"/> }.</summary>
    public static UnitOfAngle BinaryDegree { get; } = Turn.ScaledBy(1d / 256);

    /// <summary>The <see cref="Quantities.Angle"/> that the <see cref="UnitOfAngle"/> represents.</summary>
    public Angle Angle { get; private init; }

    /// <summary>Constructs a new <see cref="UnitOfAngle"/>, representing the <see cref="Quantities.Angle"/> <paramref name="angle"/>.</summary>
    /// <param name="angle">The <see cref="Quantities.Angle"/> that the new <see cref="UnitOfAngle"/> represents.</param>
    private UnitOfAngle(Angle angle)
    {
        Angle = angle;
    }

    /// <summary>Derives a new <see cref="UnitOfAngle"/> from this instance, by prefixing the <see cref="MetricPrefix"/> <paramref name="prefix"/>.</summary>
    /// <param name="prefix">The <see cref="MetricPrefix"/> with which the new <see cref="UnitOfAngle"/> is expressed.</param>
    /// <remarks>Any <see cref="MetricPrefix"/> applied to the original instance will be retained, essentially stacking the prefixes.</remarks>
    public UnitOfAngle WithPrefix(MetricPrefix prefix) => new(Angle * prefix.Scale);
    /// <summary>Derives a new <see cref="UnitOfAngle"/> from this instance, through scaling by <paramref name="scale"/>.</summary>
    /// <param name="scale">The original <see cref="UnitOfAngle"/> is scaled by this value.</param>
    public UnitOfAngle ScaledBy(Scalar scale) => new(Angle * scale);
    /// <summary>Derives a new <see cref="UnitOfAngle"/> from this instance, through scaling by <paramref name="scale"/>.</summary>
    /// <param name="scale">The original <see cref="UnitOfAngle"/> is scaled by this value.</param>
    public UnitOfAngle ScaledBy(double scale) => new(Angle * scale);

    /// <inheritdoc/>
    public int CompareTo(UnitOfAngle other) => Angle.CompareTo(other.Angle);
    /// <summary>Produces a formatted string constisting of the type followed by the represented <see cref="Quantities.Angle"/>.</summary>
    public override string ToString() => $"{GetType()}: {Angle}";

    /// <summary>Determines whether the <see cref="Quantities.Angle"/> represented by <paramref name="x"/> is
    /// less than that of <paramref name="y"/>.</summary>
    /// <param name="x">The method determines whether the <see cref="Quantities.Angle"/> represented by this <see cref="UnitOfAngle"/> is
    /// less than that of <paramref name="y"/>.</param>
    /// <param name="y">The method determines whether the <see cref="Quantities.Angle"/> represented by <paramref name="x"/> is
    /// less than that of this <see cref="UnitOfAngle"/>.</param>
    public static bool operator <(UnitOfAngle x, UnitOfAngle y) => x.Angle < y.Angle;
    /// <summary>Determines whether the <see cref="Quantities.Angle"/> represented by <paramref name="x"/> is
    /// greater than that of <paramref name="y"/>.</summary>
    /// <param name="x">The method determines whether the <see cref="Quantities.Angle"/> represented by this <see cref="UnitOfAngle"/> is
    /// greater than that of <paramref name="y"/>.</param>
    /// <param name="y">The method determines whether the <see cref="Quantities.Angle"/> represented by <paramref name="x"/> is
    /// greater than that of this <see cref="UnitOfAngle"/>.</param>
    public static bool operator >(UnitOfAngle x, UnitOfAngle y) => x.Angle > y.Angle;
    /// <summary>Determines whether the <see cref="Quantities.Angle"/> represented by <paramref name="x"/> is
    /// less than or equal to that of <paramref name="y"/>.</summary>
    /// <param name="x">The method determines whether the <see cref="Quantities.Angle"/> represented by this <see cref="UnitOfAngle"/> is
    /// less than or equal to that of <paramref name="y"/>.</param>
    /// <param name="y">The method determines whether the <see cref="Quantities.Angle"/> represented by <paramref name="x"/> is
    /// less than or equal to that of this <see cref="UnitOfAngle"/>.</param>
    public static bool operator <=(UnitOfAngle x, UnitOfAngle y) => x.Angle <= y.Angle;
    /// <summary>Determines whether the <see cref="Quantities.Angle"/> represented by <paramref name="x"/> is
    /// greater than or equal to that of <paramref name="y"/>.</summary>
    /// <param name="x">The method determines whether the <see cref="Quantities.Angle"/> represented by this <see cref="UnitOfAngle"/> is
    /// greater than or equal to that of <paramref name="y"/>.</param>
    /// <param name="y">The method determines whether the <see cref="Quantities.Angle"/> represented by <paramref name="x"/> is
    /// greater than or equal to that of this <see cref="UnitOfAngle"/>.</param>
    public static bool operator >=(UnitOfAngle x, UnitOfAngle y) => x.Angle >= y.Angle;
}
