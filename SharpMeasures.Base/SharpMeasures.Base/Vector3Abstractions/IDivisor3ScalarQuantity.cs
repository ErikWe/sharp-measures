namespace SharpMeasures.Vector3Abstractions;

using System.Diagnostics.CodeAnalysis;

/// <summary>Describes a scalar quantity that supports the operation { <typeparamref name="TDividend"/> / <see langword="this"/> }, resulting
/// in a quantity of type <typeparamref name="TQuotient"/>.</summary>
/// <typeparam name="TQuotient">The three-dimensional vector quantity that represents the result of { <typeparamref name="TDividend"/> /
/// <see langword="this"/> }.</typeparam>
/// <typeparam name="TDividend">The three-dimensional vector quantity that represents the dividend of { <typeparamref name="TDividend"/> /
/// <see langword="this"/> }.</typeparam>
public interface IDivisor3ScalarQuantity<out TQuotient, in TDividend>
    : IScalarQuantity
    where TQuotient : IVector3Quantity
    where TDividend : IVector3Quantity
{
    /// <summary>Computes { <paramref name="dividend"/> / <see langword="this"/> }.</summary>
    /// <param name="dividend">The dividend of { <paramref name="dividend"/> / <see langword="this"/> }.</param>
    public abstract TQuotient DivideInto(TDividend dividend);
}

/// <summary>Describes a scalar quantity that supports the operation { <typeparamref name="TDividend"/> / <typeparamref name="TSelf"/> },
/// resulting in a quantity of type <typeparamref name="TQuotient"/>.</summary>
/// <typeparam name="TSelf">The self-type, <see langword="this"/>.</typeparam>
/// <typeparam name="TQuotient">The three-dimensional vector quantity that represents the result of { <typeparamref name="TDividend"/> /
/// <typeparamref name="TSelf"/> }.</typeparam>
/// <typeparam name="TDividend">The three-dimensional vector quantity that represents the dividend of { <typeparamref name="TDividend"/> /
/// <typeparamref name="TSelf"/> }.</typeparam>
public interface IDivisor3ScalarQuantity<in TSelf, out TQuotient, in TDividend>
    : IDivisor3ScalarQuantity<TQuotient, TDividend>
    where TSelf : IDivisor3ScalarQuantity<TSelf, TQuotient, TDividend>
    where TQuotient : IVector3Quantity
    where TDividend : IVector3Quantity
{
    /// <summary>Computes { <paramref name="a"/> / <paramref name="b"/> }.</summary>
    /// <param name="a">The dividend of { <paramref name="a"/> / <paramref name="b"/> }, a three-dimensional vector quantity.</param>
    /// <param name="b">The divisor of { <paramref name="a"/> / <paramref name="b"/> }, a scalar quantity.</param>
    [SuppressMessage("Usage", "CA2225", Justification = "False positive, available in base interface")]
    public static abstract TQuotient operator /(TDividend a, TSelf b);
}
