namespace ErikWe.SharpMeasures.Units;

using ErikWe.SharpMeasures.Quantities;

using System;

/// <summary>Describes a unit of the quantity <see cref="Quantities.SolidAngle"/>.</summary>
/// <remarks>Common <see cref="UnitOfSolidAngle"/> exists as static properties, and from these custom <see cref="UnitOfSolidAngle"/> may be derived using
/// the instance-methods <see cref="WithPrefix(MetricPrefix)"/> or <see cref="ScaledBy(Scalar)"/>. Custom <see cref="UnitOfSolidAngle"/> can also be derived from
/// other units using the static <see cref="From(UnitOfAngle)"/>, or an overload.</remarks>
public readonly record struct UnitOfSolidAngle :
    IComparable<UnitOfSolidAngle>
{
    /// <summary>Derives a <see cref="UnitOfSolidAngle"/> according to { <paramref name="unitOfAngle"/>² }.</summary>
    /// <param name="unitOfAngle">A <see cref="UnitOfSolidAngle"/> is derived from squaring this <see cref="UnitOfAngle"/>.</param>
    public static UnitOfSolidAngle From(UnitOfAngle unitOfAngle) => new(SolidAngle.From(unitOfAngle.Angle));
    /// <summary>Derives a <see cref="UnitOfSolidAngle"/> according to { <paramref name="unitOfAngle1"/> * <paramref name="unitOfAngle2"/> }.</summary>
    /// <param name="unitOfAngle1">A <see cref="UnitOfSolidAngle"/> is derived from multiplication of this <see cref="UnitOfAngle"/> by <paramref name="unitOfAngle2"/>.</param>
    /// <param name="unitOfAngle2">A <see cref="UnitOfSolidAngle"/> is derived from multiplication of this <see cref="UnitOfAngle"/> by <paramref name="unitOfAngle1"/>.</param>
    public static UnitOfSolidAngle From(UnitOfAngle unitOfAngle1, UnitOfAngle unitOfAngle2) => new(SolidAngle.From(unitOfAngle1.Angle, unitOfAngle2.Angle));

    /// <summary>The SI unit of <see cref="Quantities.SolidAngle"/>, derived according to { <see cref="UnitOfAngle.Radian"/>² }. Usually written as [sr].</summary>
    /// <remarks>This is equivalent to <see cref="SquareRadian"/>.</remarks>
    public static UnitOfSolidAngle Steradian { get; } = From(UnitOfAngle.Radian);
    /// <summary>Expresses <see cref="Quantities.SolidAngle"/> according to { <see cref="UnitOfAngle.Radian"/>² }. Usually written as [rad²].</summary>
    /// <remarks>This is equivalent to <see cref="Steradian"/>.</remarks>
    public static UnitOfSolidAngle SquareRadian { get; } = Steradian;
    /// <summary>Expresses <see cref="Quantities.SolidAngle"/> according to { <see cref="UnitOfAngle.Degree"/>² }. Usually written as [deg²].</summary>
    public static UnitOfSolidAngle SquareDegree { get; } = From(UnitOfAngle.Degree);
    /// <summary>Expresses <see cref="Quantities.SolidAngle"/> according to { <see cref="UnitOfAngle.Arcminute"/>² }. Usually written as [arcmin²].</summary>
    public static UnitOfSolidAngle SquareArcminute { get; } = From(UnitOfAngle.Arcminute);
    /// <summary>Expresses <see cref="Quantities.SolidAngle"/> according to { <see cref="UnitOfAngle.Arcsecond"/>² }. Usually written as [arcsec²].</summary>
    public static UnitOfSolidAngle SquareArcsecond { get; } = From(UnitOfAngle.Arcsecond);

    /// <summary>The <see cref="Quantities.SolidAngle"/> that the <see cref="UnitOfSolidAngle"/> represents.</summary>
    public SolidAngle SolidAngle { get; private init; }

    /// <summary>Constructs a new <see cref="UnitOfSolidAngle"/>, representing the <see cref="Quantities.SolidAngle"/> <paramref name="solidAngle"/>.</summary>
    /// <param name="solidAngle">The <see cref="Quantities.SolidAngle"/> that the new <see cref="UnitOfSolidAngle"/> represents.</param>
    private UnitOfSolidAngle(SolidAngle solidAngle)
    {
        SolidAngle = solidAngle;
    }

    /// <summary>Derives a new <see cref="UnitOfSolidAngle"/> from this instance, by prefixing the <see cref="MetricPrefix"/> <paramref name="prefix"/>.</summary>
    /// <param name="prefix">The <see cref="MetricPrefix"/> with which the new <see cref="UnitOfSolidAngle"/> is expressed.</param>
    /// <remarks>Any <see cref="MetricPrefix"/> applied to the original instance will be retained, essentially stacking the prefixes.</remarks>
    public UnitOfSolidAngle WithPrefix(MetricPrefix prefix) => new(SolidAngle * prefix.Scale);
    /// <summary>Derives a new <see cref="UnitOfSolidAngle"/> from this instance, through scaling by <paramref name="scale"/>.</summary>
    /// <param name="scale">The original <see cref="UnitOfSolidAngle"/> is scaled by this value.</param>
    public UnitOfSolidAngle ScaledBy(Scalar scale) => new(SolidAngle * scale);
    /// <summary>Derives a new <see cref="UnitOfSolidAngle"/> from this instance, through scaling by <paramref name="scale"/>.</summary>
    /// <param name="scale">The original <see cref="UnitOfSolidAngle"/> is scaled by this value.</param>
    public UnitOfSolidAngle ScaledBy(double scale) => new(SolidAngle * scale);

    /// <inheritdoc/>
    public int CompareTo(UnitOfSolidAngle other) => SolidAngle.CompareTo(other.SolidAngle);
    /// <summary>Produces a formatted string constisting of the type followed by the represented <see cref="Quantities.SolidAngle"/>.</summary>
    public override string ToString() => $"{GetType()}: {SolidAngle}";

    /// <summary>Determines whether the <see cref="Quantities.SolidAngle"/> represented by <paramref name="x"/> is
    /// less than that of <paramref name="y"/>.</summary>
    /// <param name="x">The method determines whether the <see cref="Quantities.SolidAngle"/> represented by this <see cref="UnitOfSolidAngle"/> is
    /// less than that of <paramref name="y"/>.</param>
    /// <param name="y">The method determines whether the <see cref="Quantities.SolidAngle"/> represented by <paramref name="x"/> is
    /// less than that of this <see cref="UnitOfSolidAngle"/>.</param>
    public static bool operator <(UnitOfSolidAngle x, UnitOfSolidAngle y) => x.SolidAngle < y.SolidAngle;
    /// <summary>Determines whether the <see cref="Quantities.SolidAngle"/> represented by <paramref name="x"/> is
    /// greater than that of <paramref name="y"/>.</summary>
    /// <param name="x">The method determines whether the <see cref="Quantities.SolidAngle"/> represented by this <see cref="UnitOfSolidAngle"/> is
    /// greater than that of <paramref name="y"/>.</param>
    /// <param name="y">The method determines whether the <see cref="Quantities.SolidAngle"/> represented by <paramref name="x"/> is
    /// greater than that of this <see cref="UnitOfSolidAngle"/>.</param>
    public static bool operator >(UnitOfSolidAngle x, UnitOfSolidAngle y) => x.SolidAngle > y.SolidAngle;
    /// <summary>Determines whether the <see cref="Quantities.SolidAngle"/> represented by <paramref name="x"/> is
    /// less than or equal to that of <paramref name="y"/>.</summary>
    /// <param name="x">The method determines whether the <see cref="Quantities.SolidAngle"/> represented by this <see cref="UnitOfSolidAngle"/> is
    /// less than or equal to that of <paramref name="y"/>.</param>
    /// <param name="y">The method determines whether the <see cref="Quantities.SolidAngle"/> represented by <paramref name="x"/> is
    /// less than or equal to that of this <see cref="UnitOfSolidAngle"/>.</param>
    public static bool operator <=(UnitOfSolidAngle x, UnitOfSolidAngle y) => x.SolidAngle <= y.SolidAngle;
    /// <summary>Determines whether the <see cref="Quantities.SolidAngle"/> represented by <paramref name="x"/> is
    /// greater than or equal to that of <paramref name="y"/>.</summary>
    /// <param name="x">The method determines whether the <see cref="Quantities.SolidAngle"/> represented by this <see cref="UnitOfSolidAngle"/> is
    /// greater than or equal to that of <paramref name="y"/>.</param>
    /// <param name="y">The method determines whether the <see cref="Quantities.SolidAngle"/> represented by <paramref name="x"/> is
    /// greater than or equal to that of this <see cref="UnitOfSolidAngle"/>.</param>
    public static bool operator >=(UnitOfSolidAngle x, UnitOfSolidAngle y) => x.SolidAngle >= y.SolidAngle;
}
