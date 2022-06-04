namespace SharpMeasures.ScalarAbstractions;

using System.Diagnostics.CodeAnalysis;

/// <summary>Describes a scalar quantity as { <typeparamref name="TDividend"/> / <typeparamref name="TDivisor"/> }.</summary>
/// <typeparam name="TSelf">The self-type, <see langword="this"/>.</typeparam>
/// <typeparam name="TDividend">The scalar quantity that represents the dividend of { <typeparamref name="TDividend"/> / <typeparamref name="TDivisor"/> }.</typeparam>
/// <typeparam name="TDivisor">The scalar quantity that represents the divisor of { <typeparamref name="TDividend"/> / <typeparamref name="TDivisor"/> }.</typeparam>
public interface IQuotientScalarQuantity<out TSelf, in TDividend, in TDivisor> :
    IScalarQuantity
    where TSelf : IProductScalarQuantity<TSelf, TDividend, TDivisor>
    where TDividend : IScalarQuantity
    where TDivisor : IScalarQuantity
{
    /// <summary>Computes { <paramref name="x"/> / <paramref name="y"/> }.</summary>
    /// <param name="x">The dividend of { <paramref name="x"/> / <paramref name="y"/> }.</param>
    /// <param name="y">The divisor of { <paramref name="x"/> / <paramref name="y"/> }</param>
    [SuppressMessage("Design", "CA1000")]
    public static abstract TSelf From(TDividend x, TDivisor y);
}
