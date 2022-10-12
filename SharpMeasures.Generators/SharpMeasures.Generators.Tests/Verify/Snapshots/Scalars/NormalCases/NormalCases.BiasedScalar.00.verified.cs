﻿//HintName: Temperature.Common.g.cs
//----------------------
// <auto-generated>
//      This file was generated by SharpMeasures.Generators.Scalars <stamp>
//
//      Changes to this file may cause incorrect behavior and will be lost if the code is regenerated.
// </auto-generated>
//----------------------

#nullable enable

/// <summary>A measure of the scalar quantity Temperature, expressed in <see cref="global::UnitOfTemperature"/>.</summary>
public partial class Temperature :
    global::SharpMeasures.IScalarQuantity<global::Temperature>,
    global::System.IComparable<global::Temperature>,
    global::System.IEquatable<global::Temperature>
{
    /// <summary>The magnitude of <see langword="this"/>, expressed in an arbitrary unit.</summary>
    /// <remarks>In most cases, expressing the magnitude in a specified <see cref="global::UnitOfTemperature"/> should be preferred. This is achieved through <see cref="InUnit(global::UnitOfTemperature)"/>.</remarks>
    public global::SharpMeasures.Scalar Magnitude { get; }

    /// <summary>Constructs a new <see cref="global::Temperature"/> representing { <paramref name="magnitude"/> }, expressed in an arbitrary unit.</summary>
    /// <param name="magnitude">The magnitude represented by the constructed <see cref="global::Temperature"/>, expressed in an arbitrary unit.</param>
    /// <remarks>Consider preferring construction through <see cref="global::Temperature(global::SharpMeasures.Scalar, global::UnitOfTemperature)"/>, where the magnitude is expressed in
    /// a specified <see cref="global::UnitOfTemperature"/>.</remarks>
    public Temperature(global::SharpMeasures.Scalar magnitude)
    {
        Magnitude = magnitude;
    }

    /// <summary>Constructs a new <see cref="global::Temperature"/> representing { <paramref name="magnitude"/> }, when expressed in <paramref name="unitOfTemperature"/>.</summary>
    /// <param name="magnitude">The magnitude represented by the constructed <see cref="global::Temperature"/>, when expressed in <paramref name="unitOfTemperature"/>.</param>
    /// <param name="unitOfTemperature">The <see cref="global::UnitOfTemperature"/> in which <paramref name="magnitude"/> is expressed.</param>
    /// <exception cref="global::System.ArgumentNullException"/>
    public Temperature(global::SharpMeasures.Scalar magnitude, global::UnitOfTemperature unitOfTemperature)
        : this(ComputeRepresentedMagnitude(magnitude, unitOfTemperature)) { }

    /// <summary>The magnitude of <see langword="this"/>, expressed in <paramref name="unitOfTemperature"/>.</summary>
    /// <param name="unitOfTemperature">The <see cref="global::UnitOfTemperature"/> in which the magnitude of <see langword="this"/> is expressed.</param>
    /// <exception cref="global::System.ArgumentNullException"/>
    public global::SharpMeasures.Scalar InUnit(global::UnitOfTemperature unitOfTemperature)
    {
        global::System.ArgumentNullException.ThrowIfNull(unitOfTemperature);

        return new(Magnitude / unitOfTemperature.TemperatureDifference.Magnitude + unitOfTemperature.Bias);
    }

    /// <summary>Produces a description of <see langword="this"/> containing the represented <see cref="Magnitude"/>, expressed in an arbitrary unit.</summary>
    public override string ToString() => Magnitude.ToString();

    /// <inheritdoc/>
    public virtual bool Equals(global::Temperature? other) => other is not null && Magnitude.Value == other.Magnitude.Value;

    /// <inheritdoc/>
    public override bool Equals(object? obj) => obj is global::Temperature other && Equals(other);

    /// <summary>Indicates whether <paramref name="lhs"/> and <paramref name="rhs"/> represent equivalent magnitudes.</summary>
    /// <param name="lhs">The left-hand side of the equality check.</param>
    /// <param name="rhs">The right-hand side of the equality check.</param>
    public static bool operator ==(global::Temperature? lhs, global::Temperature? rhs) => lhs?.Equals(rhs) ?? rhs is null;

    /// <summary>Indicates whether <paramref name="lhs"/> and <paramref name="rhs"/> represent inequivalent magnitudes.</summary>
    /// <param name="lhs">The left-hand side of the inequality check.</param>
    /// <param name="rhs">The right-hand side of the inequality check.</param>
    public static bool operator !=(global::Temperature? lhs, global::Temperature? rhs) => (lhs == rhs) is false;

    /// <inheritdoc/>
    public override int GetHashCode() => Magnitude.GetHashCode();

    /// <inheritdoc cref="global::SharpMeasures.Scalar.CompareTo(global::SharpMeasures.Scalar)"/>
    public int CompareTo(global::Temperature? other) => Magnitude.Value.CompareTo(other?.Magnitude.Value);

    /// <inheritdoc cref="global::SharpMeasures.Scalar.operator &lt;(global::SharpMeasures.Scalar, global::SharpMeasures.Scalar)"/>
    public static bool operator <(global::Temperature? x, global::Temperature? y) => x?.Magnitude.Value < y?.Magnitude.Value;
    /// <inheritdoc cref="global::SharpMeasures.Scalar.operator &gt;(global::SharpMeasures.Scalar, global::SharpMeasures.Scalar)"/>
    public static bool operator >(global::Temperature? x, global::Temperature? y) => x?.Magnitude.Value > y?.Magnitude.Value;
    /// <inheritdoc cref="global::SharpMeasures.Scalar.operator &lt;=(global::SharpMeasures.Scalar, global::SharpMeasures.Scalar)"/>
    public static bool operator <=(global::Temperature? x, global::Temperature? y) => x?.Magnitude.Value <= y?.Magnitude.Value;
    /// <inheritdoc cref="global::SharpMeasures.Scalar.operator &gt;=(global::SharpMeasures.Scalar, global::SharpMeasures.Scalar)"/>
    public static bool operator >=(global::Temperature? x, global::Temperature? y) => x?.Magnitude.Value >= y?.Magnitude.Value;

    /// <inheritdoc/>
    static global::Temperature global::SharpMeasures.IScalarQuantity<global::Temperature>.WithMagnitude(global::SharpMeasures.Scalar magnitude) => new(magnitude);

    /// <summary>Computes the represented magnitude based on a magnitude, <paramref name="magnitude"/>, expressed in
    /// a certain unit <paramref name="unitOfTemperature"/>.</summary>
    /// <param name="magnitude">The magnitude expressed in a certain unit <paramref name="unitOfTemperature"/>.</param>
    /// <param name="unitOfTemperature">The <see cref="global::UnitOfTemperature"/> in which <paramref name="magnitude"/> is expressed.</param>
    /// <exception cref="global::System.ArgumentNullException"/>
    private static global::SharpMeasures.Scalar ComputeRepresentedMagnitude(global::SharpMeasures.Scalar magnitude, global::UnitOfTemperature unitOfTemperature)
    {
        global::System.ArgumentNullException.ThrowIfNull(unitOfTemperature);

        return (magnitude - unitOfTemperature.Bias) * unitOfTemperature.TemperatureDifference.Magnitude;
    }
}
