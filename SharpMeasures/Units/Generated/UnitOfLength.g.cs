#nullable enable

namespace ErikWe.SharpMeasures.Units;

using ErikWe.SharpMeasures.Quantities;

using System;

/// <summary>Describes a unit of the quantity <see cref="Quantities.Length"/>, and related quantities.</summary>
/// <remarks>Common <see cref="UnitOfLength"/> exists as static properties, and from these custom <see cref="UnitOfLength"/> may be derived using
/// the instance-methods <see cref="WithPrefix(MetricPrefix)"/> or <see cref="ScaledBy(Scalar)"/>.</remarks>
public readonly record struct UnitOfLength :
    IComparable<UnitOfLength>
{
    /// <summary>The SI unit of <see cref="Quantities.Length"/>. Usually written as [m].</summary>
    public static UnitOfLength Metre { get; } = new(new Length(1));
    /// <summary>Expresses <see cref="Quantities.Length"/> as one quadrillionth [10^(-15)] of a <see cref="Metre"/>. Usually written as [fm].</summary>
    public static UnitOfLength Femtometre { get; } = Metre.WithPrefix(MetricPrefix.Femto);
    /// <summary>Expresses <see cref="Quantities.Length"/> as one trillionth [10^(-12)] of a <see cref="Metre"/>. Usually written as [pm].</summary>
    public static UnitOfLength Picometre { get; } = Metre.WithPrefix(MetricPrefix.Pico);
    /// <summary>Expresses <see cref="Quantities.Length"/> as one billionth [10^(-9)] of a <see cref="Metre"/>. Usually written as [nm].</summary>
    public static UnitOfLength Nanometre { get; } = Metre.WithPrefix(MetricPrefix.Nano);
    /// <summary>Expresses <see cref="Quantities.Length"/> as one millionth of a <see cref="Metre"/>. Usually written as [μm].</summary>
    public static UnitOfLength Micrometre { get; } = Metre.WithPrefix(MetricPrefix.Micro);
    /// <summary>Expresses <see cref="Quantities.Length"/> as one thousandth of a <see cref="Metre"/>. Usually written as [mm].</summary>
    public static UnitOfLength Millimetre { get; } = Metre.WithPrefix(MetricPrefix.Milli);
    /// <summary>Expresses <see cref="Quantities.Length"/> as one hundredth of a <see cref="Metre"/>. Usually written as [cm].</summary>
    public static UnitOfLength Centimetre { get; } = Metre.WithPrefix(MetricPrefix.Centi);
    /// <summary>Expresses <see cref="Quantities.Length"/> as one tenth of a <see cref="Metre"/>. Usually written as [dm].</summary>
    public static UnitOfLength Decimetre { get; } = Metre.WithPrefix(MetricPrefix.Deci);
    /// <summary>Expresses <see cref="Quantities.Length"/> as one thousand <see cref="Metre"/>. Usually written as [km].</summary>
    public static UnitOfLength Kilometre { get; } = Metre.WithPrefix(MetricPrefix.Kilo);

    /// <summary>Expresses <see cref="Quantities.Length"/> in terms of the average distance (approximately) between Earth and the Sun. Usually written as [AU].</summary>
    public static UnitOfLength AstronomicalUnit { get; } = Metre.ScaledBy(1.495978797 * Math.Pow(10, 11));
    /// <summary>Expresses <see cref="Quantities.Length"/> in terms of the distance that light travels in one <see cref="UnitOfTime.JulianYear"/>.
    /// Usually written as [ly].</summary>
    public static UnitOfLength LightYear { get; } = Metre.ScaledBy(9460730472580800);
    /// <summary>Expresses <see cref="Quantities.Length"/> in terms of the distance from which one <see cref="AstronomicalUnit"/> covers one
    /// <see cref="UnitOfAngle.Arcsecond"/>. Usually written as [pc].</summary>
    public static UnitOfLength Parsec { get; } = AstronomicalUnit.ScaledBy(648000 / Math.PI);
    /// <summary>Expresses <see cref="Quantities.Length"/> as one thousand <see cref="Parsec"/>. Usually written as [kpc].</summary>
    public static UnitOfLength Kiloparsec { get; } = Parsec.WithPrefix(MetricPrefix.Kilo);
    /// <summary>Expresses <see cref="Quantities.Length"/> as one million <see cref="Parsec"/>. Usually written as [Mpc].</summary>
    public static UnitOfLength Megaparsec { get; } = Parsec.WithPrefix(MetricPrefix.Mega);
    /// <summary>Expresses <see cref="Quantities.Length"/> as one billion [10^9] <see cref="Parsec"/>. Usually written as [Gpc].</summary>
    public static UnitOfLength Gigaparsec { get; } = Parsec.WithPrefix(MetricPrefix.Giga);

    /// <summary>Expresses <see cref="Quantities.Length"/> according to { 1/12 ∙ <see cref="Foot"/> }. Usually written as [in] or [″].</summary>
    public static UnitOfLength Inch { get; } = Millimetre.ScaledBy(25.4);
    /// <summary>Expresses <see cref="Quantities.Length"/> according to { 12 ∙ <see cref="Inch"/>} or { 1/3 ∙ <see cref="Yard"/> }. Usually written as [ft] or [′].</summary>
    public static UnitOfLength Foot { get; } = Inch.ScaledBy(12);
    /// <summary>Expresses <see cref="Quantities.Length"/> according to { 3 ∙ <see cref="Foot"/> }. Usually written as [yd].</summary>
    public static UnitOfLength Yard { get; } = Foot.ScaledBy(3);
    /// <summary>Expresses <see cref="Quantities.Length"/> according to { 1760 ∙ <see cref="Yard"/> }. Usually written as [mi.].</summary>
    /// <remarks>Many units share the name 'mile'. This denotes the international mile, used in the imperial and the US customary systems.</remarks>
    public static UnitOfLength Mile { get; } = Yard.ScaledBy(1760);

    /// <summary>The <see cref="Quantities.Length"/> that the <see cref="UnitOfLength"/> represents.</summary>
    public Length Length { get; private init; }

    /// <summary>Constructs a new <see cref="UnitOfLength"/>, representing the <see cref="Quantities.Length"/> <paramref name="length"/>.</summary>
    /// <param name="length">The <see cref="Quantities.Length"/> that the new <see cref="UnitOfLength"/> represents.</param>
    private UnitOfLength(Length length)
    {
        Length = length;
    }

    /// <summary>Derives a new <see cref="UnitOfLength"/> from this instance, by prefixing the <see cref="MetricPrefix"/> <paramref name="prefix"/>.</summary>
    /// <param name="prefix">The <see cref="MetricPrefix"/> with which the new <see cref="UnitOfLength"/> is expressed.</param>
    /// <remarks>Any <see cref="MetricPrefix"/> applied to the original instance will be retained, essentially stacking the prefixes.</remarks>
    public UnitOfLength WithPrefix(MetricPrefix prefix) => new(Length * prefix.Scale);
    /// <summary>Derives a new <see cref="UnitOfLength"/> from this instance, through scaling by <paramref name="scale"/>.</summary>
    /// <param name="scale">The original <see cref="UnitOfLength"/> is scaled by this value.</param>
    public UnitOfLength ScaledBy(Scalar scale) => new(Length * scale);
    /// <summary>Derives a new <see cref="UnitOfLength"/> from this instance, through scaling by <paramref name="scale"/>.</summary>
    /// <param name="scale">The original <see cref="UnitOfLength"/> is scaled by this value.</param>
    public UnitOfLength ScaledBy(double scale) => new(Length * scale);

    /// <inheritdoc/>
    public int CompareTo(UnitOfLength other) => Length.CompareTo(other.Length);
    /// <summary>Produces a formatted string constisting of the type followed by the represented <see cref="Quantities.Length"/>.</summary>
    public override string ToString() => $"{GetType()}: {Length}";

    /// <summary>Determines whether the <see cref="Quantities.Length"/> represented by <paramref name="x"/> is
    /// less than that of <paramref name="y"/>.</summary>
    /// <param name="x">The method determines whether the <see cref="Quantities.Length"/> represented by this <see cref="UnitOfLength"/> is
    /// less than that of <paramref name="y"/>.</param>
    /// <param name="y">The method determines whether the <see cref="Quantities.Length"/> represented by <paramref name="x"/> is
    /// less than that of this <see cref="UnitOfLength"/>.</param>
    public static bool operator <(UnitOfLength x, UnitOfLength y) => x.Length < y.Length;
    /// <summary>Determines whether the <see cref="Quantities.Length"/> represented by <paramref name="x"/> is
    /// greater than that of <paramref name="y"/>.</summary>
    /// <param name="x">The method determines whether the <see cref="Quantities.Length"/> represented by this <see cref="UnitOfLength"/> is
    /// greater than that of <paramref name="y"/>.</param>
    /// <param name="y">The method determines whether the <see cref="Quantities.Length"/> represented by <paramref name="x"/> is
    /// greater than that of this <see cref="UnitOfLength"/>.</param>
    public static bool operator >(UnitOfLength x, UnitOfLength y) => x.Length > y.Length;
    /// <summary>Determines whether the <see cref="Quantities.Length"/> represented by <paramref name="x"/> is
    /// less than or equal to that of <paramref name="y"/>.</summary>
    /// <param name="x">The method determines whether the <see cref="Quantities.Length"/> represented by this <see cref="UnitOfLength"/> is
    /// less than or equal to that of <paramref name="y"/>.</param>
    /// <param name="y">The method determines whether the <see cref="Quantities.Length"/> represented by <paramref name="x"/> is
    /// less than or equal to that of this <see cref="UnitOfLength"/>.</param>
    public static bool operator <=(UnitOfLength x, UnitOfLength y) => x.Length <= y.Length;
    /// <summary>Determines whether the <see cref="Quantities.Length"/> represented by <paramref name="x"/> is
    /// greater than or equal to that of <paramref name="y"/>.</summary>
    /// <param name="x">The method determines whether the <see cref="Quantities.Length"/> represented by this <see cref="UnitOfLength"/> is
    /// greater than or equal to that of <paramref name="y"/>.</param>
    /// <param name="y">The method determines whether the <see cref="Quantities.Length"/> represented by <paramref name="x"/> is
    /// greater than or equal to that of this <see cref="UnitOfLength"/>.</param>
    public static bool operator >=(UnitOfLength x, UnitOfLength y) => x.Length >= y.Length;
}
