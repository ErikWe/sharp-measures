﻿//HintName: Length.Maths.g.cs
//----------------------
// <auto-generated>
//      This file was generated by SharpMeasures.Generators.Scalars <stamp>
//
//      Changes to this file may cause incorrect behavior and will be lost if the code is regenerated.
// </auto-generated>
//----------------------

#nullable enable

public partial class Length
{
    /// <inheritdoc cref="global::SharpMeasures.Scalar.IsNaN"/>
    public bool IsNaN => double.IsNaN(Magnitude.Value);
    /// <inheritdoc cref="global::SharpMeasures.Scalar.IsZero"/>
    public bool IsZero => Magnitude.Value is 0;
    /// <inheritdoc cref="global::SharpMeasures.Scalar.IsPositive"/>
    public bool IsPositive => Magnitude.Value > 0;
    /// <inheritdoc cref="global::SharpMeasures.Scalar.IsNegative"/>
    public bool IsNegative => Magnitude.Value < 0;
    /// <inheritdoc cref="global::SharpMeasures.Scalar.IsFinite"/>
    public bool IsFinite => double.IsFinite(Magnitude.Value);
    /// <inheritdoc cref="global::SharpMeasures.Scalar.IsInfinite"/>
    public bool IsInfinite => double.IsInfinity(Magnitude.Value);
    /// <inheritdoc cref="global::SharpMeasures.Scalar.IsPositiveInfinity"/>
    public bool IsPositiveInfinity => double.IsPositiveInfinity(Magnitude.Value);
    /// <inheritdoc cref="global::SharpMeasures.Scalar.IsNegativeInfinity"/>
    public bool IsNegativeInfinity => double.IsNegativeInfinity(Magnitude.Value);

    /// <inheritdoc cref="global::SharpMeasures.Scalar.Absolute"/>
    public global::Length Absolute() => new(global::System.Math.Abs(Magnitude.Value));

    /// <inheritdoc cref="global::SharpMeasures.Scalar.Sign"/>
    public int Sign() => global::System.Math.Sign(Magnitude.Value);

    /// <inheritdoc cref="global::SharpMeasures.Scalar.Add(global::SharpMeasures.Scalar)"/>
    /// <exception cref="global::System.ArgumentNullException"/>
    public global::Length Add(global::Length addend)
    {
        global::System.ArgumentNullException.ThrowIfNull(addend);

        return new(Magnitude.Value + addend.Magnitude.Value);
    }

    /// <inheritdoc cref="global::SharpMeasures.Scalar.Add(global::SharpMeasures.Scalar)"/>
    /// <exception cref="global::System.ArgumentNullException"/>
    public global::Distance Subtract(global::Length subtrahend)
    {
        global::System.ArgumentNullException.ThrowIfNull(subtrahend);

        return new(Magnitude.Value - subtrahend.Magnitude.Value);
    }

    /// <inheritdoc cref="global::SharpMeasures.Scalar.Add(global::SharpMeasures.Scalar)"/>
    /// <exception cref="global::System.ArgumentNullException"/>
    public global::Length Add(global::Distance addend)
    {
        global::System.ArgumentNullException.ThrowIfNull(addend);

        return new(Magnitude.Value + addend.Magnitude.Value);
    }

    /// <inheritdoc cref="global::SharpMeasures.Scalar.Subtract(global::SharpMeasures.Scalar)"/>
    /// <exception cref="global::System.ArgumentNullException"/>
    public global::Length Subtract(global::Distance subtrahend)
    {
        global::System.ArgumentNullException.ThrowIfNull(subtrahend);

        return new(Magnitude.Value - subtrahend.Magnitude.Value);
    }

    /// <inheritdoc/>
    public global::Length Plus() => this;
    /// <inheritdoc/>
    public global::Length Negate() => new(-Magnitude.Value);

    /// <inheritdoc/>
    public global::Length Multiply(global::SharpMeasures.Scalar factor) => new(Magnitude.Value * factor.Value);
    /// <inheritdoc/>
    public global::Length Divide(global::SharpMeasures.Scalar divisor) => new(Magnitude.Value / divisor.Value);

