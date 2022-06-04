namespace SharpMeasures.ScalarAbstractions;

using System.Diagnostics.CodeAnalysis;

/// <summary>Describes a scalar quantity as { <typeparamref name="TFactor1"/> ∙ <typeparamref name="TFactor2"/> }.</summary>
/// <typeparam name="TSelf">The self-type, <see langword="this"/>.</typeparam>
/// <typeparam name="TFactor1">The scalar quantity that represents the first factor of { <typeparamref name="TFactor1"/> ∙ <typeparamref name="TFactor2"/> }.</typeparam>
/// <typeparam name="TFactor2">The scalar quantity that represents the second factor of { <typeparamref name="TFactor1"/> ∙ <typeparamref name="TFactor2"/> }.</typeparam>
public interface IProductScalarQuantity<out TSelf, in TFactor1, in TFactor2> :
    IScalarQuantity
    where TSelf : IProductScalarQuantity<TSelf, TFactor1, TFactor2>
    where TFactor1 : IScalarQuantity
    where TFactor2 : IScalarQuantity
{
    /// <summary>Computes { <paramref name="x"/> ∙ <paramref name="y"/> }.</summary>
    /// <param name="x">The first factor of { <paramref name="x"/> ∙ <paramref name="y"/> }.</param>
    /// <param name="y">The second factor of { <paramref name="x"/> ∙ <paramref name="y"/> }</param>
    [SuppressMessage("Design", "CA1000")]
    public static abstract TSelf From(TFactor1 x, TFactor2 y);
}
