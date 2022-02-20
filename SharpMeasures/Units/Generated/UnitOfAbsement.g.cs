#nullable enable

namespace ErikWe.SharpMeasures.Units;

using ErikWe.SharpMeasures.Quantities;

using System;

/// <summary>Describes a unit of the quantity <see cref="Quantities.Absement"/>, and related quantities.</summary>
/// <remarks>Common <see cref="UnitOfAbsement"/> exists as static properties, and from these custom <see cref="UnitOfAbsement"/> may be derived using
/// the instance-methods <see cref="WithPrefix(MetricPrefix)"/> or <see cref="ScaledBy(Scalar)"/>. Custom <see cref="UnitOfAbsement"/> can also be derived from
/// other units using the static <see cref="From(UnitOfLength, UnitOfTime)"/>.</remarks>
public readonly record struct UnitOfAbsement :
    IComparable<UnitOfAbsement>
{
    /// <summary>Derives a <see cref="UnitOfAbsement"/> according to { <paramref name="unitOfLength"/> ∙ <paramref name="unitOfTime"/> }.</summary>
    /// <param name="unitOfLength">A <see cref="UnitOfAbsement"/> is derived from multiplication of this <see cref="UnitOfLength"/> by <paramref name="unitOfTime"/>.</param>
    /// <param name="unitOfTime">A <see cref="UnitOfAbsement"/> is derived from multiplication of this <see cref="UnitOfTime"/> by <paramref name="unitOfLength"/>.</param>
    public static UnitOfAbsement From(UnitOfLength unitOfLength, UnitOfTime unitOfTime) => new(Absement.From(unitOfLength.Length.AsDistance, unitOfTime.Time));

    /// <summary>The SI unit of <see cref="Quantities.Absement"/>, derived according to { <see cref="UnitOfLength.Metre"/> ∙ <see cref="UnitOfTime.Second"/> }.
    /// Usually written as [m∙s].</summary>
    public static UnitOfAbsement MetreSecond { get; } = From(UnitOfLength.Metre, UnitOfTime.Second);

    /// <summary>The <see cref="Quantities.Absement"/> that the <see cref="UnitOfAbsement"/> represents.</summary>
    public Absement Absement { get; private init; }

    /// <summary>Constructs a new <see cref="UnitOfAbsement"/>, representing the <see cref="Quantities.Absement"/> <paramref name="absement"/>.</summary>
    /// <param name="absement">The <see cref="Quantities.Absement"/> that the new <see cref="UnitOfAbsement"/> represents.</param>
    private UnitOfAbsement(Absement absement)
    {
        Absement = absement;
    }

    /// <summary>Derives a new <see cref="UnitOfAbsement"/> from this instance, by prefixing the <see cref="MetricPrefix"/> <paramref name="prefix"/>.</summary>
    /// <param name="prefix">The <see cref="MetricPrefix"/> with which the new <see cref="UnitOfAbsement"/> is expressed.</param>
    /// <remarks>Any <see cref="MetricPrefix"/> applied to the original instance will be retained, essentially stacking the prefixes.</remarks>
    public UnitOfAbsement WithPrefix(MetricPrefix prefix) => new(Absement * prefix.Scale);
    /// <summary>Derives a new <see cref="UnitOfAbsement"/> from this instance, through scaling by <paramref name="scale"/>.</summary>
    /// <param name="scale">The original <see cref="UnitOfAbsement"/> is scaled by this value.</param>
    public UnitOfAbsement ScaledBy(Scalar scale) => new(Absement * scale);
    /// <summary>Derives a new <see cref="UnitOfAbsement"/> from this instance, through scaling by <paramref name="scale"/>.</summary>
    /// <param name="scale">The original <see cref="UnitOfAbsement"/> is scaled by this value.</param>
    public UnitOfAbsement ScaledBy(double scale) => new(Absement * scale);

    /// <inheritdoc/>
    public int CompareTo(UnitOfAbsement other) => Absement.CompareTo(other.Absement);
    /// <summary>Produces a formatted string constisting of the type followed by the represented <see cref="Quantities.Absement"/>.</summary>
    public override string ToString() => $"{GetType()}: {Absement}";

    /// <summary>Determines whether the <see cref="Quantities.Absement"/> represented by <paramref name="x"/> is
    /// less than that of <paramref name="y"/>.</summary>
    /// <param name="x">The method determines whether the <see cref="Quantities.Absement"/> represented by this <see cref="UnitOfAbsement"/> is
    /// less than that of <paramref name="y"/>.</param>
    /// <param name="y">The method determines whether the <see cref="Quantities.Absement"/> represented by <paramref name="x"/> is
    /// less than that of this <see cref="UnitOfAbsement"/>.</param>
    public static bool operator <(UnitOfAbsement x, UnitOfAbsement y) => x.Absement < y.Absement;
    /// <summary>Determines whether the <see cref="Quantities.Absement"/> represented by <paramref name="x"/> is
    /// greater than that of <paramref name="y"/>.</summary>
    /// <param name="x">The method determines whether the <see cref="Quantities.Absement"/> represented by this <see cref="UnitOfAbsement"/> is
    /// greater than that of <paramref name="y"/>.</param>
    /// <param name="y">The method determines whether the <see cref="Quantities.Absement"/> represented by <paramref name="x"/> is
    /// greater than that of this <see cref="UnitOfAbsement"/>.</param>
    public static bool operator >(UnitOfAbsement x, UnitOfAbsement y) => x.Absement > y.Absement;
    /// <summary>Determines whether the <see cref="Quantities.Absement"/> represented by <paramref name="x"/> is
    /// less than or equal to that of <paramref name="y"/>.</summary>
    /// <param name="x">The method determines whether the <see cref="Quantities.Absement"/> represented by this <see cref="UnitOfAbsement"/> is
    /// less than or equal to that of <paramref name="y"/>.</param>
    /// <param name="y">The method determines whether the <see cref="Quantities.Absement"/> represented by <paramref name="x"/> is
    /// less than or equal to that of this <see cref="UnitOfAbsement"/>.</param>
    public static bool operator <=(UnitOfAbsement x, UnitOfAbsement y) => x.Absement <= y.Absement;
    /// <summary>Determines whether the <see cref="Quantities.Absement"/> represented by <paramref name="x"/> is
    /// greater than or equal to that of <paramref name="y"/>.</summary>
    /// <param name="x">The method determines whether the <see cref="Quantities.Absement"/> represented by this <see cref="UnitOfAbsement"/> is
    /// greater than or equal to that of <paramref name="y"/>.</param>
    /// <param name="y">The method determines whether the <see cref="Quantities.Absement"/> represented by <paramref name="x"/> is
    /// greater than or equal to that of this <see cref="UnitOfAbsement"/>.</param>
    public static bool operator >=(UnitOfAbsement x, UnitOfAbsement y) => x.Absement >= y.Absement;
}
