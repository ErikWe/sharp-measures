namespace SharpMeasures.Vector3Abstractions;

/// <summary>Describes a three-dimensional vector quantity that supports the operation { <see langword="this"/> ⨯ <typeparamref name="TFactor"/> },
/// resulting in a quantity of type <typeparamref name="TProduct"/>.</summary>
/// <typeparam name="TProduct">The three-dimensional vector quantity that represents the result of { <see langword="this"/> ⨯
/// <typeparamref name="TFactor"/> }.</typeparam>
/// <typeparam name="TFactor">The three-dimensional vector quantity that represents the second factor of { <see langword="this"/> ⨯
/// <typeparamref name="TFactor"/> }.</typeparam>
public interface ICrossFactorVector3Quantity<out TProduct, in TFactor> :
    IVector3Quantity
    where TProduct : IVector3Quantity
    where TFactor : IVector3Quantity
{
    /// <summary>Computes { <see langword="this"/> ⨯ <paramref name="factor"/> }.</summary>
    /// <param name="factor">The second factor of { <see langword="this"/> ⨯ <paramref name="factor"/> }.</param>
    public abstract TProduct Cross(TFactor factor);

    /// <summary>Computes { <paramref name="factor"/> ⨯ <see langword="this"/> }.</summary>
    /// <param name="factor">The first factor of { <paramref name="factor"/> ⨯ <see langword="this"/> }.</param>
    public abstract TProduct CrossInto(TFactor factor);
}
