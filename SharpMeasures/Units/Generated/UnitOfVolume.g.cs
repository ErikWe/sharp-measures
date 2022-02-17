namespace ErikWe.SharpMeasures.Units;

using ErikWe.SharpMeasures.Quantities;

using System;

/// <summary>Describes a unit of the quantity <see cref="Quantities.Volume"/>.</summary>
/// <remarks>Common <see cref="UnitOfVolume"/> exists as static properties, and from these custom <see cref="UnitOfVolume"/> may be derived using
/// the instance-methods <see cref="WithPrefix(MetricPrefix)"/> or <see cref="ScaledBy(Scalar)"/>. Custom <see cref="UnitOfVolume"/> can also be derived from
/// other units using the static <see cref="From(UnitOfLength)"/>, or an overload.</remarks>
public readonly record struct UnitOfVolume :
    IComparable<UnitOfVolume>
{
    /// <summary>Derives a <see cref="UnitOfVolume"/> according to { <paramref name="unitOfLength"/>³ }.</summary>
    /// <param name="unitOfLength">A <see cref="UnitOfVolume"/> is derived from cubing this <see cref="UnitOfLength"/>.</param>
    public static UnitOfVolume From(UnitOfLength unitOfLength) => new(Volume.From(unitOfLength.Length));
    /// <summary>Derives a <see cref="UnitOfVolume"/> according to { <paramref name="unitOfLength1"/> ∙ <paramref name="unitOfLength2"/>
    /// ∙ <paramref name="unitOfLength3"/> }.</summary>
    /// <param name="unitOfLength1">A <see cref="UnitOfVolume"/> is derived from multiplication of this <see cref="UnitOfLength"/> by
    /// <paramref name="unitOfLength2"/> and <paramref name="unitOfLength3"/>.</param>
    /// <param name="unitOfLength2">A <see cref="UnitOfVolume"/> is derived from multiplication of this <see cref="UnitOfLength"/> by
    /// <paramref name="unitOfLength1"/> and <paramref name="unitOfLength3"/>.</param>
    /// <param name="unitOfLength3">A <see cref="UnitOfVolume"/> is derived from multiplication of this <see cref="UnitOfLength"/> by
    /// <paramref name="unitOfLength1"/> and <paramref name="unitOfLength2"/>.</param>
    public static UnitOfVolume From(UnitOfLength unitOfLength1, UnitOfLength unitOfLength2, UnitOfLength unitOfLength3) 
    	=> new(Volume.From(unitOfLength1.Length, unitOfLength2.Length, unitOfLength3.Length));
    /// <summary>Derives a <see cref="UnitOfVolume"/> according to { <paramref name="unitOfArea"/> ∙ <paramref name="unitOfLength"/> }.</summary>
    /// <param name="unitOfArea">A <see cref="UnitOfVolume"/> is derived from multiplication of this <see cref="UnitOfArea"/> by <paramref name="unitOfLength"/>.</param>
    /// <param name="unitOfLength">A <see cref="UnitOfVolume"/> is derived from multiplication of this <see cref="UnitOfLength"/> by <paramref name="unitOfArea"/>.</param>
    public static UnitOfVolume From(UnitOfArea unitOfArea, UnitOfLength unitOfLength) => new(Volume.From(unitOfArea.Area, unitOfLength.Length));

    /// <summary>The SI unit of <see cref="Quantities.Volume"/>, derived according to { <see cref="UnitOfLength.Metre"/>³ }. Usually written as [m³].</summary>
    public static UnitOfVolume CubicMetre { get; } = From(UnitOfLength.Metre);
    /// <summary>Expresses <see cref="Quantities.Volume"/> according to { <see cref="UnitOfLength.Decimetre"/>³ }. Usually written as [dm³].</summary>
    /// <remarks>This is equivalent to <see cref="Litre"/>.</remarks>
    public static UnitOfVolume CubicDecimetre { get; } = From(UnitOfLength.Decimetre);

    /// <summary>Expresses <see cref="Quantities.Volume"/> according to { <see cref="UnitOfLength.Decimetre"/>³ }. Usually written as [L] or [l].</summary>
    /// <remarks>This is equivalent to <see cref="CubicDecimetre"/>.</remarks>
    public static UnitOfVolume Litre { get; } = CubicDecimetre;
    /// <summary>Expresses <see cref="Quantities.Volume"/> as one thousandth of a <see cref="Litre"/>. Usually written as [mL] or [ml].</summary>
    public static UnitOfVolume Millilitre { get; } = Litre.WithPrefix(MetricPrefix.Milli);
    /// <summary>Expresses <see cref="Quantities.Volume"/> as one hundredth of a <see cref="Litre"/>. Usually written as [cL] or [cl].</summary>
    public static UnitOfVolume Centilitre { get; } = Litre.WithPrefix(MetricPrefix.Centi);
    /// <summary>Expresses <see cref="Quantities.Volume"/> as one tenth of a <see cref="Litre"/>. Usually written as [dL] or [dl].</summary>
    public static UnitOfVolume Decilitre { get; } = Litre.WithPrefix(MetricPrefix.Deci);

    /// <summary>The <see cref="Quantities.Volume"/> that the <see cref="UnitOfVolume"/> represents.</summary>
    public Volume Volume { get; private init; }

    /// <summary>Constructs a new <see cref="UnitOfVolume"/>, representing the <see cref="Quantities.Volume"/> <paramref name="volume"/>.</summary>
    /// <param name="volume">The <see cref="Quantities.Volume"/> that the new <see cref="UnitOfVolume"/> represents.</param>
    private UnitOfVolume(Volume volume)
    {
        Volume = volume;
    }

    /// <summary>Derives a new <see cref="UnitOfVolume"/> from this instance, by prefixing the <see cref="MetricPrefix"/> <paramref name="prefix"/>.</summary>
    /// <param name="prefix">The <see cref="MetricPrefix"/> with which the new <see cref="UnitOfVolume"/> is expressed.</param>
    /// <remarks>Any <see cref="MetricPrefix"/> applied to the original instance will be retained, essentially stacking the prefixes.</remarks>
    public UnitOfVolume WithPrefix(MetricPrefix prefix) => new(Volume * prefix.Scale);
    /// <summary>Derives a new <see cref="UnitOfVolume"/> from this instance, through scaling by <paramref name="scale"/>.</summary>
    /// <param name="scale">The original <see cref="UnitOfVolume"/> is scaled by this value.</param>
    public UnitOfVolume ScaledBy(Scalar scale) => new(Volume * scale);
    /// <summary>Derives a new <see cref="UnitOfVolume"/> from this instance, through scaling by <paramref name="scale"/>.</summary>
    /// <param name="scale">The original <see cref="UnitOfVolume"/> is scaled by this value.</param>
    public UnitOfVolume ScaledBy(double scale) => new(Volume * scale);

    /// <inheritdoc/>
    public int CompareTo(UnitOfVolume other) => Volume.CompareTo(other.Volume);
    /// <summary>Produces a formatted string constisting of the type followed by the represented <see cref="Quantities.Volume"/>.</summary>
    public override string ToString() => $"{GetType()}: {Volume}";

    /// <summary>Determines whether the <see cref="Quantities.Volume"/> represented by <paramref name="x"/> is
    /// less than that of <paramref name="y"/>.</summary>
    /// <param name="x">The method determines whether the <see cref="Quantities.Volume"/> represented by this <see cref="UnitOfVolume"/> is
    /// less than that of <paramref name="y"/>.</param>
    /// <param name="y">The method determines whether the <see cref="Quantities.Volume"/> represented by <paramref name="x"/> is
    /// less than that of this <see cref="UnitOfVolume"/>.</param>
    public static bool operator <(UnitOfVolume x, UnitOfVolume y) => x.Volume < y.Volume;
    /// <summary>Determines whether the <see cref="Quantities.Volume"/> represented by <paramref name="x"/> is
    /// greater than that of <paramref name="y"/>.</summary>
    /// <param name="x">The method determines whether the <see cref="Quantities.Volume"/> represented by this <see cref="UnitOfVolume"/> is
    /// greater than that of <paramref name="y"/>.</param>
    /// <param name="y">The method determines whether the <see cref="Quantities.Volume"/> represented by <paramref name="x"/> is
    /// greater than that of this <see cref="UnitOfVolume"/>.</param>
    public static bool operator >(UnitOfVolume x, UnitOfVolume y) => x.Volume > y.Volume;
    /// <summary>Determines whether the <see cref="Quantities.Volume"/> represented by <paramref name="x"/> is
    /// less than or equal to that of <paramref name="y"/>.</summary>
    /// <param name="x">The method determines whether the <see cref="Quantities.Volume"/> represented by this <see cref="UnitOfVolume"/> is
    /// less than or equal to that of <paramref name="y"/>.</param>
    /// <param name="y">The method determines whether the <see cref="Quantities.Volume"/> represented by <paramref name="x"/> is
    /// less than or equal to that of this <see cref="UnitOfVolume"/>.</param>
    public static bool operator <=(UnitOfVolume x, UnitOfVolume y) => x.Volume <= y.Volume;
    /// <summary>Determines whether the <see cref="Quantities.Volume"/> represented by <paramref name="x"/> is
    /// greater than or equal to that of <paramref name="y"/>.</summary>
    /// <param name="x">The method determines whether the <see cref="Quantities.Volume"/> represented by this <see cref="UnitOfVolume"/> is
    /// greater than or equal to that of <paramref name="y"/>.</param>
    /// <param name="y">The method determines whether the <see cref="Quantities.Volume"/> represented by <paramref name="x"/> is
    /// greater than or equal to that of this <see cref="UnitOfVolume"/>.</param>
    public static bool operator >=(UnitOfVolume x, UnitOfVolume y) => x.Volume >= y.Volume;
}
