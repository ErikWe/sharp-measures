namespace ErikWe.SharpMeasures.Units;

using ErikWe.SharpMeasures.Quantities;

using System;

/// <summary>Describes a unit of the quantity <see cref="Quantities.Momentum"/>, and related quantities.</summary>
/// <remarks>Common <see cref="UnitOfMomentum"/> exists as static properties, and from these custom <see cref="UnitOfMomentum"/> may be derived using
/// the instance-methods <see cref="WithPrefix(MetricPrefix)"/> or <see cref="ScaledBy(Scalar)"/>. Custom <see cref="UnitOfMomentum"/> can also be derived from
/// other units using the static <see cref="From(UnitOfMass, UnitOfVelocity)"/>.</remarks>
public readonly record struct UnitOfMomentum :
    IComparable<UnitOfMomentum>
{
    /// <summary>Derives a <see cref="UnitOfMomentum"/> according to { <paramref name="unitOfMass"/> ∙ <paramref name="unitOfVelocity"/> }.</summary>
    /// <param name="unitOfMass">A <see cref="UnitOfMomentum"/> is derived from multiplication of this <see cref="UnitOfMass"/> by <paramref name="unitOfVelocity"/>.</param>
    /// <param name="unitOfVelocity">A <see cref="UnitOfMomentum"/> is derived from multiplication of this <see cref="UnitOfVelocity"/> by <paramref name="unitOfMass"/>.</param>
    public static UnitOfMomentum From(UnitOfMass unitOfMass, UnitOfVelocity unitOfVelocity) => new(Momentum.From(unitOfMass.Mass, unitOfVelocity.Speed));

    /// <summary>The SI unit of <see cref="Quantities.Momentum"/>, derived according to { <see cref="UnitOfMass.Kilogram"/> ∙ <see cref="UnitOfVelocity.MetrePerSecond"/> }.
    /// Usually written as [kg∙m/s] or [kg∙m∙s⁻¹].</summary>
    public static UnitOfMomentum KilogramMetrePerSecond { get; } = From(UnitOfMass.Kilogram, UnitOfVelocity.MetrePerSecond);

    /// <summary>The <see cref="Quantities.Momentum"/> that the <see cref="UnitOfMomentum"/> represents.</summary>
    public Momentum Momentum { get; private init; }

    /// <summary>Constructs a new <see cref="UnitOfMomentum"/>, representing the <see cref="Quantities.Momentum"/> <paramref name="momentum"/>.</summary>
    /// <param name="momentum">The <see cref="Quantities.Momentum"/> that the new <see cref="UnitOfMomentum"/> represents.</param>
    private UnitOfMomentum(Momentum momentum)
    {
        Momentum = momentum;
    }

    /// <summary>Derives a new <see cref="UnitOfMomentum"/> from this instance, by prefixing the <see cref="MetricPrefix"/> <paramref name="prefix"/>.</summary>
    /// <param name="prefix">The <see cref="MetricPrefix"/> with which the new <see cref="UnitOfMomentum"/> is expressed.</param>
    /// <remarks>Any <see cref="MetricPrefix"/> applied to the original instance will be retained, essentially stacking the prefixes.</remarks>
    public UnitOfMomentum WithPrefix(MetricPrefix prefix) => new(Momentum * prefix.Scale);
    /// <summary>Derives a new <see cref="UnitOfMomentum"/> from this instance, through scaling by <paramref name="scale"/>.</summary>
    /// <param name="scale">The original <see cref="UnitOfMomentum"/> is scaled by this value.</param>
    public UnitOfMomentum ScaledBy(Scalar scale) => new(Momentum * scale);
    /// <summary>Derives a new <see cref="UnitOfMomentum"/> from this instance, through scaling by <paramref name="scale"/>.</summary>
    /// <param name="scale">The original <see cref="UnitOfMomentum"/> is scaled by this value.</param>
    public UnitOfMomentum ScaledBy(double scale) => new(Momentum * scale);

    /// <inheritdoc/>
    public int CompareTo(UnitOfMomentum other) => Momentum.CompareTo(other.Momentum);
    /// <summary>Produces a formatted string constisting of the type followed by the represented <see cref="Quantities.Momentum"/>.</summary>
    public override string ToString() => $"{GetType()}: {Momentum}";

    /// <summary>Determines whether the <see cref="Quantities.Momentum"/> represented by <paramref name="x"/> is
    /// less than that of <paramref name="y"/>.</summary>
    /// <param name="x">The method determines whether the <see cref="Quantities.Momentum"/> represented by this <see cref="UnitOfMomentum"/> is
    /// less than that of <paramref name="y"/>.</param>
    /// <param name="y">The method determines whether the <see cref="Quantities.Momentum"/> represented by <paramref name="x"/> is
    /// less than that of this <see cref="UnitOfMomentum"/>.</param>
    public static bool operator <(UnitOfMomentum x, UnitOfMomentum y) => x.Momentum < y.Momentum;
    /// <summary>Determines whether the <see cref="Quantities.Momentum"/> represented by <paramref name="x"/> is
    /// greater than that of <paramref name="y"/>.</summary>
    /// <param name="x">The method determines whether the <see cref="Quantities.Momentum"/> represented by this <see cref="UnitOfMomentum"/> is
    /// greater than that of <paramref name="y"/>.</param>
    /// <param name="y">The method determines whether the <see cref="Quantities.Momentum"/> represented by <paramref name="x"/> is
    /// greater than that of this <see cref="UnitOfMomentum"/>.</param>
    public static bool operator >(UnitOfMomentum x, UnitOfMomentum y) => x.Momentum > y.Momentum;
    /// <summary>Determines whether the <see cref="Quantities.Momentum"/> represented by <paramref name="x"/> is
    /// less than or equal to that of <paramref name="y"/>.</summary>
    /// <param name="x">The method determines whether the <see cref="Quantities.Momentum"/> represented by this <see cref="UnitOfMomentum"/> is
    /// less than or equal to that of <paramref name="y"/>.</param>
    /// <param name="y">The method determines whether the <see cref="Quantities.Momentum"/> represented by <paramref name="x"/> is
    /// less than or equal to that of this <see cref="UnitOfMomentum"/>.</param>
    public static bool operator <=(UnitOfMomentum x, UnitOfMomentum y) => x.Momentum <= y.Momentum;
    /// <summary>Determines whether the <see cref="Quantities.Momentum"/> represented by <paramref name="x"/> is
    /// greater than or equal to that of <paramref name="y"/>.</summary>
    /// <param name="x">The method determines whether the <see cref="Quantities.Momentum"/> represented by this <see cref="UnitOfMomentum"/> is
    /// greater than or equal to that of <paramref name="y"/>.</param>
    /// <param name="y">The method determines whether the <see cref="Quantities.Momentum"/> represented by <paramref name="x"/> is
    /// greater than or equal to that of this <see cref="UnitOfMomentum"/>.</param>
    public static bool operator >=(UnitOfMomentum x, UnitOfMomentum y) => x.Momentum >= y.Momentum;
}
