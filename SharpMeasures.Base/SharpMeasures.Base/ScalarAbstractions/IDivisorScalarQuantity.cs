namespace SharpMeasures.ScalarAbstractions;

using System.Diagnostics.CodeAnalysis;

/// <summary>Describes a scalar quantity that supports the operation { <typeparamref name="TDividend"/> / <see langword="this"/> }, resulting in a quantity of type
/// <typeparamref name="TQuotient"/>.</summary>
/// <typeparam name="TQuotient">The scalar quantity that represents the result of { <typeparamref name="TDividend"/> / <see langword="this"/> }.</typeparam>
/// <typeparam name="TDividend">The scalar quantity that represents the dividend of { <typeparamref name="TDividend"/> / <see langword="this"/> }.</typeparam>
public interface IDivisorScalarQuantity<out TQuotient, in TDividend>
    : IScalarQuantity
    where TQuotient : IScalarQuantity
    where TDividend : IScalarQuantity
{
    /// <summary>Computes { <paramref name="dividend"/> / <see langword="this"/> }.</summary>
    /// <param name="dividend">The dividend of { <paramref name="dividend"/> / <see langword="this"/> }.</param>
    public abstract TQuotient DivideInto(TDividend dividend);
}

/// <summary>Describes a scalar quantity that supports the operation { <typeparamref name="TDividend"/> / <typeparamref name="TSelf"/> }, resulting in a quantity of type
/// <typeparamref name="TQuotient"/>.</summary>
/// <typeparam name="TSelf">The self-type, <see langword="this"/>.</typeparam>
/// <typeparam name="TQuotient">The scalar quantity that represents the result of { <typeparamref name="TDividend"/> / <typeparamref name="TSelf"/> }.</typeparam>
/// <typeparam name="TDividend">The scalar quantity that represents the dividend of { <typeparamref name="TDividend"/> / <typeparamref name="TSelf"/> }.</typeparam>
public interface IDivisorScalarQuantity<in TSelf, out TQuotient, in TDividend>
    : IDivisorScalarQuantity<TQuotient, TDividend>
    where TSelf : IDivisorScalarQuantity<TSelf, TQuotient, TDividend>
    where TQuotient : IScalarQuantity
    where TDividend : IScalarQuantity
{
    /// <summary>Computes { <paramref name="x"/> / <paramref name="y"/> }.</summary>
    /// <param name="x">The dividend of { <paramref name="x"/> / <paramref name="y"/> }.</param>
    /// <param name="y">The divisor of { <paramref name="x"/> / <paramref name="y"/> }.</param>
    [SuppressMessage("Usage", "CA2225", Justification = "Available as 'DivideInto'")]
    public static abstract TQuotient operator /(TDividend x, TSelf y);
}
