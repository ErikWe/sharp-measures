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

    /// <summary>Constructs a new <see cref="global::A"/>, derived from other quantities.</summary>
    /// <param name="b">This <see cref="global::B2"/> is used to derive a <see cref="global::A"/>.</param>
    /// <param name="c">This <see cref="global::C2"/> is used to derive a <see cref="global::A"/>.</param>
    /// <exception cref="global::System.ArgumentNullException"/>
    public static global::A From(global::B2 b, global::C2 c)
    {
        global::System.ArgumentNullException.ThrowIfNull(b);
        global::System.ArgumentNullException.ThrowIfNull(c);

        return new(PureScalarMaths.Dot2(b.Components, c.Components));
    }

    /// <summary>Computes { <paramref name="a"/> * <paramref name="b"/> }, resulting in a <see cref="global::B3"/>.</summary>
    /// <param name="a">The factor of { <paramref name="a"/> * <paramref name="b"/> }.</param>
    /// <param name="b">The factor of { <paramref name="a"/> * <paramref name="b"/> }.</param>
    /// <exception cref="global::System.ArgumentNullException"/>
    public static global::B3 operator *(global::A a, global::C3 b)
    {
        global::System.ArgumentNullException.ThrowIfNull(a);
        global::System.ArgumentNullException.ThrowIfNull(b);

        return new(a.Magnitude * b.Components);
    }

    /// <summary>Computes { <paramref name="a"/> * <paramref name="b"/> }, resulting in a <see cref="global::B2"/>.</summary>
    /// <param name="a">The factor of { <paramref name="a"/> * <paramref name="b"/> }.</param>
    /// <param name="b">The factor of { <paramref name="a"/> * <paramref name="b"/> }.</param>
    /// <exception cref="global::System.ArgumentNullException"/>
    public static global::B2 operator *(global::A a, global::C2 b)
    {
        global::System.ArgumentNullException.ThrowIfNull(a);
        global::System.ArgumentNullException.ThrowIfNull(b);

        return new(a.Magnitude * b.Components);
    }

    /// <summary>Describes mathematical operations that result in a pure <see cref="global::SharpMeasures.Scalar"/>.</summary>
    private static global::SharpMeasures.Maths.IScalarResultingMaths<global::SharpMeasures.Scalar> PureScalarMaths { get; } = global::SharpMeasures.Maths.MathFactory.ScalarResult();
}
