namespace ErikWe.SharpMeasures.Units;

using ErikWe.SharpMeasures.Quantities;

using System;

/// <summary>Describes a unit of the quantity <see cref="Quantities.SpecificAngularMomentum"/>, and related quantities.</summary>
/// <remarks>Common <see cref="UnitOfSpecificAngularMomentum"/> exists as static properties, and from these custom <see cref="UnitOfSpecificAngularMomentum"/> may be derived using
/// the instance-methods <see cref="WithPrefix(MetricPrefix)"/> or <see cref="ScaledBy(Scalar)"/>. Custom <see cref="UnitOfSpecificAngularMomentum"/> can also be derived from
/// other units using the static <see cref="From(UnitOfAngularMomentum, UnitOfMass)"/>.</remarks>
public readonly record struct UnitOfSpecificAngularMomentum :
    IComparable<UnitOfSpecificAngularMomentum>
{
    /// <summary>Derives a <see cref="UnitOfSpecificAngularMomentum"/> according to { <paramref name="unitOfAngularMomentum"/> / <paramref name="unitOfMass"/> }.</summary>
    /// <param name="unitOfAngularMomentum">A <see cref="UnitOfSpecificAngularMomentum"/> is derived from division of this <see cref="UnitOfAngularMomentum"/>
    /// by <paramref name="unitOfMass"/>.</param>
    /// <param name="unitOfMass">A <see cref="UnitOfSpecificAngularMomentum"/> is derived from division of <paramref name="unitOfAngularMomentum"/> by this <see cref="UnitOfMass"/>.</param>
    public static UnitOfSpecificAngularMomentum From(UnitOfAngularMomentum unitOfAngularMomentum, UnitOfMass unitOfMass) 
    	=> new(SpecificAngularMomentum.From(unitOfAngularMomentum.AngularMomentum, unitOfMass.Mass));

    /// <summary>The SI unit of <see cref="Quantities.SpecificAngularMomentum"/>, derived according to {
    /// <see cref="UnitOfAngularMomentum.KilogramSquareMetrePerSecond"/> / <see cref="UnitOfMass.Kilogram"/> }. Usually written as [m²/s] or [m²∙s⁻¹].</summary>
    public static UnitOfSpecificAngularMomentum SquareMetrePerSecond { get; } = From(UnitOfAngularMomentum.KilogramSquareMetrePerSecond, UnitOfMass.Kilogram);

    /// <summary>The <see cref="Quantities.SpecificAngularMomentum"/> that the <see cref="UnitOfSpecificAngularMomentum"/> represents.</summary>
    public SpecificAngularMomentum SpecificAngularMomentum { get; private init; }

    /// <summary>Constructs a new <see cref="UnitOfSpecificAngularMomentum"/>, representing the <see cref="Quantities.SpecificAngularMomentum"/> <paramref name="specificAngularMomentum"/>.</summary>
    /// <param name="specificAngularMomentum">The <see cref="Quantities.SpecificAngularMomentum"/> that the new <see cref="UnitOfSpecificAngularMomentum"/> represents.</param>
    private UnitOfSpecificAngularMomentum(SpecificAngularMomentum specificAngularMomentum)
    {
        SpecificAngularMomentum = specificAngularMomentum;
    }

    /// <summary>Derives a new <see cref="UnitOfSpecificAngularMomentum"/> from this instance, by prefixing the <see cref="MetricPrefix"/> <paramref name="prefix"/>.</summary>
    /// <param name="prefix">The <see cref="MetricPrefix"/> with which the new <see cref="UnitOfSpecificAngularMomentum"/> is expressed.</param>
    /// <remarks>Any <see cref="MetricPrefix"/> applied to the original instance will be retained, essentially stacking the prefixes.</remarks>
    public UnitOfSpecificAngularMomentum WithPrefix(MetricPrefix prefix) => new(SpecificAngularMomentum * prefix.Scale);
    /// <summary>Derives a new <see cref="UnitOfSpecificAngularMomentum"/> from this instance, through scaling by <paramref name="scale"/>.</summary>
    /// <param name="scale">The original <see cref="UnitOfSpecificAngularMomentum"/> is scaled by this value.</param>
    public UnitOfSpecificAngularMomentum ScaledBy(Scalar scale) => new(SpecificAngularMomentum * scale);
    /// <summary>Derives a new <see cref="UnitOfSpecificAngularMomentum"/> from this instance, through scaling by <paramref name="scale"/>.</summary>
    /// <param name="scale">The original <see cref="UnitOfSpecificAngularMomentum"/> is scaled by this value.</param>
    public UnitOfSpecificAngularMomentum ScaledBy(double scale) => new(SpecificAngularMomentum * scale);

    /// <inheritdoc/>
    public int CompareTo(UnitOfSpecificAngularMomentum other) => SpecificAngularMomentum.CompareTo(other.SpecificAngularMomentum);
    /// <summary>Produces a formatted string constisting of the type followed by the represented <see cref="Quantities.SpecificAngularMomentum"/>.</summary>
    public override string ToString() => $"{GetType()}: {SpecificAngularMomentum}";

    /// <summary>Determines whether the <see cref="Quantities.SpecificAngularMomentum"/> represented by <paramref name="x"/> is
    /// less than that of <paramref name="y"/>.</summary>
    /// <param name="x">The method determines whether the <see cref="Quantities.SpecificAngularMomentum"/> represented by this <see cref="UnitOfSpecificAngularMomentum"/> is
    /// less than that of <paramref name="y"/>.</param>
    /// <param name="y">The method determines whether the <see cref="Quantities.SpecificAngularMomentum"/> represented by <paramref name="x"/> is
    /// less than that of this <see cref="UnitOfSpecificAngularMomentum"/>.</param>
    public static bool operator <(UnitOfSpecificAngularMomentum x, UnitOfSpecificAngularMomentum y) => x.SpecificAngularMomentum < y.SpecificAngularMomentum;
    /// <summary>Determines whether the <see cref="Quantities.SpecificAngularMomentum"/> represented by <paramref name="x"/> is
    /// greater than that of <paramref name="y"/>.</summary>
    /// <param name="x">The method determines whether the <see cref="Quantities.SpecificAngularMomentum"/> represented by this <see cref="UnitOfSpecificAngularMomentum"/> is
    /// greater than that of <paramref name="y"/>.</param>
    /// <param name="y">The method determines whether the <see cref="Quantities.SpecificAngularMomentum"/> represented by <paramref name="x"/> is
    /// greater than that of this <see cref="UnitOfSpecificAngularMomentum"/>.</param>
    public static bool operator >(UnitOfSpecificAngularMomentum x, UnitOfSpecificAngularMomentum y) => x.SpecificAngularMomentum > y.SpecificAngularMomentum;
    /// <summary>Determines whether the <see cref="Quantities.SpecificAngularMomentum"/> represented by <paramref name="x"/> is
    /// less than or equal to that of <paramref name="y"/>.</summary>
    /// <param name="x">The method determines whether the <see cref="Quantities.SpecificAngularMomentum"/> represented by this <see cref="UnitOfSpecificAngularMomentum"/> is
    /// less than or equal to that of <paramref name="y"/>.</param>
    /// <param name="y">The method determines whether the <see cref="Quantities.SpecificAngularMomentum"/> represented by <paramref name="x"/> is
    /// less than or equal to that of this <see cref="UnitOfSpecificAngularMomentum"/>.</param>
    public static bool operator <=(UnitOfSpecificAngularMomentum x, UnitOfSpecificAngularMomentum y) => x.SpecificAngularMomentum <= y.SpecificAngularMomentum;
    /// <summary>Determines whether the <see cref="Quantities.SpecificAngularMomentum"/> represented by <paramref name="x"/> is
    /// greater than or equal to that of <paramref name="y"/>.</summary>
    /// <param name="x">The method determines whether the <see cref="Quantities.SpecificAngularMomentum"/> represented by this <see cref="UnitOfSpecificAngularMomentum"/> is
    /// greater than or equal to that of <paramref name="y"/>.</param>
    /// <param name="y">The method determines whether the <see cref="Quantities.SpecificAngularMomentum"/> represented by <paramref name="x"/> is
    /// greater than or equal to that of this <see cref="UnitOfSpecificAngularMomentum"/>.</param>
    public static bool operator >=(UnitOfSpecificAngularMomentum x, UnitOfSpecificAngularMomentum y) => x.SpecificAngularMomentum >= y.SpecificAngularMomentum;
}
