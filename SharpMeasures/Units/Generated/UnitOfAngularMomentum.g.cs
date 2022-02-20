#nullable enable

namespace ErikWe.SharpMeasures.Units;

using ErikWe.SharpMeasures.Quantities;

using System;

/// <summary>Describes a unit of the quantity <see cref="Quantities.AngularMomentum"/>, and related quantities.</summary>
/// <remarks>Common <see cref="UnitOfAngularMomentum"/> exists as static properties, and from these custom <see cref="UnitOfAngularMomentum"/> may be derived using
/// the instance-methods <see cref="WithPrefix(MetricPrefix)"/> or <see cref="ScaledBy(Scalar)"/>. Custom <see cref="UnitOfAngularMomentum"/> can also be derived from
/// other units using the static <see cref="From(UnitOfMomentOfInertia, UnitOfAngularVelocity)"/>.</remarks>
public readonly record struct UnitOfAngularMomentum :
    IComparable<UnitOfAngularMomentum>
{
    /// <summary>Derives a <see cref="UnitOfAngularMomentum"/> according to { <paramref name="unitOfMomentOfInertia"/> ∙ <paramref name="unitOfAngularVelocity"/> }.</summary>
    /// <param name="unitOfMomentOfInertia">A <see cref="UnitOfAngularMomentum"/> is derived from multiplication of this
    /// <see cref="UnitOfMomentOfInertia"/> by <paramref name="unitOfAngularVelocity"/>.</param>
    /// <param name="unitOfAngularVelocity">A <see cref="UnitOfAngularMomentum"/> is derived from multiplication of the <see cref="UnitOfAngularVelocity"/>
    /// by <paramref name="unitOfMomentOfInertia"/>.</param>
    public static UnitOfAngularMomentum From(UnitOfMomentOfInertia unitOfMomentOfInertia, UnitOfAngularVelocity unitOfAngularVelocity) 
    	=> new(AngularMomentum.From(unitOfMomentOfInertia.MomentOfInertia, unitOfAngularVelocity.AngularSpeed));

    /// <summary>The SI unit of <see cref="Quantities.AngularMomentum"/>, derived according to {
    /// <see cref="UnitOfMomentOfInertia.KilogramSquareMetre"/> ∙ <see cref="UnitOfAngularVelocity.RadianPerSecond"/> }. Usually written as [kg∙m²/s] or [kg∙m²∙s⁻¹].</summary>
    public static UnitOfAngularMomentum KilogramSquareMetrePerSecond { get; } = From(UnitOfMomentOfInertia.KilogramSquareMetre, UnitOfAngularVelocity.RadianPerSecond);

    /// <summary>The <see cref="Quantities.AngularMomentum"/> that the <see cref="UnitOfAngularMomentum"/> represents.</summary>
    public AngularMomentum AngularMomentum { get; private init; }

    /// <summary>Constructs a new <see cref="UnitOfAngularMomentum"/>, representing the <see cref="Quantities.AngularMomentum"/> <paramref name="angularMomentum"/>.</summary>
    /// <param name="angularMomentum">The <see cref="Quantities.AngularMomentum"/> that the new <see cref="UnitOfAngularMomentum"/> represents.</param>
    private UnitOfAngularMomentum(AngularMomentum angularMomentum)
    {
        AngularMomentum = angularMomentum;
    }

    /// <summary>Derives a new <see cref="UnitOfAngularMomentum"/> from this instance, by prefixing the <see cref="MetricPrefix"/> <paramref name="prefix"/>.</summary>
    /// <param name="prefix">The <see cref="MetricPrefix"/> with which the new <see cref="UnitOfAngularMomentum"/> is expressed.</param>
    /// <remarks>Any <see cref="MetricPrefix"/> applied to the original instance will be retained, essentially stacking the prefixes.</remarks>
    public UnitOfAngularMomentum WithPrefix(MetricPrefix prefix) => new(AngularMomentum * prefix.Scale);
    /// <summary>Derives a new <see cref="UnitOfAngularMomentum"/> from this instance, through scaling by <paramref name="scale"/>.</summary>
    /// <param name="scale">The original <see cref="UnitOfAngularMomentum"/> is scaled by this value.</param>
    public UnitOfAngularMomentum ScaledBy(Scalar scale) => new(AngularMomentum * scale);
    /// <summary>Derives a new <see cref="UnitOfAngularMomentum"/> from this instance, through scaling by <paramref name="scale"/>.</summary>
    /// <param name="scale">The original <see cref="UnitOfAngularMomentum"/> is scaled by this value.</param>
    public UnitOfAngularMomentum ScaledBy(double scale) => new(AngularMomentum * scale);

    /// <inheritdoc/>
    public int CompareTo(UnitOfAngularMomentum other) => AngularMomentum.CompareTo(other.AngularMomentum);
    /// <summary>Produces a formatted string constisting of the type followed by the represented <see cref="Quantities.AngularMomentum"/>.</summary>
    public override string ToString() => $"{GetType()}: {AngularMomentum}";

    /// <summary>Determines whether the <see cref="Quantities.AngularMomentum"/> represented by <paramref name="x"/> is
    /// less than that of <paramref name="y"/>.</summary>
    /// <param name="x">The method determines whether the <see cref="Quantities.AngularMomentum"/> represented by this <see cref="UnitOfAngularMomentum"/> is
    /// less than that of <paramref name="y"/>.</param>
    /// <param name="y">The method determines whether the <see cref="Quantities.AngularMomentum"/> represented by <paramref name="x"/> is
    /// less than that of this <see cref="UnitOfAngularMomentum"/>.</param>
    public static bool operator <(UnitOfAngularMomentum x, UnitOfAngularMomentum y) => x.AngularMomentum < y.AngularMomentum;
    /// <summary>Determines whether the <see cref="Quantities.AngularMomentum"/> represented by <paramref name="x"/> is
    /// greater than that of <paramref name="y"/>.</summary>
    /// <param name="x">The method determines whether the <see cref="Quantities.AngularMomentum"/> represented by this <see cref="UnitOfAngularMomentum"/> is
    /// greater than that of <paramref name="y"/>.</param>
    /// <param name="y">The method determines whether the <see cref="Quantities.AngularMomentum"/> represented by <paramref name="x"/> is
    /// greater than that of this <see cref="UnitOfAngularMomentum"/>.</param>
    public static bool operator >(UnitOfAngularMomentum x, UnitOfAngularMomentum y) => x.AngularMomentum > y.AngularMomentum;
    /// <summary>Determines whether the <see cref="Quantities.AngularMomentum"/> represented by <paramref name="x"/> is
    /// less than or equal to that of <paramref name="y"/>.</summary>
    /// <param name="x">The method determines whether the <see cref="Quantities.AngularMomentum"/> represented by this <see cref="UnitOfAngularMomentum"/> is
    /// less than or equal to that of <paramref name="y"/>.</param>
    /// <param name="y">The method determines whether the <see cref="Quantities.AngularMomentum"/> represented by <paramref name="x"/> is
    /// less than or equal to that of this <see cref="UnitOfAngularMomentum"/>.</param>
    public static bool operator <=(UnitOfAngularMomentum x, UnitOfAngularMomentum y) => x.AngularMomentum <= y.AngularMomentum;
    /// <summary>Determines whether the <see cref="Quantities.AngularMomentum"/> represented by <paramref name="x"/> is
    /// greater than or equal to that of <paramref name="y"/>.</summary>
    /// <param name="x">The method determines whether the <see cref="Quantities.AngularMomentum"/> represented by this <see cref="UnitOfAngularMomentum"/> is
    /// greater than or equal to that of <paramref name="y"/>.</param>
    /// <param name="y">The method determines whether the <see cref="Quantities.AngularMomentum"/> represented by <paramref name="x"/> is
    /// greater than or equal to that of this <see cref="UnitOfAngularMomentum"/>.</param>
    public static bool operator >=(UnitOfAngularMomentum x, UnitOfAngularMomentum y) => x.AngularMomentum >= y.AngularMomentum;
}
