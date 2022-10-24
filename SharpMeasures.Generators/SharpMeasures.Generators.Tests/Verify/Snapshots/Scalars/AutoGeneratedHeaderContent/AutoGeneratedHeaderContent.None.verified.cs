﻿//HintName: Length.Common.g.cs
#nullable enable

/// <summary>A measure of the scalar quantity Length, expressed in <see cref="global::UnitOfLength"/>.</summary>
public partial class Length :
    global::SharpMeasures.IScalarQuantity<global::Length>,
    global::System.IComparable<global::Length>,
    global::System.IEquatable<global::Length>
{
    /// <summary>The <see cref="global::Length"/> representing { 0 }.</summary>
    public static global::Length Zero { get; } = new(0);

    /// <summary>The magnitude of <see langword="this"/>, expressed in an arbitrary unit.</summary>
    /// <remarks>In most cases, expressing the magnitude in a specified <see cref="global::UnitOfLength"/> should be preferred. This is achieved through <see cref="InUnit(global::UnitOfLength)"/>.</remarks>
    public global::SharpMeasures.Scalar Magnitude { get; }

    /// <summary>Constructs a new <see cref="global::Length"/> representing { <paramref name="magnitude"/> }, expressed in an arbitrary unit.</summary>
    /// <param name="magnitude">The magnitude represented by the constructed <see cref="global::Length"/>, expressed in an arbitrary unit.</param>
    /// <remarks>Consider preferring construction through <see cref="global::Length(global::SharpMeasures.Scalar, global::UnitOfLength)"/>, where the magnitude is expressed in
    /// a specified <see cref="global::UnitOfLength"/>.</remarks>
    public Length(global::SharpMeasures.Scalar magnitude)
    {
        Magnitude = magnitude;
    }

    /// <summary>Constructs a new <see cref="global::Length"/> representing { <paramref name="magnitude"/> }, when expressed in <paramref name="unitOfLength"/>.</summary>
    /// <param name="magnitude">The magnitude represented by the constructed <see cref="global::Length"/>, when expressed in <paramref name="unitOfLength"/>.</param>
    /// <param name="unitOfLength">The <see cref="global::UnitOfLength"/> in which <paramref name="magnitude"/> is expressed.</param>
    /// <exception cref="global::System.ArgumentNullException"/>
    public Length(global::SharpMeasures.Scalar magnitude, global::UnitOfLength unitOfLength)
        : this(ComputeRepresentedMagnitude(magnitude, unitOfLength)) { }

    /// <summary>The magnitude of <see langword="this"/>, expressed in <paramref name="unitOfLength"/>.</summary>
    /// <param name="unitOfLength">The <see cref="global::UnitOfLength"/> in which the magnitude of <see langword="this"/> is expressed.</param>
    /// <exception cref="global::System.ArgumentNullException"/>
    public global::SharpMeasures.Scalar InUnit(global::UnitOfLength unitOfLength)
    {
        global::System.ArgumentNullException.ThrowIfNull(unitOfLength);

        return new(Magnitude / unitOfLength.Length.Magnitude);
    }

    /// <summary>Formats the represented <see cref="Magnitude"/> using the current culture.</summary>
    public override string ToString() => ToString(global::System.Globalization.CultureInfo.CurrentCulture);

    /// <summary>Formats the represented <see cref="Magnitude"/> according to <paramref name="format"/>, using the current culture.</summary>
    public string ToString(string? format) => ToString(format, global::System.Globalization.CultureInfo.CurrentCulture);

    /// <summary>Formats the represented <see cref="Magnitude"/> using the culture-specific formatting information provided by <paramref name="formatProvider"/>.</summary>
    public string ToString(global::System.IFormatProvider? formatProvider) => ToString("G", formatProvider);

    /// <summary>Formats the represented <see cref="Magnitude"/> according to <paramref name="format"/>, using the culture-specific formatting information provided by <paramref name="formatProvider"/>.</summary>
    public string ToString(string? format, global::System.IFormatProvider? formatProvider) => Magnitude.ToString(format, formatProvider);

    /// <inheritdoc/>
    public virtual bool Equals(global::Length? other) => other is not null && Magnitude.Value == other.Magnitude.Value;

    /// <inheritdoc/>
    public override bool Equals(object? obj) => obj is global::Length other && Equals(other);

    /// <summary>Indicates whether <paramref name="lhs"/> and <paramref name="rhs"/> represent equivalent magnitudes.</summary>
    /// <param name="lhs">The left-hand side of the equality check.</param>
    /// <param name="rhs">The right-hand side of the equality check.</param>
    public static bool operator ==(global::Length? lhs, global::Length? rhs) => lhs?.Equals(rhs) ?? rhs is null;

    /// <summary>Indicates whether <paramref name="lhs"/> and <paramref name="rhs"/> represent inequivalent magnitudes.</summary>
    /// <param name="lhs">The left-hand side of the inequality check.</param>
    /// <param name="rhs">The right-hand side of the inequality check.</param>
    public static bool operator !=(global::Length? lhs, global::Length? rhs) => (lhs == rhs) is false;

    /// <inheritdoc/>
    public override int GetHashCode() => Magnitude.GetHashCode();

    /// <inheritdoc cref="global::SharpMeasures.Scalar.CompareTo(global::SharpMeasures.Scalar)"/>
    public int CompareTo(global::Length? other) => Magnitude.Value.CompareTo(other?.Magnitude.Value);

    /// <inheritdoc cref="global::SharpMeasures.Scalar.operator &lt;(global::SharpMeasures.Scalar, global::SharpMeasures.Scalar)"/>
    public static bool operator <(global::Length? x, global::Length? y) => x?.Magnitude.Value < y?.Magnitude.Value;
    /// <inheritdoc cref="global::SharpMeasures.Scalar.operator &gt;(global::SharpMeasures.Scalar, global::SharpMeasures.Scalar)"/>
    public static bool operator >(global::Length? x, global::Length? y) => x?.Magnitude.Value > y?.Magnitude.Value;
    /// <inheritdoc cref="global::SharpMeasures.Scalar.operator &lt;=(global::SharpMeasures.Scalar, global::SharpMeasures.Scalar)"/>
    public static bool operator <=(global::Length? x, global::Length? y) => x?.Magnitude.Value <= y?.Magnitude.Value;
    /// <inheritdoc cref="global::SharpMeasures.Scalar.operator &gt;=(global::SharpMeasures.Scalar, global::SharpMeasures.Scalar)"/>
    public static bool operator >=(global::Length? x, global::Length? y) => x?.Magnitude.Value >= y?.Magnitude.Value;

    /// <inheritdoc/>
    static global::Length global::SharpMeasures.IScalarQuantity<global::Length>.WithMagnitude(global::SharpMeasures.Scalar magnitude) => new(magnitude);

    /// <summary>Computes the represented magnitude based on a magnitude, <paramref name="magnitude"/>, expressed in
    /// a certain unit <paramref name="unitOfLength"/>.</summary>
    /// <param name="magnitude">The magnitude expressed in a certain unit <paramref name="unitOfLength"/>.</param>
    /// <param name="unitOfLength">The <see cref="global::UnitOfLength"/> in which <paramref name="magnitude"/> is expressed.</param>
    /// <exception cref="global::System.ArgumentNullException"/>
    private static global::SharpMeasures.Scalar ComputeRepresentedMagnitude(global::SharpMeasures.Scalar magnitude, global::UnitOfLength unitOfLength)
    {
        global::System.ArgumentNullException.ThrowIfNull(unitOfLength);

        return magnitude * unitOfLength.Length.Magnitude;
    }
}
