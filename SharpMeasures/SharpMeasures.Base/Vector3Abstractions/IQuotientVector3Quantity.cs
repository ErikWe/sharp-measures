namespace SharpMeasures.Vector3Abstractions;

using System.Diagnostics.CodeAnalysis;

/// <summary>Describes a three-dimensional vector quantity as { <typeparamref name="TDividend"/> / <typeparamref name="TDivisor"/> }.</summary>
/// <typeparam name="TSelf">The self-type, <see langword="this"/>.</typeparam>
/// <typeparam name="TDividend">The three-dimensional vector quantity that represents the dividend of { <typeparamref name="TDividend"/> /
/// <typeparamref name="TDivisor"/> }.</typeparam>
/// <typeparam name="TDivisor">The scalar quantity that represents the divisor of { <typeparamref name="TDividend"/> / <typeparamref name="TDivisor"/> }.</typeparam>
public interface IQuotientVector3Quantity<out TSelf, in TDividend, in TDivisor> :
    IVector3Quantity
    where TSelf : IQuotientVector3Quantity<TSelf, TDividend, TDivisor>
    where TDividend : IVector3Quantity
    where TDivisor : IScalarQuantity
{
    /// <summary>Computes { <paramref name="a"/> / <paramref name="b"/> }.</summary>
    /// <param name="a">The dividend of { <paramref name="a"/> / <paramref name="b"/> }, a three-dimensional vector quantity.</param>
    /// <param name="b">The divisor of { <paramref name="a"/> / <paramref name="b"/> }, a scalar quantity.</param>
    [SuppressMessage("Design", "CA1000")]
    public static abstract TSelf From(TDividend a, TDivisor b);
}
