namespace ErikWe.SharpMeasures.Units;

using ErikWe.SharpMeasures.Quantities;

using System;

/// <summary>Describes a unit of the quantity <see cref="Quantities.Mass"/>.</summary>
/// <remarks>Common <see cref="UnitOfMass"/> exists as static properties, and from these custom <see cref="UnitOfMass"/> may be derived using
/// the instance-methods <see cref="WithPrefix(MetricPrefix)"/> or <see cref="ScaledBy(Scalar)"/>.</remarks>
public readonly record struct UnitOfMass :
    IComparable<UnitOfMass>
{
    /// <summary>Expresses <see cref="Quantities.Mass"/> as one thousandth of a <see cref="Kilogram"/>. Usually written as [g].</summary>
    public static UnitOfMass Gram { get; } = new(new Mass(1));
    /// <summary>Expresses <see cref="Quantities.Mass"/> as one thousandth of a <see cref="Gram"/>. Usually written as [mg].</summary>
    public static UnitOfMass Milligram { get; } = Gram.WithPrefix(MetricPrefix.Milli);
    /// <summary>Expresses <see cref="Quantities.Mass"/> as one hundred <see cref="Gram"/>. Usually written as [hg].</summary>
    public static UnitOfMass Hectogram { get; } = Gram.WithPrefix(MetricPrefix.Hecto);
    /// <summary>The SI unit of <see cref="Quantities.Mass"/>, equal to one thousand <see cref="Gram"/>. Usually written as [kg].</summary>
    public static UnitOfMass Kilogram { get; } = Gram.WithPrefix(MetricPrefix.Kilo);
    /// <summary>Expresses <see cref="Quantities.Mass"/> as one thousand <see cref="Kilogram"/>. Also known as the metric ton, and usually written as [t].</summary>
    /// <remarks>Not to be confused with other units of the name 'ton'.</remarks>
    public static UnitOfMass Tonne { get; } = Gram.WithPrefix(MetricPrefix.Mega);

    /// <summary>Expresses <see cref="Quantities.Mass"/> according to { 1/16 ∙ <see cref="Pound"/> }. Usually written [oz].</summary>
    /// <remarks>Many units share the name 'ounce'. This denotes the avoirdupois ounce, used in the imperial and the US customary systems.</remarks>
    public static UnitOfMass Ounce { get; } = Gram.ScaledBy(28.349523125);
    /// <summary>Expresses <see cref="Quantities.Mass"/> according to { 16 ∙ <see cref="Ounce"/> }. Usually written [lb].</summary>
    /// <remarks>Many units share the name 'pound'. This denotes the avoirdupois pound, used in the imperial and the US customary systems.</remarks>
    public static UnitOfMass Pound { get; } = Ounce.ScaledBy(16);

    /// <summary>The <see cref="Quantities.Mass"/> that the <see cref="UnitOfMass"/> represents.</summary>
    public Mass Mass { get; private init; }

    /// <summary>Constructs a new <see cref="UnitOfMass"/>, representing the <see cref="Quantities.Mass"/> <paramref name="mass"/>.</summary>
    /// <param name="mass">The <see cref="Quantities.Mass"/> that the new <see cref="UnitOfMass"/> represents.</param>
    private UnitOfMass(Mass mass)
    {
        Mass = mass;
    }

    /// <summary>Derives a new <see cref="UnitOfMass"/> from this instance, by prefixing the <see cref="MetricPrefix"/> <paramref name="prefix"/>.</summary>
    /// <param name="prefix">The <see cref="MetricPrefix"/> with which the new <see cref="UnitOfMass"/> is expressed.</param>
    /// <remarks>Any <see cref="MetricPrefix"/> applied to the original instance will be retained, essentially stacking the prefixes.</remarks>
    public UnitOfMass WithPrefix(MetricPrefix prefix) => new(Mass * prefix.Scale);
    /// <summary>Derives a new <see cref="UnitOfMass"/> from this instance, through scaling by <paramref name="scale"/>.</summary>
    /// <param name="scale">The original <see cref="UnitOfMass"/> is scaled by this value.</param>
    public UnitOfMass ScaledBy(Scalar scale) => new(Mass * scale);
    /// <summary>Derives a new <see cref="UnitOfMass"/> from this instance, through scaling by <paramref name="scale"/>.</summary>
    /// <param name="scale">The original <see cref="UnitOfMass"/> is scaled by this value.</param>
    public UnitOfMass ScaledBy(double scale) => new(Mass * scale);

    /// <inheritdoc/>
    public int CompareTo(UnitOfMass other) => Mass.CompareTo(other.Mass);
    /// <summary>Produces a formatted string constisting of the type followed by the represented <see cref="Quantities.Mass"/>.</summary>
    public override string ToString() => $"{GetType()}: {Mass}";

    /// <summary>Determines whether the <see cref="Quantities.Mass"/> represented by <paramref name="x"/> is
    /// less than that of <paramref name="y"/>.</summary>
    /// <param name="x">The method determines whether the <see cref="Quantities.Mass"/> represented by this <see cref="UnitOfMass"/> is
    /// less than that of <paramref name="y"/>.</param>
    /// <param name="y">The method determines whether the <see cref="Quantities.Mass"/> represented by <paramref name="x"/> is
    /// less than that of this <see cref="UnitOfMass"/>.</param>
    public static bool operator <(UnitOfMass x, UnitOfMass y) => x.Mass < y.Mass;
    /// <summary>Determines whether the <see cref="Quantities.Mass"/> represented by <paramref name="x"/> is
    /// greater than that of <paramref name="y"/>.</summary>
    /// <param name="x">The method determines whether the <see cref="Quantities.Mass"/> represented by this <see cref="UnitOfMass"/> is
    /// greater than that of <paramref name="y"/>.</param>
    /// <param name="y">The method determines whether the <see cref="Quantities.Mass"/> represented by <paramref name="x"/> is
    /// greater than that of this <see cref="UnitOfMass"/>.</param>
    public static bool operator >(UnitOfMass x, UnitOfMass y) => x.Mass > y.Mass;
    /// <summary>Determines whether the <see cref="Quantities.Mass"/> represented by <paramref name="x"/> is
    /// less than or equal to that of <paramref name="y"/>.</summary>
    /// <param name="x">The method determines whether the <see cref="Quantities.Mass"/> represented by this <see cref="UnitOfMass"/> is
    /// less than or equal to that of <paramref name="y"/>.</param>
    /// <param name="y">The method determines whether the <see cref="Quantities.Mass"/> represented by <paramref name="x"/> is
    /// less than or equal to that of this <see cref="UnitOfMass"/>.</param>
    public static bool operator <=(UnitOfMass x, UnitOfMass y) => x.Mass <= y.Mass;
    /// <summary>Determines whether the <see cref="Quantities.Mass"/> represented by <paramref name="x"/> is
    /// greater than or equal to that of <paramref name="y"/>.</summary>
    /// <param name="x">The method determines whether the <see cref="Quantities.Mass"/> represented by this <see cref="UnitOfMass"/> is
    /// greater than or equal to that of <paramref name="y"/>.</param>
    /// <param name="y">The method determines whether the <see cref="Quantities.Mass"/> represented by <paramref name="x"/> is
    /// greater than or equal to that of this <see cref="UnitOfMass"/>.</param>
    public static bool operator >=(UnitOfMass x, UnitOfMass y) => x.Mass >= y.Mass;
}