    /// <inheritdoc cref="global::SharpMeasures.Scalar.Divide(global::SharpMeasures.Scalar)"/>
    /// <exception cref="global::System.ArgumentNullException"/>
    public global::SharpMeasures.Scalar Divide(global::Length divisor)
    {
        global::System.ArgumentNullException.ThrowIfNull(divisor);

        return new(Magnitude.Value / divisor.Magnitude.Value);
    }

    /// <summary>Computes { <see langword="this"/> + <paramref name="addend"/> }.</summary>
    /// <typeparam name="TScalar">The type of <paramref name="addend"/>.</typeparam>
    /// <param name="addend">The second term of { <see langword="this"/> + <paramref name="addend"/> }.</param>
    /// <exception cref="global::System.ArgumentNullException"/>
    public global::SharpMeasures.Unhandled Add<TScalar>(TScalar addend) where TScalar : global::SharpMeasures.IScalarQuantity
    {
        global::System.ArgumentNullException.ThrowIfNull(addend);

        return new(Magnitude + addend.Magnitude);
    }

    /// <summary>Computes { <see langword="this"/> - <paramref name="subtrahend"/> }.</summary>
    /// <typeparam name="TScalar">The type of <paramref name="subtrahend"/>.</typeparam>
    /// <param name="subtrahend">The second term of { <see langword="this"/> - <paramref name="subtrahend"/> }.</param>
    /// <exception cref="global::System.ArgumentNullException"/>
    public global::SharpMeasures.Unhandled Subtract<TScalar>(TScalar subtrahend) where TScalar : global::SharpMeasures.IScalarQuantity
    {
        global::System.ArgumentNullException.ThrowIfNull(subtrahend);

        return new(Magnitude - subtrahend.Magnitude);
    }

    /// <summary>Computes { <paramref name="minuend"/> - <see langword="this"/> }.</summary>
    /// <typeparam name="TScalar">The type of <paramref name="minuend"/>.</typeparam>
    /// <param name="minuend">The first term of { <paramref name="minuend"/> - <see langword="this"/> }.</param>
    /// <exception cref="global::System.ArgumentNullException"/>
    public global::SharpMeasures.Unhandled SubtractFrom<TScalar>(TScalar minuend) where TScalar : global::SharpMeasures.IScalarQuantity
    {
        global::System.ArgumentNullException.ThrowIfNull(minuend);

        return new(minuend.Magnitude - Magnitude);
    }

    /// <summary>Computes { <see langword="this"/> * <paramref name="factor"/> }.</summary>
    /// <typeparam name="TScalar">The type of <paramref name="factor"/>.</typeparam>
    /// <param name="factor">The second factor of { <see langword="this"/> * <paramref name="factor"/> }.</param>
    /// <exception cref="global::System.ArgumentNullException"/>
    public global::SharpMeasures.Unhandled Multiply<TScalar>(TScalar factor) where TScalar : global::SharpMeasures.IScalarQuantity
    {
        global::System.ArgumentNullException.ThrowIfNull(factor);

        return new(Magnitude * factor.Magnitude);
    }

    /// <summary>Computes { <see langword="this"/> / <paramref name="divisor"/> }.</summary>
    /// <typeparam name="TScalar">The type of <paramref name="divisor"/>.</typeparam>
    /// <param name="divisor">The divisor of { <see langword="this"/> / <paramref name="divisor"/> }.</param>
    /// <exception cref="global::System.ArgumentNullException"/>
    public global::SharpMeasures.Unhandled Divide<TScalar>(TScalar divisor) where TScalar : global::SharpMeasures.IScalarQuantity
    {
        global::System.ArgumentNullException.ThrowIfNull(divisor);

        return new(Magnitude / divisor.Magnitude);
    }

