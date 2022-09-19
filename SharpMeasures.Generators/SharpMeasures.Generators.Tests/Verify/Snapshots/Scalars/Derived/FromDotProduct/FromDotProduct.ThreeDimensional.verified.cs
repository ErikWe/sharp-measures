﻿//HintName: A.Derivations.g.cs
//----------------------
// <auto-generated>
//      This file was generated by SharpMeasures.Generators.Scalars <stamp>
//
//      Changes to this file may cause incorrect behavior and will be lost if the code is regenerated.
// </auto-generated>
//----------------------

#nullable enable

public partial class A
{
    /// <summary>Constructs a new <see cref="global::A"/>, derived from other quantities.</summary>
    /// <param name="b">This <see cref="global::B3"/> is used to derive a <see cref="global::A"/>.</param>
    /// <param name="c">This <see cref="global::C3"/> is used to derive a <see cref="global::A"/>.</param>
    /// <exception cref="global::System.ArgumentNullException"/>
    public static global::A From(global::B3 b, global::C3 c)
    {
        global::System.ArgumentNullException.ThrowIfNull(b);
        global::System.ArgumentNullException.ThrowIfNull(c);

        return new(PureScalarMaths.Dot3(b.Components, c.Components));
    }

    /// <summary>Describes mathematical operations that result in a pure <see cref="global::SharpMeasures.Scalar"/>.</summary>
    private static global::SharpMeasures.Maths.IScalarResultingMaths<global::SharpMeasures.Scalar> PureScalarMaths { get; } = global::SharpMeasures.Maths.MathFactory.ScalarResult();
}
