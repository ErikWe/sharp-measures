﻿//HintName: Position2.Common.g.cs
//----------------------
// <auto-generated>
//      This file was generated by SharpMeasures.Generators.Vectors <stamp>
//
//      Changes to this file may cause incorrect behavior and will be lost if the code is regenerated.
// </auto-generated>
//----------------------

#nullable enable

/// <summary>A measure of the 2-dimensional vector quantity Position, expressed in <see cref="global::UnitOfLength"/>.</summary>
public partial class Position2 :
    global::SharpMeasures.IVector2Quantity<global::Position2>,
    global::System.IEquatable<global::Position2>
{

    /// <summary>The <see cref="global::Position2"/> representing { 0, 0 }.</summary>
    public static global::Position2 Zero { get; } = new(0, 0);

    /// <summary>The X-component of <see langword="this"/>.</summary>
    public global::Length X { get; }
    /// <summary>The Y-component of <see langword="this"/>.</summary>
    public global::Length Y { get; }

    /// <summary>The magnitude of the X-component of <see langword="this"/>.</summary>
    global::SharpMeasures.Scalar global::SharpMeasures.IVector2Quantity.X => X.Magnitude;
    /// <summary>The magnitude of the Y-component of <see langword="this"/>.</summary>
    global::SharpMeasures.Scalar global::SharpMeasures.IVector2Quantity.Y => Y.Magnitude;

    /// <summary>The magnitudes of the components of <see langword="this"/>, expressed in an arbitrary unit.</summary>
    /// <remarks>In most cases, expressing the magnitudes in a specified <see cref="global::UnitOfLength"/> should be preferred. This is achieved through
    /// <see cref="InUnit(global::UnitOfLength)"/>.</remarks>
    public global::SharpMeasures.Vector2 Components => (X.Magnitude, Y.Magnitude);

    /// <summary>Constructs a new <see cref="global::Position2"/> representing { <paramref name="x"/>, <paramref name="y"/> }.</summary>
    /// <param name="x">The X-component of the constructed <see cref="global::Position2"/>.</param>
    /// <param name="y">The Y-component of the constructed <see cref="global::Position2"/>.</param>
    /// <exception cref="global::System.ArgumentNullException"/>
    /// <exception cref="global::System.ArgumentNullException"/>
    public Position2(Length x, Length y)
    {
        global::System.ArgumentNullException.ThrowIfNull(x);
        global::System.ArgumentNullException.ThrowIfNull(y);

        X = x;
        Y = y;
    }
    /// <summary>Constructs a new <see cref="global::Position2"/> representing { <paramref name="x"/>, <paramref name="y"/> }, expressed in an arbitrary unit.</summary>
    /// <param name="x">The magnitude of the X-component of the constructed <see cref="global::Position2"/>, expressed in an arbitrary unit.</param>
    /// <param name="y">The magnitude of the Y-component of the constructed <see cref="global::Position2"/>, expressed in an arbitrary unit.</param>
    /// <remarks>Consider preferring construction through <see cref="global::Position2(global::SharpMeasures.Scalar, global::SharpMeasures.Scalar, global::UnitOfLength)"/>,
    /// where the components are expressed in a specified <see cref="global::UnitOfLength"/>.</remarks>
    public Position2(global::SharpMeasures.Scalar x, global::SharpMeasures.Scalar y)
        : this(new Length(x), new Length(y)) { }

    /// <summary>Constructs a new <see cref="global::Position2"/> representing { <paramref name="components"/> }, expressed in an arbitrary unit.</summary>
    /// <param name="components">The magnitudes of the components of the constructed <see cref="global::Position2"/>, expressed in an arbitrary unit.</param>
    /// <remarks>Consider preferring construction through <see cref="global::Position2(global::SharpMeasures.Vector2, global::UnitOfLength)"/>,
    /// where the components are expressed in a specified <see cref="global::UnitOfLength"/>.</remarks>
    public Position2(global::SharpMeasures.Vector2 components)
        : this(components.X, components.Y) { }

    /// <summary>Constructs a new <see cref="global::Position2"/> representing { <paramref name="x"/>, <paramref name="y"/> }, when expressed in <paramref name="unitOfLength"/>.</summary>
    /// <param name="x">The magnitude of the X-component of the constructed <see cref="global::Position2"/>, expressed in <paramref name="unitOfLength"/>.</param>
    /// <param name="y">The magnitude of the Y-component of the constructed <see cref="global::Position2"/>, expressed in <paramref name="unitOfLength"/>.</param>
    /// <param name="unitOfLength">The <see cref="global::UnitOfLength"/> in which the magnitudes of the components are expressed.</param>
    /// <exception cref="global::System.ArgumentNullException"/>
    public Position2(global::SharpMeasures.Scalar x, global::SharpMeasures.Scalar y, global::UnitOfLength unitOfLength)
        : this(ComputeRepresentedComponents(x, y, unitOfLength)) { }

    /// <summary>Constructs a new <see cref="global::Position2"/> representing { <paramref name="components"/> }, when expressed in <paramref name="unitOfLength"/>.</summary>
    /// <param name="components">The magnitudes of the components of the constructed <see cref="global::Position2"/>, expressed in <paramref name="unitOfLength"/>.</param>
    /// <param name="unitOfLength">The <see cref="global::UnitOfLength"/> in which <paramref name="components"/> is expressed.</param>
    public Position2(global::SharpMeasures.Vector2 components, global::UnitOfLength unitOfLength)
        : this(components.X, components.Y, unitOfLength) { }

    /// <inheritdoc cref="global::SharpMeasures.IVector2Quantity.Magnitude()"/>
    public global::Length Magnitude() => ScalarMaths.Magnitude2(this);

    /// <inheritdoc/>
    global::SharpMeasures.Scalar global::SharpMeasures.IVector2Quantity.Magnitude() => PureScalarMaths.Magnitude2(this);

    /// <inheritdoc/>
    public global::SharpMeasures.Scalar SquaredMagnitude() => PureScalarMaths.SquaredMagnitude2(this);

    /// <summary>The magnitudes of the components of <see langword="this"/>, expressed in <paramref name="unitOfLength"/>.</summary>
    /// <param name="unitOfLength">The <see cref="global::UnitOfLength"/> in which the components of <see langword="this"/> are expressed.</param>
    /// <exception cref="global::System.ArgumentNullException"/>
    public global::SharpMeasures.Vector2 InUnit(global::UnitOfLength unitOfLength)
    {
        global::System.ArgumentNullException.ThrowIfNull(unitOfLength);

        return new(X.Magnitude.Value / unitOfLength.Length.Magnitude.Value, Y.Magnitude.Value / unitOfLength.Length.Magnitude.Value);
    }

    /// <inheritdoc/>
    public global::Position2 Normalize() => VectorMaths.Normalize(this);

    /// <summary>Produces a description of <see langword="this"/> containing the components expressed in an arbitrary unit.</summary>
    public override string ToString() => Components.ToString();

    /// <summary>Deconstructs <see langword="this"/> into the individual components.</summary>
    /// <param name="x">The X-component of <see langword="this"/>.</param>
    /// <param name="y">The Y-component of <see langword="this"/>.</param>
    public void Deconstruct(out Length x, out Length y)
    {
        x = X;
        y = Y;
    }

    /// <inheritdoc/>
    public bool Equals(global::Position2? other)
        => other is not null && X.Magnitude.Value == other.X.Magnitude.Value && Y.Magnitude.Value == other.Y.Magnitude.Value;

    /// <inheritdoc/>
    public override bool Equals(object? obj) => obj is Position2 other && Equals(other);

    /// <summary>Indicates whether <paramref name="lhs"/> and <paramref name="rhs"/> represent equivalent components.</summary>
    /// <param name="lhs">The left-hand side of the equality check.</param>
    /// <param name="rhs">The right-hand side of the equality check.</param>
    public static bool operator ==(global::Position2? lhs, global::Position2? rhs) => lhs?.Equals(rhs) ?? rhs is null;

    /// <summary>Indicates whether <paramref name="lhs"/> and <paramref name="rhs"/> represent inequivalent components.</summary>
    /// <param name="lhs">The left-hand side of the inequality check.</param>
    /// <param name="rhs">The right-hand side of the inequality check.</param>
    public static bool operator !=(global::Position2? lhs, global::Position2? rhs) => (lhs == rhs) is false;

    /// <inheritdoc/>
    public override int GetHashCode() => (X, Y).GetHashCode();

    /// <summary>Constructs the <see cref="global::Position2"/> with the elements of <paramref name="components"/> as components.</summary>
    /// <exception cref="global::System.ArgumentNullException"/>
    [global::System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "CA2225", Justification = "Behaviour can be achived through a constructor")]
    public static implicit operator global::Position2((Length X, Length Y) components)
    {
        global::System.ArgumentNullException.ThrowIfNull(components.X);
        global::System.ArgumentNullException.ThrowIfNull(components.Y);

        return new(components.X, components.Y);
    }

    /// <inheritdoc/>
    static global::Position2 global::SharpMeasures.IVector2Quantity<global::Position2>.WithComponents(global::SharpMeasures.Scalar x, global::SharpMeasures.Scalar y) => new(x, y);

    /// <inheritdoc/>
    static global::Position2 global::SharpMeasures.IVector2Quantity<global::Position2>.WithComponents(global::SharpMeasures.Vector2 components) => new(components);

    /// <summary>Computes the magnitudes of the represented components based on magnitudes expressed in a certain <paramref name="unitOfLength"/>.</summary>
    /// <param name="x">The magnitude of the X-component, expressed in a certain unit <paramref name="unitOfLength"/>.</param>
    /// <param name="y">The magnitude of the Y-component, expressed in a certain unit <paramref name="unitOfLength"/>.</param>
    /// <param name="unitOfLength">The global::UnitOfLength in which the magnitudes of the components are expressed.</param>
    /// <exception cref="global::System.ArgumentNullException"/>
    private static global::SharpMeasures.Vector2 ComputeRepresentedComponents(global::SharpMeasures.Scalar x, global::SharpMeasures.Scalar y, global::UnitOfLength unitOfLength)
    {
        global::System.ArgumentNullException.ThrowIfNull(unitOfLength);

        return (x * unitOfLength.Length.Magnitude, y * unitOfLength.Length.Magnitude);
    }
}
