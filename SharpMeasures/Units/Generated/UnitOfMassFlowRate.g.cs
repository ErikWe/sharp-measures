namespace ErikWe.SharpMeasures.Units;

using ErikWe.SharpMeasures.Quantities;

using System;

/// <summary>Describes a unit of the quantity <see cref="Quantities.MassFlowRate"/>.</summary>
/// <remarks>Common <see cref="UnitOfMassFlowRate"/> exists as static properties, and from these custom <see cref="UnitOfMassFlowRate"/> may be derived using
/// the instance-methods <see cref="WithPrefix(MetricPrefix)"/> or <see cref="ScaledBy(Scalar)"/>. Custom <see cref="UnitOfMassFlowRate"/> can also be derived from
/// other units using the static <see cref="From(UnitOfMass, UnitOfTime)"/>.</remarks>
public readonly record struct UnitOfMassFlowRate :
    IComparable<UnitOfMassFlowRate>
{
    /// <summary>Derives a <see cref="UnitOfMassFlowRate"/> according to { <paramref name="unitOfMass"/> / <paramref name="unitOfTime"/> }.</summary>
    /// <param name="unitOfMass">A <see cref="UnitOfMassFlowRate"/> is derived from division of this <see cref="UnitOfMass"/> by <paramref name="unitOfTime"/>.</param>
    /// <param name="unitOfTime">A <see cref="UnitOfMassFlowRate"/> is derived from division of <paramref name="unitOfMass"/> by this <see cref="UnitOfTime"/>.</param>
    public static UnitOfMassFlowRate From(UnitOfMass unitOfMass, UnitOfTime unitOfTime) => new(MassFlowRate.From(unitOfMass.Mass, unitOfTime.Time));

    /// <summary>The SI unit of <see cref="Quantities.MassFlowRate"/>, derived according to { <see cref="UnitOfMass.Kilogram"/> / <see cref="UnitOfTime.Second"/> }.
    /// Usually written as [kg/s] or [kg∙s⁻¹].</summary>
    public static UnitOfMassFlowRate KilogramPerSecond { get; } = From(UnitOfMass.Kilogram, UnitOfTime.Second);
    /// <summary>Expresses <see cref="Quantities.MassFlowRate"/> according to { <see cref="UnitOfMass.Pound"/> / <see cref="UnitOfTime.Second"/> }.
    /// Usually written as [lb/s] or [lb∙s⁻¹].</summary>
    public static UnitOfMassFlowRate PoundPerSecond { get; } = From(UnitOfMass.Pound, UnitOfTime.Second);

    /// <summary>The <see cref="Quantities.MassFlowRate"/> that the <see cref="UnitOfMassFlowRate"/> represents.</summary>
    public MassFlowRate MassFlowRate { get; private init; }

    /// <summary>Constructs a new <see cref="UnitOfMassFlowRate"/>, representing the <see cref="Quantities.MassFlowRate"/> <paramref name="massFlowRate"/>.</summary>
    /// <param name="massFlowRate">The <see cref="Quantities.MassFlowRate"/> that the new <see cref="UnitOfMassFlowRate"/> represents.</param>
    private UnitOfMassFlowRate(MassFlowRate massFlowRate)
    {
        MassFlowRate = massFlowRate;
    }

    /// <summary>Derives a new <see cref="UnitOfMassFlowRate"/> from this instance, by prefixing the <see cref="MetricPrefix"/> <paramref name="prefix"/>.</summary>
    /// <param name="prefix">The <see cref="MetricPrefix"/> with which the new <see cref="UnitOfMassFlowRate"/> is expressed.</param>
    /// <remarks>Any <see cref="MetricPrefix"/> applied to the original instance will be retained, essentially stacking the prefixes.</remarks>
    public UnitOfMassFlowRate WithPrefix(MetricPrefix prefix) => new(MassFlowRate * prefix.Scale);
    /// <summary>Derives a new <see cref="UnitOfMassFlowRate"/> from this instance, through scaling by <paramref name="scale"/>.</summary>
    /// <param name="scale">The original <see cref="UnitOfMassFlowRate"/> is scaled by this value.</param>
    public UnitOfMassFlowRate ScaledBy(Scalar scale) => new(MassFlowRate * scale);
    /// <summary>Derives a new <see cref="UnitOfMassFlowRate"/> from this instance, through scaling by <paramref name="scale"/>.</summary>
    /// <param name="scale">The original <see cref="UnitOfMassFlowRate"/> is scaled by this value.</param>
    public UnitOfMassFlowRate ScaledBy(double scale) => new(MassFlowRate * scale);

    /// <inheritdoc/>
    public int CompareTo(UnitOfMassFlowRate other) => MassFlowRate.CompareTo(other.MassFlowRate);
    /// <summary>Produces a formatted string constisting of the type followed by the represented <see cref="Quantities.MassFlowRate"/>.</summary>
    public override string ToString() => $"{GetType()}: {MassFlowRate}";

    /// <summary>Determines whether the <see cref="Quantities.MassFlowRate"/> represented by <paramref name="x"/> is
    /// less than that of <paramref name="y"/>.</summary>
    /// <param name="x">The method determines whether the <see cref="Quantities.MassFlowRate"/> represented by this <see cref="UnitOfMassFlowRate"/> is
    /// less than that of <paramref name="y"/>.</param>
    /// <param name="y">The method determines whether the <see cref="Quantities.MassFlowRate"/> represented by <paramref name="x"/> is
    /// less than that of this <see cref="UnitOfMassFlowRate"/>.</param>
    public static bool operator <(UnitOfMassFlowRate x, UnitOfMassFlowRate y) => x.MassFlowRate < y.MassFlowRate;
    /// <summary>Determines whether the <see cref="Quantities.MassFlowRate"/> represented by <paramref name="x"/> is
    /// greater than that of <paramref name="y"/>.</summary>
    /// <param name="x">The method determines whether the <see cref="Quantities.MassFlowRate"/> represented by this <see cref="UnitOfMassFlowRate"/> is
    /// greater than that of <paramref name="y"/>.</param>
    /// <param name="y">The method determines whether the <see cref="Quantities.MassFlowRate"/> represented by <paramref name="x"/> is
    /// greater than that of this <see cref="UnitOfMassFlowRate"/>.</param>
    public static bool operator >(UnitOfMassFlowRate x, UnitOfMassFlowRate y) => x.MassFlowRate > y.MassFlowRate;
    /// <summary>Determines whether the <see cref="Quantities.MassFlowRate"/> represented by <paramref name="x"/> is
    /// less than or equal to that of <paramref name="y"/>.</summary>
    /// <param name="x">The method determines whether the <see cref="Quantities.MassFlowRate"/> represented by this <see cref="UnitOfMassFlowRate"/> is
    /// less than or equal to that of <paramref name="y"/>.</param>
    /// <param name="y">The method determines whether the <see cref="Quantities.MassFlowRate"/> represented by <paramref name="x"/> is
    /// less than or equal to that of this <see cref="UnitOfMassFlowRate"/>.</param>
    public static bool operator <=(UnitOfMassFlowRate x, UnitOfMassFlowRate y) => x.MassFlowRate <= y.MassFlowRate;
    /// <summary>Determines whether the <see cref="Quantities.MassFlowRate"/> represented by <paramref name="x"/> is
    /// greater than or equal to that of <paramref name="y"/>.</summary>
    /// <param name="x">The method determines whether the <see cref="Quantities.MassFlowRate"/> represented by this <see cref="UnitOfMassFlowRate"/> is
    /// greater than or equal to that of <paramref name="y"/>.</param>
    /// <param name="y">The method determines whether the <see cref="Quantities.MassFlowRate"/> represented by <paramref name="x"/> is
    /// greater than or equal to that of this <see cref="UnitOfMassFlowRate"/>.</param>
    public static bool operator >=(UnitOfMassFlowRate x, UnitOfMassFlowRate y) => x.MassFlowRate >= y.MassFlowRate;
}