    /// <summary>Computes { <paramref name="dividend"/> / <see langword="this"/> }.</summary>
    /// <typeparam name="TScalar">The type of <paramref name="dividend"/>.</typeparam>
    /// <param name="dividend">The dividend of { <paramref name="dividend"/> / <see langword="this"/> }.</param>
    /// <exception cref="global::System.ArgumentNullException"/>
    public global::SharpMeasures.Unhandled DivideInto<TScalar>(TScalar dividend) where TScalar : global::SharpMeasures.IScalarQuantity
    {
        global::System.ArgumentNullException.ThrowIfNull(dividend);

        return new(dividend.Magnitude / Magnitude);
    }

    /// <inheritdoc/>
    public static global::Length operator +(global::Length x) => x;

    /// <inheritdoc/>
    /// <exception cref="global::System.ArgumentNullException"/>
    public static global::Length operator -(global::Length x)
    {
        global::System.ArgumentNullException.ThrowIfNull(x);

        return new(-x.Magnitude.Value);
    }

    /// <inheritdoc cref="global::SharpMeasures.Scalar.operator +(global::SharpMeasures.Scalar, global::SharpMeasures.Scalar)"/>
    /// <exception cref="global::System.ArgumentNullException"/>
    public static global::Length operator +(global::Length x, global::Length y)
    {
        global::System.ArgumentNullException.ThrowIfNull(x);
        global::System.ArgumentNullException.ThrowIfNull(y);

        return new(x.Magnitude.Value + y.Magnitude.Value);
    }

    /// <inheritdoc cref="global::SharpMeasures.Scalar.operator -(global::SharpMeasures.Scalar, global::SharpMeasures.Scalar)"/>
    /// <exception cref="global::System.ArgumentNullException"/>
    public static global::Distance operator -(global::Length x, global::Length y)
    {
        global::System.ArgumentNullException.ThrowIfNull(x);
        global::System.ArgumentNullException.ThrowIfNull(y);

        return new(x.Magnitude.Value - y.Magnitude.Value);
    }

    /// <inheritdoc cref="global::SharpMeasures.Scalar.operator +(global::SharpMeasures.Scalar, global::SharpMeasures.Scalar)"/>
    /// <exception cref="global::System.ArgumentNullException"/>
    public static global::Length operator +(global::Length x, global::Distance y)
    {
        global::System.ArgumentNullException.ThrowIfNull(x);
        global::System.ArgumentNullException.ThrowIfNull(y);

        return new(x.Magnitude.Value + y.Magnitude.Value);
    }

    /// <inheritdoc cref="global::SharpMeasures.Scalar.operator +(global::SharpMeasures.Scalar, global::SharpMeasures.Scalar)"/>
    /// <exception cref="global::System.ArgumentNullException"/>
    public static global::Length operator +(global::Distance x, global::Length y)
    {
        global::System.ArgumentNullException.ThrowIfNull(x);
        global::System.ArgumentNullException.ThrowIfNull(y);

        return new(x.Magnitude.Value + y.Magnitude.Value);
    }

    /// <inheritdoc cref="global::SharpMeasures.Scalar.operator -(global::SharpMeasures.Scalar, global::SharpMeasures.Scalar)"/>
    /// <exception cref="global::System.ArgumentNullException"/>
    public static global::Length operator -(global::Length x, global::Distance y)
    {
        global::System.ArgumentNullException.ThrowIfNull(x);
        global::System.ArgumentNullException.ThrowIfNull(y);

        return new(x.Magnitude.Value - y.Magnitude.Value);
    }

    /// <inheritdoc cref="global::SharpMeasures.Scalar.operator /(global::SharpMeasures.Scalar, global::SharpMeasures.Scalar)"/>
    /// <exception cref="global::System.ArgumentNullException"/>
    public static global::SharpMeasures.Scalar operator /(global::Length x, global::Length y)
    {
        global::System.ArgumentNullException.ThrowIfNull(x);
        global::System.ArgumentNullException.ThrowIfNull(y);

        return new(x.Magnitude.Value / y.Magnitude.Value);
    }

    /// <inheritdoc/>
    /// <exception cref="global::System.ArgumentNullException"/>
    public static global::Length operator *(global::Length x, global::SharpMeasures.Scalar y)
    {
        global::System.ArgumentNullException.ThrowIfNull(x);

        return new(x.Magnitude.Value * y.Value);
    }

