#nullable enable

namespace ErikWe.SharpMeasures.Units;

using ErikWe.SharpMeasures.Quantities;

using System;

/// <summary>Describes a unit of the quantity <see cref="Quantities.Area"/>.</summary>
/// <remarks>Common <see cref="UnitOfArea"/> exists as static properties, and from these custom <see cref="UnitOfArea"/> may be derived using
/// the instance-methods <see cref="WithPrefix(MetricPrefix)"/> or <see cref="ScaledBy(Scalar)"/>. Custom <see cref="UnitOfArea"/> can also be derived from
/// other units using the static <see cref="From(UnitOfLength)"/>, or an overload.</remarks>
public readonly record struct UnitOfArea :
    IComparable<UnitOfArea>
{
    /// <summary>Derives a <see cref="UnitOfArea"/> according to { <paramref name="unitOfLength"/>² }.</summary>
    /// <param name="unitOfLength">A <see cref="UnitOfArea"/> is derived from squaring this <see cref="UnitOfLength"/>.</param>
    public static UnitOfArea From(UnitOfLength unitOfLength) => new(Area.From(unitOfLength.Length));
    /// <summary>Derives a <see cref="UnitOfArea"/> according to { <paramref name="unitOfLength1"/> ∙ <paramref name="unitOfLength2"/> }.</summary>
    /// <param name="unitOfLength1">A <see cref="UnitOfArea"/> is derived from multiplication of this <see cref="UnitOfLength"/> by <paramref name="unitOfLength2"/>.</param>
    /// <param name="unitOfLength2">A <see cref="UnitOfArea"/> is derived from multiplication of this <see cref="UnitOfLength"/> by <paramref name="unitOfLength1"/>.</param>
    public static UnitOfArea From(UnitOfLength unitOfLength1, UnitOfLength unitOfLength2) => new(Area.From(unitOfLength1.Length, unitOfLength2.Length));

    /// <summary>The SI unit of <see cref="Quantities.Area"/>, derived according to { <see cref="UnitOfLength.Metre"/>² }. Usually written as [m²].</summary>
    public static UnitOfArea SquareMetre { get; } = From(UnitOfLength.Metre);
    /// <summary>Expresses <see cref="Quantities.Area"/> according to { <see cref="UnitOfLength.Kilometre"/>² }. Usually written as [km²].</summary>
    public static UnitOfArea SquareKilometre { get; } = From(UnitOfLength.Kilometre);
    /// <summary>Expresses <see cref="Quantities.Area"/> according to { <see cref="UnitOfLength.Inch"/>² }. Usually written as [sq in].</summary>
    public static UnitOfArea SquareInch { get; } = From(UnitOfLength.Inch);
    /// <summary>Expresses <see cref="Quantities.Area"/> according to { <see cref="UnitOfLength.Mile"/>² }. Usually written as [sq mi].</summary>
    public static UnitOfArea SquareMile { get; } = From(UnitOfLength.Mile);

    /// <summary>Expresses <see cref="Quantities.Area"/> as a square with sides 10 <see cref="UnitOfLength.Metre"/> - equal to { 100 ∙ <see cref="SquareMetre"/> }.</summary>
    /// <remarks>Not to be confused with <see cref="Acre"/>.</remarks>
    public static UnitOfArea Are { get; } = SquareMetre.ScaledBy(100);
    /// <summary>Expresses <see cref="Quantities.Area"/> as a square with sides 100 <see cref="UnitOfLength.Metre"/> - equal to { 10 000 ∙ <see cref="SquareMetre"/> }.
    /// Usually written as [ha].</summary>
    public static UnitOfArea Hectare { get; } = Are.WithPrefix(MetricPrefix.Hecto);
    /// <summary>Expresses <see cref="Quantities.Area"/> according to { 1/640 ∙ <see cref="SquareMile"/> }. Usually written as [ac].</summary>
    public static UnitOfArea Acre { get; } = SquareMile.ScaledBy(1d / 640);

    /// <summary>The <see cref="Quantities.Area"/> that the <see cref="UnitOfArea"/> represents.</summary>
    public Area Area { get; private init; }

    /// <summary>Constructs a new <see cref="UnitOfArea"/>, representing the <see cref="Quantities.Area"/> <paramref name="area"/>.</summary>
    /// <param name="area">The <see cref="Quantities.Area"/> that the new <see cref="UnitOfArea"/> represents.</param>
    private UnitOfArea(Area area)
    {
        Area = area;
    }

    /// <summary>Derives a new <see cref="UnitOfArea"/> from this instance, by prefixing the <see cref="MetricPrefix"/> <paramref name="prefix"/>.</summary>
    /// <param name="prefix">The <see cref="MetricPrefix"/> with which the new <see cref="UnitOfArea"/> is expressed.</param>
    /// <remarks>Any <see cref="MetricPrefix"/> applied to the original instance will be retained, essentially stacking the prefixes.</remarks>
    public UnitOfArea WithPrefix(MetricPrefix prefix) => new(Area * prefix.Scale);
    /// <summary>Derives a new <see cref="UnitOfArea"/> from this instance, through scaling by <paramref name="scale"/>.</summary>
    /// <param name="scale">The original <see cref="UnitOfArea"/> is scaled by this value.</param>
    public UnitOfArea ScaledBy(Scalar scale) => new(Area * scale);
    /// <summary>Derives a new <see cref="UnitOfArea"/> from this instance, through scaling by <paramref name="scale"/>.</summary>
    /// <param name="scale">The original <see cref="UnitOfArea"/> is scaled by this value.</param>
    public UnitOfArea ScaledBy(double scale) => new(Area * scale);

    /// <inheritdoc/>
    public int CompareTo(UnitOfArea other) => Area.CompareTo(other.Area);
    /// <summary>Produces a formatted string constisting of the type followed by the represented <see cref="Quantities.Area"/>.</summary>
    public override string ToString() => $"{GetType()}: {Area}";

    /// <summary>Determines whether the <see cref="Quantities.Area"/> represented by <paramref name="x"/> is
    /// less than that of <paramref name="y"/>.</summary>
    /// <param name="x">The method determines whether the <see cref="Quantities.Area"/> represented by this <see cref="UnitOfArea"/> is
    /// less than that of <paramref name="y"/>.</param>
    /// <param name="y">The method determines whether the <see cref="Quantities.Area"/> represented by <paramref name="x"/> is
    /// less than that of this <see cref="UnitOfArea"/>.</param>
    public static bool operator <(UnitOfArea x, UnitOfArea y) => x.Area < y.Area;
    /// <summary>Determines whether the <see cref="Quantities.Area"/> represented by <paramref name="x"/> is
    /// greater than that of <paramref name="y"/>.</summary>
    /// <param name="x">The method determines whether the <see cref="Quantities.Area"/> represented by this <see cref="UnitOfArea"/> is
    /// greater than that of <paramref name="y"/>.</param>
    /// <param name="y">The method determines whether the <see cref="Quantities.Area"/> represented by <paramref name="x"/> is
    /// greater than that of this <see cref="UnitOfArea"/>.</param>
    public static bool operator >(UnitOfArea x, UnitOfArea y) => x.Area > y.Area;
    /// <summary>Determines whether the <see cref="Quantities.Area"/> represented by <paramref name="x"/> is
    /// less than or equal to that of <paramref name="y"/>.</summary>
    /// <param name="x">The method determines whether the <see cref="Quantities.Area"/> represented by this <see cref="UnitOfArea"/> is
    /// less than or equal to that of <paramref name="y"/>.</param>
    /// <param name="y">The method determines whether the <see cref="Quantities.Area"/> represented by <paramref name="x"/> is
    /// less than or equal to that of this <see cref="UnitOfArea"/>.</param>
    public static bool operator <=(UnitOfArea x, UnitOfArea y) => x.Area <= y.Area;
    /// <summary>Determines whether the <see cref="Quantities.Area"/> represented by <paramref name="x"/> is
    /// greater than or equal to that of <paramref name="y"/>.</summary>
    /// <param name="x">The method determines whether the <see cref="Quantities.Area"/> represented by this <see cref="UnitOfArea"/> is
    /// greater than or equal to that of <paramref name="y"/>.</param>
    /// <param name="y">The method determines whether the <see cref="Quantities.Area"/> represented by <paramref name="x"/> is
    /// greater than or equal to that of this <see cref="UnitOfArea"/>.</param>
    public static bool operator >=(UnitOfArea x, UnitOfArea y) => x.Area >= y.Area;
}
