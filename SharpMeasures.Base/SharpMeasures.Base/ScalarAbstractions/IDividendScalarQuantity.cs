namespace SharpMeasures.ScalarAbstractions;

using System.Diagnostics.CodeAnalysis;

/// <summary>Describes a scalar quantity that supports the operation { <see langword="this"/> / <typeparamref name="TDivisor"/> }, resulting in a quantity of type
/// <typeparamref name="TQuotient"/>.</summary>
/// <typeparam name="TQuotient">The scalar quantity that represents the result of { <see langword="this"/> / <typeparamref name="TDivisor"/> }.</typeparam>
/// <typeparam name="TDivisor">The scalar quantity that represents the divisor of { <see langword="this"/> / <typeparamref name="TDivisor"/> }.</typeparam>
public interface IDividendScalarQuantity<out TQuotient, in TDivisor>
    : IScalarQuantity
    where TQuotient : IScalarQuantity
    where TDivisor : IScalarQuantity
{
    /// <summary>Computes { <see langword="this"/> / <paramref name="divisor"/> }.</summary>
    /// <param name="divisor">The divisor of { <see langword="this"/> / <paramref name="divisor"/> }.</param>
    public abstract TQuotient Divide(TDivisor divisor);
}

/// <summary>Describes a scalar quantity that supports the operation { <typeparamref name="TSelf"/> / <typeparamref name="TDivisor"/> }, resulting in a quantity of type
/// <typeparamref name="TQuotient"/>.</summary>
/// <typeparam name="TSelf">The self-type, <see langword="this"/>.</typeparam>
/// <typeparam name="TQuotient">The scalar quantity that represents the result of { <typeparamref name="TSelf"/> / <typeparamref name="TDivisor"/> }.</typeparam>
/// <typeparam name="TDivisor">The scalar quantity that represents the divisor of { <typeparamref name="TSelf"/> / <typeparamref name="TDivisor"/> }.</typeparam>
public interface IDividendScalarQuantity<in TSelf, out TQuotient, in TDivisor>
    : IDividendScalarQuantity<TQuotient, TDivisor>
    where TSelf : IDividendScalarQuantity<TSelf, TQuotient, TDivisor>
    where TQuotient : IScalarQuantity
    where TDivisor : IScalarQuantity
{
    /// <summary>Computes { <paramref name="x"/> / <paramref name="y"/> }.</summary>
    /// <param name="x">The first factor of { <paramref name="x"/> / <paramref name="y"/> }.</param>
    /// <param name="y">The second factor of { <paramref name="x"/> / <paramref name="y"/> }.</param>
    [SuppressMessage("Usage", "CA2225", Justification = "False positive, available in base interface")]
    public static abstract TQuotient operator /(TSelf x, TDivisor y);
}