    /// <inheritdoc/>
    /// <exception cref="global::System.ArgumentNullException"/>
    public static global::Length operator *(global::SharpMeasures.Scalar x, global::Length y)
    {
        global::System.ArgumentNullException.ThrowIfNull(y);

        return new(x.Value * y.Magnitude.Value);
    }

    /// <inheritdoc cref="global::SharpMeasures.Scalar.operator /(global::SharpMeasures.Scalar, global::SharpMeasures.Scalar)"/>
    /// <exception cref="global::System.ArgumentNullException"/>
    public static global::Length operator /(global::Length x, global::SharpMeasures.Scalar y)
    {
        global::System.ArgumentNullException.ThrowIfNull(x);

        return new(x.Magnitude.Value / y.Value);
    }

    /// <summary>Computes { <paramref name="x"/> + <paramref name="y"/> }.</summary>
    /// <param name="x">The first term of { <paramref name="x"/> + <paramref name="y"/> }.</param>
    /// <param name="y">The second term of { <paramref name="x"/> + <paramref name="y"/> }.</param>
    /// <remarks>Consider preferring <see cref="Add{TScalar}(TScalar)"/>, where boxing is avoided.</remarks>
    /// <exception cref="global::System.ArgumentNullException"/>
    public static global::SharpMeasures.Unhandled operator +(global::Length x, global::SharpMeasures.IScalarQuantity y)
    {
        global::System.ArgumentNullException.ThrowIfNull(x);
        global::System.ArgumentNullException.ThrowIfNull(y);

        return new(x.Magnitude + y.Magnitude);
    }

    /// <summary>Computes { <paramref name="x"/> - <paramref name="y"/> }.</summary>
    /// <param name="x">The first term of { <paramref name="x"/> - <paramref name="y"/> }.</param>
    /// <param name="y">The second term of { <paramref name="x"/> - <paramref name="y"/> }.</param>
    /// <remarks>Consider preferring <see cref="Subtract{TScalar}(TScalar)"/>, where boxing is avoided.</remarks>
    /// <exception cref="global::System.ArgumentNullException"/>
    public static global::SharpMeasures.Unhandled operator -(global::Length x, global::SharpMeasures.IScalarQuantity y)
    {
        global::System.ArgumentNullException.ThrowIfNull(x);
        global::System.ArgumentNullException.ThrowIfNull(y);

        return new(x.Magnitude - y.Magnitude);
    }

    /// <summary>Computes { <paramref name="x"/> * <paramref name="y"/> }.</summary>
    /// <param name="x">The first factor of { <paramref name="x"/> * <paramref name="y"/> }.</param>
    /// <param name="y">The second factor of { <paramref name="x"/> * <paramref name="y"/> }.</param>
    /// <remarks>Consider preferring <see cref="Multiply{TScalar}(TScalar)"/>, where boxing is avoided.</remarks>
    /// <exception cref="global::System.ArgumentNullException"/>
    public static global::SharpMeasures.Unhandled operator *(global::Length x, global::SharpMeasures.IScalarQuantity y)
    {
        global::System.ArgumentNullException.ThrowIfNull(x);
        global::System.ArgumentNullException.ThrowIfNull(y);

        return new(x.Magnitude * y.Magnitude);
    }

    /// <summary>Computes { <paramref name="x"/> / <paramref name="y"/> }.</summary>
    /// <param name="x">The dividend of { <paramref name="x"/> / <paramref name="y"/> }.</param>
    /// <param name="y">The divisor of { <paramref name="x"/> / <paramref name="y"/> }.</param>
    /// <remarks>Consider preferring <see cref="Divide{TScalar}(TScalar)"/>, where boxing is avoided.</remarks>
    /// <exception cref="global::System.ArgumentNullException"/>
    public static global::SharpMeasures.Unhandled operator /(global::Length x, global::SharpMeasures.IScalarQuantity y)
    {
        global::System.ArgumentNullException.ThrowIfNull(x);
        global::System.ArgumentNullException.ThrowIfNull(y);

        return new(x.Magnitude / y.Magnitude);
    }

