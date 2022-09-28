﻿//HintName: Position.MemberFactory.g.cs
//----------------------
// <auto-generated>
//      This file was generated by SharpMeasures.Generators.Vectors <stamp>
//
//      Changes to this file may cause incorrect behavior and will be lost if the code is regenerated.
// </auto-generated>
//----------------------

#nullable enable

/// <summary>Root of a group of vector quantities, each expressed in <see cref="global::UnitOfLength"/>.</summary>
public partial class Position
{
    /// <summary>Constructs a new <see cref="global::Position3"/> representing { <paramref name="x"/>, <paramref name="y"/>, <paramref name="z"/> }, expressed in an arbitrary unit.</summary>
    /// <param name="x">The magnitude of the X-component of the constructed <see cref="global::Position3"/>, expressed in <paramref name="unitOfLength"/>.</param>
    /// <param name="y">The magnitude of the Y-component of the constructed <see cref="global::Position3"/>, expressed in <paramref name="unitOfLength"/>.</param>
    /// <param name="z">The magnitude of the Z-component of the constructed <see cref="global::Position3"/>, expressed in <paramref name="unitOfLength"/>.</param>
    /// <param name="unitOfLength">The <see cref="global::UnitOfLength"/> in which the magnitudes of the components are expressed.</param>
    public static global::Position3 Create(global::SharpMeasures.Scalar x, global::SharpMeasures.Scalar y, global::SharpMeasures.Scalar z, global::UnitOfLength unitOfLength) => new(x, y, z, unitOfLength);

    /// <summary>Constructs a new <see cref="global::Position3"/> representing { <paramref name="components"/> [<paramref name="unitOfLength"/>] }.</summary>
    /// <param name="components">The magnitudes of the components of the constructed <see cref="global::Position3"/>, expressed in <paramref name="unitOfLength"/>.</param>
    /// <param name="unitOfLength">The <see cref="global::UnitOfLength"/> in which <paramref name="components"/> is expressed.</param>
    public static global::Position3 Create(global::SharpMeasures.Vector3 components, global::UnitOfLength unitOfLength) => new(components, unitOfLength);

    /// <summary>Constructs a new <see cref="global::Position2"/> representing { <paramref name="x"/>, <paramref name="y"/> }, expressed in an arbitrary unit.</summary>
    /// <param name="x">The magnitude of the X-component of the constructed <see cref="global::Position2"/>, expressed in <paramref name="unitOfLength"/>.</param>
    /// <param name="y">The magnitude of the Y-component of the constructed <see cref="global::Position2"/>, expressed in <paramref name="unitOfLength"/>.</param>
    /// <param name="unitOfLength">The <see cref="global::UnitOfLength"/> in which the magnitudes of the components are expressed.</param>
    public static global::Position2 Create(global::SharpMeasures.Scalar x, global::SharpMeasures.Scalar y, global::UnitOfLength unitOfLength) => new(x, y, unitOfLength);

    /// <summary>Constructs a new <see cref="global::Position2"/> representing { <paramref name="components"/> [<paramref name="unitOfLength"/>] }.</summary>
    /// <param name="components">The magnitudes of the components of the constructed <see cref="global::Position2"/>, expressed in <paramref name="unitOfLength"/>.</param>
    /// <param name="unitOfLength">The <see cref="global::UnitOfLength"/> in which <paramref name="components"/> is expressed.</param>
    public static global::Position2 Create(global::SharpMeasures.Vector2 components, global::UnitOfLength unitOfLength) => new(components, unitOfLength);
}
