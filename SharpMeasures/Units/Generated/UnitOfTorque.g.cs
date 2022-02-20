#nullable enable

namespace ErikWe.SharpMeasures.Units;

using ErikWe.SharpMeasures.Quantities;

using System;

/// <summary>Describes a unit of the quantity <see cref="Quantities.Torque"/>, or related quantities.</summary>
/// <remarks>Common <see cref="UnitOfTorque"/> exists as static properties, and from these custom <see cref="UnitOfTorque"/> may be derived using
/// the instance-methods <see cref="WithPrefix(MetricPrefix)"/> or <see cref="ScaledBy(Scalar)"/>. Custom <see cref="UnitOfTorque"/> can also be derived from
/// other units using the static <see cref="From(UnitOfLength, UnitOfForce)"/>.</remarks>
public readonly record struct UnitOfTorque :
    IComparable<UnitOfTorque>
{
    /// <summary>Derives a <see cref="UnitOfTorque"/> according to { <paramref name="unitOfLength"/> ∙ <paramref name="unitOfForce"/> }.</summary>
    /// <param name="unitOfLength">A <see cref="UnitOfTorque"/> is derived from multiplication of this <see cref="UnitOfLength"/> by <paramref name="unitOfForce"/>.</param>
    /// <param name="unitOfForce">A <see cref="UnitOfTorque"/> is derived from multiplication of this <see cref="UnitOfForce"/> by <paramref name="unitOfLength"/>.</param>
    public static UnitOfTorque From(UnitOfLength unitOfLength, UnitOfForce unitOfForce) 
    	=> new(Torque.From(unitOfLength.Length.AsDistance, unitOfForce.Force, Angle.OneQuarterTurn));

    /// <summary>The SI unit of <see cref="Quantities.Torque"/>, derived according to { <see cref="UnitOfLength.Metre"/> ∙ <see cref="UnitOfForce.Newton"/> }.
    /// Usually written as [N∙m].</summary>
    public static UnitOfTorque NewtonMetre { get; } = From(UnitOfLength.Metre, UnitOfForce.Newton);

    /// <summary>The <see cref="Quantities.Torque"/> that the <see cref="UnitOfTorque"/> represents.</summary>
    public Torque Torque { get; private init; }

    /// <summary>Constructs a new <see cref="UnitOfTorque"/>, representing the <see cref="Quantities.Torque"/> <paramref name="torque"/>.</summary>
    /// <param name="torque">The <see cref="Quantities.Torque"/> that the new <see cref="UnitOfTorque"/> represents.</param>
    private UnitOfTorque(Torque torque)
    {
        Torque = torque;
    }

    /// <summary>Derives a new <see cref="UnitOfTorque"/> from this instance, by prefixing the <see cref="MetricPrefix"/> <paramref name="prefix"/>.</summary>
    /// <param name="prefix">The <see cref="MetricPrefix"/> with which the new <see cref="UnitOfTorque"/> is expressed.</param>
    /// <remarks>Any <see cref="MetricPrefix"/> applied to the original instance will be retained, essentially stacking the prefixes.</remarks>
    public UnitOfTorque WithPrefix(MetricPrefix prefix) => new(Torque * prefix.Scale);
    /// <summary>Derives a new <see cref="UnitOfTorque"/> from this instance, through scaling by <paramref name="scale"/>.</summary>
    /// <param name="scale">The original <see cref="UnitOfTorque"/> is scaled by this value.</param>
    public UnitOfTorque ScaledBy(Scalar scale) => new(Torque * scale);
    /// <summary>Derives a new <see cref="UnitOfTorque"/> from this instance, through scaling by <paramref name="scale"/>.</summary>
    /// <param name="scale">The original <see cref="UnitOfTorque"/> is scaled by this value.</param>
    public UnitOfTorque ScaledBy(double scale) => new(Torque * scale);

    /// <inheritdoc/>
    public int CompareTo(UnitOfTorque other) => Torque.CompareTo(other.Torque);
    /// <summary>Produces a formatted string constisting of the type followed by the represented <see cref="Quantities.Torque"/>.</summary>
    public override string ToString() => $"{GetType()}: {Torque}";

    /// <summary>Determines whether the <see cref="Quantities.Torque"/> represented by <paramref name="x"/> is
    /// less than that of <paramref name="y"/>.</summary>
    /// <param name="x">The method determines whether the <see cref="Quantities.Torque"/> represented by this <see cref="UnitOfTorque"/> is
    /// less than that of <paramref name="y"/>.</param>
    /// <param name="y">The method determines whether the <see cref="Quantities.Torque"/> represented by <paramref name="x"/> is
    /// less than that of this <see cref="UnitOfTorque"/>.</param>
    public static bool operator <(UnitOfTorque x, UnitOfTorque y) => x.Torque < y.Torque;
    /// <summary>Determines whether the <see cref="Quantities.Torque"/> represented by <paramref name="x"/> is
    /// greater than that of <paramref name="y"/>.</summary>
    /// <param name="x">The method determines whether the <see cref="Quantities.Torque"/> represented by this <see cref="UnitOfTorque"/> is
    /// greater than that of <paramref name="y"/>.</param>
    /// <param name="y">The method determines whether the <see cref="Quantities.Torque"/> represented by <paramref name="x"/> is
    /// greater than that of this <see cref="UnitOfTorque"/>.</param>
    public static bool operator >(UnitOfTorque x, UnitOfTorque y) => x.Torque > y.Torque;
    /// <summary>Determines whether the <see cref="Quantities.Torque"/> represented by <paramref name="x"/> is
    /// less than or equal to that of <paramref name="y"/>.</summary>
    /// <param name="x">The method determines whether the <see cref="Quantities.Torque"/> represented by this <see cref="UnitOfTorque"/> is
    /// less than or equal to that of <paramref name="y"/>.</param>
    /// <param name="y">The method determines whether the <see cref="Quantities.Torque"/> represented by <paramref name="x"/> is
    /// less than or equal to that of this <see cref="UnitOfTorque"/>.</param>
    public static bool operator <=(UnitOfTorque x, UnitOfTorque y) => x.Torque <= y.Torque;
    /// <summary>Determines whether the <see cref="Quantities.Torque"/> represented by <paramref name="x"/> is
    /// greater than or equal to that of <paramref name="y"/>.</summary>
    /// <param name="x">The method determines whether the <see cref="Quantities.Torque"/> represented by this <see cref="UnitOfTorque"/> is
    /// greater than or equal to that of <paramref name="y"/>.</param>
    /// <param name="y">The method determines whether the <see cref="Quantities.Torque"/> represented by <paramref name="x"/> is
    /// greater than or equal to that of this <see cref="UnitOfTorque"/>.</param>
    public static bool operator >=(UnitOfTorque x, UnitOfTorque y) => x.Torque >= y.Torque;
}
