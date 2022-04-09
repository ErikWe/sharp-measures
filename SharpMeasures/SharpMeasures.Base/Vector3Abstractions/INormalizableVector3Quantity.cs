namespace SharpMeasures.Vector3Abstractions;

/// <summary>Describes a three-dimensional vector quantity that may be normalized.</summary>
/// <typeparam name="TNormalizedVector3Quantity">The three-dimensional vector quantity that is the normalized version of the original quantity,
/// generally <see langword="this"/>.</typeparam>
public interface INormalizableVector3Quantity<out TNormalizedVector3Quantity> :
    IVector3Quantity
    where TNormalizedVector3Quantity : IVector3Quantity
{
    /// <summary>Normalizes the quantity.</summary>
    public abstract TNormalizedVector3Quantity Normalize();
}