    /// <inheritdoc cref="global::SharpMeasures.Scalar.operator +(global::SharpMeasures.Scalar, global::SharpMeasures.Scalar)"/>
    /// <exception cref="global::System.ArgumentNullException"/>
    public static global::SharpMeasures.Unhandled operator +(global::Length x, global::SharpMeasures.Unhandled y)
    {
        global::System.ArgumentNullException.ThrowIfNull(x);

        return new(x.Magnitude + y.Magnitude);
    }

    /// <inheritdoc cref="global::SharpMeasures.Scalar.operator +(global::SharpMeasures.Scalar, global::SharpMeasures.Scalar)"/>
    /// <exception cref="global::System.ArgumentNullException"/>
    public static global::SharpMeasures.Unhandled operator +(global::SharpMeasures.Unhandled x, global::Length y)
    {
        global::System.ArgumentNullException.ThrowIfNull(y);

        return new(x.Magnitude + y.Magnitude);
    }

    /// <inheritdoc cref="global::SharpMeasures.Scalar.operator -(global::SharpMeasures.Scalar, global::SharpMeasures.Scalar)"/>
    /// <exception cref="global::System.ArgumentNullException"/>
    public static global::SharpMeasures.Unhandled operator -(global::Length x, global::SharpMeasures.Unhandled y)
    {
        global::System.ArgumentNullException.ThrowIfNull(x);

        return new(x.Magnitude - y.Magnitude);
    }

    /// <inheritdoc cref="global::SharpMeasures.Scalar.operator -(global::SharpMeasures.Scalar, global::SharpMeasures.Scalar)"/>
    /// <exception cref="global::System.ArgumentNullException"/>
    public static global::SharpMeasures.Unhandled operator -(global::SharpMeasures.Unhandled x, global::Length y)
    {
        global::System.ArgumentNullException.ThrowIfNull(y);

        return new(x.Magnitude - y.Magnitude);
    }

    /// <inheritdoc cref="global::SharpMeasures.Scalar.operator *(global::SharpMeasures.Scalar, global::SharpMeasures.Scalar)"/>
    /// <exception cref="global::System.ArgumentNullException"/>
    public static global::SharpMeasures.Unhandled operator *(global::Length x, global::SharpMeasures.Unhandled y)
    {
        global::System.ArgumentNullException.ThrowIfNull(x);

        return new(x.Magnitude * y.Magnitude);
    }

    /// <inheritdoc cref="global::SharpMeasures.Scalar.operator *(global::SharpMeasures.Scalar, global::SharpMeasures.Scalar)"/>
    /// <exception cref="global::System.ArgumentNullException"/>
    public static global::SharpMeasures.Unhandled operator *(global::SharpMeasures.Unhandled x, global::Length y)
    {
        global::System.ArgumentNullException.ThrowIfNull(y);

        return new(x.Magnitude * y.Magnitude);
    }

    /// <inheritdoc cref="global::SharpMeasures.Scalar.operator /(global::SharpMeasures.Scalar, global::SharpMeasures.Scalar)"/>
    /// <exception cref="global::System.ArgumentNullException"/>
    public static global::SharpMeasures.Unhandled operator /(global::Length x, global::SharpMeasures.Unhandled y)
    {
        global::System.ArgumentNullException.ThrowIfNull(x);

        return new(x.Magnitude / y.Magnitude);
    }

    /// <inheritdoc cref="global::SharpMeasures.Scalar.operator /(global::SharpMeasures.Scalar, global::SharpMeasures.Scalar)"/>
    /// <exception cref="global::System.ArgumentNullException"/>
    public static global::SharpMeasures.Unhandled operator /(global::SharpMeasures.Unhandled x, global::Length y)
    {
        global::System.ArgumentNullException.ThrowIfNull(y);

        return new(x.Magnitude / y.Magnitude);
    }
}
