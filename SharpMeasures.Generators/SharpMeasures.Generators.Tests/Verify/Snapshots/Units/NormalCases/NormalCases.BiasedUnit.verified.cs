﻿//HintName: UnitOfTemperature.Common.g.cs
//----------------------
// <auto-generated>
//      This file was generated by SharpMeasures.Generators.Units <stamp>
//
//      Changes to this file may cause incorrect behavior and will be lost if the code is regenerated.
// </auto-generated>
//----------------------

#nullable enable

/// <summary>A unit of measurement, describing <see cref="global::TemperatureDifference"/> together with a <see cref="global::SharpMeasures.Scalar"/> bias term.</summary>
public partial class UnitOfTemperature :
    global::System.IEquatable<global::UnitOfTemperature>
{
    /// <summary>The <see cref="global::TemperatureDifference"/> described by <see langword="this"/>.</summary>
    public global::TemperatureDifference TemperatureDifference { get; }
    /// <summary>The <see cref="global::SharpMeasures.Scalar"/> bias term associated with <see langword="this"/>.</summary>
    /// <remarks>This is the value of <see langword="this"/> when a unit with bias { 0 } represents the value { 0 }.</remarks>
    public global::SharpMeasures.Scalar Bias { get; }

    /// <summary>Constructs a new <see cref="global::UnitOfTemperature"/>, describing <paremref name="temperatureDifference"/>, together with a bias <paramref name="bias"/>.</summary>
    /// <param name="temperatureDifference">The <see cref="global::TemperatureDifference"/> described by the constructed <see cref="global::UnitOfTemperature"/>.</param>
    /// <param name="bias">The <see cref="global::SharpMeasures.Scalar"/> bias associated with the constructed <see cref="global::UnitOfTemperature"/>.</param>
    /// <exception cref="global::System.ArgumentNullException"/>
    private UnitOfTemperature(global::TemperatureDifference temperatureDifference, global::SharpMeasures.Scalar bias)
    {
        global::System.ArgumentNullException.ThrowIfNull(temperatureDifference);

        TemperatureDifference = temperatureDifference;
        Bias = bias;
    }

    /// <summary>Scales the <see cref="global::TemperatureDifference"/> described by <see langword="this"/> by <paramref name="scale"/> to derive a new <see cref="global::UnitOfTemperature"/>.</summary>
    /// <param name="scale">The described <see cref="global::TemperatureDifference"/> is scaled by this value.
    /// <para>The bias term associated with <see langword="this"/> is also scaled, but by the reciprocal / inverse of this value.</para></param>
    /// <remarks>The bias term associated with <see langword="this"/> is also scaled, but by { 1 / <paramref name="scale"/> }.
    /// <para>When used together with <see cref="WithBias(global::SharpMeasures.Scalar)"/>, the order matters.</para></remarks>
    public global::UnitOfTemperature ScaledBy(global::SharpMeasures.Scalar scale) => new(TemperatureDifference * scale, Bias / scale);

    /// <summary>Prefixes the <see cref="global::TemperatureDifference"/> described by <see langword="this"/> with <paramref name="prefix"/> to derive a new <see cref="global::UnitOfTemperature"/>.</summary>
    /// <param name="prefix">The described <see cref="global::TemperatureDifference"/> is prefixed by this <see cref="global::SharpMeasures.IPrefix"/>.</param>
    /// <remarks>The bias term associated with <see langword="this"/> is also scaled, but by the reciprocal / inverse of the scale-factor of <paramref name="prefix"/>.
    /// <para>Repeated invokation will <i>stack</i> the prefixes, rather than <i>replace</i> the previously applied <see cref="global::SharpMeasures.IPrefix"/>.</para>
    /// <para>When used together with <see cref="WithBias(global::SharpMeasures.Scalar)"/>, the order matters.</para></remarks>
    /// <exception cref="global::System.ArgumentNullException"/>
    public global::UnitOfTemperature WithPrefix<TPrefix>(TPrefix prefix) where TPrefix : global::SharpMeasures.IPrefix
    {
        global::System.ArgumentNullException.ThrowIfNull(prefix);

        return new(TemperatureDifference * prefix.Factor, Bias / prefix.Factor);
    }

    /// <summary>Derives a new <see cref="global::UnitOfTemperature"/> that has bias <paramref name="bias"/> relative to <see langword="this"/>.</summary>
    /// <param name="bias">The bias of the derived <see cref="global::UnitOfTemperature"/> relative to <see langword="this"/>.</param>
    /// <remarks>When used together with <see cref="ScaledBy(global::SharpMeasures.Scalar)"/> or <see cref="WithPrefix{TPrefix}(TPrefix)"/>, the order matters.</remarks>
    public global::UnitOfTemperature WithBias(global::SharpMeasures.Scalar bias) => new(TemperatureDifference, Bias + bias);

    ///<summary>Produces a description of <see langword="this"/> containing the described <see cref="global::TemperatureDifference"/> and the associated bias.</summary>
    public override string ToString() => "{TemperatureDifference} + {Bias}";

    /// <inheritdoc/>
    public virtual bool Equals(global::UnitOfTemperature? other) => other is not null && TemperatureDifference == other.TemperatureDifference && Bias == other.Bias;

    /// <inheritdoc/>
    public override bool Equals(object? obj) => obj is global::UnitOfTemperature other && Equals(other);

    /// <summary>Indicates whether <paramref name="lhs"/> and <paramref name="rhs"/> represent equivalent <see cref="global::TemperatureDifference"/> and bias.</summary>
    /// <param name="lhs">The left-hand side of the equality check.</param>
    /// <param name="rhs">The right-hand side of the equality check.</param>
    public static bool operator ==(global::UnitOfTemperature? lhs, global::UnitOfTemperature? rhs) => lhs?.Equals(rhs) ?? rhs is null;

    /// <summary>Indicates whether <paramref name="lhs"/> and <paramref name="rhs"/> represent inequivalent <see cref="global::TemperatureDifference"/> or bias.</summary>
    /// <param name="lhs">The left-hand side of the inequality check.</param>
    /// <param name="rhs">The right-hand side of the inequality check.</param>
    public static bool operator !=(global::UnitOfTemperature? lhs, global::UnitOfTemperature? rhs) => (lhs == rhs) is false;

    /// <inheritdoc/>
    public override int GetHashCode() => (TemperatureDifference, Bias).GetHashCode();
}
