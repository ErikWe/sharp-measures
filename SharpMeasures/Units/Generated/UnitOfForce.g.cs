#nullable enable

namespace ErikWe.SharpMeasures.Units;

using ErikWe.SharpMeasures.Quantities;

using System;

/// <summary>Describes a unit of the quantity <see cref="Quantities.Force"/>, and related quantities.</summary>
/// <remarks>Common <see cref="UnitOfForce"/> exists as static properties, and from these custom <see cref="UnitOfForce"/> may be derived using
/// the instance-methods <see cref="WithPrefix(MetricPrefix)"/> or <see cref="ScaledBy(Scalar)"/>. Custom <see cref="UnitOfForce"/> can also be derived from
/// other units using the static <see cref="From(UnitOfMass, UnitOfAcceleration)"/>.</remarks>
public readonly record struct UnitOfForce :
    IComparable<UnitOfForce>
{
    /// <summary>Derives a <see cref="UnitOfForce"/> according to { <paramref name="unitOfMass"/> ∙ <paramref name="unitOfAcceleration"/> }.</summary>
    /// <param name="unitOfMass">A <see cref="UnitOfForce"/> is derived from multiplication of this <see cref="UnitOfMass"/> by <paramref name="unitOfAcceleration"/>.</param>
    /// <param name="unitOfAcceleration">A <see cref="UnitOfForce"/> is derived from multiplication of this <see cref="UnitOfAcceleration"/> by <paramref name="unitOfMass"/>.</param>
    public static UnitOfForce From(UnitOfMass unitOfMass, UnitOfAcceleration unitOfAcceleration) => new(Force.From(unitOfMass.Mass, unitOfAcceleration.Acceleration));

    /// <summary>The SI unit of <see cref="Quantities.Force"/>, derived according to { <see cref="UnitOfMass.Kilogram"/>
    /// ∙ <see cref="UnitOfAcceleration.MetrePerSecondSquared"/> }. Usually written as [N].</summary>
    public static UnitOfForce Newton { get; } = From(UnitOfMass.Kilogram, UnitOfAcceleration.MetrePerSecondSquared);
    /// <summary>Expresses <see cref="Quantities.Force"/> according to { <see cref="UnitOfMass.Pound"/> ∙ <see cref="UnitOfAcceleration.StandardGravity"/> }.
    /// Usually written as [lbf].</summary>
    public static UnitOfForce PoundForce { get; } = From(UnitOfMass.Pound, UnitOfAcceleration.StandardGravity);

    /// <summary>The <see cref="Quantities.Force"/> that the <see cref="UnitOfForce"/> represents.</summary>
    public Force Force { get; private init; }

    /// <summary>Constructs a new <see cref="UnitOfForce"/>, representing the <see cref="Quantities.Force"/> <paramref name="force"/>.</summary>
    /// <param name="force">The <see cref="Quantities.Force"/> that the new <see cref="UnitOfForce"/> represents.</param>
    private UnitOfForce(Force force)
    {
        Force = force;
    }

    /// <summary>Derives a new <see cref="UnitOfForce"/> from this instance, by prefixing the <see cref="MetricPrefix"/> <paramref name="prefix"/>.</summary>
    /// <param name="prefix">The <see cref="MetricPrefix"/> with which the new <see cref="UnitOfForce"/> is expressed.</param>
    /// <remarks>Any <see cref="MetricPrefix"/> applied to the original instance will be retained, essentially stacking the prefixes.</remarks>
    public UnitOfForce WithPrefix(MetricPrefix prefix) => new(Force * prefix.Scale);
    /// <summary>Derives a new <see cref="UnitOfForce"/> from this instance, through scaling by <paramref name="scale"/>.</summary>
    /// <param name="scale">The original <see cref="UnitOfForce"/> is scaled by this value.</param>
    public UnitOfForce ScaledBy(Scalar scale) => new(Force * scale);
    /// <summary>Derives a new <see cref="UnitOfForce"/> from this instance, through scaling by <paramref name="scale"/>.</summary>
    /// <param name="scale">The original <see cref="UnitOfForce"/> is scaled by this value.</param>
    public UnitOfForce ScaledBy(double scale) => new(Force * scale);

    /// <inheritdoc/>
    public int CompareTo(UnitOfForce other) => Force.CompareTo(other.Force);
    /// <summary>Produces a formatted string constisting of the type followed by the represented <see cref="Quantities.Force"/>.</summary>
    public override string ToString() => $"{GetType()}: {Force}";

    /// <summary>Determines whether the <see cref="Quantities.Force"/> represented by <paramref name="x"/> is
    /// less than that of <paramref name="y"/>.</summary>
    /// <param name="x">The method determines whether the <see cref="Quantities.Force"/> represented by this <see cref="UnitOfForce"/> is
    /// less than that of <paramref name="y"/>.</param>
    /// <param name="y">The method determines whether the <see cref="Quantities.Force"/> represented by <paramref name="x"/> is
    /// less than that of this <see cref="UnitOfForce"/>.</param>
    public static bool operator <(UnitOfForce x, UnitOfForce y) => x.Force < y.Force;
    /// <summary>Determines whether the <see cref="Quantities.Force"/> represented by <paramref name="x"/> is
    /// greater than that of <paramref name="y"/>.</summary>
    /// <param name="x">The method determines whether the <see cref="Quantities.Force"/> represented by this <see cref="UnitOfForce"/> is
    /// greater than that of <paramref name="y"/>.</param>
    /// <param name="y">The method determines whether the <see cref="Quantities.Force"/> represented by <paramref name="x"/> is
    /// greater than that of this <see cref="UnitOfForce"/>.</param>
    public static bool operator >(UnitOfForce x, UnitOfForce y) => x.Force > y.Force;
    /// <summary>Determines whether the <see cref="Quantities.Force"/> represented by <paramref name="x"/> is
    /// less than or equal to that of <paramref name="y"/>.</summary>
    /// <param name="x">The method determines whether the <see cref="Quantities.Force"/> represented by this <see cref="UnitOfForce"/> is
    /// less than or equal to that of <paramref name="y"/>.</param>
    /// <param name="y">The method determines whether the <see cref="Quantities.Force"/> represented by <paramref name="x"/> is
    /// less than or equal to that of this <see cref="UnitOfForce"/>.</param>
    public static bool operator <=(UnitOfForce x, UnitOfForce y) => x.Force <= y.Force;
    /// <summary>Determines whether the <see cref="Quantities.Force"/> represented by <paramref name="x"/> is
    /// greater than or equal to that of <paramref name="y"/>.</summary>
    /// <param name="x">The method determines whether the <see cref="Quantities.Force"/> represented by this <see cref="UnitOfForce"/> is
    /// greater than or equal to that of <paramref name="y"/>.</param>
    /// <param name="y">The method determines whether the <see cref="Quantities.Force"/> represented by <paramref name="x"/> is
    /// greater than or equal to that of this <see cref="UnitOfForce"/>.</param>
    public static bool operator >=(UnitOfForce x, UnitOfForce y) => x.Force >= y.Force;
}
