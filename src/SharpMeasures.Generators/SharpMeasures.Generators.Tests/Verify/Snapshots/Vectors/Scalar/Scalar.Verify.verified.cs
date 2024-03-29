﻿//HintName: Position3.Common.g.cs
//----------------------
// <auto-generated>
//      This file was generated by SharpMeasures.Generators.Vectors <stamp>
//
//      Changes to this file may cause incorrect behavior and will be lost if the code is regenerated.
// </auto-generated>
//----------------------

#nullable enable

/// <summary>A measure of the 3-dimensional vector quantity Position, expressed in <see cref="global::UnitOfLength"/>.</summary>
public partial class Position3 :
    global::SharpMeasures.IVector3Quantity<global::Position3>,
    global::System.IEquatable<global::Position3>
{

    /// <summary>The <see cref="global::Position3"/> representing { 0, 0, 0 }.</summary>
    public static global::Position3 Zero { get; } = new(0, 0, 0);

    /// <summary>The X-component of <see langword="this"/>.</summary>
    public global::Length X { get; }
    /// <summary>The Y-component of <see langword="this"/>.</summary>
    public global::Length Y { get; }
    /// <summary>The Z-component of <see langword="this"/>.</summary>
    public global::Length Z { get; }

    /// <summary>The magnitude of the X-component of <see langword="this"/>.</summary>
    global::SharpMeasures.Scalar global::SharpMeasures.IVector3Quantity.X => X.Magnitude;
    /// <summary>The magnitude of the Y-component of <see langword="this"/>.</summary>
    global::SharpMeasures.Scalar global::SharpMeasures.IVector3Quantity.Y => Y.Magnitude;
    /// <summary>The magnitude of the Z-component of <see langword="this"/>.</summary>
    global::SharpMeasures.Scalar global::SharpMeasures.IVector3Quantity.Z => Z.Magnitude;

    /// <summary>The magnitudes of the components of <see langword="this"/>, expressed in an arbitrary unit.</summary>
    /// <remarks>In most cases, expressing the magnitudes in a specified <see cref="global::UnitOfLength"/> should be preferred. This is achieved through
    /// <see cref="InUnit(global::UnitOfLength)"/>.</remarks>
    public global::SharpMeasures.Vector3 Components => (X.Magnitude, Y.Magnitude, Z.Magnitude);

    /// <summary>Constructs a new <see cref="global::Position3"/> representing { <paramref name="x"/>, <paramref name="y"/>, <paramref name="z"/> }.</summary>
    /// <param name="x">The X-component of the constructed <see cref="global::Position3"/>.</param>
    /// <param name="y">The Y-component of the constructed <see cref="global::Position3"/>.</param>
    /// <param name="z">The Z-component of the constructed <see cref="global::Position3"/>.</param>
    /// <exception cref="global::System.ArgumentNullException"/>
    /// <exception cref="global::System.ArgumentNullException"/>
    public Position3(Length x, Length y, Length z)
    {
        global::System.ArgumentNullException.ThrowIfNull(x);
        global::System.ArgumentNullException.ThrowIfNull(y);
        global::System.ArgumentNullException.ThrowIfNull(z);

        X = x;
        Y = y;
        Z = z;
    }
    /// <summary>Constructs a new <see cref="global::Position3"/> representing { <paramref name="x"/>, <paramref name="y"/>, <paramref name="z"/> }, expressed in an arbitrary unit.</summary>
    /// <param name="x">The magnitude of the X-component of the constructed <see cref="global::Position3"/>, expressed in an arbitrary unit.</param>
    /// <param name="y">The magnitude of the Y-component of the constructed <see cref="global::Position3"/>, expressed in an arbitrary unit.</param>
    /// <param name="z">The magnitude of the Z-component of the constructed <see cref="global::Position3"/>, expressed in an arbitrary unit.</param>
    /// <remarks>Consider preferring construction through <see cref="global::Position3(global::SharpMeasures.Scalar, global::SharpMeasures.Scalar, global::SharpMeasures.Scalar, global::UnitOfLength)"/>,
    /// where the components are expressed in a specified <see cref="global::UnitOfLength"/>.</remarks>
    public Position3(global::SharpMeasures.Scalar x, global::SharpMeasures.Scalar y, global::SharpMeasures.Scalar z) : this(new Length(x), new Length(y), new Length(z)) { }

    /// <summary>Constructs a new <see cref="global::Position3"/> representing { <paramref name="components"/> }, expressed in an arbitrary unit.</summary>
    /// <param name="components">The magnitudes of the components of the constructed <see cref="global::Position3"/>, expressed in an arbitrary unit.</param>
    /// <remarks>Consider preferring construction through <see cref="global::Position3(global::SharpMeasures.Vector3, global::UnitOfLength)"/>,
    /// where the components are expressed in a specified <see cref="global::UnitOfLength"/>.</remarks>
    public Position3(global::SharpMeasures.Vector3 components) : this(components.X, components.Y, components.Z) { }

    /// <summary>Constructs a new <see cref="global::Position3"/> representing { <paramref name="x"/>, <paramref name="y"/>, <paramref name="z"/> }, when expressed in <paramref name="unitOfLength"/>.</summary>
    /// <param name="x">The magnitude of the X-component of the constructed <see cref="global::Position3"/>, expressed in <paramref name="unitOfLength"/>.</param>
    /// <param name="y">The magnitude of the Y-component of the constructed <see cref="global::Position3"/>, expressed in <paramref name="unitOfLength"/>.</param>
    /// <param name="z">The magnitude of the Z-component of the constructed <see cref="global::Position3"/>, expressed in <paramref name="unitOfLength"/>.</param>
    /// <param name="unitOfLength">The <see cref="global::UnitOfLength"/> in which the magnitudes of the components are expressed.</param>
    /// <exception cref="global::System.ArgumentNullException"/>
    public Position3(global::SharpMeasures.Scalar x, global::SharpMeasures.Scalar y, global::SharpMeasures.Scalar z, global::UnitOfLength unitOfLength) : this(ComputeRepresentedComponents(x, y, z, unitOfLength)) { }

    /// <summary>Constructs a new <see cref="global::Position3"/> representing { <paramref name="components"/> }, when expressed in <paramref name="unitOfLength"/>.</summary>
    /// <param name="components">The magnitudes of the components of the constructed <see cref="global::Position3"/>, expressed in <paramref name="unitOfLength"/>.</param>
    /// <param name="unitOfLength">The <see cref="global::UnitOfLength"/> in which <paramref name="components"/> is expressed.</param>
    public Position3(global::SharpMeasures.Vector3 components, global::UnitOfLength unitOfLength) : this(components.X, components.Y, components.Z, unitOfLength) { }

    /// <inheritdoc cref="global::SharpMeasures.IVector3Quantity.Magnitude()"/>
    public global::Length Magnitude() => ScalarMaths.Magnitude3(this);

    /// <inheritdoc/>
    global::SharpMeasures.Scalar global::SharpMeasures.IVector3Quantity.Magnitude() => PureScalarMaths.Magnitude3(this);

    /// <inheritdoc/>
    public global::SharpMeasures.Scalar SquaredMagnitude() => PureScalarMaths.SquaredMagnitude3(this);

    /// <summary>The magnitudes of the components of <see langword="this"/>, expressed in <paramref name="unitOfLength"/>.</summary>
    /// <param name="unitOfLength">The <see cref="global::UnitOfLength"/> in which the components of <see langword="this"/> are expressed.</param>
    /// <exception cref="global::System.ArgumentNullException"/>
    public global::SharpMeasures.Vector3 InUnit(global::UnitOfLength unitOfLength)
    {
        global::System.ArgumentNullException.ThrowIfNull(unitOfLength);

        return new(X.Magnitude.Value / unitOfLength.Length.Magnitude.Value, Y.Magnitude.Value / unitOfLength.Length.Magnitude.Value, Z.Magnitude.Value / unitOfLength.Length.Magnitude.Value);
    }

    /// <inheritdoc/>
    public global::Position3 Normalize() => VectorMaths.Normalize(this);

    /// <inheritdoc/>
    public global::Position3 Transform(global::System.Numerics.Matrix4x4 transform) => VectorMaths.Transform(this, transform);

    /// <summary>Formats the represented <see cref="Components"/> using the current culture.</summary>
    public override string ToString() => ToString(global::System.Globalization.CultureInfo.CurrentCulture);

    /// <summary>Formats the represented <see cref="Components"/> according to <paramref name="format"/>, using the current culture.</summary>
    public string ToString(string? format) => ToString(format, global::System.Globalization.CultureInfo.CurrentCulture);

    /// <summary>Formats the represented <see cref="Components"/> using the culture-specific formatting information provided by <paramref name="formatProvider"/>.</summary>
    public string ToString(global::System.IFormatProvider? formatProvider) => ToString("G", formatProvider);

    /// <summary>Formats the represented <see cref="Components"/> according to <paramref name="format"/>, using the culture-specific formatting information provided by <paramref name="formatProvider"/>.</summary>
    public string ToString(string? format, global::System.IFormatProvider? formatProvider) => Components.ToString(format, formatProvider);

    /// <summary>Deconstructs <see langword="this"/> into the components (<see cref="X"/>, <see cref="Y"/>, <see cref="Z"/>).</summary>
    /// <param name="x">The X-component of <see langword="this"/>.</param>
    /// <param name="y">The Y-component of <see langword="this"/>.</param>
    /// <param name="z">The Z-component of <see langword="this"/>.</param>
    public void Deconstruct(out Length x, out Length y, out Length z)
    {
        x = X;
        y = Y;
        z = Z;
    }

    /// <inheritdoc/>
    public bool Equals(global::Position3? other)
        => other is not null && X.Magnitude.Value == other.X.Magnitude.Value && Y.Magnitude.Value == other.Y.Magnitude.Value && Z.Magnitude.Value == other.Z.Magnitude.Value;

    /// <inheritdoc/>
    public override bool Equals(object? obj) => obj is Position3 other && Equals(other);

    /// <summary>Indicates whether <paramref name="lhs"/> and <paramref name="rhs"/> represent equivalent components.</summary>
    /// <param name="lhs">The left-hand side of the equality check.</param>
    /// <param name="rhs">The right-hand side of the equality check.</param>
    public static bool operator ==(global::Position3? lhs, global::Position3? rhs) => lhs?.Equals(rhs) ?? rhs is null;

    /// <summary>Indicates whether <paramref name="lhs"/> and <paramref name="rhs"/> represent inequivalent components.</summary>
    /// <param name="lhs">The left-hand side of the inequality check.</param>
    /// <param name="rhs">The right-hand side of the inequality check.</param>
    public static bool operator !=(global::Position3? lhs, global::Position3? rhs) => (lhs == rhs) is false;

    /// <inheritdoc/>
    public override int GetHashCode() => (X, Y, Z).GetHashCode();

    /// <summary>Constructs the <see cref="global::Position3"/> with the elements of <paramref name="components"/> as components.</summary>
    /// <exception cref="global::System.ArgumentNullException"/>
    [global::System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "CA2225", Justification = "Behaviour can be achived through a constructor")]
    public static implicit operator global::Position3((Length X, Length Y, Length Z) components)
    {
        global::System.ArgumentNullException.ThrowIfNull(components.X);
        global::System.ArgumentNullException.ThrowIfNull(components.Y);
        global::System.ArgumentNullException.ThrowIfNull(components.Z);

        return new(components.X, components.Y, components.Z);
    }

    /// <inheritdoc/>
    static global::Position3 global::SharpMeasures.IVector3Quantity<global::Position3>.WithComponents(global::SharpMeasures.Scalar x, global::SharpMeasures.Scalar y, global::SharpMeasures.Scalar z) => new(x, y, z);

    /// <inheritdoc/>
    static global::Position3 global::SharpMeasures.IVector3Quantity<global::Position3>.WithComponents(global::SharpMeasures.Vector3 components) => new(components);

    /// <summary>Computes the magnitudes of the represented components based on magnitudes expressed in a certain <paramref name="unitOfLength"/>.</summary>
    /// <param name="x">The magnitude of the X-component, expressed in a certain unit <paramref name="unitOfLength"/>.</param>
    /// <param name="y">The magnitude of the Y-component, expressed in a certain unit <paramref name="unitOfLength"/>.</param>
    /// <param name="z">The magnitude of the Z-component, expressed in a certain unit <paramref name="unitOfLength"/>.</param>
    /// <param name="unitOfLength">The global::UnitOfLength in which the magnitudes of the components are expressed.</param>
    /// <exception cref="global::System.ArgumentNullException"/>
    private static global::SharpMeasures.Vector3 ComputeRepresentedComponents(global::SharpMeasures.Scalar x, global::SharpMeasures.Scalar y, global::SharpMeasures.Scalar z, global::UnitOfLength unitOfLength)
    {
        global::System.ArgumentNullException.ThrowIfNull(unitOfLength);

        return (x * unitOfLength.Length.Magnitude, y * unitOfLength.Length.Magnitude, z * unitOfLength.Length.Magnitude);
    }

    /// <summary>Describes mathematical operations that result in a pure <see cref="global::SharpMeasures.Scalar"/>.</summary>
    private static global::SharpMeasures.Maths.IScalarResultingMaths<global::SharpMeasures.Scalar> PureScalarMaths { get; } = global::SharpMeasures.Maths.MathFactory.ScalarResult();

    /// <summary>Describes mathematical operations that result in <see cref="global::Length"/>.</summary>
    private static global::SharpMeasures.Maths.IScalarResultingMaths<global::Length> ScalarMaths { get; } = global::SharpMeasures.Maths.MathFactory.ScalarResult<global::Length>();

    /// <summary>Describes mathematical operations that result in <see cref="global::Position3"/>.</summary>
    private static global::SharpMeasures.Maths.IVector3ResultingMaths<global::Position3> VectorMaths { get; } = global::SharpMeasures.Maths.MathFactory.Vector3Result<global::Position3>();
}
