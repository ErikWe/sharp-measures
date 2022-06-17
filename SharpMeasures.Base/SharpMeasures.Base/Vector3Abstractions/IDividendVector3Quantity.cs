namespace SharpMeasures.Vector3Abstractions;

using System.Diagnostics.CodeAnalysis;

/// <summary>Describes a three-dimensional vector quantity that supports the operation { <see langword="this"/> / <typeparamref name="TDivisor"/> }, resulting
/// in a quantity of type <typeparamref name="TQuotient"/>.</summary>
/// <typeparam name="TQuotient">The three-dimensional vector quantity that represents the result of { <see langword="this"/> / <typeparamref name="TDivisor"/> }.</typeparam>
/// <typeparam name="TDivisor">The scalar quantity that represents the divisor of { <see langword="this"/> / <typeparamref name="TDivisor"/> }.</typeparam>
public interface IDividendVector3Quantity<out TQuotient, in TDivisor>
    : IVector3Quantity
    where TQuotient : IVector3Quantity
    where TDivisor : IScalarQuantity
{
    /// <summary>Computes { <see langword="this"/> / <paramref name="divisor"/> }.</summary>
    /// <param name="divisor">The divisor of { <see langword="this"/> / <paramref name="divisor"/> }.</param>
    public abstract TQuotient Divide(TDivisor divisor);
}

/// <summary>Describes a three-dimensional vector quantity that supports the operation { <typeparamref name="TSelf"/> / <typeparamref name="TDivisor"/> },
/// resulting in a quantity of type <typeparamref name="TQuotient"/>.</summary>
/// <typeparam name="TSelf">The self-type, <see langword="this"/>.</typeparam>
/// <typeparam name="TQuotient">The three-dimensional vector quantity that represents the result of { <typeparamref name="TSelf"/> /
/// <typeparamref name="TDivisor"/> }.</typeparam>
/// <typeparam name="TDivisor">The scalar quantity that represents the divisor of { <typeparamref name="TSelf"/> / <typeparamref name="TDivisor"/> }.</typeparam>
public interface IDividendVector3Quantity<in TSelf, out TQuotient, in TDivisor>
    : IDividendVector3Quantity<TQuotient, TDivisor>
    where TSelf : IDividendVector3Quantity<TSelf, TQuotient, TDivisor>
    where TQuotient : IVector3Quantity
    where TDivisor : IScalarQuantity
{
    /// <summary>Computes { <paramref name="a"/> / <paramref name="b"/> }.</summary>
    /// <param name="a">The first factor of { <paramref name="a"/> / <paramref name="b"/> }, a three-dimensional vector quantity.</param>
    /// <param name="b">The second factor of { <paramref name="a"/> / <paramref name="b"/> }, a scalar quantity.</param>
    [SuppressMessage("Usage", "CA2225", Justification = "False positive, available in base interface")]
    public static abstract TQuotient operator /(TSelf a, TDivisor b);
}
