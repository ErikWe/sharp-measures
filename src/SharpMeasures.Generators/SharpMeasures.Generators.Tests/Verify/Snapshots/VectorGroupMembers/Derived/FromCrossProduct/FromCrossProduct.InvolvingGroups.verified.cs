﻿//HintName: D3.Derivations.g.cs
//----------------------
// <auto-generated>
//      This file was generated by SharpMeasures.Generators.Vectors <stamp>
//
//      Changes to this file may cause incorrect behavior and will be lost if the code is regenerated.
// </auto-generated>
//----------------------

#nullable enable

public partial class D3
{
    /// <summary>Constructs a new <see cref="global::D3"/>, derived from other quantities.</summary>
    /// <param name="b">This <see cref="global::B3"/> is used to derive a <see cref="global::D3"/>.</param>
    /// <param name="c">This <see cref="global::C3"/> is used to derive a <see cref="global::D3"/>.</param>
    /// <exception cref="global::System.ArgumentNullException"/>
    public static global::D3 From(global::B3 b, global::C3 c)
    {
        global::System.ArgumentNullException.ThrowIfNull(b);
        global::System.ArgumentNullException.ThrowIfNull(c);

        return new(PureVector3Maths.Cross(b.Components, c.Components));
    }

    /// <summary>Describes mathematical operations that result in a pure <see cref="global::SharpMeasures.Vector3"/>.</summary>
    private static global::SharpMeasures.Maths.IVector3ResultingMaths<global::SharpMeasures.Vector3> PureVector3Maths { get; } = global::SharpMeasures.Maths.MathFactory.Vector3Result();
}
