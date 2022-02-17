namespace ErikWe.SharpMeasures.Units;

using ErikWe.SharpMeasures.Quantities;

using System;

/// <summary>Describes a unit of the quantity <see cref="Quantities.MomentOfInertia"/>.</summary>
/// <remarks>Common <see cref="UnitOfMomentOfInertia"/> exists as static properties, and from these custom <see cref="UnitOfMomentOfInertia"/> may be derived using
/// the instance-methods <see cref="WithPrefix(MetricPrefix)"/> or <see cref="ScaledBy(Scalar)"/>. Custom <see cref="UnitOfMomentOfInertia"/> can also be derived from
/// other units using the static <see cref="From(UnitOfMass, UnitOfLength)"/>.</remarks>
public readonly record struct UnitOfMomentOfInertia :
    IComparable<UnitOfMomentOfInertia>
{
    /// <summary>Derives a <see cref="UnitOfMomentOfInertia"/> according to { <paramref name="unitOfMass"/> ∙ <paramref name="unitOfLength"/>² }.</summary>
    /// <param name="unitOfMass">A <see cref="UnitOfMomentOfInertia"/> is derived from multiplication of this <see cref="UnitOfMass"/> by the square
    /// of <paramref name="unitOfLength"/>.</param>
    /// <param name="unitOfLength">A <see cref="UnitOfMomentOfInertia"/> is derived from multiplication of the square of this <see cref="UnitOfLength"/>
    /// by <paramref name="unitOfMass"/>.</param>
    public static UnitOfMomentOfInertia From(UnitOfMass unitOfMass, UnitOfLength unitOfLength) => new(MomentOfInertia.From(unitOfMass.Mass, unitOfLength.Length.AsDistance));

    /// <summary>The SI unit of <see cref="Quantities.MomentOfInertia"/>, derived according to { <see cref="UnitOfMass.Kilogram"/> ∙ <see cref="UnitOfLength.Metre"/>² }.
    /// Usually written as [kg∙m²].</summary>
    public static UnitOfMomentOfInertia KilogramSquareMetre { get; } = From(UnitOfMass.Kilogram, UnitOfLength.Metre);

    /// <summary>The <see cref="Quantities.MomentOfInertia"/> that the <see cref="UnitOfMomentOfInertia"/> represents.</summary>
    public MomentOfInertia MomentOfInertia { get; private init; }

    /// <summary>Constructs a new <see cref="UnitOfMomentOfInertia"/>, representing the <see cref="Quantities.MomentOfInertia"/> <paramref name="momentOfInertia"/>.</summary>
    /// <param name="momentOfInertia">The <see cref="Quantities.MomentOfInertia"/> that the new <see cref="UnitOfMomentOfInertia"/> represents.</param>
    private UnitOfMomentOfInertia(MomentOfInertia momentOfInertia)
    {
        MomentOfInertia = momentOfInertia;
    }

    /// <summary>Derives a new <see cref="UnitOfMomentOfInertia"/> from this instance, by prefixing the <see cref="MetricPrefix"/> <paramref name="prefix"/>.</summary>
    /// <param name="prefix">The <see cref="MetricPrefix"/> with which the new <see cref="UnitOfMomentOfInertia"/> is expressed.</param>
    /// <remarks>Any <see cref="MetricPrefix"/> applied to the original instance will be retained, essentially stacking the prefixes.</remarks>
    public UnitOfMomentOfInertia WithPrefix(MetricPrefix prefix) => new(MomentOfInertia * prefix.Scale);
    /// <summary>Derives a new <see cref="UnitOfMomentOfInertia"/> from this instance, through scaling by <paramref name="scale"/>.</summary>
    /// <param name="scale">The original <see cref="UnitOfMomentOfInertia"/> is scaled by this value.</param>
    public UnitOfMomentOfInertia ScaledBy(Scalar scale) => new(MomentOfInertia * scale);
    /// <summary>Derives a new <see cref="UnitOfMomentOfInertia"/> from this instance, through scaling by <paramref name="scale"/>.</summary>
    /// <param name="scale">The original <see cref="UnitOfMomentOfInertia"/> is scaled by this value.</param>
    public UnitOfMomentOfInertia ScaledBy(double scale) => new(MomentOfInertia * scale);

    /// <inheritdoc/>
    public int CompareTo(UnitOfMomentOfInertia other) => MomentOfInertia.CompareTo(other.MomentOfInertia);
    /// <summary>Produces a formatted string constisting of the type followed by the represented <see cref="Quantities.MomentOfInertia"/>.</summary>
    public override string ToString() => $"{GetType()}: {MomentOfInertia}";

    /// <summary>Determines whether the <see cref="Quantities.MomentOfInertia"/> represented by <paramref name="x"/> is
    /// less than that of <paramref name="y"/>.</summary>
    /// <param name="x">The method determines whether the <see cref="Quantities.MomentOfInertia"/> represented by this <see cref="UnitOfMomentOfInertia"/> is
    /// less than that of <paramref name="y"/>.</param>
    /// <param name="y">The method determines whether the <see cref="Quantities.MomentOfInertia"/> represented by <paramref name="x"/> is
    /// less than that of this <see cref="UnitOfMomentOfInertia"/>.</param>
    public static bool operator <(UnitOfMomentOfInertia x, UnitOfMomentOfInertia y) => x.MomentOfInertia < y.MomentOfInertia;
    /// <summary>Determines whether the <see cref="Quantities.MomentOfInertia"/> represented by <paramref name="x"/> is
    /// greater than that of <paramref name="y"/>.</summary>
    /// <param name="x">The method determines whether the <see cref="Quantities.MomentOfInertia"/> represented by this <see cref="UnitOfMomentOfInertia"/> is
    /// greater than that of <paramref name="y"/>.</param>
    /// <param name="y">The method determines whether the <see cref="Quantities.MomentOfInertia"/> represented by <paramref name="x"/> is
    /// greater than that of this <see cref="UnitOfMomentOfInertia"/>.</param>
    public static bool operator >(UnitOfMomentOfInertia x, UnitOfMomentOfInertia y) => x.MomentOfInertia > y.MomentOfInertia;
    /// <summary>Determines whether the <see cref="Quantities.MomentOfInertia"/> represented by <paramref name="x"/> is
    /// less than or equal to that of <paramref name="y"/>.</summary>
    /// <param name="x">The method determines whether the <see cref="Quantities.MomentOfInertia"/> represented by this <see cref="UnitOfMomentOfInertia"/> is
    /// less than or equal to that of <paramref name="y"/>.</param>
    /// <param name="y">The method determines whether the <see cref="Quantities.MomentOfInertia"/> represented by <paramref name="x"/> is
    /// less than or equal to that of this <see cref="UnitOfMomentOfInertia"/>.</param>
    public static bool operator <=(UnitOfMomentOfInertia x, UnitOfMomentOfInertia y) => x.MomentOfInertia <= y.MomentOfInertia;
    /// <summary>Determines whether the <see cref="Quantities.MomentOfInertia"/> represented by <paramref name="x"/> is
    /// greater than or equal to that of <paramref name="y"/>.</summary>
    /// <param name="x">The method determines whether the <see cref="Quantities.MomentOfInertia"/> represented by this <see cref="UnitOfMomentOfInertia"/> is
    /// greater than or equal to that of <paramref name="y"/>.</param>
    /// <param name="y">The method determines whether the <see cref="Quantities.MomentOfInertia"/> represented by <paramref name="x"/> is
    /// greater than or equal to that of this <see cref="UnitOfMomentOfInertia"/>.</param>
    public static bool operator >=(UnitOfMomentOfInertia x, UnitOfMomentOfInertia y) => x.MomentOfInertia >= y.MomentOfInertia;
}
